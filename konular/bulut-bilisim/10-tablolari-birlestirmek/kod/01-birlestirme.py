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
           ROUND(AVG(AVERAGE_SPEED), 1) AS ortalama_hiz,
           COUNT(*) AS olcum_sayisi
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

# Beklenen çıktı: ALTI satır.
#   2024-12-17, 2024-12-18, 2024-12-22, 2024-12-28, 2024-12-29, 2024-12-30
# Üç küme hâlinde (2 gün, 1 gün, 3 gün), hepsi Aralık'ın ikinci yarısında;
# Ocak'ta tek bir eksik gün yok. Neden kesildiğini veri söylemiyor.

# COMMAND ----------

# 3b) BİRLEŞTİRMENİN BULAMADIĞI
# IS NULL yalnizca HIC SATIRI OLMAYAN gunu yakalar. Satiri olan ama
# kapsamasi cokmus bir gun bu yontemin kor noktasidir: eksik listesinde
# gorunmez. Gun basina olcum sayisina bakinca ortaya cikiyor.
display(spark.sql("""
    SELECT gun, olcum_sayisi, toplam_arac
    FROM gunluk
    ORDER BY olcum_sayisi
    LIMIT 10
"""))

# COMMAND ----------

# 21 Aralik eksik listesinde YOK ama normal bir cumartesinin yuzde ucu
# kadar arac sayilmis (156.826 / ~5.000.000). 19 Aralik da benzer.
# Altinci haftanin ayrimina dorduncu bir durum ekleniyor: satir var,
# deger var, ama kac olcumden geldigi cok az.

# COMMAND ----------

# 3c) BOŞLUKLARI TABLOYA YAZMAK
# Geçen haftanın hareketli ortalaması ROWS BETWEEN 6 PRECEDING kullanıyordu.
# ROWS SATIR sayar, gün saymaz: altı gün eksik olduğu için 31 Aralık'ın
# "yedi günlük ortalaması" aslında 21-31 Aralık aralığından, yani on bir
# günden alınıyordu. Takvimle birleştirince seri 62 satır olur ve yedi
# satır gerçekten yedi gün demeye başlar.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW tam_seri AS
    SELECT t.gun, g.toplam_arac
    FROM takvim t
    LEFT JOIN gunluk g ON t.gun = g.gun
""")

# AVG boş değerleri atlar: pencere yedi gün olur, ortalama o yedi günün
# ÖLÇÜLENLERİNDEN alınır.
display(spark.sql("""
    SELECT gun,
           toplam_arac,
           ROUND(AVG(toplam_arac) OVER (
               ORDER BY gun ROWS BETWEEN 6 PRECEDING AND CURRENT ROW
           ), 0) AS yedi_gun_ortalama
    FROM tam_seri
    ORDER BY gun
"""))

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
# 5. tam_seri üzerinden aldığınız hareketli ortalamayı, geçen haftanın
#    gunluk görünümünden aldığınızla karşılaştırın. Aralık sonunda iki eğri
#    ayrışıyor mu? Ayrışıyorsa hangisi doğru ve neden?
