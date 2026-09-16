// Statik üyeler: nesneye değil, SINIFA ait olanlar.
//
// Şema: assets/02-statik-vs-ornek.svg
// Çalıştırmak için:  dotnet run 03-statik-uyeler.cs

Console.WriteLine($"Başlangıçta üretilen hesap sayısı: {BankaHesabi.ToplamHesapSayisi}");

BankaHesabi h1 = new BankaHesabi("Ayşe Yılmaz", 1000m);
BankaHesabi h2 = new BankaHesabi("Mehmet Demir", 2500m);
BankaHesabi h3 = new BankaHesabi("Zeynep Kaya", 400m);

Console.WriteLine($"\nÜç hesap açıldıktan sonra: {BankaHesabi.ToplamHesapSayisi}");

Console.WriteLine("\n--- Her hesabın KENDİ numarası ve bakiyesi var ---");
h1.OzetYazdir();
h2.OzetYazdir();
h3.OzetYazdir();

Console.WriteLine("\n--- Sayaç NESNEYE değil SINIFA ait ---");
// h1.ToplamHesapSayisi yazamayız: statik üye nesne üzerinden çağrılmaz.
// Aşağıdaki satırın yorumunu kaldırın, derlenmez:
// Console.WriteLine(h1.ToplamHesapSayisi);
Console.WriteLine($"Doğru kullanım: BankaHesabi.ToplamHesapSayisi = {BankaHesabi.ToplamHesapSayisi}");

Console.WriteLine("\n--- Statik metot ---");
Console.WriteLine($"Toplam hesap sayısı çift mi? {BankaHesabi.SayiCiftMi(BankaHesabi.ToplamHesapSayisi)}");


class BankaHesabi
{
    // STATİK ALAN — sınıfa ait, TEK KOPYA.
    // Kaç nesne üretilirse üretilsin bu sayaç birdir ve hepsi onu paylaşır.
    private static int uretilenHesapSayisi = 0;

    // Statik alanı dışarıya açan statik özellik
    public static int ToplamHesapSayisi
    {
        get { return uretilenHesapSayisi; }
    }

    // ÖRNEK (instance) ALANLARI — her nesnede AYRI kopya
    private decimal bakiye;

    public int HesapNo { get; private set; }
    public string SahipAdi { get; private set; }

    public BankaHesabi(string sahipAdi, decimal acilisBakiyesi)
    {
        this.SahipAdi = sahipAdi;
        bakiye = acilisBakiyesi;

        // Paylaşılan sayacı artır ve yeni numarayı buradan üret.
        uretilenHesapSayisi = uretilenHesapSayisi + 1;
        HesapNo = 1000 + uretilenHesapSayisi;
    }

    // STATİK METOT — nesne olmadan çağrılır.
    // Kendi başına anlamlı bir hesap yapıyor, hiçbir nesnenin verisine
    // ihtiyaç duymuyor. Bu yüzden statik olması doğru.
    public static bool SayiCiftMi(int sayi)
    {
        return sayi % 2 == 0;
    }

    // ÖRNEK METODU — nesnenin kendi verisiyle çalışıyor
    public void OzetYazdir()
    {
        Console.WriteLine($"#{HesapNo} {SahipAdi,-14} bakiye: {bakiye,10:C}");
    }
}

// --- KURAL: STATİK METOT ÖRNEK ÜYESİNE ERİŞEMEZ ---
//
// Yukarıdaki SayiCiftMi metodunun içine "bakiye" yazmayı deneyin:
//
//     public static bool SayiCiftMi(int sayi)
//     {
//         Console.WriteLine(bakiye);      // DERLEME HATASI
//         return sayi % 2 == 0;
//     }
//
// Hata: An object reference is required for the non-static field 'bakiye'
//
// Sebebi mantıklı: statik metot nesne olmadan çağrılır. Ortada nesne
// yokken "bakiye" denince HANGİ hesabın bakiyesi kastedilecek?
//
// Tersi serbesttir: örnek metodu statik üyeye erişebilir. Çünkü nesne
// varsa sınıf da vardır.
