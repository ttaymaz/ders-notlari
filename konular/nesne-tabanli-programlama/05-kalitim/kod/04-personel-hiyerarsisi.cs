// İkinci örnek: personel hiyerarşisi.
//
// Kütüphane demirbaşları "nesneleri" modelliyordu; bu örnek "insanları"
// modelliyor. Kalıtımın her iki durumda da aynı biçimde çalıştığını
// görmek için iki farklı alandan örnek veriyoruz.
//
// Çalıştırmak için:  dotnet run 04-personel-hiyerarsisi.cs

Yonetici y = new Yonetici("Ayşe Yılmaz", 60000m, "Bilgi İşlem", 8000m);
Memur m1 = new Memur("Mehmet Demir", 42000m, 12);
Memur m2 = new Memur("Zeynep Kaya", 45000m, 0);

y.BilgiYazdir();
m1.BilgiYazdir();
m2.BilgiYazdir();

Console.WriteLine("\n--- Türetilmiş sınıfa özgü davranışlar ---");
y.ToplantiDuzenle("Dönem değerlendirmesi");
m1.MesaiBildir();
m2.MesaiBildir();

Console.WriteLine("\n--- protected alan türetilmiş sınıftan görünüyor ---");
y.ZamYap(10);
y.BilgiYazdir();


class Personel
{
    public string AdSoyad { get; private set; }
    public string Departman { get; protected set; }

    // protected: dışarıdan kapalı, türetilmiş sınıflara açık.
    // Yonetici ve Memur bu alana doğrudan erişebilecek.
    protected decimal temelMaas;

    public Personel(string adSoyad, decimal temelMaas)
    {
        AdSoyad = adSoyad;
        this.temelMaas = temelMaas;
        Departman = "Atanmadı";
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{AdSoyad,-14} {Departman,-12} temel maaş: {temelMaas,10:C}");
    }

    // Zam kuralı tek yerde: iki türetilmiş sınıf da aynı kuralı kullanır.
    public void ZamYap(int yuzde)
    {
        if (yuzde <= 0)
        {
            Console.WriteLine($"{AdSoyad}: zam oranı pozitif olmalı.");
            return;
        }

        temelMaas = temelMaas + (temelMaas * yuzde / 100m);
        Console.WriteLine($"{AdSoyad}: %{yuzde} zam uygulandı.");
    }
}


class Yonetici : Personel
{
    public decimal YoneticiTazminati { get; private set; }

    public Yonetici(string adSoyad, decimal temelMaas, string departman, decimal tazminat)
        : base(adSoyad, temelMaas)
    {
        Departman = departman;          // protected set sayesinde
        YoneticiTazminati = tazminat;
    }

    public void ToplantiDuzenle(string konu)
    {
        Console.WriteLine($"{AdSoyad} toplantı düzenledi: \"{konu}\"");
    }

    // protected alana doğrudan erişim — private olsaydı derlenmezdi.
    public decimal ToplamMaas()
    {
        return temelMaas + YoneticiTazminati;
    }
}


class Memur : Personel
{
    public int MesaiSaati { get; private set; }

    public Memur(string adSoyad, decimal temelMaas, int mesaiSaati)
        : base(adSoyad, temelMaas)
    {
        MesaiSaati = mesaiSaati;
    }

    public void MesaiBildir()
    {
        if (MesaiSaati == 0)
        {
            Console.WriteLine($"{AdSoyad}: bu ay mesai yapmadı.");
            return;
        }

        Console.WriteLine($"{AdSoyad}: {MesaiSaati} saat mesai yaptı.");
    }

    public decimal ToplamMaas()
    {
        return temelMaas + (MesaiSaati * 250m);
    }
}

// --- DİKKAT: İKİ AYRI ToplamMaas ---
//
// Yonetici ve Memur sınıflarının her ikisinde de ToplamMaas metodu var
// ama ikisi ilgisiz metotlar: aynı ada sahip olmaları tesadüf.
//
// Bu iyi bir tasarım DEĞİL. "Her personelin bir toplam maaşı vardır,
// ama hesaplanışı türe göre değişir" demek istiyoruz. Bunu söylemenin
// doğru yolu, metodu temel sınıfa koyup türetilmiş sınıflarda EZMEKTİR.
//
// Gelecek haftanın konusu tam olarak bu.
