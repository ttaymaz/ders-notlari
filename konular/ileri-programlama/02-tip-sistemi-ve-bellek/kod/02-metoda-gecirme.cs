// Metoda geçirmek de bir atamadır.
//
// Parametre, çağıran taraftaki değişkenin KOPYASIDIR. struct için kopya
// değerin kendisi, class için kopya "nesnenin adresi". ref ise kopya
// değil, değişkenin kendisini verir.
// Çalıştırmak için:  dotnet run 02-metoda-gecirme.cs

var ns = new NoktaS(1);
var nc = new NoktaC(1);

DegistirS(ns);
DegistirC(nc);
Console.WriteLine($"Degistir sonrası : ns.X={ns.X}  nc.X={nc.X}");

Yenile(nc);
Console.WriteLine($"Yenile sonrası   : nc.X={nc.X}");

RefDegistir(ref ns);
Console.WriteLine($"ref sonrası      : ns.X={ns.X}");

// Yerel fonksiyonlar aşırı yüklenemez; bu yüzden iki ayrı ad.
void DegistirS(NoktaS n) => n.X = 99;
void DegistirC(NoktaC n) => n.X = 99;
void Yenile(NoktaC n) => n = new NoktaC(-1);
void RefDegistir(ref NoktaS n) => n.X = 99;

struct NoktaS(int x) { public int X = x; }
class NoktaC(int x) { public int X = x; }
