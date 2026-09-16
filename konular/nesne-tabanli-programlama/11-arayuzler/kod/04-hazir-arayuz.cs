// Hazır arayüzler: IComparable
//
// Arayüzleri yalnızca siz yazmazsınız. .NET'in kendi arayüzlerini
// uygulayarak hazır altyapıya bağlanırsınız.
//
// Çalıştırmak için:  dotnet run 04-hazir-arayuz.cs

Kitap[] raf =
{
    new Kitap("Tutunamayanlar", 724),
    new Kitap("Kürk Mantolu Madonna", 160),
    new Kitap("Saatleri Ayarlama Enstitüsü", 384),
    new Kitap("Sefiller", 1232),
};

Console.WriteLine("--- Sıralamadan önce ---");
foreach (Kitap k in raf) { Console.WriteLine($"  {k}"); }

// Array.Sort nasıl sıralayacağını BİLMİYOR. Kitap sınıfının
// IComparable sözleşmesini uygulamasına güveniyor.
Array.Sort(raf);

Console.WriteLine("\n--- Sayfa sayısına göre sıralandı ---");
foreach (Kitap k in raf) { Console.WriteLine($"  {k}"); }

Console.WriteLine("\n--- Sözleşme uygulanmazsa ne olur? ---");
Dergi[] dergiler =
{
    new Dergi("Bilim ve Teknik", 745),
    new Dergi("Arkitekt", 512),
};

try
{
    Array.Sort(dergiler);
}
catch (InvalidOperationException)
{
    Console.WriteLine("  InvalidOperationException: Dergi IComparable uygulamıyor");
    Console.WriteLine("  Array.Sort neye göre sıralayacağını bilemiyor.");
}


// IComparable<T>: .NET'in sıralama sözleşmesi.
// Tek bir metot ister: CompareTo
class Kitap : IComparable<Kitap>
{
    public string Baslik { get; private set; }
    public int SayfaSayisi { get; private set; }

    public Kitap(string baslik, int sayfaSayisi)
    {
        Baslik = baslik;
        SayfaSayisi = sayfaSayisi;
    }

    // Sözleşmenin istediği metot.
    // Dönüş: negatif = bu nesne önce, 0 = eşit, pozitif = diğeri önce
    public int CompareTo(Kitap? digeri)
    {
        if (digeri == null) { return 1; }

        // Sayfa sayısına göre karşılaştırıyoruz.
        return SayfaSayisi.CompareTo(digeri.SayfaSayisi);
    }

    public override string ToString()
    {
        return $"{Baslik,-30} {SayfaSayisi,5} sayfa";
    }
}


// IComparable uygulamıyor — sıralanamaz.
class Dergi
{
    public string Baslik { get; private set; }
    public int Sayi { get; private set; }

    public Dergi(string baslik, int sayi)
    {
        Baslik = baslik;
        Sayi = sayi;
    }
}

// --- BURADA NE OLDU? ---
//
// Array.Sort metodunu Microsoft yazdı ve sizin Kitap sınıfınızı
// tanımıyor. Buna rağmen onu sıralayabildi.
//
// Sebebi: Array.Sort, "sıralanabilir olmak" için bir SÖZLEŞME tanımlamış
// (IComparable) ve yalnızca o sözleşmeye güveniyor. Siz sözleşmeyi
// uygularsanız, kodunuz onun altyapısına bağlanıyor.
//
// Arayüzlerin asıl gücü budur: BİRBİRİNİ TANIMAYAN iki kodun ortak bir
// sözleşme üzerinden çalışabilmesi.
//
// --- BAHAR DÖNEMİ BAĞLANTISI ---
//
// Windows Forms'ta bir listeyi ekrana bağladığınızda, form sizin
// sınıfınızı tanımaz; belirli arayüzlere güvenir. Veritabanı katmanı da
// öyle. Baharda yazacağınız her sınıf, tanımadığı kodlarla arayüzler
// üzerinden konuşacak.
//
// --- DENEYİN ---
//
// CompareTo metodunu başlığa göre sıralayacak biçimde değiştirin:
//
//     return Baslik.CompareTo(digeri.Baslik);
//
// Array.Sort satırına dokunmadan sıralama ölçütü değişti mi?
