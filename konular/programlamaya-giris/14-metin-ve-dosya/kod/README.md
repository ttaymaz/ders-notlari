# 14. Hafta — Örnek Kodlar

| Dosya | Konu |
| ----- | ---- |
| `01-string-metotlari.cs` | `Trim`, `ToUpper`, `Substring`, `Split` |
| `02-string-degismezlik.cs` | **En sık yapılan hata** |
| `03-dosyaya-yazma.cs` | `StreamWriter` ve `using` |
| `04-dosyadan-okuma.cs` | `StreamReader`, üç okuma yöntemi |
| `05-gunluk.cs` | Entegre uygulama |

## Çalıştırma

```bash
dotnet run 03-dosyaya-yazma.cs     # önce yaz
dotnet run 04-dosyadan-okuma.cs    # sonra oku
```

## Denemeniz için

- `02-string-degismezlik.cs` neden `ad.Trim();` tek başına işe yaramıyor?
- `03-dosyaya-yazma.cs` içindeki `append: true` yerine `false` yazın.
  Dosyaya ne oluyor?
- `03-dosyaya-yazma.cs` birkaç kez çalıştırın. Dosya büyüyor mu?
- `04-dosyadan-okuma.cs` çalıştırmadan önce `notlarim.txt` dosyasını silin.
  Program çöküyor mu? Hangi kontrol koruyor?
- `05-gunluk.cs` içinde arama yaparken `ToLower()` çağrılarını silin.
  "Ders" yazıp "ders" aramak çalışıyor mu?

## Dosya nereye kaydediliyor?

Göreli yol (`"gunluk.txt"`) kullandığımız için dosya, programın **çalıştığı**
klasöre yazılır. `Path.GetFullPath(yol)` ile tam konumu öğrenebilirsiniz.
Visual Studio'da bu genelde `bin\Debug\net10.0\` klasörüdür.
