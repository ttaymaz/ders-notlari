/*
 * 04-semafor.c — aynı sınırlı tampon, semaforlarla
 *
 * Gösterdiği: Semafor bir tamsayıdır. sem_wait değeri bir azaltır;
 * değer sıfırın altına inecekse iş parçacığı UYUR. sem_post değeri
 * bir artırır ve uyuyan varsa birini uyandırır. Tampon için üç tane:
 *   bos   = MAKS  — tamponda kaç boş yer var (üretici bunu azaltır)
 *   dolu  = 0     — tamponda kaç dolu yer var (tüketici bunu azaltır)
 *   kilit = 1     — koy/al sırasında karşılıklı dışlama (ikili semafor)
 * Sonuç 03-uretici-tuketici.c ile aynıdır: toplam 210.
 *
 * DİKKAT — sıra önemlidir: önce bos/dolu, SONRA kilit. Tersi
 * kilitlenmeye yol açar (bkz. not, bölüm 6).
 *
 * Derleme:    gcc 04-semafor.c -o 04-semafor.out -pthread
 * Çalıştırma: ./04-semafor.out
 *
 * macOS'ta sem_init desteklenmez; programı Linux ya da WSL'de çalıştırın.
 */
#include <stdio.h>
#include <pthread.h>
#include <semaphore.h>

#define MAKS 4
#define ADET 20
#define BITTI -1

int tampon[MAKS];
int koy_yeri = 0, al_yeri = 0;

sem_t bos, dolu, kilit;

void *uretici(void *arg)
{
    int tuketici_sayisi = *(int *) arg;
    for (int i = 1; i <= ADET + tuketici_sayisi; i++) {
        sem_wait(&bos);                 /* boş yer yoksa uyu */
        sem_wait(&kilit);
        tampon[koy_yeri] = (i <= ADET ? i : BITTI);
        koy_yeri = (koy_yeri + 1) % MAKS;
        sem_post(&kilit);
        sem_post(&dolu);                /* bir dolu yer daha var */
    }
    return NULL;
}

void *tuketici(void *arg)
{
    char *ad = arg;
    int toplam = 0, adet = 0;
    while (1) {
        sem_wait(&dolu);                /* dolu yer yoksa uyu */
        sem_wait(&kilit);
        int deger = tampon[al_yeri];
        al_yeri = (al_yeri + 1) % MAKS;
        sem_post(&kilit);
        sem_post(&bos);                 /* bir boş yer daha var */

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
    sem_init(&bos,   0, MAKS);  /* ortadaki 0: semafor bu prosesin iş parçacıkları arasında */
    sem_init(&dolu,  0, 0);
    sem_init(&kilit, 0, 1);

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
