/*
 * 01-tampon-tasmasi.c — kısa bir dizi, hemen ardındaki alanı ezer
 *
 * Gösterdiği: Bir kullanıcı kaydında 8 baytlık bir ad dizisi ve hemen
 * ardından bir yetki alanı var. strcpy hedefin boyutunu bilmez: ad 8
 * bayttan uzunsa fazlası bellekte bir sonraki alana, YETKİYE yazılır.
 * En fazla 11 harf verin: o zaman taşma kaydın içinde kalır. Daha uzun
 * bir ad kaydın dışına, stack'teki başka verilere taşar — bunun neden
 * tehlikeli olduğu 02-yigin-korumasi.c'nin konusu.
 * İkinci yarıda aynı kopya snprintf ile, hedefin boyutu verilerek
 * yapılır ve taşma olmaz.
 *
 * Derleme:    gcc 01-tampon-tasmasi.c -o 01-tampon-tasmasi.out
 * Çalıştırma: ./01-tampon-tasmasi.out ali
 *             ./01-tampon-tasmasi.out ABCDEFGHIJK     (11 harf: 8 ad + 3 yetkiye + '\0')
 */
#include <stdio.h>
#include <string.h>

struct kullanici {
    char ad[8];         /* 7 harf + '\0' sığar */
    int  yetki;         /* 0: sıradan kullanıcı, başka her değer: yönetici */
};

int main(int argc, char *argv[])
{
    if (argc < 2) {
        printf("kullanım: %s AD\n", argv[0]);
        return 1;
    }

    struct kullanici k = { "", 0 };
    printf("önce   : yetki = %d\n", k.yetki);

    strcpy(k.ad, argv[1]);              /* boyut denetimi YOK */
    printf("strcpy : ad = \"%.8s...\", yetki = %d (0x%08x) → %s\n",
           k.ad, k.yetki, (unsigned) k.yetki,
           k.yetki == 0 ? "sıradan kullanıcı" : "YÖNETİCİ");

    struct kullanici g = { "", 0 };
    snprintf(g.ad, sizeof g.ad, "%s", argv[1]);   /* en fazla 7 harf + '\0' */
    printf("snprintf: ad = \"%s\", yetki = %d → %s\n",
           g.ad, g.yetki, g.yetki == 0 ? "sıradan kullanıcı" : "YÖNETİCİ");
    return 0;
}
