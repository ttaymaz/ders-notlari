/*
 * 01-donerek-bekle.c — başka bir iş parçacığını dönerek beklemek
 *
 * Gösterdiği: main, çocuk iş parçacığının işini bitirmesini bir
 * bayrağa bakarak bekler. Çocuk 1 saniye uyur, sonra bayrağı 1 yapar.
 * Bekleme DOĞRU çalışır ama main o bir saniye boyunca işlemciyi boşa
 * kullanır. "time" çıktısında user satırına bakın: main'in döndüğü
 * süre orada görünür. 02-kosul-degiskeni.c ile karşılaştırın.
 *
 * Derleme:    gcc 01-donerek-bekle.c -o 01-donerek-bekle.out -pthread
 * Çalıştırma: time ./01-donerek-bekle.out
 */
#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

volatile int bitti = 0;         /* 0: çocuk çalışıyor, 1: bitti */

void *cocuk(void *arg)
{
    (void) arg;
    printf("çocuk: çalışıyor (1 saniye)\n");
    sleep(1);                   /* uzun bir iş yapıyormuş gibi */
    bitti = 1;
    return NULL;
}

int main(void)
{
    pthread_t c;
    pthread_create(&c, NULL, cocuk, NULL);

    while (bitti == 0)
        ;                       /* dönerek bekle — işlemci boşa çalışır */

    printf("main: çocuk bitti\n");
    return 0;
}
