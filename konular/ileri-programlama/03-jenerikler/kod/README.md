# 3. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-kutulama-maliyeti.cs` | `ArrayList` ve `List<int>`: bellek ve tip güvenliği | — |
| `02-jenerik-raf.cs` | Kendi jenerik tipimiz: `Raf<T>`, `default` | — |
| `03-jenerik-metot.cs` | Jenerik metot, kısıt, tür çıkarımı | — |
| `04-jenerik-arayuz.cs` | Jenerik arayüz ve birden çok kısıt: `IDepo<T>` | — |
| `05-statik-alan.cs` | Her kapalı tipin kendi statik alanı | — |
| `06-kovaryans.cs` | `IEnumerable<out T>`, `List<T>` ve dizi kovaryansı | `01-varyans-yonu.svg` |
| `07-kontravaryans.cs` | `IComparer<in T>` ve Türkçe sıralama | `01-varyans-yonu.svg` |
| `hatali/01-kisit-yok.cs` | **Kasıtlı olarak derlenmez** — kısıt olmadan `>` | — |
| `hatali/02-kisit-ihlali.cs` | **Kasıtlı olarak derlenmez** — kısıtı karşılamayan tip | — |

Bu haftanın veri tipleri `record` ile yazıldı. `06` ve `07` dosyalarında
`record` kalıtımı var: `record Kitap(...) : Yayin(Baslik)`.

## Çalıştırma

```
dotnet run 01-kutulama-maliyeti.cs
```

### .NET 9 kullanıyorsanız

Bu haftanın dosyalarında `#:` satırı yok. Dosyayı bir konsol projesinin
`Program.cs` dosyasına koyup çalıştırın:

```
dotnet new console -o Deneme
copy 01-kutulama-maliyeti.cs Deneme\Program.cs
dotnet run --project Deneme
```

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün dosyalar bu
yolla da denendi (C# 13, proje biçimi); çıktılar aşağıdakilerle birebir aynı.

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01-kutulama-maliyeti.cs` içinde `Adet` değerini 10 katına çıkarın. İki
  ölçüm de 10 katına mı çıkıyor? `ArrayList` için bir `int` başına kaç bayt
  düşüyor?
- `02-jenerik-raf.cs` içinde `Raf<int>` için `Al(0)` neden `0` yazıyor?
  `Raf<string>` olsaydı ne yazardı?
- `03-jenerik-metot.cs` içinde son satırı `EnBuyuk(3, 7.5)` yapın. Derleniyor
  mu? Derleniyorsa `T` ne olarak çıkarıldı, nereden anlıyorsunuz? Sonra
  `EnBuyuk(3, "7")` yazın: bu kez ne değişti?
- `hatali/01-kisit-yok.cs` ve `hatali/02-kisit-ihlali.cs` dosyalarını
  derlenir hale getirin. İkinci dosyada `Kitap` için `CompareTo` metodunu
  yazmanız gerekecek.
- `04-jenerik-arayuz.cs` içinde iki kısıttan da `class` kelimesini silin.
  Program hâlâ derleniyor mu? Şimdi `record Kitap` satırını
  `record struct Kitap` yapın. Hangi satır, hangi hatayı veriyor? `class`
  kısıtı bu hatayı nasıl önlüyordu?
- `05-statik-alan.cs` içinde `Sayac<T>` sınıfını jenerik olmaktan çıkarın
  (`Sayac`), çağrıları da buna göre değiştirin. Çıktı ne olur?
- `06-kovaryans.cs` içindeki `List<Yayin> liste = kitaplar;` satırını
  açın. Hata kodu ne? Bir alttaki satır neden bu hatanın gerekçesi?
- `07-kontravaryans.cs` içinde `StringComparison.CurrentCulture` yerine
  `StringComparison.Ordinal` yazın. Sıra nasıl değişiyor? Neden?

## Çıktılar

Denemeden önce bakmayın. .NET 10.0.12, Windows'ta ölçüldü.

<details>
<summary>01-kutulama-maliyeti.cs</summary>

```
ArrayList : 40,8 MB
List<int> : 8,4 MB
Unhandled exception. System.InvalidCastException: Unable to cast object of type 'System.String' to type 'System.Int32'.
```

Bellek ölçümü .NET 8 çalışma zamanında da aynı çıktı. Çökme bilerek
bırakıldı: `ArrayList` bir `string` kabul etti, hata ancak okunurken geldi.
</details>

<details>
<summary>02-jenerik-raf.cs</summary>

```
False
Raf<Kitap>: 2/2 dolu
Raf<Dergi>: 1/3 dolu
Dergi { Ad = Kütüphane Bülteni, Sayi = 12 }
True
0
```
</details>

<details>
<summary>03-jenerik-metot.cs</summary>

```
7
elma
29.10.1923
2,5
```

Tarih ve ondalık sayı biçimi bilgisayarın kültürüne göre değişir; burada Türkçe.
</details>

<details>
<summary>hatali/01-kisit-yok.cs ve hatali/02-kisit-ihlali.cs</summary>

```
error CS0019: '>' işleci 'T' ve 'T' türündeki işlenenlere uygulanamaz
```

```
error CS0311: 'Kitap' türü, 'EnBuyuk<T>(T, T)' genel türü veya yöntemi için
'T' tür parametresi olarak kullanılamaz. 'Kitap' türünden
'System.IComparable<Kitap>' türüne örtük bir başvuru dönüştürmesi yoktur.
```
</details>

<details>
<summary>04-jenerik-arayuz.cs</summary>

```
Kitap { Kod = KTP-002, Baslik = Kuyucaklı Yusuf, Yazar = Sabahattin Ali }
True
Uye { Kod = UYE-101, AdSoyad = Ayşe Yılmaz }
2 kitap, 1 üye
```
</details>

<details>
<summary>05-statik-alan.cs</summary>

```
int    : 2
string : 1
Kitap  : 3
double : 0
```
</details>

<details>
<summary>06-kovaryans.cs</summary>

```
Çalıkuşu
Kuyucaklı Yusuf
ArrayTypeMismatchException
```

Yorum satırı açılırsa: `error CS0029` — `List<Kitap>` örtük olarak
`List<Yayin>` türüne dönüştürülemez.
</details>

<details>
<summary>07-kontravaryans.cs</summary>

```
Çalıkuşu
İnce Memed
Orta Direk
Ölmez Otu
Sinekli Bakkal
```

`Ordinal` ile: `Orta Direk`, `Sinekli Bakkal`, `Çalıkuşu`, `Ölmez Otu`,
`İnce Memed` — harfler Türkçe alfabeye göre değil, karakter kodlarına göre
sıralanır.
</details>
