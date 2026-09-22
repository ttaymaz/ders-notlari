# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.** Metin
dosyası olarak okunabilirler, ama asıl yerleri çalışma alanınızdır.

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. Not defteri hücrelere bölünmüş olarak açılır.

Dosyayı indirmeden, içeriğini kopyalayıp boş bir not defterine
yapıştırarak da çalışabilirsiniz; `# COMMAND ----------` satırları hücre
sınırlarını gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-veriyi-oku.py`](01-veriyi-oku.py) | Volume'daki CSV'yi okumak, ilk satırlara bakmak, satır saymak |

## Çalıştırmadan önce

**İlk hücredeki `VERI` satırına bakın.** Volume'unuzun adı `trafik`
değilse yalnızca o satırı düzeltin — dosyanın geri kalanına dokunmanız
gerekmez. Yol zaten bunun için tek bir yerde toplanmıştır.

Not defteri **üstten aşağı, tek seferde** çalışır. Bir hücreyi atlarsanız
sonraki hücre tanımlanmamış bir değişkenle karşılaşır.

## Denemeniz için

Sorular dosyanın sonunda da duruyor. Cevapları aramayın; çalıştırıp görün:

1. `VERI` satırındaki volume adını bilerek yanlış yazın. Hata mesajı yolun
   neresinin yanlış olduğunu söylüyor mu?
2. Ocak dosyasını da okuyup satır sayısını yazdırın. Hangi ay daha kalabalık?
3. `limit(10)` yerine `limit(1000)` yazın. Süre belirgin biçimde değişti mi?
   Değişmediyse sebebi ne olabilir?
