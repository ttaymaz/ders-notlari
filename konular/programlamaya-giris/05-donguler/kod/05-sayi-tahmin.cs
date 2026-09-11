// Sayı tahmin oyunu — bu döneme kadar öğrendiğimiz her şey bir arada.
//
// Çalıştırmak için:  dotnet run 05-sayi-tahmin.cs
//
// Kullanılanlar: değişkenler, tip dönüşümü, if-else if-else,
//                do-while döngüsü, sayaç ve rastgele sayı.

// Random.Shared, C#'ın hazır rastgele sayı üretecidir.
// .Next(1, 101) → 1 DAHİL, 101 HARİÇ. Yani 1-100 arası.
int tutulanSayi = Random.Shared.Next(1, 101);

int tahmin = 0;
int denemeSayisi = 0;

Console.WriteLine("--- Sayı Tahmin Oyunu ---");
Console.WriteLine("Aklımdan 1-100 arasında bir sayı tuttum. Bakalım bulabilecek misin?");

do
{
    Console.Write("Tahmininiz: ");
    tahmin = Convert.ToInt32(Console.ReadLine());
    denemeSayisi++;

    if (tahmin > tutulanSayi)
    {
        Console.WriteLine("Daha KÜÇÜK bir sayı girin.");
    }
    else if (tahmin < tutulanSayi)
    {
        Console.WriteLine("Daha BÜYÜK bir sayı girin.");
    }
    else
    {
        Console.WriteLine($"TEBRİKLER! {denemeSayisi}. denemede bildiniz.");
    }
} while (tahmin != tutulanSayi);

// --- Neden do-while? ---
// Kullanıcıdan en az bir tahmin almamız GEREKİYOR. while kullansaydık,
// döngüye girmeden önce tahmin değişkenine bir değer atamamız gerekirdi.
