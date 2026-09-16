// BÖLÜM A PROVASI — çıktı tahmini.
//
// Sınavın çoktan seçmeli bölümü tam olarak bu beceriyi ölçer: kodu
// okuyup ne yazacağını söyleyebilmek.
//
// NASIL ÇALIŞILIR:
//   1. Aşağıdaki her bölümü OKUYUN ve çıktıyı KAĞIDA yazın.
//   2. Sonra programı çalıştırın.
//   3. Tutmayan yeri bulun ve NEDEN tutmadığını açıklayın.
//
// Çalıştırmadan önce tahmin etmezseniz bu alıştırma işe yaramaz.
//
// Çalıştırmak için:  dotnet run 01-cikti-tahmini.cs

Console.WriteLine("=== 1 === (alanların varsayılan değerleri)");
Urun u1 = new Urun("Klavye");
u1.BilgiYazdir();

Console.WriteLine("\n=== 2 === (kurucu aşırı yüklemesi)");
Urun u2 = new Urun("Mouse", 320m);
u2.BilgiYazdir();

Console.WriteLine("\n=== 3 === (özellik kuralı)");
u2.StokAdedi = 50;
Console.WriteLine($"stok: {u2.StokAdedi}");
u2.StokAdedi = -10;
Console.WriteLine($"stok: {u2.StokAdedi}");

Console.WriteLine("\n=== 4 === (statik alan: kaç nesne üretildi?)");
Console.WriteLine($"toplam: {Urun.UretilenSayisi}");

Console.WriteLine("\n=== 5 === (nesneler bağımsız mı?)");
Urun u3 = new Urun("Monitör", 4200m);
u3.StokAdedi = 7;
Console.WriteLine($"u2 stok: {u2.StokAdedi}   u3 stok: {u3.StokAdedi}");

Console.WriteLine("\n=== 6 === (kalıtım: devralınan metot)");
Kitap k = new Kitap(101, "Tutunamayanlar", "Oğuz Atay");
k.OduncVer();
k.OduncVer();

Console.WriteLine("\n=== 7 === (ezme: hangi versiyon çalışır?)");
k.BilgiYazdir();

Console.WriteLine("\n=== 8 === (ezmeyen türetilmiş sınıf)");
Harita h = new Harita(102, "Türkiye Fiziki");
h.BilgiYazdir();

Console.WriteLine("\n=== 9 === (temel sınıf tipinde değişken)");
Demirbas d = k;
d.BilgiYazdir();

Console.WriteLine("\n=== 10 === (ToString ezildi mi?)");
Console.WriteLine(k);
Console.WriteLine(h);


class Urun
{
    private int stokAdedi;

    public static int UretilenSayisi { get; private set; }

    public string Ad { get; private set; }
    public decimal Fiyat { get; private set; }

    public int StokAdedi
    {
        get { return stokAdedi; }
        set
        {
            if (value < 0) { return; }
            stokAdedi = value;
        }
    }

    public Urun(string ad, decimal fiyat)
    {
        Ad = ad;
        Fiyat = fiyat;
        UretilenSayisi = UretilenSayisi + 1;
    }

    public Urun(string ad) : this(ad, 0m)
    {
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{Ad} — {Fiyat:C} — stok {stokAdedi}");
    }
}


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }

    protected bool oduncVerildi = false;

    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }

    public void OduncVer()
    {
        if (oduncVerildi)
        {
            Console.WriteLine($"{Baslik}: zaten ödünçte.");
            return;
        }

        oduncVerildi = true;
        Console.WriteLine($"{Baslik}: ödünç verildi.");
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
        base.BilgiYazdir();
        Console.WriteLine($"   yazar: {Yazar}");
    }

    public override string ToString()
    {
        return $"{Baslik} / {Yazar}";
    }
}


class Harita : Demirbas
{
    public Harita(int no, string baslik) : base(no, baslik)
    {
    }
}

// --- KENDİNİZİ DENETLEYİN ---
//
// Tutmayan her bölüm için şu soruyu cevaplayın:
//
//   1  → alan değeri verilmezse ne olur?
//   2  → hangi kurucu çalıştı, diğerini çağırdı mı?
//   3  → -10 neden atanmadı, hangi blok engelledi?
//   4  → sayaç neden nesne üzerinden değil sınıf üzerinden okundu?
//   5  → bir nesnenin stoğunu değiştirmek diğerini etkiledi mi, neden?
//   6  → ikinci OduncVer neden farklı yazdı?
//   7  → BilgiYazdir kaç satır yazdı, hangi metotlar çalıştı?
//   8  → Harita neden yazarsız yazdı?
//   9  → d değişkeni Demirbas tipinde; neden Kitap'ın versiyonu çalıştı?
//   10 → Harita'nın çıktısı neden farklı görünüyor?
