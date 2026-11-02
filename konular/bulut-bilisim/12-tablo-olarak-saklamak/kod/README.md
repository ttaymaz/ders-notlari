# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

**Önce dosyayı indirin:** GitHub'da dosyayı açın → **Raw** düğmesi →
tarayıcıda **Ctrl+S** ile kaydedin. Uzantısı `.py` kalsın.

Sonra çalışma alanına aktarın: sol menü → **Workspace** → **Users** →
**kendi e-posta adresiniz** → o satırın sağındaki **üç nokta** →
**Import** → **File** → indirdiğiniz dosyayı seçin.
`# COMMAND ----------` satırları hücre sınırlarını gösterir.

> **Üstteki `Workspace` satırına değil, kendi klasörünüze aktarın.** Kök
> `Workspace` düğümünde içe aktarma hata verir; oraya yazma yetkiniz yok.
> Kendi e-posta adresinizi taşıyan klasör sizindir, içe aktarma orada
> çalışır.

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

| Hücre | Ölçülen |
| ----- | ------- |
| `format` · `numFiles` | `delta` · **3** dosya |
| `sizeInBytes` | **8.029.833** = 7,7 MB — CSV'nin 231,8 MB'ına karşı **~30 kat** |
| `DESCRIBE TABLE` | Tipler yazılı; `inferSchema` gerekmiyor |
| CSV sorgusu | 1,00 ve 1,00 sn |
| Tablo sorgusu | 0,85 ve **0,74** sn — yaklaşık **1,35 kat** |
| Tablo planı | `PhotonScan parquet`; `PhotonRowToColumnar` **yok** |
| Karıştırma bölümü | CSV'de `231`, tabloda **`16`** |
| `COUNT(*)` tamamı / süzülmüşü | 0,58 sn / 0,78 sn — **süzülmüş daha yavaş** |

> **Çalıştırmadan önce tahmininizi yazın.** Ölçümden sonra yazılan tahmin
> tahmin değildir.

**Boyut 30 kat, süre 1,35 kat.** Aradaki uçurum bu haftanın asıl dersi: bu
ölçekte zamanın çoğu okumaya değil sabit giderlere gidiyor. Üstelik aynı CSV
sorgusu geçen hafta 1,28 ve 1,54 sn ölçülmüştü — **sapma, kazançla aynı
mertebede.** Yani "tablo 1,35 kat hızlı" demek bile bu ölçümle ispatlanmış
sayılmaz.

**30 katı dürüst okuyun:** CSV'nin sekiz sütunu var, tabloya dördünü aldık.
Kazancın bir kısmı sıkışma, bir kısmı atılan sütun. Denemelerin ikincisi
payları ayırıyor.

**`COUNT(*)` satırı iki şey söylüyor.** Birincisi: o sorgu veriyi hiç
okumuyor, tablo satır sayısını zaten biliyor — bu yüzden karşılaştırma için
kötü bir başlangıç noktası. İkincisi: atlanacak dosya yok, çünkü tabloyu
sıralamadan yazdık ve üç dosyanın üçü de bütün dönemi kapsıyor. İstatistik
var ama işe yaramıyor; denemelerin birincisi bunu ölçüyor.

**İstatistiğin işe yaradığı yer** karıştırma bölüm sayısı: 231 yerine 16.
Planın sonundaki `Optimizer Statistics — full = trafik_tablo` satırı bunun
kanıtı.

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
