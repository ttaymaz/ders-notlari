# 10. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-anlamsiz-nesne.cs` | Üç sorun: anlamsız nesne, uydurma varsayılan, sessiz hata | — |
| `02-soyut-sinif.cs` | Aynı program, üç sorun da çözülmüş | `01-soyut-vs-somut.svg` |
| `03-virtual-mi-abstract-mi.cs` | Karar ölçütü: ödeme yöntemleri | `02-virtual-mi-abstract-mi.svg` |
| `04-sekil-hiyerarsisi.cs` | Klasik örnek: geometrik şekiller | — |

İlk iki dosyayı **yan yana açın.**

## Çalıştırma

```bash
dotnet run 03-virtual-mi-abstract-mi.cs
```

## Denemeniz için

- `01-anlamsiz-nesne.cs` çıktısındaki üçüncü satıra bakın. `14` sayısı nereden
  geldi? Bu sayıya kim karar verdi?
- `02-soyut-sinif.cs` içine yeni bir tür ekleyin ama `OduncSuresi` metodunu
  **yazmayın**. Derleyici mesajı size tam olarak neyi söylüyor?
- Aynı dosyada soyut metoda gövde yazmayı deneyin
  (`public abstract int OduncSuresi() { return 14; }`). Ne oluyor?
- `02-soyut-sinif.cs` içindeki `CezaHesapla` metodu soyut değil. Ama soyut
  metotları çağırıyor. Yeni bir tür eklediğinizde bu metoda dokundunuz mu?
- `03-virtual-mi-abstract-mi.cs` içinde `MakbuzYazdir` metodunu `abstract`
  yapın. `Havale` ve `Nakit` sınıflarına ne yazmak zorunda kaldınız?
- Aynı dosyada `ToplamTutar` metodunu `virtual` yapıp `Nakit` sınıfında ezin,
  formülü değiştirin. Bu iyi bir fikir mi? Neden?
- `04-sekil-hiyerarsisi.cs` içinde `Kare` neden `Sekil`den değil
  `Dikdortgen`den türüyor? is-a testini uygulayın.
- Aynı dosyada `Ucgen.Cevre()` metodu `0` döndürüyor. Bu dürüst bir cevap mı?
  Sınıfı nasıl tasarlasaydınız gerçek çevreyi hesaplayabilirdiniz?
