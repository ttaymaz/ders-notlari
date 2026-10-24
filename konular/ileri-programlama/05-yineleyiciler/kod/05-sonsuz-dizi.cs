// Ertelenmiş yürütmenin bir sonucu: sonsuz diziler.
//
// Fibonacci() hiç bitmez; ama çağıran yalnızca istediği kadarını alır.
// Liste döndüren bir metot sonsuz bir diziyi asla döndüremezdi: bellek biterdi.
// yield break, gezintiyi bilerek sonlandırır.
// Çalıştırmak için:  dotnet run 05-sonsuz-dizi.cs

foreach (var f in Fibonacci())
{
    if (f > 100) break;
    Console.Write($"{f} ");
}
Console.WriteLine();

foreach (var f in Fibonacci(sinir: 30))
    Console.Write($"{f} ");
Console.WriteLine();

IEnumerable<long> Fibonacci(long sinir = long.MaxValue)
{
    long a = 0, b = 1;
    while (true)
    {
        if (a > sinir) yield break;
        yield return a;
        (a, b) = (b, a + b);
    }
}
