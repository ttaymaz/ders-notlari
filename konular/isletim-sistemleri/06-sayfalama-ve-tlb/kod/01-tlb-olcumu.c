/*
 * 01-tlb-olcumu.c — TLB'nin etkisini ölçmek
 *
 * Gösterdiği: Program, her sayfadan yalnızca BİR tamsayıya dokunarak
 * giderek artan sayıda sayfayı tekrar tekrar dolaşır ve erişim başına
 * ortalama süreyi yazdırır. Dolaşılan sayfa sayısı TLB'nin
 * tutabileceği giriş sayısını aşınca her erişim bir TLB ıskasına döner
 * ve süre belirgin biçimde artar.
 *
 * Şema: assets/03-tlb-akisi.svg
 *
 * Derleme:    gcc -O1 01-tlb-olcumu.c -o 01-tlb-olcumu.out
 * Çalıştırma: ./01-tlb-olcumu.out
 *
 * Ölçüm gürültülüdür: aynı makinede bile çalıştırmadan çalıştırmaya
 * değişir. Sayıların kendisine değil, sayfa sayısı arttıkça sürenin
 * nerede sıçradığına bakın. Sıçrama noktaları makineden makineye farklıdır.
 */
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h>

#define EN_COK_SAYFA 16384
#define TOPLAM_ERISIM 20000000L

static double simdi_ns(void)
{
    struct timespec t;
    clock_gettime(CLOCK_MONOTONIC, &t);
    return t.tv_sec * 1e9 + t.tv_nsec;
}

int main(void)
{
    long sayfa_boyu = sysconf(_SC_PAGESIZE);
    long adim = sayfa_boyu / sizeof(int);   /* bir sayfadaki int sayısı */
    int *dizi = calloc((size_t) EN_COK_SAYFA * adim, sizeof(int));
    if (dizi == NULL) {
        fprintf(stderr, "bellek ayrılamadı\n");
        return 1;
    }

    printf("sayfa boyu: %ld bayt\n", sayfa_boyu);
    printf("%8s  %s\n", "sayfa", "ns/erisim");

    for (long sayfa = 1; sayfa <= EN_COK_SAYFA; sayfa *= 2) {
        long tur = TOPLAM_ERISIM / sayfa;

        for (long p = 0; p < sayfa; p++)                /* ısınma */
            dizi[p * adim + (p * 16) % adim] += 1;

        double bas = simdi_ns();
        for (long t = 0; t < tur; t++)
            for (long p = 0; p < sayfa; p++)
                dizi[p * adim + (p * 16) % adim] += 1;
        double son = simdi_ns();

        printf("%8ld  %.2f\n", sayfa, (son - bas) / (double) (tur * sayfa));
    }

    printf("(kontrol: %d)\n", dizi[0]);     /* derleyici döngüyü silmesin */
    free(dizi);
    return 0;
}
