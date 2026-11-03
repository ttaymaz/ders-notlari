# İş Parçacıkları ve Kilitler — Örnek Kodlar

Programlar Linux'ta, macOS'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md). İş parçacığı kullanan programlar `-pthread` ile derlenir:

```
gcc 01-sayac-yarisi.c -o 01-sayac-yarisi.out -pthread
./01-sayac-yarisi.out
```

`-O2` gibi en iyileme seçenekleri kullanmayın: derleyici döngüyü tek bir toplamaya çevirebilir ve yarış görünmez olur.

| Dosya | Gösterdiği | Şema |
| ----- | ---------- | ---- |
| `01-sayac-yarisi.c` | İki iş parçacığı, korumasız sayaç: artışlar kaybolur, sonuç her çalıştırmada farklı | `02-yaris.svg` |
| `02-olustur-bekle.c` | `pthread_create` ve `pthread_join`; yerel değişkenler ayrı stack'te, genel değişken ortak | `01-adres-uzayi-is-parcacigi.svg` |
| `03-sayac-kilitli.c` | Aynı sayaç `pthread_mutex` ile: sonuç her zaman tam | — |
| `04-donen-kilit.c` | Test-and-set (atomik değiş-tokuş) ile kendi kurduğumuz dönen kilit | `04-test-and-set.svg` |

## Beklenen Çıktı

`01-sayac-yarisi.out` — son satır her çalıştırmada farklıdır (16 çekirdekli bir makinede ölçülen altı değer: 1 126 626, 1 101 208, 1 312 873, 1 215 187, 1 238 943, 1 131 014):

```
beklenen: 2000000
sonuç   : 1215187
```

`02-olustur-bekle.out` — adresler sizde farklı olacak, A ile B'nin sırası değişebilir:

```
main: başlıyor
iş parçacığı A: yerel = 1, &yerel = 0x74829e3feeac
iş parçacığı B: yerel = 1, &yerel = 0x74829dbfdeac
main: ikisi de bitti, ortak = 2
```

`03-sayac-kilitli.out` ve `04-donen-kilit.out` — her çalıştırmada:

```
beklenen: 2000000
sonuç   : 2000000
```

## Denemeniz İçin

- `01-sayac-yarisi.out` programını 1000, 10000 ve 100000 argümanlarıyla çalıştırın. Kayıp hangi büyüklükte başlıyor?
- `01`, `03` ve `04` programlarını `time` ile çalıştırıp sürelerini karşılaştırın. Kilit ne kadar yavaşlatıyor?
- `03-sayac-kilitli.c` içinde `pthread_mutex_unlock` satırını silin. Program ne yapıyor? (`Ctrl+C` ile durdurun.)
- `03-sayac-kilitli.c` içinde kilidi döngünün **dışına** alın: döngüden önce bir kez kilitleyin, sonra bırakın. Sonuç doğru mu? Süre ne oldu? Bu durumda iki iş parçacığından ne kazanıyoruz?
- `02-olustur-bekle.c` içindeki `ortak++` satırı da korumasız bir artıştır. Burada neden hep 2 çıkıyor? Hangi durumda bozulabilir?
- `04-donen-kilit.c` içinde `__atomic_exchange_n` satırını şöyle değiştirin: `while (*k == 1) ; *k = 1;` (bayrak kilidi). Sonuç hâlâ 2000000 mi?
- `pthread_join` satırlarını silin. `main` ne yazdırıyor? Neden?
