# İşletim Sistemleri: Bu Ders İçin Yeterli C — Adres, İşaretçi, struct

Bu derste C programı **yazmayacaksınız**; derste gösterilen kısa programları **okuyacak** ve çıktılarını tahmin edeceksiniz. Bu not, o programları okuyabilmeniz için gereken kadar C'yi toplar: adres, işaretçi, `malloc`, `struct` ve metin listesi. Konuyu daha önce görmediyseniz ya da unuttuysanız **5. haftadan önce** okuyun; adres uzayı konusu tamamen bunun üzerine kurulu.

Bu notun örnekleri `kod/` klasöründedir. Derlemek için bir Linux ortamı gerekir (bkz. [Çalışma Ortamı](../00-calisma-ortami/not.md)); derlemeden de notu okuyarak izleyebilirsiniz, bütün çıktılar aşağıda verildi.

---

## 1. Bellek ve Adres

Belleği **numaralı kutulardan** oluşan uzun bir sıra gibi düşünün. Her kutu bir bayt tutar, her kutunun bir numarası vardır. Bu numaraya **adres** denir.

Bir değişken tanımladığınızda derleyici ona bellekte yer ayırır. `int x = 42;` satırından sonra `x` belirli bir adreste durur. O adresi `&` işleci verir:

```c
int x = 42;
printf("%d\n", x);            /* 42 — değer */
printf("%p\n", (void *) &x);  /* 0x7ffd6b693d90 — adres */
```

- `%d` bir tamsayı yazdırır, `%p` bir **adres** yazdırır. Adresler onaltılık (`0x...`) yazılır.
- `(void *)` dönüşümü `printf`'in `%p` için beklediği türdür; şimdilik "adres yazdırırken böyle yazılır" demeniz yeterli.
- Adres her çalıştırmada değişebilir. Değer değişmez.

---

## 2. İşaretçi: Adres Tutan Değişken

Adresi bir değişkende saklayabilirsiniz. Böyle bir değişkene **işaretçi** (pointer) denir. Türü, gösterdiği değerin türüne `*` eklenerek yazılır:

```c
int x = 42;
int *p = &x;      /* p bir int'in adresini tutar: x'in adresini */

printf("%d\n", *p);   /* 42 — p'nin gösterdiği yerdeki değer */
*p = 100;             /* o yere 100 yaz */
printf("%d\n", x);    /* 100 — x değişti */
```

`*` iki farklı yerde iki farklı iş yapar; karışıklığın asıl kaynağı budur:

| Nerede | Örnek | Anlamı |
| ------ | ----- | ------ |
| Tanımda | `int *p` | "p bir `int` adresi tutar" |
| Kullanımda | `*p` | "p'nin gösterdiği adresteki değer" |

`&` ile `*` birbirinin tersidir: `&x` "x'in adresi", `*p` "p adresindeki değer". Dolayısıyla `*&x` yine `x`'tir.

---

## 3. Neden Adres Veririz? `wait(&durum)`

C'de bir fonksiyona değişken verildiğinde fonksiyon onun **kopyasını** alır; kopyayı değiştirmek asıl değişkeni değiştirmez. Fonksiyonun çağıranın değişkenini değiştirmesi gerekiyorsa değişkenin **adresi** verilir:

```c
void yediyi_yaz(int *hedef)
{
    *hedef = 7;           /* çağıranın değişkenine yaz */
}

int durum = 0;
yediyi_yaz(&durum);       /* durum artık 7 */
```

2. haftada göreceğiniz `wait(&durum)` çağrısı tam olarak budur: `wait`, beklediği çocuğun çıkış bilgisini sizin `durum` değişkeninize yazar. `&` yazılmasa `wait` nereye yazacağını bilemezdi.

---

## 4. `malloc` ve `free`: Heap'ten Yer İstemek

Yerel değişkenler fonksiyon bitince yok olur. Daha uzun yaşayacak ya da boyutu çalışırken belli olacak bir yer gerekiyorsa `malloc` ile **heap**'ten istenir. `malloc` ayrılan yerin **adresini** döndürür; bu yüzden sonucu bir işaretçide tutulur:

```c
int *dizi = malloc(3 * sizeof(int));   /* üç int'lik yer */
if (dizi == NULL) { /* yer kalmadı */ }
dizi[0] = 10;
dizi[1] = 20;
free(dizi);                            /* yeri geri ver */
```

- `sizeof(int)` bir `int`'in kaç bayt tuttuğunu verir (bugünkü sistemlerde 4).
- `malloc` başarısız olursa `NULL` döndürür. `NULL` "hiçbir yeri göstermeyen adres"tir.
- `free` ile geri verilmeyen yer, program bitene kadar boşa tutulur.
- Bir işaretçi dizi gibi kullanılabilir: `dizi[1]`, `dizi` adresinden bir eleman ötedeki değerdir.

Program çalıştırılınca `x` ile `dizi`nin adresleri birbirinden çok uzak çıkar:

```
x'in adresi    : 0x7ffd6b693d90
heap'teki dizi : 0x5c01cd79c2b0, ikinci eleman 20
```

İlki stack'te (yerel değişken), ikincisi heap'te. Neden bu kadar uzak oldukları 5. haftanın konusu.

---

## 5. `struct`: Birkaç Alanı Tek Kayıtta Toplamak

`struct`, birbirine ait birkaç değeri tek bir kayıt olarak tutar. İşletim sistemi her proses için böyle bir kayıt tutar:

```c
struct proses {
    int pid;
    char durum;                 /* 'H' hazır, 'C' çalışıyor, 'B' bloke */
    struct proses *sonraki;     /* bir sonraki kaydın adresi */
};
```

Alanlara iki yolla erişilir:

| Elinizde ne var? | Yazılış | Örnek |
| ---------------- | ------- | ----- |
| Kaydın kendisi | nokta `.` | `ilk.pid = 1;` |
| Kaydın **adresi** | ok `->` | `p->pid = 10;` |

`p->pid`, `(*p).pid` ifadesinin kısa yazılışıdır: "p'nin gösterdiği kaydın `pid` alanı". Çekirdek kodunda neredeyse her zaman kayıtların adresiyle çalışıldığı için okla çok karşılaşırsınız.

---

## 6. Bağlı Liste: Kayıtların Birbirini Göstermesi

`sonraki` alanı bir kaydın adresini tutar. Her kayıt bir sonrakinin adresini tutunca kayıtlar zincir olur: **bağlı liste**. Son kaydın `sonraki` alanı `NULL`'dır.

```c
struct proses *bas = yeni_proses(10, 'C');
bas->sonraki = yeni_proses(11, 'H');
bas->sonraki->sonraki = yeni_proses(12, 'B');

for (struct proses *p = bas; p != NULL; p = p->sonraki)
    printf(" [%d %c]", p->pid, p->durum);
```

```
proses listesi : [10 C] [11 H] [12 B]
```

Döngüyü şöyle okuyun: `p` baştan başlar, her adımda `p = p->sonraki` ile bir sonraki kayda geçer, `NULL`'a gelince durur. Zamanlayıcının hazır prosesleri ararken yaptığı iş özünde budur.

---

## 7. Metin ve Metin Listesi: `char *argv[]`

C'de metin, sonu özel bir `'\0'` karakteriyle biten bir karakter dizisidir; metne onun **ilk karakterinin adresiyle** erişilir. Bu yüzden metnin türü `char *` yazılır:

```c
char *komut = "ls";
```

Birden çok metin bir **metin listesinde** tutulur: her elemanı bir `char *` olan dizi. Listenin sonu `NULL` ile işaretlenir, böylece listeyi alan fonksiyon nerede duracağını bilir:

```c
char *argumanlar[] = { "ls", "-l", "/tmp", NULL };
```

2. haftadaki `exec` çağrısı çalıştıracağı programın komut satırını tam bu biçimde ister. `main(int argc, char *argv[])` biçiminde gördüğünüz `argv` da aynı şeydir: programa komut satırından verilen kelimeler.

---

## 8. `void *` ve Sık Hatalar

**`void *`**, türü belirtilmemiş bir adrestir: "bir yerin adresi, ama orada ne olduğunu söylemiyorum". `malloc` bu türü döndürür. 1. haftadaki `struct proses` yapısındaki `void *bellek` alanı ve eşzamanlılık haftalarında iş parçacığına verilen argüman da bu türdendir.

Sık yapılan hatalar ve sonuçları:

| Hata | Sonuç |
| ---- | ----- |
| Değer atanmamış bir işaretçiyi kullanmak: `int *p; *p = 5;` | `p` rastgele bir yeri gösterir; program çöker ya da başka bir değişkeni bozar |
| `NULL` işaretçinin gösterdiği yere erişmek | Program **segmentation fault** ile çöker |
| `free` edilmiş yeri kullanmak | Tanımsız davranış: bazen çalışır, bazen çöker |
| `scanf("%d", x)` — `&` unutulmuş | `scanf` `x`'in değerini adres sanıp oraya yazmaya çalışır |

"Segmentation fault" adı 5. haftada anlam kazanacak: işletim sistemi, prosesin kendisine ait olmayan bir adrese eriştiğini yakalayıp onu durdurur.

---

## 9. Bu Derste Nerede Karşınıza Çıkacak?

| Hafta | Nerede | Bu notta |
| :---: | ------ | -------- |
| 1 | `struct proses` — proses kontrol bloğu | Bölüm 5, 6 |
| 2 | `wait(&durum)`, `exec` için `char *argumanlar[]` | Bölüm 3, 7 |
| 5 | `%p` ile adres yazdırma, `malloc`, stack ve heap adresleri | Bölüm 1, 4 |
| 6 | Dizi elemanlarına erişim ve bellek erişim süresi | Bölüm 4 |
| 9–11 | İş parçacığına `void *` argüman, paylaşılan değişkenin adresi | Bölüm 3, 8 |

---

## 10. Kendinizi Sınayın

Cevaplar verilmez. Önce kâğıtta tahmin edin, sonra `kod/` klasöründeki programları değiştirip çalıştırarak kontrol edin.

1. `int a = 5; int *p = &a; *p = *p + 1;` satırlarından sonra `a` kaçtır?
2. `int a = 5; int *p = &a; int b = *p; b = 9;` satırlarından sonra `a` kaçtır? Neden?
3. `yediyi_yaz` fonksiyonu `void yediyi_yaz(int hedef) { hedef = 7; }` biçiminde yazılsaydı, `yediyi_yaz(durum)` çağrısından sonra `durum` kaç olurdu?
4. Elinizde `struct proses *p` var. `pid` alanına erişmenin iki yazılışı nedir?
5. Bölüm 6'daki listede `bas->sonraki->pid` kaçtır? `bas->sonraki->sonraki->sonraki` nedir?
6. `char *argumanlar[] = { "ls", "-l", NULL };` listesinde `NULL` unutulursa, listeyi sonuna kadar okuyan bir fonksiyon ne yapar?
7. `01-adres-ve-isaretci.c` programını iki kez çalıştırın. Hangi satırlar değişiyor, hangileri aynı kalıyor?

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
