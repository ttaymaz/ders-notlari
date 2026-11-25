namespace YansimaLab;

// HAZIR: taşıyıcı. Bir tipten köke (object) giden yol: tip ağacında bir
// yapraktan köke yürümek. Testleri baştan yeşildir.
public static class TipAgaci
{
    public static IReadOnlyList<string> KokeGidenYol(Type tip)
    {
        ArgumentNullException.ThrowIfNull(tip);

        var yol = new List<string>();
        for (Type? t = tip; t is not null; t = t.BaseType)
            yol.Add(t.Name);
        return yol;
    }
}
