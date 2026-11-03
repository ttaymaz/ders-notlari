/*
 * 02-yigin-korumasi.c — derleyicinin yığın koruması taşmayı yakalar
 *
 * Gösterdiği: Bir fonksiyonun yerel dizisi stack'tedir; hemen
 * yukarısında fonksiyonun DÖNÜŞ ADRESİ durur. Dizi taşarsa dönüş
 * adresi ezilebilir ve fonksiyon bittiğinde program saldırganın
 * seçtiği bir yere atlayabilir. Ubuntu'daki gcc, varsayılan olarak
 * dizinin ardına gizli bir değer (kanarya) koyar ve fonksiyon dönmeden
 * önce kontrol eder. Değer bozulmuşsa program DURDURULUR:
 *   *** stack smashing detected ***: terminated
 *
 * Derleme:    gcc 02-yigin-korumasi.c -o 02-yigin-korumasi.out
 * Çalıştırma: ./02-yigin-korumasi.out kisa
 *             ./02-yigin-korumasi.out AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
 */
#include <stdio.h>
#include <string.h>

void selamla(const char *ad)
{
    char tampon[16];
    strcpy(tampon, ad);                 /* boyut denetimi YOK */
    printf("merhaba, %s\n", tampon);
}                                       /* kanarya burada kontrol edilir */

int main(int argc, char *argv[])
{
    if (argc < 2) {
        printf("kullanım: %s AD\n", argv[0]);
        return 1;
    }
    selamla(argv[1]);
    printf("selamla() normal döndü\n");
    return 0;
}
