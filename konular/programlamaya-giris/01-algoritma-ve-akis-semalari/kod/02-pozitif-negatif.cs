// Mantıksal (şartlı) akış örneği — 02-mantiksal-akis.svg şemasının kod karşılığı.
//
// Çalıştırmak için:  dotnet run 02-pozitif-negatif.cs
//
// Not: if-else yapısını 4. haftada ayrıntılı işleyeceğiz. Burada amaç,
// akış şemasındaki karar kutusunun kodda neye dönüştüğünü görmek.

Console.Write("Bir sayı girin: ");
int sayi = Convert.ToInt32(Console.ReadLine());

if (sayi > 0)
{
    Console.WriteLine("POZİTİF");
}
else
{
    Console.WriteLine("NEGATİF");
}
