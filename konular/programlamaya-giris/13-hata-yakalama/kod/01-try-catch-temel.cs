// try-catch: program çökmesin.
//
// Çalıştırmak için:  dotnet run 01-try-catch-temel.cs

Console.Write("Bir sayı giriniz: ");

try
{
    // TEHLİKELİ BÖLGE — hata verebilecek kodlar buraya
    int sayi = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine($"Karesi: {sayi * sayi}");
}
catch (Exception hata)
{
    // Hata olursa program ÇÖKMEZ, buraya atlar
    Console.WriteLine($"Bir hata oluştu: {hata.Message}");
}

Console.WriteLine("Program devam ediyor ve normal şekilde bitiyor.");

// --- ANALOJİ ---
// try   → araba sürmek. Kaza yapabilirsiniz.
// catch → emniyet kemeri. Kaza olursa sizi hayatta tutar.
//
// --- DENEYİN ---
// Önce bir sayı girin: normal çalışır.
// Sonra "on" yazın: hata yakalanır, program yine de son satıra ulaşır.
// try-catch'i silip "on" yazarsanız program çöker ve son satır çalışmaz.
