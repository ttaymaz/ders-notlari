using AsyncLab;

namespace AsyncLab.Testler;

// Testlerin kataloğu: gerçek ağ yok. Her soru 10 ms bekler; bu, sorunun
// gerçekten asenkron bitmesini ve aynı anda başlatılan soruların
// birbiriyle örtüşmesini sağlar.
//
// Katalog kendisine ne olduğunu kaydeder: kaç soru soruldu, hangi
// kitaplar, en çok kaç soru aynı anda sürdü, token'sız gelen soru oldu mu.
public sealed class SahteKatalog(Dictionary<string, string[]> graf) : IKatalog
{
    private readonly object _kilit = new();
    private int _suren;

    public int Cagri { get; private set; }
    public int EnCokAyniAnda { get; private set; }
    public int TokensizCagri { get; private set; }
    public List<string> Sorulanlar { get; } = [];

    // Bu kitaplar sorulunca KatalogHatasi fırlatılır
    public HashSet<string> Hatali { get; } = [];

    // Verilirse: bu kadar soru başarıyla bittikten sonra kaynak iptal edilir
    public (CancellationTokenSource Kaynak, int Sonra)? BitinceIptalEt { get; set; }

    // Verilirse: bu kitap sorulduğu anda kaynak iptal edilir; o anda süren
    // bütün sorular OperationCanceledException ile biter
    public (CancellationTokenSource Kaynak, string Baslik)? SoruluncaIptalEt { get; set; }

    public async Task<IReadOnlyList<string>> BaglantilarAsync(string baslik, CancellationToken iptal = default)
    {
        lock (_kilit)
        {
            Cagri++;
            Sorulanlar.Add(baslik);
            if (!iptal.CanBeCanceled)
                TokensizCagri++;
            _suren++;
            EnCokAyniAnda = Math.Max(EnCokAyniAnda, _suren);
        }
        if (SoruluncaIptalEt is { } s && s.Baslik == baslik)
            s.Kaynak.Cancel();
        try
        {
            await Task.Delay(10);
            iptal.ThrowIfCancellationRequested();
            if (Hatali.Contains(baslik))
                throw new KatalogHatasi(baslik);

            if (BitinceIptalEt is { } ip && Cagri == ip.Sonra)
                ip.Kaynak.Cancel();
            return graf.GetValueOrDefault(baslik, []);
        }
        finally
        {
            lock (_kilit) _suren--;
        }
    }

    // Derste kullanılan graf: 7 kitap, 3 düzey, iki döngü
    public static Dictionary<string, string[]> OrnekGraf() => new()
    {
        ["Huzur"] = ["Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam"],
        ["Tutunamayanlar"] = ["Tehlikeli Oyunlar", "Huzur"],
        ["Saatleri Ayarlama Enstitüsü"] = ["Beş Şehir", "Huzur"],
        ["Aylak Adam"] = ["Anayurt Oteli"],
        ["Tehlikeli Oyunlar"] = ["Tutunamayanlar"],
        ["Beş Şehir"] = [],
        ["Anayurt Oteli"] = ["Aylak Adam"],
    };

    public static readonly string[] OrnekSira =
    [
        "Huzur", "Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam",
        "Tehlikeli Oyunlar", "Beş Şehir", "Anayurt Oteli",
    ];
}
