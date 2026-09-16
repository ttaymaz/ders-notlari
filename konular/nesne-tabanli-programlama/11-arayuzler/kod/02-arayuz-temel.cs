// ÇÖZÜM: Arayüz (interface) — hiyerarşiden bağımsız sözleşme.
//
// 01-kalitimin-siniri.cs ile yan yana açın.
//
// Şema: assets/01-dikey-yatay.svg
// Çalıştırmak için:  dotnet run 02-arayuz-temel.cs

Demirbas[] koleksiyon =
{
    new BasiliKitap(101, "Tutunamayanlar"),
    new EKitap(102, "Sefiller", 4.2),
    new DijitalDergi(201, "Bilim ve Teknik", 745, 18.5),
    new DVD(301, "Kış Uykusu"),
};

Console.WriteLine("--- Tüm koleksiyon ---");
foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine($"  #{d.DemirbasNo} {d.Baslik}");
}

Console.WriteLine("\n--- İndirilebilir olanlar ---");
foreach (Demirbas d in koleksiyon)
{
    // Tür sormuyoruz; SÖZLEŞME soruyoruz.
    // "Bu nesne IIndirilebilir mi?" — türü ne olursa olsun.
    if (d is IIndirilebilir indirilebilir)
    {
        indirilebilir.Indir();
    }
}

Console.WriteLine("\n--- Arayüz tipinde dizi de kurulabilir ---");
IIndirilebilir[] indirilebilirler =
{
    new EKitap(103, "Suç ve Ceza", 5.1),
    new DijitalDergi(202, "Arkitekt", 512, 22.0),
};

double toplamBoyut = 0;
foreach (IIndirilebilir i in indirilebilirler)
{
    toplamBoyut = toplamBoyut + i.BoyutMB();
}
Console.WriteLine($"Toplam indirme boyutu: {toplamBoyut:F1} MB");


// ARAYÜZ: yalnızca sözleşme. Gövde yok, alan yok, kurucu yok.
// Adın başına I koymak C# geleneğidir (Interface).
interface IIndirilebilir
{
    void Indir();
    double BoyutMB();
}


abstract class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }

    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }
}


class BasiliKitap : Demirbas
{
    public BasiliKitap(int no, string baslik) : base(no, baslik) { }
}


// Hem Demirbas'tan TÜRÜYOR hem IIndirilebilir sözleşmesini UYGULUYOR.
// Sınıf adından sonra önce temel sınıf, sonra arayüzler yazılır.
class EKitap : Demirbas, IIndirilebilir
{
    private double boyut;

    public EKitap(int no, string baslik, double boyutMB) : base(no, baslik)
    {
        boyut = boyutMB;
    }

    // Arayüzdeki üyeler yazılmak ZORUNDA. override yazılmaz —
    // ortada ezilecek bir gövde yok, sözleşme uygulanıyor.
    public void Indir()
    {
        Console.WriteLine($"  {Baslik} indiriliyor... ({boyut} MB)");
    }

    public double BoyutMB()
    {
        return boyut;
    }
}


// Dergi hiyerarşisini bozmadan aynı sözleşmeyi uyguluyor.
class Dergi : Demirbas
{
    public int Sayi { get; private set; }

    public Dergi(int no, string baslik, int sayi) : base(no, baslik)
    {
        Sayi = sayi;
    }
}


class DijitalDergi : Dergi, IIndirilebilir
{
    private double boyut;

    public DijitalDergi(int no, string baslik, int sayi, double boyutMB)
        : base(no, baslik, sayi)
    {
        boyut = boyutMB;
    }

    public void Indir()
    {
        Console.WriteLine($"  {Baslik} sayı {Sayi} indiriliyor... ({boyut} MB)");
    }

    public double BoyutMB()
    {
        return boyut;
    }
}


class DVD : Demirbas
{
    public DVD(int no, string baslik) : base(no, baslik) { }
}

// --- NE KAZANDIK? ---
//
// 1. DijitalDergi hem Dergi'dir hem indirilebilirdir. Kalıtım hiyerarşisi
//    bozulmadı; Sayi alanı yerinde kaldı.
//
// 2. Döngüde tür sormuyoruz, SÖZLEŞME soruyoruz. Yeni bir indirilebilir
//    tür eklendiğinde döngüye dokunmuyoruz.
//
// 3. Arayüz tipinde dizi kurabiliyoruz: IIndirilebilir[] — içinde bir
//    EKitap ve bir DijitalDergi var, ortak temel sınıfları farklı olsa da.
//
// --- ARAYÜZ İLE SOYUT SINIFIN FARKI ---
//
//   Soyut sınıf : "bu NEDİR"     — bir e-kitap bir demirbaştır
//   Arayüz      : "bu NE YAPAR"  — bir e-kitap indirilebilir
//
// Sınıf adları isim olur (Demirbas, Kitap), arayüz adları genelde
// sıfat veya yetenek bildirir (IIndirilebilir, IKarsilastirilabilir).
