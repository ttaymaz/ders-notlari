/*
 * 01-fork.c — fork bir kez çağrılır, iki kez döner
 *
 * Gösterdiği: fork() çağrısından sonra iki proses vardır ve ikisi de
 * fork'un döndüğü satırdan devam eder. Dönüş değeri çocukta 0, ebeveynde
 * çocuğun PID'idir. İki prosesin x değişkeni birbirinden bağımsızdır:
 * çocuk ebeveynin adres uzayının bir KOPYASIYLA başlar.
 *
 * Şema: assets/01-fork-akisi.svg
 *
 * Derleme:   gcc 01-fork.c -o 01-fork.out
 * Çalıştırma: ./01-fork.out
 *
 * Linux, WSL veya macOS gerekir; fork() Windows'ta yoktur.
 */
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

int main(void)
{
    int x = 100;
    printf("fork öncesi : pid %d, x = %d\n", (int) getpid(), x);
    fflush(stdout);                     /* tamponu fork'tan önce boşalt */

    int rc = fork();

    if (rc < 0) {
        fprintf(stderr, "fork başarısız\n");
        exit(1);
    } else if (rc == 0) {
        x = x + 1;
        printf("çocuk       : pid %d, fork dönüşü %d, x = %d\n",
               (int) getpid(), rc, x);
    } else {
        x = x - 1;
        printf("ebeveyn     : pid %d, fork dönüşü %d, x = %d\n",
               (int) getpid(), rc, x);
    }
    return 0;
}
