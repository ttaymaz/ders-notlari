# 15. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-cikti-tahmini.cs` | On blok, on dört hafta — çıktıyı tahmin edin | `01-donem-haritasi.svg` |
| `02-tasarim-provasi.cs` | **1., 2. ve 4. soru provası** — dokuz `TODO` | `02-final-yapisi.svg` |
| `03-prova-cozumu.cs` | Provanın çözümü ve puanlama ölçütleri | — |
| `hatali/01-final-hata-avi.cs` | **3. soru provası** — sekiz hata, kasıtlı hatalı | — |

`02` dosyasını çözmeden `03`'e bakmayın. Çözümü okumak, çözmek değildir.

## Çalıştırma

```bash
dotnet run 01-cikti-tahmini.cs
```

`hatali/` klasöründeki dosya **derlenmez.** Bu beklenen davranıştır.

## Yanıldığınız blok hangi haftaya ait?

| Blok | Konu | Hafta |
| :--: | ---- | :---: |
| 1 | Nesneler bağımsızdır | 1 |
| 2 | Alan başlangıcı ve kurucu sırası | 2 |
| 3 | `set` içindeki doğrulama | 3 |
| 4 | `static` alan ile örnek alanı | 4 |
| 5 | Kurucu zinciri sırası | 5 |
| 6 | Ezme mi gizleme mi | 6 |
| 7 | Polimorfizm, temel tip değişken | 9 |
| 8 | `abstract` ve arayüz | 10–11 |
| 9 | `struct` ile `class` atama | 12 |
| 10 | `List` ve `Dictionary` | 13 |

## Denemeniz için

- `01-cikti-tahmini.cs` dosyasındaki **on bloğun çıktısını kağıda yazın**,
  sonra çalıştırın. Tutmayan her blok için yukarıdaki haftaya dönün.
- Aynı dosyada 6. blok: `Cihaz.Bilgi` metodunu `virtual`, `Yazici`
  içindekini `override` yapın. Çıktının hangi yarısı değişti?
- Aynı dosyada 9. blok: `struct NoktaS` ifadesini `class NoktaS` yapın.
  Tek kelime değişti, davranış tamamen değişti.
- `02-tasarim-provasi.cs` içindeki dokuz `TODO` maddesini tamamlayın.
  Çıktınız dosyanın başındaki beklenen çıktıyla aynı mı?
- Aynı dosyada TODO 1: arayüz neden temel sınıfa yazılmıyor? Cevabınızı
  yorum satırı olarak yazın — dördüncü soruda tam olarak bu isteniyor.
- `03-prova-cozumu.cs` içindeki puanlama ölçütlerini okuyun ve kendi
  çözümünüzü **kendiniz puanlayın.** Hangi ölçütten kaç aldınız?
- `hatali/01-final-hata-avi.cs` içindeki sekiz hatayı bulun. Kaçını
  derleyici buldu, kaçını siz buldunuz?
