using System.Diagnostics;
using BasarimLab;

namespace BasarimLab.Testler;

// Bütün görevlerin testleri. Bu dosyayı DEĞİŞTİRMEYİN.
//
// Bu testler süre ölçmez; bellek ölçer. Ayrılan bayt sayısı aynı .NET
// sürümünde her çalıştırmada aynıdır, bu yüzden testler makinenin hızına
// bağlı değildir. Tek istisna 3. görevin hız testi: orada sınır, doğru
// çözümün süresinin yüzlerce katı.
public class BasarimTestleri
{
    // ---- Hazır: Olcum (baştan yeşil) ----

    [Fact]
    public void Bos_is_sifir_bayt_ayirir()
    {
        Assert.Equal(0, Olcum.AyrilanBayt(() => { }));
    }

    [Fact]
    public void On_elemanli_int_dizisi_64_bayttir()
    {
        // 24 bayt dizi başlığı + 10 × 4 bayt
        Assert.Equal(64, Olcum.AyrilanBayt(() => _ = new int[10]));
    }

    // ---- GÖREV 1: RafKodu ----

    [Fact]
    public void Gecerli_kod_ayristirilir()
    {
        Assert.True(RafKodu.TryAyristir("TR-813-07", out int bolum, out int raf));
        Assert.Equal((813, 7), (bolum, raf));
    }

    [Theory]
    [InlineData("tr-813-07")]
    [InlineData("TR-81-07")]
    [InlineData("TR-8130-07")]
    [InlineData("TR-813-7")]
    [InlineData("TR-813_07")]
    [InlineData("TR-8a3-07")]
    [InlineData(" TR-813-07")]
    [InlineData("")]
    public void Gecersiz_kod_reddedilir(string metin)
    {
        Assert.False(RafKodu.TryAyristir(metin, out int bolum, out int raf));
        Assert.Equal((0, 0), (bolum, raf));
    }

    [Fact]
    public void Ayristirma_bellek_ayirmaz()
    {
        string gecerli = "TR-813-07", gecersiz = "TR-81-07";

        Assert.Equal(0, Olcum.AyrilanBayt(() => RafKodu.TryAyristir(gecerli, out _, out _)));
        Assert.Equal(0, Olcum.AyrilanBayt(() => RafKodu.TryAyristir(gecersiz, out _, out _)));
    }

    // ---- GÖREV 2: HalkaTampon ----

    [Fact]
    public void Dolmadan_ekleme_sirasini_korur()
    {
        var tampon = new HalkaTampon<int>(5);
        tampon.Ekle(1);
        tampon.Ekle(2);
        tampon.Ekle(3);

        Assert.Equal(3, tampon.Sayi);
        Assert.Equal(5, tampon.Kapasite);
        Assert.Equal([1, 2, 3], tampon.ToArray());
    }

    [Fact]
    public void Doluyken_en_eskinin_yerine_yazar()
    {
        var tampon = new HalkaTampon<string>(3);
        foreach (string s in new[] { "a", "b", "c", "d", "e" })
            tampon.Ekle(s);

        Assert.Equal(3, tampon.Sayi);
        Assert.Equal(["c", "d", "e"], tampon.ToArray());
    }

    [Fact]
    public void Bos_tamponun_dizisi_bostur()
    {
        Assert.Empty(new HalkaTampon<int>(4).ToArray());
    }

    [Fact]
    public void Ekleme_bellek_ayirmaz()
    {
        var tampon = new HalkaTampon<int>(100);

        long ayrilan = Olcum.AyrilanBayt(() =>
        {
            for (int i = 0; i < 10_000; i++)
                tampon.Ekle(i);
        });

        Assert.Equal(0, ayrilan);
        Assert.Equal(Enumerable.Range(9_900, 100), tampon.ToArray());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Kapasite_birden_kucukse_istisna(int kapasite)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new HalkaTampon<int>(kapasite));
    }

    // ---- GÖREV 3: KitapIndeksi ----

    [Fact]
    public void Isbn_ile_bulunur_yoksa_null()
    {
        var indeks = new KitapIndeksi();
        indeks.Ekle(new Kitap("975-1", "Huzur", "Tanpınar"));

        Assert.Equal("Huzur", indeks.Bul("975-1")?.Baslik);
        Assert.Null(indeks.Bul("975-9"));
    }

    [Fact]
    public void Yazarin_kitaplari_ekleme_sirasiyla()
    {
        var indeks = new KitapIndeksi();
        indeks.Ekle(new Kitap("1", "Huzur", "Tanpınar"));
        indeks.Ekle(new Kitap("2", "Aylak Adam", "Atılgan"));
        indeks.Ekle(new Kitap("3", "Beş Şehir", "Tanpınar"));

        Assert.Equal(["Huzur", "Beş Şehir"], indeks.YazarinKitaplari("Tanpınar").Select(k => k.Baslik));
        Assert.Empty(indeks.YazarinKitaplari("Oğuz Atay"));
    }

    [Fact]
    public void Ayni_isbn_ikinci_kez_eklenemez()
    {
        var indeks = new KitapIndeksi();
        indeks.Ekle(new Kitap("1", "Huzur", "Tanpınar"));

        Assert.Throws<ArgumentException>(() => indeks.Ekle(new Kitap("1", "Başka", "Başka")));
    }

    [Fact]
    public void Elli_bin_kitapta_arama_bir_saniyeden_kisa()
    {
        var kitaplar = Enumerable.Range(0, 50_000)
            .Select(i => new Kitap($"isbn-{i}", $"kitap-{i}", $"yazar-{i % 500}"))
            .ToList();
        var indeks = new KitapIndeksi();

        var saat = Stopwatch.StartNew();
        foreach (var k in kitaplar)
            indeks.Ekle(k);
        int bulunan = 0;
        foreach (var k in kitaplar)
        {
            if (indeks.Bul(k.Isbn) is not null) bulunan++;
            _ = indeks.YazarinKitaplari(k.Yazar);
        }
        saat.Stop();

        Assert.Equal(50_000, bulunan);
        Assert.Equal(100, indeks.YazarinKitaplari("yazar-7").Count);
        Assert.True(saat.ElapsedMilliseconds < 1000, $"{saat.ElapsedMilliseconds} ms sürdü");
    }

    // ---- GÖREV 4: RaporYazici ----

    [Fact]
    public void Rapor_bicimi()
    {
        Odunc[] odunclar = [new("Ece", "Huzur", 3), new("Can", "Aylak Adam", 0)];

        Assert.Equal("Ece\tHuzur\t3\nCan\tAylak Adam\t0", RaporYazici.Yaz(odunclar));
        Assert.Equal("", RaporYazici.Yaz([]));
    }

    [Fact]
    public void Rapor_az_bellekle_uretilir()
    {
        var odunclar = Enumerable.Range(0, 2000)
            .Select(i => new Odunc($"uye{i}", $"kitap{i}", i % 30))
            .ToList();

        string rapor = "";
        long ayrilan = Olcum.AyrilanBayt(() => rapor = RaporYazici.Yaz(odunclar));
        long metinBoyutu = 2L * rapor.Length;   // UTF-16: karakter başına 2 bayt

        Assert.Equal(2000, rapor.Split('\n').Length);
        Assert.True(ayrilan < 5 * metinBoyutu, $"{ayrilan:N0} bayt ayrıldı, metin {metinBoyutu:N0} bayt");
    }
}
