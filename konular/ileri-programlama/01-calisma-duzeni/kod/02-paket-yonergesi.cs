// Tek dosyada NuGet paketi kullanmak: #:package yönergesi.
//
// İlk satır, projedeki <PackageReference> satırının tek dosyadaki
// karşılığıdır. Sürüm sabitlenir; "en son sürüm" yazılmaz.
// İlk çalıştırmada paket indirilir, internet bağlantısı gerekir.
//
// Çalıştırmak için:  dotnet run 02-paket-yonergesi.cs

#:package Spectre.Console@0.57.2

using Spectre.Console;

var tablo = new Table();
tablo.AddColumn("Kitap");
tablo.AddColumn("Yazar");
tablo.AddColumn(new TableColumn("Yıl").RightAligned());

tablo.AddRow("Nutuk", "Mustafa Kemal Atatürk", "1927");
tablo.AddRow("İnce Memed", "Yaşar Kemal", "1955");
tablo.AddRow("Tutunamayanlar", "Oğuz Atay", "1972");

AnsiConsole.Write(tablo);
