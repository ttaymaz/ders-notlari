# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-sorular-sql.py`](01-sorular-sql.py) | Üç soru, **SQL** ile |
| [`02-sorular-python.py`](02-sorular-python.py) | **Aynı üç soru**, Python ile |

**İki dosyayı yan yana açın.** Çıktılar birebir aynıdır; farklı olan yalnızca
soruyu yazma biçimidir. Bu haftanın asıl kazanımı o karşılaştırmadır.

## Çalıştırmadan önce

**İlk hücredeki `VERI` satırına bakın.** Volume'unuzun adı `trafik` değilse
yalnızca o satırı düzeltin.

Bu hafta okuma satırına `inferSchema=True` eklendi. Geçen hafta her sütun
metin geliyordu; bu ayar olmadan `SUM` ve `AVG` çalışmaz. Bedeli, Spark'ın
dosyayı bir kez fazladan okumasıdır — ilk hücre geçen haftakinden uzun sürer.

## Denemeniz için

Sorular dosyaların sonunda da duruyor.

1. `ORDER BY toplam_arac DESC` yerine `ORDER BY saat` yazın. Tablo neyi
   anlatıyor artık? Hangi sıralama soruyu daha iyi cevaplıyor?
2. `LIMIT 10` satırını silin. Kaç satır döndü ve neden?
3. Hafta sonu sorusunu ortalama hız yerine araç sayısına göre yorumlayın.
   İki ölçüt aynı cevabı mı veriyor? Vermiyorsa hangisi doğru?
4. Python dosyasındaki `display(...)` çağrısını kaldırıp sonucu yalnızca bir
   değişkene atayın. Hücre ne kadar sürdü? Sonuç nerede?
