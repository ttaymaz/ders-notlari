// En sık kullanılan string metotları.
//
// Çalıştırmak için:  dotnet run 01-string-metotlari.cs

string metin = "  Merhaba Dünya  ";

Console.WriteLine($"Orijinal   : [{metin}]");
Console.WriteLine($"Length     : {metin.Length}");           // boşluklar dahil
Console.WriteLine($"ToUpper    : [{metin.ToUpper()}]");
Console.WriteLine($"ToLower    : [{metin.ToLower()}]");
Console.WriteLine($"Trim       : [{metin.Trim()}]");         // baş/son boşluklar gitti
Console.WriteLine($"Contains   : {metin.Contains("Dünya")}");
Console.WriteLine($"Replace    : [{metin.Replace("Dünya", "C#")}]");

// Substring(baslangic, adet) — indisler 0'dan başlar, dizilerdeki gibi
string temiz = metin.Trim();                                 // "Merhaba Dünya"
Console.WriteLine($"Substring  : [{temiz.Substring(0, 7)}]"); // "Merhaba"
Console.WriteLine($"Substring  : [{temiz.Substring(8)}]");    // "Dünya" (sonuna kadar)

// string aslında bir KARAKTER DİZİSİDİR — indisle erişebilirsiniz
Console.WriteLine($"İlk harf   : {temiz[0]}");
Console.WriteLine($"Son harf   : {temiz[temiz.Length - 1]}");

// --- Split: metni parçalara ayırır, DİZİ döndürür ---
string satir = "Ali;Veli;Ayşe;Fatma";
string[] isimler = satir.Split(';');

Console.WriteLine($"\n{isimler.Length} isim bulundu:");
foreach (string isim in isimler)
{
    Console.WriteLine($"  - {isim}");
}

// Split, dosyadan okunan satırları işlemek için çok kullanışlıdır.
