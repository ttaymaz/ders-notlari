namespace DiLab;

// Derste yazdığımız küçük DI kabı. Kaydet ve Coz (döngü tespitiyle) hazır.
// Sizden üç görev isteniyor: GÖREV 1, 2 ve 3. Testleri
// DiLab.Testler/KapTestleri.cs dosyasında; testleri değiştirmeyin.
public sealed class Kap
{
    private readonly Dictionary<Type, Func<Kap, object>> _fabrikalar = new();

    // Şu an kurulmakta olan tipler: grafta "yolda olduğumuz" düğümler
    private readonly List<Type> _yol = new();

    // HAZIR — geçici (transient) kayıt: her Coz çağrısında fabrika yeniden
    // çalışır ve yeni bir nesne döner.
    public void Kaydet<T>(Func<Kap, T> fabrika) where T : class
    {
        ArgumentNullException.ThrowIfNull(fabrika);
        _fabrikalar[typeof(T)] = k => fabrika(k);
    }

    // GÖREV 1 — tekil (singleton) kayıt: fabrika yalnızca İLK Coz'da çalışır;
    // sonraki her Coz aynı nesneyi döndürür.
    // İpucu: Kaydet'teki gibi bir lambda saklayın; lambda, dışarıda tanımlı
    // bir değişkeni yakalayabilir (4. hafta: kapanım).
    public void KaydetTekil<T>(Func<Kap, T> fabrika) where T : class
    {
        throw new NotImplementedException();
    }

    public T Coz<T>() where T : class => (T)Coz(typeof(T));

    private object Coz(Type tip)
    {
        if (!_fabrikalar.TryGetValue(tip, out var fabrika))
        {
            // GÖREV 2 — kayıtsız tip için InvalidOperationException fırlatın.
            // Mesaj:
            //   doğrudan istendiyse          "Fatura kayıtlı değil."
            //   bir servis kurulurken istendiyse "Fatura kayıtlı değil (Hatirlatici istiyor)."
            // İsteyen, _yol listesinin SON elemanıdır. Tip adı için tip.Name.
            throw new NotImplementedException();
        }

        // HAZIR — yolda olduğumuz bir düğüme yeniden geldiysek graf döngülüdür
        if (_yol.Contains(tip))
        {
            var dongu = _yol.SkipWhile(t => t != tip).Append(tip).Select(t => t.Name);
            throw new InvalidOperationException("Döngüsel bağımlılık: " + string.Join(" → ", dongu));
        }

        _yol.Add(tip);
        try
        {
            return fabrika(this);
        }
        finally
        {
            _yol.RemoveAt(_yol.Count - 1);
        }
    }

    // GÖREV 3 — kayıtlı her tipi bir kez çözmeyi deneyin ve karşılaşılan
    // InvalidOperationException mesajlarını, kayıt sırasıyla bir listede
    // döndürün. Sorun yoksa boş liste. Hazır kaptaki ValidateOnBuild
    // seçeneğinin küçük karşılığı: hatayı program çalışırken değil, kap
    // kurulur kurulmaz görmek.
    public IReadOnlyList<string> Dogrula()
    {
        throw new NotImplementedException();
    }
}
