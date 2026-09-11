// Temel veri tipleri ve tırnak kuralları.
//
// Çalıştırmak için:  dotnet run 03-veri-tipleri.cs

string ad = "Mehmet";        // metin      → ÇİFT tırnak
char harf = 'M';             // tek karakter → TEK tırnak
int adet = 42;               // tam sayı   → tırnak YOK
double boy = 1.78;           // ondalıklı  → nokta ile, tırnak YOK
decimal fiyat = 129.90m;     // para       → sonuna m
bool aktifMi = true;         // doğru/yanlış

Console.WriteLine(ad);
Console.WriteLine(harf);
Console.WriteLine(adet);
Console.WriteLine(boy);
Console.WriteLine(fiyat);
Console.WriteLine(aktifMi);

// --- Denemeniz için: aşağıdaki satırların başındaki // işaretini kaldırın ---
// Her biri derleyici hatası verir. Hata mesajını okuyun, ne dediğini anlayın.

// string sehir = 'Afyon';   // metin için tek tırnak kullanılamaz
// int sayi = "50";          // sayıya tırnaklı değer atanamaz
// int tam = 3.14;           // ondalıklı sayı int kutusuna sığmaz
// Console.WriteLine(Ad);    // C# büyük/küçük harfe duyarlıdır
