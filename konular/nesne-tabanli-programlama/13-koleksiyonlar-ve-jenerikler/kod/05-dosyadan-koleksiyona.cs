// Dosyadan koleksiyona: metin satırlarını nesnelere çevirmek.
//
// Program kapandığında bellekteki her şey silinir. Demirbaş listesi bir
// dosyada durursa program her açıldığında onu geri kurabilir.
//
// Bu dosyanın asıl konusu okumak değil, ÇEVİRMEK: elinizdeki metin satırını
// hangi sınıfın nesnesine dönüştüreceğinize nasıl karar verirsiniz?
//
// Çalıştırmak için:  dotnet run 05-dosyadan-koleksiyona.cs

string yol = "demirbas.txt";

// Dosya yoksa örnek bir tane üretiyoruz; böylece bu dosya tek başına çalışır.
// Son iki satır kasıtlı olarak bozuk — ayrıştırıcının onları ne yaptığına bakın.
if (!File.Exists(yol))
{
    File.WriteAllLines(yol, new string[]
    {
        "# tur;no;baslik;ek",
        "K;101;Tutunamayanlar;Oğuz Atay",
        "K;102;Sefiller;Victor Hugo",
        "D;201;Bilim ve Teknik;745",
        "",
        "K;103;Beyaz Diş;Jack London",
        "D;202;Arkitekt;512",
        "K;abc;Numarası Bozuk Kitap;Yazar",
        "X;301;Tanınmayan Tür;ek",
    });

    Console.WriteLine($"{yol} oluşturuldu.");
}

Console.WriteLine($"Okunan dosya: {Path.GetFullPath(yol)}\n");

string[] satirlar = File.ReadAllLines(yol);

// Kaç satır geleceğini bilmiyoruz. Dizi burada işe yaramazdı.
List<Demirbas> koleksiyon = new List<Demirbas>();
int atlanan = 0;

foreach (string ham in satirlar)
{
    string satir = ham.Trim();

    // Boş satır ve yorum satırı veri değildir.
    if (satir.Length == 0 || satir.StartsWith("#"))
    {
        continue;
    }

    Demirbas? d = SatiriCevir(satir);

    if (d is null)
    {
        atlanan++;
        continue;
    }

    koleksiyon.Add(d);
}

Console.WriteLine($"\n{koleksiyon.Count} demirbaş okundu, {atlanan} satır atlandı.\n");

// Rapor tek döngü. Satırın biçimini nesnenin kendi türü belirliyor.
foreach (Demirbas d in koleksiyon)
{
    Console.WriteLine($"  {d.Etiket()}");
}


// Bir satırı doğru sınıfın nesnesine çevirir. Çeviremezse null döndürür.
//
// Dönüş tipi Demirbas — ama üretilen nesne Kitap ya da Dergi oluyor.
// Kalıtım olmasaydı bu metodu yazamazdık: iki ayrı tür döndürmek zorunda
// kalırdık ve tek bir listede toplayamazdık.
static Demirbas? SatiriCevir(string satir)
{
    string[] alan = satir.Split(';');

    // Alan sayısını ÖNCE doğrulayın. alan[3] yazmadan önce dört alan
    // olduğundan emin olmazsanız kısa bir satır programı çökertir.
    if (alan.Length != 4)
    {
        Console.WriteLine($"  ! alan sayısı 4 değil  : {satir}");
        return null;
    }

    // Parse değil TryParse: bozuk veri istisna değil, karar üretmeli.
    if (!int.TryParse(alan[1].Trim(), out int no))
    {
        Console.WriteLine($"  ! numara sayı değil    : {satir}");
        return null;
    }

    string baslik = alan[2].Trim();

    // Tür kodu hangi sınıfın kurulacağını belirler.
    switch (alan[0].Trim().ToUpper())
    {
        case "K":
            return new Kitap(no, baslik, alan[3].Trim());

        case "D":
            if (!int.TryParse(alan[3].Trim(), out int sayi))
            {
                Console.WriteLine($"  ! sayı okunamadı       : {satir}");
                return null;
            }
            return new Dergi(no, baslik, sayi);

        default:
            Console.WriteLine($"  ! bilinmeyen tür kodu  : {satir}");
            return null;
    }
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
// 1. demirbas.txt dosyasını bir metin düzenleyicide açın, yeni bir satır
//    ekleyin ve programı tekrar çalıştırın. Kodda tek bir satır bile
//    değiştirmediniz — koleksiyon yine de büyüdü. Dizi kullansaydınız
//    boyutu nereden bilecektiniz?
//
// 2. Bozuk iki satırı silin. Atlanan sayısı sıfıra indi mi?
//
// 3. TryParse yerine int.Parse yazın: "abc" satırında program çöker.
//    Hangisi daha iyi — çökmek mi, satırı atlayıp devam etmek mi? Cevap
//    duruma bağlı: bir muhasebe dosyasında sessizce atlamak tehlikelidir.
//
// 4. SatiriCevir metodunun dönüş tipini Kitap yapın. Hangi satır derlenmez?
//    Neden temel sınıf tipinde döndürmek zorundayız?
//
// 5. Dosyaya "T;401;Tez;Ahmet Yılmaz" satırı ekleyin. Program onu atlıyor.
//    Tez adında yeni bir sınıf yazıp switch'e bir case ekleyin. Raporu
//    yazan foreach döngüsünde kaç satır değiştirmeniz gerekti?
//
// 6. Path.GetFullPath satırındaki yolu okuyun. Dosya projenin klasöründe
//    değil, programın ÇALIŞTIĞI klasörde oluşuyor. Neden?
