using EsLab;

namespace EsLab.Testler;

// 3. ve 4. görevin testleri. Bu dosyayı DEĞİŞTİRMEYİN.
public class GezginVeKuyrukTestleri
{
    // Kuyruk kapatılmazsa (Complete unutulursa) tüketiciler sonsuza dek
    // bekler. Test asılı kalmasın diye her bekleme en çok 5 saniye sürer;
    // süre dolarsa test TimeoutException ile kırmızı olur.
    private static readonly TimeSpan EnCok5Saniye = TimeSpan.FromSeconds(5);

    // ---- GÖREV 3: SinirliGezgin ----

    [Fact]
    public async Task Sinirli_gezinti_sinirsiz_ile_ayni_sirayi_verir()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        var sira = await SinirliGezgin.GezAsync(katalog, "Huzur", enCok: 2);

        Assert.Equal(SahteKatalog.OrnekSira, sira);
        Assert.Equal(8, katalog.Cagri);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task Ayni_anda_surenler_siniri_asmaz_ve_sinira_ulasir(int enCok)
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        await SinirliGezgin.GezAsync(katalog, "Huzur", enCok);

        // 3. düzeyde dört kitap var: sınır kadar soru aynı anda sürmeli
        Assert.Equal(enCok, katalog.EnCokAyniAnda);
    }

    [Fact]
    public async Task Sinir_birden_kucukse_istisna()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => SinirliGezgin.GezAsync(katalog, "Huzur", enCok: 0));
    }

    [Fact]
    public async Task Token_her_soruya_iletilir()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        using var kaynak = new CancellationTokenSource();

        await SinirliGezgin.GezAsync(katalog, "Huzur", 2, kaynak.Token);

        Assert.Equal(0, katalog.TokensizCagri);
    }

    // ---- GÖREV 4: IadeKuyrugu ----

    [Fact]
    public async Task Tek_tuketici_ekleme_sirasiyla_yerlestirir()
    {
        var yerlesen = new List<string>();
        var kuyruk = new IadeKuyrugu(kapasite: 2, tuketiciSayisi: 1, async kitap =>
        {
            await Task.Delay(1);
            yerlesen.Add(kitap);
        });

        foreach (var kitap in SahteKatalog.OrnekSira)
            await kuyruk.EkleAsync(kitap).WaitAsync(EnCok5Saniye);
        await kuyruk.TamamlaAsync().WaitAsync(EnCok5Saniye);

        Assert.Equal(SahteKatalog.OrnekSira, yerlesen);
    }

    [Fact]
    public async Task Cok_tuketicide_her_kitap_bir_kez_yerlesir()
    {
        var yerlesen = new System.Collections.Concurrent.ConcurrentBag<string>();
        var kuyruk = new IadeKuyrugu(kapasite: 3, tuketiciSayisi: 4, async kitap =>
        {
            await Task.Delay(1);
            yerlesen.Add(kitap);
        });

        var kitaplar = Enumerable.Range(1, 100).Select(i => $"kitap{i}").ToList();
        foreach (var kitap in kitaplar)
            await kuyruk.EkleAsync(kitap).WaitAsync(EnCok5Saniye);
        await kuyruk.TamamlaAsync().WaitAsync(EnCok5Saniye);

        Assert.Equal(kitaplar.Order(), yerlesen.Order());
    }

    [Fact]
    public async Task Kuyruk_doluysa_ekleme_bekler()
    {
        var ilkBasladi = new TaskCompletionSource();
        var birak = new TaskCompletionSource();
        var kuyruk = new IadeKuyrugu(kapasite: 2, tuketiciSayisi: 1, async kitap =>
        {
            ilkBasladi.TrySetResult();
            await birak.Task;          // tüketici ilk kitapta takılı kalır
        });

        await kuyruk.EkleAsync("1").WaitAsync(EnCok5Saniye);
        await ilkBasladi.Task.WaitAsync(EnCok5Saniye);         // tüketici 1'i aldı, kuyruk boş
        await kuyruk.EkleAsync("2").WaitAsync(EnCok5Saniye);
        await kuyruk.EkleAsync("3").WaitAsync(EnCok5Saniye);   // kuyrukta 2 kitap: dolu
        Task dorduncu = kuyruk.EkleAsync("4");

        await Task.Delay(50);
        Assert.False(dorduncu.IsCompleted);

        birak.SetResult();
        await dorduncu.WaitAsync(EnCok5Saniye);                // yer açılınca tamamlanır
        await kuyruk.TamamlaAsync().WaitAsync(EnCok5Saniye);
    }

    [Fact]
    public async Task Tamamla_butun_yerlestirmeleri_bekler()
    {
        int yerlesen = 0;
        var kuyruk = new IadeKuyrugu(kapasite: 5, tuketiciSayisi: 2, async _ =>
        {
            await Task.Delay(20);
            Interlocked.Increment(ref yerlesen);
        });

        for (int i = 0; i < 10; i++)
            await kuyruk.EkleAsync($"kitap{i}").WaitAsync(EnCok5Saniye);
        await kuyruk.TamamlaAsync().WaitAsync(EnCok5Saniye);

        Assert.Equal(10, Volatile.Read(ref yerlesen));
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void Kapasite_ya_da_tuketici_birden_kucukse_istisna(int kapasite, int tuketici)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new IadeKuyrugu(kapasite, tuketici, _ => Task.CompletedTask));
    }
}
