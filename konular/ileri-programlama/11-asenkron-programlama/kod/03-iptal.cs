// İptal: CancellationTokenSource ve CancellationToken.
//
// İptali isteyen ile işi yapan ayrı nesneler tutar: istek tarafı
// CancellationTokenSource'tur ("iptal et" düğmesi), iş tarafına yalnızca
// onun Token'ı verilir ("iptal istendi mi?" diye bakılan pencere).
//
// İptal işbirliğine dayanır: token'ı almayan ya da ona bakmayan bir iş
// durdurulamaz. 3. bölüm bunu gösteriyor.
//
// Süreler makineye göre birkaç milisaniye oynar; adımlar arasında yeterli
// pay bırakıldı, sonuç değişmez.
// Çalıştırmak için:  dotnet run 03-iptal.cs

Console.WriteLine("=== 1 === (süre dolunca iptal: CancelAfter)");
using (var kaynak = new CancellationTokenSource())
{
    kaynak.CancelAfter(TimeSpan.FromMilliseconds(250));
    try
    {
        await Task.Delay(TimeSpan.FromSeconds(5), kaynak.Token);
        Console.WriteLine("5 saniye bekledim");
    }
    catch (OperationCanceledException e)
    {
        Console.WriteLine($"yakalanan tip: {e.GetType().Name}");
        Console.WriteLine($"OperationCanceledException mı? {e is OperationCanceledException}");
    }
}

Console.WriteLine("\n=== 2 === (işlemciye bağlı işte iptal: ThrowIfCancellationRequested)");
using (var kaynak = new CancellationTokenSource(TimeSpan.FromMilliseconds(100)))
{
    try
    {
        long toplam = await Task.Run(() => Is.UzunHesap(kaynak.Token));
        Console.WriteLine($"hesap bitti: {toplam}");
    }
    catch (OperationCanceledException e)
    {
        Console.WriteLine($"hesap yarıda kesildi ({e.GetType().Name})");
    }
}

Console.WriteLine("\n=== 3 === (iptal işbirliğidir)");
using (var kaynak = new CancellationTokenSource(TimeSpan.FromMilliseconds(150)))
{
    int adim = await Is.TokenAlmayanAsync();
    Console.WriteLine($"token almayan iş: {adim}/5 adım, iptal istendi mi? {kaynak.IsCancellationRequested}");
}
using (var kaynak = new CancellationTokenSource(TimeSpan.FromMilliseconds(150)))
{
    try
    {
        await Is.TokenAlanAsync(kaynak.Token);
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine($"token alan iş: {Is.TamamlananAdim}/5 adımda durdu");
    }
}

Console.WriteLine("\n=== 4 === (iki sebep, tek token: CreateLinkedTokenSource)");
Console.WriteLine(await Is.AraAsync(kullaniciVazgecerMs: 100));
Console.WriteLine(await Is.AraAsync(kullaniciVazgecerMs: null));

static class Is
{
    // İşlemciye bağlı: G/Ç yok, beklenecek bir şey yok; arada token'a bakılıyor
    public static long UzunHesap(CancellationToken iptal)
    {
        long toplam = 0;
        for (long i = 0; i < 50_000_000_000; i++)
        {
            if (i % 1_000_000 == 0)
                iptal.ThrowIfCancellationRequested();
            toplam += i % 7;
        }
        return toplam;
    }

    public static async Task<int> TokenAlmayanAsync()
    {
        int adim = 0;
        for (int i = 0; i < 5; i++)
        {
            await Task.Delay(100);
            adim++;
        }
        return adim;
    }

    public static int TamamlananAdim { get; private set; }

    public static async Task TokenAlanAsync(CancellationToken iptal)
    {
        TamamlananAdim = 0;
        for (int i = 0; i < 5; i++)
        {
            await Task.Delay(100, iptal);
            TamamlananAdim++;
        }
    }

    // Kullanıcı vazgeçebilir (Ctrl+C gibi) ya da arama 300 ms'de bitmezse
    // zaman aşımı olur. İkisi de aynı işi durdurmalı; hangisinin durdurduğu
    // sonradan ayırt edilir.
    public static async Task<string> AraAsync(int? kullaniciVazgecerMs)
    {
        using var kullanici = new CancellationTokenSource();
        if (kullaniciVazgecerMs is int ms)
            kullanici.CancelAfter(ms);

        using var birlesik = CancellationTokenSource.CreateLinkedTokenSource(kullanici.Token);
        birlesik.CancelAfter(TimeSpan.FromMilliseconds(300));

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(5), birlesik.Token);
            return "arama bitti";
        }
        catch (OperationCanceledException) when (kullanici.IsCancellationRequested)
        {
            return "kullanıcı vazgeçti";
        }
        catch (OperationCanceledException)
        {
            return "zaman aşımı";
        }
    }
}
