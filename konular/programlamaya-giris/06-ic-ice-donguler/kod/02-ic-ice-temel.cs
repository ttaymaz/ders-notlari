// İç içe döngünün temel mantığı.
//
// Çalıştırmak için:  dotnet run 02-ic-ice-temel.cs
//
// KURAL: Dış döngünün HER adımı için, iç döngü BAŞTAN SONA çalışır.

int toplamTur = 0;

for (int i = 1; i <= 3; i++)          // dış döngü — 3 kez
{
    Console.WriteLine($"Dış döngü turu {i} başladı");

    for (int j = 1; j <= 5; j++)      // iç döngü — her seferinde 5 kez
    {
        Console.Write($"  ({i},{j})");
        toplamTur++;
    }

    Console.WriteLine();              // iç döngü bitti, alt satıra geç
}

Console.WriteLine($"\nİç blok toplam {toplamTur} kez çalıştı. (3 x 5 = 15)");

// --- Saat analojisi ---
// Dış döngü  = yelkovan (dakika), yavaş
// İç döngü   = saniye, hızlı
// Yelkovan 1 adım atarken saniye 60 adımla tam tur atar.

// --- SIK YAPILAN HATA: aynı sayacı iki döngüde kullanmak ---
//
// Sayaç for içinde tanımlıysa C# sizi korur:
//
//     for (int i = 0; i < 5; i++)
//         for (int i = 0; i < 3; i++)   // DERLEME HATASI
//
// Ama sayaç dışarıda tanımlıysa koruma yoktur.
// Aşağıdaki satırların başındaki // işaretini kaldırıp deneyin.
// UYARI: sonsuz döngüye girer. Ctrl + C ile durdurun.
//
// int k;
// for (k = 0; k < 5; k++)
// {
//     for (k = 0; k < 3; k++)      // aynı sayacı sıfırlıyor
//     {
//         Console.WriteLine("Bu satır asla bitmez");
//     }
// }
//
// Neden? İç döngü her turda k'yı 0'a çekiyor, dış döngü 5'e asla ulaşamıyor.
// Sınırları ters çevirirseniz (dış < 3, iç < 5) program biter —
// ama gövde beklenenden çok daha az çalışır. Sessiz mantık hatası.
//
// KURAL: sayacı her zaman for içinde tanımlayın: for (int i = ...)
