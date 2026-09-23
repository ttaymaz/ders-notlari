# İşletim Sistemleri: MLFQ, Orantılı Pay ve Çok Çekirdekli Zamanlama

Geçen haftanın sonunda bir duvara çarptık. Ortalama dönüş süresinde en iyi iki politika — SJF ve STCF — her işin **ne kadar süreceğini** bilmek zorundaydı. Yanıt süresinde iyi olan Round Robin ise dönüş süresinde kötüydü. Bu hafta üç soruya cevap arıyoruz:

1. İşin süresini bilmeden hem kısa işleri öne alabilir hem de etkileşimli işlere hızlı yanıt verebilir miyiz? → **MLFQ**
2. Amaç dönüş ya da yanıt değil de "herkese hakkı kadar pay" ise? → **Orantılı pay**
3. Tek değil birden çok çekirdek varsa ne değişir? → **Çok çekirdekli zamanlama**

---

## 1. Geleceği Bilemiyorsak Geçmişe Bakarız

Bir hastane acil servisini düşünün. Görevli, hastanın tedavisinin ne kadar süreceğini kapıdan bilemez. Ama gözlem yapabilir: kısa bir muayeneyle çıkan hasta muhtemelen hafif bir vakadır; saatlerdir yatakta olan hasta ağır bir vakadır. Görevli, tahminini hastanın **şimdiye kadarki davranışına** göre günceller.

**Çok düzeyli geri beslemeli kuyruk** (Multi-Level Feedback Queue, MLFQ) tam olarak bunu yapar. İlk kez 1962'de Corbató ve arkadaşlarının CTSS sisteminde kullanıldı; bu çalışma, Corbató'ya 1990'da Turing Ödülü'nü getiren çalışmaların parçasıdır. Temel fikir:

- İşlemciyi kısa süre kullanıp bırakan iş (örneğin bir tuşa basılmasını bekleyen editör) muhtemelen **etkileşimlidir** → yüksek öncelik.
- Dilimini sonuna kadar kullanan iş muhtemelen **uzun bir hesaplamadır** → düşük öncelik.

"Geri besleme" kelimesi buradan gelir: işin önceliği, gözlenen davranışına göre sürekli ayarlanır.

---

## 2. MLFQ'nun Yapısı: Birden Çok Kuyruk

MLFQ'da her biri farklı bir **öncelik düzeyine** sahip birkaç kuyruk vardır. Bu notta üç kuyruk kullanacağız: **Q2** en yüksek, **Q0** en düşük öncelik. Her hazır iş bu kuyruklardan tam birinde durur. İlk iki kural hangi işin çalışacağını belirler:

> **Kural 1:** A'nın önceliği B'ninkinden yüksekse A çalışır, B çalışmaz.<br>
> **Kural 2:** A ile B aynı öncelikteyse, aralarında Round Robin uygulanır.

Bu iki kural tek başına yeterli değildir: öncelikler hiç değişmeseydi üst kuyruktaki işler alttakileri sonsuza kadar bekletirdi. Asıl mesele **önceliğin nasıl değiştiğidir**:

> **Kural 3:** Sisteme yeni giren iş **en üst** kuyruğa yerleştirilir.<br>
> **Kural 4 (ilk hali):** İş dilimini **sonuna kadar kullanırsa** bir alt kuyruğa iner. Dilim bitmeden işlemciyi bırakırsa (örneğin G/Ç için) aynı düzeyde kalır.

Kural 3'ün mantığı şudur: MLFQ yeni gelen işin kısa mı uzun mu olduğunu bilmez, bu yüzden önce kısa olduğunu **varsayar**. Kısaysa üst kuyrukta hızla biter. Uzunsa dilimlerini doldurdukça yavaş yavaş aşağı iner. MLFQ böylece, işin süresini bilmeden SJF'ye yaklaşır.

---

## 3. Elle İzleme: Uzun İş ve Kısa İş

**Kurulum:** 3 kuyruk (Q2, Q1, Q0), her düzeyde dilim 1 birim.

| İş | Varış | Süre |
| :-: | :---: | :--: |
| A | 0 | 8 |
| B | 2 | 2 |

![MLFQ zaman çizelgesi: satırlar kuyruk düzeyleri](assets/01-mlfq.svg)

Bu çizelgede satırlar işler değil, **kuyruk düzeyleridir**; bir kutudaki harf, o anda o düzeyden çalışan işi gösterir.

| Zaman | Ne oldu | Neden |
| :---: | ------- | ----- |
| 0 | A, Q2'de çalışır | Kural 3: yeni iş en üstte başlar |
| 1 | A, Q1'de çalışır | Kural 4: A dilimini doldurdu, bir düzey indi |
| 2 | B gelir, Q2'de çalışır | Kural 3 ve 1: B en üstte, A'dan yüksek öncelikte |
| 3 | B, Q1'de çalışır | B dilimini doldurdu ve indi; ama hâlâ Q0'daki A'dan yüksekte |
| 4–9 | A, Q0'da çalışır | B bitti; sistemde yalnızca A kaldı |

- **B:** 4'te biter → dönüş 4 − 2 = **2**, yanıt 2 − 2 = **0**
- **A:** 10'da biter → dönüş **10**, yanıt **0**
- Ortalama dönüş **6**, ortalama yanıt **0**

B, geldiği anda işlemciyi aldı ve kesintisiz bitti. MLFQ, B'nin kısa olduğunu **bilmeden** ona kısa bir iş gibi davrandı: A zaten dilimlerini doldurup aşağı inmişti. Bu çizelge `mlfq.py` simülatörüyle birebir üretilir (bölüm 10).

---

## 4. İlk Halin Üç Sorunu

Kural 1–4 zarif görünür ama üç ciddi açığı vardır.

**Aç kalma.** Sistemde çok sayıda etkileşimli iş varsa, bunlar hep dilim bitmeden işlemciyi bırakır ve hep üst kuyrukta kalırlar. Birlikte işlemciyi sürekli meşgul edebilirler. Alt kuyruğa inmiş uzun iş **hiç** çalışamaz.

**Zamanlayıcıyı oyunla kandırmak.** Kural 4'ün ilk hali bir açık bırakır: dilimin %99'unu kullanıp tam bitmeden önemsiz bir G/Ç yapan iş, dilimi doldurmamış sayılır ve **üst kuyrukta kalır**. Kurnazca yazılmış bir program böylece işlemcinin neredeyse tamamını ele geçirebilir.

**Davranış değişimi.** Bir iş hayatına uzun bir hesaplamayla başlayıp sonra etkileşimli hale gelebilir (örneğin veriyi yükledikten sonra kullanıcıyla konuşmaya başlayan bir program). İlk hal bir işi bir kez aşağı indirdikten sonra onu **hiç yukarı çıkarmaz**.

---

## 5. Kural 5: Öncelik Yükseltme

İlk ve üçüncü sorunun ortak çözümü basittir:

> **Kural 5:** Belirli bir **S** süresi dolduğunda sistemdeki bütün işleri en üst kuyruğa taşı.

Böylece aç kalan iş en geç S birim sonra üst kuyruğa çıkar ve orada Round Robin ile pay alır. Etkileşimli hale gelen iş de yükseltmeyle birlikte hak ettiği yere döner.

S ne olmalı? Çok büyükse uzun işler yine uzun süre aç kalır; çok küçükse etkileşimli işler üst kuyruğu uzun işlerle paylaşmak zorunda kalır ve yanıt süresi kötüleşir. Doğru değer iş yüküne bağlıdır. OSTEP'in yazarları bu tür ayar sabitlerine yarı şaka *"voodoo sabitleri"* der: doğru değeri bulmak bilimden çok deneyim ister.

---

## 6. Kural 4'ün Yeni Hali: Muhasebe

İkinci sorun — oyunla kandırma — kural 5 ile çözülmez. Çözüm, kural 4'ün kendisini değiştirmektir. İşin dilimi "bir seferde doldurup doldurmadığına" değil, o düzeyde **toplam ne kadar süre kullandığına** bakılır:

> **Kural 4 (yeni hali):** Bir iş bir düzeyde kendisine tanınan **süre hakkını** (allotment) doldurduğunda — işlemciyi kaç kez bırakmış olursa olsun — bir alt kuyruğa iner.

Artık dilimin %99'unu kullanıp G/Ç yapan iş kazanç sağlamaz: kullandığı süreler birikir ve hakkı dolunca aşağı iner.

### Üç kuralın simülatörde karşılaştırılması

Bu etkiyi elle izlemek için iş yükü çok uzun olur, ama simülatörde net görülür. 3 kuyruk, dilim 10 birim:

- **A:** 0'da gelir, 30 birim, hiç G/Ç yapmaz
- **B ve C:** 10'da gelir, her biri 30 birim; **her birimden sonra** 1 birimlik G/Ç yapar — dilim bitmeden hep bırakırlar

B ve C, G/Ç'leri sırayla yaptıkları için işlemciyi kesintisiz paylaşabilirler. A ise 0–10 arasında dilimini doldurup aşağı inmiştir. A'nın kaderi kurala bağlıdır:

| Kural | A, 10'dan sonra ilk ne zaman çalışır? | A'nın dönüş süresi |
| ----- | :-----------------------------------: | :----------------: |
| Kural 4 ilk hali, yükseltme yok | **70** — B ve C bitene kadar aç | 90 |
| Kural 4 ilk hali + Kural 5 (S = 20) | 20 | 50 |
| Kural 4 yeni hali (muhasebe), yükseltme yok | 30 | 70 |

İlk satır aç kalmanın ta kendisidir: A 60 birim boyunca hiç çalışamaz. Yükseltme A'yı her 20 birimde bir üst kuyruğa taşır. Muhasebe ise B ve C'yi 10'ar birimlik hakları dolunca aşağı indirir; A, onlar kendi düzeyine indiğinde yeniden pay alır. Bu satırların komutları bölüm 10'dadır.

---

## 7. MLFQ: Beş Kural

1. **Kural 1:** Öncelik(A) > Öncelik(B) ise A çalışır.
2. **Kural 2:** Öncelik(A) = Öncelik(B) ise A ve B Round Robin ile çalışır.
3. **Kural 3:** Yeni iş en üst kuyruğa girer.
4. **Kural 4:** Bir düzeydeki süre hakkını dolduran iş, kaç kez bırakmış olursa olsun, bir alt kuyruğa iner.
5. **Kural 5:** Her S sürede bir bütün işler en üst kuyruğa taşınır.

Gerçek sistemler bunlara ayar ekler. Tipik olarak **üst kuyruklarda dilim kısa**, alt kuyruklarda uzundur: üstteki etkileşimli işler hızla dönsün, alttaki uzun işler de sık sık kesilip bağlam değişimi harcamasın. BSD Unix türevleri, Solaris ve Windows NT ailesi farklı biçimlerde MLFQ fikrine dayanan zamanlayıcılar kullanmıştır.

---

## 8. Orantılı Pay

Şimdiye kadar hedefimiz dönüş ya da yanıt süresini iyileştirmekti. Bazen hedef başkadır: bir üniversite sunucusunda üç bölümün paylaştığı işlemcinin **%50'si** birine, **%30'u** ikincisine, **%20'si** üçüncüsüne ait olmalıdır. Buna **orantılı pay** (proportional share) ya da adil pay zamanlaması denir.

Temel kavram **bilettir** (ticket): bir işin sahip olduğu bilet sayısı, işlemciden alması gereken payı temsil eder. Bu bölümdeki örneklerde üç işin biletleri:

| İş | Bilet | Hedef pay |
| :-: | :---: | :-------: |
| A | 3 | 3/6 = %50 |
| B | 2 | 2/6 ≈ %33 |
| C | 1 | 1/6 ≈ %17 |

### 8.1 Piyango zamanlaması

Her dilimin başında zamanlayıcı bir **piyango çeker**: 0 ile toplam bilet sayısı − 1 arasında rastgele bir sayı üretir. Biletler işlere sırayla dağıtılmıştır — A'nın biletleri 0, 1, 2; B'ninkiler 3, 4; C'ninki 5 — ve kazanan bilet kimdeyse o çalışır. OSTEP'in `lottery.py` simülatörü rastgele sayıyı büyük bir tamsayı olarak verir; kazanan bilet, bu sayının **toplam bilete bölümünden kalandır**.

Simülatörün 2 numaralı tohumla ürettiği ilk altı çekiliş:

| Dilim | Rastgele sayı | mod 6 | Kazanan |
| :---: | ------------: | :---: | :-----: |
| 0 | 956035 | 1 | A |
| 1 | 947828 | 2 | A |
| 2 | 56551 | 1 | A |
| 3 | 84872 | 2 | A |
| 4 | 835499 | 5 | C |
| 5 | 735970 | 4 | B |

İlk 12 çekilişin tamamı aşağıdaki çizelgenin üst satırındadır:

![Piyango ve adım zamanlaması, bilet oranı 3:2:1](assets/02-piyango-adim.svg)

12 dilimde hedef A 6, B 4, C 2 dilimdi. Piyango **A 7, B 3, C 2** verdi. Piyango **kısa vadede** hedeften sapar; çekiliş sayısı arttıkça oran hedefe yaklaşır. Rastgelelik bir kusur gibi görünse de üç avantaj sağlar:

- **Durum tutmaz:** Zamanlayıcının geçmişi hatırlaması gerekmez; her dilim bağımsız bir çekiliştir.
- **Yeni iş eklemek kolaydır:** Yeni işin biletleri toplama eklenir, o kadar.
- **Kötü durumlara karşı dayanıklıdır:** Belirli bir iş yükünün her seferinde tökezlettiği bir deterministik kural yoktur.

### 8.2 Adım zamanlaması

Rastgelelik istemiyorsak **adım zamanlaması** (stride scheduling) aynı oranı **tam olarak** sağlar:

- Her işin **adımı** (stride), büyük bir sabitin bilet sayısına bölümüdür. Sabit 6 seçilirse: A 6/3 = **2**, B 6/2 = **3**, C 6/1 = **6**.
- Her işin bir **geçiş değeri** (pass) vardır, başlangıçta 0.
- Her dilimde **geçiş değeri en küçük** iş çalışır; eşitlikte liste sırası (A, B, C). Çalışan işin geçiş değeri adımı kadar artar.

| Dilim | Geçiş (A, B, C) önce | Çalışan | Geçiş sonra |
| :---: | :------------------: | :-----: | :---------: |
| 0 | 0, 0, 0 | A | 2, 0, 0 |
| 1 | 2, 0, 0 | B | 2, 3, 0 |
| 2 | 2, 3, 0 | C | 2, 3, 6 |
| 3 | 2, 3, 6 | A | 4, 3, 6 |
| 4 | 4, 3, 6 | B | 4, 6, 6 |
| 5 | 4, 6, 6 | A | 6, 6, 6 |

Altı dilimde A 3, B 2, C 1 kez çalıştı — tam olarak 3:2:1. 6. dilimde geçiş değerleri yine eşitlenir ve aynı düzen tekrar eder (çizelgenin alt satırı).

Adım zamanlamasının bedeli **durum tutmasıdır**. Sisteme yeni bir iş geldiğinde geçiş değeri ne olmalı? 0 verilirse, diğerlerinin geçiş değerleri büyümüş olduğu için yeni iş bir süre işlemciyi tekeline alır. Piyangoda böyle bir soru yoktur.

### 8.3 Linux'ta orantılı pay

Linux uzun yıllar **CFS** (Completely Fair Scheduler) adlı bir zamanlayıcı kullandı. CFS her işin **sanal çalışma süresini** (virtual runtime) tutar ve her kararda sanal süresi en küçük olan işi seçer — adım zamanlamasının geçiş değerine çok benzer bir fikir. İşlerin ağırlığı `nice` değeriyle ayarlanır: ağırlığı yüksek işin sanal süresi daha yavaş artar, bu yüzden daha sık seçilir. İşler, en küçüğü hızla bulunabilsin diye dengeli bir ikili ağaçta (kırmızı-siyah ağaç) tutulur. Linux 6.6 sürümüyle CFS'nin yerini **EEVDF** adlı zamanlayıcı aldı; o da aynı sanal zaman fikri üzerine kuruludur.

---

## 9. Çok Çekirdekli Zamanlama

Şimdiye kadar tek bir işlemci varsaydık. Bugünün bilgisayarlarında 4, 8, 16 çekirdek olağandır. Birden çok çekirdek iki yeni soru getirir: işler **hangi çekirdekte** çalışacak ve çekirdekler arasında **nasıl dengelenecek**?

### Aynı çekirdekte kalmanın faydası

Her çekirdeğin yanında, o çekirdeğin **son kullandığı verileri** tutan küçük ve çok hızlı bir bellek bulunur. Bir iş aynı çekirdekte çalışmaya devam ederse verisinin bir kısmı orada hazır bekler. Başka bir çekirdeğe taşınırsa o çekirdeğin hızlı belleği bu işin verisini bilmez; veri yavaş ana bellekten yeniden getirilir. Bu yüzden bir işi mümkün olduğunca **aynı çekirdekte** tutmak istenir. Buna **işlemci yakınlığı** (processor affinity) denir.

Bu hızlı belleğin adı **önbellektir** (cache). Aynı fikri — "son kullanılanı yakında tut" — 6. haftada adres çevirisi için TLB ile ayrıntılı göreceğiz; donanım tarafını gelecek dönem mimari derslerinde göreceksiniz.

### Tek kuyruk mu, çekirdek başına kuyruk mu?

**Kurulum:** İki çekirdek (Ç0, Ç1), üç iş A, B, C — her biri 4 birim, hepsi 0'da; dilim 1.

![Tek ortak kuyruk ile çekirdek başına kuyruk](assets/03-cok-cekirdek.svg)

**Tek ortak kuyruk:** Bütün işler tek bir kuyrukta durur; boşalan çekirdek kuyruğun başındakini alır. Toplam iş 3 × 4 = 12 birim, iki çekirdek var; iş **6 birimde** biter ve hiçbir çekirdek boş kalmaz. Ama çizelgenin ilk iki satırına bakın: A önce Ç0'da, sonra Ç1'de, sonra yine Ç0'da çalışıyor. İşler çekirdekten çekirdeğe **atlıyor** ve yakınlık kayboluyor. Üstelik iki çekirdek aynı kuyruğa aynı anda erişeceği için kuyruğun bir **kilitle** korunması gerekir (9. hafta); çekirdek sayısı arttıkça bu kilit darboğaz olur.

**Çekirdek başına kuyruk:** İşler başta çekirdeklere dağıtılır ve her çekirdek yalnızca kendi kuyruğundan iş alır. Bu örnekte A ve C Ç0'a, B Ç1'e düşer. İşler hiç çekirdek değiştirmez, ortak kilit gerekmez. Ama B 4'te biter ve **Ç1, 4–8 arasında boş** kalır; iş **8 birimde** biter. Buna **yük dengesizliği** (load imbalance) denir.

Çözüm **iş çalmadır** (work stealing): kuyruğu boşalan çekirdek, ara sıra diğer çekirdeklerin kuyruklarına bakar ve kalabalık olandan bir iş alır. Ne sıklıkla bakılacağı yine bir ödünleşimdir: sık bakmak dengeyi iyileştirir ama yakınlığı bozar ve ek iş getirir. Linux gibi gerçek sistemler çekirdek başına kuyruk ve düzenli yük dengeleme kullanır.

---

## 10. Simülatörle Deneyin

**MLFQ** — `cpu-sched-mlfq` klasörü. Bölüm 3'teki örnek:

```
python mlfq.py -n 3 -q 1 -a 1 -l 0,8,0:2,2,0 -c
```

- `-n` kuyruk sayısı, `-q` dilim, `-a` her düzeydeki süre hakkı (dilim cinsinden)
- `-l` işler: `varış,süre,G/Ç sıklığı` biçiminde, iki nokta ile ayrılır. `0` G/Ç yok demektir
- Çıktıda işler `JOB 0`, `JOB 1`; öncelik `PRIORITY 2` en yüksektir

Bölüm 6'daki tablonun üç satırı:

```
python mlfq.py -n 3 -q 10 -a 1 -i 1 -l 0,30,0:10,30,1:10,30,1 -S -c
python mlfq.py -n 3 -q 10 -a 1 -i 1 -l 0,30,0:10,30,1:10,30,1 -S -B 20 -c
python mlfq.py -n 3 -q 10 -a 1 -i 1 -l 0,30,0:10,30,1:10,30,1 -c
```

`-S` kural 4'ün **ilk halini** (G/Ç yapan iş düzeyinde kalır), `-B` kural 5'in yükseltme aralığını seçer. `-S` verilmezse simülatör kural 4'ün yeni halini — muhasebeyi — uygular. `-i` G/Ç süresidir.

**Piyango** — `cpu-sched-lottery` klasörü:

```
python lottery.py -l 12:3,12:2,12:1 -s 2 -c
```

`-l` işleri `süre:bilet` biçiminde verir. `-c` olmadan çalıştırırsanız simülatör rastgele sayıları gösterir ve kazananı sizin bulmanızı ister.

**Çok çekirdek** — `cpu-sched-multi` klasörü:

```
python multi.py -n 2 -q 1 -L a:4:100,b:4:100,c:4:100 -t -c
python multi.py -n 2 -q 1 -L a:4:100,b:4:100,c:4:100 -p -t -c
```

`-n` çekirdek sayısı, `-p` çekirdek başına kuyruk, `-t` hangi işin nerede çalıştığını gösterir. `-L` işleri `ad:süre:çalışma kümesi` biçiminde verir; üçüncü alan hızlı belleğin modellenmesiyle ilgilidir ve bu hafta 100'de sabit kalabilir.

---

## 11. İyi Pratikler ve Sık Yapılan Hatalar

- **"MLFQ işin süresini bilir."** Bilmez; davranışını gözleyerek **tahmin eder**. Bütün değeri de buradadır.
- **"Yüksek öncelikli kuyruk büyük numaralıdır" ile "küçük numaralıdır"ı karıştırmak.** Numaralandırma kaynaktan kaynağa değişir. Bu notta ve simülatörde **büyük numara yüksek öncelik**tir; bir soruyu çözmeden önce hangi düzenin kullanıldığına bakın.
- **Kural 5'i unutmak.** Yükseltme olmadan MLFQ aç kalmaya açıktır.
- **Kural 4'ün iki halini karıştırmak.** İlk hal "dilimi bir seferde doldurdu mu?" diye sorar ve kandırılabilir; yeni hal "bu düzeyde toplam ne kadar kullandı?" diye sorar.
- **"Piyango adil değildir çünkü rastgeledir."** Kısa vadede sapar, uzun vadede oran tutar. Adım zamanlaması tam oranı verir ama durum tutar.
- **"Çok çekirdekte tek kuyruk her zaman en iyisidir çünkü hiç boşluk bırakmaz."** Yakınlığı bozar ve ortak kilit gerektirir; çekirdek sayısı arttıkça ölçeklenmez.

---

## 12. Kendinizi Sınayın

Cevaplar verilmez. Elle çözün, sonra simülatörle kontrol edin.

1. Bölüm 3'teki örnekte B, 2 yerine **1** anında gelseydi çizelge nasıl olurdu? B'nin dönüş ve yanıt süresi kaç olur? `mlfq.py` ile doğrulayın.
2. Aynı örnekte her düzeyin süre hakkı 2 dilim olsaydı (`-a 2`) A hangi anlarda hangi düzeyde çalışırdı?
3. `python mlfq.py -n 3 -q 10 -a 1 -i 1 -l 0,30,0:10,30,1:10,30,1 -S -B 10 -c` komutunu çalıştırın. S'yi 20'den 10'a indirmek A'nın dönüş süresini nasıl değiştirdi? B ve C'ninkini?
4. Bilet oranı 3:2:1 olan üç iş için `lottery.py` ile 2 dışında birkaç tohum deneyin (`-s 1`, `-s 4`). İlk 12 çekilişte oranın 6:4:2'den en çok saptığı tohum hangisi?
5. Adımları A 2, B 3, C 6 olan örneğe 6. dilimde yeni bir iş D (bilet 2, adım 3) eklendiğini düşünün. D'nin geçiş değeri 0 ile başlarsa sonraki birkaç dilimde ne olur? Geçiş değeri ne ile başlamalı?
6. İki çekirdek, çekirdek başına kuyruk ve bölüm 9'daki iş yükü için iş çalmayı açın (`-p -P 1`). İş kaç birimde biter? Çizelgede hangi iş çekirdek değiştirdi?

---

## İleri Okuma

- OSTEP, 8. bölüm — *Scheduling: The Multi-Level Feedback Queue*: <https://pages.cs.wisc.edu/~remzi/OSTEP/cpu-sched-mlfq.pdf>
- OSTEP, 9. bölüm — *Scheduling: Proportional Share* (CFS dahil): <https://pages.cs.wisc.edu/~remzi/OSTEP/cpu-sched-lottery.pdf>
- OSTEP, 10. bölüm — *Multiprocessor Scheduling*: <https://pages.cs.wisc.edu/~remzi/OSTEP/cpu-sched-multi.pdf> — bu bölüm önbellek bilgisine dayanır; 6. haftadan sonra okumanız daha kolay olur.

---

## Gelecek Hafta

İşlemcinin sanallaştırılmasını bitirdik. Gelecek hafta sanallaştırmanın ikinci yarısına, **belleğe** geçiyoruz. Her proses belleği sıfırdan başlayan, kendine ait bir dizi gibi görür — 1. haftadaki apartman benzetmesi. Bu hafta bir işi çekirdekten çekirdeğe taşıdık; gelecek hafta bir prosesin **adresini** fiziksel belleğin başka bir yerine taşıyacağız: **adres uzayı**, **adres çevirme** ve **segmentasyon**.

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
