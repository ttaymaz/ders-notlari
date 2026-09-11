// Arama algoritması: aranan değer dizide var mı?
//
// Çalıştırmak için:  dotnet run 04-arama.cs

int[] sayilar = { 15, 8, 42, 4, 23 };

Console.Write("Aranacak sayı: ");
int aranan = Convert.ToInt32(Console.ReadLine());

// --- Yöntem 1: bayrak değişkeni ile ---
bool bulundu = false;

foreach (int sayi in sayilar)
{
    if (sayi == aranan)
    {
        bulundu = true;
    }
}

Console.WriteLine(bulundu ? $"{aranan} dizide VAR." : $"{aranan} dizide YOK.");

// --- Yöntem 2: indisini de bul (for gerekli!) ---
int konum = -1;              // -1 = bulunamadı

for (int i = 0; i < sayilar.Length; i++)
{
    if (sayilar[i] == aranan)
    {
        konum = i;
        break;               // bulduk, aramaya devam etmeye gerek yok
    }
}

if (konum >= 0)
{
    Console.WriteLine($"{aranan} sayısı {konum}. indiste bulundu.");
}
else
{
    Console.WriteLine($"{aranan} sayısı dizide yok.");
}

// İkinci yöntemde neden foreach kullanamadık?
// Çünkü elemanın KAÇINCI sırada olduğunu bilmemiz gerekiyor.
// foreach elemanı verir, indisini vermez.
