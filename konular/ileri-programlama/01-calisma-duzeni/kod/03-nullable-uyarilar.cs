// Nullable referans tipleri: derleyici null hatasını ÖNCEDEN söylüyor.
//
// Bu dosya derlenir ama derleyici üç uyarı verir. Uyarıları okumadan
// çalıştırırsanız program ikinci aramada çöker.
// Uyarıları görmek için:  dotnet build 03-nullable-uyarilar.cs
// Çalıştırmak için:       dotnet run 03-nullable-uyarilar.cs
// Düzeltilmiş sürüm:      04-nullable-duzeltme.cs

var raf = new Dictionary<string, Kitap>
{
    ["KTP-001"] = new Kitap("Nutuk", "Mustafa Kemal Atatürk"),
    ["KTP-002"] = new Kitap("İnce Memed", "Yaşar Kemal"),
};

Kitap? kitap = Bul("KTP-002");
Console.WriteLine(kitap.Baslik);

kitap = Bul("KTP-999");
Console.WriteLine(kitap.Baslik);

// Kitap? : "bu metot kitap döndürebilir, döndürmeyebilir de"
Kitap? Bul(string kod)
{
    raf.TryGetValue(kod, out Kitap? bulunan);
    return bulunan;
}

class Kitap
{
    public string Baslik { get; }
    public string Yazar { get; }
    public string Ozet { get; set; }

    public Kitap(string baslik, string yazar)
    {
        Baslik = baslik;
        Yazar = yazar;
    }
}
