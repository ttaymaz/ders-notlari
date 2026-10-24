// foreach aslında ne yapar?
//
// Derleyici foreach döngüsünü üç çağrıya çevirir: GetEnumerator ile bir
// "gezgin" (enumerator) alır, MoveNext ile bir adım ilerletir, Current ile
// o anki elemanı okur. İkinci döngü, birincinin derleyicinin yazdığı halidir.
// Çalıştırmak için:  dotnet run 01-foreach-nasil-calisir.cs

var kitaplar = new List<string> { "Çalıkuşu", "Sinekli Bakkal", "İnce Memed" };

foreach (var k in kitaplar)
    Console.WriteLine(k);

Console.WriteLine("---");

using (IEnumerator<string> gezgin = kitaplar.GetEnumerator())
{
    while (gezgin.MoveNext())
    {
        var k = gezgin.Current;
        Console.WriteLine(k);
    }
}
