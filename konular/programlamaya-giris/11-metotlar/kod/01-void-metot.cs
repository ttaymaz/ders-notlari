// En basit metot: parametresiz, değer döndürmeyen (void).
//
// Çalıştırmak için:  dotnet run 01-void-metot.cs

// --- Metodu tanımlıyoruz ---
void SelamVer()
{
    Console.WriteLine("Merhaba! Sisteme hoş geldiniz.");
    Console.WriteLine("Lütfen işleminizi seçiniz.");
}

// --- Metodu çağırıyoruz ---
SelamVer();
Console.WriteLine("---");
SelamVer();          // istediğimiz kadar çağırabiliriz

// void = "geriye bir şey vermez". Metot işini yapar, biter.
//
// NOT: Üst düzey ifadeler kullandığımız için metotları doğrudan
// yazabiliyoruz. İnternetteki örneklerde "static void SelamVer()"
// biçimini göreceksiniz — o, class ve Main içeren eski yapı içindir.
// İkisi de aynı işi yapar.
