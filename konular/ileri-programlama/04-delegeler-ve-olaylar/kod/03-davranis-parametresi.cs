// Listeye davranış vermek: neye göre arayacağına çağıran karar versin.
//
// Bul, "hangi kitap?" sorusunun cevabını bilmez; bir koşul (Func<T, bool>)
// alır ve onu her düğümde sorar. EnBuyuk da karşılaştırma kuralını dışarıdan
// alır (Comparison<T>): Strateji kalıbının tek satırlık hali.
// Çalıştırmak için:  dotnet run 03-davranis-parametresi.cs

using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

var raf = new BagliListe<Kitap>();
raf.Ekle(new Kitap("Çalıkuşu", "Reşat Nuri Güntekin", 1922));
raf.Ekle(new Kitap("Sinekli Bakkal", "Halide Edib Adıvar", 1936));
raf.Ekle(new Kitap("Kuyucaklı Yusuf", "Sabahattin Ali", 1937));
raf.Ekle(new Kitap("İnce Memed", "Yaşar Kemal", 1955));

Console.WriteLine(raf.Bul(k => k.Yil > 1930)?.Baslik);
Console.WriteLine(raf.Bul(k => k.Yazar.Contains("Ali"))?.Baslik);
Console.WriteLine(raf.Bul(k => k.Yil > 2000)?.Baslik ?? "(yok)");

raf.HerBiri(k => Console.Write($"{k.Yil} "));
Console.WriteLine();

// Aynı metot, üç farklı karşılaştırma kuralı
Console.WriteLine(raf.EnBuyuk((a, b) => a.Yil.CompareTo(b.Yil))?.Baslik);
Console.WriteLine(raf.EnBuyuk((a, b) => a.Baslik.Length.CompareTo(b.Baslik.Length))?.Baslik);
Console.WriteLine(raf.EnBuyuk((a, b) => string.Compare(a.Baslik, b.Baslik, StringComparison.CurrentCulture))?.Baslik);

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

    public void Ekle(T deger)
    {
        var yeni = new Dugum<T>(deger);
        if (_son is null) _bas = yeni; else _son.Sonraki = yeni;
        _son = yeni;
        Sayi++;
    }

    // Koşulu sağlayan ilk eleman; yoksa T'nin varsayılan değeri
    public T? Bul(Func<T, bool> kosul)
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            if (kosul(d.Deger))
                return d.Deger;
        return default;
    }

    public void HerBiri(Action<T> islem)
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            islem(d.Deger);
    }

    // Karşılaştırma kuralı dışarıdan: pozitifse a büyük, negatifse b büyük
    public T? EnBuyuk(Comparison<T> karsilastir)
    {
        if (_bas is null) return default;
        T enBuyuk = _bas.Deger;
        for (var d = _bas.Sonraki; d is not null; d = d.Sonraki)
            if (karsilastir(d.Deger, enBuyuk) > 0)
                enBuyuk = d.Deger;
        return enBuyuk;
    }
}

record Kitap(string Baslik, string Yazar, int Yil);
