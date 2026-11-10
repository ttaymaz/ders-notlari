using Yapilar;

namespace Yapilar.Testler;

// Her test üç bölümden oluşur:
//   Hazırla  (Arrange) — sınanacak nesneyi kur
//   Çalıştır (Act)     — tek bir davranışı tetikle
//   Doğrula  (Assert)  — sonucu beklenenle karşılaştır
// Assert.Equal(beklenen, gerçek): önce beklenen yazılır.
public class YiginTestleri
{
    [Fact]
    public void Yeni_yigin_bostur()
    {
        var yigin = new Yigin<int>();

        Assert.Equal(0, yigin.Sayi);
        Assert.Empty(yigin);
    }

    [Fact]
    public void Son_eklenen_ilk_cikar()
    {
        var yigin = new Yigin<string>();
        yigin.Ekle("a");
        yigin.Ekle("b");

        string cikan = yigin.Cikar();

        Assert.Equal("b", cikan);
        Assert.Equal(1, yigin.Sayi);
    }

    [Fact]
    public void Gezinti_tepeden_baslar()
    {
        var yigin = new Yigin<int>();
        yigin.Ekle(1);
        yigin.Ekle(2);
        yigin.Ekle(3);

        Assert.Equal([3, 2, 1], yigin);
    }

    [Fact]
    public void Bos_yigindan_cikarmak_istisna_firlatir()
    {
        var yigin = new Yigin<int>();

        var hata = Assert.Throws<InvalidOperationException>(() => yigin.Cikar());

        Assert.Equal("Yığın boş.", hata.Message);
    }

    [Fact]
    public void Bos_yiginin_tepesine_bakmak_istisna_firlatir()
    {
        var yigin = new Yigin<int>();

        Assert.Throws<InvalidOperationException>(() => yigin.Tepe);
    }

    [Fact]
    public void Tepe_elemani_cikarmaz()
    {
        var yigin = new Yigin<int>();
        yigin.Ekle(7);

        int tepe = yigin.Tepe;

        Assert.Equal(7, tepe);
        Assert.Equal(1, yigin.Sayi);
    }

    [Fact]
    public void TryCikar_bos_yiginda_false_doner_ve_istisna_firlatmaz()
    {
        var yigin = new Yigin<string>();

        bool basarili = yigin.TryCikar(out _);

        Assert.False(basarili);
    }

    // Aynı test, farklı verilerle: her InlineData ayrı bir test olarak çalışır
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(100)]
    public void N_eleman_eklenip_N_kez_cikarilinca_yigin_bos_kalir(int n)
    {
        var yigin = new Yigin<int>();
        for (int i = 0; i < n; i++) yigin.Ekle(i);

        for (int i = 0; i < n; i++) yigin.Cikar();

        Assert.Equal(0, yigin.Sayi);
        Assert.False(yigin.TryCikar(out _));
    }
}
