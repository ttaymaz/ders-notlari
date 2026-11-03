/*
 * 01-sayac-yarisi.c — iki iş parçacığı, bir sayaç, kaybolan artışlar
 *
 * Gösterdiği: İki iş parçacığı aynı genel değişkeni birer milyon kez
 * artırır. Beklenen sonuç iki milyondur; çok çekirdekli bir makinede
 * sonuç çoğu zaman daha küçük çıkar ve her çalıştırmada değişir.
 * "sayac = sayac + 1" tek satırdır ama işlemci için üç adımdır:
 * oku, bir ekle, geri yaz. İki iş parçacığı bu adımları iç içe
 * geçirirse bir artış kaybolur.
 *
 * Şema: assets/02-yaris.svg
 *
 * Derleme:    gcc 01-sayac-yarisi.c -o 01-sayac-yarisi.out -pthread
 * Çalıştırma: ./01-sayac-yarisi.out
 *             ./01-sayac-yarisi.out 1000     (döngü sayısını değiştirir)
 *
 * -O2 gibi en iyileme seçenekleriyle derlemeyin: derleyici döngüyü
 * tek bir toplamaya çevirebilir ve yarış görünmez olur.
 */
#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

volatile int sayac = 0;     /* iki iş parçacığının ORTAK değişkeni */
int dongu = 1000000;

void *arttir(void *arg)
{
    (void) arg;                 /* bu örnekte argüman kullanılmıyor */
    for (int i = 0; i < dongu; i++)
        sayac = sayac + 1;      /* kritik bölge — korumasız */
    return NULL;
}

int main(int argc, char *argv[])
{
    if (argc > 1)
        dongu = atoi(argv[1]);

    pthread_t i1, i2;
    pthread_create(&i1, NULL, arttir, NULL);
    pthread_create(&i2, NULL, arttir, NULL);
    pthread_join(i1, NULL);     /* ikisinin de bitmesini bekle */
    pthread_join(i2, NULL);

    printf("beklenen: %d\n", 2 * dongu);
    printf("sonuç   : %d\n", sayac);
    return 0;
}
