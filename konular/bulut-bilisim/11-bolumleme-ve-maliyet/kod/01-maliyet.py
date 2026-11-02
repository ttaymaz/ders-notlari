# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: yazdığımız sorguların neye mal olduğunu görmek.
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

# 1) VERİ KAÇ BÖLÜME AYRILMIŞ?
# Her bölüm bir görev, her görev bir çekirdek. Paralelliğin tamamı burada.
# İki şeye bakın: kaç bölüm var, ve bölümler birbirine yakın büyüklükte mi.
display(spark.sql("""
    SELECT spark_partition_id() AS bolum, COUNT(*) AS satir
    FROM ham
    GROUP BY bolum
    ORDER BY bolum
"""))

# COMMAND ----------

# Bölümlerden biri diğerlerinin on katı büyükse diğer çekirdekler bekler ve
# sorgu EN YAVAŞ BÖLÜM kadar sürer. Bunun adı çarpıklık (skew).

# COMMAND ----------

# 2) DAR DÖNÜŞÜM — PLANDA EXCHANGE YOK
# WHERE bir satırı atarken başka satıra bakmaz; her bölüm kendi işini yapar.
# Çıktı uzun; tek bir kelime arıyoruz: Exchange
spark.sql("""
    EXPLAIN
    SELECT GEOHASH FROM trafik WHERE AVERAGE_SPEED > 100
""").show(truncate=False)

# COMMAND ----------

# 3) GENİŞ DÖNÜŞÜM — PLANDA EXCHANGE VAR
# GROUP BY aynı anahtarın satırlarını aynı makinede buluşturmak zorunda.
# Exchange, karıştırmanın plandaki adı.
spark.sql("""
    EXPLAIN
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM trafik
    GROUP BY GEOHASH
""").show(truncate=False)

# COMMAND ----------

# 4) GEÇEN HAFTANIN İDDİASINI KANITLAMAK
# "Küçük tablo her makineye kopyalanır" demiştik. Planda görünecek:
# BroadcastHashJoin. Büyük tablonun satırları hiç taşınmıyor.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW gunluk AS
    SELECT to_date(zaman) AS gun,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac
    FROM trafik
    GROUP BY gun
""")

spark.sql("""
    CREATE OR REPLACE TEMP VIEW ozel_gunler AS
    SELECT * FROM VALUES
        (DATE '2024-12-31', 'yilbasi gecesi'),
        (DATE '2025-01-01', 'yilbasi tatili')
    AS t(gun, aciklama)
""")

spark.sql("""
    EXPLAIN
    SELECT g.gun, o.aciklama
    FROM gunluk g LEFT JOIN ozel_gunler o ON g.gun = o.gun
""").show(truncate=False)

# COMMAND ----------

# 5) SÜREYİ İKİ KEZ ÖLÇMEK
# İlk süre kaynak tahsisini de içerir; sorgunun gerçek maliyeti ikinci
# ölçüme daha yakındır. Tek ölçümle karar vermeyin.
import time

def olc(sorgu, ad):
    t0 = time.time()
    sonuc = spark.sql(sorgu).collect()
    sure = time.time() - t0
    print(f"{ad:28s} {sure:6.2f} sn   ({len(sonuc)} satir)")
    return sure

SORGU = """
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM trafik
    GROUP BY GEOHASH
"""

olc(SORGU, "gruplama - 1. olcum")
olc(SORGU, "gruplama - 2. olcum")

# COMMAND ----------

# 6) ÖNCE SÜZMEK NE KAZANDIRIYOR?
# Aynı gruplama, ama önce satır sayısı azaltılmış. Gruplanan satır azalınca
# karıştırmada taşınan veri de azalır.
SORGU_SUZULMUS = """
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM trafik
    WHERE AVERAGE_SPEED > 100
    GROUP BY GEOHASH
"""

a = olc(SORGU, "suzmeden")
b = olc(SORGU_SUZULMUS, "once suzerek")
print(f"\nfark: {a - b:.2f} sn")

# COMMAND ----------

# 7) SELECT * NE KADARA MAL OLUYOR?
# Kullanmadığınız sütunlar da okunur ve taşınır.
olc("SELECT * FROM ham", "butun sutunlar")
olc("SELECT GEOHASH, AVERAGE_SPEED FROM ham", "iki sutun")

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. EXPLAIN çıktısını ORDER BY ve DISTINCT için de alın. Kaç Exchange var?
# 2. 5) numaralı hücreyi üçüncü, dördüncü kez çalıştırın. Süre düşmeye
#    devam ediyor mu, bir yerde duruyor mu?
# 3. inferSchema=True olmadan okuyup ilk hücrenin süresini karşılaştırın.
#    (Sütunlar metin gelecek; sayısal işlem için dönüştürmek gerekir.)
# 4. ozel_gunler tablosuna iki yüz bin satır ekleyin. Plan hâlâ
#    BroadcastHashJoin mi, yoksa karıştırmaya mı döndü?
