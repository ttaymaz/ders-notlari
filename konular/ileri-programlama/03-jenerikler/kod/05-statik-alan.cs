// Her kapalı jenerik tip kendi statik alanlarına sahiptir.
//
// Sayac<int> ile Sayac<string> aynı kaynak koddan gelir ama çalışma
// zamanında iki ayrı tiptir. Statik alan tipe aittir; iki tip, iki alan.
// Çalıştırmak için:  dotnet run 05-statik-alan.cs

Sayac<int>.Artir();
Sayac<int>.Artir();
Sayac<string>.Artir();
Sayac<Kitap>.Artir();
Sayac<Kitap>.Artir();
Sayac<Kitap>.Artir();

Console.WriteLine($"int    : {Sayac<int>.Deger}");
Console.WriteLine($"string : {Sayac<string>.Deger}");
Console.WriteLine($"Kitap  : {Sayac<Kitap>.Deger}");
Console.WriteLine($"double : {Sayac<double>.Deger}");

static class Sayac<T>
{
    public static int Deger { get; private set; }
    public static void Artir() => Deger++;
}

record Kitap(string Baslik);
