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

// --- SINAVDA BU NASIL PUANLANIR? ---
//
// Bu çözüm sınavdaki İKİ sorunun toplamına karşılık gelir.
//
//   SORU 1 — tek sınıf (20 puan)        → yukarıdaki Uye sınıfı
//     kapsülleme ....................... 10
//       private alan + set içinde kontrol + geçersiz değerde eski değer kalır
//     kuruluş .......................... 10
//       kurucu, this ile atama, static sayaç, otomatik numara
//
//   SORU 2 — kalıtım (20 puan)          → yukarıdaki OgrenciUye sınıfı
//     kalıtım ve base .................. 10
//       ": Uye" ve ": base(...)" ile kurucu zinciri
//     ezme ve ToString ................. 10
//       virtual/override çifti, base.ToplamBorc çağrısı, ToString ezilmesi
//
// Kısmi puan: her ölçüt kendi içinde parçalanır. Kalıtımı kurup ezmeyi
// yapamadıysanız kalıtım puanını tam alırsınız.
//
// --- SIK KAÇIRILAN İKİ NOKTA ---
//
// 1. Kurucuda "aylikUcret = aylikUcret;" yazıp ALANA değil parametreye
//    atamak. this olmadan bu satır hiçbir şey yapmaz.
//
// 2. Kurucuda özellik yerine doğrudan alana atamak. Doğrudan alana
//    atarsanız kuruluş anındaki geçersiz değer kontrolden KAÇAR.
//    Yukarıdaki çözümde kurucu "this.AylikUcret" (özellik) kullanıyor.
