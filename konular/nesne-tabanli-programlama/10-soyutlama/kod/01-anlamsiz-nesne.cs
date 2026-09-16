// SORUN: Temel sınıftan nesne üretmenin anlamı var mı?
//
// Geçen hafta polimorfizmi kurduk ama bir şeyi fark etmedik:
// Demirbas sınıfının kendisinden de nesne üretilebiliyor.
//
// Çalıştırmak için:  dotnet run 01-anlamsiz-nesne.cs

Demirbas[] koleksiyon =
{
    new Kitap(101, "Tutunamayanlar", "Oğuz Atay"),
    new Dergi(102, "Bilim ve Teknik", 745),

    // Bu satır çalışıyor. Ama ne ürettik?
    // "Kitap olmayan, dergi olmayan, sadece demirbaş olan şey" nedir?
    new Demirbas(999, "Belirsiz"),
};

Console.WriteLine("--- Koleksiyon ---");
foreach (Demirbas d in koleksiyon)
{
    d.BilgiYazdir();
}

Console.WriteLine("\n--- Ödünç süreleri ---");
foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine($"  {d.Baslik,-24} {d.OduncSuresi()} gün");
}

Console.WriteLine();
Console.WriteLine("Üçüncü satıra bakın: 14 gün nereden geldi?");
Console.WriteLine("Temel sınıftaki uydurma varsayılandan. Kimse bu sayıya karar vermedi.");


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
        // Bu gövdeye ne yazmalıyız? Her tür kendi biçiminde yazıyor.
        // Buradaki hâli yalnızca "hiçbir tür değilse" çalışır — ki o da
        // olmaması gereken bir durum.
        Console.WriteLine($"#{DemirbasNo} {Baslik}");
    }

    public virtual int OduncSuresi()
    {
        // 14 sayısı nereden geliyor? Kitaptan. Yani temel sınıf,
        // alt türlerinden birinin değerini varsayılan diye taşıyor.
        // Bu bir tasarım kokusudur.
        return 14;
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

    public override int OduncSuresi()
    {
        return 14;
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

    public override int OduncSuresi()
    {
        return 7;
    }
}

// --- ÜÇ SORUN ---
//
// 1. ANLAMSIZ NESNE ÜRETİLEBİLİYOR.
//    new Demirbas(999, "Belirsiz") gerçek dünyada bir karşılığı olmayan
//    bir şey üretti ve koleksiyona girdi.
//
// 2. TEMEL SINIFTAKİ GÖVDE UYDURMA.
//    OduncSuresi metodundaki 14 sayısı kitaptan kopyalandı. Temel sınıf,
//    alt türlerinden birini kayırıyor.
//
// 3. EZMEYİ UNUTMAK SESSİZ HATA ÜRETİYOR.
//    Yeni bir tür yazıp OduncSuresi ezmeyi unutursanız, derleyici
//    uyarmaz; türünüz sessizce 14 gün kullanır.
//
// Üçünün de tek bir çözümü var: SOYUT SINIF.
