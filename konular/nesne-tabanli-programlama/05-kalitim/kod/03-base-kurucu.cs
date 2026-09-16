// Kurucu sırası: türetilmiş nesne doğarken önce TEMEL sınıf kurulur.
//
// Şema: assets/02-kurucu-sirasi.svg
// Çalıştırmak için:  dotnet run 03-base-kurucu.cs

Console.WriteLine("=== new Kitap(...) çağrılıyor ===");
Kitap k = new Kitap(101, "Tutunamayanlar", "Oğuz Atay");

Console.WriteLine("\n=== Nesne hazır ===");
k.BilgiYazdir();

Console.WriteLine("\n=== İkinci nesne ===");
Kitap k2 = new Kitap(102, "Kürk Mantolu Madonna", "Sabahattin Ali");
k2.BilgiYazdir();


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }

    public Demirbas(int no, string baslik)
    {
        Console.WriteLine("  1. Demirbas kurucusu çalıştı (TEMEL sınıf)");
        DemirbasNo = no;
        Baslik = baslik;
    }

    public void BilgiYazdir()
    {
        Console.WriteLine($"  #{DemirbasNo} {Baslik}");
    }
}


class Kitap : Demirbas
{
    public string Yazar { get; private set; }

    // ": base(no, baslik)" ÖNCE çalışır, sonra aşağıdaki gövde.
    public Kitap(int no, string baslik, string yazar) : base(no, baslik)
    {
        Console.WriteLine("  2. Kitap kurucusunun gövdesi çalıştı (TÜRETİLMİŞ)");
        Yazar = yazar;
    }
}

// --- ÇIKTIYA BAKIN ---
//
// Sıra her zaman aynı: önce TEMEL sınıfın kurucusu, sonra türetilmiş
// sınıfın gövdesi. Bu sıra değiştirilemez ve mantıklıdır: temel sınıf
// hazır olmadan üzerine ekleme yapamazsınız.
//
// --- base(...) YAZMAZSANIZ NE OLUR? ---
//
// C# temel sınıfın PARAMETRESİZ kurucusunu çağırmayı dener. Demirbas
// sınıfında parametresiz kurucu yok (ikinci haftadaki kural: elle
// kurucu yazıldığı için görünmez olan kayboldu). Bu yüzden:
//
//     public Kitap(int no, string baslik, string yazar)   // base(...) yok
//     {
//         Yazar = yazar;
//     }
//
// şu hatayı verir:
//     'Demirbas' does not contain a constructor that takes 0 arguments
//
// Yani ikinci haftanın "görünmez kurucu kayboldu" tuzağı, kalıtımda
// ikinci kez karşınıza çıkıyor. Deneyin: yukarıdaki ": base(no, baslik)"
// kısmını silin ve hatayı okuyun.
