// İmza nedir, neyi değiştirmek yeterlidir?
//
// Çalıştırmak için:  dotnet run 02-imza-kurallari.cs
//
// İMZA = metot adı + parametrelerin tipi, sayısı ve sırası
// Dönüş tipi imzaya DAHİL DEĞİLDİR.

Ornek.Bilgi("Ayşe");
Ornek.Bilgi("Ayşe", 20);
Ornek.Yaz(7);
Ornek.Yaz(7.5);
Ornek.Kayit("Ali", 101);
Ornek.Kayit(101, "Ali");


static class Ornek
{
    // 1) Parametre SAYISI farklı → geçerli
    public static void Bilgi(string ad) { Console.WriteLine($"Ad: {ad}"); }
    public static void Bilgi(string ad, int yas) { Console.WriteLine($"Ad: {ad}, Yaş: {yas}"); }

    // 2) Parametre TİPİ farklı → geçerli
    public static void Yaz(int sayi) { Console.WriteLine($"Tam sayı: {sayi}"); }
    public static void Yaz(double sayi) { Console.WriteLine($"Ondalıklı: {sayi}"); }

    // 3) Parametre SIRASI farklı → geçerli
    public static void Kayit(string ad, int no) { Console.WriteLine($"{ad} / {no}"); }
    public static void Kayit(int no, string ad) { Console.WriteLine($"{no} / {ad}"); }

    // --- GEÇERSİZ: yalnızca dönüş tipi farklı ---
    // Aşağıdaki satırların başındaki // işaretini kaldırın. DERLENMEZ.
    //
    // public static int Hesapla(int x) { return x * 2; }
    // public static double Hesapla(int x) { return x * 2.5; }
    //
    // Neden? Hesapla(5) yazdığınızda C# hangisini çağıracağını bilemez.
    // Dönüş tipi çağrı satırında görünmez — imzaya dahil değildir.

    // --- GEÇERSİZ: yalnızca parametre ADI farklı ---
    // public static void Selam(string ad) { }
    // public static void Selam(string isim) { }     // aynı imza, DERLENMEZ
}
