# Dosya Sistemleri — Örnek Kod

Program Linux'ta, macOS'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md).

```
gcc 01-dosya-api.c -o 01-dosya-api.out
./01-dosya-api.out
```

| Dosya | Gösterdiği |
| ----- | ---------- |
| `01-dosya-api.c` | `open`, `write`, `lseek`, `read`, `close`; dosya tanımlayıcının 3'ten başlaması; her açılışın kendi konumu olması |

Program çalıştığı klasörde geçici bir `deneme.txt` oluşturur ve sonunda siler.

## Beklenen Çıktı

```
open   → dosya tanımlayıcı 3
write  → 13 bayt yazıldı, konum artık 13
read   → 0 bayt okundu (konum dosyanın sonunda)
lseek 8, read 5 → "dunya"
ikinci open → 4, kendi konumu 0'dan: "merhaba"
```

Sistem çağrılarını görmek için:

```
strace -e trace=openat,write,lseek,read,close ./01-dosya-api.out
```

## Bağlar: Kabuk Komutları

```
echo merhaba > a
ln a b          # sert bağ
ln -s a c       # sembolik bağ
ls -li a b c    # inode numaraları ve bağ sayıları
rm a
cat b           # hâlâ okunur
cat c           # No such file or directory
```

## Denemeniz İçin

- `01-dosya-api.c`'nin sonundaki `unlink` satırını silin, programı çalıştırın ve `ls -li deneme.txt` ile inode numarasına ve bağ sayısına bakın.
- `close(fd2)` satırından önce `close(1)` ekleyip ardından bir `open` daha yapın. Yeni dosya tanımlayıcı kaç oldu? Neden? (İpucu: 2. haftanın yönlendirmesi.)
- `lseek(fd, 100, SEEK_SET)` ile dosyanın sonunun ötesine gidip bir bayt yazın. `ls -l` boyutu kaç gösteriyor? `du` diskte kaç blok gösteriyor?
- `ln a b` sonrası `echo ek >> b` ile `b`'ye bir satır ekleyin. `cat a` ne gösteriyor? Neden?
- Bir klasöre sert bağ vermeyi deneyin: `mkdir k; ln k k2`. Ne oluyor? Neden izin verilmiyor olabilir?
