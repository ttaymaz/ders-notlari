// Mini ATM: dönemin tamamı tek dosyada.
//
// Kapsülleme  -> bakiye yalnızca metotlarla değişir
// Kalıtım     -> Hesap ailesi
// Ezme        -> her tür kendi para çekme kuralını yazar
// Polimorfizm -> tek liste, tek döngü, tür başına farklı davranış
// Soyutlama   -> Hesap tek başına üretilemez
// Arayüz      -> faiz yalnızca bazı hesaplarda
// Koleksiyon  -> List ile rapor, Dictionary ile numaradan erişim
//
// Bahar dönemindeki otomasyonun konsol hâli budur. Baharda Atm sınıfının
// yerini bir form alacak; Hesap ailesi olduğu gibi kalacak.
//
// Şema: assets/02-atm-yapisi.svg
// Çalıştırmak için:  dotnet run 03-mini-atm.cs

Atm atm = new Atm();

atm.HesapAc(new VadesizHesap("Ayşe Kaya", 1500m));
atm.HesapAc(new VadeliHesap("Mehmet Demir", 5000m, 0.35m));
atm.HesapAc(new VadesizHesap("Zeynep Ak", 320m));

atm.Rapor("AÇILIŞ");

// --- İşlemler: hepsi hesap numarası üzerinden ---
atm.ParaCek(1001, 500m);
atm.ParaCek(1002, 1000m);      // vadeli: ceza kesilir
atm.ParaCek(1003, 900m);       // bakiye yetersiz
atm.ParaCek(9999, 100m);       // hesap yok
atm.ParaYatir(1003, 2000m);

atm.Rapor("İŞLEMLERDEN SONRA");

atm.YilSonuFaiz();
atm.Rapor("FAİZ SONRASI");


// ===================== YÖNETİCİ SINIF =====================
//
// Atm bir Hesap DEĞİLDİR; hesapları yönetir. Bu yüzden kalıtım yok.
class Atm
{
    // İki koleksiyon, iki farklı iş:
    //   liste  -> sırayla rapor almak
    //   defter -> numaradan hesaba anında ulaşmak
    private readonly List<Hesap> liste = new List<Hesap>();
    private readonly Dictionary<int, Hesap> defter = new Dictionary<int, Hesap>();

    public void HesapAc(Hesap h)
    {
        liste.Add(h);
        defter.Add(h.HesapNo, h);      // Add: aynı numara ikinci kez gelirse hata versin
        Console.WriteLine($"Hesap açıldı: {h.Ozet()}");
    }

    public void ParaCek(int hesapNo, decimal tutar)
    {
        if (!defter.TryGetValue(hesapNo, out Hesap? h))
        {
            Console.WriteLine($"[{hesapNo}] Böyle bir hesap yok.");
            return;
        }

        Console.WriteLine($"[{hesapNo}] {tutar:C} çekiliyor...");
        h.ParaCek(tutar);              // kuralı hesabın kendi türü belirliyor
    }

    public void ParaYatir(int hesapNo, decimal tutar)
    {
        if (!defter.TryGetValue(hesapNo, out Hesap? h))
        {
            Console.WriteLine($"[{hesapNo}] Böyle bir hesap yok.");
            return;
        }

        h.ParaYatir(tutar);
        Console.WriteLine($"[{hesapNo}] {tutar:C} yatırıldı.");
    }

    public void YilSonuFaiz()
    {
        Console.WriteLine("\n--- Yıl sonu faiz işlemi ---");

        foreach (Hesap h in liste)
        {
            if (h is IFaizGetirir faizli)
            {
                decimal kazanc = faizli.FaizIsle();
                Console.WriteLine($"  {h.SahipAdi}: +{kazanc:C}");
            }
        }
    }

    public void Rapor(string baslik)
    {
        Console.WriteLine($"\n===== {baslik} =====");

        decimal toplam = 0m;

        foreach (Hesap h in liste)
        {
            Console.WriteLine($"  {h.Ozet()}");
            toplam += h.Bakiye;
        }

        Console.WriteLine($"  {"TOPLAM",-21} {toplam,12:C}   ({liste.Count} hesap)");
    }
}


// ===================== ALAN MODELİ =====================

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
            Console.WriteLine($"  [{HesapNo}] Bakiye yetersiz, işlem iptal.");
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
// 1. Dördüncü bir hesap türü ekleyin: `OgrenciHesabi`. İşlem ücreti almasın
//    ve aylık çekim sınırı 2000 TL olsun. `Atm` sınıfında KAÇ satır
//    değiştirdiniz? Cevap sıfır olmalı — o zaman tasarım doğrudur.
//
// 2. `Atm` sınıfındaki `defter.Add(...)` çağrısını `defter[h.HesapNo] = h;`
//    yapın. İki hesap aynı numarayı alırsa ne olur? Hangi davranışı
//    istersiniz — sessizce üzerine yazmak mı, hata vermek mi?
//
// 3. `liste` alanını silip yalnızca `defter` ile çalışmayı deneyin. Rapor
//    hâlâ alınabiliyor mu? Sıra garantisi var mı? İki koleksiyonu birlikte
//    tutmanın bedeli nedir — biri güncellenip diğeri unutulursa ne olur?
//
// 4. `Rapor` metodundaki `toplam += h.Bakiye;` satırını `Hesap` sınıfına
//    taşımaya çalışın. Neden olmuyor? Toplam kimin sorumluluğu — tek bir
//    hesabın mı, hesapları yönetenin mi?
//
// 5. `Atm` sınıfına `HesapKapat(int hesapNo)` metodu yazın. Hem listeden
//    hem sözlükten silmeyi unutmayın. Listeyi `foreach` ile gezip silmeye
//    çalışırsanız ne olur? (Geçen haftanın tuzağı.)
