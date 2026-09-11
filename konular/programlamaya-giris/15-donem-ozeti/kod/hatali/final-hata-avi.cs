// FİNAL HATA AVI — bu dosyada 8 hata var.
//
// DİKKAT: Bu dosya KASITLI OLARAK hatalıdır ve derlenmez.
// Hataların bir kısmı derleme, bir kısmı MANTIK hatasıdır.
//
// Nasıl çalışacaksınız:
//   1. Önce kağıt üzerinde bulmaya çalışın
//   2. Sonra derleyin, hata mesajlarını okuyun
//   3. Tek tek düzeltin, program doğru çalışana kadar devam edin
//
// Cevap anahtarı ders notunda, kapalı bölümde.

int[] notlar = new int[5];

for (int i = 0; i <= notlar.Length; i++)
{
    Console.Write($"{i + 1}. not: ");
    notlar[i] = Console.ReadLine();
}

int enBuyuk = 0;
int toplam;

foreach (int n in notlar)
{
    toplam += n;
    if (n > enBuyuk)
        enBuyuk = n;
}

double ortalama = toplam / notlar.Length;

Console.WriteLine("Ortalama: " + ortalama);
Console.WriteLine("En yüksek: " + enBuyuk);

string mesaj = "  sonuc hazir  ";
mesaj.Trim().ToUpper();
Console.WriteLine(mesaj);

StreamWriter yazici = new StreamWriter("sonuc.txt");
yazici.WriteLine(ortalama);
