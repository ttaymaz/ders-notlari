/*
 * 03-uretici-tuketici.c — sınırlı tampon, iki koşul değişkeni
 *
 * Gösterdiği: Bir üretici 1'den 20'ye kadar sayıları 4 yerlik bir
 * tampona koyar; iki tüketici tampondan alıp toplar. Tampon doluysa
 * üretici, boşsa tüketici UYUR. Doğru çözümün iki parçası:
 *   - İki koşul değişkeni: üretici yalnızca tüketiciyi (dolu_var),
 *     tüketici yalnızca üreticiyi (bos_var) uyandırır
 *   - while: uyanan iş parçacığı koşulu yeniden kontrol eder
 * Toplam her çalıştırmada 210'dur (1 + 2 + ... + 20); tüketicilerin
 * paylaşımı her çalıştırmada değişebilir.
 *
 * Şema: assets/02-sinirli-tampon.svg
 *
 * Derleme:    gcc 03-uretici-tuketici.c -o 03-uretici-tuketici.out -pthread
 * Çalıştırma: ./03-uretici-tuketici.out
 */
#include <stdio.h>
#include <pthread.h>

#define MAKS 4              /* tampon kaç sayı tutabilir */
#define ADET 20             /* üretici kaç sayı üretir */
#define BITTI -1            /* "iş kalmadı" işareti */

int tampon[MAKS];
int koy_yeri = 0, al_yeri = 0, sayi = 0;   /* sayi: tamponda kaç eleman var */

pthread_mutex_t kilit    = PTHREAD_MUTEX_INITIALIZER;
pthread_cond_t  bos_var  = PTHREAD_COND_INITIALIZER;   /* üretici bekler */
pthread_cond_t  dolu_var = PTHREAD_COND_INITIALIZER;   /* tüketici bekler */

void koy(int deger)
{
    tampon[koy_yeri] = deger;
    koy_yeri = (koy_yeri + 1) % MAKS;
    sayi++;
}

int al(void)
{
    int deger = tampon[al_yeri];
    al_yeri = (al_yeri + 1) % MAKS;
    sayi--;
    return deger;
}

void *uretici(void *arg)
{
    int tuketici_sayisi = *(int *) arg;
    for (int i = 1; i <= ADET + tuketici_sayisi; i++) {
        pthread_mutex_lock(&kilit);
        while (sayi == MAKS)                    /* tampon dolu: uyu */
            pthread_cond_wait(&bos_var, &kilit);
        koy(i <= ADET ? i : BITTI);             /* sonda her tüketiciye bir BITTI */
        pthread_cond_signal(&dolu_var);         /* bir tüketiciyi uyandır */
        pthread_mutex_unlock(&kilit);
    }
    return NULL;
}

void *tuketici(void *arg)
{
    char *ad = arg;
    int toplam = 0, adet = 0;
    while (1) {
        pthread_mutex_lock(&kilit);
        while (sayi == 0)                       /* tampon boş: uyu */
            pthread_cond_wait(&dolu_var, &kilit);
        int deger = al();
        pthread_cond_signal(&bos_var);          /* üreticiyi uyandır */
        pthread_mutex_unlock(&kilit);

        if (deger == BITTI)
            break;
        toplam += deger;
        adet++;
    }
    printf("tüketici %s: %2d sayı aldı, toplamı %3d\n", ad, adet, toplam);
    return NULL;
}

int main(void)
{
    int tuketici_sayisi = 2;
    pthread_t u, t1, t2;
    pthread_create(&u,  NULL, uretici,  &tuketici_sayisi);
    pthread_create(&t1, NULL, tuketici, "A");
    pthread_create(&t2, NULL, tuketici, "B");
    pthread_join(u,  NULL);
    pthread_join(t1, NULL);
    pthread_join(t2, NULL);
    printf("beklenen toplam: %d\n", ADET * (ADET + 1) / 2);
    return 0;
}
