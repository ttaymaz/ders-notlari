// Ölçmenin tuzakları: Stopwatch ile tek ölçüm neden yanıltır?
//
// Aynı işi (10 000 sayıyı toplamak) Stopwatch ile on kez ölçüyoruz. Bütün
// ölçümler aynı olmalı, değil mi?
//
// Süreler her makinede ve her çalıştırmada farklıdır. Bu dosya bu yüzden
// süreleri değil, yalnızca büyük farkların cevabını yazıyor. Kendi
// makinenizde süreleri de görmek için README'deki "Denemeniz için"
// bölümüne bakın.
//
// İkinci bölüm, 13. haftanın sorusu: yansımayla çağırmak ne kadar yavaş?
// Çalıştırmak için:  dotnet run -c Release 01-olcmenin-tuzaklari.cs

using System.Diagnostics;
using System.Reflection;

Console.WriteLine("=== 1 === (aynı iş, on ölçüm)");
int[] sayilar = Enumerable.Range(1, 10_000).ToArray();
var sureler = new List<double>();
for (int i = 0; i < 10; i++)
{
    var saat = Stopwatch.StartNew();
    _ = Hesap.Topla(sayilar);
    sureler.Add(saat.Elapsed.TotalMicroseconds);
}
double ilk = sureler[0];
double sonrakiler = sureler.Skip(1).Average();
Console.WriteLine($"ilk ölçüm, sonrakilerin ortalamasının en az 5 katı mı? {ilk >= 5 * sonrakiler}");

Console.WriteLine("\n=== 2 === (doğrudan çağrı ve yansımayla çağrı)");
const int Tekrar = 200_000;
double dogrudan = Olc(() => Hesap.Ikiye(21), Tekrar);
MethodInfo ikiye = typeof(Hesap).GetMethod(nameof(Hesap.Ikiye))!;
object[] yirmiBir = [21];
double yansima = Olc(() => ikiye.Invoke(null, yirmiBir), Tekrar);
Console.WriteLine($"yansımayla çağrı, doğrudan çağrının en az 5 katı mı? {yansima >= 5 * dogrudan}");

// Isınma turu + birçok tekrarın ortalaması: tek ölçümden çok daha güvenilir
static double Olc(Action govde, int tekrar)
{
    for (int i = 0; i < 1_000; i++) govde();        // ısınma: JIT derlesin
    var saat = Stopwatch.StartNew();
    for (int i = 0; i < tekrar; i++) govde();
    return saat.Elapsed.TotalNanoseconds / tekrar;
}

static class Hesap
{
    public static long Topla(int[] dizi)
    {
        long toplam = 0;
        foreach (int s in dizi)
            toplam += s;
        return toplam;
    }

    public static int Ikiye(int x) => x * 2;
}
