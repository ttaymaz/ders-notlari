# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: volume'daki dosyayı okumak ve kaç satır olduğunu görmek.
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
# Yolu elle yazmayın: Catalog ekranında volume sayfasının üstündeki
# kopyalama düğmesi tam yolu panoya alır.
VERI = "/Volumes/workspace/default/trafik"

# COMMAND ----------

# Volume'da hangi dosyalar var? Yolun doğru olduğunu en hızlı böyle anlarsınız.
# Liste boş geliyorsa dosyalar başka bir yere yüklenmiş demektir.
display(dbutils.fs.ls(VERI))

# COMMAND ----------

# Aralık dosyasını okumayı tarif ediyoruz. header=True: ilk satır sütun adları.
# Bu satır veriyi OKUMAZ, yalnızca nasıl okunacağını yazar — anında biter.
trafik = spark.read.csv(f"{VERI}/traffic_density_202412.csv", header=True)

# COMMAND ----------

# İlk on satıra bakalım. display() sonucu tablo olarak gösterir.
display(trafik.limit(10))

# COMMAND ----------

# Kaç satır var? İşin gerçekten yapıldığı ilk komut budur.
# İlk çalıştırma kırk saniye kadar sürer; aynı hücreyi tekrar çalıştırın,
# çok daha kısa sürecek. Aradaki fark, makinelerin ayağa kaldırılmasıdır.
print(trafik.count())

# COMMAND ----------

# Sütun adları ve tipleri. Hepsi string göründü: CSV'de tip bilgisi yoktur,
# hız ve sayı sütunlarını sayıya çevirmek ilerleyen haftaların konusu.
trafik.printSchema()

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. VERI satırındaki volume adını bilerek yanlış yazın ve ikinci hücreyi
#    çalıştırın. Hata mesajı size yolun neresinin yanlış olduğunu söylüyor mu?
# 2. Ocak dosyasını da okuyup satır sayısını yazdırın. Hangi ay daha kalabalık?
# 3. limit(10) yerine limit(1000) yazın. Süre belirgin biçimde değişti mi?
#    Değişmediyse bunun sebebi ne olabilir?
