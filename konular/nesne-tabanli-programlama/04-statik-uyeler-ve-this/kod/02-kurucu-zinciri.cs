// Kurucu zinciri: bir kurucunun diğerini çağırması.
//
// İkinci haftada üç kurucu yazmış ve gövdelerinin birbirine benzediğini
// not etmiştik: "bu tekrarı this ile ortadan kaldıracağız" demiştik.
// Sıra geldi.
//
// Çalıştırmak için:  dotnet run 02-kurucu-zinciri.cs

Console.WriteLine("--- Tekrar eden kurucular (eski hâli) ---");
UrunEski e1 = new UrunEski("Klavye", 750m, 12);
UrunEski e2 = new UrunEski("Mouse", 320m);
UrunEski e3 = new UrunEski("Monitör");
e1.BilgiYazdir();
e2.BilgiYazdir();
e3.BilgiYazdir();

Console.WriteLine("\n--- Kurucu zinciriyle (yeni hâli) ---");
Urun u1 = new Urun("Klavye", 750m, 12);
Urun u2 = new Urun("Mouse", 320m);
Urun u3 = new Urun("Monitör");
u1.BilgiYazdir();
u2.BilgiYazdir();
u3.BilgiYazdir();

Console.WriteLine("\nÇıktılar aynı. Fark, sınıfın içindeki tekrarda.");


// --- ÖNCE: her kurucu işini baştan yapıyor ---
class UrunEski
{
    private string ad;
    private decimal fiyat;
    private int stokAdedi;

    public UrunEski(string ad, decimal fiyat, int stok)
    {
        this.ad = ad;
        this.fiyat = fiyat;
        stokAdedi = stok;
    }

    public UrunEski(string ad, decimal fiyat)
    {
        this.ad = ad;          // tekrar
        this.fiyat = fiyat;    // tekrar
        stokAdedi = 0;
    }

    public UrunEski(string ad)
    {
        this.ad = ad;          // yine tekrar
        fiyat = 0m;
        stokAdedi = 0;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{ad,-10} {fiyat,10:C}  stok: {stokAdedi}");
    }
}


// --- SONRA: tek bir kurucu asıl işi yapıyor, diğerleri ona yönlendiriyor ---
class Urun
{
    private string ad;
    private decimal fiyat;
    private int stokAdedi;

    // ASIL KURUCU — atamalar yalnızca burada
    public Urun(string ad, decimal fiyat, int stok)
    {
        Console.Write("  [asıl kurucu] ");
        this.ad = ad;
        this.fiyat = fiyat;
        stokAdedi = stok;
    }

    // ": this(...)" → önce o kurucuyu çalıştır, sonra bu gövdeye dön
    public Urun(string ad, decimal fiyat) : this(ad, fiyat, 0)
    {
        Console.Write("  [ad, fiyat] → ");
    }

    public Urun(string ad) : this(ad, 0m, 0)
    {
        Console.Write("  [ad] → ");
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{ad,-10} {fiyat,10:C}  stok: {stokAdedi}");
    }
}

// --- ÇALIŞMA SIRASI ---
//
// new Urun("Mouse", 320m) çağrıldığında:
//   1. ": this(ad, fiyat, 0)" önce çalışır  → asıl kurucu alanları doldurur
//   2. sonra iki parametreli kurucunun kendi gövdesi çalışır
//
// Çıktıdaki sıra bunu gösteriyor: önce [asıl kurucu], sonra [ad, fiyat].
//
// --- NEDEN ÖNEMLİ? ---
//
// Alanlara atama TEK YERDE yapılıyor. Yarın bir alan eklerseniz veya
// kurulum kuralı değişirse, yalnızca asıl kurucuyu düzeltirsiniz.
// Eski hâlde üç kurucuyu da düzeltmeniz gerekirdi — ve biri unutulurdu.
