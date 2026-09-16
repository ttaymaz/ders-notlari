# 11. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-kalitimin-siniri.cs` | Kalıtımla çözülemeyen durum | `01-dikey-yatay.svg` |
| `02-arayuz-temel.cs` | Aynı program, arayüzle çözülmüş | `01-dikey-yatay.svg` |
| `03-coklu-arayuz.cs` | Birden fazla sözleşme, elmas problemi | `02-secim-olcutu.svg` |
| `04-hazir-arayuz.cs` | `IComparable` ile hazır altyapıya bağlanmak | — |

İlk iki dosyayı **yan yana açın.**

## Çalıştırma

```bash
dotnet run 04-hazir-arayuz.cs
```

## Denemeniz için

- `01-kalitimin-siniri.cs` sonundaki üç denemeyi okuyun. Dördüncü bir çözüm
  düşünebiliyor musunuz?
- `02-arayuz-temel.cs` içindeki `EKitap` sınıfından `IIndirilebilir` ifadesini
  silin. Hangi satırlar derlenmez oldu? Döngüdeki `is` kontrolü ne yapıyor?
- Aynı dosyada `Indir` metodundan `public` kelimesini kaldırın. Derleyici ne
  diyor? Arayüz üyeleri neden `public` olmak zorunda?
- Aynı dosyada `class EKitap : IIndirilebilir, Demirbas` diye sıralamayı ters
  yazın. Ne oluyor?
- `03-coklu-arayuz.cs` içindeki `IDinlenebilir` arayüzüne `void Duraklat();`
  ekleyin. **Kaç sınıf derlenmez oldu?** Bu, arayüzleri küçük tutma kuralının
  deneysel kanıtıdır.
- `04-hazir-arayuz.cs` içindeki `CompareTo` metodunu başlığa göre sıralayacak
  biçimde değiştirin. `Array.Sort` satırına dokundunuz mu?
- Aynı dosyada `Dergi` sınıfına `IComparable<Dergi>` uygulayın ve sıralamayı
  çalıştırın. İstisna kalktı mı?
