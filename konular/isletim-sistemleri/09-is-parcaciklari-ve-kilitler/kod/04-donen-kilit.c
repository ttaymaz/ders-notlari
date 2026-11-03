/*
 * 04-donen-kilit.c — test-and-set ile kendi kilidimizi kurmak
 *
 * Gösterdiği: Bir kilit tek bir tamsayıdır: 0 boş, 1 dolu. Ama
 * "değere bak, 0 ise 1 yap" iki adım olursa iki iş parçacığı birden
 * 0 görüp birlikte girebilir. Donanımın atomik değiş-tokuş komutu
 * (test-and-set) eski değeri okuyup yerine 1 yazmayı BÖLÜNMEZ tek
 * adımda yapar. Eski değer 0 geldiyse kilit bizimdir; 1 geldiyse
 * başkası tutuyordur, tekrar deneriz (döneriz).
 *
 * __atomic_exchange_n, derleyicinin bu donanım komutuna (x86'da xchg)
 * verdiği addır.
 *
 * Derleme:    gcc 04-donen-kilit.c -o 04-donen-kilit.out -pthread
 * Çalıştırma: ./04-donen-kilit.out
 *             time ./04-donen-kilit.out
 */
#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

volatile int sayac = 0;
int dongu = 1000000;
int kilit = 0;                  /* 0 = boş, 1 = dolu */

void kilitle(int *k)
{
    /* eski değeri al, yerine 1 yaz — tek, bölünmez adım */
    while (__atomic_exchange_n(k, 1, __ATOMIC_ACQUIRE) == 1)
        ;                       /* doluymuş: dön ve tekrar dene */
}

void birak(int *k)
{
    __atomic_store_n(k, 0, __ATOMIC_RELEASE);
}

void *arttir(void *arg)
{
    (void) arg;
    for (int i = 0; i < dongu; i++) {
        kilitle(&kilit);
        sayac = sayac + 1;      /* kritik bölge */
        birak(&kilit);
    }
    return NULL;
}

int main(int argc, char *argv[])
{
    if (argc > 1)
        dongu = atoi(argv[1]);

    pthread_t i1, i2;
    pthread_create(&i1, NULL, arttir, NULL);
    pthread_create(&i2, NULL, arttir, NULL);
    pthread_join(i1, NULL);
    pthread_join(i2, NULL);

    printf("beklenen: %d\n", 2 * dongu);
    printf("sonuç   : %d\n", sayac);
    return 0;
}
