// List<T> temelleri: ekleme, silme, arama, gezme.
//
// Bu dosyadaki her metot bahar dönemindeki formda da aynı adla çalışacak.
//
// Çalıştırmak için:  dotnet run 02-liste-temelleri.cs

List<string> raf = new List<string>();

// --- EKLEME ---
raf.Add("Tutunamayanlar");
raf.Add("Sefiller");
raf.Add("Beyaz Diş");

raf.Insert(1, "Kürk Mantolu Madonna");     // araya sokar, kalanları kaydırır

Console.WriteLine($"Eleman sayısı: {raf.Count}");   // Length değil, Count

// --- İNDİSLE ERİŞİM: dizideki gibi ---
Console.WriteLine($"İlk  : {raf[0]}");
Console.WriteLine($"Son  : {raf[raf.Count - 1]}");

// --- GEZME ---
Console.WriteLine("\n--- foreach ile ---");
foreach (string ad in raf)
{
    Console.WriteLine($"  {ad}");
}

Console.WriteLine("\n--- for ile (indis gerekiyorsa) ---");
for (int i = 0; i < raf.Count; i++)
{
    Console.WriteLine($"  {i}: {raf[i]}");
}

// --- ARAMA ---
Console.WriteLine("\n--- Arama ---");
Console.WriteLine($"Sefiller var mı?      {raf.Contains("Sefiller")}");
Console.WriteLine($"Sefiller kaçıncı?     {raf.IndexOf("Sefiller")}");
Console.WriteLine($"Olmayan kitap kaçıncı? {raf.IndexOf("Şeker Portakalı")}");   // -1

// --- SİLME: üç yol ---
raf.Remove("Sefiller");          // değere göre siler, bulamazsa false döner
raf.RemoveAt(0);                 // indise göre siler

Console.WriteLine($"\nSilmelerden sonra: {raf.Count} eleman");
foreach (string ad in raf)
{
    Console.WriteLine($"  {ad}");
}

// --- SIRALAMA ---
raf.Sort();
Console.WriteLine($"\nSıralı: {string.Join(" | ", raf)}");

// --- TEMİZLEME ---
raf.Clear();
Console.WriteLine($"Temizlendi: {raf.Count} eleman");

// --- BAŞLANGIÇ DEĞERİYLE KURMAK ---
List<int> notlar = new List<int> { 65, 90, 45, 78 };
Console.WriteLine($"\nNotlar: {string.Join(", ", notlar)}  (toplam {notlar.Count})");

int toplam = 0;
foreach (int n in notlar)
{
    toplam += n;
}

Console.WriteLine($"Ortalama: {(double)toplam / notlar.Count:F1}");

// --- Denemeniz için ---
//
// 1. `raf.Count` yerine `raf.Length` yazın. Derleyici ne diyor? Dizide
//    `Length`, koleksiyonda `Count` kullanılır — ikisi de aynı şeyi söyler
//    ama adları farklıdır.
//
// 2. `raf[raf.Count]` yazıp çalıştırın. Hangi hata gelir? Dizideki sınır
//    kuralı burada da geçerlidir: son indis `Count - 1`.
//
// 3. `raf.Remove("sefiller")` (küçük s) deneyin. Program çökmez, hiçbir şey
//    silinmez. Silinip silinmediğini anlamak için dönüş değerini yazdırın:
//    `Console.WriteLine(raf.Remove("sefiller"));`
//
// 4. `notlar.Sort();` ardından `notlar.Reverse();` çağırın. Çıktı ne oldu?
//
// 5. Boş bir listede `liste[0]` okumayı deneyin. Hata mesajı, dizi sınırı
//    aşımındakiyle aynı mı?
