// Sıralı beklemek, birlikte beklemek.
//
// Uzak bir kataloğa yapılan çağrıyı Task.Delay ile taklit ediyoruz: her
// çağrı G/Ç'ye bağlı bir iştir, beklerken işlemciyi kullanmaz. Üç çağrıyı
// art arda beklemek ile üçünü birden başlatıp birlikte beklemek arasındaki
// fark ne kadar?
//
// Süreler makineye göre birkaç milisaniye oynar; dosya süreyi bir
// gecikmenin (200 ms) katına yuvarlayarak yazıyor.
//
// 2. bölümde görevler farklı sürelerde bitiyor. Bitiş sırası ile
// Task.WhenAll'un döndürdüğü dizinin sırası aynı mı? 4. bölümde iki görev
// de hata veriyor; catch bloğuna kaç hata gelir?
// Çalıştırmak için:  dotnet run 02-sirali-ve-eszamanli.cs

using System.Diagnostics;

Console.WriteLine("=== 1 === (sıralı: await, await, await)");
var saat = Stopwatch.StartNew();
string a = await Katalog.GetirAsync("Huzur", 1);
string b = await Katalog.GetirAsync("Aylak Adam", 1);
string c = await Katalog.GetirAsync("Beş Şehir", 1);
Console.WriteLine($"{a}, {b}, {c}");
Console.WriteLine($"süre ≈ {Katalog.Yuvarla(saat)} gecikme");

Console.WriteLine("\n=== 2 === (birlikte: önce başlat, sonra Task.WhenAll)");
saat.Restart();
Task<string> ga = Katalog.GetirAsync("Huzur", 3);
Task<string> gb = Katalog.GetirAsync("Aylak Adam", 1);
Task<string> gc = Katalog.GetirAsync("Beş Şehir", 2);
string[] hepsi = await Task.WhenAll(ga, gb, gc);
Console.WriteLine(string.Join(", ", hepsi));
Console.WriteLine($"süre ≈ {Katalog.Yuvarla(saat)} gecikme");

Console.WriteLine("\n=== 3 === (ilk gelen: Task.WhenAny)");
saat.Restart();
Task<string> asil = Katalog.GetirAsync("asıl sunucu", 3);
Task<string> yedek = Katalog.GetirAsync("yedek sunucu", 1);
Task<string> kazanan = await Task.WhenAny(asil, yedek);
Console.WriteLine($"kazanan: {await kazanan}");
Console.WriteLine($"süre ≈ {Katalog.Yuvarla(saat)} gecikme");
Console.WriteLine($"asıl sunucu bitti mi? {asil.IsCompleted}");
await asil;   // bırakılan görev arka planda sürüyor; programı bitirmeden bekleyelim

Console.WriteLine("\n=== 4 === (iki görev hata verirse)");
Task h1 = Katalog.BozukAsync("Huzur", 3);
Task h2 = Katalog.BozukAsync("Aylak Adam", 1);
Task ikisi = Task.WhenAll(h1, h2);
try
{
    await ikisi;
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"catch'e gelen: {e.Message}");
}
Console.WriteLine($"görevdeki hata sayısı: {ikisi.Exception!.InnerExceptions.Count}");

static class Katalog
{
    public const int GecikmeMs = 200;

    public static async Task<string> GetirAsync(string baslik, int gecikme)
    {
        await Task.Delay(gecikme * GecikmeMs);
        Console.WriteLine($"  bitti: {baslik}");
        return $"«{baslik}»";
    }

    public static async Task BozukAsync(string baslik, int gecikme)
    {
        await Task.Delay(gecikme * GecikmeMs);
        throw new InvalidOperationException($"{baslik} getirilemedi");
    }

    public static long Yuvarla(Stopwatch saat)
        => (long)Math.Round(saat.Elapsed.TotalMilliseconds / GecikmeMs);
}
