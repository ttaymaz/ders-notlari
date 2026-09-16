// BU DOSYA KASITLI OLARAK BOZUKTUR — derlenmemesi normaldir.
//
// Amaç: private bir alana dışarıdan erişmeye çalıştığınızda derleyicinin
// ne dediğini görmek. Hatayı okuyun, sonra düzeltin.
//
// Denemek için:  dotnet build 01-private-alana-erisim.cs

BankaHesabi hesap = new BankaHesabi("TR12 0001", "Ayşe Yılmaz", 1000m);

// HATA 1: private alana dışarıdan erişim
// error CS0122: 'BankaHesabi.bakiye' is inaccessible due to its protection level
hesap.bakiye = 1000000m;

// HATA 2: set'i private olan özelliğe dışarıdan yazma
// error CS0272: The property or indexer 'BankaHesabi.Bakiye' cannot be used
//               in this context because the set accessor is inaccessible
hesap.Bakiye = 500m;

Console.WriteLine(hesap.Bakiye);


class BankaHesabi
{
    private decimal bakiye;

    public string HesapNo { get; private set; }
    public string SahipAdi { get; private set; }

    public decimal Bakiye
    {
        get { return bakiye; }
        private set { bakiye = value; }
    }

    public BankaHesabi(string hesapNo, string sahipAdi, decimal acilisBakiyesi)
    {
        HesapNo = hesapNo;
        SahipAdi = sahipAdi;
        bakiye = acilisBakiyesi;
    }

    public void ParaYatir(decimal tutar)
    {
        if (tutar <= 0m) { return; }
        bakiye = bakiye + tutar;
    }
}

// --- DÜZELTMENİZ İÇİN ---
//
// 1. İki hatalı satırı silin ve bakiyeyi ParaYatir metoduyla artırın.
//    Program şimdi derleniyor mu?
//
// 2. Sınıftaki "private decimal bakiye;" satırını "public" yapın.
//    Birinci hata kayboldu mu? Peki bu iyi bir çözüm mü?
//
// 3. İkinci soruya cevabınız "hayır" ise nedenini bir cümleyle yazın.
//    Bu, bu haftanın en önemli sorusudur.
