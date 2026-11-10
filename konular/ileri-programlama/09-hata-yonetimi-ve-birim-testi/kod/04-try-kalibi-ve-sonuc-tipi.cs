// Aynı soru, üç cevap: boş yığından eleman istenirse ne olmalı?
//
//   Cikar()          istisna fırlatır      — boş yığından çıkarmak bir HATADIR
//   TryCikar(out x)  false döner           — boş olabilir, olağan bir durumdur
//   CikarSonuc()     Sonuc<T> döner        — başarısızlığın NEDENİ de bilgidir
//
// Üçü de doğru olabilir; hangisinin seçileceği, çağıranın bu durumu ne
// sıklıkta ve nasıl karşılayacağına bağlı. .NET'te ikisi yan yana bulunur:
// int.Parse istisna fırlatır, int.TryParse false döner.
// Sonuc<T> 6. konunun desen eşlemesiyle okunuyor.
// Çalıştırmak için:  dotnet run 04-try-kalibi-ve-sonuc-tipi.cs

var yigin = new Yigin<string>();
yigin.Ekle("Çalıkuşu");

// 1) Try kalıbı: döngü boşalana kadar döner, istisna yok
while (yigin.TryCikar(out var kitap))
    Console.WriteLine($"çıktı: {kitap}");
Console.WriteLine($"TryCikar boş yığında: {yigin.TryCikar(out _)}");

// 2) Sonuç tipi: başarısızlık bir değer olarak döner, nedeni içinde
Console.WriteLine(Anlat(yigin.CikarSonuc()));
yigin.Ekle("Huzur");
Console.WriteLine(Anlat(yigin.CikarSonuc()));

// 3) Aynı ayrım .NET'in kendisinde
Console.WriteLine($"int.TryParse(\"12a\"): {int.TryParse("12a", out int sayi)}, sayi = {sayi}");
try
{
    int.Parse("12a");
}
catch (FormatException)
{
    Console.WriteLine("int.Parse(\"12a\"): FormatException");
}

string Anlat(Sonuc<string> s) => s switch
{
    Basarili<string>(var deger) => $"başarılı: {deger}",
    Hatali<string>(var neden) => $"hatalı: {neden}",
    _ => throw new InvalidOperationException("bilinmeyen sonuç"),
};

abstract record Sonuc<T>;
record Basarili<T>(T Deger) : Sonuc<T>;
record Hatali<T>(string Neden) : Sonuc<T>;

class Yigin<T>
{
    private readonly List<T> _elemanlar = new();

    public int Sayi => _elemanlar.Count;

    public void Ekle(T deger) => _elemanlar.Add(deger);

    public T Cikar()
    {
        if (Sayi == 0) throw new InvalidOperationException("Yığın boş.");
        T deger = _elemanlar[^1];
        _elemanlar.RemoveAt(Sayi - 1);
        return deger;
    }

    public bool TryCikar(out T deger)
    {
        if (Sayi == 0)
        {
            deger = default!;
            return false;
        }
        deger = Cikar();
        return true;
    }

    public Sonuc<T> CikarSonuc()
        => Sayi == 0 ? new Hatali<T>("yığın boş") : new Basarili<T>(Cikar());
}
