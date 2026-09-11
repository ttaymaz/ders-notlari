// Metotlara dizi göndermek — geçen haftanın algoritmalarını yeniden kullanma.
//
// Çalıştırmak için:  dotnet run 04-dizi-parametresi.cs
//
// Geçen hafta "en büyüğü bul" algoritmasını yazmıştık. Başka bir dizi için
// tekrar kullanmak isteseydik kopyalamamız gerekirdi. Artık gerekmiyor.

int EnBuyuk(int[] dizi)
{
    int enBuyuk = dizi[0];
    foreach (int sayi in dizi)
    {
        if (sayi > enBuyuk) { enBuyuk = sayi; }
    }
    return enBuyuk;
}

double OrtalamaBul(int[] dizi)
{
    int toplam = 0;
    foreach (int sayi in dizi) { toplam += sayi; }
    return (double)toplam / dizi.Length;
}

void DiziYazdir(string baslik, int[] dizi)
{
    Console.WriteLine($"{baslik}: {string.Join(", ", dizi)}");
}

int[] notlar = { 50, 80, 70, 90, 40 };
int[] sicakliklar = { -3, 12, 8, -7, 21 };

DiziYazdir("Notlar", notlar);
Console.WriteLine($"  En büyük : {EnBuyuk(notlar)}");
Console.WriteLine($"  Ortalama : {OrtalamaBul(notlar)}");

DiziYazdir("Sıcaklıklar", sicakliklar);
Console.WriteLine($"  En büyük : {EnBuyuk(sicakliklar)}");
Console.WriteLine($"  Ortalama : {OrtalamaBul(sicakliklar)}");

// Algoritmayı BİR KEZ yazdık, İKİ dizi için kullandık.
// Bir hata bulsak tek bir yerde düzeltirdik. Metotların asıl faydası bu.
