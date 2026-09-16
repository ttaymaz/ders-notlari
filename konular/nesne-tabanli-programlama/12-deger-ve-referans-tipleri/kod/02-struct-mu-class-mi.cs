// struct mu, class mı? Karar ölçütü ve struct'ın tuzağı.
//
// Şema: assets/02-struct-mu-class-mi.svg
// Çalıştırmak için:  dotnet run 02-struct-mu-class-mi.cs

Console.WriteLine("=== struct'ın DOĞRU kullanımı: tek bir değer ===");
Nokta baslangic = new Nokta(0, 0);
Nokta hedef = new Nokta(3, 4);
Console.WriteLine($"Başlangıç: {baslangic}");
Console.WriteLine($"Hedef    : {hedef}");
Console.WriteLine($"Uzaklık  : {baslangic.Uzaklik(hedef):F2}");

Console.WriteLine("\n=== struct kopyalanır: yan etki YOK ===");
Nokta kopya = hedef;
kopya = new Nokta(99, 99);
Console.WriteLine($"hedef hâlâ: {hedef}");

Console.WriteLine("\n=== TUZAK: değiştirilebilir struct ===");
Ayar[] ayarlar = { new Ayar("ses", 5), new Ayar("parlaklik", 7) };

// Bu satır DERLENMEZ — dizi elemanı üzerinden struct alanı değiştirilemez
// gibi görünse de, aslında burada çalışır ve BEKLENEN etkiyi yapar:
ayarlar[0].Deger = 10;
Console.WriteLine($"Dizi elemanı: {ayarlar[0]}");

// Ama foreach değişkeni bir KOPYADIR — değiştirmeye çalışmak derlenmez.
// Aşağıdaki satırların yorumunu kaldırın:
//
// foreach (Ayar a in ayarlar)
// {
//     a.Deger = 0;      // DERLEME HATASI: foreach değişkeni salt okunur
// }
Console.WriteLine("foreach değişkeni salt okunurdur — kopyayı değiştirmenin anlamı yok.");

Console.WriteLine("\n=== Aynı işi class ile yapmak ===");
AyarSinif[] ayarlar2 = { new AyarSinif("ses", 5) };
foreach (AyarSinif a in ayarlar2)
{
    a.Deger = 99;      // class'ta çalışır — nesneye erişiyoruz
}
Console.WriteLine($"class ile: {ayarlar2[0]}");


// DOĞRU struct: küçük, tek bir değeri temsil ediyor, değişmez.
// Alanlar yalnızca kurucuda atanıyor (readonly benzeri davranış).
struct Nokta
{
    public int X { get; }
    public int Y { get; }

    public Nokta(int x, int y)
    {
        X = x;
        Y = y;
    }

    public double Uzaklik(Nokta digeri)
    {
        int dx = X - digeri.X;
        int dy = Y - digeri.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}


// TUZAKLI struct: değiştirilebilir alanı var.
// Çalışır ama sürprizler üretir; bu yüzden önerilmez.
struct Ayar
{
    public string Ad { get; set; }
    public int Deger { get; set; }

    public Ayar(string ad, int deger)
    {
        Ad = ad;
        Deger = deger;
    }

    public override string ToString()
    {
        return $"{Ad} = {Deger}";
    }
}


class AyarSinif
{
    public string Ad { get; set; }
    public int Deger { get; set; }

    public AyarSinif(string ad, int deger)
    {
        Ad = ad;
        Deger = deger;
    }

    public override string ToString()
    {
        return $"{Ad} = {Deger}";
    }
}

// --- KARAR ÖLÇÜTÜ ---
//
// struct seçin, EĞER üçü birden doğruysa:
//   1. Tek bir DEĞERİ temsil ediyor (nokta, para, tarih, renk)
//   2. Küçük (kabaca 16 bayt altı)
//   3. Üretildikten sonra DEĞİŞMEYECEK
//
// Aksi hâlde class seçin.
//
// ŞÜPHEDEYSENİZ class SEÇİN. struct dar bir durum için vardır ve
// yanlış kullanıldığında bulunması zor hatalar üretir.
//
// --- NEDEN DEĞİŞTİRİLEBİLİR struct KÖTÜ? ---
//
// struct kopyalanarak taşınır. Bir struct'ı metoda gönderdiğinizde,
// bir listeye koyduğunuzda veya foreach ile gezdiğinizde KOPYASI
// üzerinde çalışırsınız. Kopyayı değiştirmek aslını değiştirmez ve
// bu, "değiştirdim ama olmadı" hatalarına yol açar.
//
// Değişmez (immutable) struct'ta bu sorun hiç doğmaz: zaten
// değiştirmezsiniz, yenisini üretirsiniz.
//
// --- .NET'TEKİ ÖRNEKLER ---
//
//   struct : int, double, bool, DateTime, TimeSpan, Guid
//   class  : string, dizi, List<T>, Random, sizin sınıflarınız
//
// DateTime bir struct'tır ve değişmezdir: date.AddDays(1) tarihi
// değiştirmez, YENİ bir tarih döndürür.
