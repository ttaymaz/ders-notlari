# Tablo Olarak Saklamak

Geçen hafta planda bir satır gördük ve üstünde durmadık:

```
Batched: false
```

CSV toplu okunamıyor. Spark her satırı tek tek okuyup, işleyebilmek için
sütunlara çevirmek zorunda. Dosyada hiçbir istatistik de yok: hangi değerin
nerede olduğunu önceden bilmenin yolu olmadığı için **her sorgu baştan
tarıyor.**

Bu hafta veriyi aynı bırakıp **yerleşimini** değiştiriyoruz. Yeni bir analiz
yok; aynı sorgular, farklı süreler.

---

## 1. Dosya ile Tablo Aynı Şey Değil

Şimdiye kadar hep **dosya** okuduk: `spark.read.csv(VERI, ...)`. Yol verdik,
Spark gidip okudu. Her seferinde.

**Tablo** bundan fazlasıdır. Bir adı vardır, şeması yazılıdır, istatistikleri
tutulur ve nerede durduğunu bilmek sizin işiniz değildir.

| | Dosya | Tablo |
| --- | ----- | ----- |
| Nasıl ulaşılır | Yol ile | **Ad** ile |
| Şema | Her okumada çıkarılır | **Yazılı** |
| İstatistik | Yok | Var (en küçük, en büyük, sayı) |
| Yerleşim | Satır satır | **Sütun sütun** |

Tablonun adı üç parçalıdır: **katalog**, **şema**, tablo.

```sql
workspace.default.trafik_tablo
```

Bu ad tanıdık olmalı. Verinizi yüklediğiniz volume'un yolu
`/Volumes/workspace/default/trafik` idi — aynı katalog, aynı şema. Volume ve
tablo komşu; biri dosya tutuyor, diğeri tablo.

---

## 2. Tabloyu Kurmak

Tablo kurmak tek bir sorgudur:

```sql
CREATE OR REPLACE TABLE workspace.default.trafik_tablo AS
SELECT to_timestamp(DATE_TIME, 'yyyy-MM-dd HH:mm:ss') AS zaman,
       GEOHASH,
       AVERAGE_SPEED,
       NUMBER_OF_VEHICLES
FROM ham
```

Dikkat edin: tarihi burada **bir kez** çeviriyoruz. Şimdiye kadar her not
defterinde `to_timestamp` satırını baştan yazıyorduk; artık çevrilmiş hâli
saklanıyor.

Bu sorgu diğerlerinden yavaş olacak, çünkü veriyi okumakla kalmıyor,
**yazıyor** da.

> **Bir kez öde, her sorguda kazan.** Tabloyu kurmak bir kerelik bir
> maliyettir. Ondan sonraki her sorgu bu maliyetin karşılığını alır.

Tablonun ne olduğunu sorabilirsiniz:

```sql
DESCRIBE DETAIL workspace.default.trafik_tablo
```

Çıktıda üç şeye bakın: `format`, `numFiles`, `sizeInBytes`. Biçim **Delta**
olacak — Databricks'in tablo biçimi. Boyut ise sizi şaşırtacak.

---

## 3. Sütunlu Saklama

![Satır mı, sütun mu](assets/01-satir-mi-sutun-mu.svg)

CSV'de bir satırın sekiz sütunu **yan yana** yazılıdır. İki sütun istediğinizde
diskten okunan şey yine bütün satırdır; gerisi okunduktan sonra atılır.

Sütunlu biçimde her sütun **kendi bloğunda** durur. İki sütun istediğinizde
yalnızca iki blok okunur.

Bunun ikinci bir sonucu var ve daha çarpıcı: **sıkışma.** Aynı sütunun
değerleri birbirine benzer — `GEOHASH` sütununda binlerce kez aynı önekler,
`AVERAGE_SPEED` sütununda birbirine yakın sayılar. Benzer değerler yan yana
olduğunda çok daha iyi sıkışır.

> Bu yüzden `sizeInBytes` değeri CSV'nin 232 MB'ından **belirgin biçimde
> küçük** çıkar. Veri aynı; yerleşim farklı.

Üçüncü sonuç geçen haftanın planında eksik olan şey: sütunlu biçim **toplu**
okunabilir. `PhotonRowToColumnar` adımı artık gerekmiyor, çünkü veri zaten
sütunlu.

---

## 4. İstatistik ve Dosya Atlama

Tablo, her dosya için sütunların **en küçük ve en büyük** değerini saklar.
Bu küçük bilgi büyük bir şey mümkün kılıyor.

Şu sorguyu düşünün:

```sql
SELECT COUNT(*) FROM workspace.default.trafik_tablo
WHERE zaman >= '2025-01-15'
```

Tablo, bir dosyadaki en büyük `zaman` değerinin 2024-12-20 olduğunu
biliyorsa **o dosyayı hiç açmaz.** Buna **dosya atlama** denir.

CSV'de bu mümkün değildi: içinde ne olduğunu anlamanın tek yolu dosyayı
okumaktı.

> Geçen haftanın cümlesi hâlâ geçerli ama bir adım derinleşti: maliyet
> taşımadadır — ve **hiç okunmayan veri hiç taşınmaz.**

### Sıralama atlamayı mümkün kılar

Dosya atlama, verinin **sıralı** olmasına bağlıdır. Zaman değerleri bütün
dosyalara karışık dağılmışsa her dosyanın aralığı bütün dönemi kapsar ve
atlanacak dosya kalmaz.

İstatistik vardır, ama işe yaramaz. Veriyi hangi sütuna göre düzenlediğiniz,
hangi sorguların hızlanacağını belirler.

---

## 5. Şema Artık Yazılı

`inferSchema=True` kullandığımız her okumada Spark veriyi **bir kez
fazladan** tarıyordu: önce tipleri anlamak için, sonra asıl okuma için.

Tabloda tipler veriyle birlikte saklanır:

```sql
DESCRIBE TABLE workspace.default.trafik_tablo
```

Fazladan tarama yok. Ve daha önemlisi: tip bir **sözleşme** oldu.
`AVERAGE_SPEED` sütunu sayıysa, yarın birisi oraya metin yazamaz.

Dördüncü haftada `inferSchema`yı bilerek kullanmıştık — ilk haftalarda şema
yazmak konuyu dağıtırdı. O geçici çözümün bedelini geçen hafta ölçtük, bu
hafta kaldırıyoruz.

---

## 6. Tabloyu Sütuna Göre Bölmek

Burada bir terim çakışması var ve baştan ayırmak gerekiyor.

Geçen hafta **bölüm** derken verinin çalışma anında kaç parçaya ayrıldığını
anlatıyorduk — sekiz bölüm, her biri bir görev. Bu, sorgu çalışırken olan
bir şeydi.

Tabloyu sütuna göre bölmek **diskte** olan bir şeydir: örneğin aya göre
bölerseniz her ay kendi klasörüne yazılır ve yalnızca o ayı soran bir sorgu
diğer klasörlere hiç bakmaz.

> **Bizim verimiz için gerekmiyor** ve yapmıyoruz. 232 MB iki aya bölünürse
> ortaya küçük dosyalar çıkar; küçük dosya çok olduğunda her birini açmanın
> maliyeti kazancı yer. Bölmek büyük tablolarda işe yarar, küçüklerde zarar.

Bilmeniz yeterli: bölme bir araçtır, her zaman doğru cevap değil.

---

## 7. Ölçmeden Önce Tahmin Edin

Geçen haftanın dersi burada da geçerli. Tabloyu kurduktan sonra aynı sorguyu
iki biçimde çalıştıracaksınız:

- CSV üzerinden (`ham` görünümü)
- Tablo üzerinden (`trafik_tablo`)

**Çalıştırmadan önce tahmin yazın.** Kaç kat hızlanmasını bekliyorsunuz?
Sonra ölçün ve tahmininizle karşılaştırın.

Geçen hafta "önce süz" kuralının bu veride kazandırmadığını ölçerek
görmüştük. Bu hafta beklenti yönünde çıkabilir de çıkmayabilir de — önemli
olan **sayıyı yazmak.**

> Ölçerken sonucu sürücüye çekmeyin. Küçük sonuç döndüren bir sorgu seçin,
> yoksa ölçtüğünüz şey taşıma olur.

---

## 8. Laboratuvar: Aynı Soru, İki Biçim

Dönem sonundaki projenin yedinci parçası.

1. Tabloyu kurun ve `DESCRIBE DETAIL` ile **boyutunu** not edin. CSV'nin
   232 MB'ı neye düştü?
2. Proje sorgularınızdan birini seçin. Süresini **CSV üzerinden** ölçün.
3. Aynı sorguyu **tablo üzerinden** ölçün. Önce tahmininizi yazın.
4. İki `EXPLAIN` çıktısını karşılaştırın: `PhotonRowToColumnar` adımı tablo
   planında da var mı?
5. Projenizin **veri hazırlama** adımını yazın: ham CSV'den tabloya nasıl
   geçtiniz, hangi sütunları aldınız, tarihi nerede çevirdiniz?

> Beşinci madde projenin parçası ve bundan sonraki bütün analizlerin
> başlangıç noktası. Projenizde artık CSV değil tablo okuyun.

---

## 9. İyi Pratikler ve Sık Yapılan Hatalar

**İyi pratikler**

- Aynı veriyi birden fazla sorguda kullanacaksanız **bir kez** tabloya
  dönüştürün.
- Dönüşümü tablo kurarken yapın: tarihi bir kez çevirin, bir daha
  uğraşmayın.
- Tabloya yalnızca kullandığınız sütunları alın.
- Tablo kurduktan sonra `DESCRIBE DETAIL` ile boyutuna ve dosya sayısına
  bakın.
- Hızlanmayı **ölçün**; tahminle yetinmeyin.

**Sık yapılan hatalar**

- **Her not defterinde CSV'yi baştan okumak.** Tablo tam bunun için var.
- **Tabloyu kurup CSV'yi kullanmaya devam etmek.** Kodun bir yerinde
  `spark.read.csv` kalırsa kazancın yarısını kaybedersiniz.
- **Küçük tabloyu sütuna göre bölmek.** Küçük dosya çoğaltmak maliyeti
  artırır.
- **İstatistiğin tek başına yeteceğini sanmak.** Veri sıralı değilse
  atlanacak dosya kalmaz.
- **Tablo adını yolla karıştırmak.** Tabloya ad ile ulaşılır
  (`katalog.şema.tablo`), yol ile değil.
- **Ölçümde sonucu sürücüye çekmek.** Geçen haftanın hatası; tekrar etmeyin.

---

## 10. İsteğe Bağlı Ev Uygulaması

1. Tabloyu `zaman` sütununa göre sıralı yazıp (`ORDER BY zaman`) tarih
   süzen bir sorguyu yeniden ölçün. Sıralama atlamayı mümkün kıldı mı?
2. Yalnızca iki sütunlu ikinci bir tablo kurun. Boyutu, dört sütunlunun
   kaçta kaçı?
3. `DESCRIBE HISTORY workspace.default.trafik_tablo` çalıştırın. Tablo
   kendi geçmişini tutuyor; kaç sürüm görüyorsunuz?
4. `OPTIMIZE workspace.default.trafik_tablo` çalıştırıp `DESCRIBE DETAIL`e
   yeniden bakın. `numFiles` değişti mi?

**Düşündürücü soru:** Tabloyu kurmak bir kerelik maliyet dedik. Ham veriye
her ay yeni bir dosya ekleniyor olsaydı bu cümle hâlâ doğru olur muydu?

---

## Gelecek Hafta

Üç haftadır veriyi hızlı ve ucuz işlemeyi konuştuk. Bir soruyu hiç
sormadık: **bu veriye kim erişebilir?**

Tabloyu kurduğunuzda bir ad verdiniz ve o ad bir katalogda duruyor.
Kataloglar yetkilendirmenin de yaşadığı yer: kimin hangi tabloyu
görebileceği orada tanımlanır.

> Gelecek hafta bulutun ikinci yüzüne bakıyoruz: dağıtım modelleri,
> güvenlik ve erişim denetimi.

---

## Kaynaklar

- Databricks. *What is Delta Lake?*
  https://docs.databricks.com/aws/en/delta/
- Databricks. *Data skipping for Delta Lake.*
  https://docs.databricks.com/aws/en/delta/data-skipping
- Apache Software Foundation. *Parquet Files.*
  https://spark.apache.org/docs/latest/sql-data-sources-parquet.html

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
