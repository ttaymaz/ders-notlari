// ÇÖZÜM: Ortak olanı bir kez yaz, farklı olanı üzerine ekle.
//
// 01-tekrar-sorunu.cs ile yan yana açın: aynı program, üçte bir kod.
//
// Şema: assets/01-kalitim-hiyerarsisi.svg
// Çalıştırmak için:  dotnet run 02-kalitim-temel.cs

Kitap k = new Kitap(101, "Tutunamayanlar", "A-12", "Oğuz Atay");
Dergi d = new Dergi(102, "Bilim ve Teknik", "B-03", 745);
DVD v = new DVD(103, "Kış Uykusu", "C-07", 196);

// OduncVer metodu Demirbas sınıfında BİR KEZ yazıldı,
// üçü de kullanabiliyor.
k.OduncVer();
d.OduncVer();
d.OduncVer();

Console.WriteLine();
k.BilgiYazdir();
d.BilgiYazdir();
v.BilgiYazdir();

Console.WriteLine("\n--- Türetilmiş sınıf kendi eklerini de taşır ---");
Console.WriteLine($"Kitabın yazarı : {k.Yazar}");
Console.WriteLine($"Derginin sayısı: {d.Sayi}");
Console.WriteLine($"DVD süresi     : {v.SureDakika} dk");


// --- TEMEL SINIF (base class) ---
// Ortak olan her şey burada, bir kez.
class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public string Raf { get; private set; }

    // protected: private gibi dışarıya kapalı, AMA türetilmiş
    // sınıflar erişebilir. Üçüncü haftada söz verdiğimiz belirleyici.
    protected bool oduncVerildi = false;

    public bool OduncVerildi
    {
        get { return oduncVerildi; }
    }

    public Demirbas(int no, string baslik, string raf)
    {
        DemirbasNo = no;
        Baslik = baslik;
        Raf = raf;
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

    public void IadeAl()
    {
        oduncVerildi = false;
    }

    public void BilgiYazdir()
    {
        string durum = oduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"#{DemirbasNo} {Baslik} [{Raf}] — {durum}");
    }
}


// --- TÜRETİLMİŞ SINIFLAR (derived classes) ---
// ":" işareti "şundan türüyor" demektir.
class Kitap : Demirbas
{
    public string Yazar { get; private set; }

    // ": base(...)" temel sınıfın kurucusunu çağırır.
    public Kitap(int no, string baslik, string raf, string yazar)
        : base(no, baslik, raf)
    {
        Yazar = yazar;
    }
}

class Dergi : Demirbas
{
    public int Sayi { get; private set; }

    public Dergi(int no, string baslik, string raf, int sayi)
        : base(no, baslik, raf)
    {
        Sayi = sayi;
    }
}

class DVD : Demirbas
{
    public int SureDakika { get; private set; }

    public DVD(int no, string baslik, string raf, int sure)
        : base(no, baslik, raf)
    {
        SureDakika = sure;
    }
}

// --- NE KAZANDIK? ---
//
// OduncVer, IadeAl ve BilgiYazdir metotları TEK YERDE. Ödünç verme
// kuralı değişirse tek bir metodu düzeltiyorsunuz; üç tür de anında
// yeni kurala uyuyor.
//
// Dördüncü bir tür eklemek artık üç satır:
//
//     class Harita : Demirbas
//     {
//         public string Olcek { get; private set; }
//         public Harita(int no, string b, string r, string olcek)
//             : base(no, b, r) { Olcek = olcek; }
//     }
//
// --- DİKKAT: BilgiYazdir eksik kalıyor ---
//
// Çıktıya bakın: kitabın YAZARI, derginin SAYISI yazılmıyor. Çünkü
// BilgiYazdir temel sınıfta ve temel sınıf bunları bilmiyor.
//
// Bir metodu türetilmiş sınıfta ÖZELLEŞTİRMEK gelecek haftanın konusu.
