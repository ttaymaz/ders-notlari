// Kapsam (scope): metot içindeki değişkenler dışarıdan görünmez.
//
// Çalıştırmak için:  dotnet run 05-kapsam.cs

int KareAl(int sayi)
{
    int sonuc = sayi * sayi;     // 'sonuc' YALNIZCA bu metodun içinde yaşar
    return sonuc;
}

int deger = KareAl(7);
Console.WriteLine($"7'nin karesi: {deger}");

// --- Denemeniz için ---
// Aşağıdaki satırın başındaki // işaretini kaldırın. Derlenmiyor. Neden?
//
// Console.WriteLine(sonuc);
//
// Çünkü 'sonuc' metodun içinde tanımlandı, metot bitince yok oldu.
// Altıncı haftada döngü sayacı için de aynı şeyi konuşmuştuk:
// bir değişken, tanımlandığı blok içinde yaşar.

// --- DİZİLER FARKLIDIR ---
void HepsineBesEkle(int[] dizi)
{
    for (int i = 0; i < dizi.Length; i++) { dizi[i] += 5; }
}

int[] notlar = { 50, 60, 70 };
Console.WriteLine($"Önce : {string.Join(", ", notlar)}");
HepsineBesEkle(notlar);
Console.WriteLine($"Sonra: {string.Join(", ", notlar)}");   // DEĞİŞTİ!

// Sayı gönderirseniz metot bir KOPYA alır, aslını değiştiremez.
// Dizi gönderirseniz metot ASLINA erişir ve değiştirebilir.
// Sebebini bahar döneminde bellek yönetimiyle birlikte göreceksiniz.
