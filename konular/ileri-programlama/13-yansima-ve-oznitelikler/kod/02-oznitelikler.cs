// Öznitelikler: koda iliştirilen bilgi.
//
// [Fact], [Theory], [EnumeratorCancellation] birer özniteliktir (attribute).
// Kendileri hiçbir şey yapmazlar; derlenmiş koda bir etiket olarak
// yapışırlar. Etiketi okuyan bir şey (derleyici, xUnit, kendi kodumuz)
// olursa anlam kazanırlar.
//
// Bu dosyada kendi özniteliğimizi yazıyoruz: [Komut]. Bir konsol menüsünün
// komutları, sınıfın metotlarına iliştirilmiş [Komut] etiketlerinden
// kuruluyor. Yeni bir komut eklemek için menü koduna dokunmak gerekmiyor:
// metodu yazıp etiketlemek yetiyor.
// Çalıştırmak için:  dotnet run 02-oznitelikler.cs

using System.Reflection;

var menu = new KutuphaneMenusu();
var komutlar = typeof(KutuphaneMenusu).GetMethods()
    .Select(m => (Metot: m, Komut: m.GetCustomAttribute<KomutAttribute>()))
    .Where(x => x.Komut is not null)
    .OrderBy(x => x.Komut!.Ad, StringComparer.Ordinal)
    .ToList();

Console.WriteLine("=== 1 === (etiketlerden kurulan menü)");
foreach (var (_, komut) in komutlar)
    Console.WriteLine($"{komut!.Ad,-10} {komut.Aciklama}");

Console.WriteLine("\n=== 2 === (adıyla çağırmak)");
foreach (string girdi in new[] { "listele", "say", "yardim" })
{
    var bulunan = komutlar.FirstOrDefault(x => x.Komut!.Ad == girdi);
    if (bulunan.Metot is null)
        Console.WriteLine($"{girdi}: böyle bir komut yok");
    else
        Console.WriteLine($"{girdi}: {bulunan.Metot.Invoke(menu, null)}");
}

Console.WriteLine("\n=== 3 === (öznitelik de bir nesnedir)");
MethodInfo kaldir = typeof(KutuphaneMenusu).GetMethod(nameof(KutuphaneMenusu.Kaldir))!;
foreach (Attribute a in kaldir.GetCustomAttributes().OrderBy(a => a.GetType().Name, StringComparer.Ordinal))
    Console.WriteLine($"{a.GetType().Name}");
ObsoleteAttribute eski = kaldir.GetCustomAttribute<ObsoleteAttribute>()!;
Console.WriteLine($"Obsolete mesajı: {eski.Message}");

// Bir özniteliğin adı "Attribute" ile biter; kullanırken bu ek yazılmaz:
// [Komut(...)] yazmak KomutAttribute sınıfını kullanır.
// AttributeUsage: bu etiket yalnızca metotlara yapıştırılabilir.
[AttributeUsage(AttributeTargets.Method)]
class KomutAttribute(string ad) : Attribute
{
    public string Ad { get; } = ad;
    public string Aciklama { get; init; } = "";
}

class KutuphaneMenusu
{
    private readonly List<string> _kitaplar = ["Huzur", "Aylak Adam", "Beş Şehir"];

    [Komut("listele", Aciklama = "Bütün kitapları yazar")]
    public string Listele() => string.Join(", ", _kitaplar);

    [Komut("say", Aciklama = "Kitap sayısını yazar")]
    public string Say() => $"{_kitaplar.Count} kitap";

    [Komut("kaldir", Aciklama = "Son kitabı siler")]
    [Obsolete("Yerine 'sil' komutunu kullanın.")]
    public string Kaldir()
    {
        _kitaplar.RemoveAt(_kitaplar.Count - 1);
        return "silindi";
    }

    // Etiketsiz metot menüde görünmez
    public string GizliBakim() => "bakım";
}
