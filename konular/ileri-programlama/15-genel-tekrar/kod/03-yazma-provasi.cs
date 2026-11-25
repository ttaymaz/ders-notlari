// PROVA 3 — kısa kod yazma (dönemin ikinci yarısı).
//
// Yazılı sınavda kod okumanın yanında birkaç satırlık kod yazmanız da
// istenebilir: bir metodun gövdesini tamamlamak gibi. Bu dosyadaki altı
// görev o biçimdedir; her biri 9–14. haftalardan birine dayanır.
//
// NASIL ÇALIŞILIR:
//   1. Her görevi ÖNCE KÂĞIDA yazın. Sınavda derleyici yok.
//   2. Sonra kâğıttakini buraya geçirip çalıştırın. Denetim kaç görevin
//      tamam olduğunu söyler. Kâğıtta yaptığınız hatayı not edin.
//   3. Denetim kodunu değiştirmeyin.
//
// Her TODO şu an NotImplementedException fırlatıyor.
//
// Çalıştırmak için:  dotnet run 03-yazma-provasi.cs

using System.Collections.Concurrent;
using System.Reflection;

// ---------------------------------------------------------------- DENETİM
int tamam = 0, toplam = 0;

async Task Denetle(string ad, Func<Task<bool>> sinama)
{
    toplam++;
    try
    {
        bool sonuc = await sinama();
        if (sonuc) tamam++;
        Console.WriteLine($"[{(sonuc ? "TAMAM" : "YANLIŞ")}] {ad}");
    }
    catch (NotImplementedException)
    {
        Console.WriteLine($"[YAPILMADI] {ad}");
    }
    catch (Exception hata)
    {
        Console.WriteLine($"[HATA] {ad}: {hata.GetType().Name}: {hata.Message}");
    }
}

Task<bool> Hemen(bool b) => Task.FromResult(b);

await Denetle("1. TryBol: 7 / 2 = 3", () => Hemen(Gorevler.TryBol(7, 2, out int s) && s == 3));
await Denetle("1. TryBol: sıfıra bölmede false ve sonuç 0", () => Hemen(!Gorevler.TryBol(7, 0, out int s) && s == 0));

await Denetle("2. IlkBitenAsync: önce biten kazanır", async () =>
{
    var yavas = new TaskCompletionSource<string>();
    var hizli = new TaskCompletionSource<string>();
    Task<string> yaris = Gorevler.IlkBitenAsync(yavas.Task, hizli.Task);
    hizli.SetResult("yedek");
    try
    {
        // Yanlış bir çözüm asılı kalmasın diye en çok 2 saniye (12. hafta)
        string sonuc = await yaris.WaitAsync(TimeSpan.FromSeconds(2));
        return sonuc == "yedek";
    }
    catch (TimeoutException)
    {
        throw new InvalidOperationException("2 saniyede bitmedi: iki görevin de bitmesini mi bekliyorsunuz?");
    }
    finally
    {
        yavas.SetResult("asıl");
    }
});

await Denetle("3. CiftlerAsync: 1..6 → 2 4 6", async () =>
{
    var alinan = new List<int>();
    await foreach (int x in Gorevler.CiftlerAsync(Sayilar(6)))
        alinan.Add(x);
    return alinan.SequenceEqual([2, 4, 6]);
});
await Denetle("3. CiftlerAsync: erken bırakınca kaynak durur", async () =>
{
    int uretilen = 0;
    async IAsyncEnumerable<int> Sayan()
    {
        for (int i = 1; ; i++)
        {
            if (i > 1000)
                throw new InvalidOperationException("kaynağın tamamı okunmaya çalışıldı; akış ertelenmiş değil");
            uretilen++;
            await Task.Yield();
            yield return i;
        }
    }
    await foreach (int x in Gorevler.CiftlerAsync(Sayan()))
        if (x == 4) break;
    return uretilen == 4;
});

await Denetle("4. Frekans: paralel sayımda kayıp yok", () =>
{
    string[] kelimeler = Enumerable.Range(0, 100_000).Select(i => i % 3 == 0 ? "huzur" : "aylak").ToArray();
    var f = Gorevler.Frekans(kelimeler);
    return Hemen(f["huzur"] == 33_334 && f["aylak"] == 66_666 && f.Count == 2);
});

await Denetle("5. KomutAdi: etiket varsa etiketin adı, yoksa metodun adı", () =>
{
    var t = typeof(Ornek);
    return Hemen(Gorevler.KomutAdi(t.GetMethod(nameof(Ornek.Listele))!) == "ls"
              && Gorevler.KomutAdi(t.GetMethod(nameof(Ornek.Yardim))!) == "Yardim");
});

await Denetle("6. RakamToplami: \"TR-813-07\" → 19", () => Hemen(Gorevler.RakamToplami("TR-813-07") == 19));
await Denetle("6. RakamToplami: bellek ayırmaz", () =>
{
    string metin = "a1b2c3";
    Gorevler.RakamToplami(metin);                            // ısınma
    long once = GC.GetAllocatedBytesForCurrentThread();
    int s = Gorevler.RakamToplami(metin);
    return Hemen(s == 6 && GC.GetAllocatedBytesForCurrentThread() - once == 0);
});

Console.WriteLine($"\n{tamam}/{toplam}");

static async IAsyncEnumerable<int> Sayilar(int n)
{
    for (int i = 1; i <= n; i++) { await Task.Yield(); yield return i; }
}

// ------------------------------------------------------------------ GÖREVLER
static class Gorevler
{
    // 1 (9. hafta, Try kalıbı): a / b'yi hesapla. b sıfırsa istisna FIRLATMA:
    //   sonuc = 0 ve false döndür. Değilse sonuc = a / b ve true.
    public static bool TryBol(int a, int b, out int sonuc)
    {
        throw new NotImplementedException();
    }

    // 2 (11. hafta): iki görevden ÖNCE BİTENİN sonucunu döndür.
    public static async Task<string> IlkBitenAsync(Task<string> a, Task<string> b)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    // 3 (5. ve 11. hafta): kaynaktan gelen çift sayıları TEKER TEKER ver.
    //   Tüketici bırakınca kaynaktan fazladan eleman çekilmesin.
    public static async IAsyncEnumerable<int> CiftlerAsync(IAsyncEnumerable<int> kaynak)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
#pragma warning disable CS0162 // ulaşılamayan kod: görev yazılınca bu iki satırı silin
        yield break;
#pragma warning restore CS0162
    }

    // 4 (12. hafta): kelimeleri Parallel.ForEach ile say. Hiçbir sayım
    //   kaybolmasın. Sözlük: kelime → kaç kez.
    //   İskelet: var sayilar = new ConcurrentDictionary<string, int>();
    //            Parallel.ForEach(kelimeler, kelime => { ... });
    //            return sayilar;
    public static IReadOnlyDictionary<string, int> Frekans(string[] kelimeler)
    {
        throw new NotImplementedException();
    }

    // 5 (13. hafta): metotta [Komut] varsa onun Ad'ını, yoksa metodun adını döndür.
    public static string KomutAdi(MethodInfo metot)
    {
        throw new NotImplementedException();
    }

    // 6 (14. hafta): metindeki rakamların toplamı; hiç bellek ayırma.
    public static int RakamToplami(ReadOnlySpan<char> metin)
    {
        throw new NotImplementedException();
    }
}

[AttributeUsage(AttributeTargets.Method)]
class KomutAttribute(string ad) : Attribute
{
    public string Ad { get; } = ad;
}

static class Ornek
{
    [Komut("ls")]
    public static void Listele() { }

    public static void Yardim() { }
}
