// Tam sayı bölmesi tuzağı — yeni başlayanların en sık düştüğü hata.
//
// Çalıştırmak için:  dotnet run 03-bolme-tuzagi.cs

int a = 7;
int b = 2;

// İki tam sayıyı bölerseniz sonuç da TAM SAYI olur.
// Ondalık kısım yuvarlanmaz, doğrudan ATILIR.
int yanlis = a / b;
Console.WriteLine($"int / int      = {yanlis}");      // 3  ← 3.5 değil!

// Çözüm 1: değişkenlerden en az biri ondalıklı olsun
double dogru1 = (double)a / b;
Console.WriteLine($"(double)a / b  = {dogru1}");      // 3.5

// Çözüm 2: baştan double tanımlayın
double c = 7, d = 2;
Console.WriteLine($"double / double= {c / d}");       // 3.5

// Kalan operatörü: bölmeden kalanı verir
Console.WriteLine($"7 % 2          = {a % b}");       // 1

// Bu operatör ileride çok işinize yarayacak:
// bir sayının çift olup olmadığını anlamanın yolu sayi % 2 == 0
