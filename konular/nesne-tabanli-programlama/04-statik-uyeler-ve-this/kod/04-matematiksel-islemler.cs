// Statik sınıf: geçen yıldan kalan son borcun ödendiği yer.
//
// Aşırı yükleme haftasında metotlarımızı "static class Hesap" kutusuna
// koymuş ve şu notu düşmüştük:
//   "Bunları şimdi ezberlemeyin. İkinci sınıfta ayrıntısıyla öğreneceksiniz."
// Bu dosya o notun karşılığıdır.
//
// Çalıştırmak için:  dotnet run 04-matematiksel-islemler.cs

Console.WriteLine("--- Statik sınıf: nesne üretmeden kullanılır ---");
Console.WriteLine($"Kare(7)          = {MatematikselIslemler.Kare(7)}");
Console.WriteLine($"Kup(3)           = {MatematikselIslemler.Kup(3)}");
Console.WriteLine($"Faktoriyel(5)    = {MatematikselIslemler.Faktoriyel(5)}");
Console.WriteLine($"AsalMi(17)       = {MatematikselIslemler.AsalMi(17)}");
Console.WriteLine($"Ortalama(4, 7)   = {MatematikselIslemler.Ortalama(4, 7)}");
Console.WriteLine($"Ortalama(4,7,10) = {MatematikselIslemler.Ortalama(4, 7, 10)}");

Console.WriteLine($"\nPi sabiti        = {MatematikselIslemler.Pi}");

// Aşağıdaki satırın yorumunu kaldırın: DERLENMEZ.
// MatematikselIslemler m = new MatematikselIslemler();
//
// Hata: Cannot create an instance of the static class 'MatematikselIslemler'
Console.WriteLine("\nnew MatematikselIslemler();  →  derleme hatası");

Console.WriteLine("\n--- Zaten kullanıyordunuz ---");
Console.WriteLine($"Math.Max(3, 9)   = {Math.Max(3, 9)}");
Console.WriteLine($"Math.Sqrt(144)   = {Math.Sqrt(144)}");
Console.WriteLine("Math da bir statik sınıftır. Console da öyle.");


// static class: bu sınıftan NESNE ÜRETİLEMEZ.
// Tüm üyeleri static olmak zorundadır; derleyici bunu denetler.
static class MatematikselIslemler
{
    // Statik sabit — sınıfa ait, değişmez
    public const double Pi = 3.14159;

    public static int Kare(int sayi)
    {
        return sayi * sayi;
    }

    public static int Kup(int sayi)
    {
        return sayi * sayi * sayi;
    }

    public static long Faktoriyel(int n)
    {
        if (n < 0) { return 0; }

        long sonuc = 1;
        for (int i = 2; i <= n; i++)
        {
            sonuc = sonuc * i;
        }
        return sonuc;
    }

    public static bool AsalMi(int sayi)
    {
        if (sayi < 2) { return false; }

        for (int i = 2; i * i <= sayi; i++)
        {
            if (sayi % i == 0) { return false; }
        }
        return true;
    }

    // Aşırı yükleme — geçen yılki kural burada da aynen geçerli
    public static double Ortalama(int a, int b)
    {
        return (a + b) / 2.0;
    }

    public static double Ortalama(int a, int b, int c)
    {
        return (a + b + c) / 3.0;
    }
}

// --- GEÇEN YILIN ÜÇ KELİMESİ, ARTIK AÇIKLANMIŞ HÂLİYLE ---
//
//   class   : üyeleri bir arada tutan tip. Aşırı yükleme bir TİP ÜYESİ
//             özelliğidir; bu yüzden metotların bir sınıfın içinde
//             olması gerekiyordu. Yerel fonksiyonlar tip üyesi değildir.
//
//   static  : "bu üye nesneye değil sınıfa aittir". Kare metodunu
//             çağırmak için ortada bir MatematikselIslemler nesnesi
//             olmasına gerek yok — zaten öyle bir nesnenin ne anlamı
//             olurdu? İki farklı "matematik nesnesi" olmaz.
//
//   public  : sınıfın dışından erişilebilir. Üçüncü haftada gördük.
//
// --- NE ZAMAN STATİK SINIF? ---
//
// Sınıfın tuttuğu bir DURUM yoksa ve yalnızca girdi alıp çıktı veren
// metotlar barındırıyorsa statik sınıf doğru tercihtir.
//
// BankaHesabi statik olamaz: her hesabın kendi bakiyesi vardır, yani
// durumu vardır. MatematikselIslemler'in durumu yoktur — Kare(7) her
// zaman 49'dur, hangi "matematik nesnesi" üzerinden sorduğunuz fark etmez.
