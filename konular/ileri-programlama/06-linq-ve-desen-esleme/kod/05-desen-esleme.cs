// Desen eşleme: bir değerin "şekline" bakarak karar vermek.
//
// switch ifadesi her kolda bir desen dener; ilk eşleşen kazanır.
// Kitap bir record olduğu için özelliklerine desenle doğrudan bakabiliriz.
// Çalıştırmak için:  dotnet run 05-desen-esleme.cs

var kitaplar = new List<Kitap>
{
    new("Çalıkuşu", "Reşat Nuri Güntekin", 1922),
    new("Kuyucaklı Yusuf", "Sabahattin Ali", 1937),
    new("İnce Memed", "Yaşar Kemal", 1955),
    new("Orta Direk", "Yaşar Kemal", 1960),
};

foreach (var k in kitaplar)
    Console.WriteLine($"{k.Baslik,-16} {Donem(k),-25} {Etiket(k)}");

// İlişkisel desen: <, >, and, or
string Donem(Kitap k) => k.Yil switch
{
    < 1923 => "Cumhuriyet öncesi",
    >= 1923 and < 1950 => "Cumhuriyetin ilk yılları",
    _ => "1950 ve sonrası",
};

// Özellik deseni: { Özellik: desen }
string Etiket(Kitap k) => k switch
{
    { Yazar: "Yaşar Kemal", Yil: > 1955 } => "Yaşar Kemal, sonraki",
    { Yazar: "Yaşar Kemal" } => "Yaşar Kemal",
    { Baslik.Length: > 12 } => "uzun başlık",
    _ => "",
};

// Liste deseni: dizinin şekline bakmak
int[] yillar = [1922, 1937, 1955, 1960];
string ozet = yillar switch
{
    [] => "boş",
    [var tek] => $"tek yıl: {tek}",
    [var ilk, .., var son] => $"{ilk}'den {son}'a, {yillar.Length} kitap",
};
Console.WriteLine(ozet);

// is ile tip deseni ve değişkene bağlama
object o = kitaplar[0];
if (o is Kitap { Yil: < 1930 } eski)
    Console.WriteLine($"eski bir kitap: {eski.Baslik}");

record Kitap(string Baslik, string Yazar, int Yil);
