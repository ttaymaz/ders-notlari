# Sanal Makineler ve Konteynerler — Örnek Kod

Program x86 (Intel, AMD) işlemcili bir Linux'ta ya da Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md).

```
gcc 01-hipervizor.c -o 01-hipervizor.out
./01-hipervizor.out
```

| Dosya | Gösterdiği |
| ----- | ---------- |
| `01-hipervizor.c` | `cpuid` komutuyla işlemciye "bir hipervizörün üzerinde miyim?" diye sormak ve hipervizörün adını okumak |

## Beklenen Çıktı

WSL'de:

```
hipervizör biti: 1 → bir sanal makinedeyiz
hipervizör adı : Microsoft Hv
```

Doğrudan donanıma kurulu bir Linux'ta bit 0 çıkar ve ad satırı yazılmaz.

## WSL Gösterimleri

```
systemd-detect-virt
uname -r
grep -c hypervisor /proc/cpuinfo
ls -l /proc/self/ns
```

Docker olmadan bir konteyner (`sudo` gerekmez; `exit` ile çıkın):

```
unshare --user --map-root-user --pid --fork --mount-proc --uts bash
hostname konteyner
echo "$(hostname), PID $$, $(id -un)"
ps -e
cat /etc/shadow
exit
```

## Denemeniz İçin

- `unshare` komutunu `--pid` olmadan çalıştırıp `ps -e` deneyin. Kaç proses görüyorsunuz? Neden?
- `--uts` olmadan çalıştırıp içeride `hostname konteyner` deyin. Ne oluyor?
- İçeride `ls -l /proc/self/ns` ile dışarıdaki çıktıyı karşılaştırın. Hangi ad alanlarının numarası değişti?
- İçeride `sleep 300 &` başlatın; başka bir terminalde `ps -e | grep sleep` ile dışarıdan bakın. sleep'in dışarıdaki PID'i ile içerideki PID'i aynı mı?
- `01-hipervizor.c`'yi ARM işlemcili bir bilgisayarda derlemeyi deneyin (ör. Apple Silicon bir Mac'te). Ne oluyor? Neden?
