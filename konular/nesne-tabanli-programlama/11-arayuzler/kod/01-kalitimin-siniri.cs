// SORUN: Kalıtım dikey, bazı özellikler yatay.
//
// Kütüphanedeki bazı demirbaşlar dijital: e-kitap, dijital dergi.
// Bunlar "indirilebilir". Ama indirilebilirlik hiyerarşiyi DİK KESİYOR:
// bazı kitaplarda var, bazı dergilerde var, DVD'de yok.
//
// Şema: assets/01-dikey-yatay.svg
// Çalıştırmak için:  dotnet run 01-kalitimin-siniri.cs

Demirbas[] koleksiyon =
{
    new BasiliKitap(101, "Tutunamayanlar"),
    new EKitap(102, "Sefiller", 4.2),
    new Dergi(201, "Bilim ve Teknik"),
    new DVD(301, "Kış Uykusu"),
};

Console.WriteLine("--- Koleksiyon ---");
foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine($"  {d.Baslik}");
}

Console.WriteLine("\n--- İndirilebilir olanlar (kalıtımla çözmeye çalışalım) ---");
foreach (Demirbas d in koleksiyon)
{
    // Tür sormak zorunda kaldık — dokuzuncu haftada bunun kötü olduğunu
    // söylemiştik. Ama başka çaremiz yok: "indirilebilirlik" Demirbas
    // hiyerarşisinde tanımlı değil.
    if (d is EKitap ek)
    {
        Console.WriteLine($"  {ek.Baslik} — {ek.BoyutMB} MB");
    }
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

class EKitap : Demirbas
{
    public double BoyutMB { get; private set; }

    public EKitap(int no, string baslik, double boyutMB) : base(no, baslik)
    {
        BoyutMB = boyutMB;
    }

    public void Indir()
    {
        Console.WriteLine($"{Baslik} indiriliyor... ({BoyutMB} MB)");
    }
}

class Dergi : Demirbas
{
    public Dergi(int no, string baslik) : base(no, baslik) { }
}

class DVD : Demirbas
{
    public DVD(int no, string baslik) : base(no, baslik) { }
}

// --- NEDEN KALITIMLA ÇÖZEMİYORUZ? ---
//
// Denediğimiz çözümler ve neden hepsi kötü:
//
// 1. "IndirilebilirDemirbas" diye bir ara sınıf yapalım:
//
//        Demirbas → IndirilebilirDemirbas → EKitap
//                                        → DijitalDergi
//
//    Ama o zaman DijitalDergi, Dergi sınıfından türeyemez. Dergiye ait
//    her şeyi (sayı, ay, cilt) yeniden yazmak gerekir. Bir sınıf
//    YALNIZCA BİR sınıftan türeyebilir.
//
// 2. Indir metodunu Demirbas sınıfına koyalım:
//
//    O zaman DVD'nin ve basılı kitabın da Indir metodu olur. Basılı bir
//    kitabı indirmek ne demek? Metot "desteklenmiyor" diye hata atmak
//    zorunda kalır — yani sınıf, tutamayacağı bir söz vermiş olur.
//
// 3. Tür sormak (yukarıdaki döngü):
//
//    Dokuzuncu haftada bunun neden kötü olduğunu görmüştük. Yeni bir
//    indirilebilir tür eklendiğinde bu zinciri bulup güncellemek gerekir.
//
// İhtiyacımız olan şey şu: "indirilebilir olma" davranışını, KALITIM
// HİYERARŞİSİNDEN BAĞIMSIZ olarak ifade edebilmek.
//
// C#'ın bunun için aracı var: ARAYÜZ (interface).
