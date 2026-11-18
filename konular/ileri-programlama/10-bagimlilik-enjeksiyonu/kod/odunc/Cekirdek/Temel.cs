namespace Cekirdek;

// Dönem projesi şablonundaki iki tip, burada da aynen var.

// Başarısız olabilecek işlemlerin sonucu (9. hafta)
public abstract record Sonuc<T>;
public sealed record Basarili<T>(T Deger) : Sonuc<T>;
public sealed record Hatali<T>(string Neden) : Sonuc<T>;

// Bir tipteki kayıtların tamamını okuyup yazan depo. Uygulamada arkasında
// bir JSON dosyası durur; testlerde bellekte bir liste.
public interface IDepo<T>
{
    Task<IReadOnlyList<T>> OkuAsync(CancellationToken iptal = default);

    Task YazAsync(IReadOnlyList<T> ogeler, CancellationToken iptal = default);
}
