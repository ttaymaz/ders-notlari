using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cekirdek;

public sealed record Odunc(string Baslik, string Uye, DateOnly SonTarih);

// appsettings.json'daki "Odunc" bölümü bu sınıfa bağlanır
public sealed class OduncAyarlari
{
    public int Gun { get; set; } = 14;
    public int EnFazlaKitap { get; set; } = 3;
    public int GunlukCeza { get; set; } = 3;
    public int CezaTavani { get; set; } = 60;
}

// Bütün bağımlılıklar kurucudan gelir. Servis hiçbirini kendisi KURMAZ:
// deponun dosya mı bellek mi, saatin gerçek mi sahte mi olduğunu bilmez.
// Bu yüzden testte her birinin yerine denetlenebilir bir sürüm verilebilir.
public sealed class OduncServisi(
    IDepo<Odunc> depo,
    TimeProvider saat,
    IOptions<OduncAyarlari> secenekler,
    ILogger<OduncServisi> gunluk)
{
    private OduncAyarlari Ayar => secenekler.Value;

    private DateOnly Bugun => DateOnly.FromDateTime(saat.GetUtcNow().UtcDateTime);

    public async Task<Sonuc<Odunc>> OduncVerAsync(string baslik, string uye, CancellationToken iptal = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baslik);
        ArgumentException.ThrowIfNullOrWhiteSpace(uye);

        var tumu = await depo.OkuAsync(iptal);
        if (tumu.Any(o => o.Baslik == baslik))
            return new Hatali<Odunc>("kitap ödünçte");
        if (tumu.Count(o => o.Uye == uye) >= Ayar.EnFazlaKitap)
            return new Hatali<Odunc>("üye sınırı doldu");

        var yeni = new Odunc(baslik, uye, Bugun.AddDays(Ayar.Gun));
        await depo.YazAsync([.. tumu, yeni], iptal);
        gunluk.LogInformation("{Uye} üyesi {Baslik} kitabını aldı; son tarih {SonTarih}",
            uye, baslik, yeni.SonTarih);
        return new Basarili<Odunc>(yeni);
    }

    public async Task<Sonuc<int>> IadeEtAsync(string baslik, CancellationToken iptal = default)
    {
        var tumu = await depo.OkuAsync(iptal);
        var odunc = tumu.FirstOrDefault(o => o.Baslik == baslik);
        if (odunc is null)
            return new Hatali<int>("kitap ödünçte değil");

        int gecikme = Bugun.DayNumber - odunc.SonTarih.DayNumber;
        int ceza = gecikme <= 0 ? 0 : Math.Min(gecikme * Ayar.GunlukCeza, Ayar.CezaTavani);

        await depo.YazAsync(tumu.Where(o => o != odunc).ToList(), iptal);
        if (ceza > 0)
            gunluk.LogWarning("{Baslik} {Gecikme} gün gecikti; ceza {Ceza} TL", baslik, gecikme, ceza);
        return new Basarili<int>(ceza);
    }

    public async Task<IReadOnlyList<Odunc>> GecikenlerAsync(CancellationToken iptal = default)
    {
        var tumu = await depo.OkuAsync(iptal);
        return tumu.Where(o => o.SonTarih < Bugun).OrderBy(o => o.SonTarih).ToList();
    }
}
