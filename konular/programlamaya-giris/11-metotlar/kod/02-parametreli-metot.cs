// Parametre alan metot: dışarıdan bilgi ister.
//
// Çalıştırmak için:  dotnet run 02-parametreli-metot.cs

// "string ad" → bu metot çalışmak için bir isme ihtiyaç duyar
void OzelSelamla(string ad)
{
    Console.WriteLine($"Merhaba Sayın {ad}, hoş geldiniz!");
}

// Birden fazla parametre virgülle ayrılır
void Bilgi(string ad, int yas)
{
    Console.WriteLine($"{ad}, {yas} yaşında.");
}

OzelSelamla("Ahmet");
OzelSelamla("Ayşe");

string isim = "Mehmet";
OzelSelamla(isim);          // değişken de gönderebiliriz

Bilgi("Zeynep", 20);

// --- Terimler ---
// Tanımdaki  (string ad)   → PARAMETRE  (metodun beklediği)
// Çağrıdaki  ("Ahmet")     → ARGÜMAN    (gerçekten gönderdiğiniz)
