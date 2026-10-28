// PROVA 3 — kısa kod yazma.
//
// Sınavda kod okumanın yanında birkaç satırlık kod yazmanız da istenebilir:
// bir metodun gövdesini tamamlamak, bir lambdayı ya da bir switch ifadesini
// yazmak. Bu dosyadaki altı görev o biçimdedir.
//
// NASIL ÇALIŞILIR:
//   1. Her görevi ÖNCE KÂĞIDA yazın. Sınavda derleyici yok.
//   2. Sonra kâğıttakini buraya geçirip çalıştırın. Denetim kaç görevin
//      tamam olduğunu söyler. Kâğıtta yaptığınız hatayı not edin.
//   3. Denetim kodunu değiştirmeyin.
//
// Her TODO şu an NotImplementedException fırlatıyor.
//
// Çalıştırmak için:  dotnet run 03-yazma-provasi.cs
// .NET 9:            kod/README.md içindeki proje yolu

// ---------------------------------------------------------------- DENETİM
int tamam = 0, toplam = 0;

void Denetle(string ad, Func<bool> sinama)
{
    toplam++;
    try
    {
        bool sonuc = sinama();
        if (sonuc) tamam++;
        Console.WriteLine($"[{(sonuc ? "TAMAM" : "YANLIŞ")}] {ad}");
    }
    catch (NotImplementedException)
    {
        Console.WriteLine($"[YAPILMADI] {ad}");
    }
    catch (Exception hata)
    {
        Console.WriteLine($"[HATA] {ad}: {hata.GetType().Name}: {hata.Message}");
    }
}

// Bitmeyen bir kaynak. 10.000 elemandan fazlası istenirse durur: tam
// ertelenmiş bir yineleyici bu kadarına hiç ihtiyaç duymaz.
IEnumerable<int> Sonsuz()
{
    for (int i = 0; ; i++)
    {
        if (i == 10_000)
            throw new InvalidOperationException("kaynağın tamamı okunmaya çalışıldı; yineleyici ertelenmiş değil");
        yield return i;
    }
}

var olcumler = new List<Olcum>
{
    new("Afyon", 31), new("Afyon", -4), new("Konya", 28),
    new("Konya", 38), new("Ankara", 12),
};

Denetle("1. KacTane: 3 çift sayı",
    () => new[] { 1, 2, 3, 4, 6 }.KacTane(x => x % 2 == 0) == 3);
Denetle("1. KacTane: boş dizide 0",
    () => Array.Empty<string>().KacTane(s => s.Length > 0) == 0);
Denetle("2. Ciftler: 1 2 3 4 5 6 → 2 4 6",
    () => string.Join(" ", Ciftler([1, 2, 3, 4, 5, 6])) == "2 4 6");
Denetle("2. Ciftler ertelenmiş mi: sonsuz kaynaktan ilk üç",
    () => string.Join(" ", Ciftler(Sonsuz()).Take(3)) == "0 2 4");
Denetle("3. EnSicak: Konya 38, Afyon 31, Ankara 12",
    () => EnSicak(olcumler) is { Count: 3 } d && d["Konya"] == 38 && d["Afyon"] == 31 && d["Ankara"] == 12);
Denetle("4. Uyari: don, Afyon sıcak, aşırı sıcak, normal",
    () => string.Join(", ", olcumler.Select(Uyari))
          == "Afyon için sıcak, don, normal, aşırı sıcak, normal");
Denetle("4. Uyari: Afyon 40 derece → Afyon için sıcak (kol sırası)",
    () => Uyari(new("Afyon", 40)) == "Afyon için sıcak");
Denetle("5. Bilesik: önce f sonra g → (2 + 1) * 10 = 30",
    () => Bilesik(x => x + 1, x => x * 10)(2) == 30);
Denetle("6. Dengeli: \"(a[b]{c})\" dengeli",
    () => Dengeli("(a[b]{c})"));
Denetle("6. Dengeli: \"(]\" ve \"([)]\" dengeli değil",
    () => !Dengeli("(]") && !Dengeli("([)]"));
Denetle("6. Dengeli: \"((\" ve \")(\" dengeli değil",
    () => !Dengeli("((") && !Dengeli(")("));

Console.WriteLine($"\n{tamam}/{toplam} denetim tamam");

// ---------------------------------------------------------------- GÖREVLER

// TODO 2: Yalnızca çift sayıları döndürün. yield return kullanın; liste
//         kurmayın. (Denetim, sonsuz bir kaynakla deniyor.)
IEnumerable<int> Ciftler(IEnumerable<int> kaynak)
{
    throw new NotImplementedException();
}

// TODO 3: Her şehir için ölçülen en yüksek sıcaklık. LINQ kullanın.
Dictionary<string, int> EnSicak(IEnumerable<Olcum> liste)
{
    throw new NotImplementedException();
}

// TODO 4: Bir switch ifadesiyle (özellik desenleri) yazın:
//         sıcaklık 0'ın altındaysa             → "don"
//         şehir Afyon ve sıcaklık 30'un üstü    → "Afyon için sıcak"
//         sıcaklık 35'in üstü                  → "aşırı sıcak"
//         geri kalan her şey                   → "normal"
//         Kolların sırasını düşünün.
string Uyari(Olcum o)
{
    throw new NotImplementedException();
}

// TODO 5: Önce f'yi, sonra sonucuna g'yi uygulayan YENİ bir fonksiyon
//         döndürün. Bilesik(f, g)(x) == g(f(x)) olmalı.
Func<int, int> Bilesik(Func<int, int> f, Func<int, int> g)
{
    throw new NotImplementedException();
}

// TODO 6: Parantezler dengeli mi? ( ) [ ] { } dışındaki karakterleri
//         atlayın. İpucu: açılanı Stack<char>'a koyun; kapanan geldiğinde
//         yığının üstündekiyle eşleşmeli. Sonunda yığın boş kalmalı.
bool Dengeli(string metin)
{
    throw new NotImplementedException();
}

record Olcum(string Sehir, int Sicaklik);

static class Uzantilar
{
    // TODO 1: Koşulu sağlayan eleman sayısı. LINQ'in Count'unu KULLANMADAN,
    //         foreach ile yazın. Bu bir genişletme metodu: ilk parametredeki
    //         "this" sayesinde dizi.KacTane(...) diye çağrılabiliyor.
    public static int KacTane<T>(this IEnumerable<T> kaynak, Func<T, bool> kosul)
    {
        throw new NotImplementedException();
    }
}
