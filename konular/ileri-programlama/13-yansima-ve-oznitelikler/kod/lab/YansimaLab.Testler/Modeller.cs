using YansimaLab;

namespace YansimaLab.Testler;

// Testlerin kullandığı örnek sınıflar. Bu dosyayı DEĞİŞTİRMEYİN.

public sealed class UyeKaydi
{
    [Zorunlu]
    [EnFazlaUzunluk(10)]
    public string? Ad { get; set; } = "Ece";

    [Zorunlu]
    public string? Eposta { get; set; } = "ece@ornek.tr";

    [Aralik(1, 5)]
    public int KitapSiniri { get; set; } = 3;

    public string? Not { get; set; }   // etiketsiz: hiç denetlenmez
}

public sealed class KitapRafi
{
    private readonly List<string> _kitaplar = [];

    [Komut("ekle")]
    public string Ekle(string baslik, int adet)
    {
        for (int i = 0; i < adet; i++)
            _kitaplar.Add(baslik);
        return $"{adet} × {baslik}";
    }

    [Komut("say")]
    public int Say() => _kitaplar.Count;

    [Komut("temizle")]
    public void Temizle() => _kitaplar.Clear();

    [Komut("ortalama")]
    public double Ortalama(double toplam, int adet)
        => adet == 0 ? throw new DivideByZeroException("Adet sıfır olamaz.") : toplam / adet;

    public string Etiketsiz() => "görünmemeli";
}

public sealed class OduncKaydi
{
    public string Uye { get; set; } = "Ece";
    public string Baslik { get; set; } = "Huzur";
    public int GecikmeGun { get; set; }
    public string? Not { get; set; }
}

// 3. görevin koşucusunun bulacağı sınamalar. Test derlemesinde [Sinama]
// taşıyan başka metot yoktur.
public class OrnekSinamalar
{
    private int _sayac;

    [Sinama]
    public void Gecen() { }

    [Sinama]
    public void Kalan() => throw new InvalidOperationException("beklenen 4, gelen 3");

    [Sinama]
    public async Task AsenkronGecen() => await Task.Delay(1);

    [Sinama]
    public async Task AsenkronKalan()
    {
        await Task.Delay(1);
        throw new InvalidOperationException("asenkron hata");
    }

    [Sinama]
    [Atla("çok yavaş")]
    public void Atlanan() => throw new InvalidOperationException("çalışmamalıydı");

    [Sinama]
    public void YeniNesne1()
    {
        _sayac++;
        if (_sayac != 1)
            throw new InvalidOperationException("nesne paylaşıldı");
    }

    [Sinama]
    public void YeniNesne2()
    {
        _sayac++;
        if (_sayac != 1)
            throw new InvalidOperationException("nesne paylaşıldı");
    }

    public void Etiketsiz() => throw new InvalidOperationException("çalışmamalıydı");
}
