# Databricks notebook source
# Bu not defteri, 01-sorular-sql.py dosyasındaki ÜÇ SORUNUN AYNISINI
# Python ile soruyor. Yan yana açıp karşılaştırın: sonuçlar birebir aynı,
# değişen yalnızca yazma biçimi.
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
VERI = "/Volumes/workspace/default/trafik"

# COMMAND ----------

# Kullanacağımız hazır fonksiyonları getiriyoruz.
# col: sütuna başvurur · sum/avg/round: SQL'deki karşılıklarının aynısı
from pyspark.sql.functions import col, sum, avg, round, hour, dayofweek, to_timestamp, when

# COMMAND ----------

# Okuma satırı SQL dosyasındakiyle aynı.
trafik = spark.read.csv(
    f"{VERI}/traffic_density_202412.csv",
    header=True,
    inferSchema=True,
)

# COMMAND ----------

# DATE_TIME metin olarak geliyor. Her soruda tekrar çevirmemek için
# "zaman" adında yeni bir sütun ekliyoruz. withColumn yeni bir sütun ekler;
# veriyi değiştirmez, yeni bir tarif üretir.
trafik = trafik.withColumn(
    "zaman", to_timestamp(col("DATE_TIME"), "yyyy-MM-dd HH:mm:ss")
)

# COMMAND ----------

# Soru 1: Günün hangi saatinde en çok araç geçiyor?
# SQL karşılığı: SELECT hour(...) , SUM(...) GROUP BY saat ORDER BY ... DESC
display(
    trafik
    .withColumn("saat", hour(col("zaman")))
    .groupBy("saat")
    .agg(sum("NUMBER_OF_VEHICLES").alias("toplam_arac"))
    .orderBy(col("toplam_arac").desc())
)

# COMMAND ----------

# Soru 2: En kalabalık on ölçüm noktası hangisi?
display(
    trafik
    .groupBy("GEOHASH")
    .agg(
        sum("NUMBER_OF_VEHICLES").alias("toplam_arac"),
        round(avg("AVERAGE_SPEED"), 1).alias("ortalama_hiz"),
    )
    .orderBy(col("toplam_arac").desc())
    .limit(10)
)

# COMMAND ----------

# Soru 3: Hafta sonu ile hafta içi arasında fark var mı?
# when(...).otherwise(...) SQL'deki CASE WHEN ... ELSE ... END karşılığıdır.
display(
    trafik
    .withColumn(
        "gun_turu",
        when(dayofweek(col("zaman")).isin(1, 7), "hafta sonu").otherwise("hafta ici"),
    )
    .groupBy("gun_turu")
    .agg(
        round(avg("AVERAGE_SPEED"), 1).alias("ortalama_hiz"),
        sum("NUMBER_OF_VEHICLES").alias("toplam_arac"),
    )
)

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. Soru 1'deki display(...) satırını silip yerine sadece değişkene atayın.
#    Hücre ne kadar sürdü? Sonuç nerede?
# 2. .orderBy(col("toplam_arac").desc()) yerine .orderBy("toplam_arac")
#    yazın. Ne değişti?
# 3. İki dosyanın Soru 2 çıktısını yan yana koyun. Satırlar aynı mı?
#    Aynıysa, hangi dosyayı yazmak size daha kolay geldi ve neden?
