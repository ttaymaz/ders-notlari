/*
 * 01-hipervizor.c — bir sanal makinenin içinde miyiz?
 *
 * Gösterdiği: x86 işlemcilerde cpuid komutu işlemcinin özelliklerini
 * sorar. 1 numaralı sorunun cevabındaki bir bit "bir hipervizörün
 * üzerinde çalışıyorsun" demektir. Bit 1 ise 0x40000000 numaralı soru,
 * hipervizörün kendi adını döndürür. WSL'de çalıştırınca "Microsoft Hv"
 * (Hyper-V) görürsünüz: WSL bir sanal makinedir.
 *
 * Derleme:    gcc 01-hipervizor.c -o 01-hipervizor.out
 * Çalıştırma: ./01-hipervizor.out
 *
 * Yalnızca x86 (Intel, AMD) işlemcilerde derlenir.
 */
#include <stdio.h>
#include <string.h>
#include <cpuid.h>

int main(void)
{
    unsigned int a, b, c, d;

    __cpuid(1, a, b, c, d);
    unsigned int bit = (c >> 31) & 1;   /* ECX'in en üst biti */
    printf("hipervizör biti: %u → %s\n", bit,
           bit ? "bir sanal makinedeyiz" : "doğrudan donanımdayız");

    if (bit) {
        char ad[13];
        __cpuid(0x40000000, a, b, c, d);
        memcpy(ad, &b, 4);              /* ad 12 harf, üç yazmaçta */
        memcpy(ad + 4, &c, 4);
        memcpy(ad + 8, &d, 4);
        ad[12] = '\0';
        printf("hipervizör adı : %s\n", ad);
    }
    return 0;
}
