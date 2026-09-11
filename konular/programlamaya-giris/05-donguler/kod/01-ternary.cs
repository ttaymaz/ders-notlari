// Ternary (üçlü) operatör: basit if-else için kısayol.
//
// Çalıştırmak için:  dotnet run 01-ternary.cs

int sayi = 10;

// --- Uzun yol: if-else ---
string sonuc1;
if (sayi % 2 == 0)
{
    sonuc1 = "Çift";
}
else
{
    sonuc1 = "Tek";
}

// --- Kısa yol: ternary ---
string sonuc2 = (sayi % 2 == 0) ? "Çift" : "Tek";

Console.WriteLine($"if-else ile: {sonuc1}");
Console.WriteLine($"ternary ile: {sonuc2}");

// Sözdizimi:
//   degisken = (koşul) ? doğruysa_değer : yanlışsa_değer;

// Yalnızca BASİT ATAMA işlemleri için kullanın.
// İçine üç satır kod sığdırmaya çalışırsanız kod okunmaz hale gelir.
