// Tanıtım dersindeki üçüncü ısınma sorusunun cevabı — LINQ olmadan.
//
// İki filtre aynı işi yapıyor. Biri sonucu hemen bir listeye yazıyor,
// diğeri yield return ile ertelenmiş çalışıyor. Filtreler çağrıldıktan
// SONRA kaynağa bir eleman ekleniyor. Hangisi yeni elemanı görür?
// Çalıştırmak için:  dotnet run 06-ertelenmis-filtre.cs

var sayilar = new List<int> { 1, 2, 3 };

var hemen = FiltreleHemen(sayilar, s => s > 1);
var ertelenmis = FiltreleErtelenmis(sayilar, s => s > 1);

sayilar.Add(4);

Console.WriteLine($"Hemen      : {string.Join(" ", hemen)}");
Console.WriteLine($"Ertelenmiş : {string.Join(" ", ertelenmis)}");

List<int> FiltreleHemen(List<int> kaynak, Func<int, bool> kosul)
{
    var sonuc = new List<int>();
    foreach (var s in kaynak)
        if (kosul(s)) sonuc.Add(s);
    return sonuc;
}

IEnumerable<int> FiltreleErtelenmis(List<int> kaynak, Func<int, bool> kosul)
{
    foreach (var s in kaynak)
        if (kosul(s)) yield return s;
}
