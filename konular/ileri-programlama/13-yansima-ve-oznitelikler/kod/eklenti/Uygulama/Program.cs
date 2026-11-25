using Sozlesme;
using Uygulama;

// Eklenti mimarisi: uygulama, derlenirken var olmayan rapor sınıflarını
// çalışırken bir klasörden yükler. Yeni bir rapor eklemek için uygulamayı
// yeniden derlemek gerekmez: DLL'i eklentiler/ klasörüne bırakmak yeter.

Odunc[] odunclar =
[
    new("Huzur", "Ece", 0),
    new("Aylak Adam", "Can", 6),
    new("Huzur", "Deniz", 2),
    new("Beş Şehir", "Ece", 0),
    new("Huzur", "Mert", 11),
    new("Aylak Adam", "Zeynep", 0),
];

string klasor = Path.Combine(AppContext.BaseDirectory, "eklentiler");
var eklentiler = EklentiYukleyici.KlasordenYukle<IRaporEklentisi>(klasor);

Console.WriteLine($"eklentiler/ klasöründe {Directory.GetFiles(klasor, "*.dll").Length} DLL, {eklentiler.Count} rapor");
foreach (var eklenti in eklentiler)
{
    Console.WriteLine();
    Console.WriteLine($"[{eklenti.Ad}] ({eklenti.GetType().Name})");
    Console.WriteLine(eklenti.Uret(odunclar));
}
