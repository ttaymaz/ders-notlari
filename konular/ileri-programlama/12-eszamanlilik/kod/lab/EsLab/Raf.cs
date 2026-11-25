namespace EsLab;

// Ödünç defteri: ödünç verilen her kitap buraya yazılır. Gerçekte bir dosya
// ya da veritabanıdır, yani yazmak asenkrondur. Testlerde sahtesi verilir.
public interface IDefter
{
    Task YazAsync(string kitap, string uye, CancellationToken iptal = default);
}

// GÖREV 2: Bir kitabın rafı.
//
// OduncVerAsync aynı anda birçok üyeden çağrılacak. Rafta kopya varsa bir
// kopya düşülür, deftere yazılır ve true döner; kopya yoksa deftere
// yazılmaz ve false döner. Kural: verilen ödünç sayısı hiçbir zaman başlangıç
// kopya sayısını aşmaz ve deftere tam olarak verilen kadar kayıt düşer.
//
// "Kopya var mı?" ile "deftere yaz" arasında bir await var. lock içinde
// await yazılamaz (CS1996); asenkron kilit gerekiyor (02 dosyası).
public sealed class Raf
{
    private readonly IDefter _defter;

    public Raf(string kitap, int kopya, IDefter defter)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(kopya);
        ArgumentNullException.ThrowIfNull(defter);
        Kitap = kitap;
        Kopya = kopya;
        _defter = defter;
    }

    public string Kitap { get; }

    public int Kopya { get; private set; }

    public Task<bool> OduncVerAsync(string uye, CancellationToken iptal = default)
    {
        throw new NotImplementedException();
    }
}
