// LABORATUVAR — BagliListe<T>'ye davranış parametresi alan metotlar eklemek.
//
// Aşağıdaki BagliListe<T> sınıfında beş metot "TODO" ile işaretli ve şu an
// NotImplementedException fırlatıyor. Sırayla doldurun ve her seferinde
// çalıştırın; dosyanın alt kısmındaki denetim kaç görevin tamam olduğunu
// söyler. Denetim kodunu değiştirmeyin.
//
// Derleyici "Silindi olayı hiçbir zaman kullanılmaz" (CS0067) uyarısı
// veriyorsa doğru: olay henüz hiçbir yerde tetiklenmiyor. TODO 5'te düzelecek.
//
// Çalıştırmak için:  dotnet run bagli-liste-lab.cs
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
        Console.WriteLine($"[HATA] {ad}: {hata.GetType().Name}");
    }
}

BagliListe<Kitap> RafKur()
{
    var raf = new BagliListe<Kitap>();
    raf.Ekle(new Kitap("Çalıkuşu", "Reşat Nuri Güntekin", 1922));
    raf.Ekle(new Kitap("Sinekli Bakkal", "Halide Edib Adıvar", 1936));
    raf.Ekle(new Kitap("Kuyucaklı Yusuf", "Sabahattin Ali", 1937));
    raf.Ekle(new Kitap("İnce Memed", "Yaşar Kemal", 1955));
    return raf;
}

Denetle("1. Say: 1930 sonrası üç kitap",
    () => RafKur().Say(k => k.Yil > 1930) == 3);
Denetle("1. Say: hiçbir kitap eşleşmezse 0",
    () => RafKur().Say(k => k.Yil > 2000) == 0);
Denetle("2. Filtrele: 1930 öncesi tek kitap",
    () => RafKur().Filtrele(k => k.Yil < 1930) is { Sayi: 1 } f
          && f.Bul(_ => true)?.Baslik == "Çalıkuşu");
Denetle("2. Filtrele: asıl liste değişmemeli",
    () => { var r = RafKur(); r.Filtrele(k => false); return r.Sayi == 4; });
Denetle("3. Donustur: başlık uzunlukları",
    () => Metin(RafKur().Donustur(k => k.Baslik.Length)) == "8 14 15 10");
Denetle("3. Donustur: yalnızca yazar adları",
    () => Metin(RafKur().Donustur(k => k.Yazar.Split(' ')[0])) == "Reşat Halide Sabahattin Yaşar");
Denetle("4. Sil: ilk eşleşeni siler",
    () => { var r = RafKur(); return r.Sil(k => k.Yil > 1930) && r.Sayi == 3
                                  && Metin(r.Donustur(k => k.Yil)) == "1922 1937 1955"; });
Denetle("4. Sil: eşleşme yoksa false",
    () => { var r = RafKur(); return !r.Sil(k => k.Yil > 2000) && r.Sayi == 4; });
Denetle("4. Sil: son eleman silinince Ekle hâlâ sona ekler",
    () => { var r = RafKur(); r.Sil(k => k.Yil == 1955); r.Ekle(new Kitap("Huzur", "Ahmet Hamdi Tanpınar", 1949));
            return Metin(r.Donustur(k => k.Yil)) == "1922 1936 1937 1949"; });
Denetle("5. Silindi olayı silinen kitabı bildirir",
    () => { var r = RafKur(); string? silinen = null; r.Silindi += k => silinen = k.Baslik;
            r.Sil(k => k.Yazar.Contains("Ali")); return silinen == "Kuyucaklı Yusuf"; });

Console.WriteLine($"\n{tamam}/{toplam} denetim tamam");

string Metin<T>(BagliListe<T> liste)
{
    var parcalar = new List<string>();
    liste.HerBiri(x => parcalar.Add(x?.ToString() ?? ""));
    return string.Join(" ", parcalar);
}

// ---------------------------------------------------------------- LİSTE
class Dugum<T>(T deger)
{
    public T Deger { get; } = deger;
    public Dugum<T>? Sonraki { get; set; }
}

class BagliListe<T>
{
    private Dugum<T>? _bas;
    private Dugum<T>? _son;

    public int Sayi { get; private set; }

    public event Action<T>? Eklendi;
    public event Action<T>? Silindi;

    public void Ekle(T deger)
    {
        var yeni = new Dugum<T>(deger);
        if (_son is null) _bas = yeni; else _son.Sonraki = yeni;
        _son = yeni;
        Sayi++;
        Eklendi?.Invoke(deger);
    }

    public T? Bul(Func<T, bool> kosul)
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            if (kosul(d.Deger))
                return d.Deger;
        return default;
    }

    public void HerBiri(Action<T> islem)
    {
        for (var d = _bas; d is not null; d = d.Sonraki)
            islem(d.Deger);
    }

    // TODO 1: Koşulu sağlayan eleman sayısını döndürün.
    public int Say(Func<T, bool> kosul)
    {
        throw new NotImplementedException();
    }

    // TODO 2: Koşulu sağlayan elemanlardan YENİ bir liste döndürün.
    //         Asıl liste değişmemeli.
    public BagliListe<T> Filtrele(Func<T, bool> kosul)
    {
        throw new NotImplementedException();
    }

    // TODO 3: Her elemanı dönüştürüp YENİ bir liste döndürün. Dönüşümün
    //         sonucu başka bir tip olabilir: TSonuc bir tip parametresi.
    public BagliListe<TSonuc> Donustur<TSonuc>(Func<T, TSonuc> donusum)
    {
        throw new NotImplementedException();
    }

    // TODO 4: Koşulu sağlayan İLK düğümü listeden çıkarın, true döndürün.
    //         Eşleşme yoksa false. İpucu: önceki düğümü tutarak yürüyün;
    //         silinen ilk veya son düğümse _bas / _son da güncellenmeli.
    // TODO 5: Silme başarılıysa Silindi olayını tetikleyin.
    public bool Sil(Func<T, bool> kosul)
    {
        throw new NotImplementedException();
    }
}

record Kitap(string Baslik, string Yazar, int Yil);
