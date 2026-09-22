# 1. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-prosedurel-ogrenci.cs` | Prosedürel yöntem ve üç sınırı | `01-prosedurel-vs-nesne.svg` (sol) |
| `02-nesne-ogrenci.cs` | Aynı program, nesnelerle | `01-prosedurel-vs-nesne.svg` (sağ) |
| `03-kitap-sinifi.cs` | Kütüphane otomasyonunun ilk sınıfı | `02-sinif-ve-nesneler.svg` |
| `04-nesneler-bagimsiz.cs` | Her nesnenin kendi verisi vardır | — |

İlk iki dosyayı **yan yana açın.** Çıktıları birebir aynıdır; farklı olan kodun şeklidir.

## Çalıştırma

```bash
dotnet run 02-nesne-ogrenci.cs
```

## Denemeniz için

- İki öğrenci programına da **bölüm bilgisi** ekleyin. Her birinde kaç yere
  dokundunuz? Sayın.
- `01-prosedurel-ogrenci.cs` içinde `adlar` dizisinden ikinci ismi silin ama
  not dizilerine dokunmayın. Program çöküyor mu? Çıktı doğru mu?
- `02-nesne-ogrenci.cs` içindeki `Ortalama()` metodu neden parametre almıyor?
  Almasını sağlasanız ne değişirdi?
- `03-kitap-sinifi.cs` içinde `kitap1.OduncVerildi = true;` satırını doğrudan
  yazın. Çalışıyor mu? Sizce çalışmalı mı?
- `04-nesneler-bagimsiz.cs` içinde `sayaclar[i] = new Sayac();` satırını yorum
  satırı yapın. Aldığınız hata ne diyor?
- `Kitap` sınıfına `Tur` adında bir alan ekleyin. Kaç yere dokunmanız gerekti?
