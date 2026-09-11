// if-else: iki yoldan biri mutlaka çalışır.
//
// Çalıştırmak için:  dotnet run 02-tek-cift.cs
//
// Geçen hafta öğrendiğimiz % (kalan) operatörü burada işe yarıyor:
// bir sayı 2'ye tam bölünüyorsa kalan 0'dır, yani çifttir.

Console.Write("Bir sayı giriniz: ");
int sayi = Convert.ToInt32(Console.ReadLine());

if (sayi % 2 == 0)
{
    Console.WriteLine($"{sayi} ÇİFT sayıdır.");
}
else
{
    Console.WriteLine($"{sayi} TEK sayıdır.");
}
