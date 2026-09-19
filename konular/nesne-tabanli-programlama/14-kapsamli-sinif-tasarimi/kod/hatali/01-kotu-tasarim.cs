// HATALI DOSYA — bu kod DERLENİR ve ÇALIŞIR. Sorun tasarımda.
//
// DİKKAT: Burada derleme hatası yok. Aynı ATM'yi nesne tabanlı ilkeleri
// KULLANMADAN yazdık. Program doğru sonucu veriyor. Peki sorun ne?
//
// Nasıl çalışacaksınız:
//   1. Önce çalıştırın; çıktının doğru olduğunu görün
//   2. Sonra dosyanın sonundaki DÖRT GÖREVİ sırayla yapmayı deneyin
//   3. Her görevde kaç satır değiştirdiğinizi not edin
//   4. Aynı görevleri 03-mini-atm.cs üzerinde yapın ve sayıları karşılaştırın
//
// Kötü tasarımın bedeli hatada değil, DEĞİŞİKLİKTE ortaya çıkar.

List<Hesap> hesaplar = new List<Hesap>
{
    new Hesap { HesapNo = 1001, SahipAdi = "Ayşe Kaya",    Bakiye = 1500m, Tur = "vadesiz" },
    new Hesap { HesapNo = 1002, SahipAdi = "Mehmet Demir", Bakiye = 5000m, Tur = "vadeli"  },
};

foreach (Hesap h in hesaplar)
{
    ParaCek(h, 1000m);
    Console.WriteLine($"#{h.HesapNo} {h.SahipAdi,-14} {h.Bakiye,10:C} [{h.Tur}]");
}

// HATA 1: Tür kontrolü zinciri. Polimorfizmin olmadığı yerde hep bu çıkar.
void ParaCek(Hesap h, decimal tutar)
{
    if (h.Tur == "vadesiz")
    {
        h.Bakiye = h.Bakiye - tutar - 2m;
    }
    else if (h.Tur == "vadeli")
    {
        h.Bakiye = h.Bakiye - tutar - (tutar * 0.02m);
    }
    // Yeni bir tür eklenirse? Bu zincire bir dal daha. Peki bu zincirin
    // kaç kopyası var? Rapor, faiz, ekstre... hepsinde aynı if-else.
}


// HATA 2: Her şey public. Bakiye dışarıdan istendiği gibi değiştirilebilir.
// HATA 3: Tür bir METİN. Derleyici "vadesız" yazım hatasını yakalayamaz.
// HATA 4: Davranış yok, yalnızca veri. Sınıf bir kutudan ibaret.
class Hesap
{
    public int HesapNo;
    public string SahipAdi = "";
    public decimal Bakiye;
    public string Tur = "";
}

// --- DÖRT GÖREV ---
//
// Her görevi ÖNCE bu dosyada, SONRA 03-mini-atm.cs dosyasında yapın.
// Değiştirdiğiniz satır sayısını yan yana yazın.
//
// GÖREV 1 — Yeni bir hesap türü ekleyin: öğrenci hesabı, işlem ücreti yok.
//           Bu dosyada: ParaCek zincirine bir dal. Peki rapor, faiz ve
//           ekstre metotlarındaki zincirler? Hepsini bulmanız gerekiyor.
//
// GÖREV 2 — Bakiyenin eksiye düşmesini engelleyin.
//           Bu dosyada kural KAÇ yere yazılmalı? Bakiyeye dokunan her satıra.
//           Birini atlarsanız? Kimse fark etmez.
//
// GÖREV 3 — Tür adını "vadesız" diye yanlış yazın (i yerine ı) ve çalıştırın.
//           Derleyici uyarıyor mu? Program ne yapıyor? Hangi dal çalıştı?
//
// GÖREV 4 — Ana bloğa `hesaplar[0].Bakiye = 1000000m;` yazın.
//           Engelleyen bir şey var mı?
//
// --- Denemeniz için ---
//
// 1. `Tur` alanını `string` yerine `enum HesapTuru { Vadesiz, Vadeli }`
//    yapın. Üçüncü görev artık mümkün mü? Bu, sorunun TAMAMINI çözdü mü?
//
// 2. `ParaCek` metodunu `Hesap` sınıfının içine taşıyın. Zincir hâlâ orada
//    ama en azından tek yerde. Bu yeterli bir tasarım mı? Yeni tür eklemek
//    hâlâ var olan bir metodu DEĞİŞTİRMEYİ gerektiriyor mu?
//
// 3. Bu dosyayı adım adım 03-mini-atm.cs hâline getirin. Hangi adımda
//    if-else zinciri kendiliğinden yok oldu?
