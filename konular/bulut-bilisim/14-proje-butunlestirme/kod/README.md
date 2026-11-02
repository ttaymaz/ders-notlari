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
| [`01-rapor-iskeleti.py`](01-rapor-iskeleti.py) | Raporun on bölümlük boş iskeleti: başlıklar, her bölümün ne istediğini söyleyen notlar ve boş kod hücreleri |

## Bu dosya bir örnek değil, bir başlangıç

Dönem boyunca içe aktardığınız not defterleri çalışan örneklerdi. Bu dosya
**boş** gelir ve amacı da bu: raporunuzun iskeletini elle kurmanıza gerek
kalmıyor.

İçe aktarın, adınızı yazın, her bölümün altına ilgili haftanın hücrelerini
taşıyın.

> `# MAGIC %md` ile başlayan satırlar içe aktarıldığında **markdown
> hücresine** dönüşür — yani metin olarak görünür, kod olarak değil. İskelet
> böyle çalışıyor.

## Bölümler

| Bölüm | Hafta |
| ----- | ----- |
| 1 · Ortam ve veri | 2–3 |
| 2 · Veriye ilk sorular | 4 |
| 3 · Sonucu şekille okumak | 5 |
| 4 · Veriye güvenebilir miyiz | 6 |
| 5 · Zaman ekseni | 9 |
| 6 · Dışarıdan bilgi eklemek | 10 |
| 7 · Maliyet notu | 11 |
| 8 · Veri hazırlama | 12 |
| 9 · Erişim ve veri notu | 13 |
| 10 · Sonuç | — |

Onuncu bölüm dönem boyunca üretilmedi; bu hafta yazılıyor. Üç başlığı var ve
ikincisi — **"neyi bulamadım"** — boş bırakılmaz.

## Çalıştırmadan önce

İlk kod hücresindeki **iki satıra** bakın: `VERI` ve `TABLO`. Volume ya da
tablo adınız farklıysa yalnızca o iki satırı düzeltin; dosyanın geri kalanı
onlara bakar.

## Teslim öncesi

İskeletin son hücresinde kısa bir liste var. Tam hâli ders notunda, dört
başlıkta: çalışma, yapı, içerik, dosya.

En çok kaybedilen iki puan:

1. **Oturum sıfırlanmadan aktarılan dosya.** `Run` → `Clear` → `Clear state
   and run all` çalıştırmadan dışa aktarmayın; HTML çıktısı hücrelerin son
   çalıştırıldığı hâlini gömer. Menüde bu satır gri görünüyorsa defteri hiç
   çalıştırmamışsınız demektir; bir hücre çalıştırın, etkinleşir.
2. **Kontrol edilmeyen HTML dosyası.** Aktardıktan sonra tarayıcıda **açıp
   bakın.** Grafiklerin görünmediği bir dosya teslim etmek bu dersin en sık
   kazası.
