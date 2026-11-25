// Bellek ayırma: kaç bayt?
//
// Süre makineden makineye, çalıştırmadan çalıştırmaya değişir. Bir kod
// parçasının heap'te kaç bayt ayırdığı ise değişmez: aynı .NET sürümünde
// her seferinde aynı sayıdır. Bu dosya süreyi değil, ayrılan baytı ölçüyor:
// GC.GetAllocatedBytesForCurrentThread().
//
// Her ölçümden önce iş bir kez "ısınma" için çalıştırılıyor: ilk çağrıda
// JIT derleyicisinin kendi ayırdıkları sonuca karışmasın.
//
// 1. bölüm: metin birleştirmek. 2. bölüm: kutulama (2. hafta).
// 3. bölüm: class ve struct dizisi (2. hafta). 4. bölüm: List<T> nasıl büyür?
// Çalıştırmak için:  dotnet run 02-bellek-ayirma.cs

using System.Text;

Console.WriteLine("=== 1 === (1000 parçayı birleştirmek)");
long arti = Olcum.Ayrilan(() =>
{
    string s = "";
    for (int i = 0; i < 1000; i++)
        s += "ab";
});
long yapici = Olcum.Ayrilan(() =>
{
    var sb = new StringBuilder();
    for (int i = 0; i < 1000; i++)
        sb.Append("ab");
    _ = sb.ToString();
});
Console.WriteLine($"+=            : {arti,9:N0} bayt");
Console.WriteLine($"StringBuilder : {yapici,9:N0} bayt");
Console.WriteLine($"+= en az 100 kat fazla mı? {arti >= 100 * yapici}");

Console.WriteLine("\n=== 2 === (1000 int: List<object> ve List<int>)");
var nesneler = new List<object>(1000);
var sayilar = new List<int>(1000);
long kutulu = Olcum.Ayrilan(() => { nesneler.Clear(); for (int i = 0; i < 1000; i++) nesneler.Add(i + 1000); });
long kutusuz = Olcum.Ayrilan(() => { sayilar.Clear(); for (int i = 0; i < 1000; i++) sayilar.Add(i + 1000); });
Console.WriteLine($"List<object> : {kutulu,7:N0} bayt  (eleman başına {kutulu / 1000})");
Console.WriteLine($"List<int>    : {kutusuz,7:N0} bayt");

Console.WriteLine("\n=== 3 === (1000 nokta: class dizisi ve struct dizisi)");
long sinif = Olcum.Ayrilan(() =>
{
    var dizi = new NoktaSinif[1000];
    for (int i = 0; i < dizi.Length; i++)
        dizi[i] = new NoktaSinif(i, i);
});
long yapi = Olcum.Ayrilan(() =>
{
    var dizi = new NoktaYapi[1000];
    for (int i = 0; i < dizi.Length; i++)
        dizi[i] = new NoktaYapi(i, i);
});
Console.WriteLine($"class : {sinif,6:N0} bayt");
Console.WriteLine($"struct: {yapi,6:N0} bayt");

Console.WriteLine("\n=== 4 === (List<T> nasıl büyür?)");
var liste = new List<int>();
var kapasiteler = new List<int> { liste.Capacity };
for (int i = 0; i < 100; i++)
{
    liste.Add(i);
    if (liste.Capacity != kapasiteler[^1])
        kapasiteler.Add(liste.Capacity);
}
Console.WriteLine($"kapasite: {string.Join(" → ", kapasiteler)}");
long buyuyerek = Olcum.Ayrilan(() => { var l = new List<int>(); for (int i = 0; i < 1000; i++) l.Add(i); });
long bastan = Olcum.Ayrilan(() => { var l = new List<int>(1000); for (int i = 0; i < 1000; i++) l.Add(i); });
Console.WriteLine($"büyüyerek          : {buyuyerek,6:N0} bayt");
Console.WriteLine($"kapasite baştan 1000: {bastan,6:N0} bayt");

static class Olcum
{
    public static long Ayrilan(Action govde)
    {
        govde();                                          // ısınma
        long once = GC.GetAllocatedBytesForCurrentThread();
        govde();
        return GC.GetAllocatedBytesForCurrentThread() - once;
    }
}

class NoktaSinif(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}

readonly record struct NoktaYapi(int X, int Y);
