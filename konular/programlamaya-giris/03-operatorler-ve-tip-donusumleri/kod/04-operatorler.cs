// Aritmetik ve atama operatörleri.
//
// Çalıştırmak için:  dotnet run 04-operatorler.cs

int x = 10;
int y = 3;

Console.WriteLine($"{x} + {y} = {x + y}");
Console.WriteLine($"{x} - {y} = {x - y}");
Console.WriteLine($"{x} * {y} = {x * y}");
Console.WriteLine($"{x} / {y} = {x / y}   <- tam sayı bölmesi!");
Console.WriteLine($"{x} % {y} = {x % y}   <- kalan");

Console.WriteLine();

// --- Atama operatörleri: kısayollar ---
int sayac = 5;

sayac += 3;   // sayac = sayac + 3  ile aynı
Console.WriteLine($"+= 3 sonrası: {sayac}");   // 8

sayac -= 2;
Console.WriteLine($"-= 2 sonrası: {sayac}");   // 6

sayac *= 2;
Console.WriteLine($"*= 2 sonrası: {sayac}");   // 12

// --- Bir artır / bir azalt ---
sayac++;      // sayac = sayac + 1
Console.WriteLine($"++ sonrası:   {sayac}");   // 13

sayac--;
Console.WriteLine($"-- sonrası:   {sayac}");   // 12

// ++ operatörünü döngülerde çok kullanacağız.
// Geçen haftaki döngü akış şemasındaki "sayac = sayac + 1" kutusunu hatırlayın.
