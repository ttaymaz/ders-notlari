# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-uctan-uca.py`](01-uctan-uca.py) | Altı haftanın tamamı tek not defterinde: oku → hazırla → güven → sor → göster → yorumla |

## Bu dosya iki işe yarar

**Tekrar.** Her adımın yanında hangi haftadan geldiği yazıyor. Bir adımın
neden orada olduğunu hatırlamıyorsanız o haftanın notuna dönün.

**Proje iskeleti.** Dönem sonundaki projeyi sıfırdan kurmanıza gerek yok;
bu altı adım projenin de omurgasıdır. Kendi sorunuzu ADIM 4'e koyup
devamını kendinize göre değiştirin.

## Altı adım

| Adım | Ne yapar | Hangi hafta |
| :--: | -------- | ----------- |
| 1 | Oku | 3 ve 5 |
| 2 | Hazırla (görünüm, tarih) | 4 |
| 3 | Güven (eksik, kapsama, yinelenen) | 6 |
| 4 | Sor (gruplama, özetleme) | 4 ve 6 |
| 5 | Göster (önce küçült, sonra çiz) | 5 |
| 6 | Yorumla (sonuç + gerekçe + sınır) | 6 |

**Sıra tesadüf değil.** Güven adımı sorudan önce gelir: güvenmediğiniz veriye
soru sormanın anlamı yok.

## Denemeniz için

1. ADIM 5'te `nokta_basina_ortalama` yerine `toplam_arac` çizin. İki grafik
   aynı saati mi en yoğun gösteriyor? Değilse hangisine güvenirsiniz?
2. ADIM 3'ü silip baştan çalıştırın. Sonuç değişti mi? Değişmediyse o adım
   neden var?
3. ADIM 2'deki görünümden `LATITUDE` ve `LONGITUDE` sütunlarını çıkarın.
   Hangi adım kırılır, hangisi kırılmaz?
