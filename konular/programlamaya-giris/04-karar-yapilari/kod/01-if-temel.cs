// En basit karar: if.
//
// Çalıştırmak için:  dotnet run 01-if-temel.cs

Console.Write("Bir sayı giriniz: ");
int sayi = Convert.ToInt32(Console.ReadLine());

if (sayi > 0)
{
    Console.WriteLine("Girdiğiniz sayı pozitiftir.");
}

// Bu satır, koşul doğru da olsa yanlış da olsa HER ZAMAN çalışır.
// Çünkü if bloğunun dışında.
Console.WriteLine("Program sona erdi.");
