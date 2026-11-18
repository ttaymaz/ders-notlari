using DiLab;

namespace DiLab.Testler;

// Testlerde gerçek dosya yerine bellekte duran depo
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

// Testlerde gerçek saat yerine hep aynı günü söyleyen saat
public sealed class SabitSaat(DateOnly gun) : TimeProvider
{
    public override DateTimeOffset GetUtcNow()
        => new(gun.ToDateTime(new TimeOnly(12, 0)), TimeSpan.Zero);
}
