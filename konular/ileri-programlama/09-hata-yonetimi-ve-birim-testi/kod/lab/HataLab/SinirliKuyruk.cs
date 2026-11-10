namespace HataLab;

// GÖREV 1 — Kapasiteli kuyruk.
//
// En fazla "kapasite" kadar eleman alan, ilk giren ilk çıkar kuyruğu.
// İçeride List<T> kullanabilirsiniz; bu görevin konusu veri yapısı değil,
// HATA SÖZLEŞMESİ:
//
//   Kurucu   kapasite 0 veya negatifse  → ArgumentOutOfRangeException
//   Ekle     eleman null ise             → ArgumentNullException
//            kuyruk doluysa              → InvalidOperationException("Kuyruk dolu.")
//   TryEkle  kuyruk doluysa              → false (istisna YOK); null için yine ArgumentNullException
//   Cikar    kuyruk boşsa                → InvalidOperationException("Kuyruk boş.")
//
// Testler istisnanın TÜRÜNE bakar: ArgumentOutOfRangeException beklenen
// yerde ArgumentException fırlatmak testi geçmez. Koruma cümleleri için
// ArgumentOutOfRangeException.ThrowIfNegativeOrZero ve
// ArgumentNullException.ThrowIfNull hazır.
public class SinirliKuyruk<T>
{
    public SinirliKuyruk(int kapasite)
    {
        throw new NotImplementedException();
    }

    public int Sayi => throw new NotImplementedException();

    public void Ekle(T deger)
    {
        throw new NotImplementedException();
    }

    public bool TryEkle(T deger)
    {
        throw new NotImplementedException();
    }

    public T Cikar()
    {
        throw new NotImplementedException();
    }
}
