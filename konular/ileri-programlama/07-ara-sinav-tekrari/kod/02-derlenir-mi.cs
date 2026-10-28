// PROVA 2 — derlenir mi?
//
// Sınavdaki ikinci soru tipi: "Bu kod derlenir mi? Derlenmiyorsa neden?"
// Bu dosya olduğu haliyle derlenir ve çalışır. Her soruda bir satırın
// başında  //?  işareti var. Soruyu şöyle çalışın:
//
//   1. Satırı OKUYUN. Kâğıda üç cevaptan birini yazın:
//        derlenir · derlenir ama UYARI verir · DERLENMEZ
//      ve "derlenmez" dediyseniz nedenini bir cümleyle yazın.
//   2. Satırın başındaki  //?  işaretini silin ve çalıştırın.
//   3. Derleyicinin dediğini kendi cümlenizle karşılaştırın, sonra
//      işareti geri koyun. Aynı anda yalnızca BİR satırı açın.
//
// Hata kodunu ezberlemeniz gerekmez; sınavda sorulan, NEDEN'dir.
//
// Çalıştırmak için:  dotnet run 02-derlenir-mi.cs
// .NET 9:            kod/README.md içindeki proje yolu

// ---- Soru 1 (nullable)
string ad = "Ece";
//? string soyad = null;
Console.WriteLine(ad);

// ---- Soru 2 (record ve init)
var sefer = new Sefer("Afyon", "Konya");
//? sefer.Nereye = "Ankara";
Console.WriteLine(sefer);

// ---- Soru 3 (listede struct)
var konumlar = new List<Konum> { new(1, 2) };
//? konumlar[0].X = 5;
Console.WriteLine(konumlar[0].X);

// ---- Soru 4 (tür çıkarımı)
Console.WriteLine(Buyuk(3, 8));
//? Console.WriteLine(Buyuk(3, "8"));

// ---- Soru 5 (varyans)
IEnumerable<object> nesneler = new List<string> { "a" };
//? List<object> nesneListesi = new List<string> { "a" };
Console.WriteLine(nesneler.Count());

// ---- Soru 6 (delege imzası)
Func<int, bool> ciftMi = x => x % 2 == 0;
//? Func<int, bool> ikiKati = x => x * 2;
Console.WriteLine(ciftMi(4));

// ---- Soru 7 (olay)
var zil = new Zil();
zil.Caldi += () => Console.WriteLine("zil çaldı");
zil.Cal();
//? zil.Caldi();

// ---- Soru 8 (yield ve dönüş tipi)
Console.WriteLine(string.Join(" ", IlkUc()));
//? List<int> Liste() { yield return 1; }

// ---- Soru 9 (switch: kolların sırası)
Console.WriteLine(Derece(35));

// ---- Soru 10 (switch: eksik kol)
Console.WriteLine(Isaret(-4));

// ---- Soru 11 (genişletme metodu)
Console.WriteLine("kar".Ikile());


T Buyuk<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) >= 0 ? a : b;

IEnumerable<int> IlkUc()
{
    yield return 1;
    yield return 2;
    yield return 3;
}

// Soru 9: aşağıdaki iki kolun YERİNİ değiştirin. Derlenir mi?
string Derece(int c) => c switch
{
    > 30 => "sıcak",
    > 15 => "ılık",
    _ => "serin",
};

// Soru 10: aşağıdaki son kolu ( > 0 => ... ) silin. Derlenir mi?
string Isaret(int n) => n switch
{
    < 0 => "eksi",
    0 => "sıfır",
    > 0 => "artı",
};

record Sefer(string Nereden, string Nereye);

struct Konum
{
    public int X;
    public int Y;
    public Konum(int x, int y) { X = x; Y = y; }
}

class Zil
{
    public event Action? Caldi;
    public void Cal() => Caldi?.Invoke();
}

// Soru 11: aşağıdaki sınıftan "static" kelimesini silin. Derlenir mi?
static class MetinUzantilari
{
    public static string Ikile(this string s) => s + s;
}
