using Cekirdek;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Testler;

public class OduncServisiTestleri
{
    private readonly SahteSaat _saat = new(new DateOnly(2026, 11, 2));
    private readonly BellekDepo<Odunc> _depo = new();

    // Servisi kurarken her bağımlılığın yerine denetlenebilir bir sürüm
    // veriyoruz: bellekte depo, sahte saat, elle yazılmış ayar, sessiz günlük.
    private OduncServisi ServisKur(OduncAyarlari? ayar = null)
        => new(_depo, _saat,
               Options.Create(ayar ?? new OduncAyarlari()),
               NullLogger<OduncServisi>.Instance);

    [Fact]
    public async Task Son_tarih_ayardaki_gun_kadar_sonradir()
    {
        var servis = ServisKur(new OduncAyarlari { Gun = 7 });

        var sonuc = await servis.OduncVerAsync("Huzur", "Ece");

        var odunc = Assert.IsType<Basarili<Odunc>>(sonuc).Deger;
        Assert.Equal(new DateOnly(2026, 11, 9), odunc.SonTarih);
    }

    [Fact]
    public async Task Oduncteki_kitap_ikinci_kez_verilemez()
    {
        var servis = ServisKur();
        await servis.OduncVerAsync("Huzur", "Ece");

        var sonuc = await servis.OduncVerAsync("Huzur", "Can");

        Assert.Equal(new Hatali<Odunc>("kitap ödünçte"), sonuc);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Uye_siniri_ayardan_okunur(int sinir)
    {
        var servis = ServisKur(new OduncAyarlari { EnFazlaKitap = sinir });
        for (int i = 0; i < sinir; i++)
            await servis.OduncVerAsync($"Kitap {i}", "Ece");

        var sonuc = await servis.OduncVerAsync("Bir fazlası", "Ece");

        Assert.Equal(new Hatali<Odunc>("üye sınırı doldu"), sonuc);
    }

    [Fact]
    public async Task Bos_baslik_reddedilir()
    {
        var servis = ServisKur();

        await Assert.ThrowsAsync<ArgumentException>(() => servis.OduncVerAsync(" ", "Ece"));
    }

    [Fact]
    public async Task Zamaninda_iade_cezasizdir()
    {
        var servis = ServisKur(new OduncAyarlari { Gun = 14 });
        await servis.OduncVerAsync("Huzur", "Ece");
        _saat.Ilerle(14);

        var sonuc = await servis.IadeEtAsync("Huzur");

        Assert.Equal(new Basarili<int>(0), sonuc);
    }

    [Fact]
    public async Task Gec_iade_gun_basina_ceza_alir()
    {
        var servis = ServisKur(new OduncAyarlari { Gun = 14, GunlukCeza = 3 });
        await servis.OduncVerAsync("Huzur", "Ece");
        _saat.Ilerle(14 + 5);

        var sonuc = await servis.IadeEtAsync("Huzur");

        Assert.Equal(new Basarili<int>(15), sonuc);
    }

    [Fact]
    public async Task Ceza_tavani_asilmaz()
    {
        var servis = ServisKur(new OduncAyarlari { Gun = 14, GunlukCeza = 3, CezaTavani = 60 });
        await servis.OduncVerAsync("Huzur", "Ece");
        _saat.Ilerle(100);

        var sonuc = await servis.IadeEtAsync("Huzur");

        Assert.Equal(new Basarili<int>(60), sonuc);
    }

    [Fact]
    public async Task Oduncte_olmayan_kitap_iade_edilemez()
    {
        var servis = ServisKur();

        var sonuc = await servis.IadeEtAsync("Huzur");

        Assert.Equal(new Hatali<int>("kitap ödünçte değil"), sonuc);
    }

    [Fact]
    public async Task Iade_edilen_kitap_yeniden_odunc_verilebilir()
    {
        var servis = ServisKur();
        await servis.OduncVerAsync("Huzur", "Ece");
        await servis.IadeEtAsync("Huzur");

        var sonuc = await servis.OduncVerAsync("Huzur", "Can");

        Assert.IsType<Basarili<Odunc>>(sonuc);
    }

    [Fact]
    public async Task Gecikenler_en_eski_son_tarihten_baslar()
    {
        var servis = ServisKur(new OduncAyarlari { Gun = 10 });
        await servis.OduncVerAsync("Huzur", "Ece");      // son tarih 12 Kasım
        _saat.Ilerle(3);
        await servis.OduncVerAsync("Çalıkuşu", "Can");   // son tarih 15 Kasım
        _saat.Ilerle(20);                                  // bugün 25 Kasım

        var gecikenler = await servis.GecikenlerAsync();

        Assert.Equal(["Huzur", "Çalıkuşu"], gecikenler.Select(o => o.Baslik));
    }
}
