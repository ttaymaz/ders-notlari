// FİNAL PROVASININ ÇÖZÜMÜ ve puanlama ölçütleri.
//
// 02-tasarim-provasi.cs dosyasını ÇÖZMEDEN buraya bakmayın.
// Çözümü okumak, çözmek değildir.
//
// Çalıştırmak için:  dotnet run 03-prova-cozumu.cs

Sube sube = new Sube();

sube.GonderiAl(new StandartGonderi("TK1001", "Ayşe Kaya", 3.5m));
sube.GonderiAl(new HizliGonderi("TK1002", "Mehmet Demir", 2m, 5000m));
sube.GonderiAl(new StandartGonderi("TK1003", "Zeynep Ak", 10m));

sube.Rapor();

Console.WriteLine();
sube.Sorgula("TK1002");
sube.Sorgula("TK9999");

sube.SigortaRaporu();


// =====================================================================
// ÇÖZÜM
// =====================================================================

// [Ölçüt 4] Arayüz: sigortalanabilmek bir YETENEKTİR, bir tür değil.
// Temel sınıfa yazılsaydı standart gönderinin de beyan bedeli olurdu —
// ya anlamsız bir sıfır döndürürdü ya da hata fırlatırdı. İkisi de yanlış.
interface ISigortalanabilir
{
    decimal BeyanBedeli { get; }
    decimal SigortaPrimi();
}


// [Ölçüt 1] abstract: "gönderi" diye tek başına bir ürün yok; ya standart
// ya hızlıdır. private set: üç özellik de kurucudan sonra değişmemeli.
abstract class Gonderi
{
    public string TakipNo { get; private set; }
    public string Alici { get; private set; }
    public decimal Agirlik { get; private set; }

    protected Gonderi(string takipNo, string alici, decimal agirlik)
    {
        TakipNo = takipNo;
        Alici = alici;
        Agirlik = agirlik;
    }

    // [Ölçüt 2] abstract: her türde VAR ama ortak bir gövde yazılamaz.
    // "Genel olarak kargo ücreti" diye bir hesap yok.
    public abstract decimal UcretHesapla();

    // [Ölçüt 2] virtual: ortak bir gövde var, tür isterse üzerine ekler.
    public virtual string Etiket()
    {
        return $"{TakipNo} {Alici,-14} {Agirlik,5:F1} kg {UcretHesapla(),11:C}";
    }
}


// [Ölçüt 3] Kalıtım ve base çağrısı.
class StandartGonderi : Gonderi
{
    private const decimal KiloUcreti = 25m;

    public StandartGonderi(string takipNo, string alici, decimal agirlik)
        : base(takipNo, alici, agirlik) { }

    public override decimal UcretHesapla()
    {
        return Agirlik * KiloUcreti;
    }

    // base çağrısı: ortak kısmı yeniden yazmıyoruz, üzerine ekliyoruz.
    public override string Etiket()
    {
        return base.Etiket() + "  [standart]";
    }
}


// [Ölçüt 3-4] Hem kalıtım (bir gönderidir) hem arayüz (sigortalanabilir).
class HizliGonderi : Gonderi, ISigortalanabilir
{
    private const decimal KiloUcreti = 40m;
    private const decimal HizmetBedeli = 50m;
    private const decimal PrimOrani = 0.03m;

    public decimal BeyanBedeli { get; private set; }

    public HizliGonderi(string takipNo, string alici, decimal agirlik, decimal beyan)
        : base(takipNo, alici, agirlik)
    {
        BeyanBedeli = beyan;
    }

    public override decimal UcretHesapla()
    {
        return Agirlik * KiloUcreti + HizmetBedeli;
    }

    public decimal SigortaPrimi()
    {
        return BeyanBedeli * PrimOrani;
    }

    public override string Etiket()
    {
        return base.Etiket() + "  [hızlı]";
    }
}


// [Ölçüt 5] Sube bir Gonderi DEĞİLDİR; gönderileri içinde tutar.
class Sube
{
    // İki koleksiyon, iki farklı iş:
    //   liste  -> sırayla rapor
    //   defter -> takip numarasından anında erişim
    private readonly List<Gonderi> liste = new List<Gonderi>();
    private readonly Dictionary<string, Gonderi> defter =
        new Dictionary<string, Gonderi>();

    public void GonderiAl(Gonderi g)
    {
        liste.Add(g);

        // Add seçildi: aynı takip numarası ikinci kez gelirse bu bir
        // veri hatasıdır ve sessizce üzerine yazılmamalı. İndisleyici
        // (defter[g.TakipNo] = g) eski kaydı sessizce silerdi.
        defter.Add(g.TakipNo, g);
    }

    public void Rapor()
    {
        Console.WriteLine("===== ŞUBE RAPORU =====");

        decimal toplam = 0m;

        // Tek döngü; satırın biçimini nesnenin kendi türü belirliyor.
        foreach (Gonderi g in liste)
        {
            Console.WriteLine($"  {g.Etiket()}");
            toplam += g.UcretHesapla();
        }

        Console.WriteLine($"  {"TOPLAM",-21} {toplam,14:C}   ({liste.Count} gönderi)");
    }

    public void Sorgula(string takipNo)
    {
        // Güvenli arama: defter[takipNo] yazsaydık olmayan numarada çökerdi.
        if (defter.TryGetValue(takipNo, out Gonderi? g))
        {
            Console.WriteLine($"{takipNo} -> {g.Alici}, {g.UcretHesapla():C}");
        }
        else
        {
            Console.WriteLine($"{takipNo} -> kayıt bulunamadı");
        }
    }

    public void SigortaRaporu()
    {
        Console.WriteLine("\n===== SİGORTA =====");

        foreach (Gonderi g in liste)
        {
            // Türü sormuyoruz, yeteneği soruyoruz. Yarın sigortalanabilen
            // başka bir tür eklenirse bu satır değişmeyecek.
            if (g is ISigortalanabilir s)
            {
                Console.WriteLine(
                    $"  {g.TakipNo} beyan {s.BeyanBedeli,10:C}  prim {s.SigortaPrimi(),8:C}");
            }
        }
    }
}

// =====================================================================
// PUANLAMA ÖLÇÜTLERİ (25 puan)
// =====================================================================
//
//   1. Soyut sınıf doğru kurulmuş                                5
//      abstract sınıf + üç özellik private set + protected kurucu
//      Kısmi: abstract yazmayıp gerisi doğruysa 3
//
//   2. abstract / virtual seçimi doğru                           5
//      UcretHesapla abstract, Etiket virtual
//      Kısmi: ikisi de virtual ise 3 (kavram var, ayrım eksik)
//             ikisi de normal metot ise 1
//
//   3. Türetilmiş sınıflar ve base çağrısı                       5
//      : base(...) kurucu zinciri + Etiket içinde base.Etiket()
//      Kısmi: base çağırmayıp ortak satırı kopyalayan çözüm 3
//
//   4. Arayüz doğru yerde ve yetenek olarak sorulmuş             5
//      ISigortalanabilir yalnızca HizliGonderi'de + "is" ile sorgu
//      Kısmi: arayüz doğru ama "is HizliGonderi" ile sorulmuşsa 3
//             arayüz temel sınıfa yazılmışsa 1
//
//   5. Koleksiyon seçimi doğru ve gerekçeli                      5
//      List (rapor) + Dictionary (arama) + TryGetValue
//      Kısmi: yalnızca List kullanıp döngüyle arayan çözüm 3
//             defter[takipNo] ile çöken çözüm 2
//
// Sözdizimi hataları (noktalı virgül, büyük-küçük harf) puan
// kaybettirmez; değerlendirilen şey tasarımın doğruluğudur.
