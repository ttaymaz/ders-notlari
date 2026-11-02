# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: iki tabloyu bir anahtar üzerinden birleştirmek.
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
VERI = "/Volumes/workspace/default/trafik"

# COMMAND ----------

# Tanıdık başlangıç: klasörü oku, tarihi bir kez çevir, görünüm kur.
trafik = spark.read.csv(VERI, header=True, inferSchema=True)
trafik.createOrReplaceTempView("ham")

spark.sql("""
    CREATE OR REPLACE TEMP VIEW trafik AS
    SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman,
           GEOHASH, AVERAGE_SPEED, NUMBER_OF_VEHICLES
    FROM ham
""")

# Geçen haftanın günlük serisi; bu haftanın sol tablosu olacak.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW gunluk AS
    SELECT to_date(zaman) AS gun,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac,
           ROUND(AVG(AVERAGE_SPEED), 1) AS ortalama_hiz
    FROM trafik
    GROUP BY gun
""")

# COMMAND ----------

# 1) OLMASI GEREKENİ ÜRETMEK
# sequence iki tarih arasını doldurur, explode diziyi satırlara açar.
# Bu tablo ÖLÇÜLMEDİ, ÜRETİLDİ: 62 satır beklentimiz takvimden geliyor.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW takvim AS
    SELECT explode(sequence(DATE '2024-12-01',
                            DATE '2025-01-31',
                            INTERVAL 1 DAY)) AS gun
""")

display(spark.sql("""
    SELECT (SELECT COUNT(*) FROM takvim) AS takvim_gun,
           (SELECT COUNT(*) FROM gunluk) AS olculen_gun
"""))

# COMMAND ----------

# 2) INNER JOIN — YALNIZCA EŞLEŞENLER
# Eşleşmeyen satır sonuçta hiç yer almaz. Bu yüzden eksik günü BU SORGU
# ile bulmak imkânsız: aradığımız satır tam olarak atılan satır.
display(spark.sql("""
    SELECT t.gun, g.toplam_arac
    FROM takvim t
    JOIN gunluk g ON t.gun = g.gun
    ORDER BY t.gun
    LIMIT 5
"""))

# COMMAND ----------

# 3) LEFT JOIN + IS NULL — EKSİK OLANI GÖRÜNÜR KILMAK
# LEFT JOIN soldaki her satırı korur; sağda eşleşme yoksa NULL bırakır.
# IS NULL denetimi ANAHTAR sütununda yapılır (g.gun), ölçülmüş sütunda
# değil: ölçülmüş bir sütun gerçekten boş olabilir.
display(spark.sql("""
    SELECT t.gun
    FROM takvim t
    LEFT JOIN gunluk g ON t.gun = g.gun
    WHERE g.gun IS NULL
    ORDER BY t.gun
"""))

# COMMAND ----------

# Hiç satır dönmediyse seride boşluk yok. "Eksik yok" da bir bulgudur;
# bakmadan bilemezdiniz.

# COMMAND ----------

# 4) DIŞARIDAN BİLGİ EKLEMEK
# Trafik verisinde hangi günün özel olduğu yazmıyor. Bu tabloyu BİZ
# yazıyoruz — yani kaynağı bir sistem değil, alan bilgisi olan insan.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW ozel_gunler AS
    SELECT * FROM VALUES
        (DATE '2024-12-31', 'yilbasi gecesi'),
        (DATE '2025-01-01', 'yilbasi tatili')
    AS t(gun, aciklama)
""")

# Birleştirmeden ÖNCE: anahtar sağ tabloda tek mi?
# İki sayı eşit değilse satır çoğalması olur ve sorgu hata vermez.
display(spark.sql("""
    SELECT COUNT(*) AS satir, COUNT(DISTINCT gun) AS farkli_gun
    FROM ozel_gunler
"""))

# COMMAND ----------

# Zenginleştirme: 62 satır korunur, ikisinde aciklama dolu gelir.
zengin = spark.sql("""
    SELECT g.gun, g.toplam_arac, g.ortalama_hiz, o.aciklama
    FROM gunluk g
    LEFT JOIN ozel_gunler o ON g.gun = o.gun
    ORDER BY g.gun
""")

# Birleştirmeden SONRA: satır sayısı sol tablodan fazla mı?
print("gunluk :", spark.table("gunluk").count())
print("zengin :", zengin.count())

display(zengin.filter("aciklama IS NOT NULL"))

# COMMAND ----------

# 5) İŞARETLİ GÜNLERİ GRAFİKTE GÖSTERMEK
# Zenginleştirmenin karşılığı burada görünür: artık hangi günün neden
# farklı olduğunu grafiğin üstüne yazabiliyoruz.
import matplotlib.pyplot as plt

z = zengin.toPandas()
isaretli = z[z["aciklama"].notna()]

fig, ax = plt.subplots(figsize=(12, 4))
ax.plot(z["gun"], z["toplam_arac"], marker="o", markersize=3, label="Günlük")
ax.scatter(isaretli["gun"], isaretli["toplam_arac"],
           s=80, zorder=3, label="İşaretli gün")
for _, satir in isaretli.iterrows():
    ax.annotate(satir["aciklama"], (satir["gun"], satir["toplam_arac"]),
                textcoords="offset points", xytext=(0, 10), ha="center")
ax.set_xlabel("Gün")
ax.set_ylabel("Toplam araç")
ax.set_title("Günlük seri ve işaretlenmiş özel günler")
ax.legend()
fig.autofmt_xdate()
display(fig)

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. 3) numaralı hücreden WHERE g.gun IS NULL koşulunu kaldırın. Kaç satır
#    döndü ve neden?
# 2. ozel_gunler tablosuna aynı günü BİLEREK iki kez yazın. zengin.count()
#    kaç oldu? Toplam araç sayısını alırsanız ne olur?
# 3. takvim görünümünü INTERVAL 1 HOUR ile saatlik kurun. 62 x 24 = 1488
#    saat bekliyoruz; kaç saat eksik çıktı?
# 4. 2) numaralı hücredeki JOIN'i LEFT JOIN yapın. Satır sayısı değişti mi?
#    Değişmediyse bu veri hakkında ne söylüyor?
