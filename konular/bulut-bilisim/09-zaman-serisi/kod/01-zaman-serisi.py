# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: veriye zaman ekseni üzerinden bakmak.
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

# COMMAND ----------

# 1) TARİHTEN PARÇA ÇIKARMAK
# Tek bir zaman değerinden kaç farklı soru sorulabileceğine bakalım.
display(spark.sql("""
    SELECT zaman,
           year(zaman)       AS yil,
           month(zaman)      AS ay,
           day(zaman)        AS gun,
           hour(zaman)       AS saat,
           dayofweek(zaman)  AS haftanin_gunu,
           weekofyear(zaman) AS hafta_no
    FROM trafik
    LIMIT 5
"""))

# COMMAND ----------

# 2) GÜNLÜK SERİYE İNDİRGEMEK
# Saatlik veri üç milyon satır; günlük seri 62 satır olmalı (Aralık + Ocak).
# to_date saati atar, geriye yalnızca gün kalır.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW gunluk AS
    SELECT to_date(zaman) AS gun,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac,
           ROUND(AVG(AVERAGE_SPEED), 1) AS ortalama_hiz,
           COUNT(*) AS olcum_sayisi
    FROM trafik
    GROUP BY gun
""")

display(spark.sql("SELECT COUNT(*) AS gun_sayisi FROM gunluk"))

# COMMAND ----------

# Beklenen 62 (31 + 31). Farklıysa bazı günler hiç ölçülmemiş demektir.
# HANGİ günlerin eksik olduğunu bulmak iki tabloyu birleştirmeyi gerektirir;
# o, gelecek haftanın konusu.
display(spark.sql("SELECT * FROM gunluk ORDER BY gun LIMIT 10"))

# COMMAND ----------

# 3) SERİYİ ÇİZMEK
# 62 satır; önce küçült kuralı zaten sağlanmış durumda.
import matplotlib.pyplot as plt

g = spark.sql("SELECT * FROM gunluk ORDER BY gun").toPandas()

fig, ax = plt.subplots(figsize=(12, 4))
ax.plot(g["gun"], g["toplam_arac"], marker="o", markersize=3)
ax.set_xlabel("Gün")
ax.set_ylabel("Toplam araç")
ax.set_title("Günlük trafik yoğunluğu")
fig.autofmt_xdate()
display(fig)

# COMMAND ----------

# 4) HAREKETLİ ORTALAMA
# Günlük seri dalgalı: hafta içi yüksek, hafta sonu düşük. Eğilimi görmek
# için yedi günlük hareketli ortalama alıyoruz — her gün, kendisi ve
# önceki altı günün ortalamasıyla değiştiriliyor.
hareketli = spark.sql("""
    SELECT gun,
           toplam_arac,
           ROUND(AVG(toplam_arac) OVER (
               ORDER BY gun ROWS BETWEEN 6 PRECEDING AND CURRENT ROW
           ), 0) AS yedi_gun_ortalama
    FROM gunluk
    ORDER BY gun
""")

display(hareketli)

# COMMAND ----------

# İki seriyi üst üste çizelim: ham veri ve yumuşatılmış eğilim.
h = hareketli.toPandas()

fig, ax = plt.subplots(figsize=(12, 4))
ax.plot(h["gun"], h["toplam_arac"], alpha=0.4, label="Günlük")
ax.plot(h["gun"], h["yedi_gun_ortalama"], linewidth=2, label="7 günlük ortalama")
ax.set_xlabel("Gün")
ax.set_ylabel("Toplam araç")
ax.set_title("Günlük seri ve eğilim")
ax.legend()
fig.autofmt_xdate()
display(fig)

# COMMAND ----------

# 5) PROFİL KARŞILAŞTIRMA
# Hafta içi ve hafta sonu günün hangi saatinde farklılaşıyor?
# Kapsama eşit olmayabileceği için toplam değil, ölçüm başına ortalama.
profil = spark.sql("""
    SELECT hour(zaman) AS saat,
           CASE WHEN dayofweek(zaman) IN (1, 7) THEN 'hafta sonu'
                ELSE 'hafta ici' END AS gun_turu,
           ROUND(AVG(NUMBER_OF_VEHICLES), 1) AS ortalama_arac
    FROM trafik
    GROUP BY saat, gun_turu
    ORDER BY saat
""").toPandas()

fig, ax = plt.subplots(figsize=(10, 4))
for tur in ["hafta ici", "hafta sonu"]:
    alt = profil[profil["gun_turu"] == tur]
    ax.plot(alt["saat"], alt["ortalama_arac"], marker="o", label=tur)
ax.set_xlabel("Saat")
ax.set_ylabel("Ölçüm başına ortalama araç")
ax.set_title("Gün profili: hafta içi ve hafta sonu")
ax.legend()
display(fig)

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. Hareketli ortalamada 6 PRECEDING yerine 2 PRECEDING yazın. Eğri neyi
#    kaybetti, neyi kazandı?
# 2. İlk altı günün yedi_gun_ortalama değerine bakın. Neden ham veriye
#    yakınlar? (İpucu: ortalama kaç günden alınıyor?)
# 3. Günlük seride en düşük değerli günü bulun. Hangi güne denk geliyor
#    ve bu beklenen bir şey mi?
# 4. Profil grafiğinde iki eğrinin kesiştiği saatler var mı? Varsa ne anlama
#    gelir?
