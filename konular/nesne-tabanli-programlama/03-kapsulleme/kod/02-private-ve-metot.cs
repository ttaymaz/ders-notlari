// İLK ÇÖZÜM: alanı private yap, erişimi metotlardan ver.
//
// Bu, kapsüllemenin en temel biçimidir. Çalışır ama biraz hantaldır;
// daha zarif yolu (özellikler) 03-ozellikler.cs dosyasında.
//
// Şema: assets/01-erisim-belirleyiciler.svg
// Çalıştırmak için:  dotnet run 02-private-ve-metot.cs

Urun klavye = new Urun("Klavye", 750m, 12);

Console.WriteLine("--- Kurallı yol hâlâ çalışıyor ---");
klavye.StokCikis(5);
klavye.BilgiYazdir();

Console.WriteLine("\n--- Arka kapı artık yok ---");
// Aşağıdaki satırın yorumunu kaldırın: DERLENMEZ.
// klavye.stokAdedi = -50;
//
// Hata: 'Urun.stokAdedi' is inaccessible due to its protection level
Console.WriteLine("klavye.stokAdedi = -50;  →  derleme hatası");

Console.WriteLine("\n--- Okumak için metot kullanıyoruz ---");
Console.WriteLine($"Mevcut stok: {klavye.StokAdediGetir()}");

Console.WriteLine("\n--- Geçersiz değer denemesi ---");
klavye.StokCikis(1000);
klavye.BilgiYazdir();


class Urun
{
    // private: bu alanlar YALNIZCA bu sınıfın içinden görünür.
    // Küçük harfle başlatmak yaygın bir alışkanlıktır; böylece
    // private alan ile dışarıya açık üye bakışta ayrılır.
    private string ad;
    private decimal fiyat;
    private int stokAdedi;

    public Urun(string urunAdi, decimal urunFiyati, int baslangicStogu)
    {
        // Kurucu sınıfın İÇİNDE olduğu için private alanlara erişebiliyor.
        // Parametre adlarını alan adlarından farklı seçmek zorunda kaldık;
        // aynı adı kullanmanın yolunu gelecek hafta göreceğiz.
        ad = urunAdi;
        fiyat = urunFiyati;
        stokAdedi = baslangicStogu;
    }

    // Dışarıya okuma izni veren metot
    public int StokAdediGetir()
    {
        return stokAdedi;
    }

    // Dışarıya KONTROLLÜ yazma izni veren metot
    public void StokCikis(int adet)
    {
        if (adet <= 0)
        {
            Console.WriteLine($"{ad}: çıkış adedi pozitif olmalı.");
            return;
        }

        if (adet > stokAdedi)
        {
            Console.WriteLine($"{ad}: stok yetersiz! İstenen {adet}, mevcut {stokAdedi}.");
            return;
        }

        stokAdedi = stokAdedi - adet;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{ad,-10} {fiyat,10:C}  stok: {stokAdedi}");
    }
}

// --- BU YAKLAŞIMIN SORUNU ---
//
// Her alan için bir okuma bir yazma metodu yazmak gerekiyor:
// StokAdediGetir(), StokAdediAyarla(), FiyatGetir(), FiyatAyarla()...
// On alanlı bir sınıfta yirmi metot demek.
//
// Üstelik kullanımı da doğal değil:
//     klavye.StokAdediGetir()     yerine     klavye.StokAdedi
// yazabilmek isterdik. C#'ın bunun için özel bir aracı var: ÖZELLİKLER.
