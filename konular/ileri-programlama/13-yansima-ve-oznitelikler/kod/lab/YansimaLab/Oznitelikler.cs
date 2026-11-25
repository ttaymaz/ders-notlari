namespace YansimaLab;

// Laboratuvarın öznitelikleri. HAZIR; değiştirmeyin.

// --- Görev 1: doğrulama ---

// string: null, boş ya da yalnızca boşluk olamaz. Öteki tipler: null olamaz.
[AttributeUsage(AttributeTargets.Property)]
public sealed class ZorunluAttribute : Attribute;

// int: [EnAz, EnCok] aralığında olmalı (iki uç dahil)
[AttributeUsage(AttributeTargets.Property)]
public sealed class AralikAttribute(int enAz, int enCok) : Attribute
{
    public int EnAz { get; } = enAz;
    public int EnCok { get; } = enCok;
}

// string: en fazla Uzunluk karakter. null ise bu kural bakmaz.
[AttributeUsage(AttributeTargets.Property)]
public sealed class EnFazlaUzunlukAttribute(int uzunluk) : Attribute
{
    public int Uzunluk { get; } = uzunluk;
}

// --- Görev 2: komut tablosu ---

[AttributeUsage(AttributeTargets.Method)]
public sealed class KomutAttribute(string ad) : Attribute
{
    public string Ad { get; } = ad;
}

// --- Görev 3: test koşucusu ---

[AttributeUsage(AttributeTargets.Method)]
public sealed class SinamaAttribute : Attribute;

// [Sinama] ile birlikte kullanılır: sınama çalıştırılmaz, atlandı sayılır
[AttributeUsage(AttributeTargets.Method)]
public sealed class AtlaAttribute(string neden) : Attribute
{
    public string Neden { get; } = neden;
}
