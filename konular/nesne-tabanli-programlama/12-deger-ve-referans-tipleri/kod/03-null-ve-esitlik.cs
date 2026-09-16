// null ve eşitlik: referans tipine özgü iki konu.
//
// Birinci haftada "Object reference not set to an instance of an object"
// hatasını görmüş ve kökenini bu haftaya bırakmıştık.
//
// Çalıştırmak için:  dotnet run 03-null-ve-esitlik.cs

Console.WriteLine("=== null nedir? ===");
Kitap? bos = null;      // "hiçbir nesneyi göstermiyor"
Console.WriteLine($"bos == null : {bos == null}");

Console.WriteLine("\n=== null üzerinden üye erişimi ===");
try
{
    Console.WriteLine(bos.Baslik);
}
catch (NullReferenceException)
{
    Console.WriteLine("  NullReferenceException");
    Console.WriteLine("  Object reference not set to an instance of an object.");
    Console.WriteLine("  Çeviri: elinizde nesne yok, boş bir referans var.");
}

Console.WriteLine("\n=== Değer tipi null olamaz ===");
// Aşağıdaki satır DERLENMEZ:
// int sayi = null;
//
// Çünkü int bir kutudur ve kutu boş olamaz; içinde bir sayı vardır.
// Referans tipi ise bir ETİKETTİR ve hiçbir yeri göstermeyebilir.
Console.WriteLine("int sayi = null;  →  derleme hatası");

Console.WriteLine("\n=== Ama nullable yapılabilir ===");
int? belkiSayi = null;      // soru işareti: "boş olabilir"
Console.WriteLine($"belkiSayi.HasValue : {belkiSayi.HasValue}");

belkiSayi = 42;
Console.WriteLine($"belkiSayi.HasValue : {belkiSayi.HasValue}   değer: {belkiSayi.Value}");

Console.WriteLine("\n=== Güvenli erişim ===");
Kitap? belki = null;
Console.WriteLine($"?. ile           : {belki?.Baslik ?? "(kitap yok)"}");

Console.WriteLine("\n=== Eşitlik: değer tipi ===");
int a = 5;
int b = 5;
Console.WriteLine($"5 == 5           : {a == b}   (değerler karşılaştırılır)");

Nokta n1 = new Nokta(3, 4);
Nokta n2 = new Nokta(3, 4);
Console.WriteLine($"Nokta.Equals     : {n1.Equals(n2)}   (alanlar karşılaştırılır)");

Console.WriteLine("\n=== Eşitlik: referans tipi ===");
Kitap k1 = new Kitap("Tutunamayanlar");
Kitap k2 = new Kitap("Tutunamayanlar");
Kitap k3 = k1;

Console.WriteLine($"k1 == k2         : {k1 == k2}   (AYRI nesneler — başlık aynı olsa da)");
Console.WriteLine($"k1 == k3         : {k1 == k3}   (aynı nesne)");

Console.WriteLine("\n=== string neden farklı davranıyor? ===");
string s1 = "merhaba";
string s2 = "merhaba";
Console.WriteLine($"s1 == s2         : {s1 == s2}   (string == DEĞER karşılaştırır)");
Console.WriteLine("string referans tipidir ama == operatörü onun için özelleştirilmiştir.");


class Kitap
{
    public string Baslik { get; set; }

    public Kitap(string baslik)
    {
        Baslik = baslik;
    }
}

struct Nokta
{
    public int X { get; }
    public int Y { get; }

    public Nokta(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// --- null NEDEN YALNIZCA REFERANS TİPİNDE VAR? ---
//
// Değer tipi bir KUTUDUR: içinde bir değer vardır, boş olamaz.
// Referans tipi bir ETİKETTİR: bir nesneyi gösterir ya da hiçbir şeyi.
//
// Birinci haftadaki dizi örneği bunun sonucuydu:
//
//     Ogrenci[] sinif = new Ogrenci[3];   // üç BOŞ etiket
//     sinif[0].Ad = "Ayşe";               // boş etiketten üye istemek
//
// Diziyi açmak etiketleri üretir, nesneleri değil. Her hücre için
// ayrıca new gerekir.
//
// --- EŞİTLİK ÖZETİ ---
//
//   Değer tipi     : == değerleri karşılaştırır
//   Referans tipi  : == AYNI NESNE mi diye bakar
//   string         : referans tipi ama == değeri karşılaştırır (özel durum)
//
// Kendi sınıfınız için "aynı içerik = eşit" davranışı istiyorsanız
// Equals metodunu ezmeniz gerekir. Bu, dönem kapsamı dışında ama
// bahar projesinde karşınıza çıkacak.
//
// --- DENEYİN ---
//
// k1.Baslik ile k2.Baslik'i karşılaştırın: eşit çıkıyor mu?
// Peki k1 == k2 neden false? İkisi arasındaki farkı bir cümleyle yazın.
