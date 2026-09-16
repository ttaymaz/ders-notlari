// Hazır bir metodu ezmek: ToString()
//
// Şimdiye kadar hep kendi yazdığımız metotları ezdik. Ama C#'ta her
// sınıfın, siz yazmasanız bile devraldığı metotlar vardır. En çok
// işe yarayanı ToString().
//
// Çalıştırmak için:  dotnet run 04-tostring-ezme.cs

Console.WriteLine("=== ToString EZİLMEDEN ===");
Sade s = new Sade("Tutunamayanlar");
Console.WriteLine(s);                       // tip adını yazar
Console.WriteLine($"Metin içinde: {s}");

Console.WriteLine("\n=== ToString EZİLDİKTEN SONRA ===");
Kitap k = new Kitap("Tutunamayanlar", "Oğuz Atay", 724);
Console.WriteLine(k);                       // kendi yazdığımız metin
Console.WriteLine($"Metin içinde: {k}");

Console.WriteLine("\n=== Listede de otomatik çalışır ===");
Kitap[] raf =
{
    new Kitap("Tutunamayanlar", "Oğuz Atay", 724),
    new Kitap("Kürk Mantolu Madonna", "Sabahattin Ali", 160),
    new Kitap("Saatleri Ayarlama Enstitüsü", "A. H. Tanpınar", 384),
};

foreach (Kitap kitap in raf)
{
    Console.WriteLine($"  - {kitap}");
}


class Sade
{
    public string Baslik { get; private set; }

    public Sade(string baslik)
    {
        Baslik = baslik;
    }
}


class Kitap
{
    public string Baslik { get; private set; }
    public string Yazar { get; private set; }
    public int SayfaSayisi { get; private set; }

    public Kitap(string baslik, string yazar, int sayfaSayisi)
    {
        Baslik = baslik;
        Yazar = yazar;
        SayfaSayisi = sayfaSayisi;
    }

    // ToString, C#'taki her sınıfın devraldığı bir metottur ve
    // virtual olarak tanımlanmıştır — yani ezilmek üzere yazılmıştır.
    public override string ToString()
    {
        return $"{Baslik} / {Yazar} ({SayfaSayisi} s.)";
    }
}

// --- NE OLDU? ---
//
// Console.WriteLine(nesne) yazdığınızda C# arka planda nesne.ToString()
// çağırır. Ezmezseniz varsayılan versiyon çalışır ve yalnızca tipin
// adını yazar. Ezerseniz kendi metniniz görünür.
//
// Aynısı metin birleştirmede de olur: $"{kitap}" ifadesi de ToString
// çağırır.
//
// --- NEDEN İŞE YARAR? ---
//
// Hata ayıklarken bir nesnenin içinde ne olduğunu görmek istersiniz.
// ToString ezilmemişse ekranda yalnızca "Kitap" yazar ve hiçbir şey
// öğrenemezsiniz.
//
// Bahar döneminde bu metot daha da işe yarayacak: bir listeyi ekrandaki
// bir kutuya doldurduğunuzda, kutu her nesne için ToString çağırır.
// Ezmezseniz listede sınıf adı görünür.
//
// --- DENEYİN ---
//
// Kitap sınıfındaki "override" kelimesini silin. Derleyici ne diyor?
// Sonra metodun adını "Yazdir" yapıp override'ı geri koyun. Şimdi ne diyor?
