// Çarpım tablosu — iç içe döngünün klasik örneği.
//
// Çalıştırmak için:  dotnet run 03-carpim-tablosu.cs

for (int i = 1; i <= 10; i++)         // satırlar: 1'ler, 2'ler, 3'ler...
{
    for (int j = 1; j <= 10; j++)     // sütunlar: x1, x2, x3...
    {
        // Write kullanıyoruz ki imleç aynı satırda kalsın.
        // \t (sekme) karakteri sütunları hizalar.
        Console.Write($"{i}x{j}={i * j}\t");
    }

    // İç döngü bitti — bir sonraki satıra geçmeden alt satıra in.
    Console.WriteLine();
}

// --- Denemeniz için ---
// Son Console.WriteLine() satırını silin. Ne oluyor?
// İç döngüde Write, dış döngü sonunda WriteLine — bu kalıbı aklınızda tutun.
