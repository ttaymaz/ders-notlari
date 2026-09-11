// EN SIK YAPILAN HATA: string metotları metni DEĞİŞTİRMEZ.
//
// Çalıştırmak için:  dotnet run 02-string-degismezlik.cs
//
// C#'ta string DEĞİŞMEZDİR (immutable). Metotlar orijinali değiştirmez,
// YENİ bir metin üretip döndürür.

string ad = "  ahmet  ";

// --- YANLIŞ ---
ad.Trim();          // sonucu hiçbir yere atamadık
ad.ToUpper();       // bu da öyle
Console.WriteLine($"Yanlış kullanım: [{ad}]");     // hâlâ "  ahmet  "

// --- DOĞRU ---
ad = ad.Trim();
ad = ad.ToUpper();
Console.WriteLine($"Doğru kullanım : [{ad}]");     // "AHMET"

// --- Zincirleme de yapılabilir ---
string soyad = "  yilmaz  ";
soyad = soyad.Trim().ToUpper();
Console.WriteLine($"Zincirleme     : [{soyad}]");

// --- NEDEN? ---
// Metot çağrısı bir DEĞER üretir. O değeri kullanmazsanız atılır.
// On birinci haftada aynı şeyi konuşmuştuk:
//   Topla(5, 3);   → hesaplanır ve atılır
//   int x = Topla(5, 3);  → sonuç yakalanır
//
// string metotları da aynı: sonucu yakalamalısınız.
