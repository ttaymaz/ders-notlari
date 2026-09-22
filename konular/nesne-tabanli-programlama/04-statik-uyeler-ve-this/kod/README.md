# 4. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-this-isim-cakismasi.cs` | `ad = ad;` neden çalışmaz | `01-this-isim-cakismasi.svg` |
| `02-kurucu-zinciri.cs` | `: this(...)` ile tekrarı bitirmek | — |
| `03-statik-uyeler.cs` | Paylaşılan sayaç, statik metot kuralı | `02-statik-vs-ornek.svg` |
| `04-matematiksel-islemler.cs` | Statik sınıf: `Math` gibi bir tip yazmak | — |

## Çalıştırma

```bash
dotnet run 03-statik-uyeler.cs
```

## Denemeniz için

- `01-this-isim-cakismasi.cs` içindeki bozuk sınıfa `this` ekleyin. Kaç karakter
  yazdınız, kaç hata düzeldi?
- Aynı dosyada doğru sınıftaki `this.` öneklerini silin. Program derleniyor mu?
  Doğru çalışıyor mu? İkisinin farkı bu haftanın özeti.
- `02-kurucu-zinciri.cs` çıktısındaki sıraya bakın: `[asıl kurucu]` neden önce
  yazılıyor?
- `Urun` sınıfına `Renk` adında yeni bir alan ekleyin. Zincirli sürümde kaç
  kurucuya dokundunuz? Eski sürümde kaça dokunmanız gerekirdi?
- `03-statik-uyeler.cs` içindeki `uretilenHesapSayisi` alanından `static`
  kelimesini silin. Hesap numaraları ne oluyor? Neden?
- Aynı dosyada `SayiCiftMi` metodunun içine `Console.WriteLine(bakiye);` ekleyin.
  Hata mesajı hangi kelimeyle başlıyor?
- `04-matematiksel-islemler.cs` içinde `new MatematikselIslemler();` satırını
  açın. Derleyici ne diyor?
- Aynı sınıfa `public int sayac;` ekleyin (statik olmayan). Ne oluyor?
