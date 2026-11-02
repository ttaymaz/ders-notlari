# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: aynı veriyi tablo olarak saklayıp farkı ölçmek.
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
VERI = "/Volumes/workspace/default/trafik"

# Tablo adı — katalog.şema.tablo. Volume'unuzla aynı katalog ve şema.
TABLO = "workspace.default.trafik_tablo"

# COMMAND ----------

# Son kez CSV okuyoruz. Bundan sonra tablo okuyacağız.
trafik = spark.read.csv(VERI, header=True, inferSchema=True)
trafik.createOrReplaceTempView("ham")

# COMMAND ----------

# Süre ölçmek için. Sonucu collect() ile çeker, yani YALNIZCA sonucu küçük
# olan sorgularda kullanın; yoksa ölçtüğünüz şey taşıma olur.
import time

def olc(sorgu, ad):
    t0 = time.time()
    sonuc = spark.sql(sorgu).collect()
    sure = time.time() - t0
    print(f"{ad:28s} {sure:6.2f} sn   ({len(sonuc)} satir)")
    return sure

# COMMAND ----------

# 1) ÖNCE CSV ÜZERİNDEN ÖLÇ
# Karşılaştırmanın başlangıç noktası. İki kez çalıştırıyoruz: ilk süre
# kaynak tahsisini de içeriyor.
SORGU_CSV = """
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM ham
    GROUP BY GEOHASH
"""

olc(SORGU_CSV, "csv - 1. olcum")
olc(SORGU_CSV, "csv - 2. olcum")

# COMMAND ----------

# 2) TABLOYU KURMAK
# Tarihi BİR KEZ çeviriyoruz; artık her not defterinde baştan yazmayacağız.
# Bu hücre bugünün en yavaşı olacak: veri okunmakla kalmıyor, yazılıyor da.
# Bir kez öde, her sorguda kazan.
spark.sql(f"""
    CREATE OR REPLACE TABLE {TABLO} AS
    SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman,
           GEOHASH,
           AVERAGE_SPEED,
           NUMBER_OF_VEHICLES
    FROM ham
""")

print("tablo kuruldu:", TABLO)

# COMMAND ----------

# 3) TABLO NE OLDU?
# Üç sütuna bakın: format (Delta olmalı), numFiles, sizeInBytes.
# CSV 232 MB idi. sizeInBytes bundan belirgin biçimde küçük çıkacak:
# aynı sütunun değerleri birbirine benzer ve yan yana olunca iyi sıkışır.
display(spark.sql(f"DESCRIBE DETAIL {TABLO}"))

# COMMAND ----------

# Şema artık yazılı. inferSchema ile her okumada tip çıkarmak yok, ve tip
# bir sözleşme oldu: AVERAGE_SPEED sayıysa oraya metin yazılamaz.
display(spark.sql(f"DESCRIBE TABLE {TABLO}"))

# COMMAND ----------

# 4) ŞİMDİ TABLO ÜZERİNDEN ÖLÇ
# ÇALIŞTIRMADAN ÖNCE TAHMİN YAZIN: kaç kat hızlanmasını bekliyorsunuz?
SORGU_TABLO = f"""
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM {TABLO}
    GROUP BY GEOHASH
"""

olc(SORGU_TABLO, "tablo - 1. olcum")
olc(SORGU_TABLO, "tablo - 2. olcum")

# COMMAND ----------

# 5) PLANLARI KARŞILAŞTIRMAK
# Geçen hafta CSV planında iki satır görmüştük:
#   Batched: false
#   PhotonRowToColumnar
# Sütunlu biçimde o çevirme adımı gerekmiyor. Tablo planında var mı?
spark.sql(f"EXPLAIN {SORGU_TABLO}").show(truncate=False)

# COMMAND ----------

# 6) DOSYA ATLAMA
# Tablo her dosya için en küçük ve en büyük değeri saklar. Bir dosyadaki en
# büyük zaman 2024-12-20 ise, 2025 Ocak soran bir sorgu o dosyayı hiç açmaz.
# CSV'de bu mümkün değildi: içinde ne olduğunu anlamanın tek yolu okumaktı.
olc(f"SELECT COUNT(*) AS satir FROM {TABLO}", "tablo - tamami")
olc(f"SELECT COUNT(*) AS satir FROM {TABLO} WHERE zaman >= '2025-01-15'",
    "tablo - son iki hafta")

# COMMAND ----------

# Fark küçük çıktıysa sebebi şu olabilir: dosya atlama verinin SIRALI
# olmasına bağlıdır. Zaman değerleri bütün dosyalara karışık dağılmışsa her
# dosyanın aralığı bütün dönemi kapsar ve atlanacak dosya kalmaz.
# İstatistik vardır, ama işe yaramaz.

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. Tabloyu ORDER BY zaman ile yeniden yazın, sonra 6) numaralı hücreyi
#    tekrar çalıştırın. Sıralama atlamayı mümkün kıldı mı?
# 2. Yalnızca iki sütunlu ikinci bir tablo kurun. sizeInBytes, dört
#    sütunlunun kaçta kaçı?
# 3. DESCRIBE HISTORY ile tablonun geçmişine bakın. Kaç sürüm var?
# 4. OPTIMIZE {TABLO} çalıştırıp DESCRIBE DETAIL'e yeniden bakın.
#    numFiles değişti mi?
