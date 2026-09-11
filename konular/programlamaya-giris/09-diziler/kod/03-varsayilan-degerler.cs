// Yeni oluşturulan bir dizinin gözleri boş DEĞİLDİR.
//
// Çalıştırmak için:  dotnet run 03-varsayilan-degerler.cs
//
// new ile dizi oluşturduğunuzda C# her gözü veri tipinin
// VARSAYILAN DEĞERİ ile doldurur.

int[] sayilar = new int[3];
double[] ondalikli = new double[3];
bool[] durumlar = new bool[3];
string[] metinler = new string[3];

Console.WriteLine($"int      → {sayilar[0]}");        // 0
Console.WriteLine($"double   → {ondalikli[0]}");      // 0
Console.WriteLine($"bool     → {durumlar[0]}");       // False
Console.WriteLine($"string   → [{metinler[0]}]");     // [] — boş görünür

// string dizisinin varsayılanı boş metin DEĞİL, null'dur.
// null "burada hiçbir şey yok" demektir; ekrana basılınca boş görünür.
Console.WriteLine($"metinler[0] null mü? {metinler[0] == null}");

// Bu neden önemli?
// Bir diziyi doldurduğunuzu sanıp bir gözü atlarsanız, orada 0 veya null kalır.
// Ortalama hesabında bu, sonucu sessizce bozar — mantık hatası.
