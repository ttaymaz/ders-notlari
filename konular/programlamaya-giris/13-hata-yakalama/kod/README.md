# 13. Hafta — Örnek Kodlar

| Dosya | Konu |
| ----- | ---- |
| `01-try-catch-temel.cs` | Temel `try-catch` |
| `02-hata-turleri.cs` | Farklı hatalar, `catch` sırası |
| `03-tryparse.cs` | `TryParse` ve `out` parametresi |
| `04-guvenli-giris.cs` | Geçerli veri girilene kadar sorma kalıbı |
| `05-saglam-hesap-makinesi.cs` | `throw`, `finally`, çoklu `catch` |

## Çalıştırma

```bash
dotnet run 04-guvenli-giris.cs
```

## Denemeniz için

- `01-try-catch-temel.cs` içine `on` yazın. Program çöküyor mu? `try-catch`'i silip
  tekrar deneyin.
- `02-hata-turleri.cs` içinde `catch (Exception ...)` bloğunu **en üste**
  taşıyın. Derleyici ne diyor?
- `03-tryparse.cs` içine `12abc` yazın. `TryParse` ne döndürüyor?
- `04-guvenli-giris.cs` içine `200` yazın. Neden kabul etmiyor?
  `TryParse` bunu yakalayabilir miydi?
- `05-saglam-hesap-makinesi.cs` içindeki `throw` satırını silin ve `5 / 0`
  yapın. Sonuç ne? Neden hata vermiyor?
