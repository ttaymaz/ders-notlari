// 05-kultur-tuzagi.cs dosyasının uyarısız hali.
//
// Kural: makinenin diline bağlı olmaması gereken her karşılaştırma ve
// dönüşümde kültürü açıkça söyleyin.
//   Karşılaştırma  -> StringComparison.OrdinalIgnoreCase
//   Dosya, ağ, ayar verisi -> CultureInfo.InvariantCulture
//
// Çalıştırmak için:  dotnet run 06-kultur-duzeltme.cs

#:property AnalysisLevel=latest-recommended

using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

string komut = "i";
Console.WriteLine(string.Equals(komut, "I", StringComparison.OrdinalIgnoreCase));

double fiyat = double.Parse("12.5", CultureInfo.InvariantCulture);
Console.WriteLine(fiyat.ToString(CultureInfo.InvariantCulture));
