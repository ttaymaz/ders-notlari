// ÇÖZÜM: Özellikler (properties) — alan gibi kullanılır, metot gibi çalışır.
//
// Şema: assets/02-ozellik-akisi.svg
// Çalıştırmak için:  dotnet run 03-ozellikler.cs

Ogrenci ogr = new Ogrenci("Ayşe Yılmaz");

Console.WriteLine("--- Geçerli değerler ---");
ogr.Vize = 70;
ogr.Final = 80;
Console.WriteLine($"{ogr.Ad}: vize {ogr.Vize}, final {ogr.Final}, ortalama {ogr.Ortalama():F1}");

Console.WriteLine("\n--- Geçersiz değer denemesi ---");
ogr.Vize = 150;               // set bloğu reddediyor
Console.WriteLine($"Vize hâlâ: {ogr.Vize}");

ogr.Vize = -20;               // bu da reddediliyor
Console.WriteLine($"Vize hâlâ: {ogr.Vize}");

Console.WriteLine("\n--- Yalnızca okunabilen özellik ---");
Console.WriteLine($"Öğrenci numarası: {ogr.Numara}");
// Aşağıdaki satırın yorumunu kaldırın: DERLENMEZ.
// ogr.Numara = 999;
//
// Hata: The property or indexer 'Ogrenci.Numara' cannot be used in this
// context because the set accessor is inaccessible

Console.WriteLine("\n--- Hesaplanan özellik ---");
Console.WriteLine($"Durum: {ogr.Durum}");


class Ogrenci
{
    // Özelliğin arkasındaki gerçek depo. private: dışarıdan görünmez.
    private int vize;
    private int final;

    // 1) OTOMATİK ÖZELLİK (auto-property)
    //    Kontrol gerekmiyorsa en kısa yol. C# arkadaki alanı kendisi üretir.
    public string Ad { get; set; }

    // 2) DIŞARIDAN YALNIZCA OKUNABİLEN ÖZELLİK
    //    set'in önünde private var: değeri yalnızca sınıfın kendisi atar.
    public int Numara { get; private set; }

    // 3) TAM ÖZELLİK (full property) — kontrol burada yapılır
    public int Vize
    {
        get
        {
            return vize;
        }
        set
        {
            // "value" set bloğunun gizli parametresidir:
            // ogr.Vize = 70; yazdığınızda value 70 olur.
            if (value < 0 || value > 100)
            {
                Console.WriteLine($"  [RET] Not 0-100 arasında olmalı, gelen: {value}");
                return;
            }

            vize = value;
        }
    }

    public int Final
    {
        get { return final; }
        set
        {
            if (value < 0 || value > 100)
            {
                Console.WriteLine($"  [RET] Not 0-100 arasında olmalı, gelen: {value}");
                return;
            }

            final = value;
        }
    }

    // 4) HESAPLANAN ÖZELLİK — depolanmaz, sorulduğunda hesaplanır
    public string Durum
    {
        get
        {
            return Ortalama() >= 50 ? "Geçti" : "Kaldı";
        }
    }

    public Ogrenci(string ogrenciAdi)
    {
        Ad = ogrenciAdi;

        // Numara'nın set'i private; yalnızca sınıfın içinden atanabiliyor.
        // Kurucu sınıfın içinde olduğu için bu satır geçerli.
        Numara = 1001;
    }

    public double Ortalama()
    {
        return vize * 0.4 + final * 0.6;
    }
}

// --- DÖRT BİÇİM, TEK FİKİR ---
//
//   public string Ad { get; set; }              serbest okuma, serbest yazma
//   public int Numara { get; private set; }     dışarıdan yalnızca okuma
//   public int Vize { get {...} set {...} }     yazarken kural uygulanır
//   public string Durum { get {...} }           depolanmaz, hesaplanır
//
// Hepsinin ortak noktası: dışarısı ALAN kullanıyormuş gibi yazar,
// ama araya sınıfın koyduğu kural girer.
