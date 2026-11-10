// Koruma cümleleri: hatalı kullanımı metodun ilk satırında durdurmak.
//
// SinirliYigin, en fazla "kapasite" kadar eleman alan bir yığın. Üç tür
// yanlış kullanım var: geçersiz kapasite, null eleman, dolu yığına ekleme.
// İlk ikisi ÇAĞIRANIN hatasıdır (yanlış argüman), üçüncüsü NESNENİN
// durumudur (argüman doğru, ama şu an eklenemez). İkisi farklı istisna
// ailesinden gelir.
//
// Dene metodu iki aileyi ayrı ayrı yakalıyor. Hangi satır hangi catch'e
// düşer? Çalıştırmadan önce tahmin edin.
// Çalıştırmak için:  dotnet run 02-koruma-cumleleri.cs

Dene("kapasite 0", () => new SinirliYigin<string>(0));
Dene("kapasite 2", () => new SinirliYigin<string>(2));

var yigin = new SinirliYigin<string>(2);
Dene("null ekle", () => yigin.Ekle(null!));
Dene("ilk eleman", () => yigin.Ekle("a"));
Dene("ikinci eleman", () => yigin.Ekle("b"));
Dene("üçüncü eleman", () => yigin.Ekle("c"));
Console.WriteLine($"yığında {yigin.Sayi} eleman var");

void Dene(string ad, Action islem)
{
    try
    {
        islem();
        Console.WriteLine($"{ad,-14} sorun yok");
    }
    catch (ArgumentException e)
    {
        Console.WriteLine($"{ad,-14} {e.GetType().Name} (parametre: {e.ParamName})");
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"{ad,-14} {e.GetType().Name}: {e.Message}");
    }
}

class SinirliYigin<T>
{
    private readonly List<T> _elemanlar = new();
    private readonly int _kapasite;

    public SinirliYigin(int kapasite)
    {
        // Koruma cümlesi: geçersiz argüman nesne hiç kurulmadan reddedilir
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(kapasite);
        _kapasite = kapasite;
    }

    public int Sayi => _elemanlar.Count;

    public void Ekle(T deger)
    {
        ArgumentNullException.ThrowIfNull(deger);
        if (_elemanlar.Count == _kapasite)
            throw new InvalidOperationException("Yığın dolu.");
        _elemanlar.Add(deger);
    }
}
