// HATALI DOSYA — bu kod DERLENİR ama ÇALIŞIRKEN çöker.
//
// DİKKAT: Bu dosya kasıtlı olarak hatalıdır. Üç ayrı hata var; ikisi
// programı çökertiyor, üçüncüsü çökertmiyor — sessizce veri kaybettiriyor.
// Asıl tehlikeli olan sonuncusu.
//
// Nasıl çalışacaksınız:
//   1. Önce kağıt üzerinde tahmin edin: hangi satırda, hangi hatayla durur?
//   2. Sonra "dotnet run 02-satiri-nesneye-cevirme.cs" ile çalıştırın
//   3. Çöken hatayı düzeltin, tekrar çalıştırın — ikinci çökme ortaya çıkacak
//   4. Program hatasız çalıştığında KAÇ demirbaş okuduğuna bakın. Dosyada
//      kaç veri satırı var? Sayılar tutuyor mu?
//
// Cevap anahtarı ders notunda, kapalı bölümde. Önce kendiniz deneyin.

string yol = "hatali-demirbas.txt";

File.WriteAllLines(yol, new string[]
{
    "# tur;no;baslik;ek",
    "K;101;Tutunamayanlar;Oğuz Atay",
    "D;201;Bilim ve Teknik;745",
    "",
    "k;102;Sefiller;Victor Hugo",
    "D;202;Arkitekt;512",
});

string[] satirlar = File.ReadAllLines(yol);
List<Demirbas> koleksiyon = new List<Demirbas>();

foreach (string satir in satirlar)
{
    string[] alan = satir.Split(';');

    int no = int.Parse(alan[1]);            // <-- HATA BURADA (ve öncesinde)

    if (alan[0] == "K")
    {
        koleksiyon.Add(new Kitap(no, alan[2], alan[3]));
    }
    else if (alan[0] == "D")
    {
        koleksiyon.Add(new Dergi(no, alan[2], int.Parse(alan[3])));
    }
}

Console.WriteLine($"{koleksiyon.Count} demirbaş okundu.\n");

foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine($"  {d.Etiket()}");
}


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }

    public Demirbas(int no, string baslik)
    {
        DemirbasNo = no;
        Baslik = baslik;
    }

    public virtual string Etiket()
    {
        return $"#{DemirbasNo} {Baslik}";
    }
}


class Kitap : Demirbas
{
    public string Yazar { get; private set; }

    public Kitap(int no, string baslik, string yazar) : base(no, baslik)
    {
        Yazar = yazar;
    }

    public override string Etiket()
    {
        return $"{base.Etiket()} / {Yazar}";
    }
}


class Dergi : Demirbas
{
    public int Sayi { get; private set; }

    public Dergi(int no, string baslik, int sayi) : base(no, baslik)
    {
        Sayi = sayi;
    }

    public override string Etiket()
    {
        return $"{base.Etiket()} (sayı {Sayi})";
    }
}

// --- Denemeniz için ---
//
// 1. Hataları sırayla düzeltin. Her düzeltmeden sonra çalıştırın; bir
//    sonraki hata ortaya çıkacak. İki çökmeyi geçtikten sonra durun ve
//    okunan demirbaş sayısını dosyadaki veri satırı sayısıyla karşılaştırın.
//
// 2. Dördüncü hatayı bulduğunuzda şunu düşünün: bu hata bir kütüphane
//    kaydında olsaydı ne olurdu? Kitap dosyada var, sistemde yok. Ne zaman
//    fark edilir?
//
// 3. `else if` zincirinin sonuna bir `else` ekleyip tanınmayan satırı
//    ekrana yazdırın. Sessiz hatayı gürültülü hataya çevirdiniz. Hangisi
//    daha iyi?
//
// 4. Dosyaya alanlarından biri eksik bir satır ekleyin: "K;103;Beyaz Diş".
//    Hangi hatayı alıyorsunuz? Bunu `alan.Length` ile nasıl önlersiniz?
//
// 5. Dosyaya "K;abc;Numarası Bozuk;Yazar" satırını ekleyin. int.Parse ne
//    yapıyor? int.TryParse ile yazınca program ne yapar? Hangisini
//    seçeceğiniz veriye bağlı: bir kütüphane kataloğunda satırı atlayıp
//    devam etmek makul, bir banka dosyasında değil.
//
// 6. Düzeltilmiş sürümünüzü 05-dosyadan-koleksiyona.cs ile karşılaştırın.
//    Oradaki SatiriCevir metodu ayrıştırmayı ayrı bir yere almış. Neden
//    okumak daha kolay?
