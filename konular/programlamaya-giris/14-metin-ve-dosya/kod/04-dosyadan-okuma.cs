// Dosyadan okuma: StreamReader.
//
// Çalıştırmak için:  dotnet run 04-dosyadan-okuma.cs
// (Önce 03-dosyaya-yazma.cs çalıştırın ki dosya oluşsun)

string yol = "notlarim.txt";

// Dosya var mı? Yoksa program çöker — önce kontrol edelim.
if (!File.Exists(yol))
{
    Console.WriteLine("Dosya bulunamadı! Önce 03-dosyaya-yazma.cs çalıştırın.");
    return;
}

// --- Yöntem 1: tüm içeriği tek seferde ---
using (StreamReader okuyucu = new StreamReader(yol))
{
    string icerik = okuyucu.ReadToEnd();
    Console.WriteLine("--- TÜM İÇERİK ---");
    Console.WriteLine(icerik);
}

// --- Yöntem 2: satır satır ---
Console.WriteLine("--- SATIR SATIR ---");
using (StreamReader okuyucu = new StreamReader(yol))
{
    string satir;
    int no = 1;
    while ((satir = okuyucu.ReadLine()) != null)     // null = dosya bitti
    {
        Console.WriteLine($"{no}. {satir}");
        no++;
    }
}

// --- Yöntem 3: satırları DİZİ olarak al (en pratik) ---
Console.WriteLine("--- DİZİ OLARAK ---");
string[] satirlar = File.ReadAllLines(yol);
Console.WriteLine($"Dosyada {satirlar.Length} satır var.");

foreach (string s in satirlar)
{
    Console.WriteLine($"  {s}");
}

// Üçüncü yöntem dizileri döndürüyor — dokuzuncu ve onuncu haftada
// öğrendiğiniz her şeyi bu satırlar üzerinde kullanabilirsiniz.
