// Taşıyıcı: kuyruk yeniden, bu kez iki ucunda iki ayrı görev.
//
// 5. haftanın kuyruğunda ekleyen ve alan aynı koddu. Burada bir görev
// kuyruğun bir ucuna iade edilen kitapları yazıyor (üretici), başka bir
// görev öteki ucundan alıp rafa yerleştiriyor (tüketici). İkisi aynı anda
// çalışıyor. Bu işi .NET'te Channel<T> yapar: eşzamanlı, asenkron bir kuyruk.
//
// Kanalın kapasitesi 2: içinde iki kitap varken üretici bir üçüncüsünü
// yazmak isterse BEKLER. Böylece hızlı üretici yavaş tüketiciyi boğamaz.
//
// Süreler makineye göre birkaç milisaniye oynar (bekleme hep biraz uzun
// sürer, hiç kısa sürmez); dosya süreyi bir rafa yerleştirme süresinin
// (200 ms) katına AŞAĞI yuvarlayarak yazıyor.
// Çalıştırmak için:  dotnet run 05-kanallar.cs

using System.Diagnostics;
using System.Threading.Channels;

string[] iadeler = ["Huzur", "Aylak Adam", "Beş Şehir", "Tutunamayanlar", "Anayurt Oteli", "Tehlikeli Oyunlar"];

Console.WriteLine("=== 1 === (bir üretici, bir tüketici, kapasite 2)");
var sonuc = await Iade.CalistirAsync(iadeler, kapasite: 2, tuketiciSayisi: 1);
Console.WriteLine($"rafa giden sıra: {string.Join(", ", sonuc.Sira)}");
Console.WriteLine($"kanalda en çok {sonuc.EnCokBekleyen} kitap bekledi · süre ≈ {sonuc.Sure} yerleştirme");

Console.WriteLine("\n=== 2 === (iki tüketici)");
sonuc = await Iade.CalistirAsync(iadeler, kapasite: 2, tuketiciSayisi: 2);
Console.WriteLine($"rafa giden: {sonuc.Sira.Count} kitap, hepsi farklı mı? {sonuc.Sira.Distinct().Count() == iadeler.Length}");
Console.WriteLine($"süre ≈ {sonuc.Sure} yerleştirme");

static class Iade
{
    public const int YerlestirmeMs = 200;

    public static async Task<(List<string> Sira, int EnCokBekleyen, long Sure)> CalistirAsync(
        string[] iadeler, int kapasite, int tuketiciSayisi)
    {
        var kanal = Channel.CreateBounded<string>(kapasite);
        var sira = new List<string>();
        object kilit = new();
        int enCokBekleyen = 0;
        var saat = Stopwatch.StartNew();

        async Task Uretici()
        {
            foreach (string kitap in iadeler)
            {
                await kanal.Writer.WriteAsync(kitap);   // kanal doluysa burada bekler
                enCokBekleyen = Math.Max(enCokBekleyen, kanal.Reader.Count);
            }
            kanal.Writer.Complete();   // "başka kitap gelmeyecek"
        }

        async Task Tuketici()
        {
            await foreach (string kitap in kanal.Reader.ReadAllAsync())
            {
                await Task.Delay(YerlestirmeMs);   // rafa yerleştirmek zaman alır
                lock (kilit) sira.Add(kitap);
            }
        }

        var tuketiciler = Enumerable.Range(0, tuketiciSayisi).Select(_ => Tuketici()).ToList();
        await Uretici();
        await Task.WhenAll(tuketiciler);

        long sure = (long)Math.Floor(saat.Elapsed.TotalMilliseconds / YerlestirmeMs);
        return (sira, enCokBekleyen, sure);
    }
}
