# SD329 — İleri Programlama I

## 2026–2027 Güz Dönemi

**Yazılım Mühendisliği** · Mühendislik Fakültesi · Afyon Kocatepe Üniversitesi
Öğr. Gör. Turgay Taymaz
Kısa adres: `tymz.org/sd329`

## Değerlendirme ve Sınav Takvimi

| Değerlendirme | Ağırlık | Tarih | Kapsam |
| ------------- | :-----: | ----- | ------ |
| **Ara Sınav** | %40 | 09–13 Kasım 2026 | 1.–7. hafta · Dilin kendisi |
| **Yarıyıl Sonu Sınavı** | %60 | 04–15 Ocak 2027 | Dönemin tamamı · ağırlık 9.–14. haftalarda |

İki sınav da yazılı ve kâğıt üzerindedir. Bütünleme sınavı yarıyıl sonu sınavıyla aynı biçimdedir.

## Çalışma Ortamı

C# 14 · .NET 10 SDK · Visual Studio 2026 (veya .NET 10 SDK ile herhangi bir editör). Örnek kodların çoğu tek dosyadır ve proje açmadan çalışır:

```
dotnet run dosya.cs
```

Bu komut .NET 10 SDK gerektirir. Kurulu sürümü `dotnet --version` ile görebilirsiniz.

## Haftalık Plan

Ders içeriği `konular/ileri-programlama/` altında ders kodundan bağımsız olarak tutulur. Ders notları yazıldıkça bu tabloya bağlanır.

| Hafta | Konu | Ders notu |
| :---: | ---- | :-------: |
| 1 | Ders tanıtımı · Profesyonel C# çalışma düzeni: .NET komut satırı, proje yapısı, NuGet, nullable referans tipleri, çözümleyiciler | — |
| 2 | Tip sistemi ve bellek: değer ve referans tipleri, struct ve record, eşitlik, çöp toplayıcı | — |
| 3 | Jenerik programlama: kısıtlar, jenerik arayüzler, kovaryans ve kontravaryans | — |
| 4 | Delegeler, lambda ifadeleri ve olaylar | — |
| 5 | Yineleyiciler ve LINQ: `yield`, ertelenmiş yürütme, genişletme metotları | — |
| 6 | LINQ ile veri işleme ve desen eşleme | — |
| 7 | Hata yönetimi tasarımı ve birim testi | — |
| 8 | **Ara Sınav Haftası** | |
| 9 | Bağımlılık enjeksiyonu, yapılandırma ve günlükleme | — |
| 10 | Asenkron programlama: `async`/`await`, iptal, asenkron akışlar | — |
| 11 | Eşzamanlılık: yarış durumu, kilitler, eşzamanlı koleksiyonlar, kanallar | — |
| 12 | Yansıma ve öznitelikler, eklenti mimarisi | — |
| 13 | Başarım ölçümü ve bellek ayırma | — |
| 14 | Yeniden düzenleme ve kod incelemesi | — |
| 15 | Genel tekrar · dönem kapanışı | — |

## Sunumlar

Derste kullanılan sunumlar sınıf içi materyaldir ve ham haliyle paylaşılmaz. Ders notları sunumdaki kavramları, örnek kodları ve açıklamaları eksiksiz içerir; sınava hazırlanırken **ders notunu** esas alınız.
