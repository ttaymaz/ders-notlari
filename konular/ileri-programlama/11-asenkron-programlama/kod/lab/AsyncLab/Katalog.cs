namespace AsyncLab;

// Uzak bir kütüphane kataloğu: bir kitabın "bunu okuyanlar şunları da okudu"
// bağlantılarını döndürür. Her soru ağ üzerinden gider, yani G/Ç'ye bağlıdır.
// Testlerde bunun yerine sahte bir katalog verilir.
public interface IKatalog
{
    Task<IReadOnlyList<string>> BaglantilarAsync(string baslik, CancellationToken iptal = default);
}

// Katalog bir kitabın bağlantılarını getiremediğinde fırlatılır
// (sunucu hatası, bozuk kayıt). İptal bu değildir: iptal
// OperationCanceledException ile gelir.
public sealed class KatalogHatasi(string baslik)
    : Exception($"{baslik} için bağlantılar getirilemedi")
{
    public string Baslik { get; } = baslik;
}

// 4. görevin dönüş tipi: gezilen kitaplar ve bağlantıları getirilemeyenler
public sealed record GezintiSonucu(IReadOnlyList<string> Sira, IReadOnlyList<string> Hatalilar);
