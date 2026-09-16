// this: nesnenin kendisine referans.
//
// Geçen üç hafta boyunca kurucularda urunAdi, baslangicStogu gibi zorlama
// isimler kullandık. Sebebi buydu: alan adıyla parametre adı çakışıyordu.
// Bu dosya çakışmanın ne yaptığını ve this'in nasıl çözdüğünü gösteriyor.
//
// Şema: assets/01-this-isim-cakismasi.svg
// Çalıştırmak için:  dotnet run 01-this-isim-cakismasi.cs

Console.WriteLine("--- this KULLANMAYAN sınıf (bozuk) ---");
UrunBozuk bozuk = new UrunBozuk("Klavye", 750m);
bozuk.BilgiYazdir();
Console.WriteLine("Ad boş, fiyat sıfır. Kurucuya değerleri verdiğimiz hâlde.");

Console.WriteLine("\n--- this KULLANAN sınıf (doğru) ---");
Urun dogru = new Urun("Klavye", 750m);
dogru.BilgiYazdir();


// --- YANLIŞ: alan ile parametre aynı adı taşıyor, this yok ---
class UrunBozuk
{
    private string ad = "";
    private decimal fiyat;

    public UrunBozuk(string ad, decimal fiyat)
    {
        // C# "en yakın kapsam kazanır" kuralını uygular: buradaki iki "ad"
        // da PARAMETREdir. Parametre kendine atanıyor, alana hiç dokunulmuyor.
        //
        // Derleyici bunu hata saymaz. Uyarı bile vermeyebilir.
        ad = ad;
        fiyat = fiyat;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"[bozuk]  Ad: \"{ad}\"  Fiyat: {fiyat:C}");
    }
}


// --- DOĞRU: this ile alanı işaret ediyoruz ---
class Urun
{
    private string ad = "";
    private decimal fiyat;

    public Urun(string ad, decimal fiyat)
    {
        // this.ad  → NESNENİN ALANI
        // ad       → parametre
        this.ad = ad;
        this.fiyat = fiyat;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"[doğru]  Ad: \"{ad}\"  Fiyat: {fiyat:C}");
    }
}

// --- this NEDİR? ---
//
// this, metodun o anda ÜZERİNDE ÇALIŞTIĞI nesneyi gösterir.
//
//     klavye.BilgiYazdir();   → metodun içinde this, klavye nesnesidir
//     mouse.BilgiYazdir();    → aynı metot, ama bu kez this mouse'tur
//
// Yani her nesne için ayrı bir metot kopyası yoktur; tek metot vardır ve
// hangi nesne üzerinden çağrıldığını this ile bilir.
//
// --- NE ZAMAN YAZILIR? ---
//
// Zorunlu:  alan adı ile parametre adı aynıysa (yukarıdaki durum)
// İsteğe bağlı: diğer her yerde. this.ad ile ad aynı şeydir.
//
// Artık kurucularda urunAdi gibi zorlama isimler uydurmak zorunda değiliz.
