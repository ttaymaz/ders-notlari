// Bellek dışı kaynaklar: çöp toplayıcı dosyayı kapatmaz, siz kapatırsınız.
//
// using ile tanımlanan değişken, bulunduğu blok bitince Dispose edilir.
// Birden fazlası varsa kapanma sırası açılma sırasının TERSİDİR.
// Çalıştırmak için:  dotnet run 07-using-sirasi.cs

Calis();
Console.WriteLine("Calis bitti");

void Calis()
{
    using var veritabani = new Kaynak("Veritabanı");
    using var dosya = new Kaynak("Günlük dosyası");
    Console.WriteLine("İş yapılıyor");
}

class Kaynak : IDisposable
{
    private readonly string _ad;

    public Kaynak(string ad)
    {
        _ad = ad;
        Console.WriteLine($"{_ad} açıldı");
    }

    public void Dispose() => Console.WriteLine($"{_ad} kapatıldı");
}
