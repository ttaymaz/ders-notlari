// Span<T>: kopyalamadan bir parçaya bakmak.
//
// "TR-813-07" gibi bir raf kodunu parçalarına ayırmak için Split ya da
// Substring yazarız. İkisi de YENİ metinler üretir: her parça heap'te ayrı
// bir string. Span<T> ve ReadOnlySpan<T> ise var olan bir belleğin bir
// parçasına bakan bir "pencere"dir: kopyalamaz, ayırmaz.
//
// 1. bölüm: aynı ayrıştırma iki yolla; kaç bayt ayrılıyor?
// 2. bölüm: pencere kopya değildir: Span üzerinden yazılan dizide görünür.
// 3. bölüm: stackalloc: küçük bir tamponu heap yerine stack'te açmak.
// Çalıştırmak için:  dotnet run 03-span.cs

string kod = "TR-813-07";

Console.WriteLine("=== 1 === (raf kodunu ayrıştırmak)");
var (b1, r1) = RafKodu.SplitIle(kod);
var (b2, r2) = RafKodu.SpanIle(kod);
Console.WriteLine($"Split ile: bölüm {b1}, raf {r1}");
Console.WriteLine($"Span ile : bölüm {b2}, raf {r2}");
long split = Olcum.Ayrilan(() => RafKodu.SplitIle(kod));
long span = Olcum.Ayrilan(() => RafKodu.SpanIle(kod));
Console.WriteLine($"Split ayırıyor mu? {split > 0}");
Console.WriteLine($"Span ile ayrılan: {span} bayt");

Console.WriteLine("\n=== 2 === (pencere kopya değildir)");
int[] puanlar = [10, 20, 30, 40, 50];
Span<int> orta = puanlar.AsSpan(1, 3);   // 20, 30, 40
orta[0] = 99;
orta.Reverse();
Console.WriteLine($"orta    : {string.Join(", ", orta.ToArray())}");
Console.WriteLine($"puanlar : {string.Join(", ", puanlar)}");

Console.WriteLine("\n=== 3 === (stackalloc)");
long yigin = Olcum.Ayrilan(() =>
{
    Span<int> tampon = stackalloc int[16];
    for (int i = 0; i < tampon.Length; i++)
        tampon[i] = i * i;
    _ = tampon[15];
});
long heap = Olcum.Ayrilan(() =>
{
    int[] tampon = new int[16];
    for (int i = 0; i < tampon.Length; i++)
        tampon[i] = i * i;
    _ = tampon[15];
});
Console.WriteLine($"stackalloc: {yigin} bayt · new int[16]: {heap} bayt");

static class RafKodu
{
    public static (int Bolum, int Raf) SplitIle(string kod)
    {
        string[] parcalar = kod.Split('-');               // yeni dizi + üç yeni metin
        return (int.Parse(parcalar[1]), int.Parse(parcalar[2]));
    }

    public static (int Bolum, int Raf) SpanIle(string kod)
    {
        ReadOnlySpan<char> s = kod;                       // kopya yok
        return (int.Parse(s.Slice(3, 3)), int.Parse(s.Slice(7, 2)));
    }
}

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
