# 5. Hafta — Örnek Kodlar ve Laboratuvar

Bu haftanın taşıyıcısı **yığın ve kuyruk**; ikisi de geçen haftanın
`Dugum<T>` sınıfı üzerine kuruluyor. Konu bu iki yapı değil; onları
`foreach` ile gezilebilir yapmak: `IEnumerable<T>`, `yield return` ve
ertelenmiş yürütme. .NET'teki hazırları `Stack<T>` ve `Queue<T>`; gerçek
projede onları kullanırsınız.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-foreach-nasil-calisir.cs` | `foreach` = `GetEnumerator` + `MoveNext` + `Current` | — |
| `02-yield-ile-liste.cs` | Geçen haftanın `BagliListe<T>`'si artık `foreach` ile gezilebilir | — |
| `03-ertelenmis-yurutme.cs` | `yield` içeren metot çağrıldığında çalışmaz | — |
| `04-yigin-ve-kuyruk.cs` | Aynı eklemeler, iki farklı gezinti sırası | `01-yigin-ve-kuyruk.svg` |
| `05-sonsuz-dizi.cs` | Bitmeyen dizi, `yield break` | — |
| `06-ertelenmis-filtre.cs` | Hemen çalışan ve ertelenmiş filtre: tanıtımdaki üçüncü ısınma | — |
| `lab/yigin-kuyruk-lab.cs` | **Laboratuvar:** yedi görev, kendi kendini denetler | — |

## Çalıştırma

```
dotnet run 03-ertelenmis-yurutme.cs
```

### .NET 9 kullanıyorsanız

Bu haftanın dosyalarında `#:` satırı yok. Dosyayı bir konsol projesinin
`Program.cs` dosyasına koyup çalıştırın:

```
dotnet new console -o Deneme
copy 03-ertelenmis-yurutme.cs Deneme\Program.cs
dotnet run --project Deneme
```

Laboratuvar için: `copy lab\yigin-kuyruk-lab.cs Deneme\Program.cs`.
Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün dosyalar bu
yolla da denendi (C# 13, proje biçimi); çıktılar aşağıdakilerle birebir aynı.

## Laboratuvar

`lab/yigin-kuyruk-lab.cs` içinde `Yigin<T>` ve `Kuyruk<T>` iskeleti var;
`Ekle` metotları hazır. Yedi görev:

| Görev | Metot | Ne yapar |
| :---: | ----- | -------- |
| 1 | `Yigin<T>.Cikar()` | Tepedekini çıkarıp döndürür; boşsa `InvalidOperationException` |
| 2 | `Yigin<T>.Bak()` | Tepedekini döndürür ama **çıkarmaz** |
| 3 | `Yigin<T>.GetEnumerator()` | Tepeden aşağı `yield return` |
| 4 | `Kuyruk<T>.Cikar()` | Baştakini çıkarır; son eleman çıkınca kuyruk tamamen boşalır |
| 5 | `Kuyruk<T>.GetEnumerator()` | Baştan sona `yield return` |
| 6 | `IlkN<T>(kaynak, n)` | İlk `n` eleman; **sonsuz** bir kaynakla da çalışmalı |
| 7 | `Tersten<T>(kaynak)` | Kendi yığınınızla ters çevirme |

Çalıştırdığınızda her denetim için `[TAMAM]`, `[YANLIŞ]`, `[HATA]` veya
`[YAPILMADI]` yazar. **Denetim kodunu değiştirmeyin.**

İki görev bilerek tuzaklı:

- **4. görev:** son eleman çıkınca yalnızca `_bas` değil `_son` da
  boşalmalı. Unutursanız kuyruk boş görünür ama bir sonraki `Ekle` eski
  düğüme bağlanır. Denetimlerden biri tam olarak bunu sınar.
- **6. görev:** kaynağın hepsini önce bir listeye toplayıp sonra ilk `n`
  tanesini verirseniz sonlu kaynaklarda doğru çalışır, ama sonsuz kaynakta
  hiç bitmez. Denetimdeki sonsuz kaynak 10.000 elemandan sonra durur ve
  `[HATA]` ile ne olduğunu söyler.

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `03-ertelenmis-yurutme.cs` içinde ilk `foreach` döngüsünü silin. Ekrana
  `[başladı]` yazılır mı? Neden?
- `03-ertelenmis-yurutme.cs` içinde `var sayilar = Uret();` satırını
  `var sayilar = Uret().ToList();` yapın. Çıktının sırası nasıl değişir?
  "İkinci kez" bölümünde `[başladı]` yazılır mı?
- `04-yigin-ve-kuyruk.cs` içinde `Cikar` çağrılarını `string.Join`
  satırlarından **önce** yapın. Yığın ve kuyruk satırları ne yazar?
- `05-sonsuz-dizi.cs` içinde `if (f > 100) break;` satırını silin.
  Program ne yapar? Ne zaman durur? (Durdurmak için `Ctrl+C`.)
- `06-ertelenmis-filtre.cs` içinde `sayilar.Add(4);` satırını
  `sayilar.Clear();` yapın. İki satır ne yazar?
- `02-yield-ile-liste.cs` içinde sınıf tanımındaki `: IEnumerable<T>`
  kısmını ve en alttaki `IEnumerator IEnumerable.GetEnumerator()` satırını
  silin; `public IEnumerator<T> GetEnumerator()` metodunu bırakın. `foreach`
  yine derleniyor mu? `foreach` neye bakıyor: arayüze mi, metoda mı?
  (Yalnızca arayüzü silerseniz başka bir hata alırsınız; o da öğretici.)

## Çıktılar

Denemeden önce bakmayın. .NET 10.0.12, Windows'ta ölçüldü.

<details>
<summary>01-foreach-nasil-calisir.cs</summary>

```
Çalıkuşu
Sinekli Bakkal
İnce Memed
---
Çalıkuşu
Sinekli Bakkal
İnce Memed
```
</details>

<details>
<summary>02-yield-ile-liste.cs</summary>

```
Çalıkuşu (1922)
Sinekli Bakkal (1936)
İnce Memed (1955)
```
</details>

<details>
<summary>03-ertelenmis-yurutme.cs</summary>

```
Uret() çağrıldı
  [başladı]
  [üretiliyor: 1]
alındı: 1
  [üretiliyor: 2]
alındı: 2
  [üretiliyor: 3]
alındı: 3
  [bitti]
--- ikinci kez
  [başladı]
  [üretiliyor: 1]
alındı: 1
```

İlk satır `Uret() çağrıldı`: metot çağrıldığında gövdesinin hiçbir satırı
çalışmadı. İkinci gezinti metodu **baştan** çalıştırıyor; `break` yüzünden
`[bitti]` hiç yazılmıyor.
</details>

<details>
<summary>04-yigin-ve-kuyruk.cs</summary>

```
Yığın : İnce Memed, Sinekli Bakkal, Çalıkuşu
Kuyruk: Çalıkuşu, Sinekli Bakkal, İnce Memed
Yığından çıkan : İnce Memed
Kuyruktan çıkan: Çalıkuşu
Stack<T>: İnce Memed, Sinekli Bakkal, Çalıkuşu
```
</details>

<details>
<summary>05-sonsuz-dizi.cs</summary>

```
0 1 1 2 3 5 8 13 21 34 55 89
0 1 1 2 3 5 8 13 21
```
</details>

<details>
<summary>06-ertelenmis-filtre.cs</summary>

```
Hemen      : 2 3
Ertelenmiş : 2 3 4
```
</details>
