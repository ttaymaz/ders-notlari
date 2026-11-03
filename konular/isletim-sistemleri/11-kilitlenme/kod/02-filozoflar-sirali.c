/*
 * 02-filozoflar-sirali.c — döngüyü kırmak: kilitleri hep aynı sırayla al
 *
 * Gösterdiği: 01-filozoflar.c ile tek fark, her filozofun önce NUMARASI
 * KÜÇÜK olan çatalı almasıdır. 0–3 numaralı filozoflar için bu yine
 * sol çataldır; 4 numaralı filozofun çatalları 4 ve 0'dır, o önce
 * 0'ı (sağını) alır. Bekleme zinciri artık kapanamaz: döngüsel bekleme
 * koşulu kırıldı. Aynı bekleme süreleriyle program her seferinde biter.
 *
 * Derleme:    gcc 02-filozoflar-sirali.c -o 02-filozoflar-sirali.out -pthread
 * Çalıştırma: ./02-filozoflar-sirali.out
 */
#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

#define N 5

pthread_mutex_t catal[N];

void *filozof(void *arg)
{
    int i = *(int *) arg;
    int sol = i;
    int sag = (i + 1) % N;
    int ilk  = sol < sag ? sol : sag;   /* numarası küçük olan önce */
    int ikinci = sol < sag ? sag : sol;

    pthread_mutex_lock(&catal[ilk]);
    usleep(100000);
    pthread_mutex_lock(&catal[ikinci]);

    printf("filozof %d: %d ve %d numaralı çatallarla yiyor\n", i, ilk, ikinci);
    usleep(100000);                     /* yemek */

    pthread_mutex_unlock(&catal[ikinci]);
    pthread_mutex_unlock(&catal[ilk]);
    return NULL;
}

int main(void)
{
    pthread_t f[N];
    int no[N];

    for (int i = 0; i < N; i++)
        pthread_mutex_init(&catal[i], NULL);
    for (int i = 0; i < N; i++) {
        no[i] = i;
        pthread_create(&f[i], NULL, filozof, &no[i]);
    }
    for (int i = 0; i < N; i++)
        pthread_join(f[i], NULL);

    printf("hepsi yedi\n");
    return 0;
}
