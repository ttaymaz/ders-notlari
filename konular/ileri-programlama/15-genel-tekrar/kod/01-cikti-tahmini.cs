// PROVA 1 — çıktı tahmini (dönemin ikinci yarısı).
//
// Yazılı sınavın ana soru tipi: "Bu kod ne yazdırır?" Her bölüm 9–14.
// haftalardan birinin bir fikrini sınar. Çalıştırmadan önce her bölümün
// çıktısını KÂĞIDA yazın; sonra çalıştırıp karşılaştırın. Yanıldığınız
// bölümün haftasına dönün.
//
// Dosya paketsizdir; her çalıştırmada aynı çıktıyı verir.
// Çalıştırmak için:  dotnet run 01-cikti-tahmini.cs

using System.Reflection;
using System.Threading.Channels;

Console.WriteLine("=== 1 === (9. hafta: catch, when, finally)");
foreach (int kod in new[] { 404, 500 })
{
    try
    {
        try
        {
            throw new InvalidOperationException(kod.ToString());
        }
        catch (InvalidOperationException e) when (e.Message == "404")
        {
            Console.Write("bulunamadı ");
        }
        finally
        {
            Console.Write("iç-finally ");
        }
    }
    catch (Exception)
    {
        Console.Write("dış-catch ");
    }
    Console.WriteLine();
}

Console.WriteLine("\n=== 2 === (9. hafta: using ve Dispose sırası)");
using (var a = new Kaynak("A"))
using (var b = new Kaynak("B"))
{
    Console.WriteLine("gövde");
}

Console.WriteLine("\n=== 3 === (10. hafta: tekil fabrika, kapanım)");
Fis? ornek = null;
Func<Fis> tekil = () => ornek ??= new Fis();
Func<Fis> gecici = () => new Fis();
Console.WriteLine($"{tekil().No} {tekil().No} {gecici().No} {tekil().No} {gecici().No}");

Console.WriteLine("\n=== 4 === (11. hafta: await nerede durur?)");
Console.Write("1 ");
Task t = Ornek.YazAsync();
Console.Write("3 ");
await t;
Console.WriteLine("5");

Console.WriteLine("\n=== 5 === (11. hafta: WhenAll sırası)");
int[] sonuc = await Task.WhenAll(Ornek.GecikAsync(3, 30), Ornek.GecikAsync(1, 10), Ornek.GecikAsync(2, 20));
Console.WriteLine($"  dizi: {string.Join(" ", sonuc)}");

Console.WriteLine("\n=== 6 === (12. hafta: kanal, kapasite 1)");
var kanal = Channel.CreateBounded<int>(1);
await kanal.Writer.WriteAsync(10);
Console.WriteLine($"ikinci yazma hemen bitti mi? {kanal.Writer.TryWrite(20)}");
kanal.Writer.Complete();
await foreach (int x in kanal.Reader.ReadAllAsync())
    Console.WriteLine($"okundu {x}");

Console.WriteLine("\n=== 7 === (13. hafta: tip ağacında yukarı)");
for (Type? tip = typeof(RafBulunamadi); tip is not null; tip = tip.BaseType)
    Console.Write($"{tip.Name} ");
Console.WriteLine();

Console.WriteLine("\n=== 8 === (13. hafta: öznitelikler)");
MethodInfo m = typeof(Ornek).GetMethod(nameof(Ornek.Raporla))!;
Console.WriteLine($"Etiket sayısı: {m.GetCustomAttributes<EtiketAttribute>().Count()}");
Console.WriteLine($"Etiketler: {string.Join(", ", m.GetCustomAttributes<EtiketAttribute>().Select(e => e.Ad).Order())}");

Console.WriteLine("\n=== 9 === (14. hafta: dizi aralığı ve Span)");
int[] d = [1, 2, 3, 4];
int[] kopya = d[1..3];
Span<int> pencere = d.AsSpan(1, 2);
kopya[0] = 20;
pencere[1] = 30;
Console.WriteLine(string.Join(" ", d));

Console.WriteLine("\n=== 10 === (14. hafta: List<T> kapasitesi)");
var liste = new List<int>();
for (int i = 0; i < 5; i++)
    liste.Add(i);
Console.WriteLine($"Count {liste.Count}, Capacity {liste.Capacity}");

class Kaynak : IDisposable
{
    private readonly string _ad;
    public Kaynak(string ad) { _ad = ad; Console.WriteLine($"aç {ad}"); }
    public void Dispose() => Console.WriteLine($"kapat {_ad}");
}

class Fis
{
    private static int _sayac;
    public int No { get; } = ++_sayac;
}

class RafBulunamadi() : KeyNotFoundException("raf yok");

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
class EtiketAttribute(string ad) : Attribute
{
    public string Ad { get; } = ad;
}

static class Ornek
{
    public static async Task YazAsync()
    {
        Console.Write("2 ");
        await Task.Delay(50);
        Console.Write("4 ");
    }

    public static async Task<int> GecikAsync(int deger, int ms)
    {
        await Task.Delay(ms * 5);
        Console.WriteLine($"  bitti {deger}");
        return deger;
    }

    [Etiket("rapor")]
    [Etiket("aylik")]
    public static void Raporla() { }
}
