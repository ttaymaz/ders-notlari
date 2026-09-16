// Pratikte ne fark eder? Gerçek bir yan etki hatası.
//
// Bu dosya, referans tipini anlamadan yazılan kodun nasıl sessizce
// yanlış çalıştığını gösteriyor. Bahar projesinde bu hatayı mutlaka
// yapacaksınız — bir kez burada görmüş olun.
//
// Çalıştırmak için:  dotnet run 04-pratik-yan-etki.cs

Console.WriteLine("=== SENARYO: zam yapmadan önce yedek alalım ===");

Urun[] katalog =
{
    new Urun("Klavye", 750m),
    new Urun("Mouse", 320m),
};

// "Yedek alıyoruz" — ama gerçekten alıyor muyuz?
Urun[] yedek = YedekAl(katalog);

Console.WriteLine("Zamdan önce:");
Yazdir("katalog", katalog);
Yazdir("yedek  ", yedek);

// %50 zam
foreach (Urun u in katalog)
{
    u.Fiyat = u.Fiyat * 1.5m;
}

Console.WriteLine("\nZamdan sonra:");
Yazdir("katalog", katalog);
Yazdir("yedek  ", yedek);
Console.WriteLine("\nYedek de zamlandı! Çünkü aynı nesneleri gösteriyor.");

Console.WriteLine("\n=== DOĞRU YEDEK: nesneleri KOPYALA ===");

Urun[] katalog2 =
{
    new Urun("Klavye", 750m),
    new Urun("Mouse", 320m),
};

Urun[] gercekYedek = DerinYedekAl(katalog2);

foreach (Urun u in katalog2)
{
    u.Fiyat = u.Fiyat * 1.5m;
}

Console.WriteLine("Zamdan sonra:");
Yazdir("katalog", katalog2);
Yazdir("yedek  ", gercekYedek);
Console.WriteLine("\nYedek korundu.");


// YANLIŞ: yeni bir dizi üretiyor ama içindeki nesneler AYNI.
// Buna "sığ kopya" (shallow copy) denir.
Urun[] YedekAl(Urun[] kaynak)
{
    Urun[] hedef = new Urun[kaynak.Length];
    for (int i = 0; i < kaynak.Length; i++)
    {
        hedef[i] = kaynak[i];      // ADRES kopyalanıyor
    }
    return hedef;
}

// DOĞRU: her nesnenin YENİSİNİ üretiyor.
// Buna "derin kopya" (deep copy) denir.
Urun[] DerinYedekAl(Urun[] kaynak)
{
    Urun[] hedef = new Urun[kaynak.Length];
    for (int i = 0; i < kaynak.Length; i++)
    {
        hedef[i] = new Urun(kaynak[i].Ad, kaynak[i].Fiyat);   // YENİ nesne
    }
    return hedef;
}

void Yazdir(string etiket, Urun[] liste)
{
    Console.Write($"  {etiket}: ");
    foreach (Urun u in liste)
    {
        Console.Write($"{u.Ad} {u.Fiyat:C}   ");
    }
    Console.WriteLine();
}


class Urun
{
    public string Ad { get; set; }
    public decimal Fiyat { get; set; }

    public Urun(string ad, decimal fiyat)
    {
        Ad = ad;
        Fiyat = fiyat;
    }
}

// --- NEDEN ÖNEMLİ? ---
//
// Bu hatanın en sinsi yanı, KODUN DOĞRU GÖRÜNMESİ. "Yeni bir dizi
// açtım, elemanları kopyaladım" cümlesi kulağa doğru geliyor.
//
// Ama dizinin içindeki şey nesne değil, nesnenin ADRESİ. Adresi
// kopyalamak nesneyi kopyalamaz.
//
// --- NE ZAMAN DERİN KOPYA GEREKİR? ---
//
// Sığ kopya yeterlidir eğer:
//   - nesneler değişmeyecekse (yalnızca okunacaksa)
//   - zaten aynı nesneleri paylaşmak istiyorsanız
//
// Derin kopya gerekir eğer:
//   - iki liste bağımsız değişecekse
//   - "önceki hâli sakla" gibi bir amacınız varsa
//
// --- BAHAR DÖNEMİ BAĞLANTISI ---
//
// Bir formda "Düzenle" düğmesine basıldığında nesneyi ekrana
// bağlarsınız. Kullanıcı "İptal" derse eski değerlere dönmek
// istersiniz — ama nesneyi doğrudan bağladıysanız eski değerler
// çoktan kaybolmuştur. Çözüm: düzenlemeye başlarken derin kopya almak.
//
// --- DENEYİN ---
//
// DerinYedekAl metodunu Urun sınıfının içine bir Kopyala() metodu
// olarak taşıyın. Hangi tasarım daha iyi? Neden?
