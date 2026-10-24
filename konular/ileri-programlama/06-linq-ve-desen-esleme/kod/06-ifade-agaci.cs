// Bugünün taşıyıcısı: ifade ağacı. (2 + 3) * 4 gibi bir ifadeyi kayıtlarla kurmak.
//
// Her düğüm ya bir sayıdır ya da iki alt ifadeyi birleştiren bir işlem.
// Hesapla, Yazdir ve Sadelestir'in her biri TEK bir switch ifadesi: her kol
// bir düğüm türünü tanır ve alt ifadeler için kendini yeniden çağırır.
// Şema: assets/01-ifade-agaci.svg
// Çalıştırmak için:  dotnet run 06-ifade-agaci.cs

// (2 + 3) * 4
Ifade ifade = new Carp(new Topla(new Sayi(2), new Sayi(3)), new Sayi(4));

Console.WriteLine(Yazdir(ifade));
Console.WriteLine(Hesapla(ifade));

// (x * 1) + 0 gibi gereksiz parçaları atmak
Ifade gereksiz = new Topla(new Carp(new Sayi(7), new Sayi(1)), new Sayi(0));
Console.WriteLine($"{Yazdir(gereksiz)}  →  {Yazdir(Sadelestir(gereksiz))}");

// Ağacın düğümlerini yield ile gezip LINQ ile saymak
Console.WriteLine($"Sayı düğümleri: {Dugumler(ifade).OfType<Sayi>().Count()}");
Console.WriteLine($"Sayıların toplamı: {Dugumler(ifade).OfType<Sayi>().Sum(s => s.Deger)}");

int Hesapla(Ifade i) => i switch
{
    Sayi s => s.Deger,
    Topla t => Hesapla(t.Sol) + Hesapla(t.Sag),
    Carp c => Hesapla(c.Sol) * Hesapla(c.Sag),
    _ => throw new ArgumentException($"Bilinmeyen ifade: {i}"),
};

string Yazdir(Ifade i) => i switch
{
    Sayi s => s.Deger.ToString(),
    Topla t => $"({Yazdir(t.Sol)} + {Yazdir(t.Sag)})",
    Carp c => $"{Yazdir(c.Sol)} * {Yazdir(c.Sag)}",
    _ => "?",
};

// Özellik deseni ağacın şekline bakar: "sağ taraf 0 olan bir toplama" gibi
Ifade Sadelestir(Ifade i) => i switch
{
    Topla(var sol, Sayi(0)) => Sadelestir(sol),
    Carp(var sol, Sayi(1)) => Sadelestir(sol),
    Topla t => new Topla(Sadelestir(t.Sol), Sadelestir(t.Sag)),
    Carp c => new Carp(Sadelestir(c.Sol), Sadelestir(c.Sag)),
    _ => i,
};

// Önce düğümün kendisi, sonra (varsa) sol ve sağ alt ağaçlar
IEnumerable<Ifade> Dugumler(Ifade i)
{
    yield return i;
    if (i is IkiliIslem(var sol, var sag))
    {
        foreach (var d in Dugumler(sol)) yield return d;
        foreach (var d in Dugumler(sag)) yield return d;
    }
}

abstract record Ifade;
record Sayi(int Deger) : Ifade;
abstract record IkiliIslem(Ifade Sol, Ifade Sag) : Ifade;
record Topla(Ifade Sol, Ifade Sag) : IkiliIslem(Sol, Sag);
record Carp(Ifade Sol, Ifade Sag) : IkiliIslem(Sol, Sag);
