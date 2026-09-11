// Kullanıcıdan veri alma ve metin araya ekleme.
//
// Çalıştırmak için:  dotnet run 01-kullanicidan-veri.cs

// Write kullanıyoruz ki imleç aynı satırda kalsın.
Console.Write("Adınız: ");
string ad = Console.ReadLine();

Console.Write("Şehriniz: ");
string sehir = Console.ReadLine();

// Eski yöntem — metinleri + ile birleştirme
Console.WriteLine("Hoş geldin, " + ad + "!");

// Tercih edilen yöntem — metin araya ekleme (string interpolation)
Console.WriteLine($"Hoş geldin {ad}, {sehir}'den selamlar!");
