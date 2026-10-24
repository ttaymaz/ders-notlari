// En sık kullanılan LINQ operatörleri, tek bir kitap listesi üzerinde.
//
// Her satırın çıktısını önce tahmin edin. Sorgular yalnızca okur;
// kitaplar listesi hiçbir satırda değişmez.
// Çalıştırmak için:  dotnet run 03-linq-operatorleri.cs

using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

var kitaplar = new List<Kitap>
{
    new("Çalıkuşu", "Reşat Nuri Güntekin", 1922),
    new("Yaprak Dökümü", "Reşat Nuri Güntekin", 1930),
    new("Sinekli Bakkal", "Halide Edib Adıvar", 1936),
    new("Kuyucaklı Yusuf", "Sabahattin Ali", 1937),
    new("İçimizdeki Şeytan", "Sabahattin Ali", 1940),
    new("Kürk Mantolu Madonna", "Sabahattin Ali", 1943),
    new("Huzur", "Ahmet Hamdi Tanpınar", 1949),
    new("İnce Memed", "Yaşar Kemal", 1955),
    new("Orta Direk", "Yaşar Kemal", 1960),
    new("Saatleri Ayarlama Enstitüsü", "Ahmet Hamdi Tanpınar", 1961),
};

// Sıralama: önce yazara, eşitse yıla göre
foreach (var k in kitaplar.OrderBy(k => k.Yazar).ThenBy(k => k.Yil).Take(3))
    Console.WriteLine($"{k.Yazar} — {k.Baslik}");

// Tek eleman: First bulamazsa istisna fırlatır, FirstOrDefault null döner
Console.WriteLine(kitaplar.First(k => k.Yil > 1950).Baslik);
Console.WriteLine(kitaplar.FirstOrDefault(k => k.Yil > 2000)?.Baslik ?? "(yok)");

// Soru sormak: Any "en az biri", All "hepsi"
Console.WriteLine(kitaplar.Any(k => k.Yazar == "Sabahattin Ali"));
Console.WriteLine(kitaplar.All(k => k.Yil > 1920));

// Toplama
Console.WriteLine($"{kitaplar.Min(k => k.Yil)}–{kitaplar.Max(k => k.Yil)}");

// Gruplama: her yazar için bir grup
var gruplar = kitaplar
    .GroupBy(k => k.Yazar)
    .OrderByDescending(g => g.Count())
    .ThenBy(g => g.Key);

foreach (var g in gruplar)
    Console.WriteLine($"{g.Key}: {g.Count()} kitap, ilk {g.Min(k => k.Yil)}");

// Sorgu sözdizimi: aynı şeyin SQL'e benzeyen yazımı
var sorgu = from k in kitaplar
            where k.Yil >= 1940
            orderby k.Yil descending
            select k.Baslik;
Console.WriteLine(string.Join(", ", sorgu));

record Kitap(string Baslik, string Yazar, int Yil);
