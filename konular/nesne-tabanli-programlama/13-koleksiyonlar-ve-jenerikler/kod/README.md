# 13. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-dizinin-duvari.cs` | Aynı iş, önce diziyle sonra `List<T>` ile | `01-dizi-ve-liste.svg` |
| `02-liste-temelleri.cs` | Ekleme, silme, arama, gezme, sıralama | — |
| `03-liste-ve-polimorfizm.cs` | `List<Demirbas>` — 9. haftanın raporu, büyüyebilen listeyle | — |
| `04-sozluk.cs` | `Dictionary<TKey, TValue>` ve güvenli erişim | `02-liste-mi-sozluk-mu.svg` |
| `05-dosyadan-koleksiyona.cs` | Metin satırını nesneye çevirmek | — |
| `06-koleksiyonu-kaydetme.cs` | Nesneyi satıra çevirmek — kaydetmek de polimorfik | — |
| `hatali/01-foreach-icinde-silme.cs` | **Kasıtlı hatalı** — gezerken silmek | — |
| `hatali/02-satiri-nesneye-cevirme.cs` | **Kasıtlı hatalı** — üç ayrıştırma tuzağı | — |

## Çalıştırma

```bash
dotnet run 01-dizinin-duvari.cs
```

`hatali/` klasöründeki dosyalar **çalışma zamanında çöker.** Bu beklenen
davranıştır; hatayı bulup düzeltmek alıştırmanın kendisidir.

`05` ve `06` çalıştıkları klasörde birer `.txt` dosyası oluşturur. Bu dosyalar
depoya girmez; silerseniz program bir sonraki çalıştırmada yenisini üretir.

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
- `05-dosyadan-koleksiyona.cs` çalıştıktan sonra `demirbas.txt` dosyasını bir
  metin düzenleyicide açın, yeni bir satır ekleyin ve programı tekrar
  çalıştırın. Kodda tek satır değiştirmeden koleksiyon büyüdü mü?
- Aynı dosyada `int.TryParse` yerine `int.Parse` yazın. Bozuk satırda ne
  oluyor? Hangisi sizin verinize uygun — çökmek mi, atlayıp devam etmek mi?
- `06-koleksiyonu-kaydetme.cs` içinde `Kitap` sınıfının `Satir()` metodunu
  silin. Program derleniyor ve çalışıyor — ama dosyaya ne yazılıyor?
- `hatali/02-satiri-nesneye-cevirme.cs` dosyasındaki iki çökmeyi düzeltin.
  Program hatasız çalıştığında kaç demirbaş okuyor? Dosyada kaç veri satırı
  var? İkisi tutmuyorsa üçüncü hata hâlâ duruyor demektir.
