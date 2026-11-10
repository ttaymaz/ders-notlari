using System.Collections;

namespace Yapilar;

// 5. konunun kuyruğu: sona eklenir, baştan çıkarılır.
// Son elemanı çıkarırken _son da boşaltılmalıdır; unutulursa kuyruk
// "boşalıp yeniden dolma" durumunda bozulur. KuyrukTestleri bunu sınar.
public class Kuyruk<T> : IEnumerable<T>
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

    public T Cikar()
    {
        if (_bas is null) throw new InvalidOperationException("Kuyruk boş.");
        T deger = _bas.Deger;
        _bas = _bas.Sonraki;
        if (_bas is null) _son = null;
        Sayi--;
        return deger;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            yield return d.Deger;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
