// Olay (event): "bir şey olduğunda bana haber ver."
//
// Liste, eklemeyi kimin dinlediğini bilmez; yalnızca olayı tetikler.
// Dinleyenler += ile abone olur, -= ile ayrılır. Gözlemci (Observer)
// kalıbının C#'taki dil düzeyindeki karşılığı budur.
// Çalıştırmak için:  dotnet run 05-olaylar.cs

var raf = new BagliListe<Kitap>();
int eklenen = 0;

Action<Kitap> gunluk = k => Console.WriteLine($"[günlük] {k.Baslik} eklendi");
raf.Eklendi += gunluk;
raf.Eklendi += k => eklenen++;

raf.Ekle(new Kitap("Çalıkuşu", "Reşat Nuri Güntekin", 1922));
raf.Ekle(new Kitap("Sinekli Bakkal", "Halide Edib Adıvar", 1936));

raf.Eklendi -= gunluk;
raf.Ekle(new Kitap("İnce Memed", "Yaşar Kemal", 1955));

Console.WriteLine($"Olayın saydığı: {eklenen}, listedeki: {raf.Sayi}");

class Dugum<T>(T deger)
{
    public T Deger { get; } = deger;
    public Dugum<T>? Sonraki { get; set; }
}

class BagliListe<T>
{
    private Dugum<T>? _bas;
    private Dugum<T>? _son;

    public int Sayi { get; private set; }

    // Abone yoksa null; bu yüzden tetiklerken ?.Invoke
    public event Action<T>? Eklendi;

    public void Ekle(T deger)
    {
        var yeni = new Dugum<T>(deger);
        if (_son is null) _bas = yeni; else _son.Sonraki = yeni;
        _son = yeni;
        Sayi++;
        Eklendi?.Invoke(deger);
    }
}

record Kitap(string Baslik, string Yazar, int Yil);
