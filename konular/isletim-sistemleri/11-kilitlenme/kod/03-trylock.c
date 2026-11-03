/*
 * 03-trylock.c — kesintisizliği kırmak: alamıyorsan bırak
 *
 * Gösterdiği: İki iş parçacığı iki kilide TERS sırayla ihtiyaç duyar:
 * biri önce A sonra B, diğeri önce B sonra A — 01-filozoflar.c'deki
 * döngünün en küçük hali. Kilitlenmemek için ikinci kilit
 * pthread_mutex_trylock ile denenir: kilit doluysa beklemek yerine
 * hemen hata döner; iş parçacığı elindeki kilidi BIRAKIR ve baştan
 * dener. Program kilitlenmeden biter, ama "geri çekilme" sayısına
 * bakın: iki iş parçacığı birbirine yol vererek boşuna dönebilir.
 * Buna canlı kilitlenme (livelock) denir. Her geri çekilmeden sonra
 * rastgele kısa bir süre beklemek bu riski azaltır.
 *
 * Derleme:    gcc 03-trylock.c -o 03-trylock.out -pthread
 * Çalıştırma: ./03-trylock.out
 */
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <pthread.h>

pthread_mutex_t A = PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t B = PTHREAD_MUTEX_INITIALIZER;

struct is {
    char *ad;
    pthread_mutex_t *ilk, *ikinci;
};

void *calis(void *arg)
{
    struct is *w = arg;
    int geri = 0;
    unsigned int tohum = (unsigned int) (long) w;

    for (int tur = 0; tur < 5; tur++) {
        while (1) {
            pthread_mutex_lock(w->ilk);
            usleep(10000);                          /* 0,01 sn: çakışmayı kolaylaştır */
            if (pthread_mutex_trylock(w->ikinci) == 0)
                break;                              /* ikisi de bizde */
            pthread_mutex_unlock(w->ilk);           /* alamadık: BIRAK */
            geri++;
            usleep(rand_r(&tohum) % 20000);         /* rastgele bekle, sonra tekrar */
        }
        /* kritik bölge: iki kilit de tutuluyor */
        pthread_mutex_unlock(w->ikinci);
        pthread_mutex_unlock(w->ilk);
    }
    printf("%s: 5 turu bitirdi, %d kez geri çekildi\n", w->ad, geri);
    return NULL;
}

int main(void)
{
    struct is w1 = { "iş parçacığı 1 (A sonra B)", &A, &B };
    struct is w2 = { "iş parçacığı 2 (B sonra A)", &B, &A };
    pthread_t t1, t2;

    pthread_create(&t1, NULL, calis, &w1);
    pthread_create(&t2, NULL, calis, &w2);
    pthread_join(t1, NULL);
    pthread_join(t2, NULL);
    printf("kilitlenme olmadı\n");
    return 0;
}
