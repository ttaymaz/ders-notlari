using System.Reflection;

namespace YansimaLab;

public enum SinamaDurumu { Gecti, Kaldi, Atlandi }

public sealed record SinamaSonucu(string Ad, SinamaDurumu Durum, string Mesaj);

// GÖREV 3: Kendi test koşucumuzun asenkron sürümü.
//
// 03 dosyasındaki koşucuyu genişletin:
// - Ad: "Sinif.Metot"; sonuçlar Ad'a göre sıralı (StringComparer.Ordinal)
// - Her sınama için sınıfın YENİ bir nesnesi kurulur
// - Metot Task döndürüyorsa beklenir (await). Asenkron bir sınamanın
//   hatası da, senkron olanınki gibi, asıl istisnanın mesajıyla raporlanır
// - [Atla("neden")] taşıyan sınama çalıştırılmaz: Durum Atlandi, Mesaj neden
// - Geçen sınamanın Mesaj'ı ""
public static class TestKosucu
{
    public static Task<IReadOnlyList<SinamaSonucu>> KosAsync(Assembly derleme)
    {
        throw new NotImplementedException();
    }
}
