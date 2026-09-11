// switch-case: tek bir değişkeni birçok sabit değerle karşılaştırma.
//
// Çalıştırmak için:  dotnet run 04-switch-gun.cs

Console.Write("Haftanın kaçıncı günü? (1-7): ");
int gun = Convert.ToInt32(Console.ReadLine());

switch (gun)
{
    case 1:
        Console.WriteLine("Pazartesi");
        break;
    case 2:
        Console.WriteLine("Salı");
        break;
    case 3:
        Console.WriteLine("Çarşamba");
        break;
    case 4:
        Console.WriteLine("Perşembe");
        break;
    case 5:
        Console.WriteLine("Cuma");
        break;

    // Birden fazla değer aynı işi yapacaksa case'leri alt alta yazın.
    case 6:
    case 7:
        Console.WriteLine("Hafta sonu!");
        break;

    default:
        Console.WriteLine("Hatalı giriş. 1 ile 7 arası bir sayı giriniz.");
        break;
}

// --- Denemeniz için ---
// case 1 satırındaki break; komutunu silin ve derlemeyi deneyin.
// C# ne diyor? (İpucu: bazı dillerde bu sessizce çalışır, C#'ta çalışmaz.)
