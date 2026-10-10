# Jenerik Programlama: Tipi Sonraya Bırakmak

`List<Kitap>` yazdığınızda bir jenerik tip kullanıyorsunuz. Bu hafta karşı tarafa geçiyoruz: jenerik tipi ve metodu **yazan** taraf olmak. Jeneriklerin neden var olduğunu bir ölçümle görüyor, kendi `Raf<T>` ve `IDepo<T>` tiplerimizi yazıyor, kısıtlarla derleyiciye söz veriyor ve sonunda "kitap listesi bir yayın listesi midir?" sorusunun şaşırtıcı cevabına bakıyoruz. Örneklerin veri tipleri `record` ile yazıldı; geçen haftanın `record` konusu burada pratiğe dönüşüyor.

---

## 1. Açılış: Bir Milyon Sayı, İki Koleksiyon

Aynı işi iki koleksiyonla yapalım: 1 milyon `int` ekleyelim ve bu sırada ne kadar bellek ayrıldığını ölçelim.

```csharp
var eski = new ArrayList();
for (int i = 0; i < 1_000_000; i++)
    eski.Add(i);

var yeni = new List<int>();
for (int i = 0; i < 1_000_000; i++)
    yeni.Add(i);
```

```
ArrayList : 40,8 MB
List<int> : 8,4 MB
```

Aradaki fark geçen haftanın konusu: **kutulama.** `ArrayList` her elemanı `object` olarak saklar; her `int` heap'te ayrı bir kutuya konur (64 bitlik bir sistemde kutu başına 24 bayt) ve listede o kutunun adresi tutulur. `List<int>` ise sayıları kutulamadan, kendi içindeki bir `int[]` dizisinde yan yana tutar.

İkinci fark tip güvenliğidir:

```csharp
eski.Add("bir milyon bir");    // derlenir
// yeni.Add("bir milyon bir"); // derlenmez

foreach (object o in eski)
    toplam += (int)o;          // çalışırken: InvalidCastException
```

`ArrayList` her şeyi kabul eder; hata, yanlış eleman **okunurken** ortaya çıkar — belki eklendiği yerden çok uzakta, belki aylar sonra. `List<int>` aynı hatayı **derlerken** yakalar.

> **Jeneriklerin iki kazancı:** kutulamasız değer tipleri ve derleme zamanında tip güvenliği. İkisi de aynı fikirden gelir: tip, kodu yazarken değil **kullanırken** belirlenir, ama derleyici her kullanımı ayrı ayrı denetler.

`ArrayList`, jeneriklerden önceki .NET'in koleksiyonudur. Yeni kodda kullanılmaz; eski kodda karşınıza çıkarsa nedenini artık biliyorsunuz.

---

## 2. Kendi Jenerik Tipimiz: `Raf<T>`

```csharp
class Raf<T>
{
    private readonly T[] _yerler;
    private int _dolu;

    public Raf(int kapasite) => _yerler = new T[kapasite];

    public bool Koy(T eleman)
    {
        if (_dolu == _yerler.Length)
            return false;
        _yerler[_dolu++] = eleman;
        return true;
    }

    public T? Al(int sira) => sira < _dolu ? _yerler[sira] : default;
}
```

`T` bir **tip parametresidir**: "rafa ne konacağını şimdi bilmiyorum, kullanan söyleyecek." Kullanan söylediğinde ortaya **kapalı bir tip** çıkar:

```csharp
var kitaplar = new Raf<Kitap>(kapasite: 2);
var dergiler = new Raf<Dergi>(kapasite: 3);
var sayilar  = new Raf<int>(kapasite: 1);
```

`Raf<Kitap>` ile `Raf<Dergi>` aynı kaynak koddan gelir ama derleyicinin ve çalışma zamanının gözünde **iki ayrı tiptir.** `kitaplar.Koy(new Dergi(...))` derlenmez.

### `default`: tipin boş değeri

`Al` metodu boş bir yer için ne döndürmeli? `T` bir referans tipiyse `null`, `int` ise `0`, `bool` ise `false`. Hangisi olduğunu yazarken bilmiyoruz. `default` anahtar kelimesi tam bunu söyler: "`T` her ne ise, onun varsayılan değeri."

```csharp
Console.WriteLine(dergiler.Al(1) is null);   // True
Console.WriteLine(sayilar.Al(0));            // 0
```

### `typeof(T)`: tip çalışma zamanında da bilinir

`Raf<T>` içindeki `ToString`, `typeof(T).Name` ile rafın hangi tip için kurulduğunu yazdırır: `Raf<Kitap>: 2/2 dolu`. C#'ta jenerik tip bilgisi çalışma zamanında da korunur. (Java'yı bilenler için: Java'da jenerik tip bilgisi derlemeden sonra silinir; `T`'nin ne olduğunu çalışma zamanında soramazsınız. C#'ta sorabilirsiniz.)

> **`record` hatırlatması.** `record Kitap(string Baslik, string Yazar);` tek satırda kurucu, iki salt okunur özellik, değer eşitliği ve okunaklı `ToString` üretir. `Console.WriteLine(dergiler.Al(0))` satırının `Dergi { Ad = Kütüphane Bülteni, Sayi = 12 }` yazmasının sebebi budur.

---

## 3. Jenerik Metot ve Tür Çıkarımı

Tipin tamamı değil, yalnızca bir metot jenerik olabilir:

```csharp
T EnBuyuk<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) >= 0 ? a : b;
```

```csharp
Console.WriteLine(EnBuyuk(3, 7));                     // 7
Console.WriteLine(EnBuyuk("elma", "armut"));          // elma
Console.WriteLine(EnBuyuk(new DateOnly(1923, 10, 29),
                          new DateOnly(1920, 4, 23)));  // 29.10.1923
Console.WriteLine(EnBuyuk<double>(2, 2.5));            // 2,5
```

İlk üç çağrıda `<int>`, `<string>`, `<DateOnly>` yazmadık; derleyici argümanlardan çıkardı. Buna **tür çıkarımı** denir. Son çağrıda tipi açıkça yazdık.

Tür çıkarımı şaşırtabilir. `EnBuyuk(3, 7.5)` derlenir: `int` örtük olarak `double`'a dönüşebildiği için derleyici `T`'yi `double` seçer ve sonuç `7,5` olur. `EnBuyuk(3, "7")` ise derlenmez (`CS0411`), çünkü `int` ile `string` arasında ikisini de kapsayan bir tip yok.

---

## 4. Kısıtlar: Derleyiciye Verilen Söz

`EnBuyuk` metodundan `where` satırını silerseniz `CompareTo` çağrısı derlenmez; `>` işleci de derlenmez:

```
error CS0019: '>' işleci 'T' ve 'T' türündeki işlenenlere uygulanamaz
```

Sebep basit: `T` bir `int` olabilir, ama bir `Kitap` da olabilir. Derleyici `T` hakkında hiçbir şey bilmediği sürece yalnızca **her tipte** var olan şeylere izin verir (`ToString`, `Equals`, atama). **Kısıt**, `T` hakkında bir söz verir ve karşılığında o sözün kapsadığı üyelere izin alırsınız.

| Kısıt | Söz | Kazanılan |
| ----- | --- | --------- |
| `where T : IComparable<T>` | `T` karşılaştırılabilir | `CompareTo` |
| `where T : class` | `T` bir referans tipi | `T?` gerçekten `null` olabilir |
| `where T : struct` | `T` bir değer tipi | `T?` → `Nullable<T>` |
| `where T : new()` | Parametresiz kurucusu var | `new T()` |
| `where T : notnull` | `null` olamaz | Sözlük anahtarı gibi yerlerde güvence |
| `where T : Yayin` | `Yayin` veya ondan türeyen | `Yayin`'in üyeleri |

Birden çok kısıt virgülle yazılır: `where T : class, IKimlikli`.

### Söz verilir, çağıran tutar

Kısıt yalnızca metodu yazanı değil, çağıranı da bağlar. `IComparable<Kitap>` uygulamayan bir `Kitap` ile `EnBuyuk` çağırırsanız hata çağrı satırında gelir:

```
error CS0311: 'Kitap' türü, 'EnBuyuk<T>(T, T)' genel türü veya yöntemi için
'T' tür parametresi olarak kullanılamaz. 'Kitap' türünden
'System.IComparable<Kitap>' türüne örtük bir başvuru dönüştürmesi yoktur.
```

Hata çalışırken değil, derlerken gelir. `ArrayList`'in `InvalidCastException`'ı ile karşılaştırın.

---

## 5. Jenerik Arayüz: Bir Kez Yazılan Depo

Kütüphanede kitaplar, üyeler, ödünç kayıtları tutulur. Her biri için ayrı bir "ekle, bul, say" sınıfı yazmak yerine bir kez yazarız:

```csharp
interface IKimlikli
{
    string Kod { get; }
}

interface IDepo<T> where T : class, IKimlikli
{
    void Ekle(T oge);
    T? Bul(string kod);
    int Sayi { get; }
}

class BellekDepo<T> : IDepo<T> where T : class, IKimlikli
{
    private readonly Dictionary<string, T> _ogeler = new();

    public void Ekle(T oge) => _ogeler[oge.Kod] = oge;
    public T? Bul(string kod) => _ogeler.GetValueOrDefault(kod);
    public int Sayi => _ogeler.Count;
}

record Kitap(string Kod, string Baslik, string Yazar) : IKimlikli;
record Uye(string Kod, string AdSoyad) : IKimlikli;
```

```csharp
IDepo<Kitap> kitapDepo = new BellekDepo<Kitap>();
IDepo<Uye>   uyeDepo   = new BellekDepo<Uye>();
```

İki kısıt iki ayrı iş görür:

- **`IKimlikli`** — `oge.Kod` yazabilmek için. Kısıt olmasa derleyici `T`'nin bir `Kod`'u olduğunu bilemezdi.
- **`class`** — `Bul` bulamadığında `null` dönebilsin diye.

İkincisi incedir. `class` kısıtını silerseniz program yine derlenir ve çalışır, çünkü `Kitap` ve `Uye` zaten referans tipi. Ama `Kitap`'ı bir `record struct` yaparsanız, `kitapDepo.Bul("KTP-999") is null` satırı derlenmez:

```
error CS0037: Null yapılamayan bir değer türü olduğundan, null değeri 'Kitap'
türüne dönüştürülemiyor
```

Kısıtsız bir `T` için `T?`, `T` bir değer tipiyse **hiçbir şey değiştirmez**: `Bul` bulamadığında `null` değil, boş bir `Kitap` (`default`) döner. `class` kısıtı bu belirsizliği baştan kapatır: "bulunamadı" her zaman `null` demektir.

> Kodun geri kalanı `BellekDepo<T>`'yi değil `IDepo<T>`'yi kullanır. Yarın veriler bir dosyada ya da veritabanında tutulmak istenirse yalnızca yeni bir `DosyaDepo<T> : IDepo<T>` yazılır; onu kullanan kod değişmez. Tasarım derslerinden bildiğiniz "arayüze göre programla" ilkesinin jenerik hali budur. 9. haftada bağımlılık enjeksiyonuyla geri gelecek.

---

## 6. Her Kapalı Tip Ayrı Bir Tiptir

```csharp
static class Sayac<T>
{
    public static int Deger { get; private set; }
    public static void Artir() => Deger++;
}
```

```csharp
Sayac<int>.Artir();
Sayac<int>.Artir();
Sayac<string>.Artir();
Sayac<Kitap>.Artir();  Sayac<Kitap>.Artir();  Sayac<Kitap>.Artir();
```

```
int    : 2
string : 1
Kitap  : 3
double : 0
```

Statik alan **tipe** aittir ve `Sayac<int>` ile `Sayac<string>` iki ayrı tiptir. Hiç dokunulmamış `Sayac<double>` için bile ayrı bir alan vardır ve değeri `0`'dır. Bir jenerik sınıfa "tüm örnekler için ortak" sandığınız bir statik alan koyarsanız, aslında her `T` için ayrı bir alan koymuş olursunuz.

---

## 7. Varyans: Kitap Listesi Bir Yayın Listesi midir?

Bu bölümde kalıtımlı `record` kullanıyoruz:

```csharp
record Yayin(string Baslik);
record Kitap(string Baslik, string Yazar) : Yayin(Baslik);
record Dergi(string Baslik, int Sayi) : Yayin(Baslik);
```

Her `Kitap` bir `Yayin`'dır. Öyleyse bir kitap koleksiyonu, bir yayın koleksiyonu yerine kullanılabilir mi? Cevap koleksiyona göre değişir.

### `IEnumerable<T>`: evet, güvenli

```csharp
List<Kitap> kitaplar = [ ... ];
IEnumerable<Yayin> yayinlar = kitaplar;   // derlenir
```

`IEnumerable<T>` yalnızca **okumaya** izin verir: elemanları sırayla verir, eleman almaz. Kitapları "yayın" diye okumakta hiçbir tehlike yok; her kitap gerçekten bir yayın. Bu yüzden `IEnumerable<T>` tanımında `T` için `out` yazar: `IEnumerable<out T>`. Buna **kovaryans** denir: `Kitap → Yayin` yönü, `IEnumerable<Kitap> → IEnumerable<Yayin>` olarak **aynı yönde** korunur.

### `List<T>`: hayır

```csharp
List<Yayin> liste = kitaplar;                   // CS0029
liste.Add(new Dergi("Kütüphane Bülteni", 12));  // izin verilseydi...
```

`List<T>` hem okur hem **yazar**. Eğer kitap listesini yayın listesi diye görmeye izin verilseydi, ikinci satır o kitap listesine bir dergi koyardı. Derleyici ilk satırı reddederek ikincisini imkânsız kılar.

### Diziler: evet, ama güvensiz

Diziler, jeneriklerden önceki bir tasarım kararı yüzünden kovaryanttır:

```csharp
Yayin[] raf = new Kitap[2];
raf[0] = new Kitap("Sinekli Bakkal", "Halide Edib Adıvar");   // tamam
raf[1] = new Dergi("Kütüphane Bülteni", 12);                  // ArrayTypeMismatchException
```

İkinci atama derlenir, çünkü `raf` bir `Yayin[]` ve dergi bir yayın. Ama dizinin gerçek tipi `Kitap[]`'tir ve çalışma zamanı her atamada bunu denetler. `List<T>`'nin derleme zamanında önlediği hatayı dizi ancak çalışırken yakalar.

### `IComparer<T>`: ok ters döner

![Varyansın yönü](assets/01-varyans-yonu.svg)

```csharp
class BaslikKarsilastirici : IComparer<Yayin>
{
    public int Compare(Yayin? x, Yayin? y) =>
        string.Compare(x?.Baslik, y?.Baslik, StringComparison.CurrentCulture);
}

IComparer<Yayin> baslikSirasi = new BaslikKarsilastirici();
kitaplar.Sort(baslikSirasi);    // Sort bir IComparer<Kitap> bekliyor
```

`IComparer<T>`, `T`'yi yalnızca **alır**: iki nesne verirsiniz, sayı döner. Yayınları karşılaştırabilen bir karşılaştırıcı, kitapları da karşılaştırabilir, çünkü her kitap bir yayındır. Bu yüzden tanımında `IComparer<in T>` yazar ve bir `IComparer<Yayin>`, `IComparer<Kitap>` yerine kullanılabilir. Buna **kontravaryans** denir: ok bu kez **ters yönde** döner.

| Anahtar kelime | `T` nasıl kullanılıyor | Yön | Örnek |
| -------------- | ---------------------- | --- | ----- |
| `out` | Yalnızca **çıkıyor** (dönüş değeri) | Kalıtımla aynı | `IEnumerable<out T>` |
| `in` | Yalnızca **giriyor** (parametre) | Kalıtımın tersi | `IComparer<in T>` |
| — | Hem giriyor hem çıkıyor | Varyans yok | `List<T>`, `IList<T>` |

Üç sınır:

- Varyans yalnızca **arayüzlerde** ve **delegelerde** tanımlanabilir; sınıflarda tanımlanamaz.
- Yalnızca **referans tiplerinde** çalışır. `IEnumerable<int>`, `IEnumerable<object>` yerine kullanılamaz: `int` kutulanmadan `object` olamaz.
- Kendi arayüzünüzde `out T` yazarsanız derleyici `T`'nin gerçekten yalnızca dönüş değerinde kullanıldığını denetler.

### Bonus: Türkçe sıralama

Karşılaştırıcı `StringComparison.CurrentCulture` kullanıyor ve dosyanın başında kültür Türkçe yapıldı. Sonuç Türkçe alfabeye uyar:

```
Çalıkuşu
İnce Memed
Orta Direk
Ölmez Otu
Sinekli Bakkal
```

`StringComparison.Ordinal` ile aynı liste `Orta Direk`, `Sinekli Bakkal`, `Çalıkuşu`, `Ölmez Otu`, `İnce Memed` sırasına girer: harfler alfabeye göre değil, karakter kodlarına göre sıralanır; Türkçe harflerin kodları Latin harflerinden büyüktür. 1. haftanın kuralı burada da geçerli: kullanıcıya gösterilen sıralama kullanıcının kültürüyle, makineler arası veri kültürden bağımsız (`Ordinal`) yapılır.

---

## 8. İyi Pratikler ve Sık Yapılan Hatalar

**İyi pratikler**

- `ArrayList`, `Hashtable` gibi jenerik olmayan koleksiyonları yeni kodda kullanmayın
- Kısıtı ihtiyaç kadar koyun: her kısıt kullanımı daraltır, ama eksik kısıt derleme hatası verir
- "Bulunamadı" anlamında `null` döndürecekseniz `where T : class` yazın
- Yalnızca okunan koleksiyonları parametre olarak alırken `List<T>` yerine `IEnumerable<T>` isteyin; kovaryans sayesinde daha çok çağıran kullanabilir
- Kendi arayüzünüzde `T` yalnızca dönüyorsa `out`, yalnızca giriyorsa `in` yazmayı düşünün

**Sık yapılan hatalar**

- Kısıtsız `T` üzerinde `>`, `+` veya bir metot çağırmaya çalışmak (`CS0019`)
- Jenerik sınıftaki statik alanın bütün tipler için ortak olduğunu sanmak
- `List<Kitap>`'ı `List<Yayin>` parametresine vermeye çalışmak; `IEnumerable<Yayin>` isteyin
- Dizi kovaryansına güvenip çalışma zamanında `ArrayTypeMismatchException` almak
- Kısıtsız `T` için `T?` yazıp değer tiplerinde `null` beklemek

---

## 9. Örnek Kodlar

`kod/` klasöründe; çalıştırma, .NET 9 yolu ve Denemeniz için soruları klasördeki `README.md` içinde.

| Dosya | Konu |
| ----- | ---- |
| `01-kutulama-maliyeti.cs` | `ArrayList` ve `List<int>`: bellek ve tip güvenliği |
| `02-jenerik-raf.cs` | `Raf<T>`, `default`, `typeof(T)` |
| `03-jenerik-metot.cs` | Jenerik metot, kısıt, tür çıkarımı |
| `04-jenerik-arayuz.cs` | `IDepo<T>` ve birden çok kısıt |
| `05-statik-alan.cs` | Her kapalı tipin kendi statik alanı |
| `06-kovaryans.cs` | `IEnumerable<out T>`, `List<T>`, dizi kovaryansı |
| `07-kontravaryans.cs` | `IComparer<in T>` ve Türkçe sıralama |
| `hatali/01-kisit-yok.cs` | Kasıtlı olarak derlenmez |
| `hatali/02-kisit-ihlali.cs` | Kasıtlı olarak derlenmez |

---

## 10. İsteğe Bağlı Ev Uygulaması

1. haftanın `Kutuphane` çözümünün çekirdek projesine `IKimlikli`, `IDepo<T>` ve `BellekDepo<T>` tiplerini ekleyin. `Kitap`'ı `IKimlikli` uygulayan konumsal bir `record` yapın.

Konsol projesinde bir `IDepo<Kitap>` oluşturup üç kitap ekleyin. Sonra kitapları başlığa göre Türkçe sıralı yazdırın: depodan bir `IEnumerable<Kitap>` almanız gerekecek. `IDepo<T>` arayüzüne `IEnumerable<T> Hepsi { get; }` gibi bir üye eklemek bu arayüzün varyansını değiştirir mi? `IDepo<out T>` yazabilir misiniz? Neden?

---

## Gelecek Hafta

**Delegeler, lambda ifadeleri ve olaylar.** `EnBuyuk` metodunu düşünün: karşılaştırma kuralı `IComparable<T>` ile tipin içine gömülü. Kitapları bir gün başlığa, bir gün yıla göre karşılaştırmak isteseydik? Kuralı bir **parametre** olarak vermenin yolu delegelerdir. Yazılım tasarımından bildiğiniz Strateji ve Gözlemci kalıplarının C#'taki tek satırlık karşılıkları.

---

## Kaynaklar

- Microsoft Learn — Jenerik tipler: <https://learn.microsoft.com/dotnet/csharp/fundamentals/types/generics>
- Microsoft Learn — Tip parametrelerindeki kısıtlar: <https://learn.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters>
- Microsoft Learn — Jeneriklerde kovaryans ve kontravaryans: <https://learn.microsoft.com/dotnet/standard/generics/covariance-and-contravariance>
- Microsoft Learn — .NET'te jenerikler: <https://learn.microsoft.com/dotnet/standard/generics/>

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
