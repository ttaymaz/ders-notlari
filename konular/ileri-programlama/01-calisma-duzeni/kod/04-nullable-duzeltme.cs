// 03-nullable-uyarilar.cs dosyasının uyarısız hali.
//
// Üç düzeltme, üç farklı araç:
//   is not null   -> null değilse işle
//   ?. ve ??      -> null ise yedek değer kullan
//   string?       -> "özet olmayabilir" gerçeğini tipe yazmak
//
// Çalıştırmak için:  dotnet run 04-nullable-duzeltme.cs

var raf = new Dictionary<string, Kitap>
{
    ["KTP-001"] = new Kitap("Nutuk", "Mustafa Kemal Atatürk"),
    ["KTP-002"] = new Kitap("İnce Memed", "Yaşar Kemal"),
};

Kitap? kitap = Bul("KTP-002");
if (kitap is not null)
    Console.WriteLine(kitap.Baslik);

kitap = Bul("KTP-999");
Console.WriteLine(kitap?.Baslik ?? "(bulunamadı)");

Kitap? Bul(string kod)
{
    raf.TryGetValue(kod, out Kitap? bulunan);
    return bulunan;
}

class Kitap
{
    public string Baslik { get; }
    public string Yazar { get; }
    public string? Ozet { get; set; }

    public Kitap(string baslik, string yazar)
    {
        Baslik = baslik;
        Yazar = yazar;
    }
}
