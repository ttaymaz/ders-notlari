#:property PublishAot=false

// Kendi test koşucumuz: xUnit testlerinizi nasıl buluyor?
//
// xUnit sizin test sınıflarınızı derlenirken tanımıyor. Çalışırken test
// derlemesini (assembly) açıyor, içindeki her tipe ve her metoda bakıyor,
// [Fact] etiketi taşıyan metotları buluyor, her biri için sınıfın YENİ bir
// nesnesini kuruyor ve metodu çağırıyor. Metot istisna fırlatırsa test
// kalmış sayılıyor.
//
// Aşağıdaki koşucu aynı işi 30 satırda yapıyor. Etiketimizin adı [Sinama].
//
// İlk satır neden var? `dotnet run dosya.cs` tek dosyalık programı önceden
// derlemeye (AOT) hazır kabul eder ve kullanılmayan kodu kırpabileceğini
// varsayar. Bir derlemenin BÜTÜN tiplerini tarayan yansıma kodu buna
// uymaz; satır olmadan derleyici IL2026, IL2070, IL2077 uyarıları verir
// (04 dosyası bunun çaresini anlatıyor).
// Çalıştırmak için:  dotnet run 03-test-kosucu.cs

using System.Reflection;

var sonuclar = TestKosucu.Kos(Assembly.GetExecutingAssembly());
foreach (var (ad, gecti, mesaj) in sonuclar)
    Console.WriteLine(gecti ? $"[GEÇTİ] {ad}" : $"[KALDI] {ad}: {mesaj}");
Console.WriteLine($"toplam {sonuclar.Count}, geçen {sonuclar.Count(s => s.Gecti)}");

[AttributeUsage(AttributeTargets.Method)]
class SinamaAttribute : Attribute;

static class TestKosucu
{
    public static List<(string Ad, bool Gecti, string Mesaj)> Kos(Assembly derleme)
    {
        var sonuclar = new List<(string, bool, string)>();

        var sinamalar = derleme.GetTypes()
            .SelectMany(t => t.GetMethods(), (tip, metot) => (Tip: tip, Metot: metot))
            .Where(x => x.Metot.GetCustomAttribute<SinamaAttribute>() is not null)
            .OrderBy(x => x.Tip.Name, StringComparer.Ordinal)
            .ThenBy(x => x.Metot.Name, StringComparer.Ordinal);

        foreach (var (tip, metot) in sinamalar)
        {
            string ad = $"{tip.Name}.{metot.Name}";
            object nesne = Activator.CreateInstance(tip)!;   // her sınama için YENİ nesne
            try
            {
                metot.Invoke(nesne, null);
                sonuclar.Add((ad, true, ""));
            }
            catch (TargetInvocationException e)                // asıl istisna içeride
            {
                sonuclar.Add((ad, false, e.InnerException!.Message));
            }
        }
        return sonuclar;
    }
}

static class Dogrula
{
    public static void Esit<T>(T beklenen, T gelen)
    {
        if (!EqualityComparer<T>.Default.Equals(beklenen, gelen))
            throw new InvalidOperationException($"beklenen {beklenen}, gelen {gelen}");
    }
}

class CezaSinamalari
{
    private readonly List<string> _kayit = [];   // her sınamada boş başlar mı?

    static int Ceza(int gun) => Math.Min(gun * 3, 60);

    [Sinama]
    public void Bes_gun_on_bes_lira() => Dogrula.Esit(15, Ceza(5));

    [Sinama]
    public void Tavan_altmis_lira() => Dogrula.Esit(60, Ceza(86));

    [Sinama]
    public void Bir_gun_dort_lira() => Dogrula.Esit(4, Ceza(1));   // kasıtlı yanlış

    [Sinama]
    public void Kayit_her_sinamada_bostur()
    {
        _kayit.Add("ilk");
        Dogrula.Esit(1, _kayit.Count);
    }

    [Sinama]
    public void Kayit_her_sinamada_bostur_2()
    {
        _kayit.Add("ikinci");
        Dogrula.Esit(1, _kayit.Count);
    }

    public void Etiketsiz_metot() => throw new Exception("bu hiç çalışmamalı");
}
