# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: analize başlamadan önce veriye güvenilip güvenilmeyeceğine bakmak.
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
VERI = "/Volumes/workspace/default/trafik"

# COMMAND ----------

# Geçen haftaki gibi klasörü okuyoruz: iki ay birden.
trafik = spark.read.csv(VERI, header=True, inferSchema=True)
trafik.createOrReplaceTempView("ham")

spark.sql("""
    CREATE OR REPLACE TEMP VIEW trafik AS
    SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman,
           GEOHASH,
           LATITUDE,
           LONGITUDE,
           MINIMUM_SPEED,
           MAXIMUM_SPEED,
           AVERAGE_SPEED,
           NUMBER_OF_VEHICLES
    FROM ham
""")

# COMMAND ----------

# 1) EKSİK DEĞER VAR MI?
# COUNT(*) bütün satırları sayar. COUNT(sütun) ise BOŞ olanları saymaz.
# İki sayı farklıysa o sütunda eksik değer var demektir.
display(spark.sql("""
    SELECT COUNT(*)                  AS satir_sayisi,
           COUNT(zaman)              AS zaman_dolu,
           COUNT(GEOHASH)            AS geohash_dolu,
           COUNT(AVERAGE_SPEED)      AS hiz_dolu,
           COUNT(NUMBER_OF_VEHICLES) AS arac_dolu
    FROM trafik
"""))

# COMMAND ----------

# 2) DEĞERLER HANGİ ARALIKTA?
# En küçük ve en büyük değer, aykırı ölçümü en hızlı ele veren şeydir.
display(spark.sql("""
    SELECT MIN(MINIMUM_SPEED) AS en_dusuk_min,
           MAX(MAXIMUM_SPEED) AS en_yuksek_max,
           MIN(AVERAGE_SPEED) AS en_dusuk_ort,
           MAX(AVERAGE_SPEED) AS en_yuksek_ort,
           MIN(NUMBER_OF_VEHICLES) AS en_az_arac,
           MAX(NUMBER_OF_VEHICLES) AS en_cok_arac
    FROM trafik
"""))

# COMMAND ----------

# 3) AYKIRI DEĞER NE KADAR YAYGIN?
# Tek bir uç değer mi var, yoksa binlerce mi? Cevap ne yapacağınızı belirler.
display(spark.sql("""
    SELECT COUNT(*) AS cok_hizli_olcum
    FROM trafik
    WHERE MAXIMUM_SPEED > 150
"""))

# COMMAND ----------

# 4) YÜZDELİKLER: uçlar mı kaydırıyor, yoksa veri gerçekten geniş mi?
# Medyan (0.5) ile ortalamayı karşılaştırın; çok ayrılıyorlarsa uçlar var.
display(spark.sql("""
    SELECT ROUND(AVG(AVERAGE_SPEED), 1) AS ortalama,
           percentile_approx(AVERAGE_SPEED, 0.5)  AS medyan,
           percentile_approx(AVERAGE_SPEED, 0.95) AS ust_yuzde_5,
           percentile_approx(AVERAGE_SPEED, 0.99) AS ust_yuzde_1
    FROM trafik
"""))

# COMMAND ----------

# 5) KAPSAMA: her saatte aynı sayıda nokta mı ölçülüyor?
# Bu, geçen haftaki "plato gerçek mi" sorusunun cevabı.
# olcum_sayisi saatten saate değişiyorsa, TOPLAM araç sayısını
# saatler arasında karşılaştırmak yanıltıcıdır.
display(spark.sql("""
    SELECT hour(zaman) AS saat,
           COUNT(*) AS olcum_sayisi,
           COUNT(DISTINCT GEOHASH) AS nokta_sayisi,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac,
           ROUND(AVG(NUMBER_OF_VEHICLES), 1) AS nokta_basina_ortalama
    FROM trafik
    GROUP BY saat
    ORDER BY saat
"""))

# COMMAND ----------

# 6) YİNELENEN KAYIT VAR MI?
# Aynı noktada aynı saatte iki ölçüm olmamalı.
display(spark.sql("""
    SELECT COUNT(*) AS satir_sayisi,
           COUNT(DISTINCT zaman, GEOHASH) AS benzersiz_olcum
    FROM trafik
"""))

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. 5. sorgudaki toplam_arac ile nokta_basina_ortalama sütunlarını
#    karşılaştırın. İki sütun aynı saati mi en yoğun gösteriyor?
#    Farklıysa geçen haftaki cevabımız hangisiydi?
# 2. MAXIMUM_SPEED > 150 koşulundaki 150 sayısını 200 yapın. Kaç satır kaldı?
#    Bu sayı, o ölçümleri silmeye değer mi sorusunu nasıl etkiliyor?
# 3. Ortalama ile medyan arasındaki fark ne kadar? Hangisini bir rapora
#    yazardınız?
# 4. 6. sorgudaki iki sayı eşit mi? Eşit değilse aradaki fark ne anlama gelir?
