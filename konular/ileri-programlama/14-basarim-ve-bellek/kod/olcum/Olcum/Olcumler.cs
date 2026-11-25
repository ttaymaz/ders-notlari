using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Olcum;

// 1. soru: metni nasıl birleştirmeli? Parça sayısı küçükken fark var mı?
[MemoryDiagnoser]
public class Birlestirme
{
    [Params(10, 1000)]
    public int Parca { get; set; }

    [Benchmark(Baseline = true)]
    public string Arti()
    {
        string s = "";
        for (int i = 0; i < Parca; i++)
            s += "ab";
        return s;
    }

    [Benchmark]
    public string Yapici()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < Parca; i++)
            sb.Append("ab");
        return sb.ToString();
    }

    [Benchmark]
    public string Tekrar() => string.Concat(Enumerable.Repeat("ab", Parca));
}

// 2. soru: "var mı?" sorusunda List ile HashSet arasındaki fark ne kadar?
// En kötü durum: aranan eleman yok, List baştan sona bakar.
[MemoryDiagnoser]
public class Arama
{
    private List<int> _liste = [];
    private HashSet<int> _kume = [];

    [Params(10, 10_000)]
    public int Eleman { get; set; }

    [GlobalSetup]
    public void Hazirla()
    {
        _liste = Enumerable.Range(0, Eleman).ToList();
        _kume = [.. _liste];
    }

    [Benchmark(Baseline = true)]
    public bool ListeIcinde() => _liste.Contains(-1);

    [Benchmark]
    public bool KumeIcinde() => _kume.Contains(-1);
}

// 3. soru (13. hafta): yansımayla çağırmak ne kadar yavaş? Bir kez bulunan
// metodu delegeye çevirmek farkı kapatır mı?
//
// İki tuzak, ikisi de bu sınıf yazılırken yaşandı:
// 1) Ikiye(21) yazılınca derleyici sonucu derlerken 42 olarak hesapladı;
//    "doğrudan" 0,004 ns çıktı. Çare: girdi bir alanda (_girdi).
// 2) Girdi alana taşınınca da 0,01 ns çıktı: derleyici iki satırlık metodu
//    çağıran yere kopyaladı (satır içine alma, inlining), ortada çağrı
//    kalmadı. Çare: [MethodImpl(NoInlining)]: gerçek bir çağrı ölçülsün.
[MemoryDiagnoser]
public class Cagri
{
    private readonly MethodInfo _metot = typeof(Cagri).GetMethod(nameof(Ikiye))!;
    private readonly object[] _arguman = [21];
    private Func<int, int> _delege = null!;
    private int _girdi = 21;

    [GlobalSetup]
    public void Hazirla() => _delege = _metot.CreateDelegate<Func<int, int>>();

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Ikiye(int x) => x * 2;

    [Benchmark(Baseline = true)]
    public int Dogrudan() => Ikiye(_girdi);

    [Benchmark]
    public object? Yansima() => _metot.Invoke(null, _arguman);

    [Benchmark]
    public int Delege() => _delege(_girdi);
}
