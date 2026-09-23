# İşletim Sistemleri: CPU Zamanlama — FIFO, SJF, STCF, Round Robin

Geçen hafta işletim sisteminin işlemciyi **nasıl** geri aldığını gördük: zamanlayıcı kesmesi ve bağlam değişimi. Bu hafta asıl soruya geçiyoruz: işlemciyi geri aldıktan sonra **kime** vermeli? Bu sorunun cevabına **zamanlama politikası** (scheduling policy) denir.

Bu hafta dört politikayı aynı küçük iş yükleri üzerinde elle izleyeceğiz. Her sayı hem elle hem de simülatörle hesaplanmıştır; kâğıt ve kalemle takip edin.

---

## 1. Açılış: Süpermarket Kasası

Süpermarkette tek bir kasa açık. Kasada dolu arabasıyla biri var, arkasında elinde tek bir ekmekle iki kişi bekliyor. Kasiyer önce kimi almalı?

- "Önce gelen önce" dersek ekmekçiler dakikalarca bekler.
- "Az ürünü olan önce" dersek ekmekçiler hemen çıkar, dolu araba biraz gecikir — ama toplamda herkesin beklediği süre azalır.
- Peki dolu arabalı kişinin ürünleri okutulmaya başlanmışsa? Yarıda kesip ekmekçiyi mi almalı?

İşletim sistemi her birkaç milisaniyede bir tam olarak bu kararı verir. Kasa işlemcidir, müşteriler proseslerdir. Bu derste proseslere zamanlama bağlamında çoğu zaman **iş** (job) diyeceğiz.

---

## 2. İş Yükü Varsayımları

Zamanlamayı anlamanın en kolay yolu, gerçekçi olmayan basit varsayımlarla başlayıp onları **tek tek gevşetmektir**. Başlangıç varsayımlarımız:

| No | Varsayım | Gerçekte |
| :-: | -------- | -------- |
| 1 | Her iş aynı süre çalışır | Süreler çok farklıdır |
| 2 | Bütün işler aynı anda gelir | İşler farklı zamanlarda gelir |
| 3 | Başlayan iş bitene kadar çalışır | Zamanlayıcı kesmesiyle durdurulabilir |
| 4 | İşler yalnızca işlemci kullanır | Çoğu iş G/Ç da yapar |
| 5 | Her işin süresi önceden bilinir | Neredeyse hiç bilinmez |

Bu hafta ilk dört varsayımı gevşeteceğiz. Beşincisi — ve en gerçek dışı olanı — gelecek haftanın konusu.

---

## 3. Ölçütler

Bir politikanın iyi olup olmadığını söylemek için onu **ölçmemiz** gerekir. İki temel ölçüt kullanacağız:

> **Dönüş süresi** (turnaround time) = bitiş zamanı − varış zamanı
> Bir iş sisteme girdikten kaç birim sonra tamamen bitti?

> **Yanıt süresi** (response time) = ilk çalışma zamanı − varış zamanı
> Bir iş sisteme girdikten kaç birim sonra **ilk kez** işlemciye kavuştu?

Dönüş süresi toplu işler için (bir derleme, bir yedekleme) önemlidir: iş ne zaman bitecek? Yanıt süresi etkileşimli işler için önemlidir: bir tuşa bastım, ekran ne zaman tepki verecek?

Bir üçüncü ölçüt daha sık kullanılır: **bekleme süresi** (waiting time), bir işin hazır kuyruğunda, çalışmadan geçirdiği toplam zamandır. G/Ç yapmayan işler için **bekleme = dönüş − çalışma süresi**.

Karşılaştırmalarda bu ölçütlerin bütün işler üzerindeki **ortalamasını** kullanacağız.

### Çizelgeleri okumak

Bu nottaki zaman çizelgelerinde her kutu **bir zaman birimidir**. Üst satırdaki sayı kutunun başladığı andır: "3" numaralı kutu 3 ile 4 arasıdır. Bir iş en son 11 numaralı kutuda çalışıyorsa, **12 anında** biter.

---

## 4. FIFO: İlk Gelen İlk Hizmet Alır

En basit politika: işleri geliş sırasıyla çalıştır, her biri bitene kadar. **FIFO** (First In, First Out) ya da **FCFS** (First Come, First Served) denir.

### Eşit süreli işlerde

A, B ve C'nin her biri 4 birim sürsün ve hepsi 0 anında gelsin (sıra: A, B, C). FIFO'da A 0–4, B 4–8, C 8–12 arasında çalışır:

- Dönüş süreleri: 4, 8, 12 → ortalama **8**

Makul görünüyor. Şimdi 1. varsayımı gevşetelim.

### İş yükü 1: bir uzun, iki kısa iş

| İş | Varış | Süre |
| :-: | :---: | :--: |
| A | 0 | 8 |
| B | 0 | 2 |
| C | 0 | 2 |

![İş yükü 1: FIFO ve SJF zaman çizelgesi](assets/01-fifo-sjf.svg)

FIFO satırını okuyalım: A 0–8, B 8–10, C 10–12.

| İş | Bitiş | Dönüş | İlk çalışma | Yanıt |
| :-: | :---: | :---: | :---------: | :---: |
| A | 8 | 8 | 0 | 0 |
| B | 10 | 10 | 8 | 8 |
| C | 12 | 12 | 10 | 10 |
| **Ortalama** | | **10** | | **6** |

İki birimlik iki iş, sekiz birimlik bir işin arkasında sıkıştı. Buna **konvoy etkisi** (convoy effect) denir: yavaş bir aracın arkasında biriken araç kuyruğu gibi. Süpermarketteki dolu araba tam olarak budur.

FIFO'nun sonucu **geliş sırasına** bağlıdır: aynı üç iş B, C, A sırasıyla gelseydi ortalama dönüş 6 olurdu. Şansa bağlı bir politika istemiyoruz.

---

## 5. SJF: En Kısa İş Önce

Konvoy sorununun doğal çözümü: kısa işleri öne al. **SJF** (Shortest Job First) hazır işler arasından **en kısasını** seçer ve bitene kadar çalıştırır.

İş yükü 1'de SJF: B 0–2, C 2–4, A 4–12 (çizelgenin ikinci satırı).

| İş | Bitiş | Dönüş | Yanıt |
| :-: | :---: | :---: | :---: |
| A | 12 | 12 | 4 |
| B | 2 | 2 | 0 |
| C | 4 | 4 | 2 |
| **Ortalama** | | **6** | **2** |

Ortalama dönüş 10'dan **6**'ya indi. A'nın dönüşü 8'den 12'ye çıktı ama B ve C'nin kazancı bundan büyük.

> **SJF en iyisidir — bir şartla.** Bütün işler aynı anda geliyorsa, ortalama dönüş süresini en küçük yapan politika SJF'dir. Neden: art arda çalışan bir uzun ve bir kısa iş düşünün. Sıralarını değiştirip kısayı öne alırsanız kısa iş, uzunun süresi kadar erken biter; uzun iş ise yalnızca kısanın süresi kadar geç biter. Kazanç kayıptan büyük olduğu için her değiş tokuş ortalamayı düşürür — ta ki bütün işler kısadan uzuna dizilene kadar.

---

## 6. 2. Varsayımı Gevşetmek: İşler Farklı Zamanda Gelirse

### İş yükü 2

| İş | Varış | Süre |
| :-: | :---: | :--: |
| A | 0 | 8 |
| B | **1** | 2 |
| C | **1** | 2 |

0 anında sistemde yalnızca A vardır; SJF de onu seçer. 1 anında B ve C gelir, ama SJF **başlamış işi kesmez** — 3. varsayım hâlâ geçerli. A 8'e kadar çalışır, sonra B ve C.

| İş | Bitiş | Dönüş | İlk çalışma | Yanıt |
| :-: | :---: | :---: | :---------: | :---: |
| A | 8 | 8 − 0 = 8 | 0 | 0 |
| B | 10 | 10 − 1 = 9 | 8 | 7 |
| C | 12 | 12 − 1 = 11 | 10 | 9 |
| **Ortalama** | | 28 ÷ 3 ≈ **9,33** | | 16 ÷ 3 ≈ **5,33** |

Dönüş ve yanıt hesaplarken **varışı çıkarmayı** unutmayın; B'nin dönüşü 10 değil 9'dur. Konvoy geri döndü. SJF'nin "en iyi" olma şartı — hepsinin aynı anda gelmesi — bozulunca en iyilik de gitti.

---

## 7. STCF: Kalan Süresi En Kısa Olan Önce

3. varsayımı da bırakalım. Geçen hafta gördüğümüz zamanlayıcı kesmesi sayesinde işletim sistemi çalışan bir işi **yarıda kesebilir**. **STCF** (Shortest Time-to-Completion First) — bazı kaynaklarda SRTF (Shortest Remaining Time First) — her yeni iş geldiğinde şunu sorar: *Kalan süresi en kısa olan iş hangisi?* Gerekirse çalışan işi keser.

İş yükü 2'de 1 anında A'nın kalan süresi 7'dir, B ve C'ninki 2. STCF A'yı keser:

![İş yükü 2: SJF ve STCF zaman çizelgesi](assets/02-stcf.svg)

A 0–1, B 1–3, C 3–5, A 5–12.

| İş | Bitiş | Dönüş | İlk çalışma | Yanıt |
| :-: | :---: | :---: | :---------: | :---: |
| A | 12 | 12 − 0 = 12 | 0 | 0 |
| B | 3 | 3 − 1 = 2 | 1 | 0 |
| C | 5 | 5 − 1 = 4 | 3 | 2 |
| **Ortalama** | | 18 ÷ 3 = **6** | | 2 ÷ 3 ≈ **0,67** |

Ortalama dönüş 9,33'ten **6**'ya indi — işler aynı anda gelseydi SJF'nin elde edeceği değerle aynı. Varış zamanları farklı olduğunda ortalama dönüş için en iyi politika STCF'dir.

**SJF kesmeli değildir** (non-preemptive), **STCF kesmelidir** (preemptive). Aradaki tek fark, geçen haftanın zamanlayıcı kesmesidir.

---

## 8. Yeni Bir Ölçüt: Yanıt Süresi ve Round Robin

Bilgisayarlar yalnızca toplu iş çalıştırsaydı STCF'de durabilirdik. Ama kullanıcılar terminal başında oturur ve bir tuşa bastıklarında tepki beklerler. Onlar için önemli olan dönüş değil, **yanıt süresidir**.

### İş yükü 3: üç eşit iş

A, B ve C'nin her biri 4 birim sürsün, hepsi 0 anında gelsin. Süreler eşit olduğu için SJF'nin seçimi FIFO'nunkiyle aynıdır: A, B, C sırayla. C'nin kullanıcısı ilk tepkiyi görmek için **8 birim** bekler.

**Round Robin** (RR) başka bir yol izler: her işi yalnızca kısa bir **zaman dilimi** (time slice, quantum) kadar çalıştırır, sonra kuyruktaki sıradaki işe geçer. Dilimi biten iş kuyruğun sonuna gider.

![İş yükü 3: SJF ve RR (dilim 1)](assets/03-rr-esit.svg)

| Politika | Dönüş süreleri | Ort. dönüş | Yanıt süreleri | Ort. yanıt |
| -------- | -------------- | :--------: | -------------- | :--------: |
| SJF | 4, 8, 12 | **8** | 0, 4, 8 | 4 |
| RR, dilim 1 | 10, 11, 12 | 11 | 0, 1, 2 | **1** |

RR yanıt süresini 4'ten 1'e indirdi. Ama dönüş süresi 8'den 11'e çıktı: RR işleri **olabildiğince uzatır**, çünkü her birini bitirmeden ötekine geçer. Eşit süreli işlerde bu, dönüş için neredeyse en kötü durumdur.

> **Ödünleşim:** Dönüş süresinde iyi olan politika (SJF, STCF) yanıt süresinde kötüdür; yanıt süresinde iyi olan (RR) dönüş süresinde kötüdür. İkisini birden en iyi yapan bir politika yoktur. 1. haftanın tasarım hedefleri tablosu burada somutlaşıyor.

### İş yükü 1'e dönersek

Aynı karşılaştırmayı iş yükü 1'de de yapalım (A=8, B=2, C=2, hepsi 0'da):

| Politika | Ort. dönüş | Ort. yanıt |
| -------- | :--------: | :--------: |
| FIFO | 10 | 6 |
| SJF | **6** | 2 |
| RR, dilim 1 | 7,67 | **1** |
| RR, dilim 2 | 7,33 | 2 |

Tablonun her sayısı OSTEP simülatörüyle doğrulanmıştır (bölüm 12).

---

## 9. Zaman Dilimi Ne Kadar Olmalı?

![İş yükü 1: RR dilim 1 ve dilim 2](assets/04-rr-dilim.svg)

Dilim 1'de yanıt ortalaması 1, dilim 2'de 2'dir. Öyleyse dilim ne kadar küçükse o kadar iyi mi?

Hayır, çünkü her dilim sonunda bir **bağlam değişimi** yapılır ve bağlam değişimi bedava değildir: yazmaçlar kaydedilir, yüklenir, çekirdeğe girilip çıkılır. Bir örnek:

- Bağlam değişimi 1 ms, dilim 10 ms → her 11 ms'nin 1 ms'si geçişe gider: 1 ÷ 11 ≈ **%9** kayıp
- Bağlam değişimi 1 ms, dilim 100 ms → 1 ÷ 101 ≈ **%1** kayıp

Dilim, bağlam değişiminin maliyetini **örtecek kadar uzun**, sistemin tepkisiz görünmemesi için **yeterince kısa** seçilir. Çok uzun seçilirse RR, FIFO'ya dönüşür: her iş dilimi bitmeden kendi işini tamamlar.

Bağlam değişiminin gerçek bedeli yazmaç kaydetmekten ibaret değildir. Bir iş çalışırken işlemcinin içindeki hızlı saklama alanları o işin verileriyle dolar; başka bir işe geçince bu alanlar boşa çıkar ve yeniden dolması zaman alır. Bu donanım ayrıntısını gelecek dönem mimari tarafında göreceksiniz.

---

## 10. 4. Varsayımı Gevşetmek: G/Ç

Gerçek işler G/Ç yapar. G/Ç başlatan iş bloke olur ve işlemciyi kullanamaz. 1. haftadaki elle izleme örneğini hatırlayın: G/Ç sırasında işlemciyi başka bir işe vermek süreyi 11 birimden 7 birime indirmişti.

Zamanlayıcı bu durumu şöyle ele alır: G/Ç yapan bir işin her işlemci parçası **ayrı bir kısa iş** gibi değerlendirilir. Örneğin her 1 birim hesaptan sonra diske giden bir iş, STCF'nin gözünde art arda gelen 1 birimlik işler dizisidir; her biri kısa olduğu için öne alınır ve diski meşgul tutar. Uzun hesap yapan bir iş de G/Ç'nin sürdüğü boşluklarda çalışır. Sonuç: işlemci ve disk **aynı anda** çalışır.

Etkileşimli programlar — metin editörü, kabuk, tarayıcı — işlemciyi kısa süre kullanıp uzun süre kullanıcıyı bekleyen işlerdir. Kısa işleri öne alan politikalar bu yüzden etkileşimli sistemlerde doğal olarak iyi çalışır.

---

## 11. Özet Karşılaştırma

| Politika | Kesmeli mi? | Süreyi bilmesi gerekir mi? | İyi olduğu | Zayıf olduğu |
| -------- | :---------: | :------------------------: | ---------- | ------------ |
| FIFO | Hayır | Hayır | Basitlik | Konvoy etkisi, dönüş |
| SJF | Hayır | **Evet** | Dönüş (hepsi aynı anda gelirse) | Geç gelen kısa işler, yanıt |
| STCF | Evet | **Evet** | Dönüş | Yanıt; uzun işler aç kalabilir |
| RR | Evet | Hayır | Yanıt, adillik | Dönüş |

Tablonun üçüncü sütununa bakın: dönüşte en iyi iki politika, işlerin **ne kadar süreceğini bilmek zorundadır**. Gerçek bir işletim sistemi bunu bilemez — `ls` komutunun mu yoksa bir derlemenin mi daha uzun süreceğini önceden kimse söylemez. Bu, 5. varsayımdır ve gelecek haftanın konusudur.

Tablodaki "aç kalma" (starvation) ifadesine dikkat edin: STCF'de sürekli yeni kısa işler gelirse uzun iş **hiç** çalışamayabilir.

---

## 12. Simülatörle Deneyin

`cpu-sched` klasöründe `scheduler.py`:

```
python scheduler.py -p FIFO -l 8,2,2 -c
python scheduler.py -p SJF -l 8,2,2 -c
python scheduler.py -p RR -q 1 -l 8,2,2 -c
python scheduler.py -p RR -q 1 -l 4,4,4 -c
```

- `-l` iş sürelerini verir: `8,2,2` iş yükü 1'dir. İşlerin adları A, B, C değil `Job 0`, `Job 1`, `Job 2`'dir.
- `-p` politikayı seçer: `FIFO`, `SJF` veya `RR`. `-q` RR'nin dilimidir.
- Çıktıda `Response` yanıt, `Turnaround` dönüş, `Wait` bekleme süresidir.
- `-l` vermezseniz simülatör rastgele işler üretir; `-s` ile farklı tohumlar deneyin: `python scheduler.py -p SJF -j 3 -s 5`

> **Simülatörün sınırı:** `scheduler.py` bütün işlerin 0 anında geldiğini varsayar ve STCF'yi desteklemez. İş yükü 2 ve STCF sonuçları bu yüzden elle ve ayrı bir programla iki kez hesaplanarak doğrulanmıştır.

---

## 13. İyi Pratikler ve Sık Yapılan Hatalar

- **Dönüş süresini bitiş zamanıyla karıştırmak.** Dönüş = bitiş − **varış**. Varış 0 değilse bu fark önemlidir; iş yükü 2'de B'nin bitişi 10, dönüşü 9'dur.
- **Yanıt süresini bitiş zamanından hesaplamak.** Yanıt, işin **ilk** kez çalıştığı anla ilgilidir.
- **"SJF kesmelidir."** Değildir. Kesen, kalan süreye bakan STCF'dir.
- **"RR her zaman daha iyidir çünkü adildir."** Dönüş süresinde RR genellikle en kötülerdendir.
- **"Dilim ne kadar küçükse o kadar iyi."** Bağlam değişimi maliyeti bu düşünceyi çürütür.
- **Çizelgede kutunun başlangıcını bitiş sanmak.** 11 numaralı kutuda biten iş 12 anında biter.
- **Ortalamayı hesaplarken işlemcinin boş kaldığı zamanı bir iş gibi saymak.** Ortalama, **işler** üzerinden alınır.

---

## 14. Kendinizi Sınayın

Cevaplar verilmez. Elle çözün, sonra simülatörle kontrol edin (son iki soru elle).

1. `python scheduler.py -p SJF -l 200,100,300` ve aynı iş yükünü FIFO ile çalıştırın. Önce ortalama dönüş ve yanıt sürelerini kâğıtta hesaplayın.
2. Aynı iş yükünü `-p RR -q 1` ile çalıştırın. Yanıt süresi neden bu kadar küçük, dönüş süresi neden bu kadar büyük?
3. SJF hangi iş yüklerinde FIFO ile **aynı** sonucu verir? Bir örnek iş yüküyle gösterin ve `scheduler.py` ile doğrulayın.
4. RR hangi dilim uzunluğunda FIFO ile aynı sonucu verir? `-l 8,2,2` için `-q` değerini artırarak bulun.
5. İş yükü 2'yi (A=8 0'da, B=2 ve C=2 1'de) **RR, dilim 2** ile elle çizin. Kural: aynı anda gelen yeni iş, dilimi biten işten önce kuyruğa girer. Ortalama dönüş ve yanıt süresi kaçtır? STCF ile karşılaştırın.
6. STCF'de uzun bir işin **hiç** çalışamayacağı bir iş yükü kurun. Bu sorunu çözmek için ne önerirsiniz?

---

## İleri Okuma

- OSTEP, 7. bölüm — *Scheduling: Introduction*: <https://pages.cs.wisc.edu/~remzi/OSTEP/cpu-sched.pdf>

Bölümün sonundaki "Homework (Simulation)" soruları `scheduler.py` kullanır ve bu notun 14. bölümündeki sorularla aynı mantıktadır.

---

## Gelecek Hafta

Bu haftanın bütün iyi politikaları bir şeye ihtiyaç duydu: işin **ne kadar süreceğini** bilmek. Gelecek hafta bunu bilmeden iyi zamanlama yapmanın yolunu arayacağız. Fikir şudur: geleceği bilemiyorsak **geçmişe** bakarız. Kısa çalışıp işlemciyi bırakan iş muhtemelen etkileşimlidir; dilimini sonuna kadar kullanan iş muhtemelen uzundur. Bu gözleme dayanan **çok düzeyli geri beslemeli kuyruk** (MLFQ), BSD türevlerinin, Solaris'in ve Windows NT ailesinin zamanlayıcılarına temel olmuş bir fikirdir.

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
