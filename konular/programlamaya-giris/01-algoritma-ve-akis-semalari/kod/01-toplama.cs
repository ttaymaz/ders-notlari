// Doğrusal akış örneği — 01-dogrusal-akis.svg şemasının kod karşılığı.
//
// Çalıştırmak için:  dotnet run 01-toplama.cs
// .NET 10 ile tek bir .cs dosyası, proje kurmadan doğrudan çalışır.

Console.Write("Birinci sayıyı girin: ");
int sayi1 = Convert.ToInt32(Console.ReadLine());

Console.Write("İkinci sayıyı girin: ");
int sayi2 = Convert.ToInt32(Console.ReadLine());

int toplam = sayi1 + sayi2;

Console.WriteLine($"Toplam: {toplam}");
