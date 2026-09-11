// Çıktı tahmini alıştırması — sınavın A bölümü bu tarz sorular içeriyor.
//
// ÖNCE kağıda yazın, SONRA çalıştırın:  dotnet run 01-cikti-tahmini.cs
//
// Her bloğun çıktısını tahmin edin. Yanıldığınız yer, tekrar etmeniz
// gereken konudur.

Console.WriteLine("--- 1 ---");
int a = 7, b = 2;
Console.WriteLine(a / b);
Console.WriteLine((double)a / b);
Console.WriteLine(a % b);

Console.WriteLine("--- 2 ---");
for (int i = 0; i < 5; i++)
{
    Console.Write($"{i} ");
}
Console.WriteLine();

Console.WriteLine("--- 3 ---");
int notu = 95;
if (notu >= 60)      Console.WriteLine("CC");
else if (notu >= 70) Console.WriteLine("BB");
else if (notu >= 90) Console.WriteLine("AA");

Console.WriteLine("--- 4 ---");
int x = 5;
Console.WriteLine(x++);
Console.WriteLine(x);

Console.WriteLine("--- 5 ---");
int sayac = 100;
do
{
    Console.WriteLine("Çalıştım");
} while (sayac < 5);

Console.WriteLine("--- 6 ---");
for (int i = 1; i <= 3; i++)
{
    for (int j = 1; j <= 3; j++)
    {
        if (j == 2) break;
        Console.Write($"({i},{j}) ");
    }
}
Console.WriteLine();

Console.WriteLine("--- 7 ---");
Console.WriteLine("5" + "3");
Console.WriteLine(5 + 3);
