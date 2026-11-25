namespace EsLab;

// Geçen haftanın uzak kataloğu: bir kitabın "bunu okuyanlar şunları da
// okudu" bağlantılarını döndürür.
public interface IKatalog
{
    Task<IReadOnlyList<string>> BaglantilarAsync(string baslik, CancellationToken iptal = default);
}

// GÖREV 3: Sınırlı düzey düzey gezinti.
//
// Geçen haftanın düzey düzey gezintisi, ama aynı anda en çok `enCok` soru
// sürebilir. Bir düzeyde beş kitap ve enCok = 2 ise sorular ikişer ikişer
// gider. Gezinti sırası sınırsız gezintiyle aynıdır: önce başlangıç, sonra
// onun bağlantıları katalogun döndürdüğü sırayla, sonra onların
// bağlantıları... Her kitap en fazla bir kez sorulur.
//
// enCok 1'den küçükse ArgumentOutOfRangeException. Token her soruya iletilir.
//
// İpucu: SemaphoreSlim(enCok) ya da Parallel.ForEachAsync (04 dosyası).
// Parallel.ForEachAsync sonuçları sırayla vermez; sırayı kendiniz koruyun.
public static class SinirliGezgin
{
    public static Task<IReadOnlyList<string>> GezAsync(
        IKatalog katalog, string baslangic, int enCok, CancellationToken iptal = default)
    {
        throw new NotImplementedException();
    }
}
