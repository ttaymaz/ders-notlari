// await satırında ne oluyor?
//
// Her bölüm birkaç satır yazdırıyor. Çalıştırmadan önce her bölümün
// çıktısını kâğıda yazın: hangi satır hangisinden önce gelir?
//
// 1. bölüm: bir async metot ilk BEKLEYEN await'e kadar çağıranın içinde
//    çalışır, sonra çağırana geri döner.
// 2. bölüm: zaten tamamlanmış bir görevi beklemek ne yapar?
// 3. bölüm: Task ile 5. haftanın yineleyicisi yan yana. Hangisi oluşturulur
//    oluşturulmaz çalışmaya başlar?
// 4. bölüm: sonuç döndüren görev; işlemciye bağlı iş için Task.Run.
// 5. bölüm: async metodun içinde fırlatılan istisna nereye gider?
// Çalıştırmak için:  dotnet run 01-await-sirasi.cs

Console.WriteLine("=== 1 === (await nerede durur?)");
Console.WriteLine("A");
Task t1 = Ornek.BekleyenAsync();
Console.WriteLine("C");
await t1;
Console.WriteLine("E");

Console.WriteLine("\n=== 2 === (tamamlanmış görevi beklemek)");
Console.WriteLine("1");
Task t2 = Ornek.HemenBitenAsync();
Console.WriteLine("4");
await t2;

Console.WriteLine("\n=== 3 === (görev ve yineleyici)");
Task gorev = Ornek.GorevAsync();
IEnumerable<int> dizi = Ornek.Dizi();
Console.WriteLine("ikisi de oluşturuldu");
await gorev;
foreach (int _ in dizi) { }

Console.WriteLine("\n=== 4 === (sonuç döndüren görev)");
int asal = await Task.Run(() => Ornek.AsalSay(200_000));
Console.WriteLine($"200000'den küçük asal: {asal}");

Console.WriteLine("\n=== 5 === (istisna görevin içinde kalır)");
Task hatali = Ornek.HataliAsync(-1);
Console.WriteLine($"çağrı döndü, durum: {hatali.Status}");
try
{
    await hatali;
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine($"await'te yakalandı: {e.ParamName}");
}

static class Ornek
{
    public static async Task BekleyenAsync()
    {
        Console.WriteLine("B");
        await Task.Delay(100);
        Console.WriteLine("D");
    }

    public static async Task HemenBitenAsync()
    {
        Console.WriteLine("2");
        await Task.CompletedTask;
        Console.WriteLine("3");
    }

    public static async Task GorevAsync()
    {
        Console.WriteLine("görev başladı");
        await Task.Delay(50);
        Console.WriteLine("görev bitti");
    }

    public static IEnumerable<int> Dizi()
    {
        Console.WriteLine("dizi başladı");
        yield return 1;
    }

    // İşlemciye bağlı iş: beklenecek bir G/Ç yok, yalnızca hesap
    public static int AsalSay(int sinir)
    {
        int sayi = 0;
        for (int n = 2; n < sinir; n++)
        {
            bool asal = true;
            for (int b = 2; b * b <= n; b++)
                if (n % b == 0) { asal = false; break; }
            if (asal) sayi++;
        }
        return sayi;
    }

    public static async Task HataliAsync(int adet)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(adet);
        await Task.Delay(10);
    }
}

