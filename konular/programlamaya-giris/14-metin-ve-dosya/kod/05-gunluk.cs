// Dijital günlük — dönemin tüm konuları bir arada.
//
// Çalıştırmak için:  dotnet run 05-gunluk.cs
//
// Kullanılanlar: string metotları, dosya işlemleri, switch,
//                döngü, try-catch, TryParse.

string yol = "gunluk.txt";
bool devam = true;

while (devam)
{
    Console.WriteLine("\n--- DİJİTAL GÜNLÜK ---");
    Console.WriteLine("1. Günlüğü oku");
    Console.WriteLine("2. Yeni not ekle");
    Console.WriteLine("3. Notlarda ara");
    Console.WriteLine("0. Çıkış");
    Console.Write("Seçiminiz: ");

    string secim = Console.ReadLine();

    try
    {
        switch (secim)
        {
            case "1":
                if (File.Exists(yol))
                {
                    Console.WriteLine("\n--- ESKİ NOTLAR ---");
                    Console.WriteLine(File.ReadAllText(yol));
                }
                else
                {
                    Console.WriteLine("Henüz günlük oluşturulmamış.");
                }
                break;

            case "2":
                Console.Write("Notunuz: ");
                string not = Console.ReadLine().Trim();

                if (not.Length == 0)
                {
                    Console.WriteLine("Boş not kaydedilmez.");
                    break;
                }

                using (StreamWriter yazici = new StreamWriter(yol, append: true))
                {
                    yazici.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm}] {not}");
                }
                Console.WriteLine("Notunuz günlüğe işlendi.");
                break;

            case "3":
                if (!File.Exists(yol))
                {
                    Console.WriteLine("Günlük yok.");
                    break;
                }

                Console.Write("Aranacak kelime: ");
                string aranan = Console.ReadLine().ToLower();

                string[] satirlar = File.ReadAllLines(yol);
                int bulunan = 0;

                foreach (string satir in satirlar)
                {
                    if (satir.ToLower().Contains(aranan))
                    {
                        Console.WriteLine($"  {satir}");
                        bulunan++;
                    }
                }

                Console.WriteLine($"{bulunan} sonuç bulundu.");
                break;

            case "0":
                devam = false;
                Console.WriteLine("Görüşmek üzere!");
                break;

            default:
                Console.WriteLine("Geçersiz seçim.");
                break;
        }
    }
    catch (Exception hata)
    {
        Console.WriteLine($"Bir hata oluştu: {hata.Message}");
    }
}

// --- Arama neden ToLower() ile yapılıyor? ---
// Contains büyük/küçük harfe DUYARLIDIR. Hem satırı hem aranan kelimeyi
// küçük harfe çevirerek "Ders" ile "ders" aramasını eşleştiriyoruz.
