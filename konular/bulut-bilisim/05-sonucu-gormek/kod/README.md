# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-grafikler.py`](01-grafikler.py) | Sonucu tablo yerine şekil olarak okumak: sütun grafiği, dağılım grafiği ve harita |

## Bu hafta iki yenilik var

**Klasör yolu okuma.** Dosya adı yerine klasörün kendisini veriyoruz:

```python
trafik = spark.read.csv(VERI, header=True, inferSchema=True)
```

Spark klasördeki bütün CSV'leri okur; iki ay birden gelir.

**Koordinatlar görünüme eklendi.** Geçen haftanın görünümü `LATITUDE` ve
`LONGITUDE` sütunlarını almıyordu — o hafta gerekmiyordu. Haritaya dökmek
için bu hafta alıyoruz.

## Çalıştırmadan önce

**İlk hücredeki `VERI` satırına bakın.** Volume'unuzun adı `trafik` değilse
yalnızca o satırı düzeltin.

`toPandas()` çağrılarına dikkat edin: veriyi tarayıcıya getirirler. Bu yüzden
**önce gruplayıp küçültüyoruz.** Üç milyon satırı olduğu gibi çekmeye
kalkarsanız oturum düşer.

## Denemeniz için

1. Saatlik grafikte `ORDER BY saat` yerine `ORDER BY toplam_arac DESC` yazın.
   Grafik hangi soruyu cevaplıyor artık? Hangisi daha okunaklı?
2. Bar yerine Line grafiği seçin. Hangi soru için hangisi daha uygun?
3. Dağılım grafiğinde `alpha=0.5` değerini `1.0` yapın. Ne kayboldu?
4. `noktalar.count()` kaç çıktı? Bu sayıyı **tablo olarak** okumayı dener
   miydiniz? Neden?
