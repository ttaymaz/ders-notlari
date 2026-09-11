// Faktöriyel hesaplama — döngüyle biriktirme.
//
// Çalıştırmak için:  dotnet run 01-faktoriyel.cs
//
// 5! = 5 * 4 * 3 * 2 * 1 = 120

Console.Write("Faktöriyeli hesaplanacak sayı: ");
int sayi = Convert.ToInt32(Console.ReadLine());

// Sonucu biriktiren değişken ÇARPMA yaptığımız için 1'den başlar.
// 0'dan başlatsaydık her şey 0 olurdu.
//
// long kullanıyoruz çünkü faktöriyel çok hızlı büyür:
//   12! =           479.001.600  → int'e sığar
//   13! =         6.227.020.800  → int'e SIĞMAZ
//   20! = 2.432.902.008.176.640.000 → long'a sığar
long sonuc = 1;

for (int i = 1; i <= sayi; i++)
{
    sonuc *= i;          // sonuc = sonuc * i;  ile aynı
}

Console.WriteLine($"{sayi}! = {sonuc}");

// --- Denemeniz için ---
// 1) long yerine int yazın ve 13 girin. Sonuç ne çıkıyor? Neden negatif?
// 2) 21 girin. long bile yetmiyor — sonuç saçmalıyor.
//    Buna "taşma" (overflow) denir: sayı kabına sığmadığında sessizce bozulur.
// 3) 0 girin. Döngü hiç çalışmıyor, sonuç 1 kalıyor. 0! = 1 kuralı sağlanmış oldu.
