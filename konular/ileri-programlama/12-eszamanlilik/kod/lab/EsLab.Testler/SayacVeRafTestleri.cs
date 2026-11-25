using EsLab;

namespace EsLab.Testler;

// Hazır kodun, 1. ve 2. görevin testleri. Bu dosyayı DEĞİŞTİRMEYİN.
//
// Eşzamanlılık testi bir işi aynı anda çok kez yapar ve sonunda kuralın
// bozulmadığına bakar. Korumasız bir kod bu testlerden ÇOĞU ZAMAN kalır;
// "bir kez yeşil yandı" kodun doğru olduğunu göstermez. Testi birkaç kez
// çalıştırın.
public class SayacVeRafTestleri
{
    // ---- Hazır: KilitliSayac (baştan yeşil) ----

    [Fact]
    public void Yeni_sayac_sifirdan_baslar()
    {
        Assert.Equal(0, new KilitliSayac().Deger);
    }

    [Fact]
    public void Kilitli_sayac_ayni_anda_artirilinca_kayip_vermez()
    {
        var sayac = new KilitliSayac();

        Parallel.For(0, 200_000, _ => sayac.Artir());

        Assert.Equal(200_000, sayac.Deger);
    }

    // ---- GÖREV 1: OduncIstatistigi ----

    [Fact]
    public void Kaydedilmemis_kitabin_sayisi_sifirdir()
    {
        Assert.Equal(0, new OduncIstatistigi().Sayi("Huzur"));
    }

    [Fact]
    public void Ayni_anda_kaydedilenler_kaybolmaz()
    {
        var istatistik = new OduncIstatistigi();
        string[] kitaplar = ["Huzur", "Aylak Adam", "Beş Şehir", "Tutunamayanlar"];

        Parallel.For(0, 200_000, i => istatistik.Kaydet(kitaplar[i % 4]));

        Assert.All(kitaplar, k => Assert.Equal(50_000, istatistik.Sayi(k)));
    }

    [Fact]
    public void En_cok_okunanlar_sayiya_sonra_basliga_gore_siralanir()
    {
        var istatistik = new OduncIstatistigi();
        // Altı kitap eşit (3 kez); eşitler başlığa göre sıralanmalı.
        // Kitaplar bilerek ters sırayla kaydediliyor.
        string[] esitler = ["Çalıkuşu", "Tutunamayanlar", "Mahur Beste", "Huzur", "Aylak Adam", "Anayurt Oteli"];
        foreach (string kitap in esitler)
            for (int i = 0; i < 3; i++)
                istatistik.Kaydet(kitap);
        for (int i = 0; i < 5; i++)
            istatistik.Kaydet("Beş Şehir");

        var ilkDort = istatistik.EnCokOkunanlar(4);

        Assert.Equal([("Beş Şehir", 5), ("Anayurt Oteli", 3), ("Aylak Adam", 3), ("Huzur", 3)], ilkDort);
    }

    // ---- GÖREV 2: Raf ----

    [Fact]
    public async Task Kopya_varsa_verir_deftere_yazar()
    {
        var defter = new SahteDefter();
        var raf = new Raf("Huzur", kopya: 1, defter);

        var bes = TimeSpan.FromSeconds(5);   // kilit bırakılmazsa test asılı kalmasın
        Assert.True(await raf.OduncVerAsync("Ece").WaitAsync(bes));
        Assert.False(await raf.OduncVerAsync("Can").WaitAsync(bes));

        Assert.Equal(0, raf.Kopya);
        Assert.Equal([("Huzur", "Ece")], defter.Kayitlar);
    }

    [Fact]
    public async Task Ayni_anda_istenince_kopyadan_fazla_verilmez()
    {
        var defter = new SahteDefter();
        var raf = new Raf("Huzur", kopya: 10, defter);

        bool[] sonuclar = await Task.WhenAll(
            Enumerable.Range(1, 50).Select(no => raf.OduncVerAsync($"üye{no}")))
            .WaitAsync(TimeSpan.FromSeconds(5));   // kilit bırakılmazsa test asılı kalmasın

        Assert.Equal(10, sonuclar.Count(s => s));
        Assert.Equal(0, raf.Kopya);
        Assert.Equal(10, defter.Kayitlar.Count);
    }

    [Fact]
    public async Task Ayni_anda_istenince_her_uye_deftere_bir_kez_yazilir()
    {
        var defter = new SahteDefter();
        var raf = new Raf("Huzur", kopya: 50, defter);

        await Task.WhenAll(Enumerable.Range(1, 50).Select(no => raf.OduncVerAsync($"üye{no}")))
            .WaitAsync(TimeSpan.FromSeconds(5));

        Assert.Equal(50, defter.Kayitlar.Select(k => k.Uye).Distinct().Count());
        Assert.Equal(0, raf.Kopya);
    }
}
