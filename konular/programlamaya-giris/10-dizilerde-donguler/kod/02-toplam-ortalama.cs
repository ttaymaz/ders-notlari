// Toplam ve ortalama — "kumbara" algoritması.
//
// Çalıştırmak için:  dotnet run 02-toplam-ortalama.cs
//
// Mantık: bir kumbara (biriktirici) aç, döngüyle her sayıyı içine at.

int[] notlar = { 50, 80, 70, 90, 40 };

int toplam = 0;              // kumbara boş başlar (TOPLAMA için 0)

foreach (int n in notlar)
{
    toplam += n;
}

// Ortalamayı ondalıklı almak için biri double'a çevrilir.
double ortalama = (double)toplam / notlar.Length;

Console.WriteLine($"Eleman sayısı : {notlar.Length}");
Console.WriteLine($"Toplam        : {toplam}");
Console.WriteLine($"Ortalama      : {ortalama}");

// --- Denemeniz için ---
// (double) çevrimini silin: double ortalama = toplam / notlar.Length;
// Sonuç ne oluyor? Üçüncü haftanın tam sayı bölmesi tuzağı burada da geçerli.
//
// Not: biriktirici döngünün DIŞINDA tanımlı. İçinde olsaydı her turda
// sıfırlanırdı — altıncı haftada faktöriyelde konuşmuştuk.
