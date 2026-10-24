// LABORATUVAR — LINQ sorguları ve ifade ağacı.
//
// A bölümü: dört LINQ sorgusu (TODO 1–4).
// B bölümü: ifade ağacına yeni bir düğüm türü (Fark) ve yeni kurallar (TODO 5–8).
// Her TODO şu an NotImplementedException fırlatıyor. Sırayla doldurun ve
// çalıştırın; denetim kaç görevin tamam olduğunu söyler. Denetim kodunu
// değiştirmeyin.
//
// Çalıştırmak için:  dotnet run linq-ve-agac-lab.cs
// .NET 9:            kod/README.md içindeki proje yolu

using System.Globalization;

CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

// ---------------------------------------------------------------- DENETİM
int tamam = 0, toplam = 0;

void Denetle(string ad, Func<bool> sinama)
{
    toplam++;
    try
    {
        bool sonuc = sinama();
        if (sonuc) tamam++;
        Console.WriteLine($"[{(sonuc ? "TAMAM" : "YANLIŞ")}] {ad}");
    }
    catch (NotImplementedException)
    {
        Console.WriteLine($"[YAPILMADI] {ad}");
    }
    catch (Exception hata)
    {
        Console.WriteLine($"[HATA] {ad}: {hata.GetType().Name}: {hata.Message}");
    }
}

var kitaplar = new List<Kitap>
{
    new("Çalıkuşu", "Reşat Nuri Güntekin", 1922),
    new("Yaprak Dökümü", "Reşat Nuri Güntekin", 1930),
    new("Sinekli Bakkal", "Halide Edib Adıvar", 1936),
    new("Kuyucaklı Yusuf", "Sabahattin Ali", 1937),
    new("İçimizdeki Şeytan", "Sabahattin Ali", 1940),
    new("Kürk Mantolu Madonna", "Sabahattin Ali", 1943),
    new("Huzur", "Ahmet Hamdi Tanpınar", 1949),
    new("İnce Memed", "Yaşar Kemal", 1955),
    new("Orta Direk", "Yaşar Kemal", 1960),
    new("Saatleri Ayarlama Enstitüsü", "Ahmet Hamdi Tanpınar", 1961),
};

// (5 - 2) * 4  ve  (x * 0) + 3
Ifade fark = new Carp(new Fark(new Sayi(5), new Sayi(2)), new Sayi(4));
Ifade sifirli = new Topla(new Carp(new Sayi(9), new Sayi(0)), new Sayi(3));

Denetle("1. Yeniler: 1950 sonrası başlıklar, yıla göre",
    () => string.Join(", ", Yeniler(kitaplar)) == "İnce Memed, Orta Direk, Saatleri Ayarlama Enstitüsü");
Denetle("2. YazarBasina: Sabahattin Ali'nin 3 kitabı",
    () => YazarBasina(kitaplar)["Sabahattin Ali"] == 3);
Denetle("2. YazarBasina: 5 yazar",
    () => YazarBasina(kitaplar).Count == 5);
Denetle("3. EnEski: Çalıkuşu",
    () => EnEski(kitaplar) == "Çalıkuşu");
Denetle("4. YazariVarMi: Yaşar Kemal var, Orhan Kemal yok",
    () => YazariVarMi(kitaplar, "Yaşar Kemal") && !YazariVarMi(kitaplar, "Orhan Kemal"));
Denetle("5. Hesapla: (5 - 2) * 4 = 12",
    () => Hesapla(fark) == 12);
Denetle("6. Yazdir: (5 - 2) * 4",
    () => Yazdir(fark) == "(5 - 2) * 4");
Denetle("7. Sadelestir: (9 * 0) + 3 → 3",
    () => Sadelestir(sifirli) is Sayi(3));
Denetle("7. Sadelestir: 0 * x → 0 da çalışmalı",
    () => Sadelestir(new Carp(new Sayi(0), new Sayi(8))) is Sayi(0));
Denetle("8. SayiAdedi: (5 - 2) * 4 içinde 3 sayı",
    () => SayiAdedi(fark) == 3);

Console.WriteLine($"\n{tamam}/{toplam} denetim tamam");

// ---------------------------------------------------------------- A: LINQ

// TODO 1: 1950'den SONRA yayımlanan kitapların başlıkları, yıla göre artan.
IEnumerable<string> Yeniler(IEnumerable<Kitap> k)
{
    throw new NotImplementedException();
}

// TODO 2: Her yazar için kitap sayısı. İpucu: GroupBy, sonra ToDictionary.
Dictionary<string, int> YazarBasina(IEnumerable<Kitap> k)
{
    throw new NotImplementedException();
}

// TODO 3: En eski kitabın başlığı.
string EnEski(IEnumerable<Kitap> k)
{
    throw new NotImplementedException();
}

// TODO 4: Verilen yazarın en az bir kitabı var mı?
bool YazariVarMi(IEnumerable<Kitap> k, string yazar)
{
    throw new NotImplementedException();
}

// ---------------------------------------------------------------- B: AĞAÇ

// TODO 5: Fark (çıkarma) düğümü için bir kol ekleyin.
int Hesapla(Ifade i) => i switch
{
    Sayi s => s.Deger,
    Topla t => Hesapla(t.Sol) + Hesapla(t.Sag),
    Carp c => Hesapla(c.Sol) * Hesapla(c.Sag),
    _ => throw new NotImplementedException($"Hesapla bu düğümü bilmiyor: {i.GetType().Name}"),
};

// TODO 6: Fark için bir kol ekleyin. Fark, Topla gibi parantez içinde yazılır.
string Yazdir(Ifade i) => i switch
{
    Sayi s => s.Deger.ToString(),
    Topla t => $"({Yazdir(t.Sol)} + {Yazdir(t.Sag)})",
    Carp c => $"{Yazdir(c.Sol)} * {Yazdir(c.Sag)}",
    _ => throw new NotImplementedException($"Yazdir bu düğümü bilmiyor: {i.GetType().Name}"),
};

// TODO 7: Sadeleştirme kuralları: x * 0 → 0, 0 * x → 0, x * 1 → x,
//         x + 0 → x, 0 + x → x.
//         İpucu: ÖNCE alt ifadeleri sadeleştirin (Topla, Carp, Fark için yeni
//         düğüm kurarak), SONRA düğümün kendisine kuralları uygulayın. Böylece
//         (9 * 0) + 3 önce 0 + 3'e, sonra 3'e iner. 06-ifade-agaci.cs içindeki
//         Sadelestir yukarıdan aşağı çalıştığı için bunu yapamaz.
Ifade Sadelestir(Ifade i)
{
    throw new NotImplementedException();
}

// TODO 8: Ağaçtaki Sayi düğümlerinin sayısı. Dugumler ve LINQ kullanın.
int SayiAdedi(Ifade i)
{
    throw new NotImplementedException();
}

IEnumerable<Ifade> Dugumler(Ifade i)
{
    yield return i;
    if (i is IkiliIslem(var sol, var sag))
    {
        foreach (var d in Dugumler(sol)) yield return d;
        foreach (var d in Dugumler(sag)) yield return d;
    }
}

record Kitap(string Baslik, string Yazar, int Yil);

abstract record Ifade;
record Sayi(int Deger) : Ifade;
abstract record IkiliIslem(Ifade Sol, Ifade Sag) : Ifade;
record Topla(Ifade Sol, Ifade Sag) : IkiliIslem(Sol, Sag);
record Carp(Ifade Sol, Ifade Sag) : IkiliIslem(Sol, Sag);
record Fark(Ifade Sol, Ifade Sag) : IkiliIslem(Sol, Sag);
