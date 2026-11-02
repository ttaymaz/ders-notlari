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
| [`01-veri-kalitesi.py`](01-veri-kalitesi.py) | Analize başlamadan önce veriye bakmak: eksik değer, aykırı ölçüm, kapsama ve yinelenen kayıt |

## Sorgular ne soruyor?

| Sorgu | Soru |
| ----- | ---- |
| 1 | Eksik değer var mı? `COUNT(*)` ile `COUNT(sütun)` farkı |
| 2 | Değerler hangi aralıkta? En küçük ve en büyük |
| 3 | Aykırı ölçüm ne kadar yaygın? |
| 4 | Ortalama ile medyan ne kadar ayrışıyor? |
| 5 | Her saatte aynı sayıda nokta mı ölçülüyor? |
| 6 | Yinelenen kayıt var mı? |

**Beşincisi en önemlisi.** Geçen haftaki saatlik grafikte gördüğümüz platonun
gerçek mi, yoksa ölçüm sayısının saatten saate değişmesinden mi kaynaklandığını
söyleyen sorgu odur.

## Çalıştırmadan önce

**İlk hücredeki `VERI` satırına bakın.** Volume'unuzun adı `trafik` değilse
yalnızca o satırı düzeltin.

Bu not defteri hiçbir satır **silmez.** Amacı veriyi düzeltmek değil, veriye
güvenilip güvenilmeyeceğine karar vermek.

## Denemeniz için

1. 5. sorgudaki `toplam_arac` ile `nokta_basina_ortalama` sütunlarını
   karşılaştırın. İkisi aynı saati mi en yoğun gösteriyor?
2. `MAXIMUM_SPEED > 150` koşulundaki 150 sayısını 200 yapın. Kaç satır kaldı?
3. Ortalama ile medyan arasındaki fark ne kadar? Hangisini rapora yazardınız?
4. 6. sorgudaki iki sayı eşit mi? Değilse aradaki fark ne anlama gelir?
