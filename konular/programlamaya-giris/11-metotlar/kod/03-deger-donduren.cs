// Değer döndüren metot: sonucu ekrana yazmaz, SİZE verir.
//
// Çalıştırmak için:  dotnet run 03-deger-donduren.cs

// void yerine dönüş tipi yazılır ve return kullanılır
int Topla(int sayi1, int sayi2)
{
    int sonuc = sayi1 + sayi2;
    return sonuc;               // sonucu çağıran yere gönder
}

double Ortalama(int a, int b, int c)
{
    return (a + b + c) / 3.0;   // doğrudan da döndürebiliriz
}

// --- Kullanım 1: sonucu değişkende tut ---
int gelenDeger = Topla(5, 3);
int yeniSonuc = gelenDeger * 10;
Console.WriteLine($"İşlem sonucu: {yeniSonuc}");

// --- Kullanım 2: doğrudan yazdır ---
Console.WriteLine(Topla(10, 20));

// --- Kullanım 3: başka bir metoda gönder ---
Console.WriteLine(Topla(Topla(1, 2), 3));      // 6

Console.WriteLine($"Ortalama: {Ortalama(70, 85, 90)}");

// --- ANALOJİ ---
// void      → garson: "su getir" dersiniz, masaya koyar, iş biter
// return    → bankamatik: para SİZE geçer, onunla markete gidersiniz
