// PROVA 2 — derlenir mi? (dönemin ikinci yarısı)
//
// "Bu kod derlenir mi? Derlenmiyorsa neden?" Bu dosya olduğu haliyle
// derlenir ve çalışır. Her soruda bir satırın başında  //?  işareti var.
// Soruyu şöyle çalışın:
//
//   1. Satırı OKUYUN. Kâğıda üç cevaptan birini yazın:
//        derlenir · derlenir ama UYARI verir · DERLENMEZ
//      ve "uyarı" ya da "derlenmez" dediyseniz nedenini bir cümleyle yazın.
//   2. Satırın başındaki  //?  işaretini silin ve çalıştırın.
//   3. Derleyicinin dediğini kendi cümlenizle karşılaştırın, sonra
//      işareti geri koyun. Aynı anda yalnızca BİR satırı açın.
//
// Hata kodunu ezberlemeniz gerekmez; sınavda sorulan, NEDEN'dir.
//
// Çalıştırmak için:  dotnet run 02-derlenir-mi.cs

// ---- Soru 1 (9. hafta: catch sırası)
try { Console.WriteLine("dene"); }
catch (InvalidOperationException) { Console.WriteLine("geçersiz"); }
catch (Exception) { Console.WriteLine("genel"); }
//? catch (ArgumentException) { Console.WriteLine("argüman"); }

// ---- Soru 2 (11. hafta: beklenmeyen görev)
await Sorular.BekleyenAsync();

// ---- Soru 3 (11. hafta: async void)
Sorular.Kaydet();

// ---- Soru 4 (12. hafta: kilit ve await)
await new Sayac().ArtirAsync();

// ---- Soru 5 (13. hafta: özniteliğin yeri)
Console.WriteLine(new Menu().Listele());

// ---- Soru 6 (13. hafta: eskimiş metot)
//? Console.WriteLine(new Menu().EskiListele());

// ---- Soru 7 (14. hafta: Span ve lambda)
Span<int> tampon = stackalloc int[4];
tampon[0] = 7;
//? Action yaz = () => Console.WriteLine(tampon[0]);
Console.WriteLine(tampon[0]);

static class Sorular
{
    public static async Task BekleyenAsync()
    {
        //? Task.Delay(10);
        await Task.Delay(10);
    }

    public static void Kaydet() => Console.WriteLine("kaydedildi");
    //? public static async void KaydetAsync() => await Task.Delay(10);
}

class Sayac
{
    private readonly object _kilit = new();
    private int _deger;

    public async Task ArtirAsync()
    {
        lock (_kilit)
        {
            _deger++;
            //? await Task.Yield();
        }
        await Task.Yield();
    }
}

[AttributeUsage(AttributeTargets.Method)]
class KomutAttribute : Attribute;

//? [Komut]
class Menu
{
    [Komut]
    public string Listele() => "liste";

    [Obsolete("Listele kullanın.")]
    public string EskiListele() => "eski liste";
}

// ---- Soru 8 (14. hafta: Span bir sınıf alanı olabilir mi?)
class Ayristirici
{
    //? private Span<char> _tampon;
    public int Uzunluk(ReadOnlySpan<char> metin) => metin.Length;
}
