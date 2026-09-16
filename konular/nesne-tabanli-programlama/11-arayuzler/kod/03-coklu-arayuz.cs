// Bir sınıf, birden fazla sözleşme.
//
// Kalıtımda "tek sınıf" kuralı vardı. Arayüzde böyle bir sınır yok:
// bir sınıf istediği kadar arayüz uygulayabilir.
//
// Çalıştırmak için:  dotnet run 03-coklu-arayuz.cs

Demirbas[] koleksiyon =
{
    new BasiliKitap(101, "Tutunamayanlar"),
    new EKitap(102, "Sefiller", 4.2),
    new SesliKitap(103, "Kürk Mantolu Madonna", 8.7, 320),
};

Console.WriteLine("--- İndirilebilirler ---");
foreach (Demirbas d in koleksiyon)
{
    if (d is IIndirilebilir i) { i.Indir(); }
}

Console.WriteLine("\n--- Dinlenebilirler ---");
foreach (Demirbas d in koleksiyon)
{
    if (d is IDinlenebilir s) { s.Oynat(); }
}

Console.WriteLine("\n--- Hem indirilebilir hem dinlenebilir olanlar ---");
foreach (Demirbas d in koleksiyon)
{
    if (d is IIndirilebilir && d is IDinlenebilir)
    {
        Console.WriteLine($"  {d.Baslik}: çevrimdışı dinlenebilir");
    }
}


interface IIndirilebilir
{
    void Indir();
    double BoyutMB();
}

interface IDinlenebilir
{
    void Oynat();
    int SureDakika();
}


abstract class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }

    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }
}


class BasiliKitap : Demirbas
{
    public BasiliKitap(int no, string baslik) : base(no, baslik) { }
}


// Tek arayüz
class EKitap : Demirbas, IIndirilebilir
{
    private double boyut;

    public EKitap(int no, string baslik, double boyutMB) : base(no, baslik)
    {
        boyut = boyutMB;
    }

    public void Indir()
    {
        Console.WriteLine($"  {Baslik} indiriliyor ({boyut} MB)");
    }

    public double BoyutMB() { return boyut; }
}


// İKİ arayüz birden. Kalıtımda bu mümkün değildi.
class SesliKitap : Demirbas, IIndirilebilir, IDinlenebilir
{
    private double boyut;
    private int sure;

    public SesliKitap(int no, string baslik, double boyutMB, int sureDakika)
        : base(no, baslik)
    {
        boyut = boyutMB;
        sure = sureDakika;
    }

    public void Indir()
    {
        Console.WriteLine($"  {Baslik} indiriliyor ({boyut} MB)");
    }

    public double BoyutMB() { return boyut; }

    public void Oynat()
    {
        Console.WriteLine($"  {Baslik} oynatılıyor ({sure} dk)");
    }

    public int SureDakika() { return sure; }
}

// --- NEDEN BİRDEN FAZLA ARAYÜZ SERBEST, BİRDEN FAZLA SINIF DEĞİL? ---
//
// Buna "elmas problemi" denir. İki sınıftan türemek serbest olsaydı:
//
//        A  (Yazdir metodu var)
//       / \
//      B   C  (ikisi de Yazdir'ı farklı ezmiş)
//       \ /
//        D
//
// D sınıfında Yazdir çağrıldığında B'nin mi C'nin mi versiyonu
// çalışacak? Cevabı yok. Bu yüzden C# çoklu SINIF kalıtımını yasaklar.
//
// Arayüzlerde bu sorun yoktur çünkü arayüz GÖVDE TAŞIMAZ. İki arayüz
// aynı adlı metodu isterse, sınıf o metodu bir kez yazar ve ikisini de
// karşılar. Çakışacak bir gövde yok.
//
// --- DENEYİN ---
//
// IDinlenebilir arayüzüne "void Duraklat();" ekleyin. Hangi sınıf(lar)
// derlenmez oldu? Arayüze üye eklemenin bedeli budur: o arayüzü uygulayan
// HER sınıf güncellenmek zorundadır. Bu yüzden arayüzleri küçük tutun.
