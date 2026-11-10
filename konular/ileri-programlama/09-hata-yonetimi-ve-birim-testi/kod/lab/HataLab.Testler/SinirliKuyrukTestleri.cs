using HataLab;

namespace HataLab.Testler;

// GÖREV 1'in testleri. Bu dosyayı DEĞİŞTİRMEYİN; testler geçene kadar
// HataLab/SinirliKuyruk.cs dosyasını düzenleyin.
public class SinirliKuyrukTestleri
{
    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void Gecersiz_kapasite_reddedilir(int kapasite)
    {
        var hata = Assert.Throws<ArgumentOutOfRangeException>(() => new SinirliKuyruk<int>(kapasite));

        Assert.Equal("kapasite", hata.ParamName);
    }

    [Fact]
    public void Ilk_giren_ilk_cikar()
    {
        var kuyruk = new SinirliKuyruk<string>(3);
        kuyruk.Ekle("a");
        kuyruk.Ekle("b");

        Assert.Equal("a", kuyruk.Cikar());
        Assert.Equal(1, kuyruk.Sayi);
    }

    [Fact]
    public void Null_eklenemez()
    {
        var kuyruk = new SinirliKuyruk<string>(3);

        var hata = Assert.Throws<ArgumentNullException>(() => kuyruk.Ekle(null!));

        Assert.Equal("deger", hata.ParamName);
    }

    [Fact]
    public void Dolu_kuyruga_eklemek_istisna_firlatir()
    {
        var kuyruk = new SinirliKuyruk<int>(2);
        kuyruk.Ekle(1);
        kuyruk.Ekle(2);

        var hata = Assert.Throws<InvalidOperationException>(() => kuyruk.Ekle(3));

        Assert.Equal("Kuyruk dolu.", hata.Message);
        Assert.Equal(2, kuyruk.Sayi);
    }

    [Fact]
    public void TryEkle_dolu_kuyrukta_false_doner()
    {
        var kuyruk = new SinirliKuyruk<int>(1);

        Assert.True(kuyruk.TryEkle(1));
        Assert.False(kuyruk.TryEkle(2));
        Assert.Equal(1, kuyruk.Sayi);
    }

    [Fact]
    public void TryEkle_null_icin_yine_istisna_firlatir()
    {
        var kuyruk = new SinirliKuyruk<string>(1);

        Assert.Throws<ArgumentNullException>(() => kuyruk.TryEkle(null!));
    }

    [Fact]
    public void Bos_kuyruktan_cikarmak_istisna_firlatir()
    {
        var kuyruk = new SinirliKuyruk<int>(1);

        var hata = Assert.Throws<InvalidOperationException>(() => kuyruk.Cikar());

        Assert.Equal("Kuyruk boş.", hata.Message);
    }

    // Sınır durumu: doldu, boşaldı, yeniden dolabiliyor mu?
    [Fact]
    public void Cikarilan_elemanin_yeri_yeniden_kullanilir()
    {
        var kuyruk = new SinirliKuyruk<int>(2);
        kuyruk.Ekle(1);
        kuyruk.Ekle(2);
        kuyruk.Cikar();

        kuyruk.Ekle(3);

        Assert.Equal(2, kuyruk.Cikar());
        Assert.Equal(3, kuyruk.Cikar());
    }
}
