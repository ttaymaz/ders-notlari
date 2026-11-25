namespace YansimaLab;

// GÖREV 4: İki nesnenin farkı.
//
// Aynı tipten iki nesnenin public örnek özelliklerini tanımlandıkları
// sırayla karşılaştırın; değeri farklı olanlar için bir satır yazın:
//
//   "Uye: Ece → Can"
//   "Not: (yok) → geç iade"           null değer "(yok)" yazılır
//
// Değerler object.Equals(a, b) ile karşılaştırılır; metne çevrilirken
// Convert.ToString(deger, CultureInfo.InvariantCulture) kullanılır.
// Fark yoksa boş liste. eski ya da yeni null ise ArgumentNullException.
//
// Bir kaydın iki sürümü arasında ne değiştiğini günlüğe yazmak için
// kullanılabilir: "Odunc güncellendi: GecikmeGun: 0 → 3".
public static class Fark
{
    public static IReadOnlyList<string> Bul<T>(T eski, T yeni) where T : class
    {
        throw new NotImplementedException();
    }
}
