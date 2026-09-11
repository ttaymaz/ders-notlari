// Geçerli veri girilene kadar sormaya devam etmek.
//
// Çalıştırmak için:  dotnet run 04-guvenli-giris.cs
//
// Bu, gerçek programlarda en sık kullanılan kalıptır.
// Beşinci haftanın do-while döngüsü burada işe yarıyor.

int yas;

do
{
    Console.Write("Yaşınızı giriniz (0-120): ");
    string giris = Console.ReadLine();

    if (!int.TryParse(giris, out yas))
    {
        Console.WriteLine("  Hatalı giriş — sayı olmalı.");
        yas = -1;                    // döngü devam etsin
    }
    else if (yas < 0 || yas > 120)
    {
        Console.WriteLine("  Mantıksız yaş — 0 ile 120 arasında olmalı.");
        yas = -1;
    }

} while (yas < 0);

Console.WriteLine($"Teşekkürler. Yaşınız: {yas}");

// İki farklı kontrol var ve ikisi de gerekli:
//   1) TryParse  → veri SAYI mı?
//   2) aralık    → sayı ANLAMLI mı?
//
// "127 yaşındayım" girişi bir sayıdır ama anlamsızdır.
// TryParse bunu yakalayamaz — mantık kontrolü sizin işiniz.
