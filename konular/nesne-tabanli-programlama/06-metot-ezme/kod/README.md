# 6. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-ezme-temel.cs` | `virtual` / `override` ve ezmeyen sınıf | — |
| `02-base-cagrisi.cs` | Ezmek silmek değil, üzerine eklemek | `02-base-cagrisi.svg` |
| `03-virtual-olmadan.cs` | Ezme ile gizleme arasındaki fark | `01-ezme-akisi.svg` |
| `04-tostring-ezme.cs` | Hazır bir metodu ezmek | — |

## Çalıştırma

```bash
dotnet run 03-virtual-olmadan.cs
```

## Denemeniz için

- `01-ezme-temel.cs` içindeki `virtual` kelimesini silin. Derleyici ne diyor?
  Mesajı kelimesi kelimesine okuyun.
- Aynı dosyada `Harita` sınıfı `BilgiYazdir`'ı ezmiyor. Çıktısı neden diğer
  ikisinden farklı?
- `01-ezme-temel.cs` içindeki üç `BilgiYazdir` metodunu yan yana koyun. Hangi
  satır üçünde de aynı? Bu size hangi haftayı hatırlatıyor?
- `02-base-cagrisi.cs` içindeki `base.BilgiYazdir();` satırını silin. Çıktının
  neyi kayboldu?
- Aynı dosyada `Personel.ToplamMaas` metoduna `- 500m` kesinti ekleyin. Kaç
  yere dokundunuz? İki türetilmiş sınıf da uydu mu?
- `03-virtual-olmadan.cs` çıktısındaki dört satırı karşılaştırın. Hangi ikisi
  aynı nesneye ait ve neden farklı yazıyor?
- `04-tostring-ezme.cs` içindeki `override` kelimesini silin. Sonra metodun
  adını `Yazdir` yapıp `override`'ı geri koyun. İki hata mesajı neden farklı?
