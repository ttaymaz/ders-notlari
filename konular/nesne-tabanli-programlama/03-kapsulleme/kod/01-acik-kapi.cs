// SORUN: public alan, açık kapıdır.
//
// Geçen hafta StokCikis metoduna güzel bir kontrol yazmıştık. Bu dosya
// o kontrolün neden yeterli olmadığını gösteriyor.
//
// Çalıştırmak için:  dotnet run 01-acik-kapi.cs

Urun klavye = new Urun("Klavye", 750m, 12);

Console.WriteLine("--- Metodu kullanarak satış (kurallı yol) ---");
klavye.StokCikis(100);        // kontrol devrede: reddediyor
klavye.BilgiYazdir();

Console.WriteLine("\n--- Alanı doğrudan değiştirerek (arka kapı) ---");
klavye.StokAdedi = -50;       // kontrol DEVRE DIŞI: kimse engellemiyor
klavye.BilgiYazdir();

Console.WriteLine("\n--- Fiyat da korumasız ---");
klavye.Fiyat = -999m;
klavye.BilgiYazdir();

Console.WriteLine();
Console.WriteLine("Depodaki toplam değer: " + klavye.ToplamDeger().ToString("C"));
Console.WriteLine("Eksi stok, eksi fiyat, artı değer. Program bunu sorun görmüyor.");


class Urun
{
    // Alanlar public olduğu sürece sınıfın koyduğu HİÇBİR kural
    // bağlayıcı değildir. Metottaki kontrol yalnızca o metodu
    // kullanmayı seçenler için geçerlidir.
    public string Ad;
    public decimal Fiyat;
    public int StokAdedi;

    public Urun(string ad, decimal fiyat, int stok)
    {
        Ad = ad;
        Fiyat = fiyat;
        StokAdedi = stok;
    }

    public void StokCikis(int adet)
    {
        if (adet > StokAdedi)
        {
            Console.WriteLine($"{Ad}: stok yetersiz! İstenen {adet}, mevcut {StokAdedi}.");
            return;
        }

        StokAdedi = StokAdedi - adet;
    }

    public decimal ToplamDeger()
    {
        return Fiyat * StokAdedi;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{Ad,-10} {Fiyat,10:C}  stok: {StokAdedi}");
    }
}

// --- DERS ---
//
// Bir kuralı metoda yazmak, o kuralı UYGULATMAZ. Kuralın bağlayıcı
// olması için, kuralı atlamanın yolunun KAPALI olması gerekir.
//
// "Ama ben o satırı yazmam ki" demeyin. Üç ay sonra kendi kodunuza
// döndüğünüzde, ya da baharda projeyi bir arkadaşınızla yazarken,
// o satırı biri yazacak. Sınıf kendini korumalı.
