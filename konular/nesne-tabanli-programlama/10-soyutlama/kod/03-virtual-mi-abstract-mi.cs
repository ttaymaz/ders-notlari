// virtual mı, abstract mı? Karar ölçütü tek bir soruda.
//
// Şema: assets/02-virtual-mi-abstract-mi.svg
// Çalıştırmak için:  dotnet run 03-virtual-mi-abstract-mi.cs

Odeme[] odemeler =
{
    new KrediKarti("1234", 1500m),
    new Havale("TR33 0006", 1500m),
    new Nakit(1500m),
};

Console.WriteLine($"{"Yöntem",-14} {"Tutar",10} {"Komisyon",10} {"Toplam",10}");
Console.WriteLine(new string('-', 48));

foreach (Odeme o in odemeler)
{
    Console.WriteLine($"{o.YontemAdi(),-14} {o.Tutar,10:C} {o.Komisyon(),10:C} {o.ToplamTutar(),10:C}");
}

Console.WriteLine("\n--- Makbuz: varsayılanı olan davranış ---");
foreach (Odeme o in odemeler)
{
    o.MakbuzYazdir();
}


abstract class Odeme
{
    public decimal Tutar { get; private set; }

    public Odeme(decimal tutar)
    {
        Tutar = tutar;
    }

    // ABSTRACT: her ödeme yönteminin adı vardır ama ORTAK bir ad yoktur.
    // Varsayılan uydurmanın anlamı yok — her tür yazmak zorunda.
    public abstract string YontemAdi();

    // ABSTRACT: her yöntemin komisyonu farklı hesaplanır, ortak formül yok.
    public abstract decimal Komisyon();

    // VIRTUAL: makbuzun ORTAK bir biçimi var. Çoğu tür bunu kullanır,
    // isteyen özelleştirir. Varsayılan anlamlı olduğu için abstract değil.
    public virtual void MakbuzYazdir()
    {
        Console.WriteLine($"  [{YontemAdi()}] {ToplamTutar():C} tahsil edildi.");
    }

    // NE ABSTRACT NE VIRTUAL: hesap her tür için aynı.
    // Soyut metotları çağırdığı için her türde doğru sonuç verir.
    public decimal ToplamTutar()
    {
        return Tutar + Komisyon();
    }
}


class KrediKarti : Odeme
{
    private string sonDortHane;

    public KrediKarti(string sonDortHane, decimal tutar) : base(tutar)
    {
        this.sonDortHane = sonDortHane;
    }

    public override string YontemAdi() { return "Kredi Kartı"; }

    public override decimal Komisyon() { return Tutar * 0.02m; }

    // Makbuzu özelleştiriyoruz: kart numarası da yazılsın.
    public override void MakbuzYazdir()
    {
        base.MakbuzYazdir();
        Console.WriteLine($"          Kart: **** {sonDortHane}");
    }
}


class Havale : Odeme
{
    private string iban;

    public Havale(string iban, decimal tutar) : base(tutar)
    {
        this.iban = iban;
    }

    public override string YontemAdi() { return "Havale"; }

    public override decimal Komisyon() { return 5m; }

    // MakbuzYazdir EZİLMEDİ — varsayılan biçim yeterli.
}


class Nakit : Odeme
{
    public Nakit(decimal tutar) : base(tutar) { }

    public override string YontemAdi() { return "Nakit"; }

    public override decimal Komisyon() { return 0m; }

    // MakbuzYazdir EZİLMEDİ.
}

// --- KARAR ÖLÇÜTÜ ---
//
// Temel sınıfa bir metot yazarken tek bir soru sorun:
//
//   "Her tür için ANLAMLI bir varsayılan yazabiliyor muyum?"
//
//   Evet  → virtual   (gövdeyi yaz, ezmek isteğe bağlı)
//   Hayır → abstract  (gövde yazma, ezmek zorunlu)
//
// Yukarıdaki örnekte:
//   YontemAdi  → abstract. "Ödeme" diye bir yöntem adı yok.
//   Komisyon   → abstract. Ortak bir komisyon formülü yok.
//   MakbuzYazdir → virtual. Ortak bir makbuz biçimi VAR; Havale ve Nakit
//                  onu olduğu gibi kullanıyor, KrediKarti üzerine ekliyor.
//
// --- ÜÇÜNCÜ SEÇENEK ---
//
// Bazı metotlar ne abstract ne virtual olmalıdır. ToplamTutar bunun
// örneği: hesap her tür için aynı, yalnızca girdileri (Komisyon) türe
// göre değişiyor. Böyle metotları ezilebilir yapmayın — ezilirse
// formül türden türe ayrışır ve tutarlılık kaybolur.
