# 9. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-tek-liste.cs` | Farklı türler tek listede, tek döngüde | `01-tek-liste.svg` |
| `02-if-zinciri-vs-polimorfizm.cs` | Aynı iş iki yöntemle | `02-yeni-tur-eklemek.svg` |
| `03-tip-donusumu.cs` | `is`, `as`, `(Tur)` ve referansın sınırı | — |
| `04-kutuphane-raporu.cs` | Tek listeden tam bir rapor | — |

## Çalıştırma

```bash
dotnet run 04-kutuphane-raporu.cs
```

## Denemeniz için

- `01-tek-liste.cs` içindeki `virtual` kelimelerini silin. Çıktıdaki üç satır
  ne oldu? Bu, altıncı haftanın hangi tuzağı?
- Aynı dosyada `GunlukCeza` metodunu `Kitap` sınıfında da ezin (2 TL yapın).
  Toplam nasıl değişti? Döngüye dokundunuz mu?
- `02-if-zinciri-vs-polimorfizm.cs` içine `Harita : Demirbas` türü ekleyin,
  ödünç süresi 30 gün olsun. **İki yöntemde de** ekleyin ve dokunduğunuz
  satırları sayın.
- `03-tip-donusumu.cs` içindeki `d.Yazar` satırının yorumunu kaldırın. Hata
  mesajı hangi tipten bahsediyor — nesnenin mi, değişkenin mi?
- Aynı dosyada `(Kitap)d2` dönüşümünü `d2 as Kitap` ile değiştirin. Program
  çöküyor mu? Hangisi ne zaman tercih edilmeli?
- `04-kutuphane-raporu.cs` içinde `CezaHesapla` metodu neden `virtual` değil?
  `virtual` yapsaydınız her alt sınıfta ne yazmak zorunda kalırdınız?
- Aynı dosyaya `Harita` türü ekleyin (30 gün, 2 TL). Rapor döngüsüne kaç satır
  eklemeniz gerekti?
