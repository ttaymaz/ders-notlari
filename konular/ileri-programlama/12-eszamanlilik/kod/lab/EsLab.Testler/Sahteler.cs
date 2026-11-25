using EsLab;

namespace EsLab.Testler;

// Testlerin sahte bağımlılıkları. Bu dosyayı DEĞİŞTİRMEYİN.

// Her yazışta 1 ms bekleyen defter: "kopya var mı?" ile "yaz" arasına
// gerçek bir bekleme koyar, böylece korumasız bir raf yarışa girer.
public sealed class SahteDefter : IDefter
{
    private readonly object _kilit = new();
    private readonly List<(string Kitap, string Uye)> _kayitlar = [];

    public IReadOnlyList<(string Kitap, string Uye)> Kayitlar
    {
        get { lock (_kilit) return _kayitlar.ToList(); }
    }

    public async Task YazAsync(string kitap, string uye, CancellationToken iptal = default)
    {
        await Task.Delay(1, iptal);
        lock (_kilit)
            _kayitlar.Add((kitap, uye));
    }
}

// Geçen haftanın sahte kataloğu: her soru 10 ms sürer; kaç soru sorulduğunu,
// en çok kaçının aynı anda sürdüğünü ve token'sız soru gelip gelmediğini sayar.
public sealed class SahteKatalog(Dictionary<string, string[]> graf) : IKatalog
{
    private readonly object _kilit = new();
    private int _suren;

    public int Cagri { get; private set; }
    public int EnCokAyniAnda { get; private set; }
    public int TokensizCagri { get; private set; }

    public async Task<IReadOnlyList<string>> BaglantilarAsync(string baslik, CancellationToken iptal = default)
    {
        lock (_kilit)
        {
            Cagri++;
            if (!iptal.CanBeCanceled)
                TokensizCagri++;
            _suren++;
            EnCokAyniAnda = Math.Max(EnCokAyniAnda, _suren);
        }
        try
        {
            await Task.Delay(10, iptal);
            return graf.GetValueOrDefault(baslik, []);
        }
        finally
        {
            lock (_kilit) _suren--;
        }
    }

    // 1. düzey: Huzur · 2. düzey: üç kitap · 3. düzey: dört kitap
    public static Dictionary<string, string[]> OrnekGraf() => new()
    {
        ["Huzur"] = ["Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam"],
        ["Tutunamayanlar"] = ["Tehlikeli Oyunlar", "Huzur"],
        ["Saatleri Ayarlama Enstitüsü"] = ["Beş Şehir", "Mahur Beste", "Huzur"],
        ["Aylak Adam"] = ["Anayurt Oteli"],
        ["Tehlikeli Oyunlar"] = ["Tutunamayanlar"],
    };

    public static readonly string[] OrnekSira =
    [
        "Huzur", "Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam",
        "Tehlikeli Oyunlar", "Beş Şehir", "Mahur Beste", "Anayurt Oteli",
    ];
}
