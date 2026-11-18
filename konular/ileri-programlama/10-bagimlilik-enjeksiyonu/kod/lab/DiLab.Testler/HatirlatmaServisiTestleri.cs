using DiLab;
using Microsoft.Extensions.Options;

namespace DiLab.Testler;

// GÖREV 4'ün testleri. Bu dosyayı DEĞİŞTİRMEYİN.
// Servisin bağımlılıklarının yerine bellekte bir depo, sahte bir saat ve
// elle yazılmış bir ayar veriliyor. Dönem projenizde servis testlerini de
// bu biçimde yazacaksınız.
public class HatirlatmaServisiTestleri
{
    private static readonly DateOnly Bugun = new(2026, 12, 1);

    private static HatirlatmaServisi ServisKur(int kacGunKala, params Odunc[] odunclar)
    {
        var depo = new BellekDepo<Odunc>();
        depo.YazAsync(odunclar).GetAwaiter().GetResult();
        return new HatirlatmaServisi(
            depo,
            new SabitSaat(Bugun),
            Options.Create(new HatirlatmaAyarlari { KacGunKala = kacGunKala }));
    }

    [Fact]
    public async Task Bos_depoda_hatirlatma_yoktur()
    {
        var servis = ServisKur(3);

        Assert.Empty(await servis.HatirlatmalarAsync());
    }

    [Fact]
    public async Task Sinir_gunu_dahildir()
    {
        var servis = ServisKur(3,
            new Odunc("Huzur", "Ece", Bugun.AddDays(3)),
            new Odunc("Aylak Adam", "Can", Bugun.AddDays(4)));

        Assert.Equal(["Ece: Huzur (3 gün)"], await servis.HatirlatmalarAsync());
    }

    [Fact]
    public async Task Bugun_son_gun_olan_dahil_geciken_haric()
    {
        var servis = ServisKur(3,
            new Odunc("Huzur", "Ece", Bugun),
            new Odunc("Aylak Adam", "Can", Bugun.AddDays(-1)));

        Assert.Equal(["Ece: Huzur (0 gün)"], await servis.HatirlatmalarAsync());
    }

    [Fact]
    public async Task Kalan_gune_sonra_basliga_gore_siralanir()
    {
        var servis = ServisKur(5,
            new Odunc("Huzur", "Ece", Bugun.AddDays(2)),
            new Odunc("Tutunamayanlar", "Can", Bugun.AddDays(1)),
            new Odunc("Aylak Adam", "Ada", Bugun.AddDays(2)));

        Assert.Equal(
        [
            "Can: Tutunamayanlar (1 gün)",
            "Ada: Aylak Adam (2 gün)",
            "Ece: Huzur (2 gün)",
        ], await servis.HatirlatmalarAsync());
    }

    [Fact]
    public async Task Gun_sayisi_ayardan_okunur()
    {
        var odunc = new Odunc("Huzur", "Ece", Bugun.AddDays(7));

        Assert.Empty(await ServisKur(6, odunc).HatirlatmalarAsync());
        Assert.Single(await ServisKur(7, odunc).HatirlatmalarAsync());
    }
}
