/*
 * 03-sayac-kilitli.c — aynı sayaç, bu kez kilitle
 *
 * Gösterdiği: 01-sayac-yarisi.c'nin kritik bölgesi bir kilitle
 * korunur. Kilidi alan iş parçacığı bırakana kadar diğeri bekler;
 * üç adım (oku, ekle, yaz) artık bölünmez. Sonuç her çalıştırmada
 * tam iki milyondur. Bedeli hızdır: "time" ile iki programın
 * süresini karşılaştırın.
 *
 * Derleme:    gcc 03-sayac-kilitli.c -o 03-sayac-kilitli.out -pthread
 * Çalıştırma: ./03-sayac-kilitli.out
 *             time ./03-sayac-kilitli.out
 */
#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

volatile int sayac = 0;
int dongu = 1000000;
pthread_mutex_t kilit = PTHREAD_MUTEX_INITIALIZER;

void *arttir(void *arg)
{
    (void) arg;
    for (int i = 0; i < dongu; i++) {
        pthread_mutex_lock(&kilit);     /* kilidi al — gerekirse bekle */
        sayac = sayac + 1;              /* kritik bölge */
        pthread_mutex_unlock(&kilit);   /* kilidi bırak */
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
