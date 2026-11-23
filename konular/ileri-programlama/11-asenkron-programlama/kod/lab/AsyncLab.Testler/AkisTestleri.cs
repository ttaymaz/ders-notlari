using AsyncLab;

namespace AsyncLab.Testler;

// 3. ve 4. görevin testleri. Bu dosyayı DEĞİŞTİRMEYİN.
public class AkisTestleri
{
    // ---- GÖREV 3: asenkron akış ----

    [Fact]
    public async Task Akis_kitaplari_genisligine_sirayla_verir()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        var alinan = new List<string>();

        await foreach (var kitap in Gezgin.KesfetAsync(katalog, "Huzur"))
            alinan.Add(kitap);

        Assert.Equal(SahteKatalog.OrnekSira, alinan);
    }

    [Fact]
    public void Akis_olusturulunca_hicbir_soru_sorulmaz()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());

        _ = Gezgin.KesfetAsync(katalog, "Huzur");

        Assert.Equal(0, katalog.Cagri);
    }

    [Fact]
    public async Task Erken_birakilinca_geri_kalan_sorular_sorulmaz()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        var alinan = new List<string>();

        await foreach (var kitap in Gezgin.KesfetAsync(katalog, "Huzur"))
        {
            alinan.Add(kitap);
            if (alinan.Count == 4)
                break;
        }

        // Dördüncü kitap verilmeden önce yalnızca ilk üçünün bağlantıları sorulur
        Assert.Equal(["Huzur", "Tutunamayanlar", "Saatleri Ayarlama Enstitüsü"], katalog.Sorulanlar);
    }

    [Fact]
    public async Task WithCancellation_ile_verilen_token_iptal_eder()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        using var kaynak = new CancellationTokenSource();
        var alinan = new List<string>();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () =>
        {
            await foreach (var kitap in Gezgin.KesfetAsync(katalog, "Huzur").WithCancellation(kaynak.Token))
            {
                alinan.Add(kitap);
                if (alinan.Count == 2)
                    kaynak.Cancel();
            }
        });

        Assert.Equal(2, alinan.Count);
    }

    // ---- GÖREV 4: hataya dayanıklı gezinti ----

    [Fact]
    public async Task Hatali_kitap_atlanir_gezinti_surer()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        katalog.Hatali.Add("Tutunamayanlar");

        var sonuc = await Gezgin.HatalariAtlayarakGezAsync(katalog, "Huzur");

        // Tehlikeli Oyunlar'a yalnızca Tutunamayanlar'dan gidiliyordu
        Assert.Equal(
            ["Huzur", "Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam", "Beş Şehir", "Anayurt Oteli"],
            sonuc.Sira);
        Assert.Equal(["Tutunamayanlar"], sonuc.Hatalilar);
    }

    [Fact]
    public async Task Ayni_duzeydeki_butun_hatalar_listelenir()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        katalog.Hatali.UnionWith(["Aylak Adam", "Tutunamayanlar"]);

        var sonuc = await Gezgin.HatalariAtlayarakGezAsync(katalog, "Huzur");

        Assert.Equal(["Huzur", "Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam", "Beş Şehir"], sonuc.Sira);
        Assert.Equal(["Tutunamayanlar", "Aylak Adam"], sonuc.Hatalilar);
    }

    [Fact]
    public async Task Iptal_hata_gibi_yutulmaz()
    {
        var katalog = new SahteKatalog(SahteKatalog.OrnekGraf());
        using var kaynak = new CancellationTokenSource();
        katalog.Hatali.Add("Tutunamayanlar");
        // 2. düzeyin soruları sürerken iptal: üç soru da iptal ile biter
        katalog.SoruluncaIptalEt = (kaynak, "Aylak Adam");

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => Gezgin.HatalariAtlayarakGezAsync(katalog, "Huzur", kaynak.Token));
    }
}
