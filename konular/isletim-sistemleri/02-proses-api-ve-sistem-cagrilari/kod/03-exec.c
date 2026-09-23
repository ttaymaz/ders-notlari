/*
 * 03-exec.c — exec ile başka bir programa dönüşmek
 *
 * Gösterdiği: Çocuk proses execvp() ile kendini "ls -l" programına
 * dönüştürür. exec yeni bir proses OLUŞTURMAZ: PID aynı kalır, ama kod,
 * veri, heap ve stack tamamen yeni programınkiyle değiştirilir. Bu
 * yüzden exec başarılı olursa ASLA geri dönmez — ondan sonraki satır
 * yalnızca exec başarısız olursa çalışır.
 *
 * Şema: assets/01-fork-akisi.svg
 *
 * Derleme:   gcc 03-exec.c -o 03-exec.out
 * Çalıştırma: ./03-exec.out
 */
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>

int main(void)
{
    int rc = fork();

    if (rc < 0) {
        fprintf(stderr, "fork başarısız\n");
        exit(1);
    } else if (rc == 0) {
        printf("çocuk       : pid %d, ls programına dönüşüyor\n",
               (int) getpid());
        fflush(stdout);                 /* exec öncesi tamponu boşalt */

        char *argumanlar[] = { "ls", "-l", NULL };
        execvp(argumanlar[0], argumanlar);

        printf("bu satır yalnızca exec başarısız olursa yazılır\n");
        exit(1);
    } else {
        wait(NULL);
        printf("ebeveyn     : çocuk bitti, kabuk yeni komut bekleyebilir\n");
    }
    return 0;
}
