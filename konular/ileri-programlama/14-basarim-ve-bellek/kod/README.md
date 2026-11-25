# 14. Hafta — Örnek Kodlar ve Laboratuvar

**Başarım ölçümü ve bellek ayırma:** tek bir `Stopwatch` ölçümü neden
yanıltır, hangi ölçüm her makinede aynı çıkar, metin birleştirmenin,
kutulamanın, `class` ile `struct`'ın bellekteki bedeli, `Span<T>` ve
koleksiyon seçimi. Taşıyıcı dönemin yapılarının kendisi: bağlı liste,
liste, dizi, `HashSet`; laboratuvarda da hiç büyümeyen bir kuyruk, halka
tampon.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-olcmenin-tuzaklari.cs` | İlk çağrı, ısınma, çok tekrar; yansımalı çağrı | `01-ne-olculur.svg` |
| `02-bellek-ayirma.cs` | `+=` / `StringBuilder`, kutulama, `class` / `struct` dizisi, `List<T>` büyümesi | — |
| `03-span.cs` | `ReadOnlySpan<char>` ile ayrıştırma, pencere kopya değildir, `stackalloc` | — |
| `04-koleksiyon-secimi.cs` | **Taşıyıcı:** bağlı liste / liste / dizi; `List` ve `HashSet` ile arama | — |
| `olcum/` | **BenchmarkDotNet:** birleştirme, arama, yansımalı çağrı | — |
| `lab/` | **Laboratuvar:** 24 hazır test, dört görev | `02-halka-tampon.svg` |

## Çalıştırma

```
dotnet run -c Release 02-bellek-ayirma.cs
```

**Süre ölçen dosyaları `-c Release` ile çalıştırın** (`01`, `04`). Debug
derlemede iyileştirmeler kapalıdır. `02` ve `03` bellek ölçer; ikisinde de
aynı sonucu verir.

**Bu haftanın çıktıları hakkında.** Ayrılan bayt sayıları aynı .NET
sürümünde her makinede aynıdır; aşağıdaki "Çıktılar" bölümünde birebir
verildi. Süreler her makinede farklıdır; dosyalar bu yüzden süreyi değil
yalnızca "en az N kat mı?" sorusunun cevabını yazar. Sayılar Türkçe
kültürde binlik ayracı nokta ile yazılır (`2.027.968`).

BenchmarkDotNet:

```
cd olcum
dotnet run -c Release --project Olcum -- --filter "*"
```

Tam ölçüm birkaç dakika sürer ve bilgisayarınızı meşgul eder; ölçüm
sırasında başka bir şey çalıştırmayın. Tek bir sınıf için
`--filter "*Arama*"`. Daha kısa ama daha az güvenilir bir ölçüm için sona
`--job short` ekleyin. Sonuçlar `BenchmarkDotNet.Artifacts/results/`
klasörüne de yazılır.

### .NET 9 kullanıyorsanız

**Tek dosyalık örnekler:** Dosyayı bir konsol projesinin `Program.cs`
dosyasına kopyalayın:

```
dotnet new console -o Deneme
copy 02-bellek-ayirma.cs Deneme\Program.cs
dotnet run -c Release --project Deneme
```

**BenchmarkDotNet ve laboratuvar:** `.csproj` dosyalarında `net10.0` →
`net9.0`. Laboratuvarda `dotnet test BasarimLab.Testler`.

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün örnekler ve
laboratuvar C# 13 ile de denendi; çıktılar ve test sonuçları aynı.

## Laboratuvar

```
cd lab
dotnet test
```

İlk çalıştırmada 24 testten **22'si kırmızı**, 2'si yeşildir. Yeşil olan
ikisi hazır `Olcum.AyrilanBayt`'ı sınar; testler görevlerinizi bununla
ölçüyor.

| Görev | Dosya | Ne yapar | Test |
| :---: | ----- | -------- | :--: |
| 1 | `BasarimLab/RafKodu.cs` | "TR-813-07"yi doğrula ve ayrıştır; **0 bayt** ayır | 10 |
| 2 | `BasarimLab/HalkaTampon.cs` | Sabit kapasiteli kuyruk; doluyken en eskinin yerine yazar; ekleme **0 bayt** | 6 |
| 3 | `BasarimLab/KitapIndeksi.cs` | ISBN'e ve yazara göre arama; 50 000 kitapta 1 saniyeden kısa | 4 |
| 4 | `BasarimLab/RaporYazici.cs` | 2000 satırlık rapor; ayrılan bellek metnin 5 katından az | 2 |

**Testleri değiştirmeyin.** Kırmızı bir testin önce adını, sonra mesajını
okuyun; bellek testleri kaç bayt ayrıldığını yazar.

**Bu testler süre değil bayt ölçer;** makinenizin hızına bağlı değildir.
Tek istisna 3. görevin hız testidir. Orada sınır, doğru bir çözümün
süresinin onlarca katıdır (doğru çözüm 67 ms sürdü). Listeyi baştan sona
tarayan bir çözümle bu test **yaklaşık bir dakika** sürer ve kırmızı olur;
beklemek, farkı görmenin bir parçası.

İpuçları:

- **1. görev:** Önce uzunluğa ve sabit karakterlere (`T`, `R`, iki `-`)
  bakın, sonra rakamlara. `int.Parse(ReadOnlySpan<char>)` ara metin
  üretmez. `ToString()`, `Split`, `Substring` bellek ayırır
- **2. görev:** Ölçüm, ekleme işini **iki kez** çalıştırır (biri ısınma);
  ikincisinde tek bayt bile ayrılmamalı. Diziyi kurucuda bir kez açın;
  `Ekle` içinde `new` olmasın
- **3. görev:** Bir değil iki sözlük gerekebilir. "Ekleme sırasıyla"
  isteği için yazarın kitaplarını bir `List<Kitap>`'te tutun
- **4. görev:** `StringBuilder`; satırlar arasına `'\n'`, son satırdan
  sonra değil

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01` dosyasında `double ilk = sureler[0];` satırının üstüne
  `Console.WriteLine(string.Join(" | ", sureler.Select(x => x.ToString("F1"))));`
  ekleyin ve birkaç kez çalıştırın. İlk ölçüm sonrakilerin kaç katı?
  Sonrakiler birbirine ne kadar yakın?
- `02` dosyasında `new StringBuilder()` yerine `new StringBuilder(2000)`
  yazın. Ayrılan bayt ne kadar değişir? Neden çok az?
- `03` dosyasında `puanlar.AsSpan(1, 3)` yerine `puanlar[1..4]` yazın.
  `puanlar` satırı ne yazar? Neden?
- `03` dosyasında `SpanIle` içinde `s.Slice(3, 3)` yerine
  `kod.Substring(3, 3)` (ve diğerini de) yazın. Span ile ayrılan kaç bayt
  olur?
- `04` dosyasında `sayilar` ve `sorular` dizilerini 10 000 yerine 10
  elemanlı yapın. `HashSet` hâlâ en az 20 kat hızlı mı? BenchmarkDotNet'in
  `Arama` tablosuyla karşılaştırın.
- `olcum/` içinde `Cagri.Ikiye` metodunun üstündeki `[MethodImpl(...)]`
  satırını silin ve yalnızca bu sınıfı ölçün (`--filter "*Cagri*"`).
  "Dogrudan" satırı ne gösterir? Neden?

## Çıktılar

Denemeden önce bakmayın. .NET 10 çalışma zamanı, Windows'ta ölçüldü.

<details>
<summary>01-olcmenin-tuzaklari.cs</summary>

```
=== 1 === (aynı iş, on ölçüm)
ilk ölçüm, sonrakilerin ortalamasının en az 5 katı mı? True

=== 2 === (doğrudan çağrı ve yansımayla çağrı)
yansımayla çağrı, doğrudan çağrının en az 5 katı mı? True
```

Süreleri yazdırınca bir ölçümde şöyle çıktı (mikrosaniye, sizinki farklı
olur): `708,3 | 34,2 | 28,0 | 26,8 | 28,7 | 28,4 | 29,0 | 26,0 | 23,4 | 23,7`.
İlk ölçüm ötekilerin yaklaşık 25 katı: metot o anda JIT ile derleniyordu.
Sonrakiler de yavaşça düşüyor: .NET sık çağrılan kodu bir süre sonra
yeniden, daha iyi derler.
</details>

<details>
<summary>02-bellek-ayirma.cs</summary>

```
=== 1 === (1000 parçayı birleştirmek)
+=            : 2.027.968 bayt
StringBuilder :     8.696 bayt
+= en az 100 kat fazla mı? True

=== 2 === (1000 int: List<object> ve List<int>)
List<object> :  24.000 bayt  (eleman başına 24)
List<int>    :       0 bayt

=== 3 === (1000 nokta: class dizisi ve struct dizisi)
class : 32.024 bayt
struct:  8.024 bayt

=== 4 === (List<T> nasıl büyür?)
kapasite: 0 → 4 → 8 → 16 → 32 → 64 → 128
büyüyerek          :  8.424 bayt
kapasite baştan 1000:  4.056 bayt
```

`new StringBuilder(2000)` ile `StringBuilder` satırı `8.096 bayt` olur:
yalnızca 600 bayt azalır. Ayrılanın büyük kısmı sonuçta üretilen 2000
karakterlik metnin kendisidir (`ToString`); onu önleyen bir kapasite yok.
</details>

<details>
<summary>03-span.cs</summary>

```
=== 1 === (raf kodunu ayrıştırmak)
Split ile: bölüm 813, raf 7
Span ile : bölüm 813, raf 7
Split ayırıyor mu? True
Span ile ayrılan: 0 bayt

=== 2 === (pencere kopya değildir)
orta    : 40, 30, 99
puanlar : 10, 40, 30, 99, 50

=== 3 === (stackalloc)
stackalloc: 0 bayt · new int[16]: 88 bayt
```

`puanlar[1..4]` ile `puanlar : 10, 20, 30, 40, 50` olur: bir **dizinin**
aralığı yeni bir dizi (kopya) üretir, `Span<int>` o kopyaya bakar. Bir
**span'ın** aralığı (`span[1..4]`) ise kopya değildir. `Substring` ile
"Span ile ayrılan" `64 bayt` olur: iki küçük metin.
</details>

<details>
<summary>04-koleksiyon-secimi.cs</summary>

```
=== 1 === (1000 sayı, üç yapı)
bağlı liste      : 32.032 bayt  (düğüm başına ≈ 32)
List<int>(1000)  :  4.056 bayt
int[1000]        :  4.024 bayt

=== 2 === (10 000 elemanda 10 000 kez "var mı?")
bulunan: List 1429, HashSet 1429
HashSet en az 20 kat hızlı mı? True
```

Bu makinede oran 57 ile 79 kat arasında çıktı. 10 elemanla `False`: oran
0,6 ile 1,1 arasında; küçük koleksiyonda liste en az `HashSet` kadar
hızlı. BenchmarkDotNet'in tablosu da aynısını söylüyor.
</details>

<details>
<summary>olcum/ — BenchmarkDotNet (bu makinede: Ryzen 7 5800HS, .NET 10.0.15)</summary>

Sizin sayılarınız farklı olur; oranlar ve **Allocated** sütunu
benzerdir.

| Sınıf | Ölçüm | Ortalama | Ayrılan |
| --- | --- | ---: | ---: |
| `Birlestirme` | `Arti`, 10 parça | 55 ns | 448 B |
| | `Yapici`, 10 parça | 59 ns | 272 B |
| | `Tekrar`, 10 parça | 49 ns | 104 B |
| | `Arti`, 1000 parça | 84 562 ns | 2 027 968 B |
| | `Yapici`, 1000 parça | 1 270 ns | 8 696 B |
| | `Tekrar`, 1000 parça | 2 785 ns | 4 064 B |
| `Arama` | `ListeIcinde`, 10 | 1,7 ns | — |
| | `KumeIcinde`, 10 | 2,3 ns | — |
| | `ListeIcinde`, 10 000 | 404 ns | — |
| | `KumeIcinde`, 10 000 | 2,3 ns | — |
| `Cagri` | `Dogrudan` | 0,25 ns | — |
| | `Delege` | 0,73 ns | — |
| | `Yansima` | 15,8 ns | 24 B |

`[MethodImpl]` silinince `Dogrudan` 0,01 ns gibi "olamayacak kadar" kısa
bir süre gösterir: derleyici iki satırlık metodu çağıran yere kopyalar
(satır içine alma) ve ortada ölçülecek bir çağrı kalmaz.
</details>

<details>
<summary>lab/ — dotnet test (ilk çalıştırma)</summary>

```
Başarısız! - Başarısız: 22, Başarılı: 2, Atlanan: 0, Toplam: 24
```

Dört görev bitince `Başarılı! - Başarısız: 0, Başarılı: 24`.
</details>
