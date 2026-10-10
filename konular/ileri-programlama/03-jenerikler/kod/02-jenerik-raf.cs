// Kendi jenerik tipimizi yazmak: Raf<T>.
//
// T bir tip parametresidir: "rafa ne konacağını şimdi bilmiyorum, kullanan
// söyleyecek". Raf<Kitap> ve Raf<Dergi> derleyicinin gözünde iki ayrı tiptir.
// Çalıştırmak için:  dotnet run 02-jenerik-raf.cs

var kitaplar = new Raf<Kitap>(kapasite: 2);
kitaplar.Koy(new Kitap("Çalıkuşu", "Reşat Nuri Güntekin"));
kitaplar.Koy(new Kitap("Kuyucaklı Yusuf", "Sabahattin Ali"));
Console.WriteLine(kitaplar.Koy(new Kitap("Sinekli Bakkal", "Halide Edib Adıvar")));
Console.WriteLine(kitaplar);

var dergiler = new Raf<Dergi>(kapasite: 3);
dergiler.Koy(new Dergi("Kütüphane Bülteni", 12));
Console.WriteLine(dergiler);
Console.WriteLine(dergiler.Al(0));
Console.WriteLine(dergiler.Al(1) is null);

var sayilar = new Raf<int>(kapasite: 1);
Console.WriteLine(sayilar.Al(0));

class Raf<T>
{
    private readonly T[] _yerler;
    private int _dolu;

    public Raf(int kapasite) => _yerler = new T[kapasite];

    public bool Koy(T eleman)
    {
        if (_dolu == _yerler.Length)
            return false;
        _yerler[_dolu++] = eleman;
        return true;
    }

    // Boş yer için T'nin varsayılan değeri döner: referans tipte null, int'te 0.
    public T? Al(int sira) => sira < _dolu ? _yerler[sira] : default;

    public override string ToString() =>
        $"Raf<{typeof(T).Name}>: {_dolu}/{_yerler.Length} dolu";
}

record Kitap(string Baslik, string Yazar);
record Dergi(string Ad, int Sayi);
