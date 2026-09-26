// Eşitlik: "aynı nesne mi?" ile "aynı değer mi?" farklı sorular.
//
// class için == varsayılan olarak ilk soruyu sorar, record ikinciyi.
// string bir class'tır ama == işlecini değer karşılaştırmasına çevirmiştir.
// Çalıştırmak için:  dotnet run 04-esitlik.cs

var k1 = new KitapC("Nutuk", 1927);
var k2 = new KitapC("Nutuk", 1927);
Console.WriteLine($"class  ==     : {k1 == k2}");
Console.WriteLine($"class  Equals : {k1.Equals(k2)}");

var r1 = new KitapR("Nutuk", 1927);
var r2 = new KitapR("Nutuk", 1927);
Console.WriteLine($"record ==     : {r1 == r2}");
Console.WriteLine($"record aynı mı: {ReferenceEquals(r1, r2)}");

string s1 = "Nutuk";
string s2 = new string(['N', 'u', 't', 'u', 'k']);
Console.WriteLine($"string ==     : {s1 == s2}");
Console.WriteLine($"string aynı mı: {ReferenceEquals(s1, s2)}");

class KitapC(string baslik, int yil)
{
    public string Baslik { get; } = baslik;
    public int Yil { get; } = yil;
}

record KitapR(string Baslik, int Yil);
