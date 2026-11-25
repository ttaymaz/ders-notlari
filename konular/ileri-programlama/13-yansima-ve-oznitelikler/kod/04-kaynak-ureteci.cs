#:property PublishAot=false

// Kaynak üreteçleri: yansımanın işini derlerken yapmak.
//
// System.Text.Json bir nesneyi JSON'a çevirirken onun özelliklerini nereden
// biliyor? Varsayılan yol yansımadır: çalışırken tipe bakar. Kaynak üreteci
// (source generator) ise aynı bilgiyi DERLERKEN çıkarır ve C# kodu olarak
// programa ekler. Çalışırken bakılacak bir şey kalmaz: daha hızlı açılır ve
// kullanılmayan kodu kırpan araçlar (AOT) onu bozmaz.
//
// Kaynak üretecini bir öznitelik çalıştırır: [JsonSerializable],
// [GeneratedRegex]. Etiketi derleyici okur ve kodu yazar.
//
// İlk satır: `dotnet run dosya.cs` programı AOT'ye hazır kabul eder ve bu
// kipte yansımalı JSON kapalıdır. 1. bölümü yansımayla da gösterebilmek için
// kapatıyoruz. README'deki "Denemeniz için" bölümünde satırı silip ne
// olduğuna bakın.
// Çalıştırmak için:  dotnet run 04-kaynak-ureteci.cs

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

var kitap = new Kitap("Huzur", 431, ["Tanpınar", "roman"]);

Console.WriteLine("=== 1 === (JSON: yansımayla ve üretilmiş kodla)");
string yansimayla = JsonSerializer.Serialize(kitap);
string uretilenle = JsonSerializer.Serialize(kitap, KitapBaglami.Default.Kitap);
Console.WriteLine(yansimayla);
Console.WriteLine($"ikisi aynı mı? {yansimayla == uretilenle}");

Console.WriteLine("\n=== 2 === (üretilen kod gerçekten var)");
var bilgi = KitapBaglami.Default.Kitap;
Console.WriteLine($"{bilgi.GetType().Name}: {string.Join(", ", bilgi.Properties.Select(p => p.Name))}");
Console.WriteLine($"KitapBaglami üretilmiş üye sayısı > 0? {typeof(KitapBaglami).GetProperties().Length > 0}");

Console.WriteLine("\n=== 3 === ([GeneratedRegex])");
foreach (string kod in new[] { "TR-813-07", "TR-81-07", "tr-813-07" })
    Console.WriteLine($"{kod,-10} raf kodu mu? {Desenler.RafKodu().IsMatch(kod)}");

record Kitap(string Baslik, int Sayfa, string[] Etiketler);

// Derleyici bu sınıfın geri kalanını kendisi yazar (partial)
[JsonSerializable(typeof(Kitap))]
partial class KitapBaglami : JsonSerializerContext;

static partial class Desenler
{
    // Düzenli ifade derlerken C# koduna çevrilir; çalışırken yorumlanmaz
    [GeneratedRegex(@"^TR-\d{3}-\d{2}$")]
    public static partial Regex RafKodu();
}
