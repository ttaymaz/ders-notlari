// Ertelenmiş yürütme: yield return içeren metot ÇAĞRILDIĞINDA çalışmaz.
//
// Çağrı yalnızca bir gezgin döndürür. Metodun gövdesi, foreach eleman
// istedikçe parça parça çalışır. Köşeli parantezli satırlar metodun
// içinden, diğerleri dışarıdan yazılıyor: sıralarına bakın.
// Çalıştırmak için:  dotnet run 03-ertelenmis-yurutme.cs

var sayilar = Uret();
Console.WriteLine("Uret() çağrıldı");

foreach (var x in sayilar)
    Console.WriteLine($"alındı: {x}");

Console.WriteLine("--- ikinci kez");
foreach (var x in sayilar)
{
    Console.WriteLine($"alındı: {x}");
    break;
}

IEnumerable<int> Uret()
{
    Console.WriteLine("  [başladı]");
    for (int i = 1; i <= 3; i++)
    {
        Console.WriteLine($"  [üretiliyor: {i}]");
        yield return i;
    }
    Console.WriteLine("  [bitti]");
}
