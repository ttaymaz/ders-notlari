# Bölümleme ve Maliyet

Dört haftadır sorgu yazdık ve sonuçlarına baktık. Bir şeyi hiç sormadık:
**bu sorgu çalışırken ne oldu?**

Geçen hafta bir söz verdik. Birleştirmeyi küçük bir tabloyla yaptık ve hiç
beklemedik; sebebinin "küçük tablo her makineye kopyalanır" olduğunu
söyledik. Bu hafta o cümleyi kanıtlayacağız — ve kanıtı motorun kendi
çıktısında bulacağız.

**Bu hafta yeni bir analiz öğrenmiyoruz. Yazdığımız sorguların neye mal
olduğunu öğreniyoruz.**

---

## 1. Veri Tek Parça Değil: Bölümler

Elimizdeki iki CSV birlikte 232 MB. Spark bunu tek bir yığın olarak
görmüyor; **bölümlere** ayırıyor.

Her bölüm bir **görevdir**, her görev bir **çekirdekte** çalışır. Üç milyon
satırı tek tek okumak yerine, bölümler **aynı anda** okunur.

```sql
SELECT spark_partition_id() AS bolum, COUNT(*) AS satir
FROM ham
GROUP BY bolum
ORDER BY bolum
```

Bu sorgu her bölümde kaç satır olduğunu gösterir. İki şeye bakın: kaç bölüm
var, ve bölümler **birbirine yakın büyüklükte mi.**

Ölçtüğümüzde **8 bölüm** çıkıyor. Her birinde yaklaşık 382.000 satır, en
küçüğünde 355.940. Sekiz sayının toplamı 3.030.359 — yani bütün veri.

> Paralelliğin tamamı buradan geliyor. Üç milyon satırlık bir sayma işi 12
> saniyede bitiyorsa, sebep makinenin hızlı olması değil; işin bölünmüş
> olmasıdır.

### Dengesiz bölüm

Bölümlerden biri diğerlerinin on katı büyükse ne olur? Diğer çekirdekler
işini bitirir ve **bekler.** Sorgu, en yavaş bölüm kadar sürer.

Bu, dağıtık işlemenin en sık sorunudur ve adı vardır: **çarpıklık** (skew).
Sorgunuz yavaşsa ve sebebini bulamıyorsanız bölüm boyutlarına bakın.

> **Bizim verimizde çarpıklık yok.** Bölümler neredeyse eşit, çünkü Spark
> CSV'yi satır sayısına göre değil **bayt sayısına göre** bölüyor ve
> satırlarımız hemen hemen aynı uzunlukta. Bu bir şans; her veri kümesi
> böyle gelmez.

---

## 2. İki Tür Dönüşüm: Dar ve Geniş

Dördüncü haftada dönüşüm ile eylemi ayırdık: dönüşümler tembeldir, eylem
gelene kadar hiçbir şey çalışmaz. Şimdi dönüşümleri **kendi aralarında**
ayırıyoruz.

![Dar ve geniş dönüşüm](assets/01-dar-ve-genis.svg)

**Dar dönüşüm** bölümün içinde biter. `WHERE` bir satırı atarken başka
satıra bakmaz; `SELECT` bir sütunu seçerken komşu bölümü ilgilendirmez. Her
bölüm kendi işini yapar, kimse kimseyle konuşmaz.

**Geniş dönüşüm** bölümler arası konuşma gerektirir. `GROUP BY bolge`
yazdığınızda aynı bölgenin satırları **aynı makinede buluşmak zorundadır** —
ama o satırlar başlangıçta farklı bölümlere dağılmıştır.

| | Dar dönüşüm | Geniş dönüşüm |
| --- | ----------- | ------------- |
| Örnek | `WHERE`, `SELECT`, `CASE` | `GROUP BY`, `JOIN`, `ORDER BY`, `DISTINCT` |
| Bölüm dışına bakar mı | Hayır | **Evet** |
| Veri taşınır mı | Hayır | **Evet** |
| Maliyet | Düşük | **Yüksek** |

---

## 3. Karıştırma Neden Pahalı?

Satırları anahtara göre yeniden dağıtma işlemine **karıştırma** (shuffle)
denir. Pahalı olmasının sebebi hesap değil, **taşıma:**

1. Her bölüm, satırlarını hedef bölümlere göre ayırır ve **diske yazar.**
2. Hedef makineler bu parçaları **ağ üzerinden** okur.
3. Ancak bundan sonra gruplama yapılabilir.

Yani araya bir disk yazma ve bir ağ transferi giriyor. Hesabın kendisi —
toplama, ortalama — bunların yanında neredeyse bedavadır.

> **Maliyet hesapta değil, taşımadadır.** Bir sorgunun ne kadar süreceğini
> tahmin etmek istiyorsanız kaç satır işlendiğine değil, **ne kadar veri
> taşındığına** bakın.

### Ama her karıştırma uzun sürmez

Üç milyon satırı `GEOHASH`e göre gruplamak bu ortamda **1,3 saniye** sürüyor.
Neden bu kadar kısa?

Çünkü karıştırmada taşınan şey üç milyon satır **değil.** Gruplama iki
aşamada yapılıyor: önce her bölüm kendi içinde sayar, sonra yalnızca bu
**kısmi sayılar** karıştırılır. Planda ikisini de göreceksiniz —
`partial_count` karıştırmadan önce, `finalmerge_count` sonra.

Karıştırma bölüm sayısını da değiştirir. Okuma 8 bölümle başlıyor; planda
karıştırmanın hedefi `hashpartitioning(GEOHASH, 231)` yazıyor, yani 231
bölüm.

> Karıştırmanın gerçekten pahalı olduğu durum, taşınacak şeyin büyük olduğu
> durumdur: anahtar sayısı çok yüksekse, ya da gruplama değil **iki büyük
> tablonun birleştirilmesi** yapılıyorsa.

---

## 4. Planı Okumak: `EXPLAIN`

Spark bir sorguyu çalıştırmadan önce **plan** yapar. Planı görebilirsiniz:

```sql
EXPLAIN
SELECT GEOHASH, COUNT(*) FROM trafik GROUP BY GEOHASH
```

Çıktı uzun ve çoğu satırı bu hafta ilgilendirmiyor. **Tek bir kelime
arayacaksınız:**

```
Exchange
```

`Exchange` karıştırmanın plandaki adıdır. Varsa veri taşınacak; yoksa
taşınmayacak.

> **Düğüm adları `Photon` ile başlıyor.** Çalıştığımız ortam sorguları
> **Photon** adlı hızlandırılmış motorla yürütüyor, bu yüzden plan satırları
> `PhotonFilter`, `PhotonGroupingAgg`, `PhotonShuffleExchangeSink` gibi
> görünür. Aradığınız kelime bu adların **içinde** geçiyor; `Exchange` diye
> aramak yeterli. Çıktının sonundaki *"The query is fully supported by
> Photon"* satırı da buradan gelir.

Şimdi aynı şeyi dar bir sorguyla deneyin:

```sql
EXPLAIN
SELECT GEOHASH FROM trafik WHERE AVERAGE_SPEED > 100
```

`Exchange` yok. Plan tek bir okuma ve bir süzmeden oluşuyor.

Aynı plandan iki şey daha okunabilir:

```
PushedFilters: [IsNotNull(AVERAGE_SPEED), GreaterThan(AVERAGE_SPEED,100)]
ReadSchema:    struct<GEOHASH:string,AVERAGE_SPEED:int>
```

**`PushedFilters`** süzmenin okuma adımına kadar indiğini söyler: satır
okunurken zaten eleniyor. **`ReadSchema`** ise hangi sütunların okunduğunu
söyler — sekiz sütunlu dosyadan yalnızca ikisi. Sütun budama tahmin değil,
planda yazılı bir gerçek.

Bir satır da uyarı niteliğinde: `Batched: false`. CSV toplu okunamıyor;
Spark satırları Photon'un işleyebilmesi için sütunlara çevirmek zorunda
(`PhotonRowToColumnar`). Bu, gelecek haftanın konusu.

> Plan okumayı öğrenmek, tahmin etmekten kurtulmaktır. "Bu sorgu pahalı
> galiba" demek yerine `EXPLAIN` yazıp bakabilirsiniz.

---

## 5. Geçen Haftanın İddiası: Yayınlama

Geçen hafta şunu söyledik: küçük tabloyla birleştirmek ucuzdur, çünkü küçük
tablo her makineye kopyalanır. Şimdi kanıtlayalım:

```sql
EXPLAIN
SELECT g.gun, o.aciklama
FROM gunluk g LEFT JOIN ozel_gunler o ON g.gun = o.gun
```

Planda şunu göreceksiniz:

```
PhotonBroadcastHashJoin ... LeftOuter, BuildRight
```

`BuildRight` sağdaki tablonun yayınlandığını söyler — yani küçük olanın.
Küçük tablo için `LocalTableScan` yazar: `VALUES` ile yazdığımız tablo
depolamadan hiç okunmuyor, doğrudan bellekte duruyor.

> **Bu planda `Exchange` de var** — ama birleştirmeden değil. `gunluk`
> görünümünün kendisi bir `GROUP BY`; o karıştırma ona ait. Planı okurken
> hangi satırın hangi işe ait olduğunu ayırt etmek gerekiyor.

**Yayınlama** (broadcast) budur: küçük tablo bütün makinelere gönderilir ve
eşleme yerinde yapılır. Büyük tablonun satırları hiç taşınmaz.

İki tür taşımayı karıştırmayın:

| | Karıştırma | Yayınlama |
| --- | ---------- | --------- |
| Ne taşınır | **Büyük** tablonun tamamı | **Küçük** tablonun tamamı |
| Kaç kez | Anahtara göre yeniden dağıtılır | Her makineye bir kopya |
| Maliyet | Yüksek | Düşük |
| Koşul | — | Küçük tablo belleğe sığmalı |

Son satır önemli: yayınlama ancak küçük tablo gerçekten küçükse işe yarar.
İki büyük tabloyu birleştirirken Spark'ın başka seçeneği yoktur — karıştırır.

---

## 6. Üç Ayrı Maliyet

Dönemin başında bir ölçüm yaptık: veriye dokunan ilk komut **42 saniye**
sürdü, aynı komut hemen ardından **12 saniye.** Aradaki 30 saniye veri
taşıma değildi.

Üç maliyet kalemini ayırmak gerekiyor:

**Kaynak tahsisi.** Sunucusuz ortamda ilk komut çalışırken arkada makineler
ayrılıyor, Spark oturumu kuruluyor. Bu bir kerelik bir bedel ve sorgunuzun
nasıl yazıldığıyla ilgisi yok.

**Veri taşıma.** Her karıştırma yeniden ödenir ve bu, sorgunuzun nasıl
yazıldığına bağlıdır.

**Sürücüye toplama.** Sonucu kendi tarafınıza çekmek de bir maliyettir ve en
sinsi olanıdır. Ölçtük: `GROUP BY` sonucunu — 2.460 satır — çekmek 1,3
saniye. Üç milyon satırın tamamını çekmek **48,63 saniye.**

Beşinci haftanın `toPandas()` tuzağı tam olarak bu. Aradaki fark hesaptan
değil, tek bir makineye akıtılan satır sayısından geliyor.

> Bir sorguyu ilk kez çalıştırıp "yavaş" demek yanlış ölçümdür. İki kez
> çalıştırın; ikinci süre sorgunun gerçek maliyetine daha yakındır.
> Ölçtüğümüzde 1,54 ve 1,28 saniye çıktı.

Maliyet neden önemli? Çünkü işlem gücü sınırsız değil. Bulutta ödediğiniz
şey makine değil, **makine zamanı**: aynı işi yarı sürede yapan bir sorgu
yarı fiyatlıdır. Paylaşılan bir ortamda ise sizin boşa harcadığınız zaman
başkasının beklediği zamandır.

---

## 7. Maliyeti Azaltmanın Yolları

**Önce süzün — ama ölçün.** Süzmek, pahalı adıma giren satırı azaltıyorsa
kazandırır. **Bu veride kazandırmadı:** süzmeden 1,25 saniye, önce süzerek
1,42 saniye. Süzmek işi biraz **uzattı.**

Sebebi plandan okunuyor. Pahalı adım dosyayı okumaktı ve süzmek okunan satır
sayısını azaltmıyor — bütün satırlar yine okunup bakılıyor. Gruplama ise
zaten 2.460 gruba düşüyordu; karıştırılacak veri baştan küçüktü. Üstüne bir
de üç milyon satırda koşul değerlendirme maliyeti eklendi.

> **Kural değil, ölçüm.** "Önce süz" iyi bir başlangıç tahminidir ama bir
> yasa değildir. Hangi adımın pahalı olduğunu bilmiyorsanız yaptığınız
> iyileştirme bir tahmindir — ve bu ölçümde tahmin yanlış çıktı.

**Az sütun okuyun.** `ReadSchema` satırı hangi sütunların okunduğunu
gösteriyor; iki sütun istediğinizde planda iki sütun yazıyor. Sekiz sütunlu
bir dosyadan üçünü kullanıyorsanız üçünü isteyin.

**Küçük tabloyu küçük tutun.** Yayınlanabilir bir tablo ucuzdur;
büyüdüğünde plan sessizce karıştırmaya döner.

**Şemayı kendiniz verin.** `inferSchema=True` veriyi **bir kez fazladan**
okur: önce tipleri anlamak için, sonra asıl okuma için. Bu bir tam
tarama ve bedava değil.

**Gereksiz sıralama yapmayın.** `ORDER BY` geniş bir dönüşümdür. Sonucu
göstermek için sıralamak gerekiyorsa küçültmeden sonra sıralayın.

> Beşinci haftanın kuralı burada da geçerli: **önce küçült.** O zaman
> taşımak içindi, şimdi gerekçesi tam olarak görünüyor.

---

## 8. Laboratuvar: Sorgunuzun Maliyet Notu

Dönem sonundaki projenin altıncı parçası. Parçaların nasıl birleşeceği, not defterinin bölüm yapısı ve
teslim biçimi **proje yönergesinde** tanımlıdır; ders sayfasından okuyun.

1. Bölüm sayısını ve bölüm boyutlarını yazdırın. Dengeli mi?
2. Proje sorgularınızdan **birini** seçin. `EXPLAIN` çalıştırın ve
   çıktıda kaç `Exchange` olduğunu sayın.
3. Aynı sorguyu **iki kez** çalıştırıp iki süreyi de not edin.
4. Sorguyu bir kez de **önce süzerek** yazın. Süre değişti mi, ve
   **beklediğiniz yönde mi?** Sayı verin.
5. Projenize bir **maliyet notu** yazın: analiziniz kaç karıştırma
   içeriyor, en pahalı adım hangisi ve neden?

> Dördüncü maddede "beklediğim gibi çıkmadı" geçerli bir cevaptır — hatta
> en öğretici olanıdır. Önemli olan sayıyı yazmak ve sebebini aramak.

> Beşinci madde projenin parçası. İki cümle yeter ama gerekçeli olmalı:
> "`GROUP BY GEOHASH` en pahalı adım, çünkü üç milyon satır anahtara göre
> yeniden dağıtılıyor."

---

## 9. İyi Pratikler ve Sık Yapılan Hatalar

**İyi pratikler**

- Süre ölçerken sorguyu **iki kez** çalıştırın; ilk süre kaynak tahsisini
  de içerir.
- Bir iyileştirmeyi uygulamadan önce **ölçün.** İyi bilinen kurallar bile
  her veride kazandırmaz.
- Süre ölçerken sonucu sürücüye çekmeyin; ölçtüğünüz şey taşıma olur.
- Bir sorgunun pahalı olup olmadığını tahmin etmek yerine `EXPLAIN` yazın.
- `GROUP BY`dan önce süzün, `ORDER BY`ı en sona bırakın.
- İhtiyacınız olan sütunları adıyla isteyin.
- Yavaş bir sorguda önce bölüm dengesine bakın.

**Sık yapılan hatalar**

- **İlk çalıştırmanın süresini sorgunun maliyeti sanmak.** Aradaki fark
  kaynak tahsisi olabilir.
- **`SELECT *` ile çalışmak.** Kullanmadığınız sütunlar da taşınır.
- **Her şeyi sıralamak.** `ORDER BY` bedava değil ve çoğu zaman gereksiz.
- **Karıştırma ile yayınlamayı karıştırmak.** Biri büyük tabloyu taşır,
  diğeri küçük tabloyu kopyalar.
- **Bölüm dengesini hiç kontrol etmemek.** Sorgu en yavaş bölüm kadar sürer.
- **`toPandas()` ile maliyeti çözdüğünü sanmak.** O, veriyi tek makineye
  toplamaktır; beşinci haftada konuştuk ve bu hafta ölçtük: 48,63 saniye.
- **Ölçmeden iyileştirmek.** "Önce süz" kuralı bu veride işi uzattı.

---

## 10. İsteğe Bağlı Ev Uygulaması

1. `EXPLAIN` çıktısını `GROUP BY`, `ORDER BY`, `DISTINCT` ve `JOIN` için
   ayrı ayrı alın. Hangisinde kaç `Exchange` var?
2. `SELECT *` ile üç sütunlu bir `SELECT` arasındaki süre farkını ölçün.
3. `inferSchema=True` olmadan okuyup süreyi karşılaştırın. (Sütunlar metin
   olarak gelecek; sayısal işlem yapacaksanız dönüştürmeniz gerekir.)
4. Bir `GROUP BY` sorgusunu, önce `WHERE` ile süzdüğünüz hâliyle
   karşılaştırın. Fark beklediğiniz kadar mı?

**Düşündürücü soru:** Bölüm sayısını ikiye katlasanız sorgu iki kat hızlanır
mı? Hangi noktada hızlanmayı bırakır ve neden?

---

## Gelecek Hafta

Planda bir satır vardı ve üstünde durmadık: `Batched: false`.

CSV toplu okunamıyor. Spark her satırı okuyup Photon'un işleyebilmesi için
sütunlara çevirmek zorunda — planda `PhotonRowToColumnar` olarak görünen
adım bu. Dosyada hiçbir istatistik de yok: hangi değerin hangi aralıkta
olduğunu önceden bilmenin yolu olmadığı için her sorgu **baştan** tarıyor.

Sebep basit: CSV satır satır yazılmış düz bir metin dosyası.

Gelecek hafta veriyi **tablo olarak** saklayacağız. Aynı veri, farklı
biçim — ve sorgu süreleri bu yüzden değişecek.

---

## Kaynaklar

- Apache Software Foundation. *Spark SQL Performance Tuning.*
  https://spark.apache.org/docs/latest/sql-performance-tuning.html
- Apache Software Foundation. *EXPLAIN.*
  https://spark.apache.org/docs/latest/sql-ref-syntax-qry-explain.html
- Databricks. *Adaptive query execution.*
  https://docs.databricks.com/aws/en/optimizations/aqe

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
