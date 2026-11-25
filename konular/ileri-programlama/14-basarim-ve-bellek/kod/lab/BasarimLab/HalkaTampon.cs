namespace BasarimLab;

// GÖREV 2: Halka tampon (ring buffer): sabit kapasiteli bir kuyruk.
//
// Son N kaydı tutar: "son 100 ödünç işlemi", "son 50 günlük satırı".
// Doluyken eklenen öğe EN ESKİ öğenin yerine yazılır. Böylece tampon hiç
// büyümez ve ekleme hiç bellek ayırmaz.
//
// - Kurucu kapasite kadar yeri BİR KEZ ayırır. kapasite < 1 ise
//   ArgumentOutOfRangeException
// - Ekle: 0 bayt ayırır (testte 10 000 ekleme ölçülüyor)
// - Sayi: içindeki öğe sayısı (en fazla Kapasite)
// - ToArray: öğeler en eskiden en yeniye
//
// İpucu: bir dizi, bir "baş" indeksi ve bir sayaç. Sonraki yazma yeri
// (bas + Sayi) % Kapasite. 5. haftanın kuyruğunu dizi üzerinde düşünün.
public sealed class HalkaTampon<T>
{
    public HalkaTampon(int kapasite)
    {
        throw new NotImplementedException();
    }

    public int Kapasite => throw new NotImplementedException();

    public int Sayi => throw new NotImplementedException();

    public void Ekle(T oge)
    {
        throw new NotImplementedException();
    }

    public T[] ToArray()
    {
        throw new NotImplementedException();
    }
}
