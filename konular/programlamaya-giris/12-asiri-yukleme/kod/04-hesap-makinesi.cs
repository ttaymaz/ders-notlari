// Modüler hesap makinesi — metotlar + switch + aşırı yükleme.
//
// Çalıştırmak için:  dotnet run 04-hesap-makinesi.cs

Console.Write("İşlem seçin (+ - * /): ");
string islem = Console.ReadLine();

Console.Write("1. sayı: ");
double s1 = Convert.ToDouble(Console.ReadLine());

Console.Write("2. sayı: ");
double s2 = Convert.ToDouble(Console.ReadLine());

double sonuc = 0;
bool gecerli = true;

switch (islem)
{
    case "+": sonuc = Islem.Topla(s1, s2); break;
    case "-": sonuc = Islem.Cikar(s1, s2); break;
    case "*": sonuc = Islem.Carp(s1, s2);  break;
    case "/":
        if (s2 == 0)
        {
            Console.WriteLine("Sıfıra bölme yapılamaz!");
            gecerli = false;
        }
        else
        {
            sonuc = s1 / s2;
        }
        break;
    default:
        Console.WriteLine("Geçersiz işlem.");
        gecerli = false;
        break;
}

if (gecerli)
{
    Console.WriteLine($"Sonuç: {sonuc}");
}

// Üç sayılı toplamayı da deneyelim — aşırı yüklenmiş versiyon
Console.WriteLine($"\nÜç sayı toplamı örneği: {Islem.Topla(1, 2, 3)}");


static class Islem
{
    // --- Her işlem kendi metodunda: tek sorumluluk ---
    public static double Topla(double a, double b) { return a + b; }
    public static double Cikar(double a, double b) { return a - b; }
    public static double Carp(double a, double b)  { return a * b; }

    // --- Aşırı yükleme: üç sayıyı da toplayabilelim ---
    public static double Topla(double a, double b, double c) { return a + b + c; }
}

// --- NEDEN BÖYLE? ---
// Her işlemi ayrı metoda koymak, switch bloğunu okunur kılıyor.
// Tek bir IslemYap(a, b, "topla") metodu yazsaydık, metodun içine
// bir if zinciri koymamız gerekirdi — ve bu AŞIRI YÜKLEME OLMAZDI.
// Aşırı yükleme, imzaların farklı olmasıdır; string parametreyle
// dallanmak değildir.
