// Sınav provası Problem 2'nin kod hali.
//
// Sınavda KOD YAZMANIZ istenmeyecek — algoritma yazacaksınız.
// Bu dosya, yazdığınız algoritmanın koda nasıl döndüğünü göstermek için.
//
// Çalıştırmak için:  dotnet run 02-negatif-toplam.cs

int negatifToplam = 0;              // biriktirici, döngü DIŞINDA, toplama için 0

Console.Write("Kaç adet sayı gireceksiniz? ");
int n = Convert.ToInt32(Console.ReadLine());

for (int i = 1; i <= n; i++)
{
    Console.Write($"{i}. sayı: ");
    int sayi = Convert.ToInt32(Console.ReadLine());

    if (sayi < 0)
    {
        negatifToplam += sayi;      // yalnızca negatifse ekle
    }
}

Console.WriteLine($"Negatif sayıların toplamı: {negatifToplam}");

// Algoritma adımlarıyla karşılaştırın:
//   1. Başla
//   2. negatifToplam = 0
//   3. N'i oku
//   4. 1'den N'e kadar döngü
//   5.   sayi'yı oku
//   6.   Eğer sayi < 0 ise negatifToplam'a ekle
//   7. Döngü bitince negatifToplam'ı yaz
//   8. Dur
