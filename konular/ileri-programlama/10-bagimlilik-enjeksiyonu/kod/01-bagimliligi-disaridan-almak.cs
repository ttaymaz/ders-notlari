// Bağımlılığı dışarıdan almak: saati kim veriyor?
//
// Gecikme cezası "bugün"e bağlı. Saati sınıfın İÇİNDE sorarsanız
// (DateTime.Now) sınıfı yalnızca bugünün tarihiyle deneyebilirsiniz; yarın
// başka sonuç verir, geçen ayın durumunu hiç deneyemezsiniz.
//
// Burada GecikmeHesabi saati KURUCUDAN alıyor. .NET 8 ile gelen TimeProvider
// soyut sınıfı tam bunun için var: uygulamada TimeProvider.System (gerçek
// saat), denemede istediğimiz anı söyleyen sahte bir saat.
//
// Aynı hesap, üç farklı "bugün" ile çalıştırılıyor. Çıktıyı tahmin edin.
// Çalıştırmak için:  dotnet run 01-bagimliligi-disaridan-almak.cs

var kural = new CezaKurali(GunlukUcret: 3, Tavan: 60);
var sonTarih = new DateOnly(2026, 11, 20);

foreach (var bugun in new[] { new DateOnly(2026, 11, 19), new DateOnly(2026, 11, 25), new DateOnly(2027, 1, 30) })
{
    var hesap = new GecikmeHesabi(new SabitSaat(bugun), kural);
    Console.WriteLine($"bugün {bugun:yyyy-MM-dd} → ceza {hesap.Ceza(sonTarih)} TL");
}

// Uygulamada aynı sınıf gerçek saatle kurulur; sınıfın kodu değişmez:
var gercek = new GecikmeHesabi(TimeProvider.System, kural);
Console.WriteLine($"gerçek saatle de kurulabiliyor: {gercek.GetType().Name}");

record CezaKurali(int GunlukUcret, int Tavan);

class GecikmeHesabi(TimeProvider saat, CezaKurali kural)
{
    public int Ceza(DateOnly sonTarih)
    {
        var bugun = DateOnly.FromDateTime(saat.GetUtcNow().UtcDateTime);
        int gecikme = bugun.DayNumber - sonTarih.DayNumber;
        if (gecikme <= 0) return 0;
        return Math.Min(gecikme * kural.GunlukUcret, kural.Tavan);
    }
}

// Denemeler için sahte saat: her zaman verilen günün öğlesini söyler
class SabitSaat(DateOnly gun) : TimeProvider
{
    public override DateTimeOffset GetUtcNow()
        => new(gun.ToDateTime(new TimeOnly(12, 0)), TimeSpan.Zero);
}
