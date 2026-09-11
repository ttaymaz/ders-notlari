// Mantıksal operatörler artık işe yarıyor: && ve ||
//
// Çalıştırmak için:  dotnet run 05-birden-fazla-kosul.cs
//
// Geçen hafta && ve || operatörlerini öğrendik ama kullanacak yerimiz yoktu.
// if yapısıyla birlikte anlam kazanıyorlar.

Console.Write("Vize notunuz: ");
double vize = Convert.ToDouble(Console.ReadLine());

Console.Write("Final notunuz: ");
double final = Convert.ToDouble(Console.ReadLine());

double ortalama = vize * 0.4 + final * 0.6;
Console.WriteLine($"Ortalamanız: {ortalama}");

// VE (&&): İKİ koşul da doğru olmalı
if (ortalama >= 60 && final >= 50)
{
    Console.WriteLine("Geçtiniz.");
}
else
{
    Console.WriteLine("Kaldınız.");
}

// VEYA (||): koşullardan BİRİ doğruysa yeter
if (vize == 0 || final == 0)
{
    Console.WriteLine("Uyarı: sınavlardan birine girmemişsiniz.");
}
