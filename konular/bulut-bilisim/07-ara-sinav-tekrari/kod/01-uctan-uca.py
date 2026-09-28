# Databricks notebook source
# UÇTAN UCA: altı haftanın tamamı tek not defterinde.
#
# Bu dosya iki işe yarar:
#   1. Tekrar — hangi adımın neden orada olduğunu hatırlatır
#   2. Proje iskeleti — kendi analizinizi bunun üstüne kurabilirsiniz
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
VERI = "/Volumes/workspace/default/trafik"

# COMMAND ----------

# ADIM 1 — OKU  (3. ve 5. hafta)
# Klasör yolu verince Spark içindeki bütün CSV'leri okur.
# inferSchema=True olmadan sayılar metin gelir ve toplanamaz (4. hafta).
trafik = spark.read.csv(VERI, header=True, inferSchema=True)
trafik.createOrReplaceTempView("ham")

# COMMAND ----------

# ADIM 2 — HAZIRLA  (4. hafta)
# Tarihi bir kez çeviriyoruz; her sorguda tekrarlamak yerine görünüm kuruyoruz.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW trafik AS
    SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman,
           GEOHASH, LATITUDE, LONGITUDE,
           MINIMUM_SPEED, MAXIMUM_SPEED, AVERAGE_SPEED,
           NUMBER_OF_VEHICLES
    FROM ham
""")

# COMMAND ----------

# ADIM 3 — GÜVEN  (6. hafta)
# Analize başlamadan önce veriye bakılır. Eksik var mı, kapsama eşit mi?
display(spark.sql("""
    SELECT COUNT(*)                  AS satir,
           COUNT(AVERAGE_SPEED)      AS hiz_dolu,
           COUNT(NUMBER_OF_VEHICLES) AS arac_dolu,
           COUNT(DISTINCT zaman, GEOHASH) AS benzersiz_olcum
    FROM trafik
"""))

# COMMAND ----------

# ADIM 4 — SOR  (4. hafta)
# Soruyu önce Türkçe kurun: "günün hangi saatinde en çok araç geçiyor?"
# Kapsama eşit değilse toplam yanıltır; ortalamayı da yanına koyuyoruz (6. hafta).
saatlik = spark.sql("""
    SELECT hour(zaman) AS saat,
           COUNT(*) AS olcum_sayisi,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac,
           ROUND(AVG(NUMBER_OF_VEHICLES), 1) AS nokta_basina_ortalama
    FROM trafik
    GROUP BY saat
    ORDER BY saat
""")

display(saatlik)

# COMMAND ----------

# ADIM 5 — GÖSTER  (5. hafta)
# Önce küçült, sonra çek: toPandas yalnızca 24 satıra uygulanıyor.
import matplotlib.pyplot as plt

p = saatlik.toPandas()

fig, ax = plt.subplots(figsize=(10, 4))
ax.bar(p["saat"], p["nokta_basina_ortalama"])
ax.set_xlabel("Saat")
ax.set_ylabel("Nokta başına ortalama araç")
ax.set_title("Gün içinde trafik yoğunluğu (kapsamaya göre düzeltilmiş)")
display(fig)

# COMMAND ----------

# ADIM 6 — YORUMLA
# Sayı tek başına cevap değildir. Bir cümleyle yazın:
#   "En yoğun saat ..., çünkü ...  Ancak ... nedeniyle bu sonuç ... ile sınırlı."
#
# Bu cümle projenizin de omurgasıdır: sonuç + gerekçe + sınır.

# COMMAND ----------

# Denemeniz için:
#
# 1. ADIM 5'te nokta_basina_ortalama yerine toplam_arac çizin. İki grafik
#    aynı saati mi en yoğun gösteriyor? Değilse hangisine güvenirsiniz?
# 2. ADIM 3'ü silip not defterini baştan çalıştırın. Sonuç değişti mi?
#    Değişmediyse o adım neden var?
# 3. ADIM 2'deki görünümden LATITUDE ve LONGITUDE sütunlarını çıkarın.
#    Hangi adım kırılır, hangisi kırılmaz?
