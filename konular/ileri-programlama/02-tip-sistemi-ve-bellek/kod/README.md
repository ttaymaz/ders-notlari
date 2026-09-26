# 2. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-atama-farki.cs` | Atama: `struct` kopyalar, `class` paylaşır · kutulama | `01-atama-bellek.svg` |
| `02-metoda-gecirme.cs` | Parametre de bir kopyadır · `ref` | — |
| `03-liste-ve-struct.cs` | Dizide ve listede `struct` | — |
| `04-esitlik.cs` | "Aynı nesne mi?" ile "aynı değer mi?" | — |
| `05-record.cs` | `record`: değer eşitliği, `ToString`, `with` | — |
| `06-kaybolan-eleman.cs` | `HashSet` içindeki eleman değişirse | — |
| `07-using-sirasi.cs` | `IDisposable`, `using` ve kapanma sırası | — |
| `hatali/01-listede-struct.cs` | **Kasıtlı olarak derlenmez** | — |

## Çalıştırma

```
dotnet run 01-atama-farki.cs
```

### .NET 9 kullanıyorsanız

Bu haftanın dosyalarında `#:` satırı yok. Dosyayı bir konsol projesinin
`Program.cs` dosyasına koyup çalıştırın:

```
dotnet new console -o Deneme
copy 01-atama-farki.cs Deneme\Program.cs
dotnet run --project Deneme
```

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün dosyalar bu
yolla da denendi (C# 13, proje biçimi); çıktılar aşağıdakilerle birebir aynı.

## Denemeniz için

Her soruda önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01-atama-farki.cs` içinde `NoktaS` tipini `record struct`, `NoktaC`
  tipini `record class` yapın (gövdeyi değiştirmeden). Çıktının hangi
  satırı değişir?
- `01-atama-farki.cs` son satırında `kutu` değişkenini `NoktaS` yerine
  `object` olarak bırakıp `kutu.X` yazmayı deneyin. Neden derlenmiyor?
- `02-metoda-gecirme.cs` içindeki `Yenile` metodunu `ref NoktaC n`
  alacak biçimde değiştirin (çağrıyı da). `nc.X` ne olur?
- `03-liste-ve-struct.cs` içinde `NoktaS` tipini `class` yapın. Üç satırın
  hangisi değişir? Neden?
- `hatali/01-listede-struct.cs` dosyasını iki farklı yolla derlenir yapın.
  Biri tipi değiştirmeden olmalı.
- `04-esitlik.cs` içinde `KitapC` sınıfına `Equals` metodunu ezin
  (`override`) ama `==` işlecine dokunmayın. İlk iki satır ne yazar?
- `06-kaybolan-eleman.cs` içinde `Numara` özelliğini `init` yapın. Hangi
  satır derlenmez? Bu iyi bir şey mi?
- `07-using-sirasi.cs` içinde `using var` satırlarından `using`
  kelimesini silin. "kapatıldı" satırları ne olur?

## Çıktılar

Denemeden önce bakmayın. .NET 10.0.12, Windows'ta ölçüldü.

<details>
<summary>01-atama-farki.cs</summary>

```
struct: a.X=1  b.X=5
class : c.X=5  d.X=5
kutu  : 1  a.X=7
```
</details>

<details>
<summary>02-metoda-gecirme.cs</summary>

```
Degistir sonrası : ns.X=1  nc.X=99
Yenile sonrası   : nc.X=99
ref sonrası      : ns.X=99
```
</details>

<details>
<summary>03-liste-ve-struct.cs</summary>

```
dizi[0].X  = 5
liste[0].X = 1
liste[0].X = 5
```
</details>

<details>
<summary>hatali/01-listede-struct.cs</summary>

```
error CS1612: Bir değişken olmadığından 'List<NoktaS>.this[int]' öğesinin
dönüş değeri değiştirilemez
```

İngilizce SDK'da: *Cannot modify the return value of ... because it is not a variable.*
</details>

<details>
<summary>04-esitlik.cs</summary>

```
class  ==     : False
class  Equals : False
record ==     : True
record aynı mı: False
string ==     : True
string aynı mı: False
```
</details>

<details>
<summary>05-record.cs</summary>

```
Kitap { Baslik = İnce Memed, Yazar = Yaşar Kemal, Yil = 1955 }
Kitap { Baslik = İnce Memed 2, Yazar = Yaşar Kemal, Yil = 1969 }
Kitap { Baslik = İnce Memed, Yazar = Yaşar Kemal, Yil = 1955 }
İnce Memed 2 / Yaşar Kemal / 1969
```
</details>

<details>
<summary>06-kaybolan-eleman.cs</summary>

```
Ekledikten sonra     : True
Değiştirdikten sonra : False
Kümede kaç üye var   : 1
Kümedeki üye         : Uye { Ad = Ayşe, Numara = 202 }
```

Üye kümede duruyor, `foreach` onu buluyor; ama `Contains` bulamıyor.
</details>

<details>
<summary>07-using-sirasi.cs</summary>

```
Veritabanı açıldı
Günlük dosyası açıldı
İş yapılıyor
Günlük dosyası kapatıldı
Veritabanı kapatıldı
Calis bitti
```
</details>
