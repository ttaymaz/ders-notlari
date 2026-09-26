/*
 * 02-struct-ve-liste.c — struct, -> ve metin listesi
 *
 * Gösterdiği: struct birkaç alanı tek kayıtta toplar. Kaydın kendisine
 * nokta (.) ile, kaydın adresine ok (->) ile erişilir. Kayıtlar
 * birbirinin adresini tutarak bağlı liste oluşturur — işletim sistemi
 * proses listesini böyle tutar. Son bölüm exec'in istediği biçimdeki
 * metin listesini (char *argv[]) gösterir.
 *
 * Derleme:    gcc 02-struct-ve-liste.c -o 02-struct-ve-liste.out
 * Çalıştırma: ./02-struct-ve-liste.out
 */
#include <stdio.h>
#include <stdlib.h>

struct proses {
    int pid;
    char durum;                         /* 'H' hazır, 'C' çalışıyor, 'B' bloke */
    struct proses *sonraki;             /* listedeki bir sonraki kaydın adresi */
};

struct proses *yeni_proses(int pid, char durum)
{
    struct proses *p = malloc(sizeof(struct proses));
    if (p == NULL) {
        fprintf(stderr, "malloc başarısız\n");
        exit(1);
    }
    p->pid = pid;                       /* p bir adres: alanlara -> ile */
    p->durum = durum;
    p->sonraki = NULL;                  /* henüz arkasında kimse yok */
    return p;
}

int main(void)
{
    /* 1) Kaydın kendisi: nokta */
    struct proses ilk;
    ilk.pid = 1;
    ilk.durum = 'C';
    ilk.sonraki = NULL;
    printf("ilk kayıt      : pid %d, durum %c\n", ilk.pid, ilk.durum);

    /* 2) Kaydın adresi: ok */
    struct proses *bas = yeni_proses(10, 'C');
    bas->sonraki = yeni_proses(11, 'H');
    bas->sonraki->sonraki = yeni_proses(12, 'B');

    printf("proses listesi :");
    for (struct proses *p = bas; p != NULL; p = p->sonraki)
        printf(" [%d %c]", p->pid, p->durum);
    printf("\n");

    int hazir = 0;
    for (struct proses *p = bas; p != NULL; p = p->sonraki)
        if (p->durum == 'H')
            hazir++;
    printf("hazır proses   : %d\n", hazir);

    while (bas != NULL) {               /* listeyi geri ver */
        struct proses *sonra = bas->sonraki;
        free(bas);
        bas = sonra;
    }

    /* 3) Metin listesi: exec bu biçimi ister, sonu NULL */
    char *argumanlar[] = { "ls", "-l", "/tmp", NULL };
    printf("argüman listesi:");
    for (int i = 0; argumanlar[i] != NULL; i++)
        printf(" \"%s\"", argumanlar[i]);
    printf("\n");

    return 0;
}
