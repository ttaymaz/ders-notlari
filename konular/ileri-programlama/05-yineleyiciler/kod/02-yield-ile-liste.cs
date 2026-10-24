// Kendi listemizi foreach ile gezilebilir yapmak: IEnumerable<T> + yield return.
//
// Geçen haftanın BagliListe<T>'si foreach ile derlenmiyordu (CS1579).
// GetEnumerator metodunu yazınca derlenir. Metodun gövdesinde yield return
// var: derleyici MoveNext / Current'ı içeren gezgin sınıfını bizim yerimize yazar.
// Çalıştırmak için:  dotnet run 02-yield-ile-liste.cs

using System.Collections;

var raf = new BagliListe<Kitap>();
raf.Ekle(new Kitap("Çalıkuşu", "Reşat Nuri Güntekin", 1922));
raf.Ekle(new Kitap("Sinekli Bakkal", "Halide Edib Adıvar", 1936));
raf.Ekle(new Kitap("İnce Memed", "Yaşar Kemal", 1955));

foreach (var k in raf)
    Console.WriteLine($"{k.Baslik} ({k.Yil})");

class Dugum<T>(T deger)
{
    public T Deger { get; } = deger;
    public Dugum<T>? Sonraki { get; set; }
}

class BagliListe<T> : IEnumerable<T>
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

    // Her yield return bir eleman verir ve metodu o noktada DURAKLATIR.
    // Bir sonraki MoveNext çağrısında metot kaldığı yerden devam eder.
    public IEnumerator<T> GetEnumerator()
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            yield return d.Deger;
    }

    // IEnumerable<T>, eski (jenerik olmayan) IEnumerable'ı da ister.
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

record Kitap(string Baslik, string Yazar, int Yil);
