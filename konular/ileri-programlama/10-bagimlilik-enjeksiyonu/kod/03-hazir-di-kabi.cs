// .NET'in hazır DI kabı: ServiceCollection ve üç ömür.
//
// 02'deki kabımızın yaptığını bu kap da yapar: kurucu parametrelerine
// bakarak bağımlılık grafını kendisi gezer, fabrika yazmanıza gerek yok.
// Fazladan bir şey daha sorar: oluşturduğu nesneyi NE KADAR süre kullansın?
//
//   Singleton (tekil)    uygulama boyunca tek nesne
//   Scoped (kapsamlı)    her kapsam (ör. her web isteği) için bir nesne
//   Transient (geçici)   her istendiğinde yeni nesne
//
// Her sınıf kurulduğunda kendine bir sıra numarası alıyor. Hangi satır
// hangi numarayı yazar?
// Çalıştırmak için:  dotnet run 03-hazir-di-kabi.cs

#:package Microsoft.Extensions.DependencyInjection@10.0.14

using Microsoft.Extensions.DependencyInjection;

var servisler = new ServiceCollection();
servisler.AddSingleton<Ayarlar>();
servisler.AddScoped<Sepet>();
servisler.AddTransient<Fis>();
// Açık jenerik kayıt: her T için IDepo<T> istenirse BellekDepo<T> verilir
servisler.AddSingleton(typeof(IDepo<>), typeof(BellekDepo<>));

using var kap = servisler.BuildServiceProvider(new ServiceProviderOptions
{
    ValidateScopes = true,
    ValidateOnBuild = true,
});

Console.WriteLine("=== 1 === (tekil ve geçici)");
Console.WriteLine($"{kap.GetRequiredService<Ayarlar>()} {kap.GetRequiredService<Ayarlar>()}");
Console.WriteLine($"{kap.GetRequiredService<Fis>()} {kap.GetRequiredService<Fis>()}");

Console.WriteLine("\n=== 2 === (kapsamlı)");
using (var kapsam1 = kap.CreateScope())
{
    var p = kapsam1.ServiceProvider;
    Console.WriteLine($"kapsam 1: {p.GetRequiredService<Sepet>()} {p.GetRequiredService<Sepet>()}");
}
using (var kapsam2 = kap.CreateScope())
{
    var p = kapsam2.ServiceProvider;
    Console.WriteLine($"kapsam 2: {p.GetRequiredService<Sepet>()}");
}

Console.WriteLine("\n=== 3 === (açık jenerik)");
Console.WriteLine(kap.GetRequiredService<IDepo<Fis>>().GetType().Name);
Console.WriteLine(ReferenceEquals(kap.GetRequiredService<IDepo<Fis>>(), kap.GetRequiredService<IDepo<Fis>>()));

Console.WriteLine("\n=== 4 === (kayıtsız servis)");
Console.WriteLine(kap.GetService<Kasa>() is null ? "GetService: null" : "GetService: bulundu");
try
{
    kap.GetRequiredService<Kasa>();
}
catch (InvalidOperationException)
{
    Console.WriteLine("GetRequiredService: InvalidOperationException");
}

Console.WriteLine("\n=== 5 === (tekil, kapsamlıya bağımlıysa)");
var hatali = new ServiceCollection();
hatali.AddScoped<Sepet>();
hatali.AddSingleton<Kasa>();     // Kasa'nın kurucusu Sepet istiyor
try
{
    hatali.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true });
    Console.WriteLine("kap kuruldu");
}
catch (AggregateException e)
{
    Console.WriteLine($"kurulamadı: {e.InnerExceptions.Count} hata");
    Console.WriteLine(e.InnerExceptions[0].Message);
}

class Ayarlar
{
    private static int _sayac;
    private readonly int _no = ++_sayac;
    public override string ToString() => $"Ayarlar#{_no}";
}

class Sepet
{
    private static int _sayac;
    private readonly int _no = ++_sayac;
    public override string ToString() => $"Sepet#{_no}";
}

class Fis
{
    private static int _sayac;
    private readonly int _no = ++_sayac;
    public override string ToString() => $"Fis#{_no}";
}

class Kasa(Sepet sepet)
{
    public Sepet Sepet { get; } = sepet;
}

interface IDepo<T>;
class BellekDepo<T> : IDepo<T>;
