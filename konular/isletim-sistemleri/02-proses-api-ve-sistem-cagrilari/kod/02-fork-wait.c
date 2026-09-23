/*
 * 02-fork-wait.c — wait ile sırayı garanti etmek
 *
 * Gösterdiği: 01-fork.c'de hangi satırın önce yazılacağı belli değildi;
 * karar zamanlayıcınındı. Ebeveyn wait() çağırınca çocuk bitene kadar
 * bloke olur, böylece çocuğun satırı HER ZAMAN önce yazılır. wait()
 * biten çocuğun PID'ini döndürür; çocuğun exit() ile verdiği çıkış
 * kodu da okunabilir.
 *
 * Şema: assets/01-fork-akisi.svg (sağ dal)
 *
 * Derleme:   gcc 02-fork-wait.c -o 02-fork-wait.out
 * Çalıştırma: ./02-fork-wait.out
 */
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>

int main(void)
{
    printf("ebeveyn     : pid %d, başlıyor\n", (int) getpid());
    fflush(stdout);                     /* tamponu fork'tan önce boşalt */

    int rc = fork();

    if (rc < 0) {
        fprintf(stderr, "fork başarısız\n");
        exit(1);
    } else if (rc == 0) {
        printf("çocuk       : pid %d, işini yapıyor\n", (int) getpid());
        exit(7);                        /* çıkış kodu ebeveyne gider */
    } else {
        int durum;
        int biten = wait(&durum);       /* çocuk bitene kadar bloke */
        printf("ebeveyn     : pid %d bitti, çıkış kodu %d\n",
               biten, WEXITSTATUS(durum));
    }
    return 0;
}
