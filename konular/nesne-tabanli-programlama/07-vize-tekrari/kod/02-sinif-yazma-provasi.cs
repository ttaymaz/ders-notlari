// BÖLÜM B PROVASI — sınıf yazma.
//
// Sınavın ikinci bölümü bu biçimde olacak: bir problem verilir, siz
// sınıfı C# olarak yazarsınız. Kısmi puan verilir — yarım kalan çözüm
// de puan alır.
//
// Bu dosya ÇALIŞIR ama sınıflar EKSİKTİR. Görev sizin.
// Çözümü görmeden önce kendiniz deneyin; çözüm 03-karma-cozum.cs içinde.
//
// Çalıştırmak için:  dotnet run 02-sinif-yazma-provasi.cs
//
// =====================================================================
// SORU
// =====================================================================
// Bir spor salonu üyelik sistemi için sınıfları yazınız.
//
// 1) Uye sınıfı
//    - AdSoyad: dışarıdan okunabilsin, yalnızca kurucuda atansın
//    - UyeNo: her üyeye otomatik verilsin (1000'den başlayıp artsın),
//      dışarıdan değiştirilemesin
//    - AylikUcret: 0'dan küçük değer KABUL EDİLMESİN (eski değer kalsın)
//    - ToplamBorc(int ay): ay sayısı kadar aylık ücreti döndürsün
//    - ToString(): "1000 - Ayşe Yılmaz" biçiminde
//
// 2) OgrenciUye sınıfı (Uye sınıfından türesin)
//    - OkulAdi alanı olsun
//    - ToplamBorc metodunu EZSİN: normal borcun %40 indirimlisini
//      döndürsün (temel sınıfın hesabını yeniden yazmayın, çağırın)
//    - ToString(): "1001 - Mehmet Demir (öğrenci)" biçiminde
//
// PUANLAMA (20 puan)
//    kapsülleme ve kurallı özellik ......... 5
//    kurucu + this + statik sayaç .......... 5
//    kalıtım ve base kullanımı ............. 5
//    ezme (override) ve ToString ........... 5
// =====================================================================

Uye u1 = new Uye("Ayşe Yılmaz", 800m);
OgrenciUye u2 = new OgrenciUye("Mehmet Demir", 800m, "Sinanpaşa MYO");

Console.WriteLine("--- Üyeler ---");
Console.WriteLine(u1);
Console.WriteLine(u2);

Console.WriteLine("\n--- 3 aylık borç ---");
Console.WriteLine($"{u1.AdSoyad,-14}: {u1.ToplamBorc(3),10:C}");
Console.WriteLine($"{u2.AdSoyad,-14}: {u2.ToplamBorc(3),10:C}");

Console.WriteLine("\n--- Geçersiz ücret denemesi ---");
u1.AylikUcret = -500m;
Console.WriteLine($"ücret hâlâ: {u1.AylikUcret:C}");

Console.WriteLine("\n--- Kaç üye üretildi? ---");
Console.WriteLine($"toplam: {Uye.UyeSayisi}");


// =====================================================================
// BURADAN AŞAĞISINI SİZ TAMAMLAYACAKSINIZ
// Aşağıdaki iskelet derlenir ama doğru çalışmaz.
// =====================================================================

class Uye
{
    // TODO 1: AylikUcret için private alan ve kurallı özellik yazın.
    //         0'dan küçük değer reddedilmeli.
    public decimal AylikUcret { get; set; }

    // TODO 2: UyeNo otomatik verilsin. Statik bir sayaç gerekiyor.
    //         Dışarıdan değiştirilememeli.
    public int UyeNo { get; set; }

    // TODO 3: UyeSayisi — kaç üye üretildiğini söyleyen statik üye.
    public static int UyeSayisi { get; private set; }

    public string AdSoyad { get; private set; }

    public Uye(string adSoyad, decimal aylikUcret)
    {
        // TODO 4: this kullanarak alanları doldurun, sayacı artırın,
        //         UyeNo'yu üretin.
        AdSoyad = adSoyad;
        AylikUcret = aylikUcret;
    }

    // TODO 5: ay sayısı kadar aylık ücreti döndürün.
    //         Türetilmiş sınıfın ezebilmesi için ne gerekiyor?
    public decimal ToplamBorc(int ay)
    {
        return 0m;
    }

    // TODO 6: "1001 - Ayşe Yılmaz" biçiminde döndürün.
}


// TODO 7: OgrenciUye sınıfını Uye'den türetin.
//         Şimdilik Uye'nin kopyası gibi duruyor; düzeltin.
class OgrenciUye
{
    public string OkulAdi { get; private set; }
    public string AdSoyad { get; private set; }
    public decimal AylikUcret { get; set; }

    public OgrenciUye(string adSoyad, decimal aylikUcret, string okulAdi)
    {
        AdSoyad = adSoyad;
        AylikUcret = aylikUcret;
        OkulAdi = okulAdi;
    }

    public decimal ToplamBorc(int ay)
    {
        return 0m;
    }
}

// --- İPUÇLARI ---
//
// - Şema: assets/02-sinif-tasarim-adimlari.svg sırayı gösteriyor.
// - "Temel sınıfın hesabını yeniden yazmayın, çağırın" cümlesi hangi
//   anahtar kelimeyi işaret ediyor?
// - Otomatik numara üretmek için sayacın NESNEYE mi SINIFA mı ait
//   olması gerekir?
// - ToplamBorc metodunu ezebilmek için temel sınıfta ne yazmalısınız?
//
// Bitirdiğinizde çıktınız şu olmalı:
//
//   1000 - Ayşe Yılmaz
//   1001 - Mehmet Demir (öğrenci)
//   Ayşe Yılmaz   :  ₺2.400,00
//   Mehmet Demir  :  ₺1.440,00
//   ücret hâlâ: ₺800,00
//   toplam: 2
