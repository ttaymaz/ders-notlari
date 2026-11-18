using DiLab;

namespace DiLab.Testler;

// GÖREV 1–3'ün testleri. Bu dosyayı DEĞİŞTİRMEYİN; testler geçene kadar
// DiLab/Kap.cs dosyasını düzenleyin. İlk iki test hazır kodu sınar ve
// baştan geçer.
public class KapTestleri
{
    // ---- Hazır davranış

    [Fact]
    public void Kaydet_her_cozumde_yeni_nesne_verir()
    {
        var kap = new Kap();
        kap.Kaydet(k => new Sepet());

        Assert.NotSame(kap.Coz<Sepet>(), kap.Coz<Sepet>());
    }

    [Fact]
    public void Dongu_yolu_mesajda_yazar()
    {
        var kap = new Kap();
        kap.Kaydet(k => new Fatura(k.Coz<Hatirlatici>()));
        kap.Kaydet(k => new Hatirlatici(k.Coz<Fatura>()));

        var hata = Assert.Throws<InvalidOperationException>(() => kap.Coz<Fatura>());

        Assert.Equal("Döngüsel bağımlılık: Fatura → Hatirlatici → Fatura", hata.Message);
    }

    // ---- GÖREV 1: tekil kayıt

    [Fact]
    public void KaydetTekil_her_cozumde_ayni_nesneyi_verir()
    {
        var kap = new Kap();
        kap.KaydetTekil(k => new Sepet());

        Assert.Same(kap.Coz<Sepet>(), kap.Coz<Sepet>());
    }

    [Fact]
    public void KaydetTekil_fabrikayi_bir_kez_calistirir()
    {
        var kap = new Kap();
        int calisma = 0;
        kap.KaydetTekil(k => { calisma++; return new Sepet(); });

        kap.Coz<Sepet>();
        kap.Coz<Sepet>();
        kap.Coz<Sepet>();

        Assert.Equal(1, calisma);
    }

    [Fact]
    public void Tekil_bagimlilik_iki_servise_ayni_nesne_olarak_gider()
    {
        var kap = new Kap();
        kap.KaydetTekil(k => new Sepet());
        kap.Kaydet(k => new Kasa(k.Coz<Sepet>()));

        var a = kap.Coz<Kasa>();
        var b = kap.Coz<Kasa>();

        Assert.NotSame(a, b);
        Assert.Same(a.Sepet, b.Sepet);
    }

    // ---- GÖREV 2: kayıtsız tip

    [Fact]
    public void Kayitsiz_tip_dogrudan_istenince_adini_soyler()
    {
        var kap = new Kap();

        var hata = Assert.Throws<InvalidOperationException>(() => kap.Coz<Fatura>());

        Assert.Equal("Fatura kayıtlı değil.", hata.Message);
    }

    [Fact]
    public void Kayitsiz_bagimlilik_isteyenini_de_soyler()
    {
        var kap = new Kap();
        kap.Kaydet(k => new Kasa(k.Coz<Sepet>()));

        var hata = Assert.Throws<InvalidOperationException>(() => kap.Coz<Kasa>());

        Assert.Equal("Sepet kayıtlı değil (Kasa istiyor).", hata.Message);
    }

    // ---- GÖREV 3: Dogrula

    [Fact]
    public void Dogrula_sorunsuz_kapta_bos_liste_doner()
    {
        var kap = new Kap();
        kap.KaydetTekil(k => new Sepet());
        kap.Kaydet(k => new Kasa(k.Coz<Sepet>()));

        Assert.Empty(kap.Dogrula());
    }

    [Fact]
    public void Dogrula_her_sorunu_kayit_sirasiyla_listeler()
    {
        var kap = new Kap();
        kap.Kaydet(k => new Kasa(k.Coz<Sepet>()));          // Sepet kayıtlı değil
        kap.Kaydet(k => new Fatura(k.Coz<Hatirlatici>()));  // döngü
        kap.Kaydet(k => new Hatirlatici(k.Coz<Fatura>()));

        var sorunlar = kap.Dogrula();

        Assert.Equal(
        [
            "Sepet kayıtlı değil (Kasa istiyor).",
            "Döngüsel bağımlılık: Fatura → Hatirlatici → Fatura",
            "Döngüsel bağımlılık: Hatirlatici → Fatura → Hatirlatici",
        ], sorunlar);
    }

    [Fact]
    public void Dogrula_sonrasi_kap_kullanilmaya_devam_eder()
    {
        var kap = new Kap();
        kap.Kaydet(k => new Kasa(k.Coz<Sepet>()));
        kap.Dogrula();
        kap.KaydetTekil(k => new Sepet());

        Assert.NotNull(kap.Coz<Kasa>());
    }

    private sealed class Sepet;
    private sealed record Kasa(Sepet Sepet);
    private sealed record Fatura(Hatirlatici Hatirlatici);
    private sealed record Hatirlatici(Fatura Fatura);
}
