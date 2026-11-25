// Taşıyıcı: dönemin yapıları, bu kez ölçerek.
//
// 4. haftada kendi bağlı listemizi yazmıştık; .NET'teki hazırı List<T>'ydi.
// Hangisi ne kadar yer tutar? 1. bölüm 1000 sayıyı üç yapıda saklıyor ve
// ayrılan baytı ölçüyor.
//
// 2. bölüm koleksiyon seçiminin asıl sorusu: "bu eleman var mı?" List<T>
// bunu baştan sona bakarak cevaplar, HashSet<T> özet koduyla (2. hafta)
// doğrudan gider. 10 000 elemanda 10 000 soru.
//
// Süreler her makinede farklıdır; dosya yalnızca büyük farkın cevabını
// yazıyor. İlk taslağında bu dosya iki döngüyü ısınmasız, art arda
// ölçüyordu ve sonucu ölçüm SIRASINA bağlıydı: önce ölçülen, JIT'in
// bedelini ödüyordu. Şimdi 01'deki gibi ölçülüyor.
// Çalıştırmak için:  dotnet run -c Release 04-koleksiyon-secimi.cs

using System.Diagnostics;

Console.WriteLine("=== 1 === (1000 sayı, üç yapı)");
long bagli = Olcum.Ayrilan(() =>
{
    var liste = new BagliListe();
    for (int i = 0; i < 1000; i++)
        liste.BasaEkle(i);
});
long dinamik = Olcum.Ayrilan(() =>
{
    var liste = new List<int>(1000);
    for (int i = 0; i < 1000; i++)
        liste.Add(i);
});
long dizi = Olcum.Ayrilan(() =>
{
    var d = new int[1000];
    for (int i = 0; i < 1000; i++)
        d[i] = i;
});
Console.WriteLine($"bağlı liste      : {bagli,6:N0} bayt  (düğüm başına ≈ {bagli / 1000})");
Console.WriteLine($"List<int>(1000)  : {dinamik,6:N0} bayt");
Console.WriteLine($"int[1000]        : {dizi,6:N0} bayt");

Console.WriteLine("\n=== 2 === (10 000 elemanda 10 000 kez \"var mı?\")");
var sayilar = Enumerable.Range(0, 10_000).Select(i => i * 7).ToList();
var kume = new HashSet<int>(sayilar);
int[] sorular = Enumerable.Range(0, 10_000).Select(i => i * 5).ToArray();

Console.WriteLine($"bulunan: List {Arama.Say(sayilar, sorular)}, HashSet {Arama.Say(kume, sorular)}");
double listeNs = Olcum.Sure(() => Arama.Say(sayilar, sorular));
double kumeNs = Olcum.Sure(() => Arama.Say(kume, sorular));
Console.WriteLine($"HashSet en az 20 kat hızlı mı? {listeNs >= 20 * kumeNs}");

// 4. haftanın bağlı listesi, yalnızca başa ekleme
class BagliListe
{
    private Dugum? _bas;
    public int Sayi { get; private set; }

    public void BasaEkle(int deger)
    {
        _bas = new Dugum(deger, _bas);
        Sayi++;
    }

    private sealed class Dugum(int deger, Dugum? sonraki)
    {
        public int Deger { get; } = deger;
        public Dugum? Sonraki { get; } = sonraki;
    }
}

static class Arama
{
    // ICollection<int>: List de HashSet de aynı metotla sorulsun
    public static int Say(ICollection<int> koleksiyon, int[] sorular)
    {
        int bulunan = 0;
        foreach (int s in sorular)
            if (koleksiyon.Contains(s))
                bulunan++;
        return bulunan;
    }
}

static class Olcum
{
    // Isınma turları + birçok tekrarın ortalaması (01 dosyası). İlk ölçülen
    // iş JIT'in bedelini ödemesin diye ikisi de önce birkaç kez çalışır.
    public static double Sure(Action govde, int tekrar = 20)
    {
        for (int i = 0; i < 5; i++) govde();
        var saat = Stopwatch.StartNew();
        for (int i = 0; i < tekrar; i++) govde();
        return saat.Elapsed.TotalNanoseconds / tekrar;
    }

    public static long Ayrilan(Action govde)
    {
        govde();                                          // ısınma
        long once = GC.GetAllocatedBytesForCurrentThread();
        govde();
        return GC.GetAllocatedBytesForCurrentThread() - once;
    }
}
