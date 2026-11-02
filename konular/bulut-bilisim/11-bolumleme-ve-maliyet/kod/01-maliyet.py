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
# Beklenen: 8 bölüm, her birinde ~382.000 satır, toplamı 3.030.359.
display(spark.sql("""
    SELECT spark_partition_id() AS bolum, COUNT(*) AS satir
    FROM ham
    GROUP BY bolum
    ORDER BY bolum
"""))

# COMMAND ----------

# Bölümlerden biri diğerlerinin on katı büyükse diğer çekirdekler bekler ve
# sorgu EN YAVAŞ BÖLÜM kadar sürer. Bunun adı çarpıklık (skew).
# Bu veride çarpıklık YOK: Spark CSV'yi bayta göre bölüyor ve satırlarımız
# hemen hemen aynı uzunlukta. Her veri kümesi böyle gelmez.

# COMMAND ----------

# 2) DAR DÖNÜŞÜM — PLANDA EXCHANGE YOK
# WHERE bir satırı atarken başka satıra bakmaz; her bölüm kendi işini yapar.
# Çıktı uzun; tek bir kelime arıyoruz: Exchange
#
# Düğüm adları Photon ile başlıyor (PhotonFilter, PhotonProject): ortam
# sorguları Photon motoruyla yürütüyor. Aradığımız kelime adın İÇİNDE geçer.
#
# Bu planda üç satır daha var, üçü de işimize yarıyor:
#   PushedFilters  -> süzme okuma adımına kadar inmiş
#   ReadSchema     -> sekiz sütunlu dosyadan yalnızca iki sütun okunuyor
#   Batched: false -> CSV toplu okunamıyor (gelecek haftanın konusu)
spark.sql("""
    EXPLAIN
    SELECT GEOHASH FROM trafik WHERE AVERAGE_SPEED > 100
""").show(truncate=False)

# COMMAND ----------

# 3) GENİŞ DÖNÜŞÜM — PLANDA EXCHANGE VAR
# GROUP BY aynı anahtarın satırlarını aynı makinede buluşturmak zorunda.
# Exchange, karıştırmanın plandaki adı: PhotonShuffleExchangeSink.
#
# Planda gruplamanın İKİ aşamada yapıldığını göreceksiniz:
#   partial_count    -> karıştırmadan ÖNCE, her bölüm kendi içinde sayar
#   finalmerge_count -> karıştırmadan SONRA, kısmi sayılar birleşir
# Karıştırmada taşınan şey üç milyon satır değil, kısmi sayılar. Sorgunun
# 1,3 saniyede bitmesinin sebebi bu.
#
# Karıştırma bölüm sayısını da değiştirir: hashpartitioning(GEOHASH, 231).
spark.sql("""
    EXPLAIN
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM trafik
    GROUP BY GEOHASH
""").show(truncate=False)

# COMMAND ----------

# 4) GEÇEN HAFTANIN İDDİASINI KANITLAMAK
# "Küçük tablo her makineye kopyalanır" demiştik. Planda görünecek:
# PhotonBroadcastHashJoin ... LeftOuter, BuildRight
# BuildRight = sagdaki, yani KUCUK tablo yayinlaniyor. VALUES ile yazdigimiz
# tablo icin LocalTableScan yazar: depolamadan hic okunmuyor.
#
# DIKKAT: bu planda Exchange de var, ama birlestirmeden DEGIL. gunluk
# gorunumunun kendisi bir GROUP BY; o karistirma ona ait.
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
#
# DİKKAT: olc() sonucu collect() ile sürücüye çeker. Yani ölçtüğü şey
# "çalıştırma + sürücüye taşıma". Bu yüzden YALNIZCA sonucu küçük olan
# sorgularda kullanın. Üç milyon satır döndüren bir sorguda ölçtüğünüz şey
# sorgunun maliyeti değil, taşımanın maliyeti olur (ölçüldü: 48,63 saniye).
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

# Beklenen: ikinci ölçüm daha kısa (ölçüldü: 1,54 ve 1,28 saniye).
olc(SORGU, "gruplama - 1. olcum")
olc(SORGU, "gruplama - 2. olcum")

# COMMAND ----------

# 6) ÖNCE SÜZMEK NE KAZANDIRIYOR?
# Aynı gruplama, ama önce satır sayısı azaltılmış.
#
# TAHMİN EDİN, SONRA ÇALIŞTIRIN. Ölçtüğümüzde süzmek işi UZATTI:
# 1,25 saniyeye karşı 1,42 saniye. Sebebi pahalı adımın dosyayı okumak
# olması; süzmek okunan satır sayısını azaltmıyor. Gruplama da zaten 2.460
# gruba düşüyordu, karıştırılacak veri baştan küçüktü.
#
# Ders: "önce süz" iyi bir tahmindir, yasa değildir. Ölçmeden iyileştirme
# yapmayın.
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

# 7) AZ SÜTUN OKUMANIN KANITI PLANDA
# Sütun budamayı süre ölçerek göstermek yanıltıcı olur: SELECT * sonucunu
# çekmek üç milyon satır taşımak demektir ve ölçtüğünüz şey artık sütun
# sayısı değil, taşıma olur (ölçüldü: 48,63 saniye).
#
# Doğru yer plan. ReadSchema satırına bakın.
spark.sql("""
    EXPLAIN
    SELECT GEOHASH, AVERAGE_SPEED FROM ham WHERE AVERAGE_SPEED > 100
""").show(truncate=False)

# Beklenen: ReadSchema: struct<GEOHASH:string,AVERAGE_SPEED:int>
# Sekiz sütunlu dosyadan iki sütun. Sütun budama tahmin değil, planda yazılı.

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. EXPLAIN çıktısını ORDER BY ve DISTINCT için de alın. Kaç Exchange var?
# 2. 5) numaralı hücreyi üçüncü, dördüncü kez çalıştırın. Süre düşmeye
#    devam ediyor mu, bir yerde duruyor mu?
# 3. inferSchema=True olmadan okuyup ilk hücrenin süresini karşılaştırın.
#    (Sütunlar metin gelecek; sayısal işlem için dönüştürmek gerekir.)
# 3b. ISTEGE BAGLI, YAVAS: olc("SELECT * FROM ham", "hepsi") calistirin.
#    Olculdu: 48,63 saniye. Bu hucre bilerek yavas; besinci haftanin
#    toPandas tuzaginin olculmus hali. Kotanizi dusunerek calistirin.
# 4. ozel_gunler tablosuna iki yüz bin satır ekleyin. Plan hâlâ
#    BroadcastHashJoin mi, yoksa karıştırmaya mı döndü?
