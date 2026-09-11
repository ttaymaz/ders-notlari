// Çökmeyen hesap makinesi.
//
// Çalıştırmak için:  dotnet run 05-saglam-hesap-makinesi.cs

try
{
    Console.Write("1. sayı: ");
    double s1 = Convert.ToDouble(Console.ReadLine());

    Console.Write("2. sayı: ");
    double s2 = Convert.ToDouble(Console.ReadLine());

    Console.Write("İşlem (+ - * /): ");
    string islem = Console.ReadLine();

    double sonuc = 0;

    switch (islem)
    {
        case "+": sonuc = s1 + s2; break;
        case "-": sonuc = s1 - s2; break;
        case "*": sonuc = s1 * s2; break;
        case "/":
            // DİKKAT: double bölmede 5.0 / 0.0 hata VERMEZ, Infinity verir!
            // Hatayı biz elle fırlatmalıyız.
            if (s2 == 0) { throw new DivideByZeroException(); }
            sonuc = s1 / s2;
            break;
        default:
            throw new ArgumentException("Geçersiz işlem seçildi.");
    }

    Console.WriteLine($"Sonuç: {sonuc}");
}
catch (FormatException)
{
    Console.WriteLine("Lütfen sayısal değer giriniz!");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Bir sayı 0'a bölünemez!");
}
catch (ArgumentException hata)
{
    Console.WriteLine(hata.Message);
}
catch (Exception hata)
{
    Console.WriteLine($"Beklenmeyen bir hata: {hata.Message}");
}
finally
{
    Console.WriteLine("İşlem tamamlandı.");   // hata olsa da olmasa da çalışır
}

// --- Denemeniz için ---
// 1) 5 / 0 girin — DivideByZeroException elle fırlatıldı, yakalandı.
// 2) throw satırını silin, 5 / 0 girin. Ne yazıyor? "Infinity"
//    double bölmede sıfıra bölme hata değildir — sessiz bir sonuçtur.
// 3) int ile deneseydik? 5 / 0 gerçekten DivideByZeroException fırlatırdı.
