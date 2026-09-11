# 15. Hafta — Dönem Özeti Kodları

| Dosya | Konu |
| ----- | ---- |
| `01-performans-olcumu.cs` | Kronometreyle gerçek ölçüm |
| `02-ikili-arama.cs` | Bahar dönemi fragmanı: daha akıllı algoritma |
| `03-cikti-tahmini.cs` | Final provası — çıktıyı tahmin edin |
| `hatali/final-hata-avi.cs` | 8 hatayı bulun |

## Çalıştırma

```bash
dotnet run 01-performans-olcumu.cs
dotnet run 02-ikili-arama.cs
```

## `03-cikti-tahmini.cs` — kendi kendine teşhis

| Blok | Konu | Yanıldıysanız |
| :--: | ---- | ------------- |
| 1 | `.Length` ve son indis | 9. hafta |
| 2 | En büyüğü ararken başlangıç değeri | 10. hafta |
| 3 | String değişmezliği | 14. hafta |
| 4 | Metot çağrısı ve `return` | 11. hafta |
| 5 | `Split` ve dizi indisi | 14. hafta |
| 6 | `TryParse` başarısız olunca `out` ne olur? | 13. hafta |
| 7 | `catch` ve `finally` sırası | 13. hafta |

## Performans deneyleri

- `01-performans-olcumu.cs` içinde diziyi `10_000_000` yapın. Süre kaç kat arttı?
- İç içe döngü tablosunda `n` 10 kat büyüyünce süre kaç kat arttı?
- `02-ikili-arama.cs` içinde diziyi büyütün. Doğrusal aramanın adımı kaç kat
  arttı, ikili aramanın kaç kat?
