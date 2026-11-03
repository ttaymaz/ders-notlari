/*
 * 02-kosul-degiskeni.c — aynı bekleme, bu kez uyuyarak
 *
 * Gösterdiği: main, çocuğu bir koşul değişkeniyle bekler.
 * pthread_cond_wait kilidi bırakır ve main'i UYUTUR (bloke); çocuk
 * pthread_cond_signal ile uyandırınca main kilidi yeniden alarak
 * döner. Bu, kendi pthread_join'imizi yazmaktır. "time" çıktısında
 * user süresi neredeyse sıfırdır: main beklerken işlemci kullanmadı.
 *
 * Üç kural:
 *   1. Bir durum değişkeni (bitti) olsun — sinyal kaybolmasın
 *   2. Beklerken ve sinyal verirken kilit tutulsun
 *   3. Koşul while ile kontrol edilsin, if ile değil
 *
 * Derleme:    gcc 02-kosul-degiskeni.c -o 02-kosul-degiskeni.out -pthread
 * Çalıştırma: time ./02-kosul-degiskeni.out
 */
#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

int bitti = 0;                                  /* durum değişkeni */
pthread_mutex_t kilit = PTHREAD_MUTEX_INITIALIZER;
pthread_cond_t  kd    = PTHREAD_COND_INITIALIZER;

void *cocuk(void *arg)
{
    (void) arg;
    printf("çocuk: çalışıyor (1 saniye)\n");
    sleep(1);

    pthread_mutex_lock(&kilit);
    bitti = 1;
    pthread_cond_signal(&kd);       /* uyuyan varsa uyandır */
    pthread_mutex_unlock(&kilit);
    return NULL;
}

void benim_join(void)
{
    pthread_mutex_lock(&kilit);
    while (bitti == 0)
        pthread_cond_wait(&kd, &kilit); /* kilidi bırak + uyu; uyanınca kilidi geri al */
    pthread_mutex_unlock(&kilit);
}

int main(void)
{
    pthread_t c;
    pthread_create(&c, NULL, cocuk, NULL);

    benim_join();

    printf("main: çocuk bitti\n");
    return 0;
}
