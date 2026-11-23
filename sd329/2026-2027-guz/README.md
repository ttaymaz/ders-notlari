# SD329 — İleri Programlama I

## 2026–2027 Güz Dönemi

**Yazılım Mühendisliği** · Mühendislik Fakültesi · Afyon Kocatepe Üniversitesi
Öğr. Gör. Turgay Taymaz
Kısa adres: `tymz.org/sd329`

## Değerlendirme ve Sınav Takvimi

| Değerlendirme | Ağırlık | Tarih | Kapsam |
| ------------- | :-----: | ----- | ------ |
| **Ara Sınav** | %40 | 13 Kasım 2026 Cuma, 14.00 | 1.–6. konular · Dilin kendisi · çoktan seçmeli |
| **Yarıyıl Sonu: Dönem Projesi** | %60 | Son teslim 2 Ocak 2027, 23.59 · değişiklik isteği 4–6 Ocak | Dönemin tamamı · [proje kılavuzu](proje-kilavuzu.md) |
| **Bütünleme** | %60 | Akademik takvime göre | Dönemin tamamı · yazılı, kod okuma |

**Ara sınav** kâğıt üzerindedir ve tamamı çoktan seçmelidir: her sorunun beş seçeneği vardır, yalnızca biri doğrudur; iki kitapçık grubu kullanılır.

**Yarıyıl sonu notu bir dönem projesinden gelir;** yazılı final sınavı yapılmaz. Herkes ortak bir şablondan başlar, kendi seçtiği alanda katmanlı ve birim testli bir C# konsol uygulaması yazar. Yapay zekâ araçlarını kullanmak serbesttir. Ayrıntılar, takvim ve değerlendirme: [**proje kılavuzu**](proje-kilavuzu.md).

**Bütünleme sınavı yazılıdır** (kod okuma, 100 puan) ve projenin yerine geçer.

## Çalışma Ortamı

C# 14 · .NET 10 SDK · Visual Studio 2026 (veya .NET 10 SDK ile herhangi bir editör). Örnek kodların çoğu tek dosyadır ve proje açmadan çalışır:

```
dotnet run dosya.cs
```

Bu komut .NET 10 SDK gerektirir. Kurulu sürümü `dotnet --version` ile görebilirsiniz.

**.NET 9 kullanıyorsanız** (laboratuvar bilgisayarları dahil) örnekleri bir konsol projesinin içinde çalıştırırsınız; adımlar her haftanın `kod/README.md` dosyasında. Bütün örnekler bu yolla da denenmiştir.

## Haftalık Plan

Ders içeriği `konular/ileri-programlama/` altında ders kodundan bağımsız olarak tutulur. Ders notları yazıldıkça bu tabloya bağlanır.

| Hafta | Konu | Ders notu |
| :---: | ---- | :-------: |
| 1 | Ders tanıtımı · Profesyonel C# çalışma düzeni: .NET komut satırı, proje yapısı, NuGet, nullable referans tipleri, çözümleyiciler | [not](../../konular/ileri-programlama/01-calisma-duzeni/not.md) · [kod](../../konular/ileri-programlama/01-calisma-duzeni/kod/) |
| 2 | Tip sistemi ve bellek: değer ve referans tipleri, struct ve record, eşitlik, çöp toplayıcı | [not](../../konular/ileri-programlama/02-tip-sistemi-ve-bellek/not.md) · [kod](../../konular/ileri-programlama/02-tip-sistemi-ve-bellek/kod/) |
| 3 | Jenerik programlama: kısıtlar, jenerik arayüzler, kovaryans ve kontravaryans | [not](../../konular/ileri-programlama/03-jenerikler/not.md) · [kod](../../konular/ileri-programlama/03-jenerikler/kod/) |
| 4 | Delegeler, lambda ifadeleri ve olaylar | [not](../../konular/ileri-programlama/04-delegeler-ve-olaylar/not.md) · [kod](../../konular/ileri-programlama/04-delegeler-ve-olaylar/kod/) |
| 5 | Yineleyiciler ve ertelenmiş yürütme: `IEnumerable`, `yield` | [not](../../konular/ileri-programlama/05-yineleyiciler/not.md) · [kod](../../konular/ileri-programlama/05-yineleyiciler/kod/) |
| 6 | LINQ ile veri işleme: genişletme metotları, sorgu operatörleri · desen eşleme | [not](../../konular/ileri-programlama/06-linq-ve-desen-esleme/not.md) · [kod](../../konular/ileri-programlama/06-linq-ve-desen-esleme/kod/) |
| 7 | Ara sınav tekrarı ve örnek sorular | [not](../../konular/ileri-programlama/07-ara-sinav-tekrari/not.md) · [kod](../../konular/ileri-programlama/07-ara-sinav-tekrari/kod/) |
| 8 | **Ara Sınav Haftası** | |
| 9 | Hata yönetimi tasarımı ve birim testi: istisna ailesi, koruma cümleleri, Try kalıbı, sonuç tipi · xUnit | [not](../../konular/ileri-programlama/09-hata-yonetimi-ve-birim-testi/not.md) · [kod](../../konular/ileri-programlama/09-hata-yonetimi-ve-birim-testi/kod/) |
| 10 | Bağımlılık enjeksiyonu, yapılandırma ve günlükleme · servisi sahte saat ve bellekte depoyla sınamak | [not](../../konular/ileri-programlama/10-bagimlilik-enjeksiyonu/not.md) · [kod](../../konular/ileri-programlama/10-bagimlilik-enjeksiyonu/kod/) |
| 11 | Asenkron programlama: `async`/`await`, birlikte beklemek, iptal, asenkron akışlar · uzaktaki grafı gezmek | [not](../../konular/ileri-programlama/11-asenkron-programlama/not.md) · [kod](../../konular/ileri-programlama/11-asenkron-programlama/kod/) |
| 12 | Eşzamanlılık: yarış durumu, kilitler, eşzamanlı koleksiyonlar, kanallar | — |
| 13 | Yansıma ve öznitelikler, eklenti mimarisi | — |
| 14 | Başarım ölçümü ve bellek ayırma | — |
| 15 | Genel tekrar · dönem kapanışı | — |

> **Bu dönem konular bir hafta kaydırılarak işleniyor.** 2. haftanın dersi yapılamadı; 5. konu salı günü yapılacak tek seferlik bir telafi dersinde işlenecek. Bu yüzden 7. hafta ara sınav tekrarına ayrıldı; ara sınav 1–6. konuları kapsar. Hata yönetimi 9. haftaya alındı; yeniden düzenleme ve kod incelemesi ayrı bir hafta olarak işlenmeyecek, 9–14. haftaların örneklerine dağıtılacak.

## Sunumlar

Derste kullanılan sunumlar sınıf içi materyaldir ve ham haliyle paylaşılmaz. Ders notları sunumdaki kavramları, örnek kodları ve açıklamaları eksiksiz içerir; sınava hazırlanırken **ders notunu** esas alınız.
