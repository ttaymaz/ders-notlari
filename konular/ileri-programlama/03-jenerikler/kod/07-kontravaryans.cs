// Kontravaryans: Yayinları karşılaştırabilen, Kitapları da karşılaştırabilir.
//
// IComparer<in T> T'yi yalnızca ALIR. Her Kitap bir Yayin olduğu için,
// Yayin bekleyen bir karşılaştırıcıya Kitap vermek güvenlidir. Bu yüzden
// IComparer<Yayin>, IComparer<Kitap> yerine kullanılabilir: ok ters yönde.
// Şema: assets/01-varyans-yonu.svg
// Çalıştırmak için:  dotnet run 07-kontravaryans.cs

using System.Globalization;

// Sıralama makinenin diline bağlı olmasın: Türkçe alfabe kuralları.
CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

IComparer<Yayin> baslikSirasi = new BaslikKarsilastirici();

List<Kitap> kitaplar =
[
    new Kitap("Sinekli Bakkal", "Halide Edib Adıvar"),
    new Kitap("İnce Memed", "Yaşar Kemal"),
    new Kitap("Çalıkuşu", "Reşat Nuri Güntekin"),
    new Kitap("Ölmez Otu", "Yaşar Kemal"),
    new Kitap("Orta Direk", "Yaşar Kemal"),
];

kitaplar.Sort(baslikSirasi);     // Sort, IComparer<Kitap> bekliyor
foreach (Kitap k in kitaplar)
    Console.WriteLine(k.Baslik);

class BaslikKarsilastirici : IComparer<Yayin>
{
    public int Compare(Yayin? x, Yayin? y) =>
        string.Compare(x?.Baslik, y?.Baslik, StringComparison.CurrentCulture);
}

record Yayin(string Baslik);
record Kitap(string Baslik, string Yazar) : Yayin(Baslik);
