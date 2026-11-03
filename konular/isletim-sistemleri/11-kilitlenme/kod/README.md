# Kilitlenme — Örnek Kodlar

Programlar Linux'ta, macOS'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md). Hepsi `-pthread` ile derlenir:

```
gcc 01-filozoflar.c -o 01-filozoflar.out -pthread
./01-filozoflar.out
```

`01-filozoflar.out` **bilerek asılı kalır**; `Ctrl+C` ile durdurun.

| Dosya | Gösterdiği | Şema |
| ----- | ---------- | ---- |
| `01-filozoflar.c` | Beş filozof, herkes önce sol çatalı alır: kilitlenme | `01-filozoflar-grafik.svg` |
| `02-filozoflar-sirali.c` | Önce numarası küçük çatal: döngüsel bekleme kırılır | `02-sirali-grafik.svg` |
| `03-trylock.c` | İki kilit ters sırayla, `trylock` ile alamayan bırakır | — |

## Beklenen Çıktı

`01-filozoflar.out` — beş satır yazar ve durur (denediğimiz beş çalıştırmanın beşinde):

```
filozof 0: sol çatalı (0) aldı, sağı (1) bekliyor
filozof 1: sol çatalı (1) aldı, sağı (2) bekliyor
filozof 2: sol çatalı (2) aldı, sağı (3) bekliyor
filozof 3: sol çatalı (3) aldı, sağı (4) bekliyor
filozof 4: sol çatalı (4) aldı, sağı (0) bekliyor
```

`02-filozoflar-sirali.out` — filozofların yeme sırası değişebilir, son satır her zaman `hepsi yedi` (10/10):

```
filozof 3: 3 ve 4 numaralı çatallarla yiyor
filozof 2: 2 ve 3 numaralı çatallarla yiyor
filozof 1: 1 ve 2 numaralı çatallarla yiyor
filozof 0: 0 ve 1 numaralı çatallarla yiyor
filozof 4: 0 ve 4 numaralı çatallarla yiyor
hepsi yedi
```

`03-trylock.out` — geri çekilme sayıları her çalıştırmada değişir (ölçtüğümüz: 2–5):

```
iş parçacığı 2 (B sonra A): 5 turu bitirdi, 3 kez geri çekildi
iş parçacığı 1 (A sonra B): 5 turu bitirdi, 4 kez geri çekildi
kilitlenme olmadı
```

## Denemeniz İçin

- `01-filozoflar.c` içindeki `usleep(100000)` satırını silin ve programı defalarca çalıştırın. Biz 50 çalıştırmada bir kez bile kilitlenme görmedik. Program artık doğru mu? Dört koşuldan hangisi hâlâ duruyor, kilitlenme neden bu kadar seyrekleşti?
- `01-filozoflar.c`'de yalnızca 4 numaralı filozofun önce **sağ** çatalı almasını sağlayın. Bu, `02-filozoflar-sirali.c` ile aynı şey mi?
- `02-filozoflar-sirali.c`'de bir filozofu (örneğin 2'yi) sırayı bozacak biçimde, önce büyük numaralı çatalı alacak şekilde değiştirin. Kilitlenme geri geliyor mu?
- `03-trylock.c`'de rastgele beklemeyi sabit `usleep(10000)` yapın. Geri çekilme sayıları nasıl değişti?
- `03-trylock.c`'de `pthread_mutex_trylock` yerine `pthread_mutex_lock` yazın (ve `if`'i kaldırın). Ne oluyor?
- `01-filozoflar.c`'yi "aynı anda en fazla 4 filozof çatal alabilir" kuralıyla düzeltin: başlangıç değeri 4 olan bir semafor kullanın (10. hafta). Hangi koşulu kırdınız?
