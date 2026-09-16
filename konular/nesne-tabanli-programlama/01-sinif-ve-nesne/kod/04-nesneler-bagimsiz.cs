// Tek sınıf, çok nesne: her nesnenin kendi verisi vardır.
//
// Bu dosyanın tek derdi şu soruyu cevaplamak:
// "Aynı sınıftan üretilen nesneler birbirini etkiler mi?"
//
// Çalıştırmak için:  dotnet run 04-nesneler-bagimsiz.cs

Sayac birinci = new Sayac();
Sayac ikinci = new Sayac();
Sayac ucuncu = new Sayac();

birinci.Ad = "Giriş kapısı";
ikinci.Ad = "Çıkış kapısı";
ucuncu.Ad = "Otopark";

// Yalnızca BİRİNCİ sayacı artırıyoruz.
birinci.Artir();
birinci.Artir();
birinci.Artir();

// İkinciye tek dokunuş.
ikinci.Artir();

// Üçüncüye hiç dokunmadık.

Console.WriteLine("Üç sayacın durumu:");
birinci.Yazdir();
ikinci.Yazdir();
ucuncu.Yazdir();

Console.WriteLine();
Console.WriteLine("Her nesne kendi Deger alanını taşıyor.");
Console.WriteLine("new her çağrıldığında bellekte AYRI bir nesne doğar.");

// Nesneleri bir döngüyle de üretebiliriz.
Console.WriteLine("\n--- Döngüyle üretilen beş sayaç ---");
Sayac[] sayaclar = new Sayac[5];

for (int i = 0; i < sayaclar.Length; i++)
{
    // Dikkat: dizi açmak nesne üretmez. Her hücre için ayrıca new gerekir.
    sayaclar[i] = new Sayac();
    sayaclar[i].Ad = $"{i + 1}. sayaç";

    // i kadar artır: birinci 0, ikinci 1, üçüncü 2 kez...
    for (int j = 0; j < i; j++)
    {
        sayaclar[i].Artir();
    }
}

foreach (Sayac s in sayaclar)
{
    s.Yazdir();
}


class Sayac
{
    public string Ad = "";
    public int Deger = 0;

    public void Artir()
    {
        Deger = Deger + 1;
    }

    public void Yazdir()
    {
        Console.WriteLine($"{Ad,-14} : {Deger}");
    }
}

// --- DENEYİN ---
//
// Dizi hücresini new ile doldurma satırını (sayaclar[i] = new Sayac();)
// yorum satırı yapıp çalıştırın. Program çöker ve şunu der:
// "Object reference not set to an instance of an object."
//
// Sebebi: new Sayac[5] yalnızca BEŞ BOŞ HÜCRE açar, içine nesne koymaz.
// Hücrelerin içi başlangıçta boştur (null). Boş bir hücreye Artir()
// demek, olmayan bir nesneden davranış istemektir.
//
// İki değişken AYNI nesneyi gösterirse ne olur sorusunun cevabı ise
// değer ve referans tiplerini işlediğimiz haftada; orada bu null
// kavramının kökenine de ineceğiz.
