# Diziler: Tek Boyutlu Diziler

Dönemin ilk yarısında bilgileri tekil kutularda saklamayı öğrendik: `int yas = 20;`, `string ad = "Ahmet";`

Şimdi size 100 öğrencinin notunu saklamanızı istesem ne yapardınız?

```csharp
int not1, not2, not3, not4, ... , not100;
```

Bu yöntem yalnızca yorucu değil, **yönetilemez**. Ortalamayı hesaplamak için 100 terimlik bir toplama yazmanız gerekir. Öğrenci sayısı 101 olduğunda kodun tamamını değiştirmeniz gerekir.

Programlamanın en temel veri yapılarından biri tam bu sorunu çözer: **diziler**.

---

## 1. Dizi Nedir?

Dizi, **aynı tipteki** birden fazla veriyi, **tek bir isim** altında, **sıralı** biçimde saklayan bir veri yapısıdır.

### Yumurta Kartonu Benzetmesi

Tek bir değişken (`int sayi;`) bir su bardağı gibidir — yalnızca bir değer alır.

Bir dizi (`int[] sayilar;`) altılı yumurta kartonu gibidir. Kartonun tek bir adı vardır, ama içinde numaralı gözlerde altı ayrı yumurta durur.

Üç şey aynı anda doğrudur:

- **Tek isim** — tüm veriye tek bir adla ulaşırsınız
- **Aynı tip** — bir `int` dizisine metin koyamazsınız, tıpkı yumurta kartonuna çorba koyamayacağınız gibi
- **Sıralı ve numaralı** — her gözün bir numarası vardır

---

## 2. Dizi Tanımlama ve Oluşturma

C#'ta dizi oluşturmak iki adımdır.

### Adım 1 — Tanımlama

Ne tür veri tutacağını ve adının ne olacağını söylersiniz. Köşeli parantez `[]` bunun bir dizi olduğunu belirtir.

```csharp
int[] sayilar;         // tam sayı tutacak bir dizi
string[] isimler;      // metin tutacak bir dizi
double[] ortalamalar;  // ondalıklı sayı tutacak bir dizi
```

Bu noktada henüz bellekte bir yer ayrılmadı. Yalnızca "böyle bir şey olacak" dediniz.

### Adım 2 — Oluşturma

`new` anahtar kelimesiyle diziyi bellekte oluşturur ve **kaç gözlü** olacağını söylersiniz.

```csharp
sayilar = new int[5];      // 5 gözlü
isimler = new string[10];  // 10 gözlü
```

### Tek Satırda

Genellikle iki adım birleştirilir:

```csharp
int[] notlar = new int[3];
string[] gunler = new string[7];
```

### Kısayol — Değerleri Baştan Vermek

Hangi değerlerin geleceğini biliyorsanız `new` yazmadan, süslü parantezle doğrudan verebilirsiniz:

```csharp
int[] numaralar = { 10, 20, 30 };
string[] isimler = { "Ali", "Veli", "Ayşe" };
```

Boyutu yazmanıza gerek yok — C# saymayı biliyor.

---

## 3. En Önemli Kavram: İndis

Dizi oluşturduğunuzda her göz otomatik olarak numaralandırılır. Bu numaraya **indis** (index) denir.

> ## İndisler her zaman **0'dan** başlar.

Bu C#'a özel değil; C, C++, Java, Python ve JavaScript'te de böyledir.

### Neden Sıfırdan?

Çünkü indis bir **sıra numarası değil, uzaklıktır**.

Dizi bellekte bir başlangıç adresi tutar. İndis, o başlangıçtan ne kadar uzakta olduğunuzu söyler:

- Birinci eleman başlangıçtan **0 birim** uzakta → `sayilar[0]`
- İkinci eleman başlangıçtan **1 birim** uzakta → `sayilar[1]`

![Dizinin bellekteki görünümü](assets/01-dizi-bellek.svg)

Bu sistem bellek erişimi açısından en verimli yoldur; bilgisayar adresi doğrudan hesaplar.

### Kuralın Sonuçları

`new int[5]` yazdığınızda indisler **0, 1, 2, 3, 4** olur.

| İfade | Anlamı |
| ----- | ------ |
| `dizi[0]` | Birinci eleman |
| `dizi[4]` | Beşinci ve son eleman |
| `dizi[5]` | **Yok** — program çöker |

> **Genel kural:** Son elemanın indisi her zaman **boyut − 1**'dir.

---

## 4. Kaç Elemanlı? `.Length`

Bir dizinin kaç gözü olduğunu `.Length` ile öğrenirsiniz:

```csharp
int[] notlar = new int[3];
Console.WriteLine(notlar.Length);        // 3
```

Son elemana ulaşmanın güvenli yolu budur:

```csharp
Console.WriteLine(notlar[notlar.Length - 1]);   // son eleman
```

Boyutu elle yazmak yerine `.Length` kullanmayı alışkanlık edinin. Dizinin boyutunu değiştirdiğinizde kodun geri kalanı kendiliğinden doğru kalır.

> **Dikkat:** `.Length` bir **özelliktir**, metot değil. Sonuna parantez konmaz.
> `notlar.Length` doğru, `notlar.Length()` hatalıdır.

Modern C#'ta bir kısayol daha vardır: `notlar[^1]` "sondan birinci" demektir. Derste klasik yazımı kullanacağız, ama gerçek kodlarda bununla karşılaşabilirsiniz.

---

## 5. Elemanlara Erişim ve Değer Atama

Köşeli parantez içine indis yazarak hem okur hem yazarsınız.

```csharp
int[] notlar = new int[3];

// Değer yazma
notlar[0] = 80;
notlar[1] = 95;
notlar[2] = 70;

// Değer okuma
Console.WriteLine($"Birinci öğrencinin notu: {notlar[0]}");   // 80
Console.WriteLine($"Son öğrencinin notu: {notlar[2]}");       // 70

// Değerlerle işlem
int toplam = notlar[0] + notlar[1] + notlar[2];
double ortalama = toplam / 3.0;
```

Son satırdaki `3.0`'a dikkat edin. `3` yazsaydınız tam sayı bölmesi olur, ondalık kısım atılırdı. Üçüncü haftanın tuzağı burada da geçerli.

---

## 6. Yeni Dizinin Gözlerinde Ne Var?

`new int[3]` yazdığınızda gözler **boş kalmaz**. C# her gözü, veri tipinin **varsayılan değeriyle** doldurur.

| Tip | Varsayılan değer |
| --- | ---------------- |
| `int`, `long` | `0` |
| `double`, `decimal` | `0` |
| `bool` | `false` |
| `char` | `'\0'` (boş karakter) |
| `string` | `null` |

`string` dizilerindeki `null` özellikle dikkat ister. `null`, "boş metin" değil, **"burada hiçbir şey yok"** demektir. Ekrana basıldığında boş görünür ama boş metinle aynı şey değildir.

> **Neden önemli?** 10 gözlü bir diziyi doldururken bir gözü atlarsanız orada `0` kalır. Ortalama hesabında bu, sonucu sessizce düşürür — klasik bir mantık hatası. Program çökmez, hata vermez, sadece yanlış sonuç verir.

---

## 7. Dizilerin Sınırı: Boyut Değişmez

Bir dizi oluşturulduktan sonra **boyutu değiştirilemez**.

```csharp
int[] notlar = new int[3];
// 4. öğrenci geldi... notlar'a bir göz ekleyemezsiniz.
```

Tek yol, daha büyük yeni bir dizi oluşturup elemanları kopyalamaktır. Bu, elle yapıldığında zahmetlidir.

Bahar döneminde tam bu sorunu çözen **dinamik listeler** ve **bağlı listeler** konusunu göreceksiniz. Bu haftaki dizi bilgisi, oranın doğrudan ön koşuludur.

---

## 8. İyi Pratikler ve Sık Yapılan Hatalar

**Sık yapılan hata — en yaygını: `IndexOutOfRangeException`.** "İndis dizinin sınırları dışındaydı" demektir.

```csharp
int[] sayilar = new int[3];   // indisler: 0, 1, 2
sayilar[3] = 100;             // ÇÖKER — [3] diye bir göz yok
```

Bu bir çalışma zamanı hatasıdır: kod derlenir, program çalışır ve o satıra geldiğinde durur. Derleyici sizi uyaramaz çünkü indis çalışma anında hesaplanabilir.

**Sık yapılan hata: Sıfırdan başlama kuralını unutmak.** 3 elemanlı dizinin son elemanına `[3]` ile erişmeye çalışmak, dizilerdeki bir numaralı başlangıç hatasıdır.

**Sık yapılan hata: Negatif indis.** `dizi[-1]` de aynı hatayı verir. C#'ta negatif indis yoktur; "sondan birinci" için `[^1]` yazılır.

**Sık yapılan hata: `.Length` sonrası parantez.** `dizi.Length()` derlenmez.

**İyi pratik: Çoğul isim verin.** `sayi` değil `sayilar`, `ogrenci` değil `ogrenciler`. Kodu okuyan kişi tek değer mi çok değer mi olduğunu isimden anlasın.

**İyi pratik: Boyut yerine `.Length` kullanın.** Dizinin boyutunu değiştirdiğinizde koda geri dönmeniz gerekmez.

---

## 9. Örnek Kodlar

Bu haftanın kodları [`kod/`](kod/) klasöründe:

| Dosya | Konu |
| ----- | ---- |
| [`01-dizi-olusturma.cs`](kod/01-dizi-olusturma.cs) | Üç tanımlama yolu, `.Length` |
| [`02-indis-erisimi.cs`](kod/02-indis-erisimi.cs) | Yazma, okuma, taşma hatası |
| [`03-varsayilan-degerler.cs`](kod/03-varsayilan-degerler.cs) | Yeni dizide ne var? |
| [`04-not-ortalamasi.cs`](kod/04-not-ortalamasi.cs) | Elemanlarla hesaplama |

---

## 10. İsteğe Bağlı Ev Uygulaması

**Problem:** 3 elemanlı `string` tipinde bir dizi oluşturun (örneğin `favoriYemekler`).

**Görev:** Her indise (`[0]`, `[1]`, `[2]`) bir favori yemeğinizi atayın ve üçünü de ekrana alt alta yazdırın. Bu hafta **döngü kullanmayın** — indisleri elle yazın.

**Amaç:** İndisle veri yazma ve okuma refleksi kazanmak.

**Düşünün:**

- Diziyi 3 elemanlı oluşturup yalnızca ilk ikisine değer atarsanız, üçüncüsünü yazdırdığınızda ne görürsünüz?
- Aynı programı 20 favori yemek için yazmanız gerekseydi kaç satır olurdu?

**Zorlayıcı ek:** `int[]` tipinde 5 elemanlı bir dizi oluşturun, içine beş sayı koyun ve **döngü kullanmadan** en büyüğünü bulun. Kaç karşılaştırma gerekiyor?

---

## Gelecek Hafta

Bu hafta dizilere elemanları tek tek koyduk ve tek tek okuduk. 3 eleman için bu çalışıyor. Peki 100 eleman olsaydı?

Gelecek hafta dizileri **döngülerle** birleştiriyoruz. Beşinci haftada öğrendiğimiz `for`, dizilerin gerçek gücünü açığa çıkaracak: kaç elemanlı olursa olsun aynı üç satır kod iş görecek.

Ayrıca `foreach` adında, özellikle diziler için tasarlanmış yeni bir döngüyle tanışacağız.

---

## Kaynaklar

- Microsoft. *Diziler (C# Programlama Kılavuzu).* https://learn.microsoft.com/tr-tr/dotnet/csharp/programming-guide/arrays/
- Microsoft. *Tek Boyutlu Diziler.* https://learn.microsoft.com/tr-tr/dotnet/csharp/language-reference/builtin-types/arrays

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi, Sinanpaşa MYO*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
