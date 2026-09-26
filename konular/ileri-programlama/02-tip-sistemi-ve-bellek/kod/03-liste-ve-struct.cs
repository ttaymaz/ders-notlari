// Dizide ve listede struct: aynı yazım, farklı sonuç.
//
// Dizinin elemanı doğrudan değiştirilebilir. List<T> ise elemanı bir
// metotla (indeksleyici) döndürür; struct için dönen şey bir KOPYADIR.
// Derlenmeyen hali: hatali/01-listede-struct.cs
// Çalıştırmak için:  dotnet run 03-liste-ve-struct.cs

NoktaS[] dizi = [new NoktaS(1)];
dizi[0].X = 5;
Console.WriteLine($"dizi[0].X  = {dizi[0].X}");

var liste = new List<NoktaS> { new NoktaS(1) };
var p = liste[0];
p.X = 5;
Console.WriteLine($"liste[0].X = {liste[0].X}");

liste[0] = p;
Console.WriteLine($"liste[0].X = {liste[0].X}");

struct NoktaS(int x) { public int X = x; }
