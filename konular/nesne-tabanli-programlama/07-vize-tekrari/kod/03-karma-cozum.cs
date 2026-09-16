// BÖLÜM B PROVASININ ÇÖZÜMÜ.
//
// 02-sinif-yazma-provasi.cs dosyasındaki sorunun tam çözümü.
// Önce kendiniz deneyin; buraya sonra bakın.
//
// Altı haftanın tamamı bu dosyada bir arada:
//   kapsülleme · kurallı özellik · this · statik sayaç ·
//   kurucu zinciri · kalıtım · base · override · ToString
//
// Çalıştırmak için:  dotnet run 03-karma-cozum.cs

Uye u1 = new Uye("Ayşe Yılmaz", 800m);
OgrenciUye u2 = new OgrenciUye("Mehmet Demir", 800m, "Sinanpaşa MYO");

Console.WriteLine("--- Üyeler ---");
Console.WriteLine(u1);
Console.WriteLine(u2);

Console.WriteLine("\n--- 3 aylık borç ---");
Console.WriteLine($"{u1.AdSoyad,-14}: {u1.ToplamBorc(3),10:C}");
Console.WriteLine($"{u2.AdSoyad,-14}: {u2.ToplamBorc(3),10:C}");

Console.WriteLine("\n--- Geçersiz ücret denemesi ---");
u1.AylikUcret = -500m;
Console.WriteLine($"ücret hâlâ: {u1.AylikUcret:C}");

Console.WriteLine("\n--- Kaç üye üretildi? ---");
Console.WriteLine($"toplam: {Uye.UyeSayisi}");

Console.WriteLine("\n--- Hepsi tek listede (temel sınıf tipinde) ---");
Uye[] uyeler = { u1, u2 };
foreach (Uye uye in uyeler)
{
    // Her üye kendi ToplamBorc versiyonunu çalıştırıyor.
    Console.WriteLine($"{uye}  →  {uye.ToplamBorc(12),10:C}");
}


class Uye
{
    // 1) KAPSÜLLEME — kural olan alan private, erişim kurallı özellikten
    private decimal aylikUcret;

    public decimal AylikUcret
    {
        get { return aylikUcret; }
        set
        {
            if (value < 0m)
            {
                Console.WriteLine($"  [RET] Ücret eksi olamaz, gelen: {value:C}");
                return;
            }

            aylikUcret = value;
        }
    }

    // 2) STATİK SAYAÇ — sınıfa ait, tek kopya
    private static int sonrakiNo = 1000;

    public static int UyeSayisi { get; private set; }

    // 3) Dışarıdan yalnızca okunabilen özellikler
    public string AdSoyad { get; private set; }
    public int UyeNo { get; private set; }

    // 4) KURUCU — this ile isim çakışması çözülüyor
    public Uye(string adSoyad, decimal aylikUcret)
    {
        this.AdSoyad = adSoyad;
        this.AylikUcret = aylikUcret;     // özellik üzerinden: kural işler

        UyeNo = sonrakiNo;
        sonrakiNo = sonrakiNo + 1;
        UyeSayisi = UyeSayisi + 1;
    }

    // 5) virtual — türetilmiş sınıf ezebilsin
    public virtual decimal ToplamBorc(int ay)
    {
        if (ay <= 0) { return 0m; }
        return aylikUcret * ay;
    }

    // 6) ToString ezildi
    public override string ToString()
    {
        return $"{UyeNo} - {AdSoyad}";
    }
}


class OgrenciUye : Uye
{
    public string OkulAdi { get; private set; }

    // base ile temel sınıfın kurucusu çağrılıyor
    public OgrenciUye(string adSoyad, decimal aylikUcret, string okulAdi)
        : base(adSoyad, aylikUcret)
    {
        OkulAdi = okulAdi;
    }

    // Ezme: temel sınıfın hesabını YENİDEN YAZMIYOR, çağırıp indirim uyguluyor.
    // Yarın aylık borç hesabı değişirse burası kendiliğinden uyar.
    public override decimal ToplamBorc(int ay)
    {
        return base.ToplamBorc(ay) * 0.60m;
    }

    public override string ToString()
    {
        return $"{base.ToString()} (öğrenci)";
    }
}

// --- PUANLAMA ÖLÇÜTLERİ (20 puan) ---
//
//   kapsülleme ve kurallı özellik ......... 5
//     private alan + set içinde kontrol + geçersiz değerde eski değer kalır
//
//   kurucu + this + statik sayaç .......... 5
//     this ile atama, otomatik numara, sayacın static olması
//
//   kalıtım ve base kullanımı ............. 5
//     ": Uye" ve ": base(...)" ile kurucu zinciri
//
//   ezme (override) ve ToString ........... 5
//     virtual/override çifti, base.ToplamBorc çağrısı, ToString ezilmesi
//
// Kısmi puan: her ölçüt kendi içinde değerlendirilir. Kalıtımı kurup
// ezmeyi yapamadıysanız kalıtım puanını alırsınız.
//
// --- SIK KAÇIRILAN İKİ NOKTA ---
//
// 1. Kurucuda "aylikUcret = aylikUcret;" yazıp ALANA değil parametreye
//    atamak. this olmadan bu satır hiçbir şey yapmaz.
//
// 2. Kurucuda özellik yerine doğrudan alana atamak. Doğrudan alana
//    atarsanız kuruluş anındaki geçersiz değer kontrolden KAÇAR.
//    Yukarıdaki çözümde kurucu "this.AylikUcret" (özellik) kullanıyor.
