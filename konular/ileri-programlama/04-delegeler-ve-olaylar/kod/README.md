# 4. Hafta — Örnek Kodlar ve Laboratuvar

Bu haftanın taşıyıcısı **kendi bağlı listemiz**, `BagliListe<T>`. Konu
bağlı liste değil; listeye delegeler, lambda ifadeleri ve olaylarla
davranış kazandırmak. .NET'te bağlı listenin hazırı `LinkedList<T>`'dir;
gerçek projede onu kullanırsınız. Biz içini görmek için yazıyoruz.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-bagli-liste.cs` | Düğüm, bağ, sona ekleme | `01-bagli-liste.svg` |
| `02-delege-nedir.cs` | Delege tipi, metodu değişkene koymak, `Func` / `Action` | — |
| `03-davranis-parametresi.cs` | `Bul(Func<T, bool>)`, `HerBiri(Action<T>)`, `EnBuyuk(Comparison<T>)` | — |
| `04-kapanim.cs` | Kapanım ve döngü tuzağı | — |
| `05-olaylar.cs` | `event`, abone olmak ve ayrılmak | — |
| `hatali/01-olayi-disaridan.cs` | **Kasıtlı olarak derlenmez** — olayı dışarıdan tetiklemek | — |
| `lab/bagli-liste-lab.cs` | **Laboratuvar:** beş görev, kendi kendini denetler | — |

## Çalıştırma

```
dotnet run 01-bagli-liste.cs
```

### .NET 9 kullanıyorsanız

Bu haftanın dosyalarında `#:` satırı yok. Dosyayı bir konsol projesinin
`Program.cs` dosyasına koyup çalıştırın:

```
dotnet new console -o Deneme
copy 01-bagli-liste.cs Deneme\Program.cs
dotnet run --project Deneme
```

Laboratuvar için de aynısı: `copy lab\bagli-liste-lab.cs Deneme\Program.cs`.
Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün dosyalar bu
yolla da denendi (C# 13, proje biçimi); çıktılar aşağıdakilerle birebir aynı.

## Laboratuvar

`lab/bagli-liste-lab.cs` içindeki `BagliListe<T>` sınıfında `Ekle`, `Bul`,
`HerBiri` ve `Eklendi` hazır. Beş görev `TODO` ile işaretli:

| Görev | Metot | Ne yapar |
| :---: | ----- | -------- |
| 1 | `int Say(Func<T, bool> kosul)` | Koşulu sağlayan eleman sayısı |
| 2 | `BagliListe<T> Filtrele(Func<T, bool> kosul)` | Koşulu sağlayanlardan **yeni** liste |
| 3 | `BagliListe<TSonuc> Donustur<TSonuc>(Func<T, TSonuc> donusum)` | Her elemanı dönüştürüp **yeni** liste; sonuç başka tipte olabilir |
| 4 | `bool Sil(Func<T, bool> kosul)` | Koşulu sağlayan **ilk** düğümü çıkarır |
| 5 | `Silindi` olayı | Silme başarılıysa tetiklenir |

Dosyayı çalıştırdığınızda alt kısımdaki denetim her görevi sınar ve sonucu
yazar:

```
[YAPILMADI] 1. Say: 1930 sonrası üç kitap
...
0/10 denetim tamam
```

Bir görevi bitirdikçe çalıştırın; `[YAPILMADI]` satırları `[TAMAM]`'a
dönmeli. `[YANLIŞ]` görürseniz kodunuz çalışıyor ama sonuç beklenen değil;
`[HATA]` görürseniz bir istisna fırladı. **Denetim kodunu değiştirmeyin.**

4. görevde en sık yapılan hata: **son** düğümü silerken `_son`'u
güncellemeyi unutmak. Liste doğru görünür, ama bir sonraki `Ekle` yanlış
yere ekler. Denetimlerden biri tam olarak bunu yakalar.

> **Bu metotları tanıyacaksınız.** `Say`, `Filtrele` ve `Donustur`,
> iki hafta sonra LINQ'te `Count`, `Where` ve `Select` adıyla karşınıza
> çıkacak. Önce kendiniz yazıyorsunuz; LINQ'in içinde ne olduğunu orada
> zaten bileceksiniz.

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01-bagli-liste.cs` içinde `_son` alanını silip `Ekle` metodunu yalnızca
  `_bas` ile yazın. Ne değişti? Bin elemanlı bir listeye eklemek şimdi kaç
  adım sürüyor?
- `02-delege-nedir.cs` içinde `Kosul kosul = EskiMi;` satırını
  `Kosul kosul = EskiMi(calikusu);` yapın. Hata ne diyor? İki yazım
  arasındaki fark ne?
- `03-davranis-parametresi.cs` içinde `EnBuyuk` metoduna yazarın soyadına
  göre karşılaştıran bir lambda verin.
- `04-kapanim.cs` içinde `SayacUret` metodunun içindeki `int n = 0;`
  satırını metodun dışına, en üste taşıyın. `s1` ve `s2` ne yazar? Neden?
- `05-olaylar.cs` içinde `raf.Eklendi -= gunluk;` satırının altına
  `raf.Eklendi -= k => eklenen++;` ekleyin. Son satır ne yazar? Lambda ile
  yapılan abonelikten neden bu yolla çıkılamıyor?
- `hatali/01-olayi-disaridan.cs` içinde `Eklendi` tanımından `event`
  kelimesini silin. Dosya derleniyor mu? Program ne yazıyor? Bu neden bir
  tehlike?

## Çıktılar

Denemeden önce bakmayın. .NET 10.0.12, Windows'ta ölçüldü.

<details>
<summary>01-bagli-liste.cs</summary>

```
Eleman sayısı: 4
Kitap { Baslik = Çalıkuşu, Yazar = Reşat Nuri Güntekin, Yil = 1922 }
Kitap { Baslik = Sinekli Bakkal, Yazar = Halide Edib Adıvar, Yil = 1936 }
Kitap { Baslik = Kuyucaklı Yusuf, Yazar = Sabahattin Ali, Yil = 1937 }
Kitap { Baslik = İnce Memed, Yazar = Yaşar Kemal, Yil = 1955 }
```
</details>

<details>
<summary>02-delege-nedir.cs</summary>

```
True
False
True
True
False 1923
```
</details>

<details>
<summary>03-davranis-parametresi.cs</summary>

```
Sinekli Bakkal
Kuyucaklı Yusuf
(yok)
1922 1936 1937 1955
İnce Memed
Kuyucaklı Yusuf
Sinekli Bakkal
```

Son üç satır aynı `EnBuyuk` metodunun üç farklı kuralla çağrılmasıdır:
yıla, başlık uzunluğuna ve Türkçe alfabetik sıraya göre.
</details>

<details>
<summary>04-kapanim.cs</summary>

```
okunan: 3
s1: 3  s2: 2
for       : 3 3 3
for+kopya : 0 1 2
foreach   : 0 1 2
```
</details>

<details>
<summary>05-olaylar.cs</summary>

```
[günlük] Çalıkuşu eklendi
[günlük] Sinekli Bakkal eklendi
Olayın saydığı: 3, listedeki: 3
```

Günlük aboneliği ikinci kitaptan sonra kaldırıldı; üçüncü kitap için günlük
satırı yok, ama sayaç aboneliği devam ettiği için sayı 3.
</details>

<details>
<summary>hatali/01-olayi-disaridan.cs</summary>

```
error CS0070: 'Raf.Eklendi' olayı += veya -= işaretlerinin yalnızca sol
tarafında görünebilir ('Raf' türü içinden kullanılması durumu dışında)
```

İki satırın ikisi de aynı hatayı verir.
</details>
