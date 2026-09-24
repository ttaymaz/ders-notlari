// KASITLI OLARAK DERLENMEZ.
//
// 05-kultur-tuzagi.cs ile aynı kod; tek fark ikinci #:property satırı.
// TreatWarningsAsErrors açıkken uyarı, derlemeyi durduran hataya döner.
// Derlemeyi deneyin:  dotnet build 01-uyari-hata-olsun.cs
//
// Görev: dört hatayı 06-kultur-duzeltme.cs dosyasına bakmadan düzeltin.

#:property AnalysisLevel=latest-recommended
#:property TreatWarningsAsErrors=true

using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

string komut = "i";
Console.WriteLine(komut.ToUpper() == "I");

double fiyat = double.Parse("12.5");
Console.WriteLine(fiyat);
