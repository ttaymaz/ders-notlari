// ÇÖZÜM: abstract sınıf ve abstract metot.
//
// 01-anlamsiz-nesne.cs ile yan yana açın: aynı program, üç sorun çözülmüş.
//
// Şema: assets/01-soyut-vs-somut.svg
// Çalıştırmak için:  dotnet run 02-soyut-sinif.cs

Demirbas[] koleksiyon =
{
    new Kitap(101, "Tutunamayanlar", "Oğuz Atay"),
    new Dergi(102, "Bilim ve Teknik", 745),
    new DVD(103, "Kış Uykusu", 196),
};

Console.WriteLine("--- Polimorfizm aynen çalışıyor ---");
foreach (Demirbas d in koleksiyon)
{
    d.BilgiYazdir();
}

Console.WriteLine("\n--- Ödünç süreleri: artık hiçbiri uydurma değil ---");
foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine($"  {d.Baslik,-24} {d.OduncSuresi()} gün");
}

Console.WriteLine("\n--- Soyut sınıftan nesne üretilemez ---");
// Aşağıdaki satırın yorumunu kaldırın: DERLENMEZ.
// Demirbas d = new Demirbas(999, "Belirsiz");
//
// Hata: Cannot create an instance of the abstract type or interface 'Demirbas'
Console.WriteLine("new Demirbas(...)  →  derleme hatası");

Console.WriteLine("\n--- Soyut sınıf yine de ORTAK KOD taşıyabilir ---");
koleksiyon[0].OduncVer(20);
Console.WriteLine($"Ceza: {koleksiyon[0].CezaHesapla():C}");


// abstract: bu sınıftan NESNE ÜRETİLEMEZ.
// Yalnızca türetilmek için vardır.
abstract class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public bool OduncVerildi { get; private set; }

    private int gecenGun;

    // Soyut sınıfın da kurucusu olur — türetilmiş sınıflar base ile çağırır.
    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }

    // SOYUT METOT: gövdesi YOK, noktalı virgülle biter.
    // Her türetilmiş sınıf bunu yazmak ZORUNDADIR.
    public abstract int OduncSuresi();

    public abstract decimal GunlukCeza();

    public abstract void BilgiYazdir();

    // Soyut sınıf SOMUT üyeler de taşıyabilir — hatta taşımalıdır.
    // Ortak olan her şey burada, bir kez.
    public void OduncVer(int gunOnce)
    {
        if (OduncVerildi)
        {
            Console.WriteLine($"{Baslik}: zaten ödünçte.");
            return;
        }

        OduncVerildi = true;
        gecenGun = gunOnce;
        Console.WriteLine($"{Baslik}: {gunOnce} gün önce ödünç verildi.");
    }

    // Bu hesap her tür için aynı; soyut metotları çağırarak çalışıyor.
    public decimal CezaHesapla()
    {
        int gecikme = gecenGun - OduncSuresi();
        if (gecikme <= 0) { return 0m; }
        return gecikme * GunlukCeza();
    }
}


class Kitap : Demirbas
{
    public string Yazar { get; private set; }

    public Kitap(int no, string baslik, string yazar) : base(no, baslik)
    {
        Yazar = yazar;
    }

    // Üç soyut metodun ÜÇÜNÜ de yazmak zorundayız.
    public override int OduncSuresi() { return 14; }

    public override decimal GunlukCeza() { return 1m; }

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

    public override int OduncSuresi() { return 7; }

    public override decimal GunlukCeza() { return 0.5m; }

    public override void BilgiYazdir()
    {
        Console.WriteLine($"#{DemirbasNo} {Baslik} sayı {Sayi}");
    }
}


class DVD : Demirbas
{
    public int SureDakika { get; private set; }

    public DVD(int no, string baslik, int sure) : base(no, baslik)
    {
        SureDakika = sure;
    }

    public override int OduncSuresi() { return 3; }

    public override decimal GunlukCeza() { return 5m; }

    public override void BilgiYazdir()
    {
        Console.WriteLine($"#{DemirbasNo} {Baslik} ({SureDakika} dk)");
    }
}

// --- ÜÇ SORUN DA ÇÖZÜLDÜ ---
//
// 1. Anlamsız nesne üretilemiyor  → abstract class
// 2. Uydurma varsayılan yok        → abstract metodun gövdesi yok
// 3. Ezmeyi unutmak sessiz değil   → yazmayan sınıf DERLENMEZ
//
// Üçüncüsü en değerlisi: hata çalışma zamanından derleme zamanına taşındı.
// Bu, ikinci haftadan beri tekrarladığımız ilkenin bir uygulaması daha.
//
// --- DENEYİN ---
//
// Yeni bir tür ekleyin ama OduncSuresi metodunu YAZMAYIN:
//
//     class Harita : Demirbas
//     {
//         public Harita(int no, string b) : base(no, b) { }
//     }
//
// Derleyici ne diyor? Mesajı okuyun — size tam olarak neyi unuttuğunuzu
// söylüyor.
