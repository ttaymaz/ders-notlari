// LINQ ertelenmiş çalışır: geçen haftanın yield return'ü burada.
//
// Where, Select, OrderBy bir "tarif" döndürür; tarif Count, ToList, First
// veya foreach ile işletilir. Lambda'nın kaç kez çağrıldığını sayıyoruz.
// Çalıştırmak için:  dotnet run 04-ertelenmis-linq.cs

// 1) Tanıtım dersindeki üçüncü ısınma sorusu
var sayilar = new List<int> { 1, 2, 3 };
var buyukler = sayilar.Where(s => s > 1);
sayilar.Add(4);
Console.WriteLine(buyukler.Count());

// 2) Aynı sorgu iki kez işletilirse lambda iki kez çalışır
int cagri = 0;
var yillar = new List<int> { 1922, 1936, 1937, 1955 };
var sorgu = yillar.Where(y => { cagri++; return y > 1930; });

Console.WriteLine($"{sorgu.Count()} {sorgu.Count()}  çağrı: {cagri}");

// 3) ToList sorguyu bir kez işletir ve sonucu saklar
cagri = 0;
var liste = yillar.Where(y => { cagri++; return y > 1930; }).ToList();
Console.WriteLine($"{liste.Count} {liste.Count}  çağrı: {cagri}");

// 4) Hiç işletilmeyen sorgu hiç çalışmaz
cagri = 0;
var hic = yillar.Where(y => { cagri++; return true; });
Console.WriteLine($"işletilmeyen sorgu, çağrı: {cagri}");
