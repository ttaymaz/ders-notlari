// Yapılandırma: aynı ayar, birden çok kaynak, sonra gelen kazanır.
//
// Uygulamada varsayılanlar appsettings.json dosyasından gelir. Bu örnek
// tek dosya olsun diye onun yerine bellekteki bir sözlüğü kullanıyor;
// davranış aynı. Üstüne iki kaynak daha ekleniyor:
//
//   1. varsayılanlar (appsettings.json yerine)
//   2. KUTUPHANE_ ile başlayan ortam değişkenleri
//   3. komut satırı argümanları
//
// Bir anahtar birden çok kaynakta varsa SONRA eklenen kaynak kazanır.
// "Ceza:GunlukUcret" gibi iki noktalı anahtarlar bir bölüm ve alt
// anahtarıdır; ortam değişkeninde ":" yerine "__" yazılır.
//
// Önce argümansız çalıştırın; sonra README'deki "Denemeniz için"
// komutlarıyla ezin.
// Çalıştırmak için:  dotnet run 04-yapilandirma.cs

#:package Microsoft.Extensions.Configuration.Binder@10.0.14
#:package Microsoft.Extensions.Configuration.EnvironmentVariables@10.0.14
#:package Microsoft.Extensions.Configuration.CommandLine@10.0.14
#:package Microsoft.Extensions.Options.ConfigurationExtensions@10.0.14
#:package Microsoft.Extensions.DependencyInjection@10.0.14

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var varsayilanlar = new Dictionary<string, string?>
{
    ["Ceza:GunlukUcret"] = "3",
    ["Ceza:Tavan"] = "60",
    ["Odunc:Gun"] = "14",
};

IConfiguration ayarlar = new ConfigurationBuilder()
    .AddInMemoryCollection(varsayilanlar)
    .AddEnvironmentVariables(prefix: "KUTUPHANE_")
    .AddCommandLine(args)
    .Build();

Console.WriteLine("=== 1 === (anahtarla okumak)");
Console.WriteLine($"Ceza:GunlukUcret = {ayarlar["Ceza:GunlukUcret"]}");
Console.WriteLine($"Odunc:Gun        = {ayarlar["Odunc:Gun"]}");
Console.WriteLine($"Olmayan:Anahtar  = {ayarlar["Olmayan:Anahtar"] ?? "(null)"}");

Console.WriteLine("\n=== 2 === (bir bölümü sınıfa bağlamak)");
var ceza = ayarlar.GetSection("Ceza").Get<CezaAyarlari>()!;
Console.WriteLine($"{ceza.GunlukUcret} TL/gün, tavan {ceza.Tavan} TL");

Console.WriteLine("\n=== 3 === (IOptions<T>: servise ayar vermek)");
var servisler = new ServiceCollection();
servisler.Configure<CezaAyarlari>(ayarlar.GetSection("Ceza"));
servisler.AddSingleton<CezaYazici>();
using var kap = servisler.BuildServiceProvider();
kap.GetRequiredService<CezaYazici>().Yaz();

// Ayar sınıfı: özellik adları yapılandırmadaki anahtarlarla aynı
class CezaAyarlari
{
    public int GunlukUcret { get; set; }
    public int Tavan { get; set; }
}

// Servis ayarı IOptions<T> üzerinden alır; nereden geldiğini bilmez
class CezaYazici(IOptions<CezaAyarlari> secenekler)
{
    public void Yaz()
    {
        var a = secenekler.Value;
        Console.WriteLine($"CezaYazici: 10 gün gecikme = {Math.Min(10 * a.GunlukUcret, a.Tavan)} TL");
    }
}
