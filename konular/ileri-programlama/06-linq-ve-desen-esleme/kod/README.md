# 6. Hafta — Örnek Kodlar ve Laboratuvar

İki yarı: **LINQ** (geçen haftanın bağlı listesi üzerinde) ve **desen
eşleme**. İkinci yarının taşıyıcısı bir **ifade ağacı**: `(2 + 3) * 4` gibi
bir ifade, kayıtlardan kurulan bir ağaç olarak tutuluyor ve tek bir
`switch` ile hesaplanıyor.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-linq-kendi-listemizde.cs` | `IEnumerable<T>` uygulayan listemizde LINQ kendiliğinden çalışıyor | — |
| `02-genisletme-metodu.cs` | Kendi `Nerede` ve `Sec` metotlarımız: LINQ'in içi | — |
| `03-linq-operatorleri.cs` | Sıralama, `First`, `Any` / `All`, `Min` / `Max`, `GroupBy`, sorgu sözdizimi | — |
| `04-ertelenmis-linq.cs` | LINQ ertelenmiş çalışır: lambda kaç kez çağrılıyor? | — |
| `05-desen-esleme.cs` | İlişkisel, özellik ve liste desenleri | — |
| `06-ifade-agaci.cs` | Kayıtlarla ifade ağacı; `Hesapla`, `Yazdir`, `Sadelestir` | `01-ifade-agaci.svg` |
| `lab/linq-ve-agac-lab.cs` | **Laboratuvar:** sekiz görev, kendi kendini denetler | — |

## Çalıştırma

```
dotnet run 04-ertelenmis-linq.cs
```

### .NET 9 kullanıyorsanız

Bu haftanın dosyalarında `#:` satırı yok. Dosyayı bir konsol projesinin
`Program.cs` dosyasına koyup çalıştırın:

```
dotnet new console -o Deneme
copy 04-ertelenmis-linq.cs Deneme\Program.cs
dotnet run --project Deneme
```

Laboratuvar için: `copy lab\linq-ve-agac-lab.cs Deneme\Program.cs`.
Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün dosyalar bu
yolla da denendi (C# 13, proje biçimi); çıktılar aşağıdakilerle birebir aynı.

## Laboratuvar

`lab/linq-ve-agac-lab.cs` iki bölümden oluşur:

| Görev | Bölüm | Ne yapar |
| :---: | ----- | -------- |
| 1 | LINQ | 1950 sonrası başlıklar, yıla göre sıralı |
| 2 | LINQ | Yazar başına kitap sayısı (`Dictionary<string, int>`) |
| 3 | LINQ | En eski kitabın başlığı |
| 4 | LINQ | Bir yazarın kitabı var mı? |
| 5 | Ağaç | `Hesapla`'ya `Fark` (çıkarma) düğümü |
| 6 | Ağaç | `Yazdir`'a `Fark` düğümü |
| 7 | Ağaç | `Sadelestir`: `x * 0`, `0 * x`, `x * 1`, `x + 0`, `0 + x` kuralları |
| 8 | Ağaç | Ağaçtaki sayı düğümlerinin sayısı (LINQ ile) |

Dosya kendi kendini denetler; `[TAMAM]`, `[YANLIŞ]`, `[HATA]` veya
`[YAPILMADI]` yazar. **Denetim kodunu değiştirmeyin.**

7. görev en zoru. `(9 * 0) + 3` ifadesinin `3`'e inmesi için önce alt
ifadelerin sadeleşmesi (`9 * 0` → `0`), sonra düğümün kendisine kural
uygulanması (`0 + 3` → `3`) gerekir. `06-ifade-agaci.cs` içindeki
`Sadelestir` yukarıdan aşağı çalıştığı için bunu yapamaz; görevin
ipucunu okuyun.

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `02-genisletme-metodu.cs` içinde `Nerede` metodunun parametresindeki
  `this` kelimesini silin. Hangi satır derlenmez? Hangisi hâlâ derlenir?
- `03-linq-operatorleri.cs` içinde `First(k => k.Yil > 1950)` yerine
  `First(k => k.Yil > 2000)` yazın. Ne olur? `FirstOrDefault` ile farkı ne?
- `04-ertelenmis-linq.cs` içinde 4. bölümün sonuna `hic.ToList();` ekleyin.
  Çağrı sayısı ne olur?
- `05-desen-esleme.cs` içinde `Etiket` metodunun ilk iki kolunun yerini
  değiştirin. Program derleniyor mu? Derleyici neden itiraz ediyor? Bu
  itiraz size hangi hatadan koruyor?
- `06-ifade-agaci.cs` içinde `Hesapla` metodundaki `_ => throw ...` kolunu
  silin. Program derleniyor mu? Derleyici ne uyarıyor?
- `06-ifade-agaci.cs` içinde `(2 + 3) * 4` yerine `2 + 3 * 4` ifadesinin
  ağacını kurun. `Yazdir` doğru yazıyor mu? Parantezler nerede?

## Çıktılar

Denemeden önce bakmayın. .NET 10.0.12, Windows'ta ölçüldü.

<details>
<summary>01-linq-kendi-listemizde.cs</summary>

```
3
Sinekli Bakkal, Kuyucaklı Yusuf, İnce Memed
8 14 15 10
Çalıkuşu (1922)
Sinekli Bakkal (1936)
Kuyucaklı Yusuf (1937)
```
</details>

<details>
<summary>02-genisletme-metodu.cs</summary>

```
Sinekli Bakkal, Kuyucaklı Yusuf, İnce Memed
Sinekli Bakkal, Kuyucaklı Yusuf, İnce Memed
```
</details>

<details>
<summary>03-linq-operatorleri.cs</summary>

```
Ahmet Hamdi Tanpınar — Huzur
Ahmet Hamdi Tanpınar — Saatleri Ayarlama Enstitüsü
Halide Edib Adıvar — Sinekli Bakkal
İnce Memed
(yok)
True
True
1922–1961
Sabahattin Ali: 3 kitap, ilk 1937
Ahmet Hamdi Tanpınar: 2 kitap, ilk 1949
Reşat Nuri Güntekin: 2 kitap, ilk 1922
Yaşar Kemal: 2 kitap, ilk 1955
Halide Edib Adıvar: 1 kitap, ilk 1936
Saatleri Ayarlama Enstitüsü, Orta Direk, İnce Memed, Huzur, Kürk Mantolu Madonna, İçimizdeki Şeytan
```
</details>

<details>
<summary>04-ertelenmis-linq.cs</summary>

```
3
3 3  çağrı: 8
3 3  çağrı: 4
işletilmeyen sorgu, çağrı: 0
```

İkinci satır: aynı sorgu iki kez `Count` edildi, lambda dört elemanın her
biri için iki kez çağrıldı. Üçüncü satır: `ToList` sorguyu bir kez işletti,
`Count` artık listeye bakıyor.
</details>

<details>
<summary>05-desen-esleme.cs</summary>

```
Çalıkuşu         Cumhuriyet öncesi
Kuyucaklı Yusuf  Cumhuriyetin ilk yılları  uzun başlık
İnce Memed       1950 ve sonrası           Yaşar Kemal
Orta Direk       1950 ve sonrası           Yaşar Kemal, sonraki
1922'den 1960'a, 4 kitap
eski bir kitap: Çalıkuşu
```

`Etiket` kolları yer değiştirirse: `error CS8510` — genel kol
(`{ Yazar: "Yaşar Kemal" }`) özel kolu kapsadığı için ikincisine hiç
ulaşılamaz.
</details>

<details>
<summary>06-ifade-agaci.cs</summary>

```
(2 + 3) * 4
20
(7 * 1 + 0)  →  7
Sayı düğümleri: 3
Sayıların toplamı: 9
```

`_` kolu silinirse: `warning CS8509` — switch ifadesi tam kapsamlı değil.
Program derlenir; ama bilinmeyen bir düğüm gelirse çalışırken istisna
fırlar.
</details>
