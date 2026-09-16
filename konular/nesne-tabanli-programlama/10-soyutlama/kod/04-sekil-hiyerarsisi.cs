// Klasik örnek: geometrik şekiller.
//
// Soyutlamanın ders kitaplarındaki standart örneği. Burada olmasının
// sebebi tanıdık olması — sınavda ve iş görüşmelerinde karşınıza çıkar.
//
// Çalıştırmak için:  dotnet run 04-sekil-hiyerarsisi.cs

Sekil[] sekiller =
{
    new Daire(5),
    new Kare(4),
    new Dikdortgen(3, 7),
    new Ucgen(6, 4),
};

Console.WriteLine($"{"Şekil",-12} {"Alan",10} {"Çevre",10}");
Console.WriteLine(new string('-', 34));

double toplamAlan = 0;
foreach (Sekil s in sekiller)
{
    Console.WriteLine($"{s.Ad(),-12} {s.Alan(),10:F2} {s.Cevre(),10:F2}");
    toplamAlan = toplamAlan + s.Alan();
}

Console.WriteLine(new string('-', 34));
Console.WriteLine($"{"TOPLAM",-12} {toplamAlan,10:F2}");

Console.WriteLine("\n--- En büyük alanlı şekil ---");
Sekil enBuyuk = sekiller[0];
foreach (Sekil s in sekiller)
{
    if (s.Alan() > enBuyuk.Alan())
    {
        enBuyuk = s;
    }
}
Console.WriteLine($"{enBuyuk.Ad()} — {enBuyuk.Alan():F2}");


abstract class Sekil
{
    // Her şeklin alanı ve çevresi VARDIR, ama ortak bir formül YOKTUR.
    // Tam olarak abstract'ın tanımı.
    public abstract double Alan();

    public abstract double Cevre();

    public abstract string Ad();

    // Ortak davranış: her şekil kendini aynı biçimde tanıtabilir.
    public override string ToString()
    {
        return $"{Ad()} (alan {Alan():F2})";
    }
}


class Daire : Sekil
{
    private double yaricap;

    public Daire(double yaricap)
    {
        this.yaricap = yaricap;
    }

    public override double Alan()
    {
        return Math.PI * yaricap * yaricap;
    }

    public override double Cevre()
    {
        return 2 * Math.PI * yaricap;
    }

    public override string Ad() { return "Daire"; }
}


class Dikdortgen : Sekil
{
    protected double kenarA;
    protected double kenarB;

    public Dikdortgen(double kenarA, double kenarB)
    {
        this.kenarA = kenarA;
        this.kenarB = kenarB;
    }

    public override double Alan()
    {
        return kenarA * kenarB;
    }

    public override double Cevre()
    {
        return 2 * (kenarA + kenarB);
    }

    public override string Ad() { return "Dikdörtgen"; }
}


// Kare bir dikdörtgendir — is-a testi geçiyor.
// Dikdörtgenin alan ve çevre hesabını devralıyor, yalnızca kuruluşu farklı.
class Kare : Dikdortgen
{
    public Kare(double kenar) : base(kenar, kenar)
    {
    }

    public override string Ad() { return "Kare"; }
}


class Ucgen : Sekil
{
    private double taban;
    private double yukseklik;

    public Ucgen(double taban, double yukseklik)
    {
        this.taban = taban;
        this.yukseklik = yukseklik;
    }

    public override double Alan()
    {
        return taban * yukseklik / 2;
    }

    // Yalnızca taban ve yükseklik biliniyorsa çevre hesaplanamaz.
    // Soyut metodu yazmak ZORUNDAYIZ; dürüst bir cevap veriyoruz.
    public override double Cevre()
    {
        return 0;
    }

    public override string Ad() { return "Üçgen"; }
}

// --- DİKKAT: Kare : Dikdortgen ---
//
// Kare, Sekil'den değil Dikdortgen'den türüyor. Sebebi is-a testi:
// "Bir kare, bir dikdörtgendir" — doğru. Böylece alan ve çevre
// hesaplarını yeniden yazmıyoruz.
//
// Hiyerarşi üç katmanlı oldu: Sekil → Dikdortgen → Kare. Beşinci
// haftada "hiyerarşiyi sığ tutun" demiştik; üç katman hâlâ makul.
//
// --- ÜÇGENİN ÇEVRESİ ---
//
// Ucgen sınıfı Cevre metodunu yazmak zorunda ama taban ve yükseklikle
// çevre hesaplanamaz. Bu, soyut metodun bir sınırını gösteriyor:
// sözleşme dayatır ama anlamlı bir cevabı garanti edemez.
//
// Daha iyi tasarım, üçgeni üç kenarıyla kurmak olurdu. Sınıf tasarımı
// yaparken hangi verinin saklanacağı, hangi soruların cevaplanabileceğini
// belirler — bu, on dördüncü haftanın konusu.
//
// --- DENEYİN ---
//
// Sekil dizisine yeni bir şekil ekleyin: Altigen (düzgün altıgen,
// kenar uzunluğu verilsin). Rapor döngüsüne dokunmanız gerekti mi?
