// Kapanım (closure): lambda, kullandığı dış değişkeni DEĞERİYLE değil
// KENDİSİYLE yakalar. Değişken değişirse lambda değişmiş halini görür.
//
// Tanıtım dersindeki ikinci ısınma sorusunun (3 3 3) açıklaması burada.
// Çalıştırmak için:  dotnet run 04-kapanim.cs

// 1) Yakalanan değişken lambda ile birlikte yaşar
int okunan = 0;
Action oku = () => okunan++;
oku(); oku(); oku();
Console.WriteLine($"okunan: {okunan}");

// 2) Her çağrı kendi değişkenini yakalar
Func<int> s1 = SayacUret();
Func<int> s2 = SayacUret();
s1(); s1(); s2();
Console.WriteLine($"s1: {s1()}  s2: {s2()}");

// 3) Döngü tuzağı: for'un tek bir i değişkeni var, üç lambda onu paylaşır
var islemler = new List<Func<int>>();
for (int i = 0; i < 3; i++)
    islemler.Add(() => i);
Yazdir("for       ", islemler);

// 4) Düzeltme: her turda yeni bir değişken
var duzeltilmis = new List<Func<int>>();
for (int i = 0; i < 3; i++)
{
    int kopya = i;
    duzeltilmis.Add(() => kopya);
}
Yazdir("for+kopya ", duzeltilmis);

// 5) foreach her turda yeni bir değişken açar
var foreachIle = new List<Func<int>>();
foreach (int j in new[] { 0, 1, 2 })
    foreachIle.Add(() => j);
Yazdir("foreach   ", foreachIle);

Func<int> SayacUret()
{
    int n = 0;
    return () => ++n;
}

void Yazdir(string etiket, List<Func<int>> liste)
{
    Console.Write(etiket + ": ");
    foreach (var f in liste)
        Console.Write($"{f()} ");
    Console.WriteLine();
}
