// Atama: struct değeri kopyalar, class aynı nesneye ikinci bir yol açar.
//
// Tanıtım dersindeki ilk ısınma sorusunun genişletilmiş hali. İki tipin
// tek farkı "struct" ve "class" kelimesi.
// Şema: assets/01-atama-bellek.svg
// Çalıştırmak için:  dotnet run 01-atama-farki.cs
// Beklenen çıktı:    kod/README.md, "Çıktılar" bölümü

var a = new NoktaS(1);
var b = a;
b.X = 5;
Console.WriteLine($"struct: a.X={a.X}  b.X={b.X}");

var c = new NoktaC(1);
var d = c;
d.X = 5;
Console.WriteLine($"class : c.X={c.X}  d.X={d.X}");

// Kutulama: değer tipini object değişkenine koymak bir KOPYA üretir.
object kutu = a;
a.X = 7;
Console.WriteLine($"kutu  : {((NoktaS)kutu).X}  a.X={a.X}");

struct NoktaS(int x) { public int X = x; }
class NoktaC(int x) { public int X = x; }
