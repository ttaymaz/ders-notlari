// PROVA 1 — çıktı tahmini.
//
// On dört bölüm, 1–6. konuların her birinden en az bir tane. Sınavdaki
// kod okuma sorularının çoğu bu biçimdedir: kodu okuyup ne yazacağını
// söylemek.
//
// NASIL ÇALIŞILIR:
//   1. Her bölümü OKUYUN ve çıktıyı KÂĞIDA yazın. Bilgisayarı kapatın.
//   2. On dört bölümün hepsini yazdıktan sonra çalıştırın.
//   3. Tutmayan her bölüm için NEDEN tutmadığını bir cümleyle yazın.
//      O cümle, ders notunda hangi bölüme dönmeniz gerektiğini söyler.
//
// Çalıştırmak için:  dotnet run 01-cikti-tahmini.cs
// .NET 9:            kod/README.md içindeki proje yolu

Console.WriteLine("=== 1 === (nullable ve ?? işleci)");
string? sehir = null;
Console.WriteLine(sehir?.Length ?? -1);
Console.WriteLine($"[{sehir}]");

Console.WriteLine("\n=== 2 === (struct ve class ataması)");
var d1 = new KonumS(1, 2);
var d2 = d1;
d2.X = 10;
var c1 = new KonumC(1, 2);
var c2 = c1;
c2.X = 10;
Console.WriteLine($"{d1.X} {c1.X}");

Console.WriteLine("\n=== 3 === (record, with ve sığ kopya)");
var s1 = new Sefer("Afyon", "Konya", new List<string> { "Bolvadin" });
var s2 = s1 with { Nereye = "Ankara" };
s2.Duraklar.Add("Emirdağ");
var s3 = s1 with { };
Console.WriteLine($"{s1 == s2} {s1 == s3} {s1.Duraklar.Count}");

Console.WriteLine("\n=== 4 === (kutulama)");
int adet = 5;
object kutu = adet;
adet++;
object a = 7, b = 7;
Console.WriteLine($"{adet} {kutu}");
Console.WriteLine($"{a == b} {a.Equals(b)}");

Console.WriteLine("\n=== 5 === (her kapalı tip ayrı bir tiptir)");
Sayac<int>.Arttir();
Sayac<int>.Arttir();
Sayac<string>.Arttir();
Console.WriteLine($"{Sayac<int>.Deger} {Sayac<string>.Deger} {Sayac<double>.Deger}");

Console.WriteLine("\n=== 6 === (default(T))");
Console.WriteLine($"{Bos<int>()} {Bos<string>() is null} {Bos<bool>()}");

Console.WriteLine("\n=== 7 === (çok noktaya yayın)");
Action? yaz = A;
yaz += B;
yaz += A;
yaz -= A;
yaz?.Invoke();
Console.WriteLine();
Func<int> uret = () => 1;
uret += () => 2;
Console.WriteLine(uret());

Console.WriteLine("\n=== 8 === (kapanım ve döngü değişkeni)");
var forIle = new List<Func<int>>();
for (int i = 0; i < 3; i++)
    forIle.Add(() => i * 10);
var foreachIle = new List<Func<int>>();
foreach (int j in new[] { 0, 1, 2 })
    foreachIle.Add(() => j * 10);
Console.WriteLine(string.Join(" ", forIle.Select(f => f())));
Console.WriteLine(string.Join(" ", foreachIle.Select(f => f())));

Console.WriteLine("\n=== 9 === (yield ve erken çıkış)");
var dizi = Say(3);
Console.Write("önce ");
foreach (int x in dizi)
{
    if (x == 2) break;
    Console.Write($"{x} ");
}
Console.WriteLine();

Console.WriteLine("\n=== 10 === (yığından kuyruğa)");
var yigin = new Stack<int>();
yigin.Push(1);
yigin.Push(2);
yigin.Push(3);
var kuyruk = new Queue<int>(yigin);
Console.WriteLine(string.Join(" ", kuyruk));
kuyruk.Dequeue();
yigin.Pop();
Console.WriteLine($"{kuyruk.Peek()} {yigin.Peek()}");

Console.WriteLine("\n=== 11 === (ertelenmiş LINQ ve değişen kaynak)");
var olcumler = new List<int> { 12, 25, 31, 8 };
var sicak = olcumler.Where(t => t > 20);
olcumler.Add(40);
var sabit = sicak.ToList();
olcumler.Add(50);
Console.WriteLine($"{sicak.Count()} {sabit.Count}");

Console.WriteLine("\n=== 12 === (boş koleksiyonda LINQ)");
var bos = new List<int>();
Console.WriteLine($"{bos.Any()} {bos.All(x => x > 100)} {bos.FirstOrDefault()} {bos.Sum()}");

Console.WriteLine("\n=== 13 === (switch ifadesi: hangi kol?)");
foreach (object? o in new object?[] { -3, 0, 7, "", "kar", null, 2.5 })
    Console.Write($"{Siniflandir(o)} | ");
Console.WriteLine();

Console.WriteLine("\n=== 14 === (liste deseni)");
Console.WriteLine(Bak([]));
Console.WriteLine(Bak([4]));
Console.WriteLine(Bak([4, 8]));
Console.WriteLine(Bak([4, 8, 15, 16]));


void A() => Console.Write("A");
void B() => Console.Write("B");

T? Bos<T>() => default;

IEnumerable<int> Say(int n)
{
    Console.Write("[başla]");
    for (int i = 1; i <= n; i++)
    {
        Console.Write($"<{i}>");
        yield return i;
    }
    Console.Write("[bitti]");
}

string Siniflandir(object? o) => o switch
{
    int n when n < 0 => "negatif",
    0 => "sıfır",
    int => "pozitif",
    string { Length: 0 } => "boş metin",
    string m => $"metin({m.Length})",
    null => "null",
    _ => "başka",
};

string Bak(int[] d) => d switch
{
    [] => "boş",
    [var tek] => $"tek: {tek}",
    [var ilk, .., var son] => $"{ilk} ... {son} ({d.Length} eleman)",
};

struct KonumS
{
    public int X;
    public int Y;
    public KonumS(int x, int y) { X = x; Y = y; }
}

class KonumC
{
    public int X;
    public int Y;
    public KonumC(int x, int y) { X = x; Y = y; }
}

record Sefer(string Nereden, string Nereye, List<string> Duraklar);

static class Sayac<T>
{
    public static int Deger;
    public static void Arttir() => Deger++;
}
