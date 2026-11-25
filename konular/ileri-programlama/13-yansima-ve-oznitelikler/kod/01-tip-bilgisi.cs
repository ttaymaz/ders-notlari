// Yansıma: program çalışırken kendi tiplerine bakabilir.
//
// Her nesne kendi tipini bilir (GetType) ve her tip bir Type nesnesiyle
// anlatılır: adı, üst tipi, özellikleri, metotları. Yansıma (reflection)
// bu bilgiyi çalışırken okumak, hatta onunla metot çağırmaktır.
//
// 3. bölüm haftanın taşıyıcısı: bir AĞAÇ. 9. haftanın istisna ailesini bu
// kez elle değil, her tipin BaseType'ından yukarı çıkarak kuruyoruz.
//
// 4. ve 5. bölüm iki sürpriz: yansıma private üyeleri de görebilir, ve
// yansımayla çağrılan metodun istisnası başka bir istisnanın içinde gelir.
// Çalıştırmak için:  dotnet run 01-tip-bilgisi.cs

using System.Reflection;

Console.WriteLine("=== 1 === (her nesne tipini bilir)");
object o = "Huzur";
Console.WriteLine($"o.GetType().Name: {o.GetType().Name}");
Console.WriteLine($"typeof(int).FullName: {typeof(int).FullName}");
Type liste = typeof(List<int>);
Console.WriteLine($"List<int>: {liste.Name}, jenerik mi? {liste.IsGenericType}, T = {liste.GetGenericArguments()[0].Name}");

Console.WriteLine("\n=== 2 === (üyeler)");
var kitap = new Kitap("Huzur", 431);
foreach (PropertyInfo ozellik in typeof(Kitap).GetProperties().OrderBy(p => p.MetadataToken))
    Console.WriteLine($"{ozellik.Name,-8} {ozellik.PropertyType.Name,-7} = {ozellik.GetValue(kitap)}");
var metotlar = typeof(Kitap)
    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    .Where(m => !m.IsSpecialName)                 // get_/set_ erişimcileri hariç
    .Select(m => m.Name)
    .Order(StringComparer.Ordinal);
Console.WriteLine($"metotlar: {string.Join(", ", metotlar)}");

Console.WriteLine("\n=== 3 === (taşıyıcı: tip ağacı)");
Type[] yapraklar =
[
    typeof(ArgumentNullException), typeof(ArgumentOutOfRangeException),
    typeof(InvalidOperationException), typeof(ObjectDisposedException),
    typeof(TaskCanceledException), typeof(KeyNotFoundException),
    typeof(FormatException), typeof(NotImplementedException),
    typeof(KitapBulunamadiException),
];
TipAgaci.Yazdir(TipAgaci.Kur(yapraklar, kok: typeof(Exception)));

Console.WriteLine("\n=== 4 === (private üyeye erişmek)");
var sayac = new OduncSayaci();
sayac.Artir();
sayac.Artir();
FieldInfo alan = typeof(OduncSayaci).GetField("_adet", BindingFlags.NonPublic | BindingFlags.Instance)!;
Console.WriteLine($"_adet = {alan.GetValue(sayac)}");
alan.SetValue(sayac, 100);
Console.WriteLine($"SetValue sonrası Adet = {sayac.Adet}");

Console.WriteLine("\n=== 5 === (yansımayla çağırmak ve istisna)");
MethodInfo artir = typeof(OduncSayaci).GetMethod(nameof(OduncSayaci.Artir))!;
artir.Invoke(sayac, null);
Console.WriteLine($"Invoke sonrası Adet = {sayac.Adet}");
MethodInfo sifirla = typeof(OduncSayaci).GetMethod(nameof(OduncSayaci.Sifirla))!;
try
{
    sifirla.Invoke(sayac, null);
}
catch (TargetInvocationException e)
{
    Console.WriteLine($"yakalanan: {e.GetType().Name}");
    Console.WriteLine($"içindeki: {e.InnerException!.GetType().Name}: {e.InnerException.Message}");
}

record Kitap(string Baslik, int Sayfa)
{
    public bool UzunMu() => Sayfa > 400;
    public string Ozet() => $"{Baslik} ({Sayfa} s.)";
}

class KitapBulunamadiException(string baslik) : KeyNotFoundException($"{baslik} bulunamadı");

class OduncSayaci
{
    private int _adet;
    public int Adet => _adet;
    public void Artir() => _adet++;
    public void Sifirla() => throw new InvalidOperationException("Sayaç sıfırlanamaz.");
}

static class TipAgaci
{
    // Her yapraktan BaseType ile köke kadar çık; geçtiğin her kenarı
    // "üst → alt" olarak kaydet. Sonuç: üst tipten alt tiplere bir ağaç.
    public static Dictionary<Type, SortedSet<Type>> Kur(IEnumerable<Type> yapraklar, Type kok)
    {
        var cocuklar = new Dictionary<Type, SortedSet<Type>>();
        foreach (Type yaprak in yapraklar)
        {
            for (Type? t = yaprak; t is not null && t != kok; t = t.BaseType)
            {
                Type ust = t.BaseType!;
                if (!cocuklar.TryGetValue(ust, out var kume))
                    cocuklar[ust] = kume = new SortedSet<Type>(Comparer<Type>.Create((a, b) => string.CompareOrdinal(a.Name, b.Name)));
                kume.Add(t);
            }
        }
        return cocuklar;
    }

    // Derinlemesine gezinti: önce düğüm, sonra çocukları bir girinti içeride
    public static void Yazdir(Dictionary<Type, SortedSet<Type>> cocuklar)
    {
        Type kok = cocuklar.Keys.Single(t => !cocuklar.Values.Any(k => k.Contains(t)));
        Gez(kok, 0);

        void Gez(Type tip, int derinlik)
        {
            Console.WriteLine(new string(' ', derinlik * 2) + tip.Name);
            if (cocuklar.TryGetValue(tip, out var altlar))
                foreach (Type alt in altlar)
                    Gez(alt, derinlik + 1);
        }
    }
}
