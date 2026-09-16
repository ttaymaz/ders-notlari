# 3. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-acik-kapi.cs` | `public` alan neden yetersiz | — |
| `02-private-ve-metot.cs` | İlk çözüm: `private` alan + metotlar | `01-erisim-belirleyiciler.svg` |
| `03-ozellikler.cs` | Özelliğin dört biçimi | `02-ozellik-akisi.svg` |
| `04-banka-hesabi.cs` | Kapsüllemenin klasik örneği | `01-erisim-belirleyiciler.svg` |
| `hatali/01-private-alana-erisim.cs` | **Kasıtlı olarak bozuk** | — |

`hatali/` klasöründeki dosya derlenmez; öyle olması gerekiyor.

## Çalıştırma

```bash
dotnet run 04-banka-hesabi.cs
```

## Denemeniz için

- `01-acik-kapi.cs` çalıştırın. Eksi stoklu, eksi fiyatlı bir ürünün toplam
  değeri neden artı çıkıyor?
- `02-private-ve-metot.cs` içindeki `klavye.stokAdedi = -50;` satırının
  yorumunu kaldırın. Derleyici ne diyor?
- `03-ozellikler.cs` içinde `Vize` özelliğinin `set` bloğundaki `return`
  satırını silin. Kontrol hâlâ işe yarıyor mu?
- Aynı dosyada `get { return vize; }` yerine `get { return Vize; }` yazın.
  Program ne yapıyor? Neden hata mesajı bile alamıyorsunuz?
- `04-banka-hesabi.cs` içinde `hesap.Bakiye = 1000000m;` satırını açın.
  Hata mesajı hangi kelimeyi kullanıyor?
- `BankaHesabi` sınıfına `FaizIsle(decimal oran)` metodu ekleyin. Faiz oranı
  dışarıdan serbestçe değiştirilebilmeli mi? Kararınızı gerekçelendirin.
- `hatali/01-private-alana-erisim.cs` dosyasındaki iki hatayı giderin.
  Dosyanın sonundaki üçüncü soru bu haftanın en önemli sorusudur.
