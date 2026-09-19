// FİNAL HATA AVI — bu dosyada 8 hata var.
//
// DİKKAT: Bu dosya KASITLI OLARAK hatalıdır ve derlenmez.
// Hataların bir kısmı derleme, bir kısmı MANTIK ve TASARIM hatasıdır.
//
// Sınavın 3. sorusu bu biçimdedir: verilen kodda hataları bulup
// hangi satırda olduğunu, türünü ve doğrusunu yazacaksınız.
// (Sınavda bu kod çıkmayacaktır; format aynı, kod farklı olacak.)
//
// Nasıl çalışacaksınız:
//   1. Önce kağıt üzerinde bulmaya çalışın
//   2. Sonra derleyin, hata mesajlarını okuyun
//   3. Tek tek düzeltin, program doğru çalışana kadar devam edin
//
// Cevap anahtarı ders notunda, kapalı bölümde.

List<Urun> sepet = new List<Urun>();

sepet.Add(new Kitap("Tutunamayanlar", 180m, 724));
sepet.Add(new Abonelik("Dergi yıllık", 900m));

decimal toplam;

foreach (Urun u in sepet)
{
    toplam += u.Fiyat;
    Console.WriteLine(u.Etiket())
}

Console.WriteLine($"Toplam: {toplam:C}");

// İndirimli ürünleri sepetten çıkaralım.
foreach (Urun u in sepet)
{
    if (u is IIndirimli)
    {
        sepet.Remove(u);
    }
}

sepet[0].Fiyat = 0m;

Console.WriteLine($"Kalan: {sepet.Count}");


interface IIndirimli
{
    decimal IndirimOrani { get; }
}


abstract class Urun
{
    public string Ad { get; set; }
    public decimal Fiyat { get; set; }

    public Urun(string ad, decimal fiyat)
    {
        Ad = ad;
        Fiyat = fiyat;
    }

    public abstract string Etiket()
    {
        return $"{Ad} - {Fiyat:C}";
    }
}


class Kitap : Urun
{
    public int SayfaSayisi { get; private set; }

    public Kitap(string ad, decimal fiyat, int sayfa)
    {
        SayfaSayisi = sayfa;
    }

    public override string Etiket()
    {
        return $"{Ad} - {Fiyat:C} ({SayfaSayisi} s.)";
    }
}


class Abonelik : Urun, IIndirimli
{
    public Abonelik(string ad, decimal fiyat) : base(ad, fiyat) { }

    public string Etiket()
    {
        return $"{Ad} - {Fiyat:C} [abonelik]";
    }
}

// --- Denemeniz için ---
//
// 1. Hataları bulduktan sonra sekizini sınıflandırın: kaçı derleme,
//    kaçı mantık, kaçı tasarım hatası? Derleyici kaçını buldu?
//
// 2. Düzeltilmiş sürümü çalıştırın ve çıktıyı 03-prova-cozumu.cs
//    dosyasındaki biçimle karşılaştırın.
//
// 3. `sepet[0].Fiyat = 0m;` satırı neden mümkün oldu? Bunu engellemek
//    için hangi tek kelimeyi değiştirmek yeterli?
//
// 4. `Abonelik` sınıfındaki `Etiket` metodu derleniyor mu? Derleniyorsa
//    neden yanlış? (İpucu: derleyici uyarı veriyor — CS0114.)
