# 12. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-atama-farki.cs` | Aynı görünen iki atama, farklı sonuç | `01-kopya-mi-referans-mi.svg` |
| `02-struct-mu-class-mi.cs` | Karar ölçütü ve değiştirilebilir `struct` tuzağı | `02-struct-mu-class-mi.svg` |
| `03-null-ve-esitlik.cs` | `null`, nullable ve eşitlik davranışı | — |
| `04-pratik-yan-etki.cs` | Sığ ve derin kopya — gerçek bir hata | — |

## Çalıştırma

```bash
dotnet run 04-pratik-yan-etki.cs
```

## Denemeniz için

- `01-atama-farki.cs` dosyasını çalıştırmadan önce **her bölümün çıktısını
  kağıda yazın.** Kaçında yanıldınız?
- Aynı dosyada `struct Nokta` ifadesini `class Nokta` yapın. Hangi satırların
  çıktısı değişti? Tek kelime, tersine dönen davranış.
- `NesneyiDegistir` metodunun içine `k.Baslik = ...` yerine
  `k = new Kitap("başka")` yazın. Dışarıdaki kitap değişti mi? Neden?
- `02-struct-mu-class-mi.cs` içindeki `foreach` bloğunun yorumunu kaldırın.
  Derleyici ne diyor? Bu hata neden **iyi** bir şey?
- `03-null-ve-esitlik.cs` içinde `k1.Baslik == k2.Baslik` karşılaştırmasını
  ekleyin. Sonuç `k1 == k2` ile neden farklı?
- Aynı dosyada `int sayi = null;` satırının yorumunu kaldırın. Hata mesajı ne
  diyor? `int?` yazarsanız ne değişir?
- `04-pratik-yan-etki.cs` içindeki `YedekAl` metodunu derin kopya yapacak
  biçimde düzeltin. Kaç karakter değiştirdiniz?
- `DerinYedekAl` mantığını `Urun` sınıfının içine bir `Kopyala()` metodu
  olarak taşıyın. Hangi tasarım daha iyi? Neden?
