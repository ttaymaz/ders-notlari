// Delege: bir metodu değişkene koymak, parametre olarak geçirmek.
//
// Delege bir tiptir; "şu imzadaki bir metot" demektir. Kosul tipindeki bir
// değişken, Kitap alıp bool döndüren HERHANGİ bir metodu tutabilir.
// Çalıştırmak için:  dotnet run 02-delege-nedir.cs

var calikusu = new Kitap("Çalıkuşu", "Reşat Nuri Güntekin", 1922);
var memed = new Kitap("İnce Memed", "Yaşar Kemal", 1955);

// 1) Kendi delege tipimiz: metot adı, parantezsiz
Kosul kosul = EskiMi;
Console.WriteLine(kosul(calikusu));
Console.WriteLine(kosul(memed));

// 2) Aynı değişkene başka bir metot
kosul = KisaMi;
Console.WriteLine(kosul(memed));

// 3) Metodun yerine bir lambda ifadesi: adı olmayan, yerinde yazılan metot
kosul = k => k.Yil > 1950;
Console.WriteLine(kosul(memed));

// 4) Hazır delegeler: kendi tipimizi yazmaya çoğunlukla gerek yok
Func<Kitap, bool> eskiMi = EskiMi;
Func<int, int, int> topla = (a, b) => a + b;
Action<string> yaz = Console.WriteLine;
yaz($"{eskiMi(memed)} {topla(1920, 3)}");

bool EskiMi(Kitap k) => k.Yil < 1930;
bool KisaMi(Kitap k) => k.Baslik.Length <= 10;

delegate bool Kosul(Kitap k);
record Kitap(string Baslik, string Yazar, int Yil);
