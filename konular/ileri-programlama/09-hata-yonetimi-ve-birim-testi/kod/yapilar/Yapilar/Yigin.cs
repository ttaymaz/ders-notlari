using System.Collections;

namespace Yapilar;

// 5. konunun yığını, bu kez bir sınıf kitaplığında ve hata sözleşmesiyle:
//   Cikar / Tepe   boş yığında InvalidOperationException fırlatır
//   TryCikar       boş yığında false döner, istisna fırlatmaz
public class Yigin<T> : IEnumerable<T>
{
    private Dugum<T>? _tepe;

    public int Sayi { get; private set; }

    public void Ekle(T deger)
    {
        _tepe = new Dugum<T>(deger) { Sonraki = _tepe };
        Sayi++;
    }

    public T Tepe
        => _tepe is null ? throw new InvalidOperationException("Yığın boş.") : _tepe.Deger;

    public T Cikar()
    {
        if (_tepe is null) throw new InvalidOperationException("Yığın boş.");
        T deger = _tepe.Deger;
        _tepe = _tepe.Sonraki;
        Sayi--;
        return deger;
    }

    public bool TryCikar(out T deger)
    {
        if (_tepe is null)
        {
            deger = default!;
            return false;
        }
        deger = Cikar();
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var d = _tepe; d is not null; d = d.Sonraki)
            yield return d.Deger;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
