// Asenkron akışlar: IAsyncEnumerable<T>, await foreach.
//
// 5. haftanın yineleyicisi (yield return) ile bu haftanın await'i aynı
// metotta buluşuyor. 04'teki gezinti bütün grafı gezip listeyi en sonda
// veriyordu. Buradaki ise her kitabı bulduğu anda verir; tüketici yeterince
// kitap aldığında durabilir ve geri kalan sorular hiç sorulmaz.
//
// Katalog her soruda bir satır yazıyor. Satırların arasına bakın: soru ne
// zaman soruluyor, kitap ne zaman tüketiciye ulaşıyor?
// Çalıştırmak için:  dotnet run 05-asenkron-akislar.cs

using System.Runtime.CompilerServices;

var katalog = new UzakKatalog();

Console.WriteLine("=== 1 === (akışı oluşturmak)");
IAsyncEnumerable<string> akis = Gezgin.KesfetAsync(katalog, "Huzur");
Console.WriteLine($"akış hazır, katalog çağrısı: {katalog.Cagri}");

Console.WriteLine("\n=== 2 === (ilk dört kitap)");
int alinan = 0;
await foreach (string kitap in akis)
{
    Console.WriteLine($"tüketici: {kitap}");
    if (++alinan == 4)
        break;
}
Console.WriteLine($"{alinan} kitap alındı, katalog çağrısı: {katalog.Cagri}");

Console.WriteLine("\n=== 3 === (tüketici iptal ediyor: WithCancellation)");
katalog = new UzakKatalog();
using var kaynak = new CancellationTokenSource();
alinan = 0;
try
{
    await foreach (string kitap in Gezgin.KesfetAsync(katalog, "Huzur").WithCancellation(kaynak.Token))
    {
        Console.WriteLine($"tüketici: {kitap}");
        if (++alinan == 2)
            kaynak.Cancel();
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine($"iptal edildi: {alinan} kitap, katalog çağrısı: {katalog.Cagri}");
}

static class Gezgin
{
    // [EnumeratorCancellation]: WithCancellation ile verilen token bu
    // parametreye bağlanır. Öznitelik olmazsa derleyici CS8425 uyarısı verir.
    public static async IAsyncEnumerable<string> KesfetAsync(
        UzakKatalog katalog, string baslangic,
        [EnumeratorCancellation] CancellationToken iptal = default)
    {
        var goruldu = new HashSet<string> { baslangic };
        var kuyruk = new Queue<string>();
        kuyruk.Enqueue(baslangic);

        while (kuyruk.TryDequeue(out var baslik))
        {
            yield return baslik;

            foreach (var komsu in await katalog.BaglantilarAsync(baslik, iptal))
                if (goruldu.Add(komsu))
                    kuyruk.Enqueue(komsu);
        }
    }
}

class UzakKatalog
{
    private static readonly Dictionary<string, string[]> Graf = new()
    {
        ["Huzur"] = ["Tutunamayanlar", "Saatleri Ayarlama Enstitüsü", "Aylak Adam"],
        ["Tutunamayanlar"] = ["Tehlikeli Oyunlar", "Huzur"],
        ["Saatleri Ayarlama Enstitüsü"] = ["Beş Şehir", "Huzur"],
        ["Aylak Adam"] = ["Anayurt Oteli"],
        ["Tehlikeli Oyunlar"] = ["Tutunamayanlar"],
        ["Beş Şehir"] = [],
        ["Anayurt Oteli"] = ["Aylak Adam"],
    };

    public int Cagri { get; private set; }

    public async Task<string[]> BaglantilarAsync(string baslik, CancellationToken iptal)
    {
        Cagri++;
        Console.WriteLine($"  katalog: {baslik} soruluyor");
        await Task.Delay(100, iptal);
        return Graf.GetValueOrDefault(baslik, []);
    }
}
