// Kovaryans: Kitap bir Yayin ise, Kitap dizisi bir Yayin dizisi midir?
//
// IEnumerable<out T> için cevap evet ve güvenli: yalnızca OKUNUR.
// List<T> için cevap hayır: listeye yazılabilir.
// Diziler için cevap evet ama GÜVENSİZ: derlenir, çalışırken patlayabilir.
// Şema: assets/01-varyans-yonu.svg
// Çalıştırmak için:  dotnet run 06-kovaryans.cs

List<Kitap> kitaplar =
[
    new Kitap("Çalıkuşu", "Reşat Nuri Güntekin"),
    new Kitap("Kuyucaklı Yusuf", "Sabahattin Ali"),
];

// 1) IEnumerable<Kitap> -> IEnumerable<Yayin>: izinli
IEnumerable<Yayin> yayinlar = kitaplar;
foreach (Yayin y in yayinlar)
    Console.WriteLine(y.Baslik);

// 2) List<Kitap> -> List<Yayin>: DERLENMEZ. Neden? Açıp deneyin:
// List<Yayin> liste = kitaplar;
// liste.Add(new Dergi("Kütüphane Bülteni", 12));   // listeye dergi girerdi!

// 3) Dizi kovaryansı: derlenir, ama çalışma zamanı denetler
Yayin[] raf = new Kitap[2];
raf[0] = new Kitap("Sinekli Bakkal", "Halide Edib Adıvar");
try
{
    raf[1] = new Dergi("Kütüphane Bülteni", 12);
}
catch (Exception hata)
{
    Console.WriteLine(hata.GetType().Name);
}

record Yayin(string Baslik);
record Kitap(string Baslik, string Yazar) : Yayin(Baslik);
record Dergi(string Baslik, int Sayi) : Yayin(Baslik);
