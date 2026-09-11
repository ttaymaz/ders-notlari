// break ve continue: döngü akışını yönetmek.
//
// Çalıştırmak için:  dotnet run 05-break-continue.cs

// --- break: döngüyü TAMAMEN bitirir ---
Console.WriteLine("break örneği — 5'i bulunca dur:");

for (int i = 1; i <= 10; i++)
{
    if (i == 5)
    {
        break;              // döngüden tamamen çık
    }
    Console.Write($"{i} ");
}

Console.WriteLine("\n");

// --- continue: o turu ATLAR, döngü devam eder ---
Console.WriteLine("continue örneği — çift sayıları atla:");

for (int i = 1; i <= 10; i++)
{
    if (i % 2 == 0)
    {
        continue;           // bu turu atla, sonrakine geç
    }
    Console.Write($"{i} ");
}

Console.WriteLine("\n");

// --- İÇ İÇE DÖNGÜDE break SADECE İÇ DÖNGÜYÜ BİTİRİR ---
Console.WriteLine("İç içe döngüde break:");

for (int i = 1; i <= 3; i++)
{
    for (int j = 1; j <= 5; j++)
    {
        if (j == 3)
        {
            break;          // yalnızca İÇ döngüden çıkar, dış döngü devam eder
        }
        Console.Write($"({i},{j}) ");
    }
}

Console.WriteLine();

// Yukarıdaki çıktıda dış döngünün 3 kez döndüğünü,
// iç döngünün her seferinde 3'te kesildiğini görüyorsunuz.
