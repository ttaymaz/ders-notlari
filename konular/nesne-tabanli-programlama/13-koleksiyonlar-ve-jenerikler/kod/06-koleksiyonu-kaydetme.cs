// Koleksiyondan dosyaya: nesneleri geri yazmak.
//
// Okumanın aynası. Satırı nesneye çevirmeyi gördük; şimdi nesneyi satıra
// çeviriyoruz. İlginç olan şu: bu çeviri de POLİMORFİK. Her sınıf kendini
// nasıl yazacağını kendisi biliyor; kaydeden döngünün türlerden haberi yok.
//
// Çalıştırmak için:  dotnet run 06-koleksiyonu-kaydetme.cs

List<Demirbas> koleksiyon = new List<Demirbas>
{
    new Kitap(101, "Tutunamayanlar", "Oğuz Atay"),
    new Dergi(201, "Bilim ve Teknik", 745),
    new Kitap(102, "Sefiller", "Victor Hugo"),
};

// Kütüphaneye yeni bir demirbaş girdi.
koleksiyon.Add(new Dergi(202, "Arkitekt", 512));

// Bir tanesi kayboldu, kayıttan düşüyoruz.
koleksiyon.RemoveAll(d => d.DemirbasNo == 102);

Console.WriteLine($"Kaydedilecek demirbaş sayısı: {koleksiyon.Count}\n");

string yol = "demirbas-kayit.txt";

// Yazılacak satırları bir listede topluyoruz. Kaç satır olacağını baştan
// bilmiyoruz — yine dizi yerine List<string>.
List<string> satirlar = new List<string> { "# tur;no;baslik;ek" };

foreach (Demirbas d in koleksiyon)
{
    satirlar.Add(d.Satir());        // hangi sınıfsa onun sürümü çalışır
}

// WriteAllLines dosyayı açar, yazar ve kapatır. Kapatmayı unutma ihtimali yok.
// Dosya varsa ÜZERİNE YAZAR; sonuna eklemek isteseydiniz AppendAllLines.
File.WriteAllLines(yol, satirlar);

Console.WriteLine($"Yazıldı: {Path.GetFullPath(yol)}\n");

// Doğrulama: dosyayı ham metin olarak geri okuyup gösteriyoruz.
Console.WriteLine("--- dosyanın içeriği ---");

foreach (string s in File.ReadAllLines(yol))
{
    Console.WriteLine($"  {s}");
}

Console.WriteLine("\n--- aynı verinin rapor hâli ---");

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

    // Ekrana gidecek biçim.
    public virtual string Etiket()
    {
        return $"#{DemirbasNo} {Baslik}";
    }

    // Dosyaya gidecek biçim. İkisi AYRI metot çünkü iki ayrı iş:
    // biri insana okunur, diğeri geri okunabilir olmak zorunda.
    public virtual string Satir()
    {
        return $"?;{DemirbasNo};{Baslik};";
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

    public override string Satir()
    {
        return $"K;{DemirbasNo};{Baslik};{Yazar}";
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

    public override string Satir()
    {
        return $"D;{DemirbasNo};{Baslik};{Sayi}";
    }
}

// --- Denemeniz için ---
//
// 1. Kaydeden döngüye bakın: içinde tek bir `if (d is Kitap)` yok. Yeni bir
//    demirbaş türü eklerseniz bu döngüde kaç satır değişir?
//
// 2. Kitap sınıfındaki `override string Satir()` metodunu silin. Program
//    yine derlenir ve çalışır — ama dosyaya ne yazılır? Bu hatayı derleyici
//    neden yakalamıyor?
//
// 3. Başlığı noktalı virgül içeren bir kitap ekleyin: "Kar; Beyaz ve Soğuk".
//    Dosyayı kaydedin, sonra 05-dosyadan-koleksiyona.cs ile okutmayı deneyin.
//    Ayraç verinin içinde geçerse ne olur? (Gerçek sistemlerin CSV yerine
//    JSON kullanmasının sebeplerinden biri budur.)
//
// 4. File.WriteAllLines yerine File.AppendAllLines yazıp programı iki kez
//    çalıştırın. Dosyada kaç satır oldu? Hangi durumda hangisi doğru?
//
// 5. Aynı işi `using StreamWriter yazici = new StreamWriter(yol);` ile yazın.
//    Kaç satır uzadı? `using` yazmayı unutursanız ne olur?
//
// 6. RemoveAll satırındaki koşulu `d.DemirbasNo == 999` yapın. Program
//    hata verir mi? Silinecek bir şey bulamayan metot ne döndürür?
