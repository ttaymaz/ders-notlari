using Yapilar;

namespace Yapilar.Testler;

public class KuyrukTestleri
{
    [Fact]
    public void Ilk_giren_ilk_cikar()
    {
        var kuyruk = new Kuyruk<string>();
        kuyruk.Ekle("a");
        kuyruk.Ekle("b");

        Assert.Equal("a", kuyruk.Cikar());
        Assert.Equal("b", kuyruk.Cikar());
    }

    [Fact]
    public void Bos_kuyruktan_cikarmak_istisna_firlatir()
    {
        var kuyruk = new Kuyruk<int>();

        Assert.Throws<InvalidOperationException>(() => kuyruk.Cikar());
    }

    // Sınır durumu: kuyruk tamamen boşaldıktan sonra yeniden kullanılabilmeli.
    // Son elemanı çıkarırken _son'u boşaltmayı unutan bir kuyruk, bu testte
    // "Kuyruk boş." istisnası fırlatır.
    [Fact]
    public void Bosalan_kuyruga_yeniden_eklenebilir()
    {
        var kuyruk = new Kuyruk<int>();
        kuyruk.Ekle(1);
        kuyruk.Cikar();

        kuyruk.Ekle(2);

        Assert.Equal(2, kuyruk.Cikar());
        Assert.Empty(kuyruk);
    }

    [Theory]
    [InlineData(new[] { 5 })]
    [InlineData(new[] { 5, 6, 7 })]
    public void Gezinti_ekleme_sirasini_korur(int[] degerler)
    {
        var kuyruk = new Kuyruk<int>();
        foreach (int d in degerler) kuyruk.Ekle(d);

        Assert.Equal(degerler, kuyruk);
        Assert.Equal(degerler.Length, kuyruk.Sayi);
    }
}
