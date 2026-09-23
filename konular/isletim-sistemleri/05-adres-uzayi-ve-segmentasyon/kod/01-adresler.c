/*
 * 01-adresler.c — adres uzayının dört bölgesi
 *
 * Gösterdiği: Bir prosesin kodu, genel değişkenleri, heap'i ve stack'i
 * adres uzayında farklı yerlerde durur. Program her bölgeden bir
 * nesnenin adresini yazdırır. Yazdırılan adresler SANALDIR — fiziksel
 * bellekte nerede oldukları bu programdan görülemez.
 *
 * Şema: assets/01-adres-uzayi.svg
 *
 * Derleme:    gcc 01-adresler.c -o 01-adresler.out
 * Çalıştırma: ./01-adresler.out
 *
 * Programı iki kez çalıştırın: adresler her seferinde değişir (adres
 * uzayı rastgeleleştirmesi). Rastgeleleştirmeyi kapatmak için:
 *   setarch $(uname -m) -R ./01-adresler.out
 */
#include <stdio.h>
#include <stdlib.h>

int genel = 42;                         /* veri bölgesi */

int main(void)
{
    int yerel = 7;                      /* stack */
    int *dinamik = malloc(sizeof(int)); /* heap */

    printf("kod   (main)    : %p\n", (void *) main);
    printf("veri  (genel)   : %p\n", (void *) &genel);
    printf("heap  (malloc)  : %p\n", (void *) dinamik);
    printf("stack (yerel)   : %p\n", (void *) &yerel);

    free(dinamik);
    return 0;
}
