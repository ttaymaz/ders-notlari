using System.Reflection;

namespace Uygulama;

// Bir derlemenin (DLL) içindeki, verilen arayüzü uygulayan sınıfları bulur
// ve kurar. Uygulama hangi sınıfların olduğunu derlerken bilmez.
public static class EklentiYukleyici
{
    // Kurulabilen her T: sınıf, soyut değil, public, parametresiz kurucusu var.
    // Sıra: tipin tam adı (her çalıştırmada aynı sıra olsun diye).
    public static IReadOnlyList<T> Bul<T>(Assembly derleme) where T : class
    {
        ArgumentNullException.ThrowIfNull(derleme);

        return derleme.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic)
            .Where(t => typeof(T).IsAssignableFrom(t))
            .Where(t => t.GetConstructor(Type.EmptyTypes) is not null)
            .OrderBy(t => t.FullName, StringComparer.Ordinal)
            .Select(t => (T)Activator.CreateInstance(t)!)
            .ToList();
    }

    // Klasördeki her DLL'i yükler ve içindeki eklentileri toplar.
    // Klasör yoksa eklenti de yoktur; bu bir hata değildir.
    public static IReadOnlyList<T> KlasordenYukle<T>(string klasor) where T : class
    {
        if (!Directory.Exists(klasor))
            return [];

        return Directory.GetFiles(klasor, "*.dll")
            .Order(StringComparer.Ordinal)
            .SelectMany(yol => Bul<T>(Assembly.LoadFrom(yol)))
            .ToList();
    }
}
