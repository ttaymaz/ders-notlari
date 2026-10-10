// Jenerik arayüz: bir kez yazılan depo, her kimlikli tip için çalışır.
//
// IDepo<T> "ne yapılacağını", BellekDepo<T> "nasıl yapılacağını" söyler.
// Kısıt (where T : class, IKimlikli) iki söz alır: T bir referans tipi
// (Bul bulamazsa null dönebilsin) ve T'nin bir Kod'u var.
// Çalıştırmak için:  dotnet run 04-jenerik-arayuz.cs

IDepo<Kitap> kitapDepo = new BellekDepo<Kitap>();
kitapDepo.Ekle(new Kitap("KTP-001", "Çalıkuşu", "Reşat Nuri Güntekin"));
kitapDepo.Ekle(new Kitap("KTP-002", "Kuyucaklı Yusuf", "Sabahattin Ali"));

IDepo<Uye> uyeDepo = new BellekDepo<Uye>();
uyeDepo.Ekle(new Uye("UYE-101", "Ayşe Yılmaz"));

Console.WriteLine(kitapDepo.Bul("KTP-002"));
Console.WriteLine(kitapDepo.Bul("KTP-999") is null);
Console.WriteLine(uyeDepo.Bul("UYE-101"));
Console.WriteLine($"{kitapDepo.Sayi} kitap, {uyeDepo.Sayi} üye");

interface IKimlikli
{
    string Kod { get; }
}

interface IDepo<T> where T : class, IKimlikli
{
    void Ekle(T oge);
    T? Bul(string kod);
    int Sayi { get; }
}

class BellekDepo<T> : IDepo<T> where T : class, IKimlikli
{
    private readonly Dictionary<string, T> _ogeler = new();

    public void Ekle(T oge) => _ogeler[oge.Kod] = oge;
    public T? Bul(string kod) => _ogeler.GetValueOrDefault(kod);
    public int Sayi => _ogeler.Count;
}

record Kitap(string Kod, string Baslik, string Yazar) : IKimlikli;
record Uye(string Kod, string AdSoyad) : IKimlikli;
