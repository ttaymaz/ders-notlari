// İndisle değer yazma ve okuma.
//
// Çalıştırmak için:  dotnet run 02-indis-erisimi.cs
//
// EN ÖNEMLİ KURAL: İndisler 0'DAN başlar.
// 3 elemanlı dizinin indisleri: 0, 1, 2  (3 diye bir göz YOKTUR)

int[] notlar = new int[3];

// --- Değer yazma ---
notlar[0] = 80;      // 1. eleman
notlar[1] = 95;      // 2. eleman
notlar[2] = 70;      // 3. ve SON eleman

// --- Değer okuma ---
Console.WriteLine($"Birinci öğrencinin notu: {notlar[0]}");
Console.WriteLine($"İkinci öğrencinin notu:  {notlar[1]}");
Console.WriteLine($"Üçüncü öğrencinin notu:  {notlar[2]}");

// --- Son elemana erişmenin iki yolu ---
Console.WriteLine($"Son eleman (klasik): {notlar[notlar.Length - 1]}");
Console.WriteLine($"Son eleman (kısa)  : {notlar[^1]}");

// notlar[^1] "sondan birinci" demektir. Modern C#'ın kısayolu.
// Derste klasik yazımı kullanacağız; kodlarda ikincisiyle karşılaşabilirsiniz.

// --- Denemeniz için ---
// Aşağıdaki satırın başındaki // işaretini kaldırın ve çalıştırın.
// Program ÇÖKER. Hata mesajını okuyun: IndexOutOfRangeException
//
// Console.WriteLine(notlar[3]);
//
// Neden? 3 elemanlı dizinin son indisi 2'dir. [3] diye bir göz yoktur.
// Bu, dizilerle çalışırken en sık yapılan hatadır.
