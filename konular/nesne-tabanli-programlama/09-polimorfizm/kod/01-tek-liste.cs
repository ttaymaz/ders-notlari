// Polimorfizm: farklı türler tek listede, tek döngüde.
//
// Altıncı haftada "aynı nesne, iki farklı çıktı" örneğinde bunun bir
// önizlemesini görmüştük. Adı polimorfizm ve asıl değeri burada ortaya
// çıkıyor: bir kütüphanedeki HER ŞEYİ tek listede tutabilmek.
//
// Şema: assets/01-tek-liste.svg
// Çalıştırmak için:  dotnet run 01-tek-liste.cs

// Dizinin tipi Demirbas — ama içine üç FARKLI tür koyuyoruz.
// Her biri "bir Demirbaştır", o yüzden bu geçerli.
Demirbas[] demirbaslar =
{
    new Kitap(101, "Tutunamayanlar", "Oğuz Atay"),
    new Dergi(102, "Bilim ve Teknik", 745),
    new DVD(103, "Kış Uykusu", 196),
    new Kitap(104, "Kürk Mantolu Madonna", "Sabahattin Ali"),
};

Console.WriteLine("--- Tek döngü, üç farklı davranış ---");
foreach (Demirbas d in demirbaslar)
{
    // Bu satır hangi sınıfın metodunu çağıracağını BİLMİYOR.
    // Kararı çalışma anında nesnenin gerçek türü veriyor.
    d.BilgiYazdir();
}

Console.WriteLine("\n--- Ortak davranış da aynı listede çalışır ---");
demirbaslar[0].OduncVer();
demirbaslar[2].OduncVer();

Console.WriteLine("\n--- Ödünçte olanlar ---");
foreach (Demirbas d in demirbaslar)
{
    if (d.OduncVerildi)
    {
        Console.WriteLine($"  {d.Baslik}");
    }
}

Console.WriteLine("\n--- Toplam gecikme cezası (türe göre farklı) ---");
decimal toplam = 0m;
foreach (Demirbas d in demirbaslar)
{
    toplam = toplam + d.GunlukCeza();
}
Console.WriteLine($"Günlük toplam ceza oranı: {toplam:C}");


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public bool OduncVerildi { get; private set; }

    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }

    public void OduncVer()
    {
        if (OduncVerildi)
        {
            Console.WriteLine($"{Baslik}: zaten ödünçte.");
            return;
        }

        OduncVerildi = true;
        Console.WriteLine($"{Baslik}: ödünç verildi.");
    }

    public virtual void BilgiYazdir()
    {
        Console.WriteLine($"#{DemirbasNo} {Baslik}");
    }

    // Türlere göre değişen bir hesap. Varsayılan ceza 1 TL.
    public virtual decimal GunlukCeza()
    {
        return 1m;
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

    // Dergi geç kalırsa ceza daha düşük
    public override decimal GunlukCeza()
    {
        return 0.5m;
    }
}


class DVD : Demirbas
{
    public int SureDakika { get; private set; }

    public DVD(int no, string baslik, int sure) : base(no, baslik)
    {
        SureDakika = sure;
    }

    public override void BilgiYazdir()
    {
        Console.WriteLine($"#{DemirbasNo} {Baslik} ({SureDakika} dk)");
    }

    // DVD daha değerli, cezası yüksek
    public override decimal GunlukCeza()
    {
        return 5m;
    }
}

// --- POLİMORFİZM NEDİR? ---
//
// Yunanca "çok biçimlilik". Tanımı şu:
//   Bir TEMEL SINIF referansı üzerinden çağrılan metot, nesnenin
//   GERÇEK türüne göre çalışır.
//
// Üç şey bir arada gerekir:
//   1. Kalıtım          (Kitap bir Demirbastır)
//   2. virtual/override (metot ezilebilir olmalı)
//   3. Temel tip referans (Demirbas d = ...)
//
// Üçü olmadan polimorfizm olmaz. Altıncı haftada virtual'sız denemiş ve
// temel sınıfın versiyonunun çalıştığını görmüştük.
