// Yıldızlarla şekil çizme — iç içe döngü pratiği.
//
// Çalıştırmak için:  dotnet run 04-sekil-cizme.cs

Console.Write("Satır sayısı: ");
int satir = Convert.ToInt32(Console.ReadLine());

Console.Write("Sütun sayısı: ");
int sutun = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("\n--- Dikdörtgen ---");

for (int i = 1; i <= satir; i++)          // dış döngü: satırlar
{
    for (int j = 1; j <= sutun; j++)      // iç döngü: o satırdaki sütunlar
    {
        Console.Write("* ");
    }
    Console.WriteLine();                  // satır bitti
}

// --- Denemeniz için: DİK ÜÇGEN ---
// Aşağıdaki satırların başındaki // işaretini kaldırın.
// Tek fark: iç döngünün bitiş koşulu SABİT değil, dış sayaca BAĞLI.
//
// Console.WriteLine("\n--- Dik üçgen ---");
// for (int i = 1; i <= satir; i++)
// {
//     for (int j = 1; j <= i; j++)      // <- dikkat: j <= i
//     {
//         Console.Write("* ");
//     }
//     Console.WriteLine();
// }
