// for döngüsü + if karar yapısı bir arada.
//
// Çalıştırmak için:  dotnet run 06-cift-sayilar.cs

Console.WriteLine("1-100 arasındaki çift sayılar:");

for (int i = 1; i <= 100; i++)
{
    if (i % 2 == 0)
    {
        Console.Write($"{i} ");
    }
}

Console.WriteLine();

// --- Denemeniz için ---
// Aynı işi if KULLANMADAN yapabilir misiniz?
// İpucu: döngü 2'den başlayıp ikişer ikişer artarsa if'e gerek kalır mı?
//
// İki çözüm de doğrudur. Hangisi daha az iş yapıyor?
// (100 kontrol mü, 50 tur mu?)
