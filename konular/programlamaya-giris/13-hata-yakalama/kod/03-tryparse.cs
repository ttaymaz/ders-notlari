// TryParse: try-catch'siz güvenli dönüşüm.
//
// Çalıştırmak için:  dotnet run 03-tryparse.cs
//
// TryParse iki şey birden söyler:
//   return değeri  → dönüşüm başarılı mı? (bool)
//   out parametresi → başarılıysa sonuç kaç?

Console.Write("Bir sayı giriniz: ");
string giris = Console.ReadLine();

// Modern yazım: değişkeni doğrudan out içinde tanımlıyoruz
if (int.TryParse(giris, out int sayi))
{
    Console.WriteLine($"Çevirdim: {sayi}, karesi: {sayi * sayi}");
}
else
{
    Console.WriteLine("Hatalı giriş! Lütfen sadece rakam kullanın.");
}

// --- Eski yazım (internetteki örneklerde göreceksiniz) ---
// int sonuc;
// bool basariliMi = int.TryParse(giris, out sonuc);
// if (basariliMi) { ... }
//
// İkisi de aynı işi yapar; modern yazım daha kısa.

// --- out NEDİR? ---
// Bir metot return ile SADECE BİR değer döndürebilir.
// out, parametreyi veri GÖNDERMEK için değil, veri ÇIKARMAK için kullanır.
//
// ANALOJİ: Arkadaşınızı markete gönderiyorsunuz.
//   normal parametre → eline para verirsiniz (içeri gider)
//   return           → para üstünü getirir   (bir sonuç)
//   out parametresi  → boş poşet verirsiniz, dolu getirir (ikinci sonuç)

// double için de aynısı var:
Console.Write("Ondalıklı bir sayı giriniz: ");
if (double.TryParse(Console.ReadLine(), out double ondalikli))
{
    Console.WriteLine($"İki katı: {ondalikli * 2}");
}
else
{
    Console.WriteLine("Geçersiz ondalıklı sayı.");
}
