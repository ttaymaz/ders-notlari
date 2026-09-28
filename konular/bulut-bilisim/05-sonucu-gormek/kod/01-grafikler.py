# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: aynı sorguların sonucunu tablo yerine şekil olarak okumak.
#
# Veri yolu — volume adınız farklıysa YALNIZCA bu satırı değiştirin.
VERI = "/Volumes/workspace/default/trafik"

# COMMAND ----------

# Bu hafta DOSYA adı değil, KLASÖR yolu veriyoruz.
# Spark klasördeki bütün CSV'leri okur — iki ay birden geliyor.
trafik = spark.read.csv(VERI, header=True, inferSchema=True)
print(trafik.count())

# COMMAND ----------

# Geçen haftanın görünümü koordinatları almıyordu; bugün lazım.
# Bu yüzden LATITUDE ve LONGITUDE sütunlarını da alıyoruz.
trafik.createOrReplaceTempView("ham")

spark.sql("""
    CREATE OR REPLACE TEMP VIEW trafik AS
    SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman,
           GEOHASH,
           LATITUDE,
           LONGITUDE,
           AVERAGE_SPEED,
           NUMBER_OF_VEHICLES
    FROM ham
""")

# COMMAND ----------

# Saatlik toplam: 24 satır. Tablo olarak zaten okunabilir bir boyut.
saatlik = spark.sql("""
    SELECT hour(zaman) AS saat,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac
    FROM trafik
    GROUP BY saat
    ORDER BY saat
""")

display(saatlik)

# COMMAND ----------

# Yukarıdaki çıktının üstünde Table sekmesinin yanındaki + düğmesine basıp
# Visualization seçin. Açılan Visualization Editor penceresinde:
#   Visualization type: Bar   (varsayılan Scatter gelir, değiştirin)
#   X column: saat
#   Y columns: toplam_arac
# ve sağ altta SAVE. Save'e basmazsanız grafik kaydedilmez.
#
# Kod yazmadan grafik çıkar. Ama bu grafik KOD DEĞİL — not defterine bağlı
# bir ayardır. Aynı grafiği kodla da çizebiliriz, aşağıdaki hücrede.

# COMMAND ----------

# Aynı grafik, kodla. Önce KÜÇÜLTÜP sonra çekiyoruz:
# toPandas() veriyi tarayıcıya getirir, o yüzden yalnızca 24 satır olmalı.
import matplotlib.pyplot as plt

p = saatlik.toPandas()

fig, ax = plt.subplots(figsize=(10, 4))
ax.bar(p["saat"], p["toplam_arac"])
ax.set_xlabel("Saat")
ax.set_ylabel("Toplam araç")
ax.set_title("Gün içinde trafik yoğunluğu")
display(fig)

# COMMAND ----------

# Geçen hafta "sayıyı bulduk ama yeri bulamadık" demiştik.
# Verideki LATITUDE ve LONGITUDE sütunları bunu çözüyor.
# Önce noktaları gruplayıp küçültüyoruz: her ölçüm noktası için tek satır.
noktalar = spark.sql("""
    SELECT GEOHASH,
           AVG(LATITUDE) AS enlem,
           AVG(LONGITUDE) AS boylam,
           SUM(NUMBER_OF_VEHICLES) AS toplam_arac
    FROM trafik
    GROUP BY GEOHASH
""")

print(noktalar.count())

# COMMAND ----------

# Harita aslında bir dağılım grafiğidir: yatay eksen boylam, dikey eksen enlem.
# Nokta büyüklüğü araç sayısını gösteriyor.
n = noktalar.toPandas()

fig, ax = plt.subplots(figsize=(8, 8))
ax.scatter(n["boylam"], n["enlem"], s=n["toplam_arac"] / n["toplam_arac"].max() * 60, alpha=0.5)
ax.set_xlabel("Boylam")
ax.set_ylabel("Enlem")
ax.set_title("Ölçüm noktaları ve yoğunluk")
display(fig)

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. Saatlik grafikte ORDER BY saat yerine ORDER BY toplam_arac DESC yazın.
#    Grafik hangi soruyu cevaplıyor artık? Hangisi daha okunaklı?
# 2. Bar yerine Line grafiği seçin. Hangi soru için hangisi daha uygun?
# 3. Dağılım grafiğinde alpha=0.5 değerini 1.0 yapın. Ne kayboldu?
# 4. noktalar.count() kaç çıktı? Bu sayıyı TABLO olarak okumayı dener
#    miydiniz? Neden?
