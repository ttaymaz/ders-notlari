// Dizi tanımlama ve oluşturmanın üç yolu.
//
// Çalıştırmak için:  dotnet run 01-dizi-olusturma.cs

// --- Yol 1: önce tanımla, sonra oluştur ---
int[] sayilar;                      // "int tutan bir dizi olacak" dedik
sayilar = new int[5];               // bellekte 5 gözlü yer ayırdık

// --- Yol 2: tek satırda (en sık kullanılan) ---
int[] notlar = new int[3];
string[] gunler = new string[7];

// --- Yol 3: değerleri baştan biliyorsak ---
int[] numaralar = { 10, 20, 30 };
string[] isimler = { "Ali", "Veli", "Ayşe" };

// Yol 3 şunun kısaltmasıdır:
//   string[] isimler = new string[3];
//   isimler[0] = "Ali";
//   isimler[1] = "Veli";
//   isimler[2] = "Ayşe";

Console.WriteLine($"sayilar dizisi {sayilar.Length} elemanlı");
Console.WriteLine($"gunler dizisi {gunler.Length} elemanlı");
Console.WriteLine($"isimler dizisi {isimler.Length} elemanlı");

// .Length bir metot değil, ÖZELLİKtir — sonuna parantez KOYULMAZ.
// isimler.Length()  → yanlış
// isimler.Length    → doğru
