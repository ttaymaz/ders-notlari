# Proses API'si — Örnek Kodlar

Bu klasördeki programlar Linux sistem çağrılarını kullanır. `fork` Windows'ta yoktur; Windows'ta WSL içinde çalıştırın. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md).

Her dosya tek başına derlenir:

```
gcc 01-fork.c -o 01-fork.out
./01-fork.out
```

Derlenmiş `.out` dosyaları ve örneklerin ürettiği `.txt` dosyaları depoya eklenmez.

| Dosya | Gösterdiği | Şema |
| ----- | ---------- | ---- |
| `01-fork.c` | `fork` bir kez çağrılır, iki kez döner; çocuk adres uzayının kopyasını alır | `01-fork-akisi.svg` |
| `02-fork-wait.c` | `wait` ile sıranın garanti edilmesi ve çıkış kodunun ebeveyne ulaşması | `01-fork-akisi.svg` |
| `03-exec.c` | `exec` ile başka programa dönüşmek; başarılı `exec` geri dönmez | `01-fork-akisi.svg` |
| `04-yonlendirme.c` | Kabuğun `>` yönlendirmesini `fork` ile `exec` arasında nasıl yaptığı | `01-fork-akisi.svg` |
| `05-fork-dongusu.c` | Döngü içinde `fork`: proses sayısı her turda ikiye katlanır | `04-fork-agaci.svg` |

## Denemeniz İçin

- `01-fork.c` programını art arda beş kez çalıştırın. Ebeveyn ile çocuğun satırları hep aynı sırada mı çıkıyor? Sırayı kim belirliyor?
- `01-fork.c` içindeki `fflush` satırını silin. Programı bir kez terminalde, bir kez de `./01-fork.out > sonuc.txt` ile çalıştırın ve `sonuc.txt` dosyasına bakın. Ne değişti?
- `02-fork-wait.c` içinde `exit(7)` yerine `exit(300)` yazın. Ebeveyn hangi çıkış kodunu görüyor? Neden?
- `03-exec.c` içinde `"ls"` yerine var olmayan bir program adı yazın. Hangi satır çalıştı?
- `04-yonlendirme.c` içindeki `close` satırını silin. `wc`'nin çıktısı nereye gitti?
- `05-fork-dongusu.c` programında `printf` satırını döngünün içine taşımadan önce kaç satır çıkacağını kâğıtta hesaplayın, sonra çalıştırıp sayın: `./05-fork-dongusu.out | wc -l`
- İleri: `strace -f ./02-fork-wait.out` ile programın yaptığı sistem çağrılarına bakın. `printf` hangi çağrıya dönüşüyor?
