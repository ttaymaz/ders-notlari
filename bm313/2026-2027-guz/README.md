# BM313 — İşletim Sistemleri

## 2026–2027 Güz Dönemi

**Bilgisayar Mühendisliği** · Mühendislik Fakültesi · Afyon Kocatepe Üniversitesi
Öğr. Gör. Turgay Taymaz
Kısa adres: `tymz.org/bm313`

## Değerlendirme ve Sınav Takvimi

| Değerlendirme | Ağırlık | Tarih | Kapsam |
| ------------- | :-----: | ----- | ------ |
| **Ara Sınav** | %40 | 09–13 Kasım 2026 | 1.–7. hafta · Sanallaştırma |
| **Yarıyıl Sonu Sınavı** | %60 | 04–15 Ocak 2027 | Dönemin tamamı · ağırlık 9.–15. haftalarda |

## Çalışma Ortamı

Birinci haftadan itibaren Python 3 ve OSTEP simülatörleri gerekir; ikinci haftadan itibaren C örneklerini kendi bilgisayarınızda çalıştırmak isteyenler için WSL ve `gcc` isteğe bağlıdır. Adım adım kurulum: **[Çalışma Ortamı](../../konular/isletim-sistemleri/00-calisma-ortami/not.md)**

## Haftalık Plan

Ders içeriği [`konular/isletim-sistemleri/`](../../konular/isletim-sistemleri/) altında ders kodundan bağımsız olarak tutulur. Ders notu bağlantıları o hafta yayımlandıkça tabloya eklenir. OSTEP sütunu haftanın ileri okumasıdır.

| Hafta | Konu | OSTEP | Ders notu |
| :---: | ---- | :---: | :-------: |
| 1 | Ders tanıtımı · İşletim sistemi ne yapar: sanallaştırma, eşzamanlılık, kalıcılık | 2, 4 | [not](../../konular/isletim-sistemleri/01-isletim-sistemine-giris/not.md) |
| 2 | Proses API'si, sistem çağrıları, kullanıcı ve çekirdek modu | 5, 6 | [not](../../konular/isletim-sistemleri/02-proses-api-ve-sistem-cagrilari/not.md) · [kod](../../konular/isletim-sistemleri/02-proses-api-ve-sistem-cagrilari/kod/) |
| 3 | CPU zamanlama 1: FIFO, SJF, STCF, Round Robin | 7 | [not](../../konular/isletim-sistemleri/03-cpu-zamanlama/not.md) |
| 4 | CPU zamanlama 2: MLFQ, orantılı pay, çok çekirdekli sistemlere bakış | 8–10 | [not](../../konular/isletim-sistemleri/04-mlfq-ve-orantili-pay/not.md) |
| 5 | Bellek 1: adres uzayı, adres çevirme, segmentasyon | 13, 15, 16 | [not](../../konular/isletim-sistemleri/05-adres-uzayi-ve-segmentasyon/not.md) · [kod](../../konular/isletim-sistemleri/05-adres-uzayi-ve-segmentasyon/kod/) |
| 6 | Bellek 2: sayfalama ve TLB | 18, 19 | — |
| 7 | Bellek 3: takas, sayfa hatası, sayfa değiştirme algoritmaları | 21, 22 | — |
| 8 | **Ara Sınav Haftası** | | |
| 9 | İş parçacıkları, yarış durumu ve kilitler | 26–28 | — |
| 10 | Koşul değişkenleri ve semaforlar | 30, 31 | — |
| 11 | Kilitlenme: koşulları, önleme, kaçınma, tespit | 32 | — |
| 12 | G/Ç aygıtları, disk ve disk zamanlama | 36–38 | — |
| 13 | Dosya sistemleri ve günlükleme | 39, 40, 42 | — |
| 14 | Koruma ve güvenlik | 53–55 | — |
| 15 | Sanal makineler ve konteynerler · dönem kapanışı | Ek: VM | — |

## Sunumlar

Derste kullanılan sunumlar sınıf içi materyaldir ve ham haliyle paylaşılmaz. Ders notları sunumdaki kavramları, şemaları ve çözümlü örnekleri eksiksiz içerir; sınava hazırlanırken **ders notunu** esas alınız.
