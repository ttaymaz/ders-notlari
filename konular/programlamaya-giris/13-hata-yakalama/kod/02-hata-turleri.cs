// Farklı hatalar, farklı catch blokları.
//
// Çalıştırmak için:  dotnet run 02-hata-turleri.cs

int[] sayilar = { 10, 20, 30 };

Console.Write("Hangi indisteki sayıyı 0'a bölelim? (0-2): ");

try
{
    int indis = Convert.ToInt32(Console.ReadLine());
    int deger = sayilar[indis];              // IndexOutOfRangeException olabilir
    int sonuc = deger / 0;                   // DivideByZeroException olur
    Console.WriteLine(sonuc);
}
catch (FormatException)
{
    Console.WriteLine("Sayı yerine metin girdiniz.");
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("Dizide böyle bir indis yok. (0, 1 veya 2 giriniz)");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Bir tam sayı 0'a bölünemez.");
}
catch (Exception hata)
{
    // Yukarıdakilerden hiçbiri değilse buraya düşer
    Console.WriteLine($"Beklenmeyen hata: {hata.Message}");
}

// --- SIRA ÖNEMLİ ---
// Exception tüm hataların ATASIDIR. En üste yazarsanız diğer catch
// blokları asla çalışmaz ve C# DERLEME HATASI verir.
// Kural: özelden genele. Exception her zaman EN SONDA.
//
// Dördüncü haftadaki else-if zinciri kuralını hatırlayın:
// en dar koşul en üste. Burada da aynı mantık.
