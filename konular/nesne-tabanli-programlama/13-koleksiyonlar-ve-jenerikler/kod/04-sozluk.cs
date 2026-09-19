// Dictionary<TKey, TValue>: sıra numarasıyla değil, anahtarla erişim.
//
// Bahar dönemindeki otomasyonda kayıtları veritabanından anahtarla
// çekeceksiniz. Sözlük, o düşünce biçiminin bellek içindeki hâlidir.
//
// Şema: assets/02-liste-mi-sozluk-mu.svg
// Çalıştırmak için:  dotnet run 04-sozluk.cs

// --- KURMA ---
Dictionary<int, string> defter = new Dictionary<int, string>();

defter.Add(101, "Tutunamayanlar");
defter.Add(102, "Sefiller");
defter.Add(201, "Bilim ve Teknik");

// İndisleyiciyle de eklenir; anahtar varsa ÜZERİNE YAZAR, hata vermez.
defter[202] = "Arkitekt";

Console.WriteLine($"Defterde {defter.Count} kayıt var.\n");

// --- ANAHTARLA OKUMA ---
Console.WriteLine($"101 numara: {defter[101]}");

// --- OLMAYAN ANAHTAR: iki yol ---

// Yanlış yol: doğrudan okumak. Anahtar yoksa program çöker.
// Console.WriteLine(defter[999]);        // KeyNotFoundException

// Doğru yol 1: önce sor
if (defter.ContainsKey(999))
{
    Console.WriteLine(defter[999]);
}
else
{
    Console.WriteLine("999 numara: kayıtlı değil");
}

// Doğru yol 2: tek adımda sor ve al
if (defter.TryGetValue(102, out string? bulunan))
{
    Console.WriteLine($"102 numara: {bulunan}");
}

// --- AYNI ANAHTARI İKİ KEZ EKLEMEK ---
// defter.Add(101, "Başka Kitap");        // ArgumentException: anahtar zaten var
//
// Add benzersizlik ister. İndisleyici (defter[101] = ...) istemez.
defter[101] = "Tutunamayanlar (2. baskı)";
Console.WriteLine($"\n101 güncellendi: {defter[101]}");

// --- GEZME ---
Console.WriteLine("\n--- Tüm kayıtlar ---");
foreach (KeyValuePair<int, string> kayit in defter)
{
    Console.WriteLine($"  {kayit.Key} -> {kayit.Value}");
}

Console.WriteLine("\n--- Yalnızca anahtarlar ---");
Console.WriteLine("  " + string.Join(", ", defter.Keys));

// --- SİLME ---
defter.Remove(201);
Console.WriteLine($"\n201 silindi. Kalan: {defter.Count}");


// --- DEĞER OLARAK NESNE TUTMAK ---
Console.WriteLine("\n=== NESNE SAKLAYAN SÖZLÜK ===");

List<Uye> uyeler = new List<Uye>
{
    new Uye(1001, "Ayşe Kaya"),
    new Uye(1002, "Mehmet Demir"),
    new Uye(1003, "Zeynep Ak"),
};

// Listeyi sözlüğe aktaralım: anahtar üye no, değer üyenin kendisi.
Dictionary<int, Uye> uyeDefteri = new Dictionary<int, Uye>();

foreach (Uye u in uyeler)
{
    uyeDefteri[u.UyeNo] = u;
}

int aranan = 1002;

if (uyeDefteri.TryGetValue(aranan, out Uye? kisi))
{
    Console.WriteLine($"{aranan}: {kisi.Ad}");
}
else
{
    Console.WriteLine($"{aranan}: bulunamadı");
}

// Listede aynı işi yapmak için tüm listeyi gezmek gerekirdi:
Uye? listeyleBulunan = null;

foreach (Uye u in uyeler)
{
    if (u.UyeNo == aranan)
    {
        listeyleBulunan = u;
        break;
    }
}

Console.WriteLine($"Listeyle arama sonucu: {listeyleBulunan?.Ad}");
Console.WriteLine("Aynı cevap; ama liste tüm elemanları gezmek zorunda kaldı.");


class Uye
{
    public int UyeNo { get; private set; }
    public string Ad { get; private set; }

    public Uye(int no, string ad)
    {
        UyeNo = no;
        Ad = ad;
    }
}

// --- Denemeniz için ---
//
// 1. `defter[999]` satırının yorumunu kaldırın. Hangi hata gelir? Mesajı
//    okuyun: sözlük, olmayan anahtarı sessizce geçmez.
//
// 2. `defter.Add(101, "Başka Kitap");` satırının yorumunu kaldırın. Hata
//    mesajı ne diyor? Sonra aynı satırı `defter[101] = "Başka Kitap";`
//    yapın. Neden biri hata verip diğeri vermiyor?
//
// 3. `Dictionary<int, string>` yerine `Dictionary<string, string>` yapın ve
//    anahtarları "101", "102" gibi metin yazın. `defter["101"]` ile
//    `defter[" 101"]` aynı kaydı bulur mu? Anahtar eşitliği tam eşleşmedir.
//
// 4. Üye listesine aynı numaradan ikinci bir üye ekleyin. `uyeDefteri[u.UyeNo] = u;`
//    satırı hata veriyor mu? Vermiyorsa hangi üye kayboldu? Bunu istiyor
//    muydunuz? `Add` kullansaydınız ne olurdu?
//
// 5. Listeyle arama döngüsünden `break;` satırını silin. Sonuç değişti mi?
//    Değişmediyse bile neden orada durmalı?
