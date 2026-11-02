# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-maliyet.py`](01-maliyet.py) | Bölüm sayısı ve dengesi, `EXPLAIN` ile dar/geniş dönüşüm ayrımı, yayınlamanın plandaki kanıtı, süre ölçümü |

## Bu hafta iki yenilik var

**`EXPLAIN` ile plana bakmak.** Çıktı uzun ve çoğu satırı bu haftayı
ilgilendirmiyor. Tek bir kelime arıyoruz:

```
Exchange
```

Varsa veri taşınacak, yoksa taşınmayacak. Bu kadar.

**Süreyi iki kez ölçmek.** İlk süre kaynak tahsisini de içerir. Not
defterindeki `olc()` yardımcısı bunun için var:

```python
olc(SORGU, "gruplama - 1. olcum")
olc(SORGU, "gruplama - 2. olcum")
```

## Çalıştırmadan önce

**İlk hücredeki `VERI` satırına bakın.** Volume'unuzun adı `trafik` değilse
yalnızca o satırı düzeltin.

Not defteri kendi `gunluk` ve `ozel_gunler` görünümlerini kuruyor; onuncu
haftanın not defterini çalıştırmış olmanız gerekmiyor.

## Beklenen çıktılar

| Hücre | Beklenen |
| ----- | -------- |
| Bölüm listesi | Birden fazla bölüm, boyutları birbirine yakın |
| Dar sorgunun planı | `Exchange` **yok** |
| Gruplamanın planı | `Exchange` **var** |
| Birleştirmenin planı | `BroadcastHashJoin` |
| İki süre ölçümü | İkinci ölçüm **belirgin biçimde** daha kısa |

Dördüncü satır geçen haftanın iddiasının kanıtı: küçük tablo her makineye
kopyalanıyor, büyük tablonun satırları taşınmıyor.

> Süreler ortamdan ortama ve günden güne değişir. Mutlak sayıyı değil,
> **iki ölçüm arasındaki farkı** okuyun.

## Denemeniz için

1. `EXPLAIN` çıktısını `ORDER BY` ve `DISTINCT` için de alın. Kaç `Exchange`?
2. Süre ölçümünü üçüncü, dördüncü kez çalıştırın. Düşmeye devam ediyor mu,
   bir yerde duruyor mu?
3. `inferSchema=True` olmadan okuyup süreyi karşılaştırın.
4. `ozel_gunler` tablosuna iki yüz bin satır ekleyin. Plan hâlâ
   `BroadcastHashJoin` mi, yoksa karıştırmaya mı döndü?

Dördüncü madde haftanın en öğretici denemesi: yayınlama bir garanti değil,
tablo boyutuna bağlı bir **karar.** Spark kararını değiştirdiğinde size
söylemez — planı okumanız gerekir.
