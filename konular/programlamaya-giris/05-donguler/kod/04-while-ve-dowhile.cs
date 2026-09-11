// while ve do-while: aradaki tek fark koşulun NEREDE kontrol edildiği.
//
// Çalıştırmak için:  dotnet run 04-while-ve-dowhile.cs

// --- while: ÖNCE SOR, sonra yap ---
Console.WriteLine("Metin giriniz ('exit' yazarak çıkabilirsiniz):");
string metin = Console.ReadLine();

while (metin != "exit")
{
    Console.WriteLine($"Yazdınız: {metin}");
    metin = Console.ReadLine();   // <- koşulu güncelleyen satır. Silmeyin!
}

Console.WriteLine("Döngüden çıkıldı.");

// --- do-while: ÖNCE YAP, sonra sor ---
// Gövde, koşul ne olursa olsun EN AZ BİR KEZ çalışır.

int sayac = 100;

do
{
    Console.WriteLine($"Bu satır koşul yanlış olsa bile çalıştı. sayac = {sayac}");
} while (sayac < 5);   // 100 < 5 yanlış, ama gövde bir kez çalıştı

// --- SONSUZ DÖNGÜ TEHLİKESİ ---
// while döngüsünün içinde koşulu değiştirecek bir satır YOKSA
// program sonsuza kadar döner ve kilitlenir.
// Birinci haftadaki "sonluluk" kuralını hatırlayın.
//
// Kilitlenirse: Ctrl + C ile durdurun.
