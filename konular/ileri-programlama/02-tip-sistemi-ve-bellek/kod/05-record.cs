// record: veri taşıyan tipler için derleyicinin yazdığı sınıf.
//
// Tek satırlık tanım; kurucu, özellikler, değer eşitliği, okunaklı
// ToString ve "with" ile kopyalama derleyiciden gelir.
// Çalıştırmak için:  dotnet run 05-record.cs

var kitap = new Kitap("İnce Memed", "Yaşar Kemal", 1955);
Console.WriteLine(kitap);

// Özellikler yalnızca kurulurken verilir. Şu satır derlenmez:
// kitap.Yil = 1960;

var ikinciCilt = kitap with { Baslik = "İnce Memed 2", Yil = 1969 };
Console.WriteLine(ikinciCilt);
Console.WriteLine(kitap);

var (baslik, yazar, yil) = ikinciCilt;
Console.WriteLine($"{baslik} / {yazar} / {yil}");

record Kitap(string Baslik, string Yazar, int Yil);
