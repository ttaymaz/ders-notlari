// Alanlar + metotlar + kurucular bir arada: eksiksiz bir Urun sınıfı.
//
// Bu sınıf bahar döneminde stok takip ekranının arkasında duracak sınıfın
// ilk hâlidir. Bugün konsola yazıyor; baharda aynı metotlar bir formdaki
// düğmelere bağlanacak ve veriler veritabanından gelecek.
//
// Çalıştırmak için:  dotnet run 04-urun-tam.cs

Urun klavye = new Urun("Klavye", 750m, 12);
Urun mouse = new Urun("Mouse", 320m);

Console.WriteLine("--- Başlangıç durumu ---");
klavye.BilgiYazdir();
mouse.BilgiYazdir();

Console.WriteLine("\n--- Stok girişi ---");
mouse.StokGiris(50);
mouse.BilgiYazdir();

Console.WriteLine("\n--- Normal satış ---");
klavye.StokCikis(5);
klavye.BilgiYazdir();

Console.WriteLine("\n--- Stoktan fazlasını satmayı deneyelim ---");
klavye.StokCikis(100);
klavye.BilgiYazdir();

Console.WriteLine("\n--- Depo toplam değeri ---");
Urun[] depo = { klavye, mouse };
decimal toplam = 0m;

foreach (Urun u in depo)
{
    toplam = toplam + u.ToplamDeger();
}

Console.WriteLine($"Depodaki toplam değer: {toplam:C}");


class Urun
{
    // ALANLAR — nesnenin verisi
    public string Ad;
    public decimal Fiyat;
    public int StokAdedi;

    // Para için decimal kullanıyoruz, double değil. double ondalık
    // sayıları yaklaşık tutar ve para hesabında kuruş kayması yapar.
    // decimal sabitlerin sonuna m yazılır: 750m
    public Urun(string ad, decimal fiyat, int stok)
    {
        Ad = ad;
        Fiyat = fiyat;
        StokAdedi = stok;
    }

    public Urun(string ad, decimal fiyat)
    {
        Ad = ad;
        Fiyat = fiyat;
        StokAdedi = 0;
    }

    // METOTLAR — nesnenin davranışı.
    // Hiçbiri mevcut stoğu parametre olarak almıyor: StokAdedi alanı
    // zaten nesnenin içinde ve metot ona doğrudan erişiyor.
    public void StokGiris(int adet)
    {
        if (adet <= 0)
        {
            Console.WriteLine($"{Ad}: giriş adedi pozitif olmalı.");
            return;
        }

        StokAdedi = StokAdedi + adet;
        Console.WriteLine($"{Ad}: {adet} adet giriş yapıldı.");
    }

    public void StokCikis(int adet)
    {
        if (adet <= 0)
        {
            Console.WriteLine($"{Ad}: çıkış adedi pozitif olmalı.");
            return;
        }

        // Kararı nesne kendi verisine bakarak veriyor.
        if (adet > StokAdedi)
        {
            Console.WriteLine($"{Ad}: stok yetersiz! İstenen {adet}, mevcut {StokAdedi}.");
            return;
        }

        StokAdedi = StokAdedi - adet;
        Console.WriteLine($"{Ad}: {adet} adet çıkış yapıldı.");
    }

    // Değer döndüren metot — hesabı yapan yer, veriyi taşıyan yer.
    public decimal ToplamDeger()
    {
        return Fiyat * StokAdedi;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{Ad,-10} {Fiyat,10:C}  stok: {StokAdedi,3}  değer: {ToplamDeger(),10:C}");
    }
}

// --- DENEMENİZ İÇİN ---
//
// StokCikis metodunun içindeki stok kontrolünü silin ve 100 adet
// satmayı tekrar deneyin. Stok eksiye düşüyor mu? Program bunu
// engelliyor mu?
//
// Sonra şunu deneyin: klavye.StokAdedi = -50; satırını ana programa
// ekleyin. Metot içindeki kontrol sizi bundan koruyor mu?
//
// Koruyamıyor — çünkü alan public. Bu kapıyı kapatmak kapsülleme
// haftasının konusu.
