// Yarış durumu: aynı değişkeni aynı anda değiştiren görevler.
//
// Sekiz görev aynı sayacı birer milyon kez artırıyor. Sonuç sekiz milyon
// olmalı. Üç biçimde deniyoruz: korumasız sayaç++, Interlocked.Increment ve
// lock. Hangisi doğru sonucu verir?
//
// Korumasız sayacın tam değeri her çalıştırmada değişir; dosya yalnızca
// "kayıp var mı?" sorusunun cevabını yazıyor.
//
// 4. bölüm "kontrol et, sonra yap" yarışı: kütüphanede tek kopyası kalmış
// bir kitabı iki üye aynı anda ödünç almaya çalışıyor.
// Çalıştırmak için:  dotnet run 01-yaris-durumu.cs

const int GorevSayisi = 8;
const int Tekrar = 1_000_000;
const int Beklenen = GorevSayisi * Tekrar;

Console.WriteLine("=== 1 === (korumasız: sayac++)");
int sayac = 0;
await Ornek.HepsiBirlikte(GorevSayisi, () =>
{
    for (int i = 0; i < Tekrar; i++)
        sayac++;
});
Console.WriteLine($"beklenen {Beklenen}, kayıp var mı? {sayac < Beklenen}");

Console.WriteLine("\n=== 2 === (Interlocked.Increment)");
int atomik = 0;
await Ornek.HepsiBirlikte(GorevSayisi, () =>
{
    for (int i = 0; i < Tekrar; i++)
        Interlocked.Increment(ref atomik);
});
Console.WriteLine($"sonuç {atomik}, doğru mu? {atomik == Beklenen}");

Console.WriteLine("\n=== 3 === (lock)");
int kilitli = 0;
object kilit = new();
await Ornek.HepsiBirlikte(GorevSayisi, () =>
{
    for (int i = 0; i < Tekrar; i++)
        lock (kilit)
            kilitli++;
});
Console.WriteLine($"sonuç {kilitli}, doğru mu? {kilitli == Beklenen}");

Console.WriteLine("\n=== 4 === (kontrol et, sonra yap: son kopya)");
int ikiKezVerilen = 0;
for (int deneme = 0; deneme < 2000; deneme++)
{
    var raf = new Raf(kopya: 1);
    await Ornek.HepsiBirlikte(2, () => raf.OduncVerKorumasiz());
    if (raf.Kopya < 0)
        ikiKezVerilen++;
}
Console.WriteLine($"korumasız: 2000 denemenin bazısında kopya -1 oldu mu? {ikiKezVerilen > 0}");

ikiKezVerilen = 0;
for (int deneme = 0; deneme < 2000; deneme++)
{
    var raf = new Raf(kopya: 1);
    await Ornek.HepsiBirlikte(2, () => raf.OduncVerKilitli());
    if (raf.Kopya < 0)
        ikiKezVerilen++;
}
Console.WriteLine($"kilitli: kopya -1 olan deneme sayısı {ikiKezVerilen}");

static class Ornek
{
    // Görevlerin hepsini aynı anda koşturmak için bir bariyer: her görev
    // bariyere gelince durur, sonuncusu da gelince hepsi birlikte devam eder.
    public static Task HepsiBirlikte(int adet, Action govde)
    {
        var bariyer = new Barrier(adet);
        var gorevler = Enumerable.Range(0, adet)
            .Select(_ => Task.Run(() => { bariyer.SignalAndWait(); govde(); }))
            .ToArray();
        return Task.WhenAll(gorevler).ContinueWith(_ => bariyer.Dispose());
    }
}

class Raf(int kopya)
{
    private readonly object _kilit = new();
    public int Kopya { get; private set; } = kopya;

    public void OduncVerKorumasiz()
    {
        if (Kopya > 0)            // kontrol et
        {
            Thread.SpinWait(200); // kontrol ile işlem arasında küçük bir iş
            Kopya--;              // sonra yap
        }
    }

    public void OduncVerKilitli()
    {
        lock (_kilit)
        {
            if (Kopya > 0)
            {
                Thread.SpinWait(200);
                Kopya--;
            }
        }
    }
}
