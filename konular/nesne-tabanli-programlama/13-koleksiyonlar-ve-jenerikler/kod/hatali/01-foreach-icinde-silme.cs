// HATALI DOSYA — bu kod DERLENİR ama ÇALIŞIRKEN çöker.
//
// DİKKAT: Bu dosya kasıtlı olarak hatalıdır. Derleyici bir şey söylemez;
// hata çalışma zamanında gelir. Koleksiyonlarla çalışırken en sık düşülen
// tuzak budur.
//
// Nasıl çalışacaksınız:
//   1. Önce kağıt üzerinde tahmin edin: program ne yazar, nerede durur?
//   2. Sonra "dotnet run 01-foreach-icinde-silme.cs" ile çalıştırın
//   3. Hata mesajını okuyun, dosyanın sonundaki üç çözümü deneyin
//
// Cevap anahtarı ders notunda, kapalı bölümde. Önce kendiniz deneyin.

List<string> raf = new List<string>
{
    "Tutunamayanlar",
    "Sefiller",
    "Beyaz Diş",
    "Kuyucaklı Yusuf",
};

Console.WriteLine($"Başlangıç: {raf.Count} kitap\n");

// Adı 'S' ile başlayan kitapları raftan çıkaralım.
foreach (string kitap in raf)
{
    if (kitap.StartsWith("S"))
    {
        raf.Remove(kitap);          // <-- HATA BURADA
    }
}

Console.WriteLine($"Kalan: {raf.Count} kitap");

// --- ÜÇ ÇÖZÜM ---
//
// Her birini sırayla deneyin. Yukarıdaki foreach bloğunu yorum satırı yapıp
// aşağıdakilerden birini açın.

// ÇÖZÜM 1 — Sondan başa doğru indisli döngü.
// Silinen eleman, henüz gezilmemiş indisleri kaydırmaz.
//
// for (int i = raf.Count - 1; i >= 0; i--)
// {
//     if (raf[i].StartsWith("S")) { raf.RemoveAt(i); }
// }

// ÇÖZÜM 2 — Önce topla, sonra sil.
// Gezerken dokunmuyoruz; silme işi döngü bittikten sonra yapılıyor.
//
// List<string> silinecekler = new List<string>();
//
// foreach (string kitap in raf)
// {
//     if (kitap.StartsWith("S")) { silinecekler.Add(kitap); }
// }
//
// foreach (string kitap in silinecekler)
// {
//     raf.Remove(kitap);
// }

// ÇÖZÜM 3 — Koleksiyonun kendi metodu.
// Koşulu sağlayan her elemanı tek satırda siler ve kaç tane sildiğini döndürür.
//
// int silinen = raf.RemoveAll(k => k.StartsWith("S"));
// Console.WriteLine($"{silinen} kitap çıkarıldı.");

// --- Denemeniz için ---
//
// 1. Baştan sona indisli döngü yazın:
//        for (int i = 0; i < raf.Count; i++) { if (...) raf.RemoveAt(i); }
//    Program çökmez ama bir kitabı ATLAR. Hangisini ve neden? (İpucu: bir
//    eleman silindiğinde sonrakiler bir sola kayar, ama `i` yine de artar.)
//
// 2. Listeye 'S' ile başlayan İKİNCİ bir kitap ekleyin ("Şeker Portakalı"
//    olmaz — Türkçe 'Ş' farklı bir harftir; "Saatleri Ayarlama Enstitüsü"
//    deneyin). Hatalı sürüm yine aynı satırda mı çöküyor?
//
// 3. `raf.Remove(kitap)` yerine `raf.Add(kitap + " (kopya)")` yazın.
//    Koleksiyonu gezerken EKLEME yapmak da aynı hatayı veriyor mu?
//
// 4. `List<string>` yerine `string[]` kullanıp aynı şeyi denemeye çalışın.
//    Dizide `Remove` metodu var mı? Neden yok?
