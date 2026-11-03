/*
 * 02-olustur-bekle.c — pthread_create ve pthread_join
 *
 * Gösterdiği: İş parçacığı bir fonksiyonu çalıştırarak başlar.
 * pthread_create yeni iş parçacığını başlatır (fork'a benzer, ama
 * adres uzayı KOPYALANMAZ, paylaşılır); pthread_join onun bitmesini
 * bekler (wait'e benzer). Hangi iş parçacığının önce yazacağı
 * zamanlayıcının kararıdır. Programın sonunda ikisi de aynı genel
 * değişkeni görmüş olur.
 *
 * Şema: assets/01-adres-uzayi-is-parcacigi.svg
 *
 * Derleme:    gcc 02-olustur-bekle.c -o 02-olustur-bekle.out -pthread
 * Çalıştırma: ./02-olustur-bekle.out
 */
#include <stdio.h>
#include <pthread.h>

int ortak = 0;              /* veri bölgesi — iki iş parçacığı da görür */

void *calis(void *arg)
{
    char *ad = arg;         /* void * : türü söylenmemiş adres; burada bir metin */
    int yerel = 0;          /* her iş parçacığının KENDİ stack'inde */
    yerel++;
    ortak++;
    printf("iş parçacığı %s: yerel = %d, &yerel = %p\n",
           ad, yerel, (void *) &yerel);
    return NULL;
}

int main(void)
{
    pthread_t p1, p2;

    printf("main: başlıyor\n");
    pthread_create(&p1, NULL, calis, "A");
    pthread_create(&p2, NULL, calis, "B");

    pthread_join(p1, NULL);     /* A bitene kadar bekle */
    pthread_join(p2, NULL);     /* B bitene kadar bekle */
    printf("main: ikisi de bitti, ortak = %d\n", ortak);
    return 0;
}
