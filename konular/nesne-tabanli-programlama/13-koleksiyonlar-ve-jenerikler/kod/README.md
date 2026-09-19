# 13. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-dizinin-duvari.cs` | Aynı iş, önce diziyle sonra `List<T>` ile | `01-dizi-ve-liste.svg` |
| `02-liste-temelleri.cs` | Ekleme, silme, arama, gezme, sıralama | — |
| `03-liste-ve-polimorfizm.cs` | `List<Demirbas>` — 9. haftanın raporu, büyüyebilen listeyle | — |
| `04-sozluk.cs` | `Dictionary<TKey, TValue>` ve güvenli erişim | `02-liste-mi-sozluk-mu.svg` |
| `hatali/01-foreach-icinde-silme.cs` | **Kasıtlı hatalı** — gezerken silmek | — |

## Çalıştırma

```bash
dotnet run 01-dizinin-duvari.cs
```

`hatali/` klasöründeki dosya **çalışma zamanında çöker.** Bu beklenen
davranıştır; hatayı bulup düzeltmek alıştırmanın kendisidir.

## Denemeniz için

- `01-dizinin-duvari.cs` içinde dizi bölümünü satır satır sayın, sonra
  `List<T>` bölümünü sayın. Aynı iş kaç satırda yapıldı?
- `02-liste-temelleri.cs` dosyasında `raf.Count` yerine `raf.Length` yazın.
  Derleyici ne diyor? Dizide `Length`, koleksiyonda `Count` kullanılır.
- Aynı dosyada `raf[raf.Count]` okumayı deneyin. Hangi hata gelir? Dizideki
  sınır kuralı burada da geçerli: son indis `Count - 1`.
- `03-liste-ve-polimorfizm.cs` içinde `koleksiyon.Add("Bir metin");`
  satırının yorumunu kaldırın. Hata **derleme** anında mı geliyor, çalışma
  anında mı? Jenerik yapının kazancı tam olarak bu.
- Aynı dosyada `List<Demirbas>` yerine `List<Kitap>` yazın. Hangi satırlar
  derlenmez oldu?
- `04-sozluk.cs` içinde `defter[999]` satırının yorumunu kaldırın. Sonra
  aynı erişimi `TryGetValue` ile yapın. Hangisi programı çökertiyor?
- Aynı dosyada `defter.Add(101, ...)` ile `defter[101] = ...` arasındaki
  farkı deneyin. Biri hata veriyor, diğeri sessizce üzerine yazıyor —
  hangisi hangi durumda doğru seçim?
- `hatali/01-foreach-icinde-silme.cs` dosyasındaki üç çözümü sırayla açın.
  Üçü de aynı sonucu veriyor mu? Hangisini kendi kodunuzda kullanırsınız?
