// Tek dosyalık program: proje, çözüm veya Main yok.
//
// .NET 10 ile gelen "dosya tabanlı uygulama" biçimi. Arka planda bir proje
// dosyası yine oluşturulur, ama siz görmezsiniz. Görmek isterseniz:
//     dotnet project convert 01-tek-dosya.cs
//
// Çalıştırmak için:  dotnet run 01-tek-dosya.cs
// Beklenen çıktı:    kod/README.md, "Çıktılar" bölümü

using System.Globalization;

Console.WriteLine($".NET sürümü : {Environment.Version}");
Console.WriteLine($"Kültür      : {CultureInfo.CurrentCulture.Name}");
Console.WriteLine($"İşletim s.  : {Environment.OSVersion}");
