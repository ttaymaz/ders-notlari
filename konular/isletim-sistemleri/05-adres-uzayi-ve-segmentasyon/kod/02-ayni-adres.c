/*
 * 02-ayni-adres.c — aynı sanal adres, iki farklı değer
 *
 * Gösterdiği: fork'tan sonra ebeveyn ve çocuk aynı değişkenin adresini
 * yazdırır — adres AYNIDIR, çünkü çocuğun adres uzayı ebeveyninkinin
 * kopyasıdır. Ama her biri değişkene farklı bir değer yazar ve kendi
 * değerini görür. Aynı sanal adres, iki prosese iki ayrı fiziksel yer
 * olarak çevrilmektedir.
 *
 * Şema: assets/02-taban-sinir.svg
 *
 * Derleme:    gcc 02-ayni-adres.c -o 02-ayni-adres.out
 * Çalıştırma: ./02-ayni-adres.out
 */
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>

int main(void)
{
    int sayi = 0;
    fflush(stdout);

    int rc = fork();
    if (rc < 0) {
        fprintf(stderr, "fork başarısız\n");
        exit(1);
    } else if (rc == 0) {
        sayi = 111;
        printf("çocuk   : adres %p, değer %d\n", (void *) &sayi, sayi);
    } else {
        sayi = 999;
        wait(NULL);                     /* çocuk yazıp bitsin, sıra sabit kalsın */
        printf("ebeveyn : adres %p, değer %d\n", (void *) &sayi, sayi);
    }
    return 0;
}
