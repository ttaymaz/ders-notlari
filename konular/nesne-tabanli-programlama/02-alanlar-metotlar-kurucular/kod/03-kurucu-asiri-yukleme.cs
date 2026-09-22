// Kurucu metotların aşırı yüklenmesi.
//
// Aşırı yükleme: aynı adı taşıyan birden fazla metot yazmak ve doğru
// olanı derleyiciye seçtirmek. Bir TİP ÜYESİ özelliğidir — metodun bir
// sınıfın içinde olması gerekir. Artık sınıf yazdığımıza göre kurucular
// da bu kuralın içinde.
//
// Çalıştırmak için:  dotnet run 03-kurucu-asiri-yukleme.cs

// Aynı sınıf, üç farklı kuruluş biçimi. C# argümanlara bakıp karar veriyor.
Urun u1 = new Urun("Klavye", 750m, 12);
u1.BilgiYazdir();

Urun u2 = new Urun("Mouse", 320m);
u2.BilgiYazdir();

Urun u3 = new Urun("Monitör");
u3.BilgiYazdir();

Console.WriteLine();
Console.WriteLine("Üç nesne de EKSİKSİZ doğdu.");
Console.WriteLine("Verilmeyen bilgiler makul varsayılanlarla dolduruldu.");


class Urun
{
    public string Ad;
    public decimal Fiyat;
    public int StokAdedi;

    // Versiyon 1 — her şey verildi
    public Urun(string ad, decimal fiyat, int stok)
    {
        Console.WriteLine("kurucu [ad, fiyat, stok] çalıştı:");
        Ad = ad;
        Fiyat = fiyat;
        StokAdedi = stok;
    }

    // Versiyon 2 — stok verilmedi, sıfırdan başlasın
    public Urun(string ad, decimal fiyat)
    {
        Console.WriteLine("kurucu [ad, fiyat] çalıştı:");
        Ad = ad;
        Fiyat = fiyat;
        StokAdedi = 0;
    }

    // Versiyon 3 — yalnızca ad verildi
    public Urun(string ad)
    {
        Console.WriteLine("kurucu [ad] çalıştı:");
        Ad = ad;
        Fiyat = 0m;
        StokAdedi = 0;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"{Ad,-10} {Fiyat,10:C}  stok: {StokAdedi}");
    }
}

// --- İMZA KURALLARI DEĞİŞMEDİ ---
//
// Kural metotlarda ne ise kurucularda da odur: iki kurucunun imzası
// farklı olmalı. Yani parametrelerin SAYISI, TİPİ veya SIRASI
// değişmeli.
//
// Şunu yazamazsınız — aynı imza, derleme hatası:
//
//     public Urun(string ad, decimal fiyat) { }
//     public Urun(string urunAdi, decimal tutar) { }
//
// Parametre ADI imzaya dahil değildir.
//
// --- DİKKAT: TEKRAR EDEN KOD ---
//
// Üç kurucunun da gövdesi birbirine benziyor. Şimdilik kabul ediyoruz,
// ama bu tekrarı ortadan kaldırmanın bir yolu var: bir kurucunun
// diğerini çağırması. Bunu "this" anahtar kelimesini öğrendiğimiz
// haftada yapacağız.
