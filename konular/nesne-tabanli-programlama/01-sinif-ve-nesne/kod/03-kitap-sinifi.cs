// Kütüphane otomasyonunun ilk tuğlası: Kitap sınıfı.
//
// Bu sınıfı aklınızda tutun. Bahar döneminde aynı kitap kavramı
// veritabanındaki bir tabloya ve ekrandaki bir forma bağlanacak;
// aşağıdaki alanlar o tablonun sütunları olacak.
//
// Şema: assets/02-sinif-ve-nesneler.svg
// Çalıştırmak için:  dotnet run 03-kitap-sinifi.cs

Kitap kitap1 = new Kitap();
kitap1.Baslik = "Tutunamayanlar";
kitap1.Yazar = "Oğuz Atay";
kitap1.SayfaSayisi = 724;

Kitap kitap2 = new Kitap();
kitap2.Baslik = "Kürk Mantolu Madonna";
kitap2.Yazar = "Sabahattin Ali";
kitap2.SayfaSayisi = 160;

kitap1.BilgiYazdir();
kitap2.BilgiYazdir();

Console.WriteLine("\n--- Tutunamayanlar ödünç veriliyor ---");
kitap1.OduncVer();
kitap1.BilgiYazdir();

Console.WriteLine("\n--- Aynı kitap bir kez daha isteniyor ---");
kitap1.OduncVer();

Console.WriteLine("\n--- Diğer kitap etkilendi mi? ---");
kitap2.BilgiYazdir();

Console.WriteLine("\n--- Kitap iade ediliyor ---");
kitap1.IadeAl();
kitap1.BilgiYazdir();


class Kitap
{
    public string Baslik = "";
    public string Yazar = "";
    public int SayfaSayisi;

    // Nesnenin durumunu tutan alan. Her kitabın kendi durumu var:
    // birini ödünç vermek diğerini etkilemiyor.
    public bool OduncVerildi = false;

    public void OduncVer()
    {
        // Davranışın kararı nesnenin kendi verisine bakarak veriliyor.
        if (OduncVerildi)
        {
            Console.WriteLine($"\"{Baslik}\" zaten ödünçte, verilemez.");
        }
        else
        {
            OduncVerildi = true;
            Console.WriteLine($"\"{Baslik}\" ödünç verildi.");
        }
    }

    public void IadeAl()
    {
        if (OduncVerildi)
        {
            OduncVerildi = false;
            Console.WriteLine($"\"{Baslik}\" iade alındı.");
        }
        else
        {
            Console.WriteLine($"\"{Baslik}\" zaten rafta.");
        }
    }

    public void BilgiYazdir()
    {
        string durum = OduncVerildi ? "ödünçte" : "rafta";
        Console.WriteLine($"{Baslik} / {Yazar} ({SayfaSayisi} sayfa) — {durum}");
    }
}
