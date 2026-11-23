using AsyncLab;

namespace AsyncLab.Testler;

// Hazır kodun testleri ile 1. ve 2. görevin testleri. Bu dosyayı DEĞİŞTİRMEYİN.
//
// Asenkron bir metodu sınayan test de async Task döndürür ve metodu await
// ile bekler. İstisna beklenen yerde Assert.ThrowsAnyAsync kullanılır:
// iptal bazen OperationCanceledException, bazen ondan türeyen
// TaskCanceledException olarak gelir (9. hafta: Throws birebir tip ister).
public class GezintiTestleri
{
    // ---- Hazır: GezSiraliAsync (baştan yeşil) ----

    [Fact]
    public async Task Sirali_gezinti_genisligine_ve_her_kitabi_bir_kez_gezer()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        var sira = await Gezgin.GezSiraliAsync(katalog, "Huzur");

        Assert.Equal(SahteKatalog.OrnekSira, sira);
        Assert.Equal(7, katalog.Cagri);
    }

    [Fact]
    public async Task Sirali_gezintide_ayni_anda_tek_soru_surer()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        await Gezgin.GezSiraliAsync(katalog, "Huzur");

        Assert.Equal(1, katalog.EnCokAyniAnda);
    }

    // ---- GÖREV 1: düzey düzey ----

    [Fact]
    public async Task Duzey_duzey_gezinti_sirali_ile_ayni_sirayi_verir()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        var sira = await Gezgin.GezDuzeyDuzeyAsync(katalog, "Huzur");

        Assert.Equal(SahteKatalog.OrnekSira, sira);
    }

    [Fact]
    public async Task Duzey_duzey_gezinti_her_kitabi_bir_kez_sorar()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        await Gezgin.GezDuzeyDuzeyAsync(katalog, "Huzur");

        Assert.Equal(7, katalog.Cagri);
        Assert.Equal(7, katalog.Sorulanlar.Distinct().Count());
    }

    [Fact]
    public async Task Ayni_duzeydeki_sorular_birlikte_surer()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        await Gezgin.GezDuzeyDuzeyAsync(katalog, "Huzur");

        // 2. düzeyde üç kitap var: üç soru aynı anda sürmeli
        Assert.Equal(3, katalog.EnCokAyniAnda);
    }

    [Fact]
    public async Task Baglantisi_olmayan_kitapta_yalniz_kendisi_doner()
    {
        var katalog = new SahteKatalog(new() { ["Beş Şehir"] = [] });

        var sira = await Gezgin.GezDuzeyDuzeyAsync(katalog, "Beş Şehir");

        Assert.Equal(["Beş Şehir"], sira);
    }

    // ---- GÖREV 2: iptal ----

    [Fact]
    public async Task Token_her_soruya_iletilir()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        using var kaynak = new CancellationTokenSource();

        await Gezgin.GezDuzeyDuzeyAsync(katalog, "Huzur", kaynak.Token);

        Assert.Equal(0, katalog.TokensizCagri);
    }

    [Fact]
    public async Task Onceden_iptal_edilmisse_istisna_firlar()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        using var kaynak = new CancellationTokenSource();
        kaynak.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => Gezgin.GezDuzeyDuzeyAsync(katalog, "Huzur", kaynak.Token));
    }

    [Fact]
    public async Task Iptalden_sonra_yeni_duzeyin_sorulari_sorulmaz()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        using var kaynak = new CancellationTokenSource();
        katalog.BitinceIptalEt = (kaynak, 1);   // ilk soru bitince iptal

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => Gezgin.GezDuzeyDuzeyAsync(katalog, "Huzur", kaynak.Token));

        Assert.Equal(["Huzur"], katalog.Sorulanlar);
    }
}
