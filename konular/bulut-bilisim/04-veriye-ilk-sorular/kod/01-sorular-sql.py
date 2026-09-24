# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: bildiğiniz SQL ile veriye ilk üç soruyu sormak.
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
VERI = "/Volumes/workspace/default/trafik"

# COMMAND ----------

# Dosyayı okuyoruz. Geçen hafta her sütun metin gelmişti; inferSchema=True
# Spark'a tipleri tahmin ettiriyor, böylece sayılarla hesap yapabiliyoruz.
# Bedeli var: Spark dosyayı bir kez fazladan okur, ilk çalıştırma uzar.
trafik = spark.read.csv(
    f"{VERI}/traffic_density_202412.csv",
    header=True,
    inferSchema=True,
)

# COMMAND ----------

# Tipler gerçekten değişmiş mi? Sayı sütunları artık int/double görünmeli.
trafik.printSchema()

# COMMAND ----------

# SQL yazabilmek için veriye bir tablo adı veriyoruz.
# Bu bir kopya DEĞİL: veri yerinde duruyor, yalnızca "ham" adıyla
# sorgulanabilir hâle geliyor.
trafik.createOrReplaceTempView("ham")

# COMMAND ----------

# DATE_TIME metin olarak geliyor ("2024-12-01 00:00:00").
# Her sorguda tekrar çevirmemek için tarihi bir kez çevirip
# "trafik" adında bir görünüm (view) oluşturuyoruz.
spark.sql("""
    CREATE OR REPLACE TEMP VIEW trafik AS
    SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman,
           GEOHASH,
           AVERAGE_SPEED,
           NUMBER_OF_VEHICLES
    FROM ham
""")

# COMMAND ----------

# Soru 1: Günün hangi saatinde en çok araç geçiyor?
display(spark.sql("""
    SELECT hour(zaman) AS saat,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac
    FROM trafik
    GROUP BY saat
    ORDER BY toplam_arac DESC
"""))

# COMMAND ----------

# Soru 2: En kalabalık on ölçüm noktası hangisi?
display(spark.sql("""
    SELECT GEOHASH,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac,
           ROUND(AVG(AVERAGE_SPEED), 1) AS ortalama_hiz
    FROM trafik
    GROUP BY GEOHASH
    ORDER BY toplam_arac DESC
    LIMIT 10
"""))

# COMMAND ----------

# Soru 3: Hafta sonu ile hafta içi arasında fark var mı?
# dayofweek: 1 = pazar, 7 = cumartesi.
display(spark.sql("""
    SELECT CASE
             WHEN dayofweek(zaman) IN (1, 7) THEN 'hafta sonu'
             ELSE 'hafta ici'
           END AS gun_turu,
           ROUND(AVG(AVERAGE_SPEED), 1) AS ortalama_hiz,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac
    FROM trafik
    GROUP BY gun_turu
"""))

# COMMAND ----------

# Not: Databricks'te bir hücrenin başına %sql yazarsanız o hücreye doğrudan
# SQL yazabilirsiniz; spark.sql(...) ile tırnak içine almanız gerekmez.
# İkisi de aynı işi yapar.

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. Soru 1'deki ORDER BY satırını "ORDER BY saat" yapın. Tablo neyi
#    anlatıyor artık? Hangi sıralama soruyu daha iyi cevaplıyor?
# 2. Soru 2'deki LIMIT 10 satırını silin. Kaç satır döndü ve neden?
# 3. Soru 3'ü ortalama hız yerine araç sayısına göre yorumlayın.
#    İki ölçüt aynı cevabı mı veriyor? Vermiyorsa hangisi doğru?
