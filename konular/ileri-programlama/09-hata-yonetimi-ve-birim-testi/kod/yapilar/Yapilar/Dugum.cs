namespace Yapilar;

// Yığın ve kuyruğun ortak yapı taşı. Kitaplığın dışına açılmaz.
internal class Dugum<T>(T deger)
{
    public T Deger { get; } = deger;
    public Dugum<T>? Sonraki { get; set; }
}
