/*
 * 01-adres-ve-isaretci.c — adres, işaretçi ve malloc
 *
 * Gösterdiği: Her değişken bellekte bir adreste durur; & o adresi verir.
 * Adresi tutan değişkene işaretçi denir; * ile adresteki değere ulaşılır.
 * Bir fonksiyona değişkenin ADRESİ verilirse fonksiyon o değişkeni
 * değiştirebilir — wait(&durum) çağrısı tam olarak bunu yapar.
 * malloc heap'ten yer ayırır ve o yerin adresini döndürür.
 *
 * Derleme:    gcc 01-adres-ve-isaretci.c -o 01-adres-ve-isaretci.out
 * Çalıştırma: ./01-adres-ve-isaretci.out
 *
 * Adresler her çalıştırmada değişir; değerler aynı kalır.
 */
#include <stdio.h>
#include <stdlib.h>

void yediyi_yaz(int *hedef)             /* değişkenin kendisi değil, adresi gelir */
{
    *hedef = 7;                         /* o adresteki değeri değiştir */
}

int main(void)
{
    int x = 42;
    int *p = &x;                        /* p, x'in adresini tutar */

    printf("x'in değeri    : %d\n", x);
    printf("x'in adresi    : %p\n", (void *) &x);
    printf("p'nin değeri   : %p\n", (void *) p);
    printf("*p (adresteki) : %d\n", *p);

    *p = 100;                           /* x'i p üzerinden değiştir */
    printf("*p = 100 sonra : x = %d\n", x);

    int durum = 0;
    yediyi_yaz(&durum);                 /* wait(&durum) ile aynı kalıp */
    printf("fonksiyondan   : durum = %d\n", durum);

    int *dizi = malloc(3 * sizeof(int));  /* heap'te üç int'lik yer */
    if (dizi == NULL) {
        fprintf(stderr, "malloc başarısız\n");
        return 1;
    }
    dizi[0] = 10;
    dizi[1] = 20;
    dizi[2] = 30;
    printf("heap'teki dizi : %p, ikinci eleman %d\n", (void *) dizi, dizi[1]);
    free(dizi);                         /* yeri geri ver */

    return 0;
}
