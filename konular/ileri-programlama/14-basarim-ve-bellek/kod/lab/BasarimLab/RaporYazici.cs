namespace BasarimLab;

public sealed record Odunc(string Uye, string Baslik, int GecikmeGun);

// GÖREV 4: Rapor metnini az bellekle üretmek.
//
// Her ödünç bir satır: üye, başlık ve gecikme günü SEKME (\t) ile
// ayrılmış. Satırlar '\n' ile ayrılır, son satırdan sonra '\n' yok.
//
//   Ece<TAB>Huzur<TAB>3
//   Can<TAB>Aylak Adam<TAB>0
//
// Boş listede boş metin. Sayı kültürden bağımsız yazılır.
//
// Kural: 2000 satırlık bir rapor için ayrılan toplam bellek, üretilen
// metnin kendi boyutunun 5 katından az olmalı. Satırları += ile eklemek
// bunu yüzlerce kat aşar (02 dosyasının 1. bölümü).
public static class RaporYazici
{
    public static string Yaz(IReadOnlyList<Odunc> odunclar)
    {
        throw new NotImplementedException();
    }
}
