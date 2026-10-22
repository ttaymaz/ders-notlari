// KASITLI OLARAK DERLENMEZ.
//
// "event" kelimesi, olayı dışarıdan yalnızca += ve -= ile kullanmaya izin
// verir. Dışarıdan tetiklemek ve = ile bütün aboneleri silmek yasaktır.
// İki hatayı okuyun. Sonra Eklendi tanımından "event" kelimesini silip
// tekrar derleyin: ne değişti? Neden bu bir tehlike?
// Derlemeyi deneyin:  dotnet build 01-olayi-disaridan.cs

var raf = new Raf();
raf.Eklendi += b => Console.WriteLine($"{b} eklendi");

raf.Eklendi("Hiç eklenmemiş kitap");   // dışarıdan tetiklemek
raf.Eklendi = null;                    // bütün aboneleri silmek

class Raf
{
    public event Action<string>? Eklendi;

    public void Ekle(string baslik) => Eklendi?.Invoke(baslik);
}
