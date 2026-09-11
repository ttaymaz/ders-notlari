// Metni sayıya çevirme ve toplama.
//
// Çalıştırmak için:  dotnet run 02-toplama.cs
//
// KİLİT NOKTA: Console.ReadLine() HER ZAMAN metin döndürür.
// Kullanıcı 5 yazsa bile elimize "5" metni gelir, 5 sayısı değil.

Console.Write("Birinci sayı: ");
string gelen1 = Console.ReadLine();

Console.Write("İkinci sayı: ");
string gelen2 = Console.ReadLine();

// Metni sayıya çeviriyoruz. İki yol da aynı sonucu verir.
int sayi1 = Convert.ToInt32(gelen1);
int sayi2 = int.Parse(gelen2);

int toplam = sayi1 + sayi2;
Console.WriteLine($"Toplam: {toplam}");

// --- Denemeniz için ---
// Aşağıdaki satırın başındaki // işaretini kaldırın ve çalıştırın.
// Dönüştürmeden toplarsak ne oluyor? Neden?
// Console.WriteLine("Dönüştürmeden: " + (gelen1 + gelen2));
