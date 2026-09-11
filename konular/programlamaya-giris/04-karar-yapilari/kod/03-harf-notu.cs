// else if zinciri: ikiden fazla durum.
//
// Çalıştırmak için:  dotnet run 03-harf-notu.cs
//
// KİLİT NOKTA: Koşullar yukarıdan aşağıya SIRAYLA denenir.
// İlk doğru olan çalışır, geri kalanlar hiç kontrol edilmez.

Console.Write("Notunuzu giriniz (0-100): ");
int notu = Convert.ToInt32(Console.ReadLine());

if (notu >= 90)
{
    Console.WriteLine("Harf notunuz: AA");
}
else if (notu >= 80)
{
    Console.WriteLine("Harf notunuz: BA");
}
else if (notu >= 70)
{
    Console.WriteLine("Harf notunuz: BB");
}
else if (notu >= 60)
{
    Console.WriteLine("Harf notunuz: CC");
}
else
{
    Console.WriteLine("Harf notunuz: FF — Kaldınız.");
}

// --- Denemeniz için: SIRA NEDEN ÖNEMLİ? ---
//
// Aşağıdaki zinciri deneyin. Sıra ters: en küçük koşul en üstte.
// 95 girdiğinizde ne yazar? Neden AA değil?
//
// if (notu >= 60)       { Console.WriteLine("CC"); }
// else if (notu >= 70)  { Console.WriteLine("BB"); }
// else if (notu >= 80)  { Console.WriteLine("BA"); }
// else if (notu >= 90)  { Console.WriteLine("AA"); }
//
// Bu kod DERLENIR ve ÇALIŞIR — ama sonuç yanlıştır.
// Derleyici sizi bu hatadan koruyamaz. Sadece siz koruyabilirsiniz.
