// Final provası — çıktıyı ÖNCE kağıda yazın, sonra çalıştırın.
//
// Çalıştırmak için:  dotnet run 03-cikti-tahmini.cs
//
// Yanıldığınız blok, tekrar etmeniz gereken haftayı gösterir.

Console.WriteLine("--- 1 ---");
int[] sayilar = { 5, 3, 9, 1 };
Console.WriteLine(sayilar.Length);
Console.WriteLine(sayilar[sayilar.Length - 1]);

Console.WriteLine("--- 2 ---");
int enBuyuk = 0;
foreach (int s in new int[] { -5, -3, -9 })
{
    if (s > enBuyuk) { enBuyuk = s; }
}
Console.WriteLine(enBuyuk);

Console.WriteLine("--- 3 ---");
string ad = "  Ahmet  ";
ad.Trim();
Console.WriteLine($"[{ad}]");

Console.WriteLine("--- 4 ---");
Console.WriteLine(Kare(4) + Kare(3));
int Kare(int x) { return x * x; }

Console.WriteLine("--- 5 ---");
string satir = "Ali;85;Matematik";
string[] parcalar = satir.Split(';');
Console.WriteLine(parcalar.Length);
Console.WriteLine(parcalar[1]);

Console.WriteLine("--- 6 ---");
if (int.TryParse("12abc", out int sonuc))
    Console.WriteLine($"Başarılı: {sonuc}");
else
    Console.WriteLine($"Başarısız, sonuc = {sonuc}");

Console.WriteLine("--- 7 ---");
try
{
    int[] kucuk = new int[2];
    Console.WriteLine(kucuk[5]);
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("Yakalandı");
}
finally
{
    Console.WriteLine("Finally çalıştı");
}
