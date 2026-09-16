// Metot ezme: aynı metot, türe göre farklı davranış.
//
// Geçen hafta BilgiYazdir metodu temel sınıftaydı ve kitabın yazarını
// yazmıyordu. Bu dosya o eksiği kapatıyor.
//
// Çalıştırmak için:  dotnet run 01-ezme-temel.cs

Kitap k = new Kitap(101, "Tutunamayanlar", "A-12", "Oğuz Atay");
Dergi d = new Dergi(102, "Bilim ve Teknik", "B-03", 745);
Harita h = new Harita(103, "Türkiye Fiziki", "C-01");

Console.WriteLine("--- Her tür kendi biçiminde yazıyor ---");
k.BilgiYazdir();       // Kitap'ın versiyonu
d.BilgiYazdir();       // Dergi'nin versiyonu
h.BilgiYazdir();       // Harita ezmedi → Demirbas'ın versiyonu


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public string Raf { get; private set; }

    protected bool oduncVerildi = false;

    public Demirbas(int no, string baslik, string raf)
    {
        DemirbasNo = no;
        Baslik = baslik;
        Raf = raf;
    }

    // virtual: "bu metot türetilmiş sınıflarda EZİLEBİLİR"
    // virtual yazmazsanız ezme izni vermemiş olursunuz.
    public virtual void BilgiYazdir()
    {
        string durum = oduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"#{DemirbasNo} {Baslik} [{Raf}] — {durum}");
    }
}


class Kitap : Demirbas
{
    public string Yazar { get; private set; }

    public Kitap(int no, string baslik, string raf, string yazar)
        : base(no, baslik, raf)
    {
        Yazar = yazar;
    }

    // override: "temel sınıftaki virtual metodun yerine bu çalışsın"
    public override void BilgiYazdir()
    {
        string durum = oduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"#{DemirbasNo} {Baslik} / {Yazar} [{Raf}] — {durum}");
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

    public override void BilgiYazdir()
    {
        string durum = oduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"#{DemirbasNo} {Baslik} sayı {Sayi} [{Raf}] — {durum}");
    }
}


// Harita BilgiYazdir metodunu EZMİYOR.
// Ezmeyen sınıf temel sınıfın versiyonunu kullanmaya devam eder.
class Harita : Demirbas
{
    public Harita(int no, string baslik, string raf)
        : base(no, baslik, raf)
    {
    }
}

// --- İKİ ANAHTAR KELİME ---
//
//   virtual   temel sınıfta yazılır: "bu metot ezilebilir"
//   override  türetilmiş sınıfta yazılır: "ben bunu eziyorum"
//
// İkisi de gereklidir. virtual yoksa override yazamazsınız:
//     'Kitap.BilgiYazdir()': cannot override inherited member
//     'Demirbas.BilgiYazdir()' because it is not marked virtual
//
// --- DİKKAT: TEKRAR GERİ GELDİ ---
//
// Üç BilgiYazdir metoduna bakın: "durum" satırı üçünde de aynı.
// Ezmek, ortak kısmı SİLMEK anlamına gelmemeli. Bunun çözümü
// 02-base-cagrisi.cs dosyasında.
