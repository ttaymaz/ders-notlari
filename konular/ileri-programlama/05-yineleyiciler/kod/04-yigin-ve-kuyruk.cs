// Bugünün taşıyıcısı: yığın ve kuyruk. İkisi de geçen haftanın düğümleri üzerine.
//
// Aynı üç eleman, aynı sırayla ekleniyor. foreach ile gezince yığın
// "son giren ilk çıkar" (LIFO), kuyruk "ilk giren ilk çıkar" (FIFO) verir.
// .NET'teki hazırları Stack<T> ve Queue<T>; onlar da aynı sırayla gezilir.
// Şema: assets/01-yigin-ve-kuyruk.svg
// Çalıştırmak için:  dotnet run 04-yigin-ve-kuyruk.cs

using System.Collections;

var yigin = new Yigin<string>();
var kuyruk = new Kuyruk<string>();
foreach (var ad in new[] { "Çalıkuşu", "Sinekli Bakkal", "İnce Memed" })
{
    yigin.Ekle(ad);
    kuyruk.Ekle(ad);
}

Console.WriteLine("Yığın : " + string.Join(", ", yigin));
Console.WriteLine("Kuyruk: " + string.Join(", ", kuyruk));

Console.WriteLine($"Yığından çıkan : {yigin.Cikar()}");
Console.WriteLine($"Kuyruktan çıkan: {kuyruk.Cikar()}");

var stack = new Stack<string>(new[] { "Çalıkuşu", "Sinekli Bakkal", "İnce Memed" });
Console.WriteLine("Stack<T>: " + string.Join(", ", stack));

class Dugum<T>(T deger)
{
    public T Deger { get; } = deger;
    public Dugum<T>? Sonraki { get; set; }
}

// Yığın: ekleme ve çıkarma aynı uçtan (tepe)
class Yigin<T> : IEnumerable<T>
{
    private Dugum<T>? _tepe;

    public void Ekle(T deger) => _tepe = new Dugum<T>(deger) { Sonraki = _tepe };

    public T Cikar()
    {
        if (_tepe is null) throw new InvalidOperationException("Yığın boş.");
        T deger = _tepe.Deger;
        _tepe = _tepe.Sonraki;
        return deger;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var d = _tepe; d is not null; d = d.Sonraki)
            yield return d.Deger;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Kuyruk: sona eklenir, baştan çıkarılır
class Kuyruk<T> : IEnumerable<T>
{
    private Dugum<T>? _bas;
    private Dugum<T>? _son;

    public void Ekle(T deger)
    {
        var yeni = new Dugum<T>(deger);
        if (_son is null) _bas = yeni; else _son.Sonraki = yeni;
        _son = yeni;
    }

    public T Cikar()
    {
        if (_bas is null) throw new InvalidOperationException("Kuyruk boş.");
        T deger = _bas.Deger;
        _bas = _bas.Sonraki;
        if (_bas is null) _son = null;
        return deger;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            yield return d.Deger;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
