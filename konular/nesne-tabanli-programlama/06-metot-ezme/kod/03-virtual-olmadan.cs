// virtual olmadan ne olur? Ezme ile GİZLEME arasındaki fark.
//
// Bu dosya dönemin en ince ayrımlarından birini gösteriyor. Farkın
// ortaya çıkması için nesneyi TEMEL SINIF tipinde bir değişkende
// tutmamız gerekiyor — bunun adı polimorfizm ve dokuzuncu haftanın
// konusu. Burada yalnızca farkı görecek kadarına bakıyoruz.
//
// Şema: assets/01-ezme-akisi.svg
// Çalıştırmak için:  dotnet run 03-virtual-olmadan.cs

Console.WriteLine("=== DOĞRU: virtual + override ===");
DogruKitap dk = new DogruKitap("Tutunamayanlar", "Oğuz Atay");

Console.WriteLine("Kendi tipinde değişkende:");
dk.BilgiYazdir();

// Aynı nesneyi TEMEL SINIF tipinde bir değişkende tutuyoruz.
DogruDemirbas dd = dk;
Console.WriteLine("Temel sınıf tipinde değişkende:");
dd.BilgiYazdir();          // yine Kitap'ın versiyonu çalışır

Console.WriteLine("\n=== YANLIŞ: virtual yok, yalnızca gizleme ===");
YanlisKitap yk = new YanlisKitap("Tutunamayanlar", "Oğuz Atay");

Console.WriteLine("Kendi tipinde değişkende:");
yk.BilgiYazdir();          // Kitap'ın versiyonu — doğru görünüyor

YanlisDemirbas yd = yk;
Console.WriteLine("Temel sınıf tipinde değişkende:");
yd.BilgiYazdir();          // TEMEL sınıfın versiyonu — yazar kayboldu

Console.WriteLine("\nAynı nesne, iki farklı çıktı. Fark: virtual.");


// --- DOĞRU KURULUM ---
class DogruDemirbas
{
    public string Baslik { get; private set; }

    public DogruDemirbas(string baslik)
    {
        Baslik = baslik;
    }

    public virtual void BilgiYazdir()
    {
        Console.WriteLine($"  [temel]  {Baslik}");
    }
}

class DogruKitap : DogruDemirbas
{
    public string Yazar { get; private set; }

    public DogruKitap(string baslik, string yazar) : base(baslik)
    {
        Yazar = yazar;
    }

    public override void BilgiYazdir()
    {
        Console.WriteLine($"  [kitap]  {Baslik} / {Yazar}");
    }
}


// --- YANLIŞ KURULUM ---
class YanlisDemirbas
{
    public string Baslik { get; private set; }

    public YanlisDemirbas(string baslik)
    {
        Baslik = baslik;
    }

    // virtual YOK
    public void BilgiYazdir()
    {
        Console.WriteLine($"  [temel]  {Baslik}");
    }
}

class YanlisKitap : YanlisDemirbas
{
    public string Yazar { get; private set; }

    public YanlisKitap(string baslik, string yazar) : base(baslik)
    {
        Yazar = yazar;
    }

    // "new" burada "gizle" demektir: temel sınıfın metodunu EZMİYOR,
    // üzerini örtüyor. new yazmazsanız program yine derlenir ama
    // derleyici uyarı verir (CS0108) — uyarı tam da bunu söyler.
    public new void BilgiYazdir()
    {
        Console.WriteLine($"  [kitap]  {Baslik} / {Yazar}");
    }
}

// --- FARK NEDEN ÖNEMLİ? ---
//
// Gizleme (new) DEĞİŞKENİN TİPİNE bakar.
// Ezme (virtual/override) NESNENİN GERÇEK TİPİNE bakar.
//
// Kendi tipinde bir değişkenle çalışırken ikisi aynı görünür; hata
// ancak nesneleri temel sınıf tipinde tuttuğunuzda ortaya çıkar.
// Ve bir kütüphane otomasyonunda tam olarak bunu yaparsınız: bütün
// demirbaşları tek bir listede tutup hepsini aynı döngüde yazdırmak
// istersiniz.
//
// Kural basit: türetilmiş sınıfta davranışı değiştirecekseniz temel
// sınıftaki metodu virtual yapın ve override kullanın. "new" ile
// gizlemeye ihtiyacınız olacak durumlar nadirdir.
