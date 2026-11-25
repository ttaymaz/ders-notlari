using System.Collections.Concurrent;

namespace EsLab;

// HAZIR: lock ile korunan bir sayaç. Testleri baştan yeşildir.
// Öteki görevlerde örnek olarak kullanın.
public sealed class KilitliSayac
{
    private readonly object _kilit = new();
    private int _deger;

    public int Deger
    {
        get { lock (_kilit) return _deger; }
    }

    public void Artir()
    {
        lock (_kilit)
            _deger++;
    }
}

// GÖREV 1: Kitap başına ödünç sayısı.
//
// Kaydet aynı anda birçok görevden çağrılacak; hiçbir kayıt kaybolmamalı.
// Sayi, hiç kaydedilmemiş bir kitap için 0 döndürür.
// EnCokOkunanlar, en çok ödünç alınan n kitabı sayıya göre azalan, eşit
// sayıda başlığa göre (StringComparer.Ordinal) artan sırayla verir.
//
// İpucu: ConcurrentDictionary<string, int> ve AddOrUpdate (03 dosyası).
// "Önce oku, sonra yaz" iki ayrı işlemdir; arası korunmaz.
public sealed class OduncIstatistigi
{
    private readonly ConcurrentDictionary<string, int> _sayilar = new();

    public void Kaydet(string kitap)
    {
        throw new NotImplementedException();
    }

    public int Sayi(string kitap)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<(string Kitap, int Sayi)> EnCokOkunanlar(int n)
    {
        throw new NotImplementedException();
    }
}
