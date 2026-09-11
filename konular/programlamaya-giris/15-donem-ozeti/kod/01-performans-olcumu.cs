// Kodlarımız ne kadar hızlı? Tahmin etmeyelim, ÖLÇELİM.
//
// Çalıştırmak için:  dotnet run 01-performans-olcumu.cs

using System.Diagnostics;

// 1 milyon elemanlı bir dizi hazırlayalım: 0, 1, 2, ... 999999
int[] dizi = new int[1_000_000];
for (int i = 0; i < dizi.Length; i++)
{
    dizi[i] = i;
}

Console.WriteLine($"Dizi hazır: {dizi.Length:N0} eleman\n");

// --- DENEY 1: EN İYİ DURUM — aranan başta ---
int aranan = 0;
Stopwatch kronometre = Stopwatch.StartNew();
int adim = DogrusalAra(dizi, aranan);
kronometre.Stop();
Console.WriteLine($"Aranan BAŞTA  : {adim:N0} adım, {kronometre.Elapsed.TotalMilliseconds:F3} ms");

// --- DENEY 2: EN KÖTÜ DURUM — aranan sonda ---
aranan = 999_999;
kronometre = Stopwatch.StartNew();
adim = DogrusalAra(dizi, aranan);
kronometre.Stop();
Console.WriteLine($"Aranan SONDA  : {adim:N0} adım, {kronometre.Elapsed.TotalMilliseconds:F3} ms");

// --- DENEY 3: HİÇ YOK ---
aranan = -1;
kronometre = Stopwatch.StartNew();
adim = DogrusalAra(dizi, aranan);
kronometre.Stop();
Console.WriteLine($"Aranan HİÇ YOK: {adim:N0} adım, {kronometre.Elapsed.TotalMilliseconds:F3} ms");

// --- DENEY 4: İÇ İÇE DÖNGÜ — altıncı haftanın tablosu ---
Console.WriteLine("\nİç içe döngü (altıncı haftadaki tablo):");
foreach (int n in new[] { 100, 1_000, 10_000 })
{
    kronometre = Stopwatch.StartNew();
    long tur = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            tur++;
    kronometre.Stop();
    Console.WriteLine($"  {n:N0} x {n:N0} = {tur:N0} tur → {kronometre.Elapsed.TotalMilliseconds:F1} ms");
}

// Onuncu haftada yazdığımız arama algoritması, adım sayacı eklenmiş hali
int DogrusalAra(int[] d, int hedef)
{
    int sayac = 0;
    for (int i = 0; i < d.Length; i++)
    {
        sayac++;
        if (d[i] == hedef) { return sayac; }
    }
    return sayac;
}

// --- DÜŞÜNÜN ---
// Dizi 10 kat büyürse süre kaç kat artar? Deneyin: 1_000_000 → 10_000_000
// İç içe döngüde dizi 10 kat büyürse? Tabloya bakın.
