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

# Olculdu: 1,00 ve 1,00 saniye. (Gecen hafta ayni sorgu 1,28 ve 1,54
# olculmustu -- oturumlar arasi sapma bu kadar.)
olc(SORGU_CSV, "csv - 1. olcum")
olc(SORGU_CSV, "csv - 2. olcum");

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
# Olculdu: format=delta, numFiles=3, sizeInBytes=8.029.833 (7,7 MB).
# CSV 231,8 MB idi -> yaklasik 30 kat kucuk.
#
# DURUST OKUYUN: kazancin tamami sikismadan gelmiyor. CSV'nin sekiz sutunu
# var, tabloya DORDUNU aldik. Hangisinin payi ne kadar? Denemeler-2 bunu
# olcuyor. Ozelliklerde sikistirma algoritmasi da yazili: zstd.
display(spark.sql(f"DESCRIBE DETAIL {TABLO}"))

# COMMAND ----------

# Şema artık yazılı. inferSchema ile her okumada tip çıkarmak yok, ve tip
# bir sözleşme oldu: AVERAGE_SPEED sayıysa oraya metin yazılamaz.
display(spark.sql(f"DESCRIBE TABLE {TABLO}"))

# COMMAND ----------

# 4) ŞİMDİ TABLO ÜZERİNDEN ÖLÇ
# ÇALIŞTIRMADAN ÖNCE TAHMİN YAZIN: kaç kat hızlanmasını bekliyorsunuz?
#
# Olculdu: 0,85 ve 0,74 saniye. Yani boyut 30 kat kuculdu, sure 1,35 kat
# iyilesti. Bu ucurum haftanin asil dersi: bu olcekte zamanin cogu okumaya
# degil sabit giderlere gidiyor (plan, gorev baslatma, karistirma).
# Ustelik CSV'nin kendi olcumleri 1,00 ile 1,54 arasinda degisiyor; sapma
# kazancla ayni mertebede.
SORGU_TABLO = f"""
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM {TABLO}
    GROUP BY GEOHASH
"""

olc(SORGU_TABLO, "tablo - 1. olcum")
olc(SORGU_TABLO, "tablo - 2. olcum");

# COMMAND ----------

# 5) PLANLARI KARŞILAŞTIRMAK
# Geçen hafta CSV planında iki satır görmüştük:
#   Batched: false
#   PhotonRowToColumnar
# Sutunlu bicimde o cevirme adimi gerekmiyor. Olculdu: ikisi de gitti,
# yerine tek satir geldi -> PhotonScan parquet
#
# IKI ADA DIKKAT, neredeyse ayni yaziliyor:
#   PhotonRowToColumnar -> GIRISTE satiri sutuna cevirir. KAYBOLDU.
#   PhotonColumnarToRow -> CIKISTA sonucu satira cevirir. IKI PLANDA DA VAR.
#
# Planin sonunda yeni bir bolum var: Optimizer Statistics, full =
# trafik_tablo. Somut sonucu: karistirma hedefi CSV'de 231 bolum iken
# tabloda 16 bolum -- hashpartitioning(GEOHASH, 16).
spark.sql(f"EXPLAIN {SORGU_TABLO}").show(truncate=False)

# COMMAND ----------

# 6) DOSYA ATLAMA -- VE ATLAMANIN OLMADIGI DURUM
# Tablo her dosya icin en kucuk ve en buyuk degeri saklar. Bir dosyadaki en
# buyuk zaman 2024-12-20 ise, 2025 Ocak soran bir sorgu o dosyayi hic acmaz.
#
# DIKKAT: COUNT(*) bir tabloda VERIYI HIC OKUMAZ; tablo dosya basina satir
# sayisini zaten biliyor. Bu yuzden karsilastirma icin kotu bir baslangic
# noktasidir -- olctugunuz sey okuma degil, muhasebe. (Olculdu: tamami
# 0,58 sn, suzulmusu 0,78 sn; yani SUZULMUS OLAN DAHA YAVAS.)
olc(f"SELECT COUNT(*) AS satir FROM {TABLO}", "tablo - COUNT(*) tamami");

# COMMAND ----------

# Dogru karsilastirma ayni sorguyu iki bicimde olcmektir.
SUZME_TABLO = f"""
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM {TABLO}
    WHERE zaman >= '2025-01-15'
    GROUP BY GEOHASH
"""

SUZME_CSV = """
    SELECT GEOHASH, COUNT(*) AS olcum
    FROM ham
    WHERE to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') >= '2025-01-15'
    GROUP BY GEOHASH
"""

olc(SUZME_CSV, "suzme - csv")
olc(SUZME_TABLO, "suzme - tablo");

# COMMAND ----------

# Fark kucuk ciktiysa sebebi su: dosya atlama verinin SIRALI olmasina
# baglidir. Tabloyu siralamadan yazdik ve yalnizca UC dosya var; zaman
# degerleri ucune de karisik dagildigi icin her dosyanin araligi butun
# donemi kapsiyor. Istatistik vardir, ama atlanacak dosya kalmaz.
#
# Bicim secmek yetmiyor; DUZEN de secmek gerekiyor. Denemeler-1 bunu olcuyor.

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
