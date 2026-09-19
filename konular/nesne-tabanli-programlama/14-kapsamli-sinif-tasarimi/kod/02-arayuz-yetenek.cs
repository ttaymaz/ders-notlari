// Kalıtım mı, arayüz mü? Karar burada veriliyor.
//
// "Bir ... -DIR"      -> kalıtım.  VadeliHesap bir Hesap-tır.
// "Bir ... -EBİLİR"   -> arayüz.   VadeliHesap faiz getirebilir.
//
// Faiz getirmek bütün hesaplarda yok. Temel sınıfa yazarsak vadesiz hesaba
// da anlamsız bir üye eklemiş oluruz. Yalnızca bazı türlerde olan yeteneği
// arayüze koyarız.
//
// Çalıştırmak için:  dotnet run 02-arayuz-yetenek.cs

List<Hesap> hesaplar = new List<Hesap>
{
    new VadesizHesap("Ayşe Kaya", 1500m),
    new VadeliHesap("Mehmet Demir", 5000m, 0.35m),
    new VadeliHesap("Zeynep Ak", 12000m, 0.40m),
};

Console.WriteLine("--- Yıl sonu faiz işlemi ---");

foreach (Hesap h in hesaplar)
{
    // "Bu hesap faiz getirebiliyor mu?" diye soruyoruz, türünü sormuyoruz.
    // Yarın faiz getiren başka bir tür eklenirse bu satır değişmeyecek.
    if (h is IFaizGetirir faizli)
    {
        decimal kazanc = faizli.FaizIsle();
        Console.WriteLine($"  {h.SahipAdi,-14} +{kazanc,10:C}  (%{faizli.FaizOrani * 100:F0})");
    }
    else
    {
        Console.WriteLine($"  {h.SahipAdi,-14} {"faiz yok",11}");
    }
}

Console.WriteLine("\n--- Son durum ---");

foreach (Hesap h in hesaplar)
{
    Console.WriteLine($"  {h.Ozet()}");
}


interface IFaizGetirir
{
    decimal FaizOrani { get; }
    decimal FaizIsle();
}


abstract class Hesap
{
    private static int uretilen = 0;

    public decimal Bakiye { get; private set; }
    public int HesapNo { get; private set; }
    public string SahipAdi { get; private set; }

    protected Hesap(string sahipAdi, decimal acilis)
    {
        SahipAdi = sahipAdi;
        Bakiye = acilis;
        uretilen++;
        HesapNo = 1000 + uretilen;
    }

    protected void BakiyeyiDegistir(decimal fark)
    {
        if (Bakiye + fark < 0)
        {
            Console.WriteLine($"  [{HesapNo}] Bakiye yetersiz.");
            return;
        }

        Bakiye = Bakiye + fark;
    }

    public void ParaYatir(decimal tutar)
    {
        if (tutar > 0) { BakiyeyiDegistir(tutar); }
    }

    public abstract void ParaCek(decimal tutar);

    public virtual string Ozet()
    {
        return $"#{HesapNo} {SahipAdi,-14} {Bakiye,12:C}";
    }
}


// Yalnızca kalıtım: vadesiz hesabın faizle işi yok.
class VadesizHesap : Hesap
{
    public VadesizHesap(string sahipAdi, decimal acilis)
        : base(sahipAdi, acilis) { }

    public override void ParaCek(decimal tutar)
    {
        BakiyeyiDegistir(-(tutar + 2m));
    }

    public override string Ozet()
    {
        return base.Ozet() + "  [vadesiz]";
    }
}


// Kalıtım VE arayüz: hem bir Hesap-tır, hem faiz getirebilir.
class VadeliHesap : Hesap, IFaizGetirir
{
    public decimal FaizOrani { get; private set; }

    public VadeliHesap(string sahipAdi, decimal acilis, decimal faizOrani)
        : base(sahipAdi, acilis)
    {
        FaizOrani = faizOrani;
    }

    public override void ParaCek(decimal tutar)
    {
        decimal ceza = tutar * 0.02m;
        Console.WriteLine($"  [{HesapNo}] Vade bozuluyor, {ceza:C} kesinti.");
        BakiyeyiDegistir(-(tutar + ceza));
    }

    public decimal FaizIsle()
    {
        decimal kazanc = Bakiye * FaizOrani;
        BakiyeyiDegistir(kazanc);
        return kazanc;
    }

    public override string Ozet()
    {
        return base.Ozet() + "  [vadeli]";
    }
}

// --- Denemeniz için ---
//
// 1. `IFaizGetirir` arayüzündeki üyeleri `Hesap` temel sınıfına taşıyın.
//    Program çalışıyor mu? Peki `VadesizHesap` için `FaizIsle()` ne
//    döndürecek? Sıfır mı, hata mı? Anlamsız bir üyeyi taşımak zorunda
//    kalmak, yanlış yere koyduğunuzun işaretidir.
//
// 2. `if (h is IFaizGetirir faizli)` yerine `if (h is VadeliHesap v)` yazın.
//    Program aynı çalışır. Şimdi faiz getiren ikinci bir tür ekleyin —
//    hangi sürümde kodu değiştirmek zorunda kaldınız?
//
// 3. `VadeliHesap` sınıfından `FaizOrani` özelliğini silin. Hangi hata
//    gelir? Arayüz bir sözleşmedir; imzalayan sınıf her maddesini yazmak
//    zorundadır.
//
// 4. `IFaizGetirir` arayüzünü uygulayan ama `Hesap` OLMAYAN bir sınıf
//    yazın: `YatirimFonu`. Yıl sonu döngüsü onu görebiliyor mu? Neden?
//    (İpucu: döngü `List<Hesap>` üzerinde geziyor.)
//
// 5. Dört numaradaki fonu da işleme katmak isteseydiniz listeyi nasıl
//    tanımlardınız? `List<IFaizGetirir>` yazsanız bu kez neyi kaybederdiniz?
