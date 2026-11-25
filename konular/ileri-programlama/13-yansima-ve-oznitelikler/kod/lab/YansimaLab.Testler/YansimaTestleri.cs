using YansimaLab;

namespace YansimaLab.Testler;

// Bütün görevlerin testleri. Bu dosyayı DEĞİŞTİRMEYİN.
public class YansimaTestleri
{
    // ---- Hazır: TipAgaci (baştan yeşil) ----

    [Fact]
    public void Istisnadan_koke_giden_yol()
    {
        Assert.Equal(
            ["ArgumentNullException", "ArgumentException", "SystemException", "Exception", "Object"],
            TipAgaci.KokeGidenYol(typeof(ArgumentNullException)));
    }

    [Fact]
    public void Object_kokun_kendisidir()
    {
        Assert.Equal(["Object"], TipAgaci.KokeGidenYol(typeof(object)));
    }

    // ---- GÖREV 1: Dogrulayici ----

    [Fact]
    public void Gecerli_nesnede_hata_yoktur()
    {
        Assert.Empty(Dogrulayici.Dogrula(new UyeKaydi()));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Zorunlu_metin_bos_olamaz(string? ad)
    {
        Assert.Equal(["Ad zorunlu"], Dogrulayici.Dogrula(new UyeKaydi { Ad = ad }));
    }

    [Fact]
    public void Uzun_metin_gercek_uzunlukla_raporlanir()
    {
        Assert.Equal(["Ad en fazla 10 karakter olabilir (13)"],
            Dogrulayici.Dogrula(new UyeKaydi { Ad = "Mustafa Kemal" }));
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    public void Aralik_iki_ucu_dahildir(int sinir, bool gecerli)
    {
        var hatalar = Dogrulayici.Dogrula(new UyeKaydi { KitapSiniri = sinir });

        if (gecerli)
            Assert.Empty(hatalar);
        else
            Assert.Equal([$"KitapSiniri 1 ile 5 arasında olmalı ({sinir})"], hatalar);
    }

    [Fact]
    public void Hatalar_ozellik_sirasiyla_gelir()
    {
        var kayit = new UyeKaydi { Ad = null, Eposta = "", KitapSiniri = 9 };

        Assert.Equal(
            ["Ad zorunlu", "Eposta zorunlu", "KitapSiniri 1 ile 5 arasında olmalı (9)"],
            Dogrulayici.Dogrula(kayit));
    }

    [Fact]
    public void Null_nesne_istisnadir()
    {
        Assert.Throws<ArgumentNullException>(() => Dogrulayici.Dogrula(null!));
    }

    // ---- GÖREV 2: KomutTablosu ----

    [Fact]
    public void Yalnizca_etiketli_metotlar_sirali_listelenir()
    {
        Assert.Equal(["ekle", "ortalama", "say", "temizle"], new KomutTablosu(new KitapRafi()).Komutlar);
    }

    [Fact]
    public void Argumanlar_parametre_tiplerine_cevrilir()
    {
        var tablo = new KomutTablosu(new KitapRafi());

        Assert.Equal("3 × Huzur", tablo.Calistir("ekle Huzur 3"));
        Assert.Equal("3", tablo.Calistir("say"));
        Assert.Equal("", tablo.Calistir("temizle"));
        Assert.Equal("0", tablo.Calistir("say"));
    }

    [Fact]
    public void Ondalik_sonuc_kulturden_bagimsizdir()
    {
        Assert.Equal("3.75", new KomutTablosu(new KitapRafi()).Calistir("ortalama 7.5 2"));
    }

    [Fact]
    public void Bilinmeyen_komut_ve_eksik_arguman()
    {
        var tablo = new KomutTablosu(new KitapRafi());

        Assert.Equal("bilinmeyen komut: Etiketsiz", tablo.Calistir("Etiketsiz"));
        Assert.Equal("ekle 2 argüman bekliyor, 1 verildi", tablo.Calistir("ekle Huzur"));
    }

    [Fact]
    public void Cevrilemeyen_arguman_raporlanir()
    {
        Assert.Equal("'üç' Int32 değil", new KomutTablosu(new KitapRafi()).Calistir("ekle Huzur üç"));
    }

    [Fact]
    public void Metodun_istisnasi_asil_mesajiyla_doner()
    {
        Assert.Equal("hata: Adet sıfır olamaz.", new KomutTablosu(new KitapRafi()).Calistir("ortalama 10 0"));
    }

    // ---- GÖREV 3: TestKosucu ----

    private static Task<IReadOnlyList<SinamaSonucu>> Kos()
        => TestKosucu.KosAsync(typeof(OrnekSinamalar).Assembly);

    private static SinamaSonucu Bul(IReadOnlyList<SinamaSonucu> sonuclar, string metot)
        => sonuclar.Single(s => s.Ad == $"OrnekSinamalar.{metot}");

    [Fact]
    public async Task Yalnizca_etiketli_metotlar_ada_gore_sirali()
    {
        var sonuclar = await Kos();

        Assert.Equal(
            ["AsenkronGecen", "AsenkronKalan", "Atlanan", "Gecen", "Kalan", "YeniNesne1", "YeniNesne2"],
            sonuclar.Select(s => s.Ad.Split('.')[1]));
    }

    [Fact]
    public async Task Senkron_sinamalar_ve_asil_mesaj()
    {
        var sonuclar = await Kos();

        Assert.Equal(new SinamaSonucu("OrnekSinamalar.Gecen", SinamaDurumu.Gecti, ""), Bul(sonuclar, "Gecen"));
        Assert.Equal(new SinamaSonucu("OrnekSinamalar.Kalan", SinamaDurumu.Kaldi, "beklenen 4, gelen 3"), Bul(sonuclar, "Kalan"));
    }

    [Fact]
    public async Task Asenkron_sinamalar_beklenir()
    {
        var sonuclar = await Kos();

        Assert.Equal(SinamaDurumu.Gecti, Bul(sonuclar, "AsenkronGecen").Durum);
        Assert.Equal(new SinamaSonucu("OrnekSinamalar.AsenkronKalan", SinamaDurumu.Kaldi, "asenkron hata"),
            Bul(sonuclar, "AsenkronKalan"));
    }

    [Fact]
    public async Task Atla_etiketli_sinama_calistirilmaz()
    {
        var sonuclar = await Kos();

        Assert.Equal(new SinamaSonucu("OrnekSinamalar.Atlanan", SinamaDurumu.Atlandi, "çok yavaş"),
            Bul(sonuclar, "Atlanan"));
    }

    [Fact]
    public async Task Her_sinama_yeni_nesneyle_calisir()
    {
        var sonuclar = await Kos();

        Assert.Equal(SinamaDurumu.Gecti, Bul(sonuclar, "YeniNesne1").Durum);
        Assert.Equal(SinamaDurumu.Gecti, Bul(sonuclar, "YeniNesne2").Durum);
    }

    // ---- GÖREV 4: Fark ----

    [Fact]
    public void Ayni_degerlerde_fark_yoktur()
    {
        Assert.Empty(Fark.Bul(new OduncKaydi(), new OduncKaydi()));
    }

    [Fact]
    public void Degisen_ozellik_eski_ve_yeni_degerle_yazilir()
    {
        Assert.Equal(["Uye: Ece → Can"], Fark.Bul(new OduncKaydi(), new OduncKaydi { Uye = "Can" }));
    }

    [Fact]
    public void Farklar_ozellik_sirasiyla_null_yok_olarak()
    {
        var eski = new OduncKaydi();
        var yeni = new OduncKaydi { Not = "geç iade", GecikmeGun = 3 };

        Assert.Equal(["GecikmeGun: 0 → 3", "Not: (yok) → geç iade"], Fark.Bul(eski, yeni));
        Assert.Equal(["GecikmeGun: 3 → 0", "Not: geç iade → (yok)"], Fark.Bul(yeni, eski));
    }

    [Fact]
    public void Null_nesne_istisnadir_fark()
    {
        Assert.Throws<ArgumentNullException>(() => Fark.Bul(null!, new OduncKaydi()));
        Assert.Throws<ArgumentNullException>(() => Fark.Bul(new OduncKaydi(), null!));
    }
}
