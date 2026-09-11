// En büyük ve en küçüğü bulma — "Kral kim?" algoritması.
//
// Çalıştırmak için:  dotnet run 03-en-buyuk-en-kucuk.cs
//
// Mantık: ilk elemanı geçici olarak kral kabul et.
// Daha büyüğüne rastlarsan tacı ona devret.

int[] sayilar = { 15, 8, 42, 4, 23 };

int enBuyuk = sayilar[0];        // varsayım: ilk eleman en büyük
int enKucuk = sayilar[0];

foreach (int sayi in sayilar)
{
    if (sayi > enBuyuk) { enBuyuk = sayi; }
    if (sayi < enKucuk) { enKucuk = sayi; }
}

Console.WriteLine($"En büyük: {enBuyuk}");
Console.WriteLine($"En küçük: {enKucuk}");

// --- KRİTİK: neden sayilar[0], neden 0 değil? ---
//
// int enBuyuk = 0;  yazsaydık ve dizi { -15, -8, -42 } olsaydı,
// hiçbir sayı 0'dan büyük olmayacağı için sonuç 0 çıkardı —
// oysa 0 dizide bile yok!
//
// Başlangıç değeri her zaman dizinin İLK ELEMANI olmalıdır.
// Aşağıdaki satırların başındaki // işaretini kaldırıp deneyin:
//
// int[] negatifler = { -15, -8, -42, -4 };
// int yanlisBaslangic = 0;
// foreach (int s in negatifler) { if (s > yanlisBaslangic) yanlisBaslangic = s; }
// Console.WriteLine($"Yanlış sonuç: {yanlisBaslangic}");   // 0 yazar!
