/*
 * 04-yonlendirme.c — kabuk "wc -l dosya > cikti.txt" komutunu nasıl çalıştırır?
 *
 * Gösterdiği: fork ile exec'in neden AYRI iki çağrı olduğu. Aradaki
 * boşlukta çocuk, henüz eski programken, kendi standart çıktısını
 * kapatıp bir dosya açar. Unix yeni açılan dosyaya en küçük boş
 * tanımlayıcıyı verdiği için dosya 1 numarayı, yani standart çıktının
 * yerini alır. Sonra exec edilen wc, ekrana yazdığını sanarak dosyaya
 * yazar — wc'nin kodunda hiçbir değişiklik yoktur.
 *
 * Şema: assets/01-fork-akisi.svg
 *
 * Derleme:   gcc 04-yonlendirme.c -o 04-yonlendirme.out
 * Çalıştırma: ./04-yonlendirme.out   sonra: cat cikti.txt
 */
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/wait.h>

int main(void)
{
    int rc = fork();

    if (rc < 0) {
        fprintf(stderr, "fork başarısız\n");
        exit(1);
    } else if (rc == 0) {
        close(STDOUT_FILENO);           /* 1 numaralı tanımlayıcı boşaldı */
        open("cikti.txt", O_CREAT | O_WRONLY | O_TRUNC, 0644);  /* 1'i alır */

        char *argumanlar[] = { "wc", "-l", "04-yonlendirme.c", NULL };
        execvp(argumanlar[0], argumanlar);
        exit(1);
    } else {
        wait(NULL);
        printf("wc bitti; çıktısı ekranda değil, cikti.txt dosyasında\n");
    }
    return 0;
}
