// Eşzamanlı koleksiyonlar: kilidi koleksiyonun kendisi tutsun.
//
// Sekiz görev ödünç kayıtlarını işleyip kitap başına ödünç sayısını
// tutuyor. Dictionary<TKey, TValue> aynı anda yazılmaya dayanıklı değildir:
// sayılar kaybolur ya da sözlüğün iç yapısı bozulur. Burada onu
// çalıştırmıyoruz; README'deki "Denemeniz için" bölümünde deneyebilirsiniz.
//
// ConcurrentDictionary her işlemi kendi içinde korur. Ama iki ayrı işlemi
// (önce oku, sonra yaz) bir araya getirmek yine bizim işimiz: 2. bölüm.
// Çalıştırmak için:  dotnet run 03-esanli-koleksiyonlar.cs

using System.Collections.Concurrent;

string[] kitaplar = ["Huzur", "Aylak Adam", "Beş Şehir", "Tutunamayanlar"];

// 400 000 ödünç kaydı: her kitap 100 000 kez
var kayitlar = Enumerable.Range(0, 400_000).Select(i => kitaplar[i % kitaplar.Length]).ToArray();
var parcalar = kayitlar.Chunk(50_000).ToArray();   // sekiz görev, sekiz parça

Console.WriteLine("=== 1 === (ConcurrentDictionary.AddOrUpdate)");
var sayilar = new ConcurrentDictionary<string, int>();
await Task.WhenAll(parcalar.Select(parca => Task.Run(() =>
{
    foreach (string kitap in parca)
        sayilar.AddOrUpdate(kitap, 1, (_, eski) => eski + 1);
})));
foreach (string kitap in kitaplar)
    Console.WriteLine($"{kitap,-15} {sayilar[kitap]}");

Console.WriteLine("\n=== 2 === (iki ayrı işlem: TryGetValue sonra indeksleyici)");
var ikiAdim = new ConcurrentDictionary<string, int>();
await Task.WhenAll(parcalar.Select(parca => Task.Run(() =>
{
    foreach (string kitap in parca)
    {
        ikiAdim.TryGetValue(kitap, out int eski);   // her biri tek başına güvenli...
        ikiAdim[kitap] = eski + 1;                  // ...ama ikisinin arası değil
    }
})));
Console.WriteLine($"toplam 400000 mü? {ikiAdim.Values.Sum() == 400_000}");

Console.WriteLine("\n=== 3 === (ConcurrentQueue: aynı anda ekleyip almak)");
var kuyruk = new ConcurrentQueue<int>();
var alinan = new ConcurrentBag<int>();
Task ekleyen = Task.Run(() =>
{
    for (int i = 0; i < 100_000; i++)
        kuyruk.Enqueue(i);
});
Task alan = Task.Run(async () =>
{
    while (alinan.Count < 100_000)
    {
        if (kuyruk.TryDequeue(out int sayi))
            alinan.Add(sayi);
        else
            await Task.Yield();   // kuyruk şimdilik boş; biraz sonra yine bak
    }
});
await Task.WhenAll(ekleyen, alan);
Console.WriteLine($"alınan {alinan.Count}, farklı {alinan.Distinct().Count()}, kuyrukta kalan {kuyruk.Count}");
