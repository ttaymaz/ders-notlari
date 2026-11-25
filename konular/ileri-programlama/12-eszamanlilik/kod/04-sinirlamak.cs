// Aynı anda kaç iş? SemaphoreSlim ve Parallel.ForEachAsync.
//
// Geçen hafta düzey düzey gezintide bir düzeydeki bütün soruları birlikte
// başlatmıştık. Düzeyde altı kitap varsa kataloğa altı soru birden gider;
// bin kitap varsa bin soru. Gerçek bir sunucu bunu ya reddeder ya da
// yavaşlar. Burada altı kitabın sorularını en çok İKİSİ aynı anda sürecek
// biçimde soruyoruz.
//
// SemaphoreSlim bir kilidin sayılı hâlidir: lock tek kişilik bir kapıysa,
// SemaphoreSlim(2) iki kişilik bir kapıdır.
//
// Süreler makineye göre birkaç milisaniye oynar; dosya süreyi bir
// gecikmenin (200 ms) katına yuvarlayarak yazıyor.
// Çalıştırmak için:  dotnet run 04-sinirlamak.cs

using System.Diagnostics;

string[] duzey = ["Huzur", "Aylak Adam", "Beş Şehir", "Tutunamayanlar", "Anayurt Oteli", "Tehlikeli Oyunlar"];

Console.WriteLine("=== 1 === (sınırsız: hepsi birlikte)");
var katalog = new UzakKatalog();
var saat = Stopwatch.StartNew();
await Task.WhenAll(duzey.Select(b => katalog.BaglantilarAsync(b)));
Console.WriteLine($"çağrı {katalog.Cagri} · en çok aynı anda {katalog.EnCokAyniAnda} · süre ≈ {UzakKatalog.Yuvarla(saat)} gecikme");

Console.WriteLine("\n=== 2 === (SemaphoreSlim(2))");
katalog = new UzakKatalog();
using var kapi = new SemaphoreSlim(2);
saat.Restart();
await Task.WhenAll(duzey.Select(async b =>
{
    await kapi.WaitAsync();
    try
    {
        return await katalog.BaglantilarAsync(b);
    }
    finally
    {
        kapi.Release();
    }
}));
Console.WriteLine($"çağrı {katalog.Cagri} · en çok aynı anda {katalog.EnCokAyniAnda} · süre ≈ {UzakKatalog.Yuvarla(saat)} gecikme");

Console.WriteLine("\n=== 3 === (Parallel.ForEachAsync, MaxDegreeOfParallelism = 2)");
katalog = new UzakKatalog();
saat.Restart();
await Parallel.ForEachAsync(duzey,
    new ParallelOptions { MaxDegreeOfParallelism = 2 },
    async (b, iptal) => await katalog.BaglantilarAsync(b, iptal));
Console.WriteLine($"çağrı {katalog.Cagri} · en çok aynı anda {katalog.EnCokAyniAnda} · süre ≈ {UzakKatalog.Yuvarla(saat)} gecikme");

// 11. haftanın sahte kataloğu: her soru bir gecikme sürer; kaç soru
// sorulduğunu ve en çok kaçının aynı anda sürdüğünü sayar.
class UzakKatalog
{
    public const int GecikmeMs = 200;

    private readonly object _kilit = new();
    private int _suren;

    public int Cagri { get; private set; }
    public int EnCokAyniAnda { get; private set; }

    public async Task<string[]> BaglantilarAsync(string baslik, CancellationToken iptal = default)
    {
        lock (_kilit)
        {
            Cagri++;
            _suren++;
            EnCokAyniAnda = Math.Max(EnCokAyniAnda, _suren);
        }
        try
        {
            await Task.Delay(GecikmeMs, iptal);
            return [];
        }
        finally
        {
            lock (_kilit) _suren--;
        }
    }

    public static long Yuvarla(Stopwatch saat)
        => (long)Math.Round(saat.Elapsed.TotalMilliseconds / GecikmeMs);
}
