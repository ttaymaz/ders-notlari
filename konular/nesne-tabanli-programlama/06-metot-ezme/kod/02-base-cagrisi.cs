// base.Metot(): ezerken üst sınıfın işini de yaptırmak.
//
// 01-ezme-temel.cs dosyasında ortak "durum" mantığı üç kez tekrar
// ediyordu. Ezmek SİLMEK değil, ÜZERİNE EKLEMEK olmalı.
//
// Şema: assets/02-base-cagrisi.svg
// Çalıştırmak için:  dotnet run 02-base-cagrisi.cs

Kitap k = new Kitap(101, "Tutunamayanlar", "A-12", "Oğuz Atay");
Dergi d = new Dergi(102, "Bilim ve Teknik", "B-03", 745);

k.OduncVer();

Console.WriteLine("\n--- Ortak kısım + türe özel ek ---");
k.BilgiYazdir();
d.BilgiYazdir();

Console.WriteLine("\n--- Maaş örneği: kural temelde, ek türetilmişte ---");
Yonetici y = new Yonetici("Ayşe Yılmaz", 60000m, 8000m);
Memur m = new Memur("Mehmet Demir", 42000m, 12);
Console.WriteLine($"{y.AdSoyad,-14} toplam maaş: {y.ToplamMaas(),12:C}");
Console.WriteLine($"{m.AdSoyad,-14} toplam maaş: {m.ToplamMaas(),12:C}");


class Demirbas
{
    public int DemirbasNo { get; private set; }
    public string Baslik { get; private set; }
    public string Raf { get; private set; }

    protected bool oduncVerildi = false;

    public Demirbas(int no, string baslik, string raf)
    {
        DemirbasNo = no;
        Baslik = baslik;
        Raf = raf;
    }

    public void OduncVer()
    {
        oduncVerildi = true;
        Console.WriteLine($"{Baslik}: ödünç verildi.");
    }

    // Ortak kısım BİR KEZ burada.
    public virtual void BilgiYazdir()
    {
        string durum = oduncVerildi ? "ödünçte" : "rafta";
        Console.Write($"#{DemirbasNo} {Baslik} [{Raf}] — {durum}");
    }
}


class Kitap : Demirbas
{
    public string Yazar { get; private set; }

    public Kitap(int no, string baslik, string raf, string yazar)
        : base(no, baslik, raf)
    {
        Yazar = yazar;
    }

    public override void BilgiYazdir()
    {
        // Önce temel sınıfın işini yaptır...
        base.BilgiYazdir();

        // ...sonra kendi ekini koy.
        Console.WriteLine($"  | yazar: {Yazar}");
    }
}


class Dergi : Demirbas
{
    public int Sayi { get; private set; }

    public Dergi(int no, string baslik, string raf, int sayi)
        : base(no, baslik, raf)
    {
        Sayi = sayi;
    }

    public override void BilgiYazdir()
    {
        base.BilgiYazdir();
        Console.WriteLine($"  | sayı: {Sayi}");
    }
}


// --- İKİNCİ ÖRNEK: maaş hesabı ---
// Geçen hafta Yonetici ve Memur sınıflarında birbiriyle ilgisiz iki
// ToplamMaas metodu vardı. Doğrusu bu: metot temel sınıfta, hesabın
// ortak kısmı orada, türe özel ek türetilmişte.
class Personel
{
    public string AdSoyad { get; private set; }
    protected decimal temelMaas;

    public Personel(string adSoyad, decimal temelMaas)
    {
        AdSoyad = adSoyad;
        this.temelMaas = temelMaas;
    }

    public virtual decimal ToplamMaas()
    {
        return temelMaas;
    }
}

class Yonetici : Personel
{
    public decimal Tazminat { get; private set; }

    public Yonetici(string adSoyad, decimal temelMaas, decimal tazminat)
        : base(adSoyad, temelMaas)
    {
        Tazminat = tazminat;
    }

    public override decimal ToplamMaas()
    {
        // base.ToplamMaas() temel maaşı verir; üzerine tazminat eklenir.
        return base.ToplamMaas() + Tazminat;
    }
}

class Memur : Personel
{
    public int MesaiSaati { get; private set; }

    public Memur(string adSoyad, decimal temelMaas, int mesaiSaati)
        : base(adSoyad, temelMaas)
    {
        MesaiSaati = mesaiSaati;
    }

    public override decimal ToplamMaas()
    {
        return base.ToplamMaas() + (MesaiSaati * 250m);
    }
}

// --- NEDEN base ÇAĞIRMAK ÖNEMLİ? ---
//
// Temel maaş hesabına yarın bir kesinti eklendiğini düşünün. Kuralı
// Personel.ToplamMaas içinde değiştirirsiniz — her iki türetilmiş
// sınıf da otomatik uyar, çünkü ikisi de base'i çağırıyor.
//
// base çağırmayıp hesabı baştan yazsalardı, kesintiyi iki yere daha
// eklemeniz gerekirdi. Beşinci haftanın tekrar sorunu geri gelirdi.
//
// --- base HER ZAMAN ÇAĞRILMAZ ---
//
// Türetilmiş sınıf temel sınıfın davranışını GERÇEKTEN tamamen
// değiştirecekse base çağırmaz. Karar ölçütü şu: üst sınıfın yaptığı
// iş hâlâ geçerli mi? Geçerliyse base çağırın, üzerine ekleyin.
