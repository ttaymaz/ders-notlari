// SORUN: Üç sınıf, aynı kod üç kez.
//
// Bir kütüphanede kitap, dergi ve DVD var. Üçü de demirbaştır: numarası,
// rafı ve ödünç durumu vardır; ödünç verilir, iade alınır.
//
// Bu dosya o üç sınıfı KALITIMSIZ yazıyor. Tekrarı gözle sayın.
//
// Çalıştırmak için:  dotnet run 01-tekrar-sorunu.cs

Kitap k = new Kitap(101, "Tutunamayanlar", "A-12", "Oğuz Atay");
Dergi d = new Dergi(102, "Bilim ve Teknik", "B-03", 745);
DVD v = new DVD(103, "Kış Uykusu", "C-07", 196);

k.OduncVer();
d.OduncVer();
d.OduncVer();

Console.WriteLine();
k.BilgiYazdir();
d.BilgiYazdir();
v.BilgiYazdir();


class Kitap
{
    // --- ORTAK KISIM (birinci kopya) ---
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public string Raf { get; private set; }
    public bool OduncVerildi { get; private set; }

    // --- KİTABA ÖZGÜ ---
    public string Yazar { get; private set; }

    public Kitap(int no, string baslik, string raf, string yazar)
    {
        DemirbasNo = no;
        Baslik = baslik;
        Raf = raf;
        OduncVerildi = false;
        Yazar = yazar;
    }

    // --- ORTAK METOTLAR (birinci kopya) ---
    public void OduncVer()
    {
        if (OduncVerildi) { Console.WriteLine($"{Baslik}: zaten ödünçte."); return; }
        OduncVerildi = true;
        Console.WriteLine($"{Baslik}: ödünç verildi.");
    }

    public void IadeAl()
    {
        OduncVerildi = false;
    }

    public void BilgiYazdir()
    {
        string durum = OduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"#{DemirbasNo} {Baslik} / {Yazar} [{Raf}] — {durum}");
    }
}


class Dergi
{
    // --- ORTAK KISIM (ikinci kopya — birebir aynı) ---
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public string Raf { get; private set; }
    public bool OduncVerildi { get; private set; }

    // --- DERGİYE ÖZGÜ ---
    public int Sayi { get; private set; }

    public Dergi(int no, string baslik, string raf, int sayi)
    {
        DemirbasNo = no;
        Baslik = baslik;
        Raf = raf;
        OduncVerildi = false;
        Sayi = sayi;
    }

    // --- ORTAK METOTLAR (ikinci kopya — birebir aynı) ---
    public void OduncVer()
    {
        if (OduncVerildi) { Console.WriteLine($"{Baslik}: zaten ödünçte."); return; }
        OduncVerildi = true;
        Console.WriteLine($"{Baslik}: ödünç verildi.");
    }

    public void IadeAl()
    {
        OduncVerildi = false;
    }

    public void BilgiYazdir()
    {
        string durum = OduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"#{DemirbasNo} {Baslik} sayı {Sayi} [{Raf}] — {durum}");
    }
}


class DVD
{
    // --- ORTAK KISIM (üçüncü kopya) ---
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public string Raf { get; private set; }
    public bool OduncVerildi { get; private set; }

    // --- DVD'YE ÖZGÜ ---
    public int SureDakika { get; private set; }

    public DVD(int no, string baslik, string raf, int sure)
    {
        DemirbasNo = no;
        Baslik = baslik;
        Raf = raf;
        OduncVerildi = false;
        SureDakika = sure;
    }

    // --- ORTAK METOTLAR (üçüncü kopya) ---
    public void OduncVer()
    {
        if (OduncVerildi) { Console.WriteLine($"{Baslik}: zaten ödünçte."); return; }
        OduncVerildi = true;
        Console.WriteLine($"{Baslik}: ödünç verildi.");
    }

    public void IadeAl()
    {
        OduncVerildi = false;
    }

    public void BilgiYazdir()
    {
        string durum = OduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"#{DemirbasNo} {Baslik} ({SureDakika} dk) [{Raf}] — {durum}");
    }
}

// --- BU KODUN BEDELİ ---
//
// 1. OduncVer metodu ÜÇ KEZ yazıldı. Kuralı değiştirmeniz gerekirse
//    (örneğin "ceza borcu olan ödünç alamaz") üç yerde düzelteceksiniz.
//    Biri unutulacak ve o tür için kural çalışmayacak.
//
// 2. Yeni bir ortak alan (örneğin KayitTarihi) eklemek üç sınıfa
//    dokunmak demek.
//
// 3. Dördüncü bir tür eklemek (Harita, Tez...) ortak kodu bir kez daha
//    kopyalamak demek.
//
// Birinci haftada paralel dizilerde yaşadığımız sorunun aynısı, bir
// üst katmanda: AYNI BİLGİ BİRDEN FAZLA YERDE.
