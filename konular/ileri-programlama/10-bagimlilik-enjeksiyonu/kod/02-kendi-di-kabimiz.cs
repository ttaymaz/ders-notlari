// Kendi DI kabımız: bir bağımlılık GRAFİNİ gezmek.
//
// Her servis bir düğüm; "şuna ihtiyacım var" bir kenar. Kap bir servisi
// kurmak için önce onun bağımlılıklarını, onlar için de kendi
// bağımlılıklarını kurar: grafı derinlemesine gezer. Kap her düğüme girerken
// bir satır yazıyor; girinti, grafın derinliği.
//
// Kayıtlar fabrika ile yapılıyor: "bu tip istenirse şu lambdayı çalıştır"
// (4. hafta: Func). Lambda kabı parametre alır ve kendi bağımlılıklarını
// ondan ister.
//
// İkinci bölümde grafta bir DÖNGÜ var: Fatura Hatirlatici'ya, Hatirlatici
// Fatura'ya ihtiyaç duyuyor. Kap bunu nasıl fark ediyor?
// Çalıştırmak için:  dotnet run 02-kendi-di-kabimiz.cs

var kap = new Kap();
kap.Kaydet<TimeProvider>(k => TimeProvider.System);
kap.Kaydet(k => new CezaKurali(GunlukUcret: 3, Tavan: 60));
kap.Kaydet(k => new GecikmeHesabi(k.Coz<TimeProvider>(), k.Coz<CezaKurali>()));
kap.Kaydet(k => new OduncDefteri());
kap.Kaydet(k => new IadeServisi(k.Coz<OduncDefteri>(), k.Coz<GecikmeHesabi>()));

Console.WriteLine("=== 1 === (bağımlılık grafı)");
var servis = kap.Coz<IadeServisi>();
Console.WriteLine($"hazır: {servis.GetType().Name}");

Console.WriteLine("\n=== 2 === (döngü)");
var donguluKap = new Kap();
donguluKap.Kaydet(k => new Fatura(k.Coz<Hatirlatici>()));
donguluKap.Kaydet(k => new Hatirlatici(k.Coz<Fatura>()));
try
{
    donguluKap.Coz<Fatura>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine("\n=== 3 === (kayıtsız tip)");
try
{
    kap.Coz<Fatura>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e.Message);
}

class Kap
{
    private readonly Dictionary<Type, Func<Kap, object>> _fabrikalar = new();

    // Şu an kurulmakta olan tipler: grafta "yolda olduğumuz" düğümler
    private readonly List<Type> _yol = new();

    public void Kaydet<T>(Func<Kap, T> fabrika) where T : class
        => _fabrikalar[typeof(T)] = k => fabrika(k);

    public T Coz<T>() where T : class
    {
        var tip = typeof(T);
        if (!_fabrikalar.TryGetValue(tip, out var fabrika))
            throw new InvalidOperationException($"{tip.Name} kayıtlı değil.");

        // Yolda olduğumuz bir düğüme yeniden geldiysek graf döngülüdür
        if (_yol.Contains(tip))
        {
            var dongu = _yol.SkipWhile(t => t != tip).Append(tip).Select(t => t.Name);
            throw new InvalidOperationException("Döngüsel bağımlılık: " + string.Join(" → ", dongu));
        }

        Console.WriteLine($"{new string(' ', _yol.Count * 2)}{tip.Name}");
        _yol.Add(tip);
        try
        {
            return (T)fabrika(this);
        }
        finally
        {
            _yol.RemoveAt(_yol.Count - 1);
        }
    }
}

// Servislerin içi bu örneğin konusu değil; kısalık için record olarak
// yazıldılar. Önemli olan kurucu parametreleri: grafın kenarları onlar.
record CezaKurali(int GunlukUcret, int Tavan);
record GecikmeHesabi(TimeProvider Saat, CezaKurali Kural);
record OduncDefteri;
record IadeServisi(OduncDefteri Defter, GecikmeHesabi Hesap);
record Fatura(Hatirlatici Hatirlatici);
record Hatirlatici(Fatura Fatura);
