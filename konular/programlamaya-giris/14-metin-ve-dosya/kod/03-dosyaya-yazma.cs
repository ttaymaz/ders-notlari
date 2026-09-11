// Dosyaya yazma: StreamWriter.
//
// Çalıştırmak için:  dotnet run 03-dosyaya-yazma.cs

string yol = "notlarim.txt";

// using: blok bittiğinde dosya OTOMATİK kapanır — hata olsa bile.
// İkinci parametre: true = sona ekle, false = sıfırdan yaz
using (StreamWriter yazici = new StreamWriter(yol, append: true))
{
    yazici.WriteLine("Bu satır dosyaya eklenecek.");
    yazici.Write("Bu metin ise ");
    yazici.WriteLine("yanına yazılacak.");
}   // <- burada dosya kapanır, Close() yazmaya gerek yok

Console.WriteLine($"Kayıt başarılı: {Path.GetFullPath(yol)}");

// --- Daha da kısa yazım (using bildirimi) ---
// using StreamWriter y = new StreamWriter(yol, true);
// ...
// Dosya, kapsam bitince otomatik kapanır. Parantez bile gerekmez.

// --- NEDEN using? ---
// Close() yazsaydık ve arada bir hata oluşsaydı, Close() satırına
// HİÇ ULAŞILMAZDI. Dosya kilitli kalır, veriler diske yazılmazdı.
// using, hata olsa da olmasa da kapatmayı garanti eder.

// --- Tek satırlık kestirme ---
File.AppendAllText(yol, "Bu satır File sınıfıyla eklendi.\n");
