// Aynı görünen iki atama, farklı sonuç.
//
// Birinci haftada "iki değişken aynı nesneyi gösterirse ne olur" sorusunu
// ertelemiştik. Bir diziyi metoda gönderip içeride değiştirdiğinizde
// dışarıdakinin de değişmesi de aynı sorunun başka bir yüzü.
// Hepsinin tek bir cevabı var.
//
// Şema: assets/01-kopya-mi-referans-mi.svg
// Çalıştırmak için:  dotnet run 01-atama-farki.cs

Console.WriteLine("=== DEĞER TİPİ (int) ===");
int a = 5;
int b = a;        // a'nın DEĞERİ kopyalandı
b = 10;
Console.WriteLine($"a = {a}   b = {b}");
Console.WriteLine("a değişmedi. b ayrı bir kutu.");

Console.WriteLine("\n=== REFERANS TİPİ (class) ===");
Kitap k1 = new Kitap("Tutunamayanlar");
Kitap k2 = k1;    // nesnenin ADRESİ kopyalandı
k2.Baslik = "Sefiller";
Console.WriteLine($"k1.Baslik = {k1.Baslik}");
Console.WriteLine($"k2.Baslik = {k2.Baslik}");
Console.WriteLine("k1 DE değişti. İkisi AYNI nesneyi gösteriyor.");

Console.WriteLine("\n=== Aynı nesne mi? ===");
Console.WriteLine($"ReferenceEquals(k1, k2) = {ReferenceEquals(k1, k2)}");

Kitap k3 = new Kitap("Sefiller");    // aynı başlık, AYRI nesne
Console.WriteLine($"ReferenceEquals(k1, k3) = {ReferenceEquals(k1, k3)}");

Console.WriteLine("\n=== struct: sınıf gibi görünür, int gibi davranır ===");
Nokta n1 = new Nokta(3, 4);
Nokta n2 = n1;    // DEĞER kopyalandı
n2.X = 99;
Console.WriteLine($"n1 = ({n1.X}, {n1.Y})");
Console.WriteLine($"n2 = ({n2.X}, {n2.Y})");
Console.WriteLine("n1 değişmedi — struct bir DEĞER tipidir.");

Console.WriteLine("\n=== Metoda gönderince? ===");
int sayi = 5;
DegeriDegistir(sayi);
Console.WriteLine($"int sonrası      : {sayi}   (değişmedi)");

Kitap kitap = new Kitap("Tutunamayanlar");
NesneyiDegistir(kitap);
Console.WriteLine($"class sonrası    : {kitap.Baslik}   (DEĞİŞTİ)");

Nokta nokta = new Nokta(1, 1);
YapiyiDegistir(nokta);
Console.WriteLine($"struct sonrası   : ({nokta.X}, {nokta.Y})   (değişmedi)");


void DegeriDegistir(int x)
{
    x = 999;
}

void NesneyiDegistir(Kitap k)
{
    // Nesnenin İÇİNİ değiştiriyoruz — dışarıdaki de etkilenir.
    k.Baslik = "Metot içinde değişti";
}

void YapiyiDegistir(Nokta n)
{
    n.X = 999;
}


class Kitap
{
    public string Baslik { get; set; }

    public Kitap(string baslik)
    {
        Baslik = baslik;
    }
}


// struct: class gibi yazılır ama DEĞER tipidir.
struct Nokta
{
    public int X { get; set; }
    public int Y { get; set; }

    public Nokta(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// --- TEK CÜMLELİK FARK ---
//
//   Değer tipi   : atamada DEĞER kopyalanır    → iki ayrı kutu
//   Referans tipi: atamada ADRES kopyalanır    → tek nesne, iki etiket
//
// --- HANGİSİ NE? ---
//
//   Değer tipi   : int, double, bool, char, decimal, struct, enum
//   Referans tipi: class, dizi, string, interface, delegate
//
// Dizinin referans tipi olması şunu açıklar: bir diziyi metoda gönderip
// içeriğini değiştirdiğinizde dışarıdaki dizi de değişir. Metoda giden
// şey dizinin kendisi değil, ADRESİDİR.
//
// --- DİKKAT: string ---
//
// string bir referans tipidir AMA değer tipi gibi davranır. Sebebi
// değişmez (immutable) olması: bir string'i "değiştirdiğinizde"
// aslında yeni bir string üretilir. Bu yüzden yan etki görmezsiniz.
