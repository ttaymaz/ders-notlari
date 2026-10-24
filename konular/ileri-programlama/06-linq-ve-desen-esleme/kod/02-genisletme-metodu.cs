// Genişletme metodu: başkasının tipine "metot ekliyormuş gibi" yazmak.
//
// Nerede ve Sec, IEnumerable<T> üzerine yazılmış iki statik metot. İlk
// parametrenin başındaki "this" sayesinde onları raf.Nerede(...) diye
// çağırabiliyoruz. LINQ'in Where ve Select metotları da tam olarak böyle
// yazılmıştır: genişletme metodu + yield return.
// Çalıştırmak için:  dotnet run 02-genisletme-metodu.cs

var kitaplar = new List<Kitap>
{
    new("Çalıkuşu", "Reşat Nuri Güntekin", 1922),
    new("Sinekli Bakkal", "Halide Edib Adıvar", 1936),
    new("Kuyucaklı Yusuf", "Sabahattin Ali", 1937),
    new("İnce Memed", "Yaşar Kemal", 1955),
};

// Genişletme metodu gibi çağrı...
var yeniler = kitaplar.Nerede(k => k.Yil > 1930).Sec(k => k.Baslik);
Console.WriteLine(string.Join(", ", yeniler));

// ...derleyicinin aslında yazdığı sıradan statik çağrı
var ayni = KendiLinqim.Sec(KendiLinqim.Nerede(kitaplar, k => k.Yil > 1930), k => k.Baslik);
Console.WriteLine(string.Join(", ", ayni));

// Genişletme metotları statik, iç içe olmayan, jenerik olmayan bir sınıfta durur
static class KendiLinqim
{
    public static IEnumerable<T> Nerede<T>(this IEnumerable<T> kaynak, Func<T, bool> kosul)
    {
        foreach (var x in kaynak)
            if (kosul(x)) yield return x;
    }

    public static IEnumerable<TSonuc> Sec<T, TSonuc>(this IEnumerable<T> kaynak, Func<T, TSonuc> donusum)
    {
        foreach (var x in kaynak)
            yield return donusum(x);
    }
}

record Kitap(string Baslik, string Yazar, int Yil);
