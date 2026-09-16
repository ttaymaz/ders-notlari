// SONRA: Aynı program, nesnelerle. Veri ile davranış tek parçada.
//
// 01-prosedurel-ogrenci.cs ile yan yana açıp karşılaştırın:
// çıktı birebir aynı, ama kodun şekli değişti.
//
// Şema: assets/01-prosedurel-vs-nesne.svg (alt kutu)
// Çalıştırmak için:  dotnet run 02-nesne-ogrenci.cs

// Artık üç dizi yok. Her öğrenci TEK bir nesne.
Ogrenci ogr1 = new Ogrenci();
ogr1.Ad = "Ayşe Yılmaz";
ogr1.Vize = 70;
ogr1.Final = 80;

Ogrenci ogr2 = new Ogrenci();
ogr2.Ad = "Mehmet Demir";
ogr2.Vize = 45;
ogr2.Final = 40;

Ogrenci ogr3 = new Ogrenci();
ogr3.Ad = "Zeynep Kaya";
ogr3.Vize = 88;
ogr3.Final = 92;

// Nesneleri tek bir dizide toplayabiliriz. Dikkat: bu dizi ARTIK
// senkron tutulması gereken üç diziden biri değil — tek dizi,
// içinde bütün halinde öğrenciler var.
Ogrenci[] sinif = { ogr1, ogr2, ogr3 };

Console.WriteLine($"{"Ad Soyad",-22} {"Vize",4} {"Final",5} {"Ort.",6}   Durum");
Console.WriteLine(new string('-', 52));

foreach (Ogrenci ogrenci in sinif)
{
    // Metoda parametre GÖNDERMİYORUZ. Nesne kendi verisini zaten biliyor.
    ogrenci.SatirYazdir();
}


// --- SINIF: nesnelerin kalıbı ---
// Bir sınıf, "bu tipteki her nesnede hangi bilgiler ve hangi davranışlar
// olacak" sorusunun cevabıdır. Kendisi bellekte yer kaplamaz; new ile
// ondan üretilen nesneler kaplar.
class Ogrenci
{
    // ALANLAR (fields) — nesnenin taşıdığı veri.
    // public: şimdilik dışarıdan serbestçe okunup yazılabilir.
    // Bunun neden tehlikeli olduğunu ve nasıl kapatacağımızı
    // kapsülleme haftasında göreceğiz.
    public string Ad = "";
    public int Vize;
    public int Final;

    // METOTLAR — nesnenin davranışı.
    // Parametre almıyorlar: ihtiyaç duydukları veri zaten kendi içlerinde.
    public double Ortalama()
    {
        return Vize * 0.4 + Final * 0.6;
    }

    public string Durum()
    {
        return Ortalama() >= 50 ? "Geçti" : "Kaldı";
    }

    public void SatirYazdir()
    {
        Console.WriteLine($"{Ad,-22} {Vize,4} {Final,5} {Ortalama(),6:F1}   {Durum()}");
    }
}

// --- NE KAZANDIK? ---
//
// 1. SENKRON DERDİ BİTTİ. Bir öğrenciyi silmek = bir nesneyi çıkarmak.
//    Adı ile notunun ayrışması artık mümkün değil.
//
// 2. YENİ BİLGİ EKLEMEK TEK YERE DOKUNMAK.
//    Bölüm bilgisi eklemek için sınıfa tek satır: public string Bolum = "";
//    Hiçbir metot imzası değişmiyor.
//
// 3. DAVRANIŞ VERİNİN YANINDA. Ortalama nasıl hesaplanır sorusunun
//    cevabı tek bir yerde. Formül değişirse tek satır değişir.
