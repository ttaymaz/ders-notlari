# 9. Hafta — Örnek Kodlar

| Dosya | Konu |
| ----- | ---- |
| `01-dizi-olusturma.cs` | Tanımlama, oluşturma, `.Length` |
| `02-indis-erisimi.cs` | İndisle yazma/okuma, `IndexOutOfRangeException` |
| `03-varsayilan-degerler.cs` | `new` sonrası gözlerde ne var? |
| `04-not-ortalamasi.cs` | Dizi elemanlarıyla hesaplama |

## Çalıştırma

```bash
dotnet run 02-indis-erisimi.cs
```

## Denemeniz için

- `02-indis-erisimi.cs` sonundaki `notlar[3]` satırını açın. Program çöküyor —
  hata mesajı ne diyor?
- `01-dizi-olusturma.cs` içinde `isimler.Length()` yazın (parantezli). Neden
  derlenmiyor? `Length` bir metot mu, özellik mi?
- `03-varsayilan-degerler.cs` içinde `string` dizisinin varsayılanı neden `0`
  veya boş metin değil de `null`?
- `04-not-ortalamasi.cs` içinde `3.0` yerine `3` yazın. Ortalama ne oluyor?
- **Asıl soru:** `04-not-ortalamasi.cs` 100 not için nasıl yazılırdı?
