# Adres, İşaretçi ve struct — Örnek Kodlar

Programlar Linux'ta, macOS'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md).

```
gcc 01-adres-ve-isaretci.c -o 01-adres-ve-isaretci.out
./01-adres-ve-isaretci.out
```

| Dosya | Gösterdiği |
| ----- | ---------- |
| `01-adres-ve-isaretci.c` | `&` ve `*`, bir fonksiyona adres vererek değişkeni değiştirmek (`wait(&durum)` kalıbı), `malloc` ve `free` |
| `02-struct-ve-liste.c` | `struct` alanlarına `.` ve `->` ile erişim, bağlı liste, `exec`'in istediği metin listesi |

## Beklenen Çıktı

`01-adres-ve-isaretci.out` — adresler sizde farklı olacak:

```
x'in değeri    : 42
x'in adresi    : 0x7ffd6b693d90
p'nin değeri   : 0x7ffd6b693d90
*p (adresteki) : 42
*p = 100 sonra : x = 100
fonksiyondan   : durum = 7
heap'teki dizi : 0x5c01cd79c2b0, ikinci eleman 20
```

`02-struct-ve-liste.out`:

```
ilk kayıt      : pid 1, durum C
proses listesi : [10 C] [11 H] [12 B]
hazır proses   : 1
argüman listesi: "ls" "-l" "/tmp"
```

## Denemeniz İçin

- `01-adres-ve-isaretci.c` içinde `yediyi_yaz(&durum)` satırındaki `&` işaretini silin. Derleyici ne diyor? (Sürüme göre uyarı ya da hata olabilir.) Derlenirse çalıştırınca ne oluyor?
- `int *p = &x;` satırını `int *p = NULL;` yapın. Program hangi satırda, hangi mesajla çöküyor?
- `dizi[1]` yerine `*(dizi + 1)` yazın. Çıktı değişiyor mu?
- `02-struct-ve-liste.c` içinde dördüncü bir proses ekleyin (`pid 13`, durum `'H'`). Hazır proses sayısı kaç olmalı?
- Listeyi dolaşan döngüde `p = p->sonraki` ifadesini silin. Ne olur? (Programı `Ctrl+C` ile durdurun.)
- `argumanlar` listesindeki `NULL`'u silip programı çalıştırın. Ne oluyor, neden?
