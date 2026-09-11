// C# hangi versiyonu seçer? Bilmek zorundasınız.
//
// Çalıştırmak için:  dotnet run 03-secim-tuzagi.cs

// --- TUZAK ---
Console.WriteLine(Geometri.AlanHesapla(5));       // KARE  → 25
Console.WriteLine(Geometri.AlanHesapla(5.0));     // DAİRE → 78.5

// 5 ve 5.0 sizin için aynı sayı. C# için DEĞİL.
// 5   → int    → kare versiyonu
// 5.0 → double → daire versiyonu
//
// Yarıçapı 5 olan dairenin alanını isteyip AlanHesapla(5) yazan
// öğrenci, karenin alanını alır ve neden yanlış olduğunu anlamaz.
// Program çökmez, hata vermez — sadece yanlış cevap verir.

// --- ÖRTÜK DÖNÜŞÜM ---
Console.WriteLine(Geometri.Carp(4, 5));    // int gönderdik, double'a çevrildi: 20

// Tam uyan imza yoksa C# örtük dönüşüm dener. int → double güvenlidir.
// Ters yön (double → int) veri kaybettireceği için otomatik yapılmaz.

// --- DERS ---
// Aşırı yükleme, metotlar AYNI İŞİ farklı verilerle yapıyorsa iyidir.
// Kare ile daire FARKLI işlerdir; aynı adı taşımamalıydılar.
// Doğrusu: KareAlani(int kenar) ve DaireAlani(double yaricap)


static class Geometri
{
    public static double AlanHesapla(int kenar)
    {
        Console.Write("KARE     → ");
        return kenar * kenar;
    }

    public static double AlanHesapla(double yaricap)
    {
        Console.Write("DAİRE    → ");
        return 3.14 * yaricap * yaricap;
    }

    public static double Carp(double a, double b) { return a * b; }
}
