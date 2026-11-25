namespace YansimaLab;

// GÖREV 1: Özniteliklere bakarak doğrulama.
//
// Nesnenin public örnek özelliklerini TANIMLANDIKLARI SIRAYLA gezin ve
// üzerlerindeki [Zorunlu], [EnFazlaUzunluk], [Aralik] özniteliklerine göre
// hata mesajlarını toplayın. Bir özellik için bakma sırası: Zorunlu,
// EnFazlaUzunluk, Aralik. Mesajlar tam olarak şu biçimde:
//
//   "Ad zorunlu"
//   "Ad en fazla 10 karakter olabilir (12)"         parantez: gerçek uzunluk
//   "KitapSiniri 1 ile 5 arasında olmalı (7)"       parantez: gerçek değer
//
// Hata yoksa boş liste. nesne null ise ArgumentNullException.
//
// İpuçları: GetProperties() tanım sırasını garanti etmez; MetadataToken'a
// göre sıralayın (01 dosyası). GetCustomAttribute<T>() (02 dosyası).
public static class Dogrulayici
{
    public static IReadOnlyList<string> Dogrula(object nesne)
    {
        throw new NotImplementedException();
    }
}
