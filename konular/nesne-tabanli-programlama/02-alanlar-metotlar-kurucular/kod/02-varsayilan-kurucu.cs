// Varsayılan kurucu: ne zaman var, ne zaman kayboluyor?
//
// Bu dosya dönemin en çok kafa karıştıran kurallarından birini gösterir:
// kendi kurucunuzu yazdığınız anda C#'ın verdiği görünmez kurucu KAYBOLUR.
//
// Şema: assets/02-varsayilan-kurucu.svg
// Çalıştırmak için:  dotnet run 02-varsayilan-kurucu.cs

// A sınıfının hiç kurucusu yok → C# görünmez bir parametresiz kurucu ekliyor.
Console.WriteLine("--- Kurucusuz sınıf ---");
Kalem kalem = new Kalem();
Console.WriteLine($"Ad: \"{kalem.Ad}\"  Fiyat: {kalem.Fiyat}");
Console.WriteLine("Alanlar varsayılan değerlerini aldı (boş metin ve 0).");

// B sınıfında parametreli kurucu YAZILDI → parametresiz olan artık yok.
Console.WriteLine("\n--- Kurucu yazılmış sınıf ---");
Defter defter = new Defter("Kareli Defter", 45m);
Console.WriteLine($"Ad: \"{defter.Ad}\"  Fiyat: {defter.Fiyat}");

// Aşağıdaki satırın yorumunu kaldırın ve derlemeyi deneyin:
//
//     Defter bos = new Defter();
//
// Hata: 'Defter' does not contain a constructor that takes 0 arguments
//
// Sebebi: Defter sınıfında ELLE yazılmış bir kurucu var. C# artık
// görünmez parametresiz kurucuyu EKLEMİYOR.

// C sınıfı ikisini birden yazmış → her iki kullanım da çalışıyor.
Console.WriteLine("\n--- İki kurucusu olan sınıf ---");
Silgi silgi1 = new Silgi();
Silgi silgi2 = new Silgi("Beyaz Silgi", 15m);
Console.WriteLine($"1: \"{silgi1.Ad}\"  {silgi1.Fiyat}");
Console.WriteLine($"2: \"{silgi2.Ad}\"  {silgi2.Fiyat}");


// Hiç kurucu yok: görünmez parametresiz kurucu devrede.
class Kalem
{
    public string Ad = "";
    public decimal Fiyat;
}

// Parametreli kurucu var: parametresiz olan KAYBOLDU.
class Defter
{
    public string Ad;
    public decimal Fiyat;

    public Defter(string ad, decimal fiyat)
    {
        Ad = ad;
        Fiyat = fiyat;
    }
}

// İhtiyacınız varsa parametresizi ELLE geri eklersiniz.
class Silgi
{
    public string Ad;
    public decimal Fiyat;

    // Parametresiz kurucu — makul başlangıç değerleri veriyor.
    public Silgi()
    {
        Ad = "(isimsiz)";
        Fiyat = 0m;
    }

    public Silgi(string ad, decimal fiyat)
    {
        Ad = ad;
        Fiyat = fiyat;
    }
}

// --- KURAL ---
//
// Sınıfta HİÇ kurucu yoksa  → C# parametresiz kurucuyu kendisi ekler.
// Sınıfta BİR kurucu varsa  → görünmez kurucu artık eklenmez.
//
// Bu yüzden parametreli kurucu yazdıktan sonra new Sinif() yazan kodlar
// aniden derlenmez olur. Hata sizi şaşırtmasın: bir şey bozulmadı,
// görünmez kurucu devre dışı kaldı.
