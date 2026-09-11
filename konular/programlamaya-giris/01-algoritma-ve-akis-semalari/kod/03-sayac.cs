// Döngüsel akış örneği — 03-dongusel-akis.svg şemasının kod karşılığı.
//
// Çalıştırmak için:  dotnet run 03-sayac.cs
//
// Not: while döngüsünü 5. haftada işleyeceğiz. Şemadaki geri dönüş okunun
// kodda nasıl göründüğüne dikkat edin.

int sayac = 1;

while (sayac <= 5)
{
    Console.WriteLine(sayac);
    sayac = sayac + 1;
}
