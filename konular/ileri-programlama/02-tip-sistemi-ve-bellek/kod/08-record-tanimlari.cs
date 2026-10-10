// record'un iki sözü: değer eşitliği ve değiştirilemezlik — ve sınırları.
//
// Değer eşitliği: iki kayıt, tipleri ve bütün özellikleri eşitse eşittir.
// Her özellik KENDİ Equals'ıyla karşılaştırılır; List referansla karşılaştırılır.
// Değiştirilemezlik: konumsal kaydın özellikleri init'tir, kurulduktan sonra
// atanamaz. Ama bu SIĞDIR: özelliğin gösterdiği liste yine değiştirilebilir.
// Çalıştırmak için:  dotnet run 08-record-tanimlari.cs

// 1) Değer eşitliği: tipler ve özellikler aynı
var k1 = new Kunye("Nutuk", 1927);
var k2 = new Kunye("Nutuk", 1927);
Console.WriteLine($"Kunye ==            : {k1 == k2}");
Console.WriteLine($"Özet kodları aynı mı: {k1.GetHashCode() == k2.GetHashCode()}");

// 2) Liste içeren kayıt: içerik aynı, listeler ayrı nesne
var a = new Kitap("Nutuk", 1927, ["tarih", "anı"]);
var b = new Kitap("Nutuk", 1927, ["tarih", "anı"]);
Console.WriteLine($"Kitap a == b        : {a == b}");

// 3) with sığ kopyadır: liste paylaşılır
var c = a with { };
Console.WriteLine($"Kitap a == c        : {a == c}");
a.Etiketler.Add("klasik");
Console.WriteLine($"c'nin etiket sayısı : {c.Etiketler.Count}");

// 4) Eşitlik tipe de bakar
Yayin y1 = new Yayin("Nutuk");
Yayin y2 = new Roman("Nutuk", "Mustafa Kemal Atatürk");
Console.WriteLine($"Yayin == Roman      : {y1 == y2}");

// Derlenmez (CS8852): init özelliğe kurulduktan sonra atama
// a.Yil = 1928;

record Kunye(string Baslik, int Yil);
record Kitap(string Baslik, int Yil, List<string> Etiketler);
record Yayin(string Baslik);
record Roman(string Baslik, string Yazar) : Yayin(Baslik);
