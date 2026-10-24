// LABORATUVAR — Yığın, kuyruk ve yineleyiciler.
//
// Aşağıda yedi metot "TODO" ile işaretli ve şu an NotImplementedException
// fırlatıyor. Sırayla doldurun ve her seferinde çalıştırın; dosyanın üst
// kısmındaki denetim kaç görevin tamam olduğunu söyler. Denetim kodunu
// değiştirmeyin.
//
// Çalıştırmak için:  dotnet run yigin-kuyruk-lab.cs
// .NET 9:            kod/README.md içindeki proje yolu

using System.Collections;

// ---------------------------------------------------------------- DENETİM
int tamam = 0, toplam = 0;

void Denetle(string ad, Func<bool> sinama)
{
    toplam++;
    try
    {
        bool sonuc = sinama();
        if (sonuc) tamam++;
        Console.WriteLine($"[{(sonuc ? "TAMAM" : "YANLIŞ")}] {ad}");
    }
    catch (NotImplementedException)
    {
        Console.WriteLine($"[YAPILMADI] {ad}");
    }
    catch (Exception hata)
    {
        Console.WriteLine($"[HATA] {ad}: {hata.GetType().Name}: {hata.Message}");
    }
}

bool Firlatti<THata>(Action islem) where THata : Exception
{
    try { islem(); return false; }
    catch (THata) { return true; }
}

// Sonsuz kaynak: 10.000'den fazla eleman istenirse durur.
IEnumerable<int> Sonsuz()
{
    for (int i = 1; ; i++)
    {
        if (i > 10_000)
            throw new InvalidOperationException("Sonsuz kaynaktan çok fazla eleman istendi; ertelenmiş değil mi?");
        yield return i;
    }
}

Yigin<string> YiginKur()
{
    var y = new Yigin<string>();
    y.Ekle("A"); y.Ekle("B"); y.Ekle("C");
    return y;
}

Kuyruk<string> KuyrukKur()
{
    var k = new Kuyruk<string>();
    k.Ekle("A"); k.Ekle("B"); k.Ekle("C");
    return k;
}

Denetle("1. Yigin.Cikar: son ekleneni verir",
    () => { var y = YiginKur(); return y.Cikar() == "C" && y.Cikar() == "B"; });
Denetle("1. Yigin.Cikar: boşsa InvalidOperationException",
    () => Firlatti<InvalidOperationException>(() => new Yigin<string>().Cikar()));
Denetle("2. Yigin.Bak: tepeyi verir ama çıkarmaz",
    () => { var y = YiginKur(); return y.Bak() == "C" && y.Bak() == "C" && y.Cikar() == "C"; });
Denetle("3. Yigin foreach: C B A",
    () => string.Join(" ", YiginKur()) == "C B A");
Denetle("4. Kuyruk.Cikar: ilk ekleneni verir",
    () => { var k = KuyrukKur(); return k.Cikar() == "A" && k.Cikar() == "B"; });
Denetle("4. Kuyruk boşalınca yeniden doldurulabilir",
    () => { var k = new Kuyruk<string>(); k.Ekle("X"); k.Cikar(); k.Ekle("Y"); k.Ekle("Z");
            return string.Join(" ", k) == "Y Z"; });
Denetle("5. Kuyruk foreach: A B C",
    () => string.Join(" ", KuyrukKur()) == "A B C");
Denetle("6. IlkN: ilk üç eleman",
    () => string.Join(" ", IlkN(KuyrukKur(), 3)) == "A B C");
Denetle("6. IlkN: kaynak kısaysa olanı verir",
    () => string.Join(" ", IlkN(KuyrukKur(), 10)) == "A B C");
Denetle("6. IlkN: sonsuz kaynakla çalışır (ertelenmiş olmalı)",
    () => string.Join(" ", IlkN(Sonsuz(), 5)) == "1 2 3 4 5");
Denetle("7. Tersten: kendi yığınınızla ters çevirir",
    () => string.Join(" ", Tersten(KuyrukKur())) == "C B A");

Console.WriteLine($"\n{tamam}/{toplam} denetim tamam");

// TODO 6: kaynağın ilk n elemanını yield return ile verin.
//         Sonsuz bir kaynakla da çalışmalı: önce hepsini toplamayın.
IEnumerable<T> IlkN<T>(IEnumerable<T> kaynak, int n)
{
    throw new NotImplementedException();
}

// TODO 7: kaynağı TERS sırada verin. Kendi Yigin<T> sınıfınızı kullanın:
//         önce hepsini yığına ekleyin, sonra yığını gezin.
IEnumerable<T> Tersten<T>(IEnumerable<T> kaynak)
{
    throw new NotImplementedException();
}

// ---------------------------------------------------------------- YAPILAR
class Dugum<T>(T deger)
{
    public T Deger { get; } = deger;
    public Dugum<T>? Sonraki { get; set; }
}

class Yigin<T> : IEnumerable<T>
{
    private Dugum<T>? _tepe;

    public void Ekle(T deger) => _tepe = new Dugum<T>(deger) { Sonraki = _tepe };

    // TODO 1: Tepedeki elemanı çıkarıp döndürün.
    //         Yığın boşsa: throw new InvalidOperationException("Yığın boş.");
    public T Cikar()
    {
        throw new NotImplementedException();
    }

    // TODO 2: Tepedeki elemanı döndürün ama ÇIKARMAYIN. Boşsa aynı istisna.
    public T Bak()
    {
        throw new NotImplementedException();
    }

    // TODO 3: Tepeden aşağı doğru yield return ile gezin.
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

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

    // TODO 4: Baştaki elemanı çıkarıp döndürün. Boşsa InvalidOperationException.
    //         Dikkat: son eleman da çıkınca kuyruk tamamen boşalır.
    public T Cikar()
    {
        throw new NotImplementedException();
    }

    // TODO 5: Baştan sona yield return ile gezin.
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
