/*
 * 01-dosya-api.c — open, write, lseek, read, close
 *
 * Gösterdiği: Bir dosya açılınca çekirdek küçük bir tamsayı döndürür:
 * dosya tanımlayıcı. 0, 1 ve 2 her prosese baştan açık gelir (standart
 * girdi, çıktı, hata); bu yüzden ilk açılan dosya 3 olur. Her açık
 * dosyanın bir KONUMU vardır: write ve read kaldığı yerden devam eder.
 * lseek konumu değiştirir. Aynı dosyayı iki kez açmak iki ayrı konum
 * verir.
 *
 * Derleme:    gcc 01-dosya-api.c -o 01-dosya-api.out
 * Çalıştırma: ./01-dosya-api.out
 *             strace -e trace=openat,write,lseek,read,close ./01-dosya-api.out
 */
#include <stdio.h>
#include <fcntl.h>
#include <unistd.h>

int main(void)
{
    char tampon[16];

    int fd = open("deneme.txt", O_CREAT | O_TRUNC | O_RDWR, 0644);
    printf("open   → dosya tanımlayıcı %d\n", fd);

    ssize_t n = write(fd, "merhaba dunya", 13);
    printf("write  → %zd bayt yazıldı, konum artık %ld\n", n, (long) lseek(fd, 0, SEEK_CUR));

    n = read(fd, tampon, sizeof tampon);
    printf("read   → %zd bayt okundu (konum dosyanın sonunda)\n", n);

    lseek(fd, 8, SEEK_SET);                     /* konumu 8. bayta taşı */
    n = read(fd, tampon, 5);
    tampon[n] = '\0';
    printf("lseek 8, read 5 → \"%s\"\n", tampon);

    int fd2 = open("deneme.txt", O_RDONLY);     /* aynı dosya, ikinci kez */
    n = read(fd2, tampon, 7);
    tampon[n] = '\0';
    printf("ikinci open → %d, kendi konumu 0'dan: \"%s\"\n", fd2, tampon);

    close(fd2);
    close(fd);
    unlink("deneme.txt");                       /* adı sil: dosya da gider */
    return 0;
}
