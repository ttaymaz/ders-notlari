using Sozlesme;

namespace RaporEklentileri;

// Bu DLL uygulamayla birlikte derlenmez; uygulama onu çalışırken
// eklentiler/ klasöründe bulur ve yükler. Uygulama bu sınıfların adlarını
// hiçbir yerde yazmaz.

public sealed class GecikmeRaporu : IRaporEklentisi
{
    public string Ad => "Geciken iadeler";

    public string Uret(IReadOnlyList<Odunc> odunclar)
    {
        var gecikenler = odunclar
            .Where(o => o.GecikmeGun > 0)
            .OrderByDescending(o => o.GecikmeGun)
            .Select(o => $"{o.Uye}: {o.Baslik} ({o.GecikmeGun} gün)");
        return string.Join(Environment.NewLine, gecikenler);
    }
}

public sealed class PopulerKitapRaporu : IRaporEklentisi
{
    public string Ad => "En çok ödünç alınanlar";

    public string Uret(IReadOnlyList<Odunc> odunclar)
    {
        var populer = odunclar
            .GroupBy(o => o.Baslik)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key, StringComparer.Ordinal)
            .Select(g => $"{g.Key}: {g.Count()}");
        return string.Join(Environment.NewLine, populer);
    }
}

// Soyut sınıf: kurulamaz, yükleyici atlamalı
public abstract class TemelRapor : IRaporEklentisi
{
    public abstract string Ad { get; }

    public abstract string Uret(IReadOnlyList<Odunc> odunclar);
}

// Parametresiz kurucusu yok: yükleyici nasıl kuracağını bilemez, atlamalı
public sealed class EsikliRapor(int esik) : IRaporEklentisi
{
    public string Ad => $"{esik} günden fazla gecikenler";

    public string Uret(IReadOnlyList<Odunc> odunclar)
        => odunclar.Count(o => o.GecikmeGun > esik).ToString(System.Globalization.CultureInfo.InvariantCulture);
}
