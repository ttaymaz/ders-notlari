// Diziyi gezmenin iki yolu: for ve foreach.
//
// Çalıştırmak için:  dotnet run 01-dizi-gezme.cs

string[] sehirler = { "Afyonkarahisar", "İstanbul", "Ankara", "İzmir" };

// --- for: indisle gezer ---
Console.WriteLine("for ile:");
for (int i = 0; i < sehirler.Length; i++)
{
    Console.WriteLine($"  {i}. {sehirler[i]}");
}

// --- foreach: elemanla gezer ---
Console.WriteLine("\nforeach ile:");
foreach (string sehir in sehirler)
{
    Console.WriteLine($"  {sehir}");
}

// DİKKAT: koşul  i < Length  şeklindedir, i <= Length DEĞİL.
// 4 elemanlı dizinin son indisi 3'tür; i=4 olursa program çöker.

// --- Denemeniz için ---
// Yukarıdaki for döngüsünde < yerine <= yazın. Ne oluyor?
// Hata mesajını okuyun — geçen haftadan tanıdık gelecek.
