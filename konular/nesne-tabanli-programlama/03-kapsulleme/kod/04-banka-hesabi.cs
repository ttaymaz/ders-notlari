// Kapsüllemenin klasik örneği: BankaHesabi.
//
// Bakiye, dışarıdan ASLA doğrudan değiştirilemez. Değiştirmenin tek yolu
// ParaYatir ve ParaCek metotlarıdır; kurallar orada uygulanır.
//
// Bu sınıfı aklınızda tutun: baharda bu bakiye bir veritabanı sütunu,
// ParaCek bir formdaki düğme olacak. Kural yine burada duracak.
//
// Şema: assets/01-erisim-belirleyiciler.svg
// Çalıştırmak için:  dotnet run 04-banka-hesabi.cs

BankaHesabi hesap = new BankaHesabi("TR12 0001 0002 0003", "Ayşe Yılmaz", 1000m);

hesap.OzetYazdir();

Console.WriteLine("\n--- 500 TL yatırma ---");
hesap.ParaYatir(500m);
hesap.OzetYazdir();

Console.WriteLine("\n--- 200 TL çekme ---");
hesap.ParaCek(200m);
hesap.OzetYazdir();

Console.WriteLine("\n--- Bakiyeden fazlasını çekmeyi deneyelim ---");
hesap.ParaCek(99999m);
hesap.OzetYazdir();

Console.WriteLine("\n--- Eksi tutar yatırmayı deneyelim ---");
hesap.ParaYatir(-5000m);
hesap.OzetYazdir();

Console.WriteLine("\n--- Bakiyeyi doğrudan değiştirmeyi deneyelim ---");
// Aşağıdaki satırın yorumunu kaldırın: DERLENMEZ.
// hesap.Bakiye = 1000000m;
//
// Hata: The property or indexer 'BankaHesabi.Bakiye' cannot be used in
// this context because the set accessor is inaccessible
Console.WriteLine("hesap.Bakiye = 1000000m;  →  derleme hatası");

Console.WriteLine($"\nHesap özeti okunabiliyor: {hesap.Bakiye:C}");


class BankaHesabi
{
    private decimal bakiye;

    public string HesapNo { get; private set; }
    public string SahipAdi { get; private set; }

    // Dışarısı bakiyeyi OKUYABİLİR ama YAZAMAZ.
    public decimal Bakiye
    {
        get { return bakiye; }
        private set { bakiye = value; }
    }

    public BankaHesabi(string hesapNo, string sahipAdi, decimal acilisBakiyesi)
    {
        HesapNo = hesapNo;
        SahipAdi = sahipAdi;

        // Açılış bakiyesi de kuralsız geçmiyor.
        if (acilisBakiyesi < 0m)
        {
            Console.WriteLine("Açılış bakiyesi eksi olamaz, 0 kabul edildi.");
            bakiye = 0m;
        }
        else
        {
            bakiye = acilisBakiyesi;
        }
    }

    public void ParaYatir(decimal tutar)
    {
        if (tutar <= 0m)
        {
            Console.WriteLine($"  [RET] Yatırılacak tutar pozitif olmalı. Gelen: {tutar:C}");
            return;
        }

        bakiye = bakiye + tutar;
        Console.WriteLine($"  [OK]  {tutar:C} yatırıldı.");
    }

    public void ParaCek(decimal tutar)
    {
        if (tutar <= 0m)
        {
            Console.WriteLine($"  [RET] Çekilecek tutar pozitif olmalı. Gelen: {tutar:C}");
            return;
        }

        if (tutar > bakiye)
        {
            Console.WriteLine($"  [RET] Yetersiz bakiye. İstenen {tutar:C}, mevcut {bakiye:C}");
            return;
        }

        bakiye = bakiye - tutar;
        Console.WriteLine($"  [OK]  {tutar:C} çekildi.");
    }

    public void OzetYazdir()
    {
        Console.WriteLine($"{HesapNo} / {SahipAdi} — bakiye: {bakiye:C}");
    }
}

// --- NEDEN BU KADAR UĞRAŞTIK? ---
//
// Çünkü bakiye, üzerinde KURAL olan bir veridir:
//   - eksiye düşemez
//   - yalnızca yatırma ve çekme ile değişir
//   - her değişim izlenebilir olmalıdır
//
// Bu kuralları nesnenin dışına yazarsanız, sınıfı kullanan herkesin
// onları bilmesi ve uygulaması gerekir. İçine yazarsanız, kural
// verinin kendisiyle birlikte taşınır.
//
// Kapsülleme tek cümleyle: KURALI VERİNİN YANINA KOY.
