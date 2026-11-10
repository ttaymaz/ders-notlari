// Kendi istisna tipimiz ve catch filtresi (when).
//
// BosYapiException, "boş yapıdan eleman istendi" durumunu taşıyor. Hangi
// yapının boş olduğunu bir özellikte tutuyor; çağıran buna bakıp farklı
// karar verebiliyor. InvalidOperationException'dan türediği için onu
// yakalayan eski kod da çalışmaya devam ediyor.
//
// Calistir metodundaki catch bloklarından hangisinin hangi çağrıda
// çalıştığını izleyin. Dördüncü catch'in gövdesi neden hiç çalışmıyor?
// Yığın ve kuyruk burada kısalık için List<T> üzerine kuruldu; 5. konudaki
// düğümlü sürümleriyle aynı davranırlar.
// Çalıştırmak için:  dotnet run 03-kendi-istisnamiz-ve-filtre.cs

var yigin = new Yigin<int>();
var kuyruk = new Kuyruk<int>();

Calistir("yığından çıkar", () => yigin.Cikar());
Calistir("kuyruktan çıkar", () => kuyruk.Cikar());
Calistir("metni sayıya çevir", () => int.Parse("on iki"));
kuyruk.Ekle(7);
Calistir("kuyruktan çıkar", () => kuyruk.Cikar());

Console.WriteLine();
try
{
    yigin.Cikar();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"Taban tipi yakalayan kod da çalışır: {e.GetType().Name}");
}

void Calistir(string ad, Func<int> islem)
{
    Console.Write($"{ad,-20} → ");
    try
    {
        Console.WriteLine($"değer: {islem()}");
    }
    catch (BosYapiException e) when (e.Yapi == "kuyruk")
    {
        Console.WriteLine("kuyruk boş: biraz bekleyip yeniden dene");
    }
    catch (BosYapiException e)
    {
        Console.WriteLine($"{e.Yapi} boş: işlem iptal");
    }
    catch (Exception e) when (Gunluge(e))
    {
        Console.WriteLine("bu satıra hiç gelinmez");
    }
    catch (FormatException)
    {
        Console.WriteLine("sayı değil");
    }
}

// Filtrede çağrılan metot false döndürür: istisna yakalanmaz, yalnızca
// günlüğe yazılır ve sıradaki catch'lere geçilir.
bool Gunluge(Exception e)
{
    Console.Write($"[günlük: {e.GetType().Name}] ");
    return false;
}

class BosYapiException(string yapi)
    : InvalidOperationException($"{yapi} boş, eleman çıkarılamaz.")
{
    public string Yapi { get; } = yapi;
}

class Yigin<T>
{
    private readonly List<T> _elemanlar = new();

    public void Ekle(T deger) => _elemanlar.Add(deger);

    public T Cikar()
    {
        if (_elemanlar.Count == 0) throw new BosYapiException("yığın");
        T deger = _elemanlar[^1];
        _elemanlar.RemoveAt(_elemanlar.Count - 1);
        return deger;
    }
}

class Kuyruk<T>
{
    private readonly List<T> _elemanlar = new();

    public void Ekle(T deger) => _elemanlar.Add(deger);

    public T Cikar()
    {
        if (_elemanlar.Count == 0) throw new BosYapiException("kuyruk");
        T deger = _elemanlar[0];
        _elemanlar.RemoveAt(0);
        return deger;
    }
}
