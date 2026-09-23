/*
 * 05-fork-dongusu.c — döngü içinde fork: kaç satır yazılır?
 *
 * Gösterdiği: Döngünün her turunda o an var olan HER proses fork eder,
 * proses sayısı her turda ikiye katlanır. Önce ders notundaki tabloyu
 * kâğıtta doldurun, sonra çalıştırıp satırları sayın.
 *
 * Derleme:   gcc 05-fork-dongusu.c -o 05-fork-dongusu.out
 * Çalıştırma: ./05-fork-dongusu.out
 * Satırları saydırmak için: ./05-fork-dongusu.out | wc -l
 */
#include <stdio.h>
#include <unistd.h>
#include <sys/wait.h>

int main(void)
{
    for (int i = 0; i < 3; i++) {
        fork();
    }
    printf("merhaba, ben pid %d\n", (int) getpid());

    while (wait(NULL) > 0)              /* varsa bütün çocukları bekle */
        ;
    return 0;
}
