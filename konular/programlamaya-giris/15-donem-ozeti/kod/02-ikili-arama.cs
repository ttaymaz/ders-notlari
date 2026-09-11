// BAHAR DÖNEMİ FRAGMANI: sıralı dizide daha akıllı arama.
//
// Çalıştırmak için:  dotnet run 02-ikili-arama.cs
//
// Bu algoritmayı bahar döneminde ayrıntısıyla göreceksiniz.
// Buradaki amaç, "daha iyi algoritma" fikrinin ne demek olduğunu görmek.

using System.Diagnostics;

int[] dizi = new int[1_000_000];
for (int i = 0; i < dizi.Length; i++) { dizi[i] = i; }   // SIRALI dizi

int aranan = 999_999;      // en kötü durum: sonda

// --- Doğrusal arama: baştan sona tara ---
Stopwatch k1 = Stopwatch.StartNew();
int adim1 = 0;
for (int i = 0; i < dizi.Length; i++)
{
    adim1++;
    if (dizi[i] == aranan) { break; }
}
k1.Stop();

// --- İkili arama: her adımda YARIYA böl ---
Stopwatch k2 = Stopwatch.StartNew();
int adim2 = 0;
int sol = 0, sag = dizi.Length - 1;
while (sol <= sag)
{
    adim2++;
    int orta = (sol + sag) / 2;

    if (dizi[orta] == aranan) { break; }
    else if (dizi[orta] < aranan) { sol = orta + 1; }   // sol yarıyı at
    else { sag = orta - 1; }                            // sağ yarıyı at
}
k2.Stop();

Console.WriteLine($"Doğrusal arama: {adim1:N0} adım, {k1.Elapsed.TotalMilliseconds:F4} ms");
Console.WriteLine($"İkili arama   : {adim2:N0} adım, {k2.Elapsed.TotalMilliseconds:F4} ms");
Console.WriteLine($"\nAdım farkı: {adim1 / (double)adim2:N0} kat");

// --- NEDEN BU KADAR HIZLI? ---
// Her adımda kalan eleman sayısı YARIYA iniyor:
//   1.000.000 → 500.000 → 250.000 → ... → 1
// Kaç adım sürer? Yaklaşık 20.
//
// AMA BİR BEDELİ VAR: dizi SIRALI olmak zorunda.
// Sıralamanın kendisi de zaman alır. Ne zaman değer?
// Bir kez sıralayıp bin kez arayacaksanız kesinlikle değer.
//
// Bahar döneminde: sıralama algoritmaları ve Big O analizi.
