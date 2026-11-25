namespace BasarimLab;

public sealed record Kitap(string Isbn, string Baslik, string Yazar);

// GÖREV 3: Doğru koleksiyonu seçmek.
//
// - Ekle: kitabı ekler. Aynı ISBN ikinci kez eklenirse ArgumentException
// - Bul: ISBN'e göre kitap; yoksa null
// - YazarinKitaplari: o yazarın kitapları, EKLEME SIRASIYLA; yazar yoksa
//   boş liste
//
// Testlerden biri 50 000 kitap ekleyip 50 000 kez Bul ve
// YazarinKitaplari çağırıyor ve bunun 1 saniyeden kısa sürmesini istiyor.
// Her aramada listeyi baştan sona gezen bir çözüm bunu geçemez (04
// dosyasının 2. bölümü).
public sealed class KitapIndeksi
{
    public void Ekle(Kitap kitap)
    {
        throw new NotImplementedException();
    }

    public Kitap? Bul(string isbn)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Kitap> YazarinKitaplari(string yazar)
    {
        throw new NotImplementedException();
    }
}
