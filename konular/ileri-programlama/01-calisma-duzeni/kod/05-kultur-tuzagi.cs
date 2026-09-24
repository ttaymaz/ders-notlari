// Derleyicinin susup çözümleyicinin konuştuğu yer: kültür.
//
// İlk satır kod çözümleyicilerinin önerilen kurallarını açar. O satırı
// silip derlerseniz hiçbir uyarı görmezsiniz; program yine yanlış çalışır.
// Uyarıları görmek için:  dotnet build 05-kultur-tuzagi.cs
// Çalıştırmak için:       dotnet run 05-kultur-tuzagi.cs
// Düzeltilmiş sürüm:      06-kultur-duzeltme.cs

#:property AnalysisLevel=latest-recommended

using System.Globalization;

// Sonuç bilgisayarın diline bağlı olmasın: kültürü açıkça Türkçe yapıyoruz.
CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

string komut = "i";
Console.WriteLine(komut.ToUpper() == "I");

double fiyat = double.Parse("12.5");
Console.WriteLine(fiyat);
