// KASITLI OLARAK DERLENMEZ.
//
// T için hiçbir kısıt yok. Derleyici T'nin ne olacağını bilmediği için
// > işlecine izin vermez: T bir int olabilir, ama bir Kitap da olabilir.
// Derlemeyi deneyin:  dotnet build 01-kisit-yok.cs
//
// Görev: bir "where" kısıtı ekleyin ve > yerine o kısıtın verdiği metodu
// kullanın. 03-jenerik-metot.cs dosyasına bakmadan deneyin.

Console.WriteLine(EnBuyuk(3, 7));

T EnBuyuk<T>(T a, T b) => a > b ? a : b;
