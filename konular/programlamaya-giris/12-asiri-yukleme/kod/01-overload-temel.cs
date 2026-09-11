// Aşırı yükleme: aynı isim, farklı parametreler.
//
// Çalıştırmak için:  dotnet run 01-overload-temel.cs

// --- NEDEN BU HAFTA "class" GÖRÜYORUZ? ---
// Aşırı yükleme, metotların bir TİP (class) içinde tanımlanmasını gerektirir.
// Şimdiye kadar yazdığımız metotlar "yerel fonksiyon"du ve C# yerel
// fonksiyonların aşırı yüklenmesine izin vermez.
//
// Bu yüzden metotları küçük bir kutuya koyuyoruz. Kutunun adı Hesap.
// Çağırırken kutunun adını da yazıyoruz: Hesap.Topla(...)

Console.WriteLine(Hesap.Topla(5, 10));          // versiyon 1
Console.WriteLine(Hesap.Topla(5, 10, 15));      // versiyon 2
Console.WriteLine(Hesap.Topla(5.5, 2.3));       // versiyon 3

// --- Zaten kullanıyordunuz ---
// Console.WriteLine de aşırı yüklenmiş bir metottur.
// Console da bir kutudur — WriteLine onun içindeki metot.
Console.WriteLine(42);
Console.WriteLine("metin");
Console.WriteLine(true);
Console.WriteLine(3.14);


// Metot kutumuz. Bu yapının ayrıntısını ikinci sınıfta göreceksiniz.
// static: "bu metodu çağırmak için kutudan bir örnek üretmeye gerek yok"
static class Hesap
{
    // Versiyon 1 — iki tam sayı
    public static int Topla(int sayi1, int sayi2)
    {
        Console.Write("[int, int] → ");
        return sayi1 + sayi2;
    }

    // Versiyon 2 — üç tam sayı (parametre SAYISI farklı)
    public static int Topla(int sayi1, int sayi2, int sayi3)
    {
        Console.Write("[int, int, int] → ");
        return sayi1 + sayi2 + sayi3;
    }

    // Versiyon 3 — iki ondalıklı (parametre TİPİ farklı)
    public static double Topla(double sayi1, double sayi2)
    {
        Console.Write("[double, double] → ");
        return sayi1 + sayi2;
    }
}
