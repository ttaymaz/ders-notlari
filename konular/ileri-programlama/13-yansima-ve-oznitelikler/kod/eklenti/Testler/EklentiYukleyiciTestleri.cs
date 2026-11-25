using Sozlesme;
using Uygulama;

namespace Testler;

// Yükleyiciyi sınamak için gerçek bir DLL gerekmez: test derlemesinin
// kendisi de bir derlemedir. Aşağıdaki sahte eklentiler bu derlemede durur.
public class EklentiYukleyiciTestleri
{
    [Fact]
    public void Kurulabilen_eklentileri_tam_ada_gore_sirali_bulur()
    {
        var eklentiler = EklentiYukleyici.Bul<IRaporEklentisi>(typeof(EklentiYukleyiciTestleri).Assembly);

        Assert.Equal(["Testler.ARaporu", "Testler.BRaporu"], eklentiler.Select(e => e.GetType().FullName));
    }

    [Fact]
    public void Soyut_parametreli_ve_public_olmayanlari_atlar()
    {
        var adlar = EklentiYukleyici.Bul<IRaporEklentisi>(typeof(EklentiYukleyiciTestleri).Assembly)
            .Select(e => e.GetType().Name)
            .ToList();

        Assert.DoesNotContain("SoyutRapor", adlar);
        Assert.DoesNotContain("ParametreliRapor", adlar);
        Assert.DoesNotContain("GizliRapor", adlar);
    }

    [Fact]
    public void Olmayan_klasorde_eklenti_yoktur()
    {
        string klasor = Path.Combine(Path.GetTempPath(), "olmayan-" + Guid.NewGuid().ToString("N"));

        Assert.Empty(EklentiYukleyici.KlasordenYukle<IRaporEklentisi>(klasor));
    }
}

public sealed class BRaporu : IRaporEklentisi
{
    public string Ad => "B";
    public string Uret(IReadOnlyList<Odunc> odunclar) => "b";
}

public sealed class ARaporu : IRaporEklentisi
{
    public string Ad => "A";
    public string Uret(IReadOnlyList<Odunc> odunclar) => "a";
}

public abstract class SoyutRapor : IRaporEklentisi
{
    public abstract string Ad { get; }
    public abstract string Uret(IReadOnlyList<Odunc> odunclar);
}

public sealed class ParametreliRapor(string ad) : IRaporEklentisi
{
    public string Ad => ad;
    public string Uret(IReadOnlyList<Odunc> odunclar) => ad;
}

internal sealed class GizliRapor : IRaporEklentisi
{
    public string Ad => "gizli";
    public string Uret(IReadOnlyList<Odunc> odunclar) => "gizli";
}
