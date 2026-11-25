namespace YansimaLab;

// GÖREV 2: Komut tablosu.
//
// Kurucu, hedef nesnenin [Komut] etiketli public metotlarını bulur.
// Komutlar: komut adları, sıralı (StringComparer.Ordinal).
// Calistir bir komut satırı alır: "ekle Huzur 3" → komut "ekle",
// argümanlar "Huzur" ve "3" (boşlukla ayrılmış). Argümanlar metodun
// parametre tiplerine çevrilir (Convert.ChangeType, InvariantCulture) ve
// metot çağrılır. Dönüş değeri Convert.ToString(deger, InvariantCulture)
// ile metne çevrilip döndürülür (void ise ""). Kültürden bağımsız: 3.75,
// 3,75 değil.
//
// Hatalar istisna fırlatmaz, metin olarak döner:
//   "bilinmeyen komut: sil"
//   "ekle 2 argüman bekliyor, 1 verildi"
//   "'üç' Int32 değil"
//   "hata: <metodun fırlattığı istisnanın mesajı>"   (TargetInvocationException değil!)
//
// İpucu: 01 dosyasının 5. bölümü ve 02 dosyası.
public sealed class KomutTablosu
{
    public KomutTablosu(object hedef)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<string> Komutlar => throw new NotImplementedException();

    public string Calistir(string satir)
    {
        throw new NotImplementedException();
    }
}
