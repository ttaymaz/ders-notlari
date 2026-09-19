// Dizinin duvarı: boyut baştan belli olmak zorunda.
//
// Bu dosya bir soruyu cevaplıyor: koleksiyonlar neden var?
// Aynı işi önce diziyle, sonra List<T> ile yapacağız.
//
// Şema: assets/01-dizi-ve-liste.svg
// Çalıştırmak için:  dotnet run 01-dizinin-duvari.cs

Console.WriteLine("=== DİZİ İLE ===");

string[] raf = new string[3];
int adet = 0;                       // kaç hücre doldu, elle takip ediyoruz

raf[adet] = "Tutunamayanlar";  adet++;
raf[adet] = "Sefiller";        adet++;
raf[adet] = "Beyaz Diş";       adet++;

Console.WriteLine($"Raf doldu: {adet}/{raf.Length}");

// Dördüncü kitap geldi. Dizi dolu. Ne yapacağız?
// Tek yol: daha büyük bir dizi açıp hepsini kopyalamak.
string[] yeniRaf = new string[raf.Length * 2];

for (int i = 0; i < raf.Length; i++)
{
    yeniRaf[i] = raf[i];
}

raf = yeniRaf;
raf[adet] = "Kuyucaklı Yusuf";  adet++;

Console.WriteLine($"Büyütüldü : {adet}/{raf.Length}");
Console.WriteLine("Dört satır kod, bir kopyalama döngüsü ve bir sayaç.");

// Ortadan bir kitap silmek daha da zahmetli: kalanları kaydırmak gerekir.
Console.WriteLine("\nSefiller siliniyor (ikinci sıradaki)...");

for (int i = 1; i < adet - 1; i++)
{
    raf[i] = raf[i + 1];            // sola kaydır
}

adet--;
raf[adet] = "";                     // son hücreyi boşalt

for (int i = 0; i < adet; i++)
{
    Console.WriteLine($"  {i}: {raf[i]}");
}


Console.WriteLine("\n=== LIST<T> İLE ===");

List<string> liste = new List<string>();

liste.Add("Tutunamayanlar");
liste.Add("Sefiller");
liste.Add("Beyaz Diş");
liste.Add("Kuyucaklı Yusuf");       // boyut sorunu yok, büyüme kendiliğinden

Console.WriteLine($"Eleman sayısı: {liste.Count}");

liste.Remove("Sefiller");           // kaydırmayı List kendi yapar

foreach (string ad in liste)
{
    Console.WriteLine($"  {ad}");
}

Console.WriteLine($"\nSilme sonrası: {liste.Count} eleman.");
Console.WriteLine("Sayaç yok, kopyalama döngüsü yok, kaydırma yok.");

// --- Denemeniz için ---
//
// 1. Dizi bölümünde `raf.Length * 2` yerine `raf.Length + 1` yazın.
//    Beşinci kitabı eklemek için ne yapmanız gerekir? Hangisi daha az
//    kopyalama yapar? (List<T> içeride ikiye katlama yapar — tesadüf değil.)
//
// 2. Dizi bölümündeki kaydırma döngüsünde `adet - 1` yerine `adet` yazın.
//    Hangi hata gelir? Neden?
//
// 3. List bölümünde `liste.Remove("Sefiller")` yerine
//    `liste.Remove("sefiller")` yazın (küçük s). Kaç eleman kaldı? Remove
//    metodu bulamadığında hata vermez, `false` döndürür — bu bir tuzaktır.
//
// 4. `liste.Insert(0, "Kürk Mantolu Madonna");` satırını ekleyin. Diziyle
//    aynı işi yapmak kaç satır sürerdi?
