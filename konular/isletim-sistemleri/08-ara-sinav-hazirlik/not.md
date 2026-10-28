# İşletim Sistemleri: Ara Sınava Hazırlık — Sanallaştırma

Yedi hafta boyunca tek bir sorunun iki yarısını çözdük: **bir işlemciyi ve bir belleği, birçok programa her biri yalnızmış gibi nasıl paylaştırırız?** Bu not ara sınavın biçimini, nasıl çalışmanız gerektiğini ve sınavdakiyle aynı biçimde yazılmış örnek soruları toplar. Örnek soruların cevapları ve kısa çözümleri kapalı kutulardadır: önce kendiniz çözün, sonra açın.

---

## 1. Sınavın Biçimi

| | |
| --- | --- |
| **Biçim** | Tamamı çoktan seçmeli |
| **Soru sayısı** | 25 |
| **Seçenek** | 5 seçenek, **yalnızca biri doğru** |
| **Bağlı sorular** | Bazı sorular ortak bir duruma bağlıdır: "aşağıdaki üç soruyu bu duruma göre cevaplayınız" |
| **Hesap makinesi** | **Kullanılmaz.** Sayılar elle hesaplanacak kadar küçüktür |
| **Kapsam** | 1–7. hafta: proses, proses API'si, zamanlama, MLFQ ve orantılı pay, adres uzayı ve segmentasyon, sayfalama ve TLB, sayfa değiştirme |

**Yedi haftanın hepsinden soru gelir.** Sorular üç türdendir:

- **Kavram ayrımı:** iki benzer kavramdan hangisi? (zombi mi yetim mi, iç mi dış parçalanma mı)
- **Kod okuma:** kısa bir C programı ne yazar, kaç satır yazar, hangi sırayla yazar?
- **Elle izleme:** bir zamanlama çizelgesi, bir adres çevirisi ya da bir sayfa değiştirme dizisi

Elle izleme soruları bağlı soru olarak gelir: **bir kez doğru izlerseniz birkaç sorunun cevabını birden bulursunuz**, bir kez yanlış izlerseniz birkaçını birden kaybedersiniz. Kâğıda çizelgeyi çizmeden cevap seçmeyin.

> Süre ve oturum bilgisi ders sayfasında duyurulur.

---

## 2. Nasıl Çalışmalı?

**İşe yarayan**

1. **Her algoritma haftasında bir örneği simülatörle yeniden üretin.** `-s` ile yeni bir problem alın, kâğıtta çözün, `-c` ile kontrol edin. Simülatörlerin listesi ve kurulumu [Çalışma Ortamı](../00-calisma-ortami/not.md) notunda.
2. **Notlardaki elle izleme örneklerini kapatıp baştan çözün.** Sayıyı hatırlamak değil, çizelgeyi kurabilmek ölçülür.
3. **Her haftanın "Sık Yapılan Hatalar" bölümünü okuyun.** Yanlış seçenekler büyük ölçüde oradaki yanılgılardan gelir.
4. **C kodlarında önce tahmin edin, sonra çalıştırın.** `fork` sorularında prosesleri bir ağaç olarak çizin.

**İşe yaramayan**

- Notları baştan sona okumak. Okuduğunuzu tanırsınız, ama sınavda çizelgeyi kendiniz kurmanız gerekir.
- Yapay zekâ aracına çizelgeyi çözdürmek. Bu tür hesaplarda sık yanılırlar; sınavda yanınızda olmayacaklar.

---

## 3. Örnek Sorular

Bu sorular **sınavdaki sorular değildir**; sınavla aynı biçimde, aynı tür düşünmeyi isteyen örneklerdir.

### Örnek 1 — Kod okuma

Aşağıdaki program çalıştırılıyor, `fork` başarılıdır.

```c
int a = 1;
if (fork() == 0)
    a = a + 10;
else
    a = a * 10;
printf("%d\n", a);
```

Ekranda ne görünür?

A) Yalnızca `11`
B) Yalnızca `10`
C) `11` ve `10`; hangisinin önce yazılacağı garanti değildir
D) `11` ve `110`
E) `20` ve `20`

<details>
<summary>Cevap ve çözüm</summary>

**C.** `fork` sonrası iki proses vardır ve her birinin kendi `a`'sı vardır. Çocukta `fork` 0 döndürür: 1 + 10 = 11. Ebeveynde çocuğun PID'ini döndürür: 1 × 10 = 10. D'yi seçen iki prosesin aynı `a`'yı paylaştığını sanıyor. Sıra zamanlayıcının kararıdır.

</details>

### Örnek 2–3 — Bağlı sorular: zamanlama

**2. ve 3. soruları aşağıdaki duruma göre cevaplayınız.** Üç iş 0. birimde gelir; işlemci süreleri **A = 4, B = 1, C = 2**. Eşitlikte A, B, C sırası geçerlidir.

**Örnek 2.** FIFO ile ortalama dönüş süresi kaçtır?

A) 3,67 · B) 4,67 · C) 5,33 · D) 6,00 · E) 7,00

**Örnek 3.** Round Robin (zaman dilimi 1) ile ortalama dönüş süresi kaçtır?

A) 3,67 · B) 4,67 · C) 5,33 · D) 6,33 · E) 7,00

<details>
<summary>Cevap ve çözüm</summary>

**Örnek 2: C.** Çizelge `A A A A B C C`; bitişler 4, 5, 7 → 16 / 3 = 5,33.

**Örnek 3: B.** Çizelge `A B C A C A A`; bitişler B 2, C 5, A 7 → 14 / 3 = 4,67.

A şıkkı (3,67) SJF'nin sonucudur — iki soruda da çeldiricidir. Kontrol: `python scheduler.py -p FIFO -l 4,1,2 -c` ve `-p RR -q 1`.

</details>

### Örnek 4 — Kavram

MLFQ'da bir iş kendisine verilen zaman dilimini G/Ç yapmadan **tamamen** kullanırsa ne olur?

A) Sistemden çıkarılır
B) Aynı kuyrukta kalır
C) Bir alt (daha düşük öncelikli) kuyruğa iner
D) En üst kuyruğa çıkar
E) Bileti azaltılır

<details>
<summary>Cevap ve çözüm</summary>

**C.** MLFQ işin süresini bilmez; işin davranışından öğrenir. Dilimini dolduran iş "uzun olabilir" sayılır ve bir alt kuyruğa iner. E, piyango zamanlamasının kavramıdır, MLFQ'nun değil.

</details>

### Örnek 5 — Adres çevirme

Bir prosesin taban yazmacı **3072**, sınır yazmacı **500**'dür. Sanal adres **120** ve sanal adres **500** için sonuçlar sırasıyla nedir?

A) 3192 ve 3572
B) 3192 ve sınır ihlali
C) 120 ve 500
D) 3192 ve 3571
E) Sınır ihlali ve sınır ihlali

<details>
<summary>Cevap ve çözüm</summary>

**B.** 120 < 500 → 3072 + 120 = 3192. Geçerli adresler 0–499'dur; 500 sınırı aşar. A'yı seçen sınırı "son geçerli adres" sanıyor.

</details>

### Örnek 6 — Sayfalama

Adres uzayı **64 KB**, sayfa boyutu **4 KB** olan bir sistemde doğrusal sayfa tablosunda kaç giriş vardır?

A) 4 · B) 12 · C) 16 · D) 4096 · E) 65536

<details>
<summary>Cevap ve çözüm</summary>

**C.** 64 KB / 4 KB = 2¹⁶ / 2¹² = 2⁴ = 16 sayfa, her sayfa için bir giriş. A sanal sayfa numarasının **bit** sayısıdır, B ofsetin bit sayısıdır.

</details>

### Örnek 7–8 — Bağlı sorular: sayfa değiştirme

**7. ve 8. soruları aşağıdaki duruma göre cevaplayınız.** Bellekte **3 çerçeve** var, başlangıçta boş. Erişim dizisi: `1, 2, 3, 2, 4, 1, 5, 2`. Boş çerçeveye yerleşme de sayfa hatasıdır.

**Örnek 7.** OPT ile toplam kaç sayfa hatası olur?

A) 4 · B) 5 · C) 6 · D) 7 · E) 8

**Örnek 8.** LRU ile toplam kaç sayfa hatası olur?

A) 4 · B) 5 · C) 6 · D) 7 · E) 8

<details>
<summary>Cevap ve çözüm</summary>

**Örnek 7: B.** Hatalar 1, 2, 3, 4, 5'te; 2 (iki kez) ve 1 isabettir. 4 geldiğinde gelecekte en geç kullanılacak sayfa 3'tür (hiç kullanılmıyor), o çıkar; böylece 1 bellekte kalır.

**Örnek 8: D.** Yalnızca 4. erişim (2) isabettir. 4 geldiğinde en uzun süredir kullanılmayan 1 çıkar, sonra 1 geri gelince hata olur — LRU geçmişe bakar, OPT geleceğe.

Kontrol: `python paging-policy.py -a 1,2,3,2,4,1,5,2 -C 3 -p OPT -c` ve `-p LRU`.

</details>

---

## 4. Kendinizi Sınayın

Cevaplar verilmez; her birini simülatörün `-c` çıktısıyla kontrol edin.

1. `python scheduler.py -p SJF -l 6,2,4 -c` komutunu `-c` olmadan çalıştırıp ortalama dönüş, yanıt ve bekleme sürelerini elle bulun. Aynı işleri RR (dilim 2) ile çözün.
2. `python process-run.py -l 2:0,3:100 -L 3 -S SWITCH_ON_IO` için toplam süreyi tahmin edin. `SWITCH_ON_END` ile ne değişir?
3. `python relocation.py -s 4 -n 4` ile üretilen adresleri elle çevirin.
4. `python segmentation.py -a 1k -p 16k --b0 1024 --l0 200 --b1 8192 --l1 300 -A 50,250,1000,700` adreslerini elle çevirin. Hangileri hata verir?
5. `python paging-policy.py -a 5,1,2,5,3,1,4,5,2 -C 3 -p FIFO -c` dizisini FIFO, LRU ve OPT için elle çözün.
6. Önceki haftaların "Kendinizi Sınayın" bölümlerinden çözemediğiniz soruya dönün.

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
