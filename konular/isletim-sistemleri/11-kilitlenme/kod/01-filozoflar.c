/*
 * 01-filozoflar.c — yemek yiyen filozoflar: kilitlenme
 *
 * Gösterdiği: Beş filozof yuvarlak bir masada oturur; aralarında beş
 * çatal vardır. Yemek için iki yandaki çatal da gerekir. Her filozof
 * önce SOL çatalını, sonra SAĞ çatalını alır. Çatallar arasında kısa
 * bir bekleme olduğu için herkes sol çatalını alır ve sağ çatalı
 * bekler: sağ çatal, sağdaki filozofun sol çatalıdır. Kimse
 * bırakmaz, kimse yiyemez. Program ASILI KALIR; Ctrl+C ile durdurun.
 *
 * Kilitlenmenin dört koşulu burada birlikte vardır: çatal tek kişilik
 * (karşılıklı dışlama), sol çatalı tutup sağı bekliyor (tut ve bekle),
 * kimse çatalı elinden alamaz (kesintisizlik), 0 → 1 → 2 → 3 → 4 → 0
 * diye kapanan bir bekleme zinciri var (döngüsel bekleme).
 *
 * Şema: assets/01-filozoflar-grafik.svg
 *
 * Derleme:    gcc 01-filozoflar.c -o 01-filozoflar.out -pthread
 * Çalıştırma: ./01-filozoflar.out        (asılı kalır — Ctrl+C)
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

    pthread_mutex_lock(&catal[sol]);
    printf("filozof %d: sol çatalı (%d) aldı, sağı (%d) bekliyor\n", i, sol, sag);
    usleep(100000);                     /* 0,1 sn: herkes solunu alsın */
    pthread_mutex_lock(&catal[sag]);    /* burada kalınır */

    printf("filozof %d: yiyor\n", i);
    pthread_mutex_unlock(&catal[sag]);
    pthread_mutex_unlock(&catal[sol]);
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

    printf("hepsi yedi\n");             /* bu satıra hiç gelinmez */
    return 0;
}
