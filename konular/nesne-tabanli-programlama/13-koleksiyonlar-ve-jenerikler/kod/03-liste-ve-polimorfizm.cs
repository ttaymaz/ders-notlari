// List<T> + polimorfizm: dokuzuncu haftanın raporu, bu kez büyüyebilen bir listeyle.
//
// Dokuzuncu haftada koleksiyonu Demirbas[] dizisinde tutuyorduk ve boyutu
// baştan yazmak zorundaydık. Tek değişiklik dizinin List<Demirbas> olması;
// döngü ve hesap aynı kaldı.
//
// Çalıştırmak için:  dotnet run 03-liste-ve-polimorfizm.cs

List<Demirbas> koleksiyon = new List<Demirbas>();

// Liste Demirbas tipinde, ama içine türetilmiş sınıflar giriyor.
koleksiyon.Add(new Kitap(101, "Tutunamayanlar", "Oğuz Atay"));
koleksiyon.Add(new Kitap(102, "Sefiller", "Victor Hugo"));
koleksiyon.Add(new Dergi(201, "Bilim ve Teknik", 745));

// Dizide olsaydı burada duracaktık. Listede ekleme devam edebilir.
koleksiyon.Add(new Dergi(202, "Arkitekt", 512));
koleksiyon.Add(new Kitap(103, "Beyaz Diş", "Jack London"));

Console.WriteLine($"Koleksiyonda {koleksiyon.Count} demirbaş var.\n");

// Tek döngü, her satırın biçimini nesnenin kendi türü belirliyor.
foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine($"  {d.Etiket()}");
}

// --- TİP GÜVENLİĞİ ---
// koleksiyon.Add("Bu bir kitap adı");      // <- derlenmez: string, Demirbas değil
//
// Jenerik yapının asıl faydası bu. Liste hangi tiple çalışacağını biliyor;
// yanlış tipi DERLEME anında reddediyor, çalışma zamanında değil.

// --- TÜRE GÖRE SAYMA ---
int kitapSayisi = 0;

foreach (Demirbas d in koleksiyon)
{
    if (d is Kitap)
    {
        kitapSayisi++;
    }
}

Console.WriteLine($"\nKitap: {kitapSayisi} · Diğer: {koleksiyon.Count - kitapSayisi}");

// --- SİLME: liste kaydırmayı kendi yapar ---
Demirbas iadeEdilen = koleksiyon[1];
koleksiyon.Remove(iadeEdilen);

Console.WriteLine($"\n'{iadeEdilen.Baslik}' çıkarıldı. Kalan: {koleksiyon.Count}");

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
// 1. Tip güvenliği satırındaki yorumu kaldırın. Derleyici hangi mesajı
//    veriyor? Bu hata ÇALIŞMA zamanında değil DERLEME zamanında geliyor —
//    jenerik yapının asıl kazancı budur.
//
// 2. `List<Demirbas>` yerine `List<Kitap>` yazın. Hangi satırlar derlenmez?
//    Neden? Listenin tipi ne kadar dar olursa içine o kadar az şey girer.
//
// 3. Listeye altıncı bir demirbaş ekleyin. Kaç satır değiştirmeniz gerekti?
//    Dokuzuncu haftadaki dizi sürümünde kaç satır değişirdi?
//
// 4. `koleksiyon.Remove(iadeEdilen)` yerine `koleksiyon.RemoveAt(1)` yazın.
//    Sonuç aynı mı? Hangisi daha güvenli — neden?
//
// 5. `d is Kitap` yerine `d.GetType() == typeof(Kitap)` yazın. Kitap
//    sınıfından bir sınıf daha türetirseniz iki ifade farklı sonuç verir.
//    Hangisi "ve alt türleri" anlamına gelir?
