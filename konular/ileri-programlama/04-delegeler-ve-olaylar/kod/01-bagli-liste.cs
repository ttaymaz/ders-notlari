// Bugünün taşıyıcısı: kendi bağlı listemiz.
//
// Her eleman bir düğümde durur; düğüm kendi değerini ve bir sonraki düğümün
// adresini tutar. Liste yalnızca ilk ve son düğümü bilir. .NET'te bunun
// hazırı LinkedList<T>'dir; biz içini görmek için yazıyoruz.
// Şema: assets/01-bagli-liste.svg
// Çalıştırmak için:  dotnet run 01-bagli-liste.cs

var raf = new BagliListe<Kitap>();
raf.Ekle(new Kitap("Çalıkuşu", "Reşat Nuri Güntekin", 1922));
raf.Ekle(new Kitap("Sinekli Bakkal", "Halide Edib Adıvar", 1936));
raf.Ekle(new Kitap("Kuyucaklı Yusuf", "Sabahattin Ali", 1937));
raf.Ekle(new Kitap("İnce Memed", "Yaşar Kemal", 1955));

Console.WriteLine($"Eleman sayısı: {raf.Sayi}");
raf.Yazdir();

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

    // Sona ekler. _son olmasaydı her eklemede baştan sona yürümek gerekirdi.
    public void Ekle(T deger)
    {
        var yeni = new Dugum<T>(deger);
        if (_son is null)
            _bas = yeni;
        else
            _son.Sonraki = yeni;
        _son = yeni;
        Sayi++;
    }

    public void Yazdir()
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            Console.WriteLine(d.Deger);
    }
}

record Kitap(string Baslik, string Yazar, int Yil);
