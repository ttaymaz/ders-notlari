# Koleksiyonlar, Jenerikler ve Dosyadan Veri

Geçen hafta bir nesneyi başka bir değişkene atadığınızda ne kopyalandığını gördük. Bu hafta iki soru soruyoruz:

1. Nesneleri **bir arada** nasıl tutacağız?
2. Program kapandığında o nesneler nereye gidecek?

Birinci sorunun cevabı `List<T>` ve `Dictionary<TKey, TValue>`. Bu adları daha önce duymuş, hatta kullanmış olabilirsiniz. Bu hafta onlara **yeni bir iş** veriyoruz: bir sınıf ailesinin nesnelerini taşımak ve bir dosyadan beslenmek.

> Bu haftanın asıl konusu `Add` ile `Remove` değil. Üç soru: `<T>` neyi garanti eder, `List<Demirbas>` neden `List<int>`'ten farklı bir iş yapar, ve bir metin satırı nasıl nesneye dönüşür?

---

## 1. Hatırlatma: Dizinin İki Duvarı

Şimdiye kadar nesneleri hep dizide tuttuk:

```csharp
Demirbas[] koleksiyon = new Demirbas[5];
```

Bu satır bir söz verir: *tam beş demirbaş olacak.* Altıncı kitap geldiğinde o söz bozulur.

**Birinci duvar: boyut sabittir.** Altıncıyı eklemenin tek yolu daha büyük bir dizi açıp hepsini kopyalamaktır.

**İkinci duvar: ortadan silmek zahmetlidir.** Kalanları elle sola kaydırır, sayacınızı azaltırsınız:

```csharp
for (int i = 1; i < adet - 1; i++)
{
    raf[i] = raf[i + 1];      // sola kaydır
}
adet--;
```

Üstelik kaç hücrenin **gerçekten dolu** olduğunu `Length` söylemez; onu ayrı bir `adet` değişkeniyle siz takip edersiniz — ve bu sayacı bir yerde güncellemeyi unuttuğunuz gün program sessizce yanlış çalışır.

![Dizi ile List karşılaştırması](assets/01-dizi-ve-liste.svg)

---

## 2. `List<T>` — Hızlı Tur

```csharp
List<string> raf = new List<string>();

raf.Add("Tutunamayanlar");
raf.Add("Sefiller");
raf.Remove("Sefiller");

Console.WriteLine(raf.Count);     // 1
```

Boyut yazılmadı, sayaç tutulmadı, kaydırma yapılmadı.

| Üye | Ne yapar |
| --- | -------- |
| `Add(x)` | Sona ekler |
| `Insert(i, x)` | `i` indisine sokar, kalanları kaydırır |
| `Remove(x)` | **Değere** göre siler; bulamazsa `false` döner |
| `RemoveAt(i)` | **İndise** göre siler |
| `RemoveAll(koşul)` | Koşulu sağlayan her elemanı siler, sayısını döndürür |
| `Count` | Eleman sayısı |
| `Contains(x)` · `IndexOf(x)` | Var mı? Kaçıncı indiste? (yoksa `-1`) |
| `Sort()` · `Clear()` | Sırala · hepsini sil |

> **`Length` değil `Count`.** Dizide `Length`, koleksiyonlarda `Count`. İkisi de aynı şeyi söyler; adları farklıdır ve karıştırılır.

İndisle erişim dizideki gibidir: `raf[0]`, `raf[raf.Count - 1]`. Sınır kuralı da aynıdır — son indis `Count - 1`'dir. Başlangıç değeriyle kurabilirsiniz:

```csharp
List<int> notlar = new List<int> { 65, 90, 45, 78 };
```

---

## 3. `<T>` Ne Demek? Jenerikler

`List<string>` ifadesindeki köşeli parantezler bir **tür parametresidir**. Listeye "hangi türle çalışacaksın?" diye söylersiniz, o da yalnızca o türü kabul eder.

```csharp
List<Demirbas> koleksiyon = new List<Demirbas>();

koleksiyon.Add(new Kitap(101, "Tutunamayanlar", "Oğuz Atay"));   // olur
koleksiyon.Add("Tutunamayanlar");                                 // DERLENMEZ
```

İkinci satır çalışma zamanında değil, **derleme zamanında** reddedilir. Hatayı siz kod yazarken görürsünüz; kullanıcı görmez.

### Neden var?

C#'ın ilk sürümlerinde `ArrayList` vardı ve her şeyi `object` olarak saklardı. İki sorun çıkarıyordu:

1. **Tip güvenliği yoktu.** Sayı listesine yanlışlıkla metin eklenebiliyor, hata ancak çalışırken ortaya çıkıyordu.
2. **Değer tipleri kutulanıyordu.** Geçen haftadan hatırlayın: `int` bir değer tipidir ve stack'te yaşar. `object` olarak saklanabilmesi için heap'te bir "kutu" açılır ve değer oraya kopyalanır. Buna **boxing** denir ve her eleman için ayrı bir bellek tahsisi demektir.

Jenerikler ikisini birden çözdü. `List<int>` tamsayıları doğrudan tutar; kutu açılmaz.

> `ArrayList` bugün **kullanılmaz.** Eski kodda görürseniz tanıyın, yeni kodda yazmayın.

### `List<int>` ile `List<Demirbas>` aynı şey değil

`List<int>` bir **kap**: sayıları tutar, hepsi aynı türdendir.

`List<Demirbas>` ise bir **söz**: "burada ne varsa `Demirbas`'tır." Ama bu, hepsinin aynı sınıftan olduğu anlamına gelmez — `Kitap` da, `Dergi` de birer `Demirbas`'tır.

Jenerik yapının bu dersteki asıl kazancı burada ortaya çıkıyor, ve bir sonraki bölümün konusu bu.

---

## 4. `List<Demirbas>`: Jenerik ile Polimorfizm Buluşuyor

Dokuzuncu haftada bir dizide farklı türden demirbaşlar tutup tek döngüyle rapor almıştık. Tek değişiklik dizinin liste olması:

```csharp
List<Demirbas> koleksiyon = new List<Demirbas>();

koleksiyon.Add(new Kitap(101, "Tutunamayanlar", "Oğuz Atay"));
koleksiyon.Add(new Dergi(201, "Bilim ve Teknik", 745));

foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine(d.Etiket());     // her nesne kendi sürümünü çalıştırır
}
```

Liste `Demirbas` tipindedir, ama içine `Kitap` ve `Dergi` girebilir — çünkü ikisi de birer `Demirbas`'tır.

**Üç şey aynı anda oluyor ve üçü de derleyicinin garantisi altında:**

| Garanti | Neyin sayesinde |
| ------- | --------------- |
| Listeye metin, sayı veya alakasız bir nesne **giremez** | Tür parametresi `<Demirbas>` |
| `Demirbas` ailesinin **her** üyesi girebilir | Kalıtım |
| Her satır kendi biçiminde yazılır | `virtual` / `override` |

Dönem boyunca kurduğumuz yapı bu altı satırda birleşiyor: kalıtım aileyi tanımladı, ezme davranışı türe göre ayırdı, jenerik liste hepsini tek yerde topladı.

### Türe göre ayırmak

Bazen "kaç tanesi kitap?" diye sormanız gerekir:

```csharp
int kitapSayisi = 0;

foreach (Demirbas d in koleksiyon)
{
    if (d is Kitap) { kitapSayisi++; }
}
```

`is` operatörü "bu nesne o türden **mi, ya da ondan türemiş mi**" diye sorar. `GetType() == typeof(Kitap)` ise yalnızca tam eşleşmeye bakar; alt türleri saymaz.

> **Bir ölçü:** `foreach` döngünüzün içinde çok sayıda `if (d is ...)` birikiyorsa, orada aslında bir `virtual` metot olması gerektiğini düşünün. Raporu yazan kod türleri saymak zorunda kalmamalıdır.

> Bahar döneminde bu liste bir formdaki tabloya bağlanacak. Döngü ve hesaplar aynı kalacak; değişen yalnızca çıktının nereye yazıldığı olacak.

---

## 5. Dosyadan Koleksiyona

Buraya kadar yazdığımız her program aynı kurala tabiydi: program kapandığında bellekteki her şey silindi. Kütüphane kataloğu her açılışta boş başlıyorsa o katalog işe yaramaz.

Veriyi bir **dosyada** tutacağız. Ama bu bölümün konusu dosya okumak değil — dosyadan gelen şey bir **metin satırı**, ihtiyacımız olan şey bir **nesne**. Aradaki dönüşüm asıl iştir.

### 5.1 Okuma tarafı

```csharp
if (!File.Exists(yol))
{
    Console.WriteLine("Dosya bulunamadı.");
    return;
}

string[] satirlar = File.ReadAllLines(yol);
```

`File.ReadAllLines` dosyayı açar, tüm satırları okur, kapatır ve bir **dizi** döndürür. Dosyayı kapatmayı unutma ihtimaliniz yoktur.

> Çok büyük dosyalarda tüm içeriği belleğe almak istemeyebilirsiniz; `StreamReader` ile satır satır okumak o zaman tercih edilir. Bu dersin örneklerinde `ReadAllLines` yeterli.

### 5.2 Satırdan nesneye

Dosya biçimimiz basit — alanlar noktalı virgülle ayrılıyor ve ilk alan **tür kodu**:

```
# tur;no;baslik;ek
K;101;Tutunamayanlar;Oğuz Atay
D;201;Bilim ve Teknik;745
```

Çeviriyi ayrı bir metoda alıyoruz. Dönüş tipi dikkat isteyen yer:

```csharp
static Demirbas? SatiriCevir(string satir)
{
    string[] alan = satir.Split(';');

    if (alan.Length != 4) { return null; }                            // eksik satır
    if (!int.TryParse(alan[1].Trim(), out int no)) { return null; }   // bozuk numara

    string baslik = alan[2].Trim();

    switch (alan[0].Trim().ToUpper())
    {
        case "K": return new Kitap(no, baslik, alan[3].Trim());
        case "D": return new Dergi(no, baslik, int.Parse(alan[3].Trim()));
        default:  return null;
    }
}
```

Metot `Demirbas` döndürüyor, ama ürettiği nesne `Kitap` ya da `Dergi`. **Kalıtım olmasaydı bu metot yazılamazdı:** iki ayrı tür döndürmek zorunda kalır, ikisini tek listede toplayamazdınız.

Çağıran taraf ise türlerden habersiz:

```csharp
List<Demirbas> koleksiyon = new List<Demirbas>();

foreach (string ham in satirlar)
{
    string satir = ham.Trim();

    if (satir.Length == 0 || satir.StartsWith("#")) { continue; }   // veri değil

    Demirbas? d = SatiriCevir(satir);

    if (d is not null) { koleksiyon.Add(d); }
}
```

**Neden dizi değil liste?** Çünkü dosyada kaç **veri** satırı olduğunu bilmiyorsunuz. `satirlar.Length` toplam satır sayısını verir — boş satırlar, yorum satırları ve bozuk satırlar dahil. Kaç nesne üretileceği ancak okuma bitince belli olur.

### 5.3 Üç savunma

Dosyadan gelen veri **sizin yazmadığınız** veridir. Üç şeyi baştan varsaymayın:

| Varsayım | Ne olur | Çözüm |
| -------- | ------- | ----- |
| "Her satır veridir" | Başlık satırında `FormatException` | Boş ve `#` ile başlayan satırları atlayın |
| "Her satırda dört alan var" | `alan[3]` → `IndexOutOfRangeException` | `alan.Length` kontrolü |
| "İkinci alan her zaman sayıdır" | `FormatException` | `int.Parse` yerine `int.TryParse` |

`TryParse` çevirebilirse `true` döner ve sonucu `out` değişkenine yazar; çeviremezse `false` döner ve **program çalışmaya devam eder**. Hangisini istediğinize karar vermek sizin işiniz: bir kütüphane kataloğunda bozuk satırı atlayıp devam etmek makuldür, bir muhasebe dosyasında sessizce atlamak tehlikelidir.

### 5.4 Yazma tarafı da polimorfik

Kaydetmek, okumanın aynasıdır. Ekrana yazdığımız biçim ile dosyaya yazdığımız biçim **aynı şey değildir**, çünkü işleri farklıdır: biri insana okunur olmalı, diğeri geri okunabilir olmalı. Bu yüzden iki ayrı `virtual` metot:

```csharp
class Demirbas
{
    public virtual string Etiket()                // ekrana
    {
        return $"#{DemirbasNo} {Baslik}";
    }

    public virtual string Satir()                 // dosyaya
    {
        return $"?;{DemirbasNo};{Baslik};";
    }
}

class Kitap : Demirbas
{
    public override string Satir()
    {
        return $"K;{DemirbasNo};{Baslik};{Yazar}";
    }
}
```

Kaydeden döngünün türlerden haberi yok:

```csharp
List<string> satirlar = new List<string> { "# tur;no;baslik;ek" };

foreach (Demirbas d in koleksiyon)
{
    satirlar.Add(d.Satir());        // hangi sınıfsa onun sürümü çalışır
}

File.WriteAllLines(yol, satirlar);
```

Yeni bir demirbaş türü eklediğinizde bu döngüde **tek satır değişmez.** Yeni sınıf kendi `Satir()` metodunu yazar, gerisi kendiliğinden çalışır.

Bir tasarımın iyi olup olmadığını ölçmenin en pratik yolu budur: *yeni bir tür eklediğimde kaç yeri değiştirmem gerekiyor?*

> **`WriteAllLines` üzerine yazar.** Dosya varsa eski içerik silinir. Sonuna eklemek istiyorsanız `File.AppendAllLines` kullanın. İkisini karıştırmak, bir dönemlik kaydı tek çalıştırmada silmenin en kolay yoludur.

### 5.5 Ayraç verinin içinde geçerse

Başlığı `Kar; Beyaz ve Soğuk` olan bir kitabı bu biçimle kaydederseniz, geri okurken satır beş alana bölünür ve kayıt bozulur. Gerçek sistemlerin bu iş için JSON gibi biçimler kullanmasının sebeplerinden biri budur.

Şimdilik kuralı bilin: **ayraç, veride geçmeyecek bir karakter olmalıdır.**

### 5.6 Tür kodu seçerken `i` harfinden kaçının

Yukarıdaki çözüm tür kodunu `alan[0].Trim().ToUpper()` ile normalleştiriyor. Bu, `k` ve `d` için çalışır. Ama tür kodunuz `i` olursa çalışmaz:

```csharp
Console.WriteLine("i".ToUpper() == "I");     // Türkçe kültürde: False
```

Türkçede küçük `i` harfinin büyüğü **`İ`**'dir (noktalı), `I` değil. İşletim sisteminiz Türkçe olduğu için `ToUpper()` bu kuralı uygular ve karşılaştırma tutmaz. Program çökmez; o satır **sessizce kaybolur.**

> **Pratik kural:** tür kodlarını `i`, `I`, `ı` ve `İ` harflerinden seçmeyin. `K`, `D`, `T`, `S` gibi harfler her kültürde aynı davranır.

Karşılaşmak zorunda kalırsanız doğru araç `ToUpperInvariant()`'tır: kültürden bağımsız çalışır. Bu dersin konusu değil, ama adını duymuş olun — bir gün açıklayamadığınız bir hatanın cevabı olacak.

---

## 6. `Dictionary<TKey, TValue>`: Anahtarla Erişim

Listede bir kaydı sırasıyla bulursunuz. Peki elinizde sıra numarası değil, demirbaş numarası varsa? Listeyle tek yol vardır: baştan sona gezmek. On bin kayıtta ortalama beş bin karşılaştırma.

Oysa bir sözlükte "elma" kelimesini ararken tüm sayfaları okumazsınız — doğrudan "E" harfine gidersiniz.

```csharp
Dictionary<int, Demirbas> defter = new Dictionary<int, Demirbas>();

foreach (Demirbas d in koleksiyon)
{
    defter[d.DemirbasNo] = d;        // anahtar: demirbaş numarası
}

Console.WriteLine(defter[101].Etiket());      // doğrudan, gezmeden
```

İki tür parametresi vardır: **anahtar** (`TKey`) ve **değer** (`TValue`). Anahtar benzersiz olmalıdır.

> Yukarıdaki örnekte aynı nesneler hem listede hem sözlükte duruyor — ama **kopyalanmıyorlar.** `Demirbas` bir referans tipidir; iki yapı da aynı nesneyi gösterir. Geçen haftanın konusu tam olarak buydu.
>
> Dikkat edilecek yer şurası: bir nesneyi listeden silerseniz sözlükten **silinmez.** İki yapı birbirinden habersizdir.

### Olmayan anahtar

`defter[999]` yazarsanız ve 999 kayıtlı değilse program **çöker** (`KeyNotFoundException`). İki güvenli yol vardır:

```csharp
// Yol 1: önce sor
if (defter.ContainsKey(999)) { Console.WriteLine(defter[999].Etiket()); }

// Yol 2: tek adımda sor ve al
if (defter.TryGetValue(102, out Demirbas? bulunan))
{
    Console.WriteLine(bulunan.Etiket());
}
```

İkincisi tercih edilir: `ContainsKey` sözlüğe iki kez bakar, `TryGetValue` bir kez.

Dosya okurken kullandığımız `int.TryParse` ile aynı kalıptır — **"olabilir de olmayabilir de" durumlarında `Try...` metotları.** Bu kalıbı tanıyın; .NET'in her yerinde karşınıza çıkacak.

### `Add` ile indisleyici farkı

| Yazım | Anahtar yoksa | Anahtar varsa |
| ----- | ------------- | ------------- |
| `defter.Add(101, x)` | Ekler | **Hata verir** |
| `defter[101] = x` | Ekler | **Sessizce üzerine yazar** |

Hangisini seçeceğiniz niyetinize bağlıdır. "Bu kayıt zaten varsa bir sorun var" demek istiyorsanız `Add` kullanın — hata sizi uyarsın. Bu, dosyadan okurken işe yarar: aynı demirbaş numarası iki satırda geçiyorsa bunu bilmek istersiniz.

### Gezme

```csharp
foreach (KeyValuePair<int, Demirbas> kayit in defter)
{
    Console.WriteLine($"{kayit.Key} -> {kayit.Value.Etiket()}");
}
```

---

## 7. Hangisini Seçmeli?

![Dizi, liste ve sözlük arasında karar](assets/02-liste-mi-sozluk-mu.svg)

| Soru | Cevap |
| ---- | ----- |
| Eleman sayısı baştan belli ve sabit mi? | **Dizi** |
| Sayı değişecek, sırayla gezeceğim | **`List<T>`** |
| Benzersiz bir anahtarla arayacağım | **`Dictionary<TKey, TValue>`** |

Kararsız kaldığınızda `List<T>` seçin. Dizinin kazandırdığı bellek, günlük programlamada ölçülemeyecek kadar küçüktür; kaybettiğiniz esneklik ise her gün canınızı yakar.

---

## 8. Sık Yapılan Hatalar

**Koleksiyonu gezerken değiştirmek.** Bu haftanın en sinsi tuzağı:

```csharp
foreach (string kitap in raf)
{
    if (kitap.StartsWith("S")) { raf.Remove(kitap); }   // ÇALIŞMA ZAMANI HATASI
}
```

Derleyici bir şey söylemez. Program çalışır ve *Collection was modified; enumeration operation may not execute* diyerek çöker. `foreach`, koleksiyonun gezinti boyunca değişmeyeceğine güvenir.

Üç çözümü var ve üçü de `kod/hatali/01-foreach-icinde-silme.cs` dosyasında:

1. Sondan başa indisli döngü
2. Önce silinecekleri topla, sonra sil
3. `RemoveAll` kullan

<details><summary>Neden sondan başa? — önce kendiniz düşünün</summary>

Baştan sona indisli döngüde `raf.RemoveAt(i)` yaptığınızda, sonraki elemanlar bir sola kayar. Ama `i` yine de artar — böylece kaydırılan eleman **atlanır.** Sondan başa giderken silinen elemanın solunda kalan indisler kaymadığı için sorun çıkmaz.
</details>

**`Count` yerine `Length` yazmak.** Derleyici yakalar, ama her dönem birkaç kez olur.

**`Remove` başarısız olduğunda fark etmemek.** Bulamazsa hata vermez, `false` döner. Silinip silinmediği önemliyse dönüş değerini kontrol edin.

**Olmayan anahtarı doğrudan okumak.** `defter[x]` yerine `TryGetValue` alışkanlığı edinin.

**`Add` ile indisleyiciyi karıştırmak.** Aynı anahtarı ikinci kez `Add` ile eklemek hata verir; indisleyici sessizce üzerine yazar. İkinci durumda eski kayıt kaybolur ve bunu kimse fark etmez.

**Dosyadan gelen veriye güvenmek.** Başlık satırını atlamamak, alan sayısını saymamak ve `Parse` kullanmak — üçü de programı çökertir.

**Tür kodunu ham hâliyle karşılaştırmak.** Dosyada `k` yazıyorsa `alan[0] == "K"` tutmaz. Program çökmez; o satır **sessizce kaybolur.** Bu haftanın en tehlikeli hatası budur, çünkü hiçbir belirti vermez.

**Tür kodunu `i` harfinden seçmek.** `ToUpper()` yazsanız bile Türkçe kültürde `"i"` → `"İ"` olur ve karşılaştırma yine tutmaz (5.6).

<details><summary><code>hatali/02-satiri-nesneye-cevirme.cs</code> cevap anahtarı — önce kendiniz deneyin</summary>

| # | Hata | Sonucu | Doğrusu |
| - | ---- | ------ | ------- |
| 1 | Yorum ve boş satırlar elenmiyor | Başlık satırında `FormatException` | Satır boşsa ya da `#` ile başlıyorsa `continue` |
| 2 | Alan sayısı doğrulanmıyor | Boş satırda `IndexOutOfRangeException` | `if (alan.Length != 4) { continue; }` |
| 3 | Tür kodu `Trim().ToUpper()` yapılmadan karşılaştırılıyor | `k;102;Sefiller` satırı **sessizce** atlanır | `switch (alan[0].Trim().ToUpper())` |

İlk iki hata düzeltildikten sonra program hatasız çalışır ve *"3 demirbaş okundu"* yazar. Oysa dosyada **dört** veri satırı vardır. Üçüncü hatayı ancak sayarak bulursunuz — asıl ders bu.
</details>

**İyi pratik: liste tipini olabildiğince dar tutun.** `List<Demirbas>` yerine `List<object>` yazmak her şeyi kabul eder — jenerik yapının bütün faydasını çöpe atar.

---

## 9. Örnek Kodlar

Bu haftanın kodları [`kod/`](kod/) klasöründe:

| Dosya | Konu |
| ----- | ---- |
| [`01-dizinin-duvari.cs`](kod/01-dizinin-duvari.cs) | Aynı iş, önce diziyle sonra `List<T>` ile |
| [`02-liste-temelleri.cs`](kod/02-liste-temelleri.cs) | Ekleme, silme, arama, gezme, sıralama |
| [`03-liste-ve-polimorfizm.cs`](kod/03-liste-ve-polimorfizm.cs) | `List<Demirbas>` — tek döngüyle karma rapor |
| [`04-sozluk.cs`](kod/04-sozluk.cs) | `Dictionary` ve güvenli erişim |
| [`05-dosyadan-koleksiyona.cs`](kod/05-dosyadan-koleksiyona.cs) | Metin satırını nesneye çevirmek |
| [`06-koleksiyonu-kaydetme.cs`](kod/06-koleksiyonu-kaydetme.cs) | Nesneyi satıra çevirmek — kaydetmek de polimorfik |
| [`hatali/01-foreach-icinde-silme.cs`](kod/hatali/01-foreach-icinde-silme.cs) | **Kasıtlı hatalı** — gezerken silmek |
| [`hatali/02-satiri-nesneye-cevirme.cs`](kod/hatali/02-satiri-nesneye-cevirme.cs) | **Kasıtlı hatalı** — üç ayrıştırma tuzağı |

> `05` ve `06` çalıştıkları klasörde birer `.txt` dosyası oluşturur. Bu dosyalar projenin klasöründe değil, programın **çalıştığı** klasörde oluşur; nerede olduğunu `Path.GetFullPath` ile programa söyletebilirsiniz.

---

## 10. İsteğe Bağlı Ev Uygulaması

**Problem:** Bir araç filosunu dosyadan yükleyip yönetin.

1. Geçen haftaların `Tasit`, `Otomobil` ve `Kamyon` sınıflarını kullanın; `Tasit` sınıfında `Plaka` özelliği olsun
2. `filo.txt` adında bir dosya hazırlayın. Her satır bir araç olsun, ilk alan tür kodunu versin:

   ```
   # tur;plaka;marka;ek
   O;03ABC123;Fiat;5
   K;03XYZ789;Ford;12000
   ```

3. Dosyayı okuyup `List<Tasit>` doldurun. Tür kodunu okuyan `switch` **tek bir yerde** olsun
4. Filoyu tek döngüyle yazdırın — her araç kendi biçiminde görünsün
5. `Dictionary<string, Tasit>` oluşturun; anahtar **plaka** olsun
6. Kullanıcıdan plaka isteyin, sözlükte arayın, bulunursa detayları yazdırın
7. Programdan çıkarken filoyu `filo.txt` dosyasına geri yazın

**Kritik sorular:**

- Dosyada aynı plaka iki kez geçerse ne olmalı — hata mı, güncelleme mi? Seçiminize göre `Add` ya da indisleyici kullanın ve **neden** öyle seçtiğinizi bir yorum satırıyla yazın.
- Yedinci adımdan sonra dosyayı açıp bakın: içerik doğru mu? Değilse hangi sınıfın `Satir()` metodu eksik?

**Zorlayıcı ekler:**

1. Filodan bir aracı plakasına göre silen bir bölüm yazın. Hem listeden hem sözlükten silmeyi unutmayın — iki yapı birbirinden habersizdir.
2. `foreach` içinde silmeyi bilerek deneyin, hatayı görün, sonra üç çözümden birini uygulayın.
3. Dosyaya kasten bozuk bir satır ekleyin. Programınız çöküyor mu, atlıyor mu, yoksa sessizce yanlış mı okuyor? Üçüncüsüyse düzeltin.
4. Yeni bir `Motosiklet` sınıfı ekleyin. Kaç dosyada, kaç satır değiştirmeniz gerekti? Bu sayı tasarımınızın notudur.

---

## Gelecek Hafta

Bu hafta son yapı taşını da yerine koyduk. Elinizde artık sınıf, kalıtım, ezme, polimorfizm, soyutlama, arayüz, koleksiyon ve kalıcı veri var.

Gelecek hafta yeni bir konu yok: **hepsini tek bir tasarımda birleştireceğiz.** Küçük bir sistemi baştan sona kuracak, hangi kararı neden verdiğimizi konuşacağız.

---

## Kaynaklar

- Microsoft. *List\<T\> sınıfı.* https://learn.microsoft.com/tr-tr/dotnet/api/system.collections.generic.list-1
- Microsoft. *Dictionary\<TKey,TValue\> sınıfı.* https://learn.microsoft.com/tr-tr/dotnet/api/system.collections.generic.dictionary-2
- Microsoft. *Jenerikler.* https://learn.microsoft.com/tr-tr/dotnet/csharp/fundamentals/types/generics
- Microsoft. *File sınıfı.* https://learn.microsoft.com/tr-tr/dotnet/api/system.io.file
- Microsoft. *Koleksiyonlar (C#).* https://learn.microsoft.com/tr-tr/dotnet/csharp/tour-of-csharp/tutorials/collections

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
