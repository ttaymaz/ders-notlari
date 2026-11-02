# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-tablo.py`](01-tablo.py) | CSV'den tablo kurmak, boyut ve süre karşılaştırması, planda `PhotonRowToColumnar` adımının kaybolması, dosya atlama |

## Bu hafta iki yenilik var

**Tablo kurmak.** Tek sorgu, ve tarih dönüşümü burada bir kez yapılıyor:

```sql
CREATE OR REPLACE TABLE workspace.default.trafik_tablo AS
SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman, ...
```

Dört haftadır her not defterinde `to_timestamp` satırını baştan yazıyorduk.
Bundan sonra yazmayacağız.

**Tabloya ad ile ulaşmak.** Yol değil ad: `katalog.şema.tablo`. Ad tanıdık
olmalı — volume'unuzun yolu da `/Volumes/workspace/default/trafik` idi.

## Çalıştırmadan önce

**İlk hücredeki `VERI` ve `TABLO` satırlarına bakın.** Volume'unuzun adı
`trafik` değilse `VERI` satırını düzeltin. `TABLO` satırına dokunmanız
gerekmez.

İkinci hücre bugünün **en yavaş** hücresi: veri okunmakla kalmıyor,
yazılıyor da. Bir kerelik maliyet.

## Beklenen çıktılar

| Hücre | Beklenen |
| ----- | -------- |
| `DESCRIBE DETAIL` → `format` | `delta` |
| `DESCRIBE DETAIL` → `sizeInBytes` | CSV'nin 232 MB'ından **belirgin biçimde küçük** |
| `DESCRIBE TABLE` | Tipler yazılı; `inferSchema` gerekmiyor |
| Tablo sorgusunun süresi | CSV'den **kısa** — kaç kat olduğunu siz ölçeceksiniz |
| Tablo planı | `PhotonRowToColumnar` **yok**, `Batched: false` yok |

> **Çalıştırmadan önce tahmininizi yazın.** Kaç kat hızlanmasını
> bekliyorsunuz, boyut kaça düşer? Ölçümden sonra yazılan tahmin tahmin
> değildir.

Dosya atlama ölçümünde fark küçük çıkabilir. Sebebi şu: atlama verinin
**sıralı** olmasına bağlıdır ve biz tabloyu sıralamadan yazdık. İstatistik
var ama atlanacak dosya kalmamış olabilir — bu da bir bulgudur, ve
denemelerin birincisi tam bunu ölçüyor.

## Denemeniz için

1. Tabloyu `ORDER BY zaman` ile yeniden yazın, sonra dosya atlama hücresini
   tekrar çalıştırın. Sıralama atlamayı mümkün kıldı mı?
2. Yalnızca iki sütunlu ikinci bir tablo kurun. `sizeInBytes`, dört
   sütunlunun kaçta kaçı?
3. `DESCRIBE HISTORY` ile tablonun geçmişine bakın. Kaç sürüm var?
4. `OPTIMIZE` çalıştırıp `DESCRIBE DETAIL`e yeniden bakın. `numFiles`
   değişti mi?

Birinci madde haftanın en öğretici denemesi: biçim seçmek yetmiyor, **düzen**
de seçmek gerekiyor.
