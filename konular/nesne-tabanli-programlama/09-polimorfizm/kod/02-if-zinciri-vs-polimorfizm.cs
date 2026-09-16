// Polimorfizm olmasaydı ne yazardık? Tür kontrolü zinciri.
//
// Bu dosya aynı işi iki kez yapıyor: önce tür sorarak, sonra polimorfizmle.
// Fark, YENİ BİR TÜR EKLENDİĞİNDE ortaya çıkıyor.
//
// Şema: assets/02-yeni-tur-eklemek.svg
// Çalıştırmak için:  dotnet run 02-if-zinciri-vs-polimorfizm.cs

Demirbas[] liste =
{
    new Kitap(101, "Tutunamayanlar"),
    new Dergi(102, "Bilim ve Teknik"),
    new DVD(103, "Kış Uykusu"),
};

Console.WriteLine("=== YÖNTEM 1: tür sorarak (kötü) ===");
foreach (Demirbas d in liste)
{
    // "is" operatörü nesnenin gerçek türünü sorar.
    if (d is Kitap)
    {
        Console.WriteLine($"  {d.Baslik}: 14 gün ödünç verilir");
    }
    else if (d is Dergi)
    {
        Console.WriteLine($"  {d.Baslik}: 7 gün ödünç verilir");
    }
    else if (d is DVD)
    {
        Console.WriteLine($"  {d.Baslik}: 3 gün ödünç verilir");
    }
    else
    {
        Console.WriteLine($"  {d.Baslik}: süre tanımsız");
    }
}

Console.WriteLine("\n=== YÖNTEM 2: polimorfizm (iyi) ===");
foreach (Demirbas d in liste)
{
    Console.WriteLine($"  {d.Baslik}: {d.OduncSuresi()} gün ödünç verilir");
}


class Demirbas
{
    public string Baslik { get; private set; }

    public Demirbas(int no, string baslik)
    {
        Baslik = baslik;
    }

    // Her tür kendi süresini bilir. Bilmeyene varsayılan verilir.
    public virtual int OduncSuresi()
    {
        return 14;
    }
}

class Kitap : Demirbas
{
    public Kitap(int no, string baslik) : base(no, baslik) { }
}

class Dergi : Demirbas
{
    public Dergi(int no, string baslik) : base(no, baslik) { }

    public override int OduncSuresi()
    {
        return 7;
    }
}

class DVD : Demirbas
{
    public DVD(int no, string baslik) : base(no, baslik) { }

    public override int OduncSuresi()
    {
        return 3;
    }
}

// --- ŞİMDİ DENEYİN: YENİ TÜR EKLEYİN ---
//
// Kütüphaneye "Harita" türü eklenecek, ödünç süresi 30 gün olsun.
//
// POLİMORFİK YÖNTEMDE yapmanız gerekenler:
//
//     class Harita : Demirbas
//     {
//         public Harita(int no, string baslik) : base(no, baslik) { }
//         public override int OduncSuresi() { return 30; }
//     }
//
// ...ve listeye ekleyin. Döngüye DOKUNMAYIN. Çalışır.
//
// TÜR SORAN YÖNTEMDE ise yukarıdaki if zincirine bir "else if" daha
// eklemeniz gerekir. Üstelik bu zincirin tek kopya olduğunu nereden
// biliyorsunuz? Gerçek bir programda ödünç süresi; ceza hesabında,
// uyarı e-postasında, raporda da sorulur. Her birinde ayrı bir zincir
// vardır ve birini güncellemeyi unutursunuz.
//
// --- ÖLÇÜT ---
//
// Kodunuzda "bu nesne hangi türden?" diye soran bir if zinciri görürseniz,
// orada büyük ihtimalle ezilmesi gereken bir metot vardır. Soruyu siz
// sormayın — nesneye sorun, o kendi cevabını bilsin.
