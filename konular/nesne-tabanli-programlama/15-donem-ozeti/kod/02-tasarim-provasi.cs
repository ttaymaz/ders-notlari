// FİNAL PROVASI — 1., 2. ve 4. soruların formatı tek problemde.
//
// Bu dosya sınavdakiyle AYNI FORMATTA bir tasarım problemidir.
// Sınavda bu senaryo çıkmayacaktır; format aynı, senaryo farklı olacak.
//
// Bu dosya DERLENİR ama iskelet EKSİKTİR — çıktı yanlıştır. Görev sizin.
// Çözüm 03-prova-cozumu.cs içinde; önce kendiniz deneyin.
//
// Çalıştırmak için:  dotnet run 02-tasarim-provasi.cs
//
// =====================================================================
// SORU
// =====================================================================
// Bir kargo şubesi gönderileri yönetiyor.
//
// Her gönderinin takip numarası, alıcısı ve ağırlığı vardır; üçü de
// dışarıdan okunabilir ama değiştirilemez.
//
// Ücret gönderi türüne göre hesaplanır:
//   - Standart gönderi : kilo başına 25 TL
//   - Hızlı gönderi    : kilo başına 40 TL + 50 TL hizmet bedeli
//
// Bazı gönderiler SİGORTALANABİLİR. Sigortalanabilen gönderinin beyan
// bedeli vardır; prim, beyan bedelinin %3'üdür. Standart gönderiler
// sigortalanamaz.
//
// Şube gönderileri hem sırayla raporlayabilmeli hem de takip
// numarasından anında bulabilmelidir.
// =====================================================================

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
// BEKLENEN ÇIKTI
// =====================================================================
// ===== ŞUBE RAPORU =====
//   TK1001 Ayşe Kaya        3,5 kg      ₺87,50  [standart]
//   TK1002 Mehmet Demir     2,0 kg     ₺130,00  [hızlı]
//   TK1003 Zeynep Ak       10,0 kg     ₺250,00  [standart]
//   TOPLAM                       ₺467,50   (3 gönderi)
//
// TK1002 -> Mehmet Demir, ₺130,00
// TK9999 -> kayıt bulunamadı
//
// ===== SİGORTA =====
//   TK1002 beyan  ₺5.000,00  prim  ₺150,00
//
// =====================================================================


// =====================================================================
// BURADAN AŞAĞISINI SİZ TAMAMLAYACAKSINIZ
// İskelet derlenir ama doğru çalışmaz.
// =====================================================================


// ---------- TODO 1 ----------
// Sigortalanabilme YETENEĞİNİ tanımlayan arayüz.
// İki üye: BeyanBedeli (yalnızca okunur) ve SigortaPrimi().
// Soru: bu neden temel sınıfa yazılmıyor? Cevabınızı yorum satırı yapın.
interface ISigortalanabilir
{
    // TODO: üyeleri ekleyin
}


// ---------- TODO 2 ----------
// Bütün gönderilerin ortak şablonu.
//   - Tek başına nesne üretilememeli
//   - Üç özellik dışarıdan OKUNUR, YAZILAMAZ
//   - UcretHesapla: her türde farklı, ortak gövde yok
//   - Etiket: ortak gövdesi var, türler üzerine ekleyebilsin
class Gonderi                       // TODO: tek başına üretilememeli
{
    public string TakipNo { get; set; }      // TODO: dışarıdan yazılamasın
    public string Alici { get; set; }        // TODO: dışarıdan yazılamasın
    public decimal Agirlik { get; set; }     // TODO: dışarıdan yazılamasın

    public Gonderi(string takipNo, string alici, decimal agirlik)
    {
        TakipNo = takipNo;
        Alici = alici;
        Agirlik = agirlik;
    }

    // TODO: her türde farklı hesaplanmalı — hangi anahtar kelime?
    public decimal UcretHesapla()
    {
        return 0m;
    }

    // TODO: türler üzerine ekleyebilmeli — hangi anahtar kelime?
    public string Etiket()
    {
        return $"{TakipNo} {Alici,-14} {Agirlik,5:F1} kg {UcretHesapla(),11:C}";
    }
}


// ---------- TODO 3 ----------
// Standart gönderi: kilo başına 25 TL. Etikete "  [standart]" ekleyin.
class StandartGonderi : Gonderi
{
    public StandartGonderi(string takipNo, string alici, decimal agirlik)
        : base(takipNo, alici, agirlik) { }

    // TODO: ücreti hesaplayın ve etiketi genişletin
}


// ---------- TODO 4 ----------
// Hızlı gönderi: kilo başına 40 TL + 50 TL hizmet bedeli.
// Sigortalanabilir; beyan bedeli kurucuda alınır, prim beyanın %3'ü.
// Etikete "  [hızlı]" ekleyin.
class HizliGonderi : Gonderi
{
    public HizliGonderi(string takipNo, string alici, decimal agirlik, decimal beyan)
        : base(takipNo, alici, agirlik)
    {
        // TODO: beyan bedelini saklayın
    }

    // TODO: ücret, etiket ve arayüz üyeleri
}


// ---------- TODO 5-9 ----------
// Şube bir Gonderi DEĞİLDİR; gönderileri yönetir.
//   5: iki koleksiyon — sırayla rapor için, takip numarasından erişim için
//   6: GonderiAl — ikisine de ekleyin; aynı numara ikinci kez gelirse
//      hata versin (hangi metodu seçtiğinizi gerekçelendirin)
//   7: Rapor — tek döngü, toplam ücret, son satır
//   8: Sorgula — sözlükten GÜVENLİ arama
//   9: SigortaRaporu — türü değil, YETENEĞİ sorun
class Sube
{
    // TODO 5: koleksiyonlar (dışarıdan görünmesin)

    public void GonderiAl(Gonderi g)
    {
        // TODO 6
    }

    public void Rapor()
    {
        Console.WriteLine("===== ŞUBE RAPORU =====");
        // TODO 7: döngü, toplam ve son satır
        // Son satır:  $"  {"TOPLAM",-21} {toplam,14:C}   ({adet} gönderi)"
    }

    public void Sorgula(string takipNo)
    {
        // TODO 8
        // Bulunursa:   $"{takipNo} -> {g.Alici}, {g.UcretHesapla():C}"
        // Bulunamazsa: $"{takipNo} -> kayıt bulunamadı"
        Console.WriteLine($"{takipNo} -> ???");
    }

    public void SigortaRaporu()
    {
        Console.WriteLine("\n===== SİGORTA =====");
        // TODO 9
        // Biçim: $"  {g.TakipNo} beyan {s.BeyanBedeli,10:C}  prim {s.SigortaPrimi(),8:C}"
    }
}

// --- Kendinizi değerlendirin ---
//
// Bu problem sınavda 25 puan değerinde olurdu. Ölçütler:
//
//   Soyut sınıf doğru kurulmuş (abstract, private set)          5
//   abstract / virtual seçimi doğru                              5
//   Türetilmiş sınıflar ve base çağrısı doğru                    5
//   Arayüz doğru yerde ve yetenek olarak sorulmuş                5
//   Koleksiyon seçimi doğru ve gerekçeli                         5
//
// Yarım kalan her parça puan getirir. Boş bırakmayın.
