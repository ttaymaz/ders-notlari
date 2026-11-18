using Cekirdek;

namespace Testler;

// Testlerde gerçek dosya yerine bellekte duran depo.
// Servis farkı bilmez: o yalnızca IDepo<T> arayüzünü görür.
public sealed class BellekDepo<T> : IDepo<T>
{
    private List<T> _ogeler = [];

    public Task<IReadOnlyList<T>> OkuAsync(CancellationToken iptal = default)
        => Task.FromResult<IReadOnlyList<T>>(_ogeler.ToList());

    public Task YazAsync(IReadOnlyList<T> ogeler, CancellationToken iptal = default)
    {
        _ogeler = [.. ogeler];
        return Task.CompletedTask;
    }
}

// Testlerde gerçek saat yerine elle ilerletilen saat
public sealed class SahteSaat(DateOnly baslangic) : TimeProvider
{
    public DateOnly Bugun { get; private set; } = baslangic;

    public void Ilerle(int gun) => Bugun = Bugun.AddDays(gun);

    public override DateTimeOffset GetUtcNow()
        => new(Bugun.ToDateTime(new TimeOnly(12, 0)), TimeSpan.Zero);
}
