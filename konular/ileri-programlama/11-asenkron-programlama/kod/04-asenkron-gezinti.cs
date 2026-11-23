// Taşıyıcı: graf yeniden, bu kez kenarlar uzaktan geliyor.
//
// Geçen hafta bağımlılık grafını derinlemesine gezdik; graf bellekteydi.
// Bu hafta bir kütüphane kataloğunda "bunu okuyanlar şunları da okudu"
// bağlantılarını GENİŞLİĞİNE gezeceğiz (BFS: önce komşular, sonra
// komşuların komşuları). Fark şu: bir kitabın bağlantılarını öğrenmek için
// uzak kataloğa sormak gerekiyor ve her soru bir gecikme sürüyor.
//
//   Huzur             → Tutunamayanlar, Saatleri Ayarlama Enstitüsü, Aylak Adam
//   Tutunamayanlar    → Tehlikeli Oyunlar, Huzur
//   Saatleri A. E.    → Beş Şehir, Huzur
//   Aylak Adam        → Anayurt Oteli
//
// Graf döngülü: Tutunamayanlar Huzur'a, Tehlikeli Oyunlar Tutunamayanlar'a
// geri bağlanıyor. "Görüldü" kümesi olmadan gezinti hiç bitmezdi.
//
// Aynı gezintiyi iki biçimde yazıyoruz: her soruyu sırayla beklemek, ve aynı
// düzeydeki soruları birlikte başlatıp birlikte beklemek. Kaç çağrı yapılır,
// en çok kaç çağrı aynı anda sürer, ne kadar zaman alır?
// Çalıştırmak için:  dotnet run 04-asenkron-gezinti.cs

using System.Diagnostics;

Console.WriteLine("=== 1 === (sıralı gezinti)");
await Calistir(Gezgin.SiraliAsync);

Console.WriteLine("\n=== 2 === (düzey düzey: Task.WhenAll)");
await Calistir(Gezgin.DuzeyDuzeyAsync);

Console.WriteLine("\n=== 3 === (aynı zaman aşımı: 5 gecikme)");
await ZamanAsimiyla("sıralı", Gezgin.SiraliAsync);
await ZamanAsimiyla("düzey düzey", Gezgin.DuzeyDuzeyAsync);

static async Task Calistir(Func<UzakKatalog, string, CancellationToken, Task<List<string>>> gez)
{
    var katalog = new UzakKatalog();
    var saat = Stopwatch.StartNew();
    List<string> sira = await gez(katalog, "Huzur", CancellationToken.None);
    Console.WriteLine(string.Join(" → ", sira));
    Console.WriteLine($"çağrı: {katalog.Cagri} · en çok aynı anda: {katalog.EnCokAyniAnda} · " +
                      $"süre ≈ {Math.Round(saat.Elapsed.TotalMilliseconds / UzakKatalog.GecikmeMs)} gecikme");
}

static async Task ZamanAsimiyla(string ad, Func<UzakKatalog, string, CancellationToken, Task<List<string>>> gez)
{
    var katalog = new UzakKatalog();
    using var zamanAsimi = new CancellationTokenSource(5 * UzakKatalog.GecikmeMs);
    try
    {
        List<string> sira = await gez(katalog, "Huzur", zamanAsimi.Token);
        Console.WriteLine($"{ad}: yetişti, {sira.Count} kitap");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine($"{ad}: yetişmedi");
    }
}

static class Gezgin
{
    public static async Task<List<string>> SiraliAsync(
        UzakKatalog katalog, string baslangic, CancellationToken iptal)
    {
        var goruldu = new HashSet<string> { baslangic };
        var kuyruk = new Queue<string>();
        kuyruk.Enqueue(baslangic);
        var sira = new List<string>();

        while (kuyruk.TryDequeue(out var baslik))
        {
            sira.Add(baslik);
            foreach (var komsu in await katalog.BaglantilarAsync(baslik, iptal))
                if (goruldu.Add(komsu))
                    kuyruk.Enqueue(komsu);
        }
        return sira;
    }

    public static async Task<List<string>> DuzeyDuzeyAsync(
        UzakKatalog katalog, string baslangic, CancellationToken iptal)
    {
        var goruldu = new HashSet<string> { baslangic };
        var duzey = new List<string> { baslangic };
        var sira = new List<string>();

        while (duzey.Count > 0)
        {
            sira.AddRange(duzey);

            // Önce bu düzeyin bütün sorularını başlat, sonra hepsini birlikte bekle
            var sorular = duzey.Select(b => katalog.BaglantilarAsync(b, iptal)).ToList();
            string[][] cevaplar = await Task.WhenAll(sorular);

            var sonraki = new List<string>();
            foreach (var cevap in cevaplar)          // WhenAll sırayı korur
                foreach (var komsu in cevap)
                    if (goruldu.Add(komsu))
                        sonraki.Add(komsu);
            duzey = sonraki;
        }
        return sira;
    }
}

// Uzak kataloğun taklidi: her soru bir gecikme sürer. Kaç soru sorulduğunu
// ve en çok kaç sorunun aynı anda sürdüğünü sayar. (lock: sayaçları aynı
// anda birden çok görev değiştirebilir; kilitler 12. haftanın konusu.)
class UzakKatalog
{
    public const int GecikmeMs = 200;

    private static readonly Dictionary<string, string[]> Graf = new()
    {
        ["Huzur"] = ["Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam"],
        ["Tutunamayanlar"] = ["Tehlikeli Oyunlar", "Huzur"],
        ["Saatleri Ayarlama Enstitüsü"] = ["Beş Şehir", "Huzur"],
        ["Aylak Adam"] = ["Anayurt Oteli"],
        ["Tehlikeli Oyunlar"] = ["Tutunamayanlar"],
        ["Beş Şehir"] = [],
        ["Anayurt Oteli"] = ["Aylak Adam"],
    };

    private readonly object _kilit = new();
    private int _suren;

    public int Cagri { get; private set; }
    public int EnCokAyniAnda { get; private set; }

    public async Task<string[]> BaglantilarAsync(string baslik, CancellationToken iptal)
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
            return Graf.GetValueOrDefault(baslik, []);
        }
        finally
        {
            lock (_kilit) _suren--;
        }
    }
}
