# BVAP203 — Bulut Bilişim

## 2026–2027 Güz Dönemi

**Büyük Veri Analistliği Programı** · Bilişim Teknolojileri Meslek Yüksekokulu
Öğr. Gör. Turgay Taymaz
Laboratuvar · Salı, 3 × 50 dakika · İlk ders **22 Eylül 2026**

Kısa adres: `tymz.org/bvap203`

## Sınav Takvimi

| Değerlendirme | Ağırlık | Tarih | Biçim |
| ------------- | :-----: | ----- | ----- |
| Ara Sınav | %40 | 09–13 Kasım 2026 | Tamamı test |
| Dönem Projesi | %60 | Teslim **27 Aralık 2026**, Pazar 23.59 | Bireysel, tek dosya |

Ara ödev yoktur. **Proje dönem içinde birikir:** her haftanın laboratuvarı
projenin bir parçasını üretir, teslim tek seferde yapılır.

Yönerge, bölüm yapısı ve puanlama tablosu:
**[proje-kilavuzu.md](proje-kilavuzu.md)** — okumadan başlamayın.

Değerlendirmeniz **29 Aralık 2026** Salı günü son derste size iletilir;
bu bir bilgilendirmedir. **Resmî not ilanı** final döneminde (04–15 Ocak
2027) OBS üzerinden yapılır. Teslim bilinçli olarak final döneminden önceye
alınmıştır ki dönemin son dersinde geri bildirim verilebilsin.

## Haftalık Plan

Ders içeriği [`konular/`](../../konular/) altında ders kodundan bağımsız olarak
tutulur. Aşağıdaki tablo hangi haftada hangi konunun işlendiğini gösterir.
Ders notu ve not defteri bağlantıları, o hafta yayımlandıkça tabloya eklenir.

| Hafta | Konu | Ders notu | Not defteri |
| :---: | ---- | :-------: | :---------: |
| 0 | Ders tanıtımı, ortam ve hesap açma | — | — |
| 1 | Bulut nedir? Kendi makinenin sınırı | [not](../../konular/bulut-bilisim/01-bulut-nedir/not.md) | — |
| 2 | Hizmet modelleri: IaaS, PaaS, SaaS | [not](../../konular/bulut-bilisim/02-hizmet-modelleri/not.md) | — |
| 3 | Çalışma alanı, not defteri ve nesne depolama | [not](../../konular/bulut-bilisim/03-nesne-depolama/not.md) | [kod](../../konular/bulut-bilisim/03-nesne-depolama/kod/) |
| 4 | Veriye ilk sorular: Python ve SQL | [not](../../konular/bulut-bilisim/04-veriye-ilk-sorular/not.md) | [kod](../../konular/bulut-bilisim/04-veriye-ilk-sorular/kod/) |
| 5 | Sonucu görmek: tablo yerine şekil | [not](../../konular/bulut-bilisim/05-sonucu-gormek/not.md) | [kod](../../konular/bulut-bilisim/05-sonucu-gormek/kod/) |
| 6 | Veri kalitesi: veriye güvenebilir miyiz? | [not](../../konular/bulut-bilisim/06-veri-kalitesi/not.md) | [kod](../../konular/bulut-bilisim/06-veri-kalitesi/kod/) |
| 7 | Ara sınav öncesi genel tekrar | [not](../../konular/bulut-bilisim/07-ara-sinav-tekrari/not.md) | [kod](../../konular/bulut-bilisim/07-ara-sinav-tekrari/kod/) |
| 8 | **Ara sınav haftası** | | |
| 9 | Zaman serisi olarak veri | [not](../../konular/bulut-bilisim/09-zaman-serisi/not.md) | [kod](../../konular/bulut-bilisim/09-zaman-serisi/kod/) |
| 10 | İki tabloyu birleştirmek | [not](../../konular/bulut-bilisim/10-tablolari-birlestirmek/not.md) | [kod](../../konular/bulut-bilisim/10-tablolari-birlestirmek/kod/) |
| 11 | Bölümleme ve maliyet | [not](../../konular/bulut-bilisim/11-bolumleme-ve-maliyet/not.md) | [kod](../../konular/bulut-bilisim/11-bolumleme-ve-maliyet/kod/) |
| 12 | Tablo olarak saklamak ve sorgu hızı | [not](../../konular/bulut-bilisim/12-tablo-olarak-saklamak/not.md) | [kod](../../konular/bulut-bilisim/12-tablo-olarak-saklamak/kod/) |
| 13 | Dağıtım modelleri, güvenlik ve erişim | [not](../../konular/bulut-bilisim/13-dagitim-ve-guvenlik/not.md) | [kod](../../konular/bulut-bilisim/13-dagitim-ve-guvenlik/kod/) |
| 14 | Parçaları birleştirmek: raporlama | [not](../../konular/bulut-bilisim/14-proje-butunlestirme/not.md) | [kod](../../konular/bulut-bilisim/14-proje-butunlestirme/kod/) |
| 15 | Değerlendirme, geri bildirim ve dönem özeti | [not](../../konular/bulut-bilisim/15-donem-ozeti/not.md) | — |

> Tanıtım ve 1. hafta **aynı oturumda** işlenir: ilk dersin ikinci yarısı
> hesap açma ve ilk not defteri için ayrılmıştır.

## Sunumlar

Derste kullanılan sunumlar sınıf içi materyaldir ve paylaşılmaz. Ders notları
sunumdaki her şeyi ve fazlasını içerir — sınava çalışırken **ders notunu**
esas alınız.

## Ortam

Ders tamamen tarayıcı üzerinden yürür. Laboratuvar bilgisayarına veya kendi
bilgisayarınıza **hiçbir şey kurmanız gerekmez.**

- **Platform:** Databricks Free Edition — sunucusuz çalışma alanı
- **Diller:** Python 3 ve SQL
- **Giriş:** e-posta ile tek kullanımlık kod, Google veya Microsoft hesabı
- **Veri:** açık veri portalından alınmış saatlik trafik yoğunluğu dosyası

Sürüm, kota ve limitlerin tamamı tanıtım sunumundaki *"Ortam ve Limitler"*
slaydında toplanmıştır. Bu bilgiler yıl içinde değişebilir; değiştiğinde
yalnızca o slayt güncellenir.

> **Giriş için okul e-posta adresinize erişebiliyor olmanız gerekir.**
> Tek kullanımlık kod oraya gelir. Erişiminizde sorun varsa ilk hafta
> içinde bildirin — ikinci haftaya kalmasın.

## Sonrası

Bu ders bir bulut platformunda veri işlemenin temelini kurar. Dönem sonundaki
proje, dönem boyunca üretilen parçaların birleşmesiyle oluşur; parçaları her
hafta laboratuvarda üretiyoruz, tek seferde toplanmıyor.
