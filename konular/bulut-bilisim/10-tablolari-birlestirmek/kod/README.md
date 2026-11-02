# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-birlestirme.py`](01-birlestirme.py) | Anahtar üzerinden eşleme, takvim tablosu üretmek, `LEFT JOIN` ile eksik gün bulmak, dışarıdan bilgi ekleyip grafikte işaretlemek |

## Bu hafta iki yenilik var

**Referans tablo üretmek.** `sequence` iki tarih arasını doldurur, `explode`
diziyi satırlara açar:

```sql
SELECT explode(sequence(DATE '2024-12-01', DATE '2025-01-31', INTERVAL 1 DAY))
```

Bu tablo **ölçülmedi, üretildi.** İçindeki 62, gerçeğin değil tanımın sayısı.

**`LEFT JOIN` ve `IS NULL`.** Eksik olanı bulmanın yolu: soldaki her satırı
koruyan bir birleştirme, sonra eşleşmeyenleri süzmek.

```sql
LEFT JOIN gunluk g ON t.gun = g.gun
WHERE g.gun IS NULL
```

`IS NULL` denetimi **anahtar** sütununda yapılır. Ölçülmüş bir sütuna
bakarsanız, değeri boş olan ama var olan bir günü eksik sanarsınız.

## Çalıştırmadan önce

**İlk hücredeki `VERI` satırına bakın.** Volume'unuzun adı `trafik` değilse
yalnızca o satırı düzeltin.

Not defteri geçen haftanın `gunluk` görünümünü baştan kuruyor; dokuzuncu
haftanın not defterini çalıştırmış olmanız gerekmiyor.

## Beklenen sayılar

| Hücre | Beklenen |
| ----- | -------- |
| Takvim ve ölçülen gün sayısı | 62 ve 62 |
| Eksik gün listesi | boş (62 = 62 ise) |
| `ozel_gunler` anahtar denetimi | 2 satır, 2 farklı gün |
| `gunluk` ve `zengin` satır sayısı | **ikisi de aynı** |

Son satır bu haftanın disiplini: birleştirme sonrası satır sayısı sol
tablodan fazla çıkarsa **satır çoğalması** var ve sorgu bunu size söylemez.

## Denemeniz için

1. `WHERE g.gun IS NULL` koşulunu kaldırın. Kaç satır döndü ve neden?
2. `ozel_gunler` tablosuna aynı günü bilerek iki kez yazın. `zengin.count()`
   kaç oldu? O günün araçlarını toplarsanız ne olur?
3. Takvimi `INTERVAL 1 HOUR` ile saatlik kurun. 62 × 24 = 1488 saat
   bekliyoruz; kaç saat eksik çıktı?
4. İkinci hücredeki `JOIN`i `LEFT JOIN` yapın. Satır sayısı değişti mi?
   Değişmediyse bu, veri hakkında ne söylüyor?
