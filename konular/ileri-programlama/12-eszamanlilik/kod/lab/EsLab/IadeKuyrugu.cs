using System.Threading.Channels;

namespace EsLab;

// GÖREV 4: İade kuyruğu (üretici–tüketici).
//
// İade edilen kitaplar EkleAsync ile kuyruğa yazılır (üretici). Kurucu
// `tuketiciSayisi` kadar tüketici görevi başlatır; her tüketici kuyruktan bir
// kitap alır ve `yerlestir` fonksiyonuyla rafa yerleştirir.
//
// - Kuyruğun kapasitesi sınırlıdır: kuyrukta `kapasite` kitap varken
//   EkleAsync, yer açılana kadar BEKLER (tamamlanmamış görev döndürür).
// - TamamlaAsync: "başka kitap gelmeyecek" der ve bütün tüketiciler
//   ellerindeki işi bitirene kadar bekler.
// - Her kitap tam olarak bir kez yerleştirilir. Tek tüketici varsa
//   yerleştirme sırası ekleme sırasıdır.
// - kapasite ya da tuketiciSayisi 1'den küçükse ArgumentOutOfRangeException.
//
// İpucu: Channel.CreateBounded<string>(kapasite), Writer.WriteAsync,
// Writer.Complete, Reader.ReadAllAsync (05 dosyası).
public sealed class IadeKuyrugu
{
    public IadeKuyrugu(int kapasite, int tuketiciSayisi, Func<string, Task> yerlestir)
    {
        throw new NotImplementedException();
    }

    public Task EkleAsync(string kitap, CancellationToken iptal = default)
    {
        throw new NotImplementedException();
    }

    public Task TamamlaAsync()
    {
        throw new NotImplementedException();
    }
}
