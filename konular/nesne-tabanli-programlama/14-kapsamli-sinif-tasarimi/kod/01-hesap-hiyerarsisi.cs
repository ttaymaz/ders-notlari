// Tasarımın omurgası: soyut temel sınıf ve iki türetilmiş hesap.
//
// Bu dosyada tek bir soru cevaplanıyor: hangi üye nereye yazılmalı?
//   - Bütün hesaplarda AYNI çalışan iş  -> temel sınıfta normal metot
//   - Bütün hesaplarda VAR ama farklı   -> temel sınıfta abstract metot
//   - Yalnızca bazılarında var          -> o sınıfa özel (veya arayüz)
//
// Şema: assets/02-atm-yapisi.svg
// Çalıştırmak için:  dotnet run 01-hesap-hiyerarsisi.cs

Hesap[] hesaplar =
{
    new VadesizHesap("Ayşe Kaya", 1500m),
    new VadeliHesap("Mehmet Demir", 5000m),
};

foreach (Hesap h in hesaplar)
{
    Console.WriteLine(h.Ozet());
}

Console.WriteLine("\n--- Her hesaptan 1000 TL çekiliyor ---");

foreach (Hesap h in hesaplar)
{
    // Tek çağrı; kuralı nesnenin kendi türü belirliyor.
    h.ParaCek(1000m);
    Console.WriteLine($"  {h.Ozet()}");
}

Console.WriteLine("\n--- Bakiyeden fazlası isteniyor ---");
hesaplar[0].ParaCek(99999m);

Console.WriteLine($"\nÜretilen hesap sayısı: {Hesap.ToplamHesap}");


// ===================== TEMEL SINIF =====================
//
// abstract: "Hesap" diye tek başına bir ürün yoktur. Ya vadesizdir ya
// vadelidir. new Hesap(...) yazılmasını derleyici engellesin istiyoruz.
abstract class Hesap
{
    private static int uretilen = 0;

    // Bakiye dışarıdan OKUNUR ama yazılamaz. Değişmesinin tek yolu
    // ParaYatir ve ParaCek metotlarıdır — kapsüllemenin bütün meselesi bu.
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

    public static int ToplamHesap
    {
        get { return uretilen; }
    }

    // Türetilmiş sınıflar bakiyeyi doğrudan yazamaz; bu kapıdan geçer.
    // Kural tek yerde duruyor: bakiye asla eksiye düşmez.
    protected void BakiyeyiDegistir(decimal fark)
    {
        if (Bakiye + fark < 0)
        {
            Console.WriteLine($"  [{HesapNo}] Bakiye yetersiz.");
            return;
        }

        Bakiye = Bakiye + fark;
    }

    // HER hesapta AYNI çalışıyor -> normal metot, ezilmesine gerek yok.
    public void ParaYatir(decimal tutar)
    {
        if (tutar <= 0)
        {
            Console.WriteLine("  Yatırılacak tutar pozitif olmalı.");
            return;
        }

        BakiyeyiDegistir(tutar);
    }

    // HER hesapta VAR ama kuralı türe göre DEĞİŞİYOR -> abstract.
    // Gövdesi yok; yazmak türetilmiş sınıfın sorumluluğu.
    public abstract void ParaCek(decimal tutar);

    // Ortak bir gövdesi var ama tür isterse değiştirebilsin -> virtual.
    public virtual string Ozet()
    {
        return $"#{HesapNo} {SahipAdi,-14} {Bakiye,10:C}";
    }
}


// ===================== TÜRETİLMİŞ SINIFLAR =====================

class VadesizHesap : Hesap
{
    private const decimal IslemUcreti = 2m;

    public VadesizHesap(string sahipAdi, decimal acilis)
        : base(sahipAdi, acilis) { }

    public override void ParaCek(decimal tutar)
    {
        BakiyeyiDegistir(-(tutar + IslemUcreti));
    }

    public override string Ozet()
    {
        return base.Ozet() + "  [vadesiz]";
    }
}


class VadeliHesap : Hesap
{
    private const decimal VadeBozmaOrani = 0.02m;

    public VadeliHesap(string sahipAdi, decimal acilis)
        : base(sahipAdi, acilis) { }

    public override void ParaCek(decimal tutar)
    {
        decimal ceza = tutar * VadeBozmaOrani;
        Console.WriteLine($"  [{HesapNo}] Vade bozuluyor, {ceza:C} kesinti.");
        BakiyeyiDegistir(-(tutar + ceza));
    }

    public override string Ozet()
    {
        return base.Ozet() + "  [vadeli]";
    }
}

// --- Denemeniz için ---
//
// 1. `new Hesap("Test", 100m)` yazmayı deneyin. Derleyici ne diyor?
//    `abstract` anahtar kelimesini silin, tekrar deneyin. Artık derleniyor —
//    ama "Hesap" diye bir hesap türü var mı gerçekten?
//
// 2. `VadesizHesap` sınıfından `override void ParaCek` metodunu silin.
//    Hangi hata gelir? `abstract` metot, türetilmiş sınıfa yazılmayı
//    ZORUNLU kılar; `virtual` kılmaz.
//
// 3. Ana bloğa `hesaplar[0].Bakiye = 1000000m;` yazın. Derlenmiyor.
//    `private set` ifadesini `set` yapın ve tekrar deneyin. Şimdi bir
//    kullanıcı bakiyesini istediği gibi değiştirebilir — kapsülleme bunun
//    için var.
//
// 4. `BakiyeyiDegistir` metodunu `protected` yerine `public` yapın. Ana
//    bloktan `hesaplar[0].BakiyeyiDegistir(50000m);` çağırın. Kural hâlâ
//    işliyor mu? Peki bu kapıyı dışarıya açmak doğru mu?
//
// 5. Üçüncü bir tür ekleyin: `AltinHesabi`. Para çekerken gram fiyatı
//    üzerinden kesinti yapsın. Temel sınıfta KAÇ satır değiştirmeniz
//    gerekti? Cevap sıfırsa tasarım doğru kurulmuş demektir.
