// Dizi elemanlarını DEĞİŞTİRMEK: foreach yetmez, for gerekir.
//
// Çalıştırmak için:  dotnet run 05-degistirme.cs
//
// Senaryo: tüm notlara 5 puan ekleyelim.

int[] notlar = { 50, 80, 70, 90, 40 };

Console.WriteLine($"Önce : {string.Join(", ", notlar)}");

// --- for ile: ÇALIŞIR ---
for (int i = 0; i < notlar.Length; i++)
{
    notlar[i] += 5;          // diziyi indisle güncelliyoruz
}

Console.WriteLine($"Sonra: {string.Join(", ", notlar)}");

// --- foreach ile: ÇALIŞMAZ ---
// Aşağıdaki satırların başındaki // işaretini kaldırın — DERLENMEZ.
//
// foreach (int n in notlar)
// {
//     n += 5;               // HATA: foreach değişkeni salt okunurdur
// }
//
// foreach size elemanın bir KOPYASINI verir. Kopyayı değiştirmek
// dizideki aslını değiştirmez — C# bu yüzden en baştan izin vermiyor.

// string.Join, dizi elemanlarını aralarına ayraç koyarak tek metne çevirir.
// 14. haftada metin işlemlerinde ayrıntısını göreceğiz.
