# 2. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-kurucu-nedir.cs` | Kurucusuz ve kuruculu aynı sınıf | `01-nesnenin-dogusu.svg` |
| `02-varsayilan-kurucu.cs` | Görünmez kurucu ne zaman kaybolur | `02-varsayilan-kurucu.svg` |
| `03-kurucu-asiri-yukleme.cs` | Aynı sınıf, üç kuruluş biçimi | — |
| `04-urun-tam.cs` | Alanlar, metotlar ve kurucular bir arada | — |

## Çalıştırma

```bash
dotnet run 04-urun-tam.cs
```

## Denemeniz için

- `01-kurucu-nedir.cs` sonundaki `new Urun("Monitör")` satırının yorumunu
  kaldırın. Derleyici ne diyor? Bu iyi bir şey mi, kötü mü?
- `02-varsayilan-kurucu.cs` içindeki `new Defter()` satırını açın. Hata
  mesajını okuyun. Sınıfa hangi satırı eklerseniz bu hata kalkar?
- `03-kurucu-asiri-yukleme.cs` içine dördüncü bir kurucu ekleyin:
  `Urun(string ad, int stok)`. Derleniyor mu? `new Urun("Kalem", 5)` hangi
  kurucuyu çağırır?
- `04-urun-tam.cs` içindeki `StokCikis` metodundan stok kontrolünü silin ve
  100 adet satmayı deneyin. Stok eksiye düşüyor mu?
- Ardından `klavye.StokAdedi = -50;` satırını ana programa ekleyin. Metot
  içindeki kontrol sizi bundan koruyor mu? Neden?
- Bir kurucuya `void` ekleyin. Derleniyor mu? Program doğru çalışıyor mu?
