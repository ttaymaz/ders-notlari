// HashSet içindeki eleman değişirse ne olur?
//
// HashSet ve Dictionary elemanı, eklendiği andaki özet koduna (hash)
// göre bir "çekmeceye" koyar. Eleman sonradan değişirse özet kodu da
// değişir ve eleman yanlış çekmecede kalır.
// Çalıştırmak için:  dotnet run 06-kaybolan-eleman.cs

var uyeler = new HashSet<Uye>();
var ayse = new Uye { Ad = "Ayşe", Numara = 101 };
uyeler.Add(ayse);
Console.WriteLine($"Ekledikten sonra     : {uyeler.Contains(ayse)}");

ayse.Numara = 202;
Console.WriteLine($"Değiştirdikten sonra : {uyeler.Contains(ayse)}");
Console.WriteLine($"Kümede kaç üye var   : {uyeler.Count}");

foreach (var u in uyeler)
    Console.WriteLine($"Kümedeki üye         : {u}");

// Bilerek set ile yazıldı: değiştirilebilir bir record.
record Uye
{
    public string Ad { get; set; } = "";
    public int Numara { get; set; }
}
