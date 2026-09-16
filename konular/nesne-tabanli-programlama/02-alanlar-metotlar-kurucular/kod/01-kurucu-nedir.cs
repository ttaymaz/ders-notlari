// Kurucu metot nedir, hangi sorunu çözer?
//
// Aynı ürün iki sınıfla kuruluyor: biri kurucusuz, biri kuruculu.
// Fark, nesnenin EKSİK KURULABİLİP kurulamamasında.
//
// Şema: assets/01-nesnenin-dogusu.svg
// Çalıştırmak için:  dotnet run 01-kurucu-nedir.cs

Console.WriteLine("=== KURUCUSUZ: alanlar tek tek doldurulur ===");

UrunEski eski1 = new UrunEski();
eski1.Ad = "Klavye";
eski1.Fiyat = 750m;
eski1.StokAdedi = 12;
eski1.BilgiYazdir();

// Burada bir satır unutuldu: Fiyat hiç yazılmadı.
// Program uyarmıyor, nesne yarım doğuyor ve öyle kullanılıyor.
UrunEski eski2 = new UrunEski();
eski2.Ad = "Mouse";
eski2.StokAdedi = 30;
eski2.BilgiYazdir();       // Fiyat: 0,00 TL  ← sessiz hata

Console.WriteLine();
Console.WriteLine("=== KURUCULU: nesne doğarken kurulur ===");

// Tek satır. Üstelik bir argümanı unutursanız DERLEYİCİ durdurur.
Urun yeni1 = new Urun("Klavye", 750m, 12);
yeni1.BilgiYazdir();

Urun yeni2 = new Urun("Mouse", 320m, 30);
yeni2.BilgiYazdir();

// Aşağıdaki satırın yorumunu kaldırın: derlenmez.
// Urun yeni3 = new Urun("Monitör");


// --- ÖNCE: kurucusuz sınıf ---
class UrunEski
{
    public string Ad = "";
    public decimal Fiyat;
    public int StokAdedi;

    public void BilgiYazdir()
    {
        Console.WriteLine($"{Ad,-10} {Fiyat,10:C}  stok: {StokAdedi}");
    }
}


// --- SONRA: kuruculu sınıf ---
class Urun
{
    public string Ad;
    public decimal Fiyat;
    public int StokAdedi;

    // KURUCU METOT (constructor)
    //   - Adı sınıfın adıyla AYNI olmak zorunda
    //   - Dönüş tipi YOK (void bile yazılmaz)
    //   - new çağrıldığında otomatik çalışır
    //
    // Parametre adlarını alan adlarından farklı seçtik (urunAdi / Ad).
    // Aynı adı kullanmanın yolu da var; onu "this" haftasında göreceğiz.
    public Urun(string urunAdi, decimal urunFiyati, int baslangicStogu)
    {
        Ad = urunAdi;
        Fiyat = urunFiyati;
        StokAdedi = baslangicStogu;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{Ad,-10} {Fiyat,10:C}  stok: {StokAdedi}");
    }
}

// --- NEDEN KURUCU? ---
//
// 1. NESNE YARIM DOĞAMAZ. Kurucu üç argüman istiyorsa, üçünü de
//    vermeden nesne üretemezsiniz. Unutma ihtimali ortadan kalkar.
//
// 2. HATA DERLEME ZAMANINDA YAKALANIR. Kurucusuz versiyonda eksik
//    alan çalışma zamanında yanlış çıktı üretiyordu; kuruculu
//    versiyonda kod derlenmiyor bile.
//
// 3. KURULUM KURALLARI TEK YERDE. "Bir ürün üretmek için neler
//    gerekir" sorusunun cevabı kurucunun imzasında yazıyor.
