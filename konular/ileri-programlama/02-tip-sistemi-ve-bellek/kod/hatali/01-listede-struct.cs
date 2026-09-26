// KASITLI OLARAK DERLENMEZ.
//
// Derleyici bu satırı reddeder, çünkü değişiklik geri dönen kopyaya
// yapılacak ve hemen kaybolacaktı. Hatanın kodu ne? Mesajı ne diyor?
// Derlemeyi deneyin:  dotnet build 01-listede-struct.cs
//
// Görev: 03-liste-ve-struct.cs dosyasına bakmadan iki farklı yolla düzeltin.

var liste = new List<NoktaS> { new NoktaS(1) };
liste[0].X = 5;
Console.WriteLine(liste[0].X);

struct NoktaS(int x) { public int X = x; }
