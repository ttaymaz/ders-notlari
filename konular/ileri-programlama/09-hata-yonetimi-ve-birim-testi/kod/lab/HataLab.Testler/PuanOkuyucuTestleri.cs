using HataLab;

namespace HataLab.Testler;

// GÖREV 2 ve 3'ün testleri. Bu dosyayı DEĞİŞTİRMEYİN.
public class PuanOkuyucuTestleri
{
    [Theory]
    [InlineData("85", 85)]
    [InlineData(" 85 ", 85)]
    [InlineData("0", 0)]
    [InlineData("100", 100)]
    public void Gecerli_puan_okunur(string metin, int beklenen)
    {
        var sonuc = PuanOkuyucu.Oku(metin);

        Assert.Equal(new Basarili<int>(beklenen), sonuc);
    }

    [Theory]
    [InlineData(null, "boş")]
    [InlineData("", "boş")]
    [InlineData("   ", "boş")]
    [InlineData("seksen", "sayı değil")]
    [InlineData("85.5", "sayı değil")]
    [InlineData("101", "aralık dışı")]
    [InlineData("-1", "aralık dışı")]
    public void Gecersiz_metin_hatali_sonuc_doner(string? metin, string neden)
    {
        var sonuc = PuanOkuyucu.Oku(metin);

        Assert.Equal(new Hatali<int>(neden), sonuc);
    }

    [Fact]
    public void OkuYaDaFirlat_gecerli_puani_dondurur()
    {
        Assert.Equal(70, PuanOkuyucu.OkuYaDaFirlat("70"));
    }

    [Fact]
    public void OkuYaDaFirlat_hatali_sonucu_istisnaya_cevirir()
    {
        var hata = Assert.Throws<ArgumentException>(() => PuanOkuyucu.OkuYaDaFirlat("yüz"));

        Assert.Equal("metin", hata.ParamName);
        Assert.StartsWith("sayı değil", hata.Message);
    }
}
