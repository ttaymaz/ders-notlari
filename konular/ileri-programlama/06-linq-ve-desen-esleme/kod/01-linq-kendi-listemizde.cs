// LINQ kendi listemizde: hiçbir şey eklemeden çalışıyor. Neden?
//
// BagliListe<T> geçen hafta IEnumerable<T> uygulamaya başladı. LINQ'in
// metotları IEnumerable<T> üzerine yazılmış genişletme metotlarıdır; bu
// arayüzü uygulayan her tip onları kendiliğinden kazanır.
// Çalıştırmak için:  dotnet run 01-linq-kendi-listemizde.cs

using System.Collections;

var raf = new BagliListe<Kitap>();
raf.Ekle(new Kitap("Çalıkuşu", "Reşat Nuri Güntekin", 1922));
raf.Ekle(new Kitap("Sinekli Bakkal", "Halide Edib Adıvar", 1936));
raf.Ekle(new Kitap("Kuyucaklı Yusuf", "Sabahattin Ali", 1937));
raf.Ekle(new Kitap("İnce Memed", "Yaşar Kemal", 1955));

// 4. haftanın laboratuvarında yazdığınız üç metodun LINQ'teki adları:
int say = raf.Count(k => k.Yil > 1930);                   // Say
var filtre = raf.Where(k => k.Yil > 1930);                // Filtrele
var donustur = raf.Select(k => k.Baslik.Length);          // Donustur

Console.WriteLine(say);
Console.WriteLine(string.Join(", ", filtre.Select(k => k.Baslik)));
Console.WriteLine(string.Join(" ", donustur));

// Zincirleme: her metot bir IEnumerable<T> döndürür, bir sonrakine verilir
var sonuc = raf
    .Where(k => k.Yil < 1950)
    .OrderBy(k => k.Baslik.Length)
    .Select(k => $"{k.Baslik} ({k.Yil})");

foreach (var s in sonuc)
    Console.WriteLine(s);

class Dugum<T>(T deger)
{
    public T Deger { get; } = deger;
    public Dugum<T>? Sonraki { get; set; }
}

class BagliListe<T> : IEnumerable<T>
{
    private Dugum<T>? _bas;
    private Dugum<T>? _son;

    public void Ekle(T deger)
    {
        var yeni = new Dugum<T>(deger);
        if (_son is null) _bas = yeni; else _son.Sonraki = yeni;
        _son = yeni;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            yield return d.Deger;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

record Kitap(string Baslik, string Yazar, int Yil);
