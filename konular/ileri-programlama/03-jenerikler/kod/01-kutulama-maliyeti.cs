// Jenerikler neden var? Aynı işi iki koleksiyonla yapıp ayrılan belleği ölçüyoruz.
//
// ArrayList her elemanı object olarak saklar: her int kutulanır (heap'te
// ayrı bir nesne). List<int> elemanları kutulamadan, kendi dizisinde tutar.
// GC.GetAllocatedBytesForCurrentThread() bu iş parçacığının o ana kadar
// ayırdığı toplam baytı verir; farkı alınca iki satır arasındaki ayırma çıkar.
// Çalıştırmak için:  dotnet run 01-kutulama-maliyeti.cs

using System.Collections;

const int Adet = 1_000_000;

long once = GC.GetAllocatedBytesForCurrentThread();
var eski = new ArrayList();
for (int i = 0; i < Adet; i++)
    eski.Add(i);
long arrayListBayt = GC.GetAllocatedBytesForCurrentThread() - once;

once = GC.GetAllocatedBytesForCurrentThread();
var yeni = new List<int>();
for (int i = 0; i < Adet; i++)
    yeni.Add(i);
long listBayt = GC.GetAllocatedBytesForCurrentThread() - once;

Console.WriteLine($"ArrayList : {arrayListBayt / 1_000_000.0:F1} MB");
Console.WriteLine($"List<int> : {listBayt / 1_000_000.0:F1} MB");

// Tip güvenliği: ArrayList her şeyi kabul eder, hata çalışma zamanına kalır.
eski.Add("bir milyon bir");
// yeni.Add("bir milyon bir");   // List<int>: bu satır DERLENMEZ

int toplam = 0;
foreach (object o in eski)
    toplam += (int)o;            // son eleman string -> çalışırken patlar
Console.WriteLine(toplam);
