// for döngüsü: tekrar sayısı baştan belli olduğunda.
//
// Çalıştırmak için:  dotnet run 03-for-dongusu.cs

// for (başlangıç; koşul; artış)
//      bir kez     her turda  her tur sonunda

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);
}

Console.WriteLine("--- geriye doğru ---");

for (int i = 10; i >= 1; i--)
{
    Console.WriteLine(i);
}

Console.WriteLine("--- ikişer ikişer ---");

for (int i = 0; i <= 20; i += 2)
{
    Console.Write($"{i} ");
}
Console.WriteLine();

// --- Denemeniz için: bir eksik / bir fazla hatası ---
// İlk döngüde i <= 10 yerine i < 10 yazın. Kaç sayı yazıyor?
// Bu hataya "off-by-one" (bir eksik/bir fazla) denir ve
// programlamanın en yaygın hatalarından biridir.
