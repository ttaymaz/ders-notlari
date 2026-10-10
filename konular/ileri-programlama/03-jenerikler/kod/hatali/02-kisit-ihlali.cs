// KASITLI OLARAK DERLENMEZ.
//
// EnBuyuk metodunun kısıtı doğru; sorun çağıran tarafta. Kitap,
// IComparable<Kitap> sözünü vermiyor. Derleyici bunu çağrı satırında yakalar.
// Derlemeyi deneyin:  dotnet build 02-kisit-ihlali.cs
//
// Görev: Kitap'ı yayın yılına göre karşılaştırılabilir yapın.

var k1 = new Kitap("Çalıkuşu", 1922);
var k2 = new Kitap("Sinekli Bakkal", 1936);
Console.WriteLine(EnBuyuk(k1, k2));

T EnBuyuk<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) >= 0 ? a : b;

record Kitap(string Baslik, int Yil);
