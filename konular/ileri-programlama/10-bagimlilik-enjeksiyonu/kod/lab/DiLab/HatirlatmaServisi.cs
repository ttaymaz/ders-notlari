using Microsoft.Extensions.Options;

namespace DiLab;

public sealed record Odunc(string Baslik, string Uye, DateOnly SonTarih);

public interface IDepo<T>
{
    Task<IReadOnlyList<T>> OkuAsync(CancellationToken iptal = default);

    Task YazAsync(IReadOnlyList<T> ogeler, CancellationToken iptal = default);
}

public sealed class HatirlatmaAyarlari
{
    // Son tarihine en fazla bu kadar gün kalan ödünçler için hatırlatma yapılır
    public int KacGunKala { get; set; } = 3;
}

// GÖREV 4 — Bütün bağımlılıklar kurucudan geliyor; servisin kendisi hiçbir
// şey kurmuyor. Testler bunların yerine bellekte bir depo, sahte bir saat ve
// elle yazılmış bir ayar veriyor (DiLab.Testler/HatirlatmaServisiTestleri.cs).
public sealed class HatirlatmaServisi(
    IDepo<Odunc> depo,
    TimeProvider saat,
    IOptions<HatirlatmaAyarlari> secenekler)
{
    // Hazır yardımcılar: ödünçler depodan, bugün saatten, gün sayısı ayardan
    private IDepo<Odunc> Depo => depo;

    private DateOnly Bugun => DateOnly.FromDateTime(saat.GetUtcNow().UtcDateTime);

    private int KacGunKala => secenekler.Value.KacGunKala;

    // Son tarihine 0 ile KacGunKala gün (ikisi de dahil) kalan ödünçler için
    // birer satır döndürün:
    //
    //     "Ece: Huzur (2 gün)"
    //
    // Gecikmiş (son tarihi geçmiş) ödünçler listeye GİRMEZ.
    // Sıra: kalan güne göre artan; eşitse başlığa göre (StringComparer.Ordinal).
    // Kalan gün: o.SonTarih.DayNumber - Bugun.DayNumber
    // Ödünçler: await Depo.OkuAsync(iptal)
    public Task<IReadOnlyList<string>> HatirlatmalarAsync(CancellationToken iptal = default)
    {
        throw new NotImplementedException();
    }
}
