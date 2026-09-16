// Polimorfizm iş başında: tek listeden tam bir rapor.
//
// Bu dosya bahar dönemindeki otomasyonun konsol hâlidir. Baharda aynı
// liste bir formdaki tabloya bağlanacak; döngü ve hesaplar aynı kalacak.
//
// Çalıştırmak için:  dotnet run 04-kutuphane-raporu.cs

Demirbas[] koleksiyon =
{
    new Kitap(101, "Tutunamayanlar", "Oğuz Atay", 724),
    new Kitap(102, "Kürk Mantolu Madonna", "Sabahattin Ali", 160),
    new Dergi(201, "Bilim ve Teknik", 745),
    new Dergi(202, "Arkitekt", 512),
    new DVD(301, "Kış Uykusu", 196),
};

// Birkaç işlem yapalım
koleksiyon[0].OduncVer(8);
koleksiyon[2].OduncVer(10);
koleksiyon[4].OduncVer(5);

Console.WriteLine("\n" + new string('=', 62));
Console.WriteLine("KÜTÜPHANE DURUM RAPORU");
Console.WriteLine(new string('=', 62));

Console.WriteLine($"\n{"Demirbaş",-34} {"Süre",5} {"Gecik.",7} {"Ceza",10}");
Console.WriteLine(new string('-', 62));

decimal toplamCeza = 0m;
int oduncteSayisi = 0;

foreach (Demirbas d in koleksiyon)
{
    // Tek döngü; her satırın içeriğini nesnenin kendi türü belirliyor.
    if (!d.OduncVerildi) { continue; }

    oduncteSayisi++;
    decimal ceza = d.CezaHesapla();
    toplamCeza = toplamCeza + ceza;

    Console.WriteLine($"{d,-34} {d.OduncSuresi(),5} {d.GecikenGun(),7} {ceza,10:C}");
}

Console.WriteLine(new string('-', 62));
Console.WriteLine($"Ödünçte: {oduncteSayisi} demirbaş · Toplam ceza: {toplamCeza:C}");

Console.WriteLine("\n--- Rafta olanlar ---");
foreach (Demirbas d in koleksiyon)
{
    if (!d.OduncVerildi)
    {
        Console.WriteLine($"  {d}");
    }
}


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public bool OduncVerildi { get; private set; }

    private int gecenGun;

    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }

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

    // Türe göre değişen iki üye — ezilmek üzere yazıldı
    public virtual int OduncSuresi()
    {
        return 14;
    }

    public virtual decimal GunlukCeza()
    {
        return 1m;
    }

    // Bu metot EZİLMEZ: hesabın kendisi her tür için aynı.
    // Değişen şey yalnızca süre ve günlük ceza; onları alt sınıflar veriyor.
    public int GecikenGun()
    {
        int gecikme = gecenGun - OduncSuresi();
        return gecikme > 0 ? gecikme : 0;
    }

    public decimal CezaHesapla()
    {
        return GecikenGun() * GunlukCeza();
    }

    public override string ToString()
    {
        return $"#{DemirbasNo} {Baslik}";
    }
}


class Kitap : Demirbas
{
    public string Yazar { get; private set; }
    public int SayfaSayisi { get; private set; }

    public Kitap(int no, string baslik, string yazar, int sayfa) : base(no, baslik)
    {
        Yazar = yazar;
        SayfaSayisi = sayfa;
    }

    public override string ToString()
    {
        return $"{base.ToString()} / {Yazar}";
    }
}


class Dergi : Demirbas
{
    public int Sayi { get; private set; }

    public Dergi(int no, string baslik, int sayi) : base(no, baslik)
    {
        Sayi = sayi;
    }

    public override int OduncSuresi()
    {
        return 7;
    }

    public override decimal GunlukCeza()
    {
        return 0.5m;
    }

    public override string ToString()
    {
        return $"{base.ToString()} sayı {Sayi}";
    }
}


class DVD : Demirbas
{
    public int SureDakika { get; private set; }

    public DVD(int no, string baslik, int sure) : base(no, baslik)
    {
        SureDakika = sure;
    }

    public override int OduncSuresi()
    {
        return 3;
    }

    public override decimal GunlukCeza()
    {
        return 5m;
    }

    public override string ToString()
    {
        return $"{base.ToString()} ({SureDakika} dk)";
    }
}

// --- DİKKAT EDİN: HANGİ METOT EZİLDİ, HANGİSİ EZİLMEDİ ---
//
// EZİLEN   : OduncSuresi, GunlukCeza, ToString  → türe göre değişiyor
// EZİLMEYEN: GecikenGun, CezaHesapla            → hesap her tür için aynı
//
// Bu ayrım iyi tasarımın özüdür. CezaHesapla metodu, ezilen iki metodu
// çağırarak çalışıyor — yani kendisi değişmeden her tür için doğru
// sonuç üretiyor. Yeni bir tür eklediğinizde bu metoda dokunmazsınız.
//
// --- DENEYİN ---
//
// Koleksiyona Harita türü ekleyin: ödünç süresi 30 gün, günlük ceza 2 TL.
// Rapor döngüsüne kaç satır eklemeniz gerekti?
