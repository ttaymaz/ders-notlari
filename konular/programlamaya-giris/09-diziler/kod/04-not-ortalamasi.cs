// Dizideki değerlerle işlem yapma.
//
// Çalıştırmak için:  dotnet run 04-not-ortalamasi.cs
//
// NOT: Bu hafta döngü kullanmıyoruz — elemanlara tek tek erişiyoruz.
// Gelecek hafta aynı işi döngüyle, kaç eleman olursa olsun yapacağız.

int[] notlar = { 80, 95, 70 };

int toplam = notlar[0] + notlar[1] + notlar[2];

// DİKKAT: 3 yerine 3.0 yazdık. Tam sayı bölmesi tuzağını hatırlayın —
// int / int işlemi ondalık kısmı atardı.
double ortalama = toplam / 3.0;

Console.WriteLine($"Notlar: {notlar[0]}, {notlar[1]}, {notlar[2]}");
Console.WriteLine($"Toplam: {toplam}");
Console.WriteLine($"Ortalama: {ortalama}");

// --- Düşünün ---
// Bu kod 3 not için çalışıyor. 100 not olsaydı ne yazacaktınız?
// notlar[0] + notlar[1] + ... + notlar[99]  → 100 terimlik bir satır mı?
//
// Gelecek haftanın konusu tam olarak bu sorunun cevabı.
