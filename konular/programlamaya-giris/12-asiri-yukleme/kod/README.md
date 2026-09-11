# 12. Hafta — Örnek Kodlar

| Dosya | Konu |
| ----- | ---- |
| `01-overload-temel.cs` | Aynı isim, farklı parametreler |
| `02-imza-kurallari.cs` | Neyi değiştirmek yeterli, neyi değil |
| `03-secim-tuzagi.cs` | C# hangi versiyonu seçer? |
| `04-hesap-makinesi.cs` | Metotlar + `switch` + aşırı yükleme |

## Çalıştırma

```bash
dotnet run 04-hesap-makinesi.cs
```

## Denemeniz için

- `02-imza-kurallari.cs` sonundaki geçersiz örnekleri açın. Derleyici ne diyor?
- `03-secim-tuzagi.cs` içinde `AlanHesapla(5)` ile `AlanHesapla(5.0)` neden
  farklı sonuç veriyor? Bu bir hata mı, tasarım mı?
- `03-secim-tuzagi.cs` içinde `Carp(4, 5)` çalışıyor ama `double` versiyonu var.
  C# ne yaptı?
- `04-hesap-makinesi.cs` içine `Bol(double, double)` metodunu ekleyin ve
  sıfıra bölme kontrolünü metodun içine taşıyın. Metot ne döndürmeli?
