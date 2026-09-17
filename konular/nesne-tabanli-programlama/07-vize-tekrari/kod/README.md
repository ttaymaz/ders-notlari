# 7. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-cikti-tahmini.cs` | **Bölüm A provası** — on bölüm, çıktıyı tahmin edin | `01-konu-haritasi.svg` |
| `02-sinif-yazma-provasi.cs` | **Bölüm B provası** — sınavdakiyle aynı formatta bir soru | `02-sinif-tasarim-adimlari.svg` |
| `03-karma-cozum.cs` | Provanın çözümü ve puanlama ölçütleri | — |

> `02` dosyasını çözmeden `03`'e bakmayın. Çözümü okumak, çözmek değildir.

## Çalıştırma

```bash
dotnet run 01-cikti-tahmini.cs
```

## Denemeniz için

- `01-cikti-tahmini.cs` dosyasındaki **on bölümün çıktısını kağıda yazın**,
  sonra çalıştırın. Tutmayan her bölüm için dosyanın sonundaki soruyu
  cevaplayın.
- Aynı dosyada 9. bölüm: `d` değişkeni `Demirbas` tipinde olduğu hâlde neden
  `Kitap`'ın versiyonu çalıştı? `virtual`'ı silerseniz ne değişir?
- Aynı dosyada 10. bölüm: `Harita` neden yalnızca `Harita` yazdırıyor?
- `02-sinif-yazma-provasi.cs` içindeki yedi `TODO` maddesini tamamlayın.
  Çıktınız dosyanın sonundaki beklenen çıktıyla aynı mı?
- `03-karma-cozum.cs` içinde kurucu `this.AylikUcret` (özellik) kullanıyor.
  Bunu `aylikUcret` (alan) yapın ve `new Uye("Test", -500m)` deneyin. Ne
  değişti? Bu neden önemli?
- Çözüm dosyasındaki `OgrenciUye.ToplamBorc` metodu `base.ToplamBorc(ay)`
  çağırıyor. Hesabı baştan yazsaydınız hangi puan ölçütünü kaybederdiniz?
- `Uye` sınıfından üçüncü bir tür türetin: `EmekliUye`, indirim %30 olsun.
  Kaç satır sürdü?
