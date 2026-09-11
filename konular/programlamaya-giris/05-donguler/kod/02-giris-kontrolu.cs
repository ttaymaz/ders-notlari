// && operatörüyle iki koşulun aynı anda sağlanması.
//
// Çalıştırmak için:  dotnet run 02-giris-kontrolu.cs

string dogruKullaniciAdi = "admin";
string dogruSifre = "12345";

Console.Write("Kullanıcı adı: ");
string girilenAd = Console.ReadLine();

Console.Write("Şifre: ");
string girilenSifre = Console.ReadLine();

// İKİ koşul da doğru olmalı — && operatörü bunu sağlıyor.
if (girilenAd == dogruKullaniciAdi && girilenSifre == dogruSifre)
{
    Console.WriteLine("Giriş başarılı! Hoş geldiniz.");
}
else
{
    Console.WriteLine("Kullanıcı adı veya şifre hatalı!");
}

// --- Denemeniz için ---
// "Admin" yazın (baş harf büyük). Neden kabul etmiyor?
// C# metin karşılaştırmasında büyük/küçük harfe duyarlıdır.
//
// NOT: Gerçek bir uygulamada şifre asla kodun içinde açık metin
// olarak tutulmaz. Bu yalnızca && operatörünü göstermek için bir örnek.
