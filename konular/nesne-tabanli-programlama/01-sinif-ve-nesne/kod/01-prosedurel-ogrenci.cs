// ÖNCE: Prosedürel yaklaşım — veri bir yanda, onu işleyen metotlar başka yanda.
// Bir veriyi tutmak için dizi, onu işlemek için ayrı metotlar: tanıdık bir düzen.
//
// Şema: assets/01-prosedurel-vs-nesne.svg (üst kutu)
// Çalıştırmak için:  dotnet run 01-prosedurel-ogrenci.cs

// Üç ayrı dizi. Aynı öğrencinin bilgileri AYNI İNDİSTE durmak zorunda:
// adlar[1], vizeler[1] ve finaller[1] hep aynı kişiye ait olmalı.
string[] adlar = { "Ayşe Yılmaz", "Mehmet Demir", "Zeynep Kaya" };
int[] vizeler = { 70, 45, 88 };
int[] finaller = { 80, 40, 92 };

// Metotlar veriyi PARAMETRE olarak almak zorunda: metot, verinin
// nerede durduğunu bilmiyor. Her çağrıda doğru diziden doğru indisi
// çekip elimizle taşıyoruz.
double Ortalama(int vize, int final)
{
    return vize * 0.4 + final * 0.6;
}

string Durum(double ortalama)
{
    return ortalama >= 50 ? "Geçti" : "Kaldı";
}

void SatirYazdir(string ad, int vize, int final)
{
    double ort = Ortalama(vize, final);
    Console.WriteLine($"{ad,-22} {vize,4} {final,5} {ort,6:F1}   {Durum(ort)}");
}

Console.WriteLine($"{"Ad Soyad",-22} {"Vize",4} {"Final",5} {"Ort.",6}   Durum");
Console.WriteLine(new string('-', 52));

for (int i = 0; i < adlar.Length; i++)
{
    SatirYazdir(adlar[i], vizeler[i], finaller[i]);
}

// --- BU KOD ÇALIŞIYOR. PEKİ SORUNU NE? ---
//
// 1. ÜÇ DİZİ ELLE SENKRON TUTULUYOR.
//    Bir öğrenciyi listeden silmek isterseniz üç diziden de aynı indisi
//    silmeniz gerekir. Birini unutursanız program çökmez — Mehmet'in
//    vizesi Zeynep'in adıyla yazılır. Sessiz ve tehlikeli bir hata.
//
// 2. YENİ BİR BİLGİ EKLEMEK HER YERE DOKUNMAK DEMEK.
//    "Öğrencinin bölümü" bilgisini ekleyin: yeni bir dizi açacak,
//    SatirYazdir metodunun imzasını değiştirecek ve metodu çağıran
//    her satırı güncelleyeceksiniz.
//
// 3. VERİ KORUNMASIZ.
//    vizeler[0] = -500; yazan hiçbir şey yok. Dizi buna izin verir.
//
// Bu üç sorunun ortak sebebi şu: VERİ İLE ONU İŞLEYEN DAVRANIŞ
// BİRBİRİNDEN KOPUK. Aynı programı 02-nesne-ogrenci.cs dosyasında
// nesnelerle yazdık; iki dosyayı yan yana açıp karşılaştırın.
