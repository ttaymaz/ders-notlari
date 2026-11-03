# Koşul Değişkenleri ve Semaforlar — Örnek Kodlar

Programlar Linux'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md). Hepsi `-pthread` ile derlenir:

```
gcc 02-kosul-degiskeni.c -o 02-kosul-degiskeni.out -pthread
time ./02-kosul-degiskeni.out
```

`04-semafor.c` macOS'ta çalışmaz: macOS `sem_init` ile oluşturulan semaforları desteklemez. Linux ya da WSL kullanın.

| Dosya | Gösterdiği | Şema |
| ----- | ---------- | ---- |
| `01-donerek-bekle.c` | Başka bir iş parçacığını bayrağa bakarak beklemek: doğru ama işlemciyi boşa harcar | — |
| `02-kosul-degiskeni.c` | Aynı bekleme koşul değişkeniyle: uyuyarak, kendi `join`'imiz | `01-kd-bekle.svg` |
| `03-uretici-tuketici.c` | Sınırlı tampon: bir üretici, iki tüketici, iki koşul değişkeni ve `while` | `02-sinirli-tampon.svg` |
| `04-semafor.c` | Aynı sınırlı tampon üç semaforla | — |

## Beklenen Çıktı

`time ./01-donerek-bekle.out` ve `time ./02-kosul-degiskeni.out` — iki program da bir saniye sürer; fark `user` satırındadır:

```
01:  real 0m1.003s   user 0m1.003s     ← main bir saniye boyunca döndü
02:  real 0m1.003s   user 0m0.002s     ← main uyudu
```

`03-uretici-tuketici.out` ve `04-semafor.out` — tüketicilerin paylaşımı her çalıştırmada değişir, toplam her zaman 210'dur:

```
tüketici B:  6 sayı aldı, toplamı  65
tüketici A: 14 sayı aldı, toplamı 145
beklenen toplam: 210
```

## Denemeniz İçin

- `02-kosul-degiskeni.c` içinde `sleep(1)` satırını çocuktan alıp `main`'e, `benim_join()` çağrısının hemen önüne taşıyın. Artık çocuk `main` beklemeye başlamadan biter. Program hâlâ doğru çalışıyor mu? Hangi değişken bunu sağlıyor?
- Aynı değişikliği yaptıktan sonra `bitti` değişkenini tamamen kaldırın: `main` yalnızca `pthread_cond_wait` çağırsın, çocuk yalnızca `pthread_cond_signal`. Program ne yapıyor? (`Ctrl+C`) Not, bölüm 3.1.
- `03-uretici-tuketici.c` içinde `MAKS`'ı 1 yapın. Toplam değişiyor mu? Tüketicilerin paylaşımı nasıl değişti?
- `03-uretici-tuketici.c` içinde iki koşul değişkeni yerine yalnızca birini kullanın (`&bos_var` yazan her yere `&dolu_var` yazın). Programı defalarca çalıştırın; sonra `MAKS`'ı 1 yapıp yine defalarca çalıştırın. Hangisinde asılı kalıyor (`Ctrl+C`), neden tampon küçülünce hata ortaya çıkıyor? Not, bölüm 4.2.
- `04-semafor.c` içinde tüketicideki iki `sem_wait` satırının yerini değiştirin (önce `kilit`, sonra `dolu`). Ne oluyor? Not, bölüm 6.
- `04-semafor.c` içinde `bos` semaforunun başlangıç değerini `MAKS` yerine 1 yapın. Program hâlâ doğru mu? Ne değişti?
