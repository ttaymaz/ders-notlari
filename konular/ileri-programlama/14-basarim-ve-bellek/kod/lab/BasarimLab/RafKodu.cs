namespace BasarimLab;

// GÖREV 1: Raf kodunu bellek ayırmadan ayrıştırmak.
//
// Geçerli biçim tam olarak: "TR-" + üç rakam + "-" + iki rakam
// (ör. "TR-813-07" → bölüm 813, raf 7). Başka her şey geçersizdir:
// küçük harf "tr", eksik ya da fazla rakam, boşluk, farklı ayraç.
//
// Geçerliyse bolum ve raf doldurulur, true döner. Geçersizse ikisi de 0,
// false döner. İstisna fırlatılmaz.
//
// Kural: metot hiç bellek ayırmaz (0 bayt). Split, Substring, Regex yok.
// İpucu: ReadOnlySpan<char>.Slice, char.IsAsciiDigit, int.TryParse'ın
// ReadOnlySpan<char> alan aşırı yüklemesi (03 dosyası).
public static class RafKodu
{
    public static bool TryAyristir(ReadOnlySpan<char> metin, out int bolum, out int raf)
    {
        throw new NotImplementedException();
    }
}
