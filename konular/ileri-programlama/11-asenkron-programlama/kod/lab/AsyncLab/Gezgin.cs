namespace AsyncLab;

// Katalog grafını genişliğine (BFS) gezen metotlar. Her metot başlangıç
// kitabından başlar; bir kitabı en fazla bir kez gezer ve en fazla bir kez
// sorar (graf döngülü olabilir). Sıra: önce başlangıç, sonra onun
// bağlantıları katalogun döndürdüğü sırayla, sonra onların bağlantıları...
public static class Gezgin
{
    // HAZIR: her soruyu sırayla bekleyen gezinti. Testleri baştan yeşildir.
    // Öteki görevlerde örnek olarak kullanın.
    public static async Task<IReadOnlyList<string>> GezSiraliAsync(
        IKatalog katalog, string baslangic, CancellationToken iptal = default)
    {
        ArgumentNullException.ThrowIfNull(katalog);

        var goruldu = new HashSet<string> { baslangic };
        var kuyruk = new Queue<string>();
        kuyruk.Enqueue(baslangic);
        var sira = new List<string>();

        while (kuyruk.TryDequeue(out var baslik))
        {
            iptal.ThrowIfCancellationRequested();
            sira.Add(baslik);
            foreach (var komsu in await katalog.BaglantilarAsync(baslik, iptal))
                if (goruldu.Add(komsu))
                    kuyruk.Enqueue(komsu);
        }
        return sira;
    }

    // GÖREV 1 ve 2: Düzey düzey gezinti.
    //
    // 1) Aynı düzeydeki kitapların sorularını BİRLİKTE başlatın ve
    //    Task.WhenAll ile birlikte bekleyin. Sonuç GezSiraliAsync ile aynı
    //    sırada olmalı.
    // 2) İptal: token'ı her soruya iletin. Yeni bir düzeye başlamadan önce
    //    iptal istenmiş mi bakın; istenmişse yeni soru sormayın.
    public static Task<IReadOnlyList<string>> GezDuzeyDuzeyAsync(
        IKatalog katalog, string baslangic, CancellationToken iptal = default)
    {
        throw new NotImplementedException();
    }

    // GÖREV 3: Asenkron akış.
    //
    // Kitapları bulundukları sırayla TEKER TEKER verin (yield return); bir
    // kitabın bağlantılarını, o kitap tüketiciye verildikten SONRA sorun.
    // Tüketici erken bırakırsa (break) geri kalan sorular hiç sorulmamalı.
    // WithCancellation ile verilen token katalog sorularına ulaşmalı.
    //
    // İpucu: metot imzasına async eklemeniz ve bir parametreye öznitelik
    // eklemeniz gerekecek (05-asenkron-akislar.cs).
    public static IAsyncEnumerable<string> KesfetAsync(
        IKatalog katalog, string baslangic, CancellationToken iptal = default)
    {
        throw new NotImplementedException();
    }

    // GÖREV 4: Hataya dayanıklı düzey düzey gezinti.
    //
    // Bir kitabın sorusu KatalogHatasi ile biterse gezinti durmasın: o
    // kitap Sira'da kalır, bağlantıları yok sayılır, başlığı Hatalilar'a
    // eklenir (gezinti sırasıyla). Aynı düzeyde birden çok hata olabilir;
    // hepsi listelenmeli. İptal ise hata değildir: OperationCanceledException
    // yutulmamalı, çağırana ulaşmalı.
    public static Task<GezintiSonucu> HatalariAtlayarakGezAsync(
        IKatalog katalog, string baslangic, CancellationToken iptal = default)
    {
        throw new NotImplementedException();
    }
}
