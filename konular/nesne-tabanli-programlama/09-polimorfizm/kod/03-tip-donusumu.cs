// Temel tip referansın sınırı ve tür dönüşümü: is, as, (Tur)
//
// Polimorfizm güçlü ama bir sınırı var: temel tip referans üzerinden
// yalnızca TEMEL SINIFIN üyelerine erişebilirsiniz.
//
// Çalıştırmak için:  dotnet run 03-tip-donusumu.cs

Demirbas d = new Kitap(101, "Tutunamayanlar", "Oğuz Atay");

Console.WriteLine("--- Temel sınıfın üyeleri: sorunsuz ---");
d.BilgiYazdir();
Console.WriteLine($"Başlık: {d.Baslik}");

// Aşağıdaki satırın yorumunu kaldırın: DERLENMEZ.
// Console.WriteLine(d.Yazar);
//
// Hata: 'Demirbas' does not contain a definition for 'Yazar'
//
// Sebebi: değişkenin tipi Demirbas. Derleyici o tipte ne varsa ona izin
// verir. Nesnenin gerçekte Kitap olması derleme anında bilinmez.
Console.WriteLine("d.Yazar  →  derleme hatası (değişken tipi Demirbas)");

Console.WriteLine("\n--- is: tür kontrolü ---");
if (d is Kitap)
{
    Console.WriteLine("d gerçekte bir Kitap");
}

Console.WriteLine("\n--- is ile desen eşleme (önerilen) ---");
if (d is Kitap k)
{
    // k değişkeni zaten Kitap tipinde; ayrıca dönüştürmeye gerek yok.
    Console.WriteLine($"Yazar: {k.Yazar}");
}

Console.WriteLine("\n--- as: dönüştür, olmazsa null ver ---");
Demirbas d2 = new Dergi(102, "Bilim ve Teknik", 745);
Kitap? denemeKitap = d2 as Kitap;

if (denemeKitap == null)
{
    Console.WriteLine("d2 bir Kitap değil, as null döndürdü (çökme yok)");
}

Console.WriteLine("\n--- (Tur) zorla dönüşüm: yanlışsa ÇÖKER ---");
try
{
    Kitap yanlis = (Kitap)d2;      // d2 aslında Dergi
    Console.WriteLine(yanlis.Yazar);
}
catch (InvalidCastException)
{
    Console.WriteLine("InvalidCastException: Dergi nesnesi Kitap'a çevrilemez");
}

Console.WriteLine("\n--- Listede yalnızca kitapların yazarlarını yazdırmak ---");
Demirbas[] liste =
{
    new Kitap(101, "Tutunamayanlar", "Oğuz Atay"),
    new Dergi(102, "Bilim ve Teknik", 745),
    new Kitap(103, "Kürk Mantolu Madonna", "Sabahattin Ali"),
};

foreach (Demirbas x in liste)
{
    if (x is Kitap kitap)
    {
        Console.WriteLine($"  {kitap.Baslik} — {kitap.Yazar}");
    }
}


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }

    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }

    public virtual void BilgiYazdir()
    {
        Console.WriteLine($"#{DemirbasNo} {Baslik}");
    }
}

class Kitap : Demirbas
{
    public string Yazar { get; private set; }

    public Kitap(int no, string baslik, string yazar) : base(no, baslik)
    {
        Yazar = yazar;
    }

    public override void BilgiYazdir()
    {
        Console.WriteLine($"#{DemirbasNo} {Baslik} / {Yazar}");
    }
}

class Dergi : Demirbas
{
    public int Sayi { get; private set; }

    public Dergi(int no, string baslik, int sayi) : base(no, baslik)
    {
        Sayi = sayi;
    }

    public override void BilgiYazdir()
    {
        Console.WriteLine($"#{DemirbasNo} {Baslik} sayı {Sayi}");
    }
}

// --- ÜÇ ARACIN KARŞILAŞTIRMASI ---
//
//   x is Kitap        → true/false döner, güvenli
//   x is Kitap k      → hem kontrol eder hem dönüştürür, en temiz yol
//   x as Kitap        → dönüştürür, olmazsa null (çökmez)
//   (Kitap)x          → dönüştürür, olmazsa ÇÖKER
//
// --- DİKKAT: BUNLAR POLİMORFİZMİN YERİNE GEÇMEZ ---
//
// Tür dönüşümüne ihtiyaç duyuyorsanız önce şunu sorun: bu davranış
// temel sınıfta virtual bir metot olabilir miydi? Genellikle cevap evet
// ve o zaman "is" kontrolüne hiç gerek kalmaz.
//
// Tür dönüşümü, yalnızca gerçekten yalnızca bir alt türe ait olan bir
// bilgiye erişmek gerektiğinde makuldür — yukarıdaki "kitapların
// yazarlarını listele" örneği gibi.
