// Çıktı tahmini — on blok, on dört hafta.
//
// ÖNCE kağıda yazın, SONRA çalıştırın:  dotnet run 01-cikti-tahmini.cs
//
// Yanıldığınız blok, dönmeniz gereken haftayı gösterir. Eşleme
// kod/README.md dosyasında.

Console.WriteLine("--- 1 --- (hafta 1: nesneler bağımsız)");
Bardak b1 = new Bardak();
Bardak b2 = new Bardak();
b1.Dolu = 200;
b2.Dolu = b1.Dolu + 50;
b1.Bosalt();
Console.WriteLine($"{b1.Dolu} {b2.Dolu}");

Console.WriteLine("\n--- 2 --- (hafta 2: alan başlangıcı kurucudan önce işlenir)");
Bilet bt = new Bilet(3);
Console.WriteLine($"{bt.Adet} {bt.Ucret}");

Console.WriteLine("\n--- 3 --- (hafta 3: set sınıra çeker, reddetmez)");
Kupon kp = new Kupon();
kp.Oran = 150;
Console.Write(kp.Oran + " ");
kp.Oran = 20;
Console.WriteLine(kp.Oran);

Console.WriteLine("\n--- 4 --- (hafta 4: paylaşılan alan, kendi alanı)");
Gise g1 = new Gise();
Gise g2 = new Gise();
g1.Islem();
g2.Islem();
g2.Islem();
Console.WriteLine($"{g1.Kendi} {g2.Kendi} {Gise.Toplam}");

Console.WriteLine("\n--- 5 --- (hafta 5: temel kurucu önce çalışır)");
Ogrenci og = new Ogrenci("Ayşe", 3);
Console.WriteLine(og.Etiket);

Console.WriteLine("\n--- 6 --- (hafta 6: ezme mi gizleme mi)");
Olcer sn = new IsiOlcer();
Console.WriteLine($"{sn.Oku()} {sn.Bilgi()}");

Console.WriteLine("\n--- 7 --- (hafta 9: polimorfizm, temel tip değişken)");
Sekil[] sekiller = { new Kare(4), new Dikdortgen(3, 5) };
foreach (Sekil s in sekiller)
{
    Console.Write($"{s.Alan()} ");
}
Console.WriteLine();

Console.WriteLine("\n--- 8 --- (hafta 10-11: abstract ve arayüz)");
foreach (Sekil s in sekiller)
{
    if (s is ICizilebilir ciz) { ciz.Ciz(); }
    else { Console.Write("[çizilemez] "); }
}
Console.WriteLine();

Console.WriteLine("\n--- 9 --- (hafta 12: struct ile class)");
NoktaS a1 = new NoktaS(1, 1);
NoktaS a2 = a1;
a2.X = 9;

NoktaC c1 = new NoktaC(1, 1);
NoktaC c2 = c1;
c2.X = 9;

Console.WriteLine($"struct: {a1.X}  class: {c1.X}");

Console.WriteLine("\n--- 10 --- (hafta 13: liste ve sözlük)");
List<string> liste = new List<string> { "a", "b", "c" };
liste.Insert(1, "x");
liste.Remove("c");

Dictionary<string, int> sozluk = new Dictionary<string, int>();
sozluk["bir"] = 1;
sozluk["bir"] = 2;

Console.WriteLine($"{string.Join("", liste)} {liste.Count} {sozluk.Count} {sozluk["bir"]}");


// ===================== SINIFLAR =====================

class Bardak
{
    public int Dolu;
    public void Bosalt() { Dolu = 0; }
}

class Bilet
{
    public int Adet;
    public decimal Ucret = 50m;
    public Bilet(int adet) { Adet = adet; Ucret = Ucret * adet; }
}

class Kupon
{
    private int oran;
    public int Oran
    {
        get { return oran; }
        set { oran = value > 100 ? 100 : value; }
    }
}

class Gise
{
    public static int Toplam;
    public int Kendi;
    public void Islem() { Kendi++; Toplam++; }
}

class Kisi
{
    public string Etiket;
    public Kisi(string ad) { Etiket = "K:" + ad; }
}

class Ogrenci : Kisi
{
    public Ogrenci(string ad, int sinif) : base(ad)
    {
        Etiket = Etiket + "/S" + sinif;
    }
}

class Olcer
{
    public virtual string Oku() { return "olcer-oku"; }
    public string Bilgi() { return "olcer-bilgi"; }
}

class IsiOlcer : Olcer
{
    public override string Oku() { return "isi-oku"; }
    public new string Bilgi() { return "isi-bilgi"; }
}

interface ICizilebilir
{
    void Ciz();
}

abstract class Sekil
{
    public abstract double Alan();
}

class Kare : Sekil, ICizilebilir
{
    private readonly double kenar;
    public Kare(double kenar) { this.kenar = kenar; }
    public override double Alan() { return kenar * kenar; }
    public void Ciz() { Console.Write("[kare] "); }
}

class Dikdortgen : Sekil
{
    private readonly double en;
    private readonly double boy;
    public Dikdortgen(double en, double boy) { this.en = en; this.boy = boy; }
    public override double Alan() { return en * boy; }
}

struct NoktaS
{
    public int X;
    public int Y;
    public NoktaS(int x, int y) { X = x; Y = y; }
}

class NoktaC
{
    public int X;
    public int Y;
    public NoktaC(int x, int y) { X = x; Y = y; }
}

// --- Denemeniz için ---
//
// 1. Her bloğun çıktısını çalıştırmadan önce kağıda yazın. Kaçında
//    yanıldınız? Yanıldığınız blokların haftalarına dönün.
//
// 2. 2. blokta `Ucret = Ucret * adet;` satırı 50'yi mi yoksa 0'ı mı
//    çarpıyor? Alan başlangıç değeri kurucudan önce mi sonra mı işlenir?
//
// 3. 3. blokta `set` bloğu geçersiz değeri REDDETMİYOR, sınıra ÇEKİYOR.
//    `oran = value > 100 ? 100 : value;` yerine `if (value <= 100) oran = value;`
//    yazın. İlk çıktı ne oldu? İki davranıştan hangisi doğru — duruma göre.
//
// 4. 6. blokta `Olcer` sınıfındaki `Bilgi` metodunu `virtual`, `IsiOlcer`
//    içindekini `override` yapın. Çıktının hangi yarısı değişti?
//
// 5. 9. blokta `struct NoktaS` ifadesini `class NoktaS` yapın. Çıktı ne
//    oldu? Tek kelime değişti, davranış tamamen değişti.
//
// 6. 10. blokta `sozluk["bir"] = 2;` yerine `sozluk.Add("bir", 2);` yazın.
//    Program çalışıyor mu? Neden?
