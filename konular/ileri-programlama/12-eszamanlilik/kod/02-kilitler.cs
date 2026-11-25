// Kilitler: neyi korur, nasıl kilitlenir, await ile nasıl kullanılır?
//
// 1. bölüm: kilit bir NESNEYİ değil bir KURALI korur. Rafla defter
//    birlikte tutarlı kalmalı: rafta eksilen kopya deftere yazılmalı.
// 2. bölüm: kilitlenme (deadlock). İki görev iki kilidi TERS sırayla
//    istiyor. Gerçek programda ikisi de sonsuza dek bekler; burada
//    Monitor.TryEnter'a bir süre veriyoruz ki program dursun ve ne olduğunu
//    yazabilsin.
// 3. bölüm: lock içinde await yazılamaz (derleyici CS1996 verir). Asenkron
//    kodda kilidin karşılığı SemaphoreSlim(1, 1).
// Çalıştırmak için:  dotnet run 02-kilitler.cs

Console.WriteLine("=== 1 === (kilit bir kuralı korur)");
var kutuphane = new Kutuphane(kopya: 50);
await Task.WhenAll(Enumerable.Range(1, 100)
    .Select(no => Task.Run(() => kutuphane.OduncVer($"üye{no}"))));
Console.WriteLine($"rafta {kutuphane.Raf}, defterde {kutuphane.Defter.Count}, toplam {kutuphane.Raf + kutuphane.Defter.Count}");

Console.WriteLine("\n=== 2 === (kilitlenme: iki kilit, ters sıra)");
object rafKilidi = new(), defterKilidi = new();
using var ikisiDeTuttu = new Barrier(2);

Task<string> a = Task.Run(() => Ornek.IkiKilit("A", rafKilidi, defterKilidi, ikisiDeTuttu));
Task<string> b = Task.Run(() => Ornek.IkiKilit("B", defterKilidi, rafKilidi, ikisiDeTuttu));
foreach (string satir in await Task.WhenAll(a, b))
    Console.WriteLine(satir);

Console.WriteLine("\n=== 3 === (await ile kilit: SemaphoreSlim)");
var sayac = new AsenkronSayac();
await Task.WhenAll(Enumerable.Range(0, 200).Select(_ => sayac.ArtirAsync()));
Console.WriteLine($"200 artırma, sonuç {sayac.Deger}");

static class Ornek
{
    public static string IkiKilit(string ad, object ilk, object ikinci, Barrier ikisiDeTuttu)
    {
        lock (ilk)
        {
            ikisiDeTuttu.SignalAndWait();   // ikisi de ilk kilidini aldı
            bool aldi = Monitor.TryEnter(ikinci, TimeSpan.FromMilliseconds(300));
            if (aldi)
                Monitor.Exit(ikinci);
            ikisiDeTuttu.SignalAndWait();   // ikisi de sonucu öğrenmeden ilk kilidi bırakma
            return aldi
                ? $"{ad}: iki kilidi de aldım"
                : $"{ad}: ilk kilidi tutuyorum, ikinciyi 300 ms bekledim, alamadım";
        }
    }
}

class Kutuphane(int kopya)
{
    private readonly object _kilit = new();
    public int Raf { get; private set; } = kopya;
    public List<string> Defter { get; } = [];

    public bool OduncVer(string uye)
    {
        lock (_kilit)
        {
            if (Raf == 0)
                return false;
            Raf--;
            Defter.Add(uye);   // List<T> de aynı anda yazılmaya dayanıklı değil
            return true;
        }
    }
}

class AsenkronSayac
{
    private readonly SemaphoreSlim _kilit = new(1, 1);
    public int Deger { get; private set; }

    public async Task ArtirAsync()
    {
        await _kilit.WaitAsync();
        try
        {
            int eski = Deger;
            await Task.Yield();   // okuma ile yazma arasında bir await
            Deger = eski + 1;
        }
        finally
        {
            _kilit.Release();
        }
    }
}
