// HATA AVI — bu dosyada 7 hata var. Bulabilir misiniz?
//
// DİKKAT: Bu dosya KASITLI OLARAK hatalıdır ve derlenmez.
// Amaç: derleyicinin verdiği hata mesajlarını okumayı öğrenmek.
//
// Nasıl çalışacaksınız:
//   1. Önce kağıt üzerinde hataları bulmaya çalışın
//   2. Sonra "dotnet run hata-avi.cs" ile derleyin
//   3. Hata mesajlarını okuyun, tek tek düzeltin
//   4. Program çalışana kadar devam edin
//
// İPUCU: Hataların hepsi derleme hatası DEĞİL. Bazıları mantık hatası —
// kod derlenir ama yanlış çalışır. Onları derleyici bulamaz, siz bulacaksınız.
//
// Cevap anahtarı ders notunda, kapalı bölümde. Önce kendiniz deneyin.

Console.Write("Notunuzu giriniz: ")

int notu = Console.ReadLine();

if (notu = 100)
{
    Console.WriteLine("Tam puan!");
}

if (notu > 0);
{
    Console.WriteLine("Geçerli bir not girdiniz.");
}

if (notu >= 60)
{
    Console.WriteLine("CC");
}
else if (notu >= 90)
{
    Console.WriteLine("AA");
}

for (int i = 1; i <= 5; i++)
{
    int carpim = 0;
    carpim = carpim * i;
}

Console.WriteLine("5! = " + carpim);
