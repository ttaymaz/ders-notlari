# BVAP203 Dönem Projesi

**Büyük Veri Analistliği Programı** · Bilişim Teknolojileri Meslek Yüksekokulu
2026–2027 Güz · Öğr. Gör. Turgay Taymaz

Dersin **final notunun tamamı** (%60) bu projeden gelir. Ayrıca ara ödev
yoktur.

---

## Özet

| | |
| --- | --- |
| **Çalışma biçimi** | Bireysel |
| **Teslim** | **27 Aralık 2026, Pazar, saat 23.59** |
| **Teslim yeri** | UES ders sayfası — tek dosya, tek seferde |
| **Dosya biçimi** | Not defterinizin **HTML** çıktısı |
| **Değerlendirme** | 100 puan üzerinden; aşağıdaki tabloya göre |
| **Geri bildirim** | **29 Aralık 2026** Salı, son derste (bilgilendirme) |
| **Resmî not ilanı** | Final döneminde, OBS üzerinden |

> **Proje dönem içinde birikti.** Her haftanın laboratuvarı bir parçasını
> üretti. Bu yönerge yeni bir iş tanımlamıyor; elinizde olanı tek bir
> anlatıya dönüştürmenizi istiyor.

---

## 1. Proje Nedir?

Elinizde iki aylık saatlik trafik yoğunluğu verisi var. Projeniz, **bu
veriye sorduğunuz soruların ve bulduğunuz cevapların hikâyesidir.**

Bir not defteri teslim ediyorsunuz ve o not defteri hem **çalışıyor** hem
**anlatıyor.** Kod hücreleri analizi yapar, markdown hücreleri ne yaptığınızı
ve ne bulduğunuzu anlatır.

**Yeni bir analiz yapmanız gerekmiyor.** Dönem boyunca ürettiğiniz
parçaları birleştirmeniz yeterli — ama birleştirmek, dosyaları üst üste
yapıştırmak değildir.

---

## 2. Teslim Biçimi

Not defterinizi Databricks içinde hazırlayın, sonra HTML olarak dışa aktarın:

> **File** → **Export** → **HTML**

Çıkan dosya tek parçadır: kod, çıktılar ve grafikler içinde gömülü gelir.
Herhangi bir tarayıcıda açılır, Databricks hesabı gerekmez.

**Dosya adı:**

```
BVAP203-Proje-AdSoyad.html
```

Türkçe karakter ve boşluk kullanmayın: `BVAP203-Proje-AyseYilmaz.html`.

> **Dışa aktarmadan önce not defterini baştan sona bir kez çalıştırın.**
> HTML çıktısı hücrelerin **son çalıştırıldığı hâlini** gömer. Hata veren ya
> da hiç çalıştırılmamış bir hücre, teslim ettiğiniz dosyada öyle görünür.

---

## 3. Not Defterinin Yapısı

Sırası aşağıdaki gibi olsun. Her başlık bir markdown hücresi olarak açılır.

**0. Başlık ve özet.** Adınız, projenizin bir cümlelik konusu ve en çarpıcı
bulgunuz. Üç cümleyi geçmesin — okuyan kişi ilk ekranda ne bulduğunuzu
anlasın.

**1. Ortam ve veri.** Hangi platformda çalıştınız, veri nereden geldi, hangi
yolda duruyor, kaç satır ve kaç sütun. (2. ve 3. hafta.)

**2. Veriye ilk sorular.** En az iki soru: önce Türkçe, sonra sorgu, sonra
cevap. (4. hafta.)

**3. Sonucu şekille okumak.** En az bir grafik ve altında yorumu. Grafiğin
neyi gösterdiğini **grafik olmadan** anlatabilmelisiniz. (5. hafta.)

**4. Veriye güvenebilir miyiz?** Eksik değerler, aykırı ölçümler ve kapsama.
Verinizin sınırını gerekçesiyle yazın. (6. hafta.)

**5. Zaman ekseni.** Zamanla ilgili bir soru, gerekçeli bir çözünürlük
seçimi, seri grafiği ve sayıyla desteklenmiş bir eğilim ifadesi. (9. hafta.)

**6. Dışarıdan bilgi eklemek.** Kendi yazdığınız referans tablo, birleştirme,
satır sayısı doğrulaması ve **kaynak bildirimi.** (10. hafta.)

**7. Maliyet notu.** Analizinizde kaç karıştırma var, en pahalı adım hangisi
ve neden. Ölçtüğünüz süreleri yazın. (11. hafta.)

**8. Veri hazırlama.** Ham CSV'den tabloya nasıl geçtiniz, hangi sütunları
aldınız, tarihi nerede çevirdiniz. (12. hafta.)

**9. Erişim ve veri notu.** Veriniz nerede duruyor, kişisel veri mi
(**gerekçesiyle**), bir kurumda kime hangi yetkiyi verirdiniz. (13. hafta.)

**10. Sonuç.** Üç başlık: ne buldunuz, neyi bulamadınız, bir hafta daha
olsaydı neye bakardınız.

> Onuncu bölümün ikinci başlığı en değerlisidir. **Bulamadığınızı yazmak
> zayıflık değil, dürüstlüktür** — ve bu derste puan getirir.

---

## 4. Değerlendirme

| Bölüm | Puan |
| ----- | ---: |
| 1 · Ortam ve veri | 5 |
| 2 · Veriye ilk sorular | 10 |
| 3 · Sonucu şekille okumak | 10 |
| 4 · Veriye güvenebilir miyiz | 12 |
| 5 · Zaman ekseni | 12 |
| 6 · Dışarıdan bilgi eklemek | 12 |
| 7 · Maliyet notu | 7 |
| 8 · Veri hazırlama | 7 |
| 9 · Erişim ve veri notu | 10 |
| 10 · Bütünlük, anlatım ve sonuç | 15 |
| **Toplam** | **100** |

### Her bölümde puanın yarısı gerekçeye gider

Bu dönemin tek değerlendirme kuralı budur:

> Bir bölümün puanının **yarısı işin yapılmasına**, **yarısı neden öyle
> yapıldığının yazılmasına** verilir.

Örnek: 9. haftada çözünürlük seçtiniz. Günlük seriyi çizmek işin
yapılmasıdır. *"Günlük seçtim çünkü sorum hafta ritmiyle ilgiliydi ve saatlik
seride üç milyon satır çizilemiyordu"* cümlesi gerekçedir. Grafik olmadan
puanın yarısını, gerekçe olmadan diğer yarısını kaybedersiniz.

Aynı kural her yerde geçerli: sayı vermeden "arttı" demek, kaynak yazmadan
referans tablo kullanmak, ölçmeden "hızlandı" demek — hepsi puanın yarısıdır.

### Puan getirmeyen şeyler

- **Uzun kod.** Kısa ve çalışan kod, uzun ve gösterişli koddan iyidir.
- **Ders notunun tekrarı.** Ne öğrendiğinizi değil, **ne yaptığınızı** yazın.
- **Ekran görüntüsü.** HTML çıktısı zaten gerçek çıktıyı içeriyor.
- **Yorumsuz grafik.** Altında bir cümlesi olmayan grafik eksik sayılır.
- **Çalışmayan hücre.** Hata veren hücrenin ait olduğu bölüm puan kaybeder.

---

## 5. Özgünlük

Veri kümesi hepinizde aynı. Bu yüzden **sorguların benzemesi beklenir** ve
sorun değil: `GROUP BY GEOHASH` yazan on kişi olabilir.

Kişiye özgü olması gereken üç şey var:

1. **Sorduğunuz sorular.** Veriye ne sorduğunuz sizin seçiminiz.
2. **Referans tablonuz.** 10. haftada kendi elinizle yazdığınız tablo.
3. **Yorumlarınız.** Grafikten ne okuduğunuz, hangi sınırı gördüğünüz.

> Sorgunun benzemesi normaldir; **yorumun** benzemesi değildir. Bir başkasının
> yorum cümlelerini kullanmak, kodu kopyalamaktan daha ağır bir durumdur ve
> öyle değerlendirilir.

Yapay zekâ araçları kullanabilirsiniz. Ama teslim ettiğiniz her cümleyi
savunabilmeniz gerekir: *"burada neden günlük çözünürlük seçmişsiniz?"*
sorusuna cevap veremiyorsanız o cümle sizin değildir.

---

## 6. Teslim ve Son Tarih

**27 Aralık 2026, Pazar, 23.59.** UES ders sayfasındaki proje ödevine tek
dosya yüklenir.

- **Tek seferde teslim.** UES'e son yüklediğiniz dosya değerlendirilir.
- **Geç teslim:** her başlayan 24 saat için **10 puan** düşer. 48 saati aşan
  teslim kabul edilmez.
- Teslim edemeyecek bir durumunuz varsa **son tarihten önce** haber verin.

### Notunuzu ne zaman öğrenirsiniz?

İki ayrı şey var ve karıştırmayın:

**29 Aralık 2026, son ders.** Değerlendirmeniz sınıfta size iletilir ve
projelerin ortak güçlü ile zayıf yanları birlikte konuşulur. Bu bir
**bilgilendirmedir**, resmî ilan değil.

**Final dönemi, OBS.** Final notunun resmî ilanı akademik takvimdeki final
tarihinde `obs.aku.edu.tr` üzerinden yapılır. Not itirazı süreci bu ilana
bağlıdır.

> **Teslim tarihi neden final döneminden (04–15 Ocak 2027) önce?** Dönemin
> son dersinde geri bildirim verebilmek için projenin o dersten önce
> elimizde olması gerekiyor. Resmî ilanın takvimi bundan etkilenmez.

---

## 7. Sık Sorulan Sorular

---

**Not defterim çok uzun olursa sorun olur mu?**
Hayır, ama gereksiz uzunluk puan getirmez. On bölüm, her bölümde birkaç
hücre yeter.

**Dönem içinde bir laboratuvarı kaçırdım, o parça eksik.**
O bölümü şimdi yapabilirsiniz; parçalar ayrı ayrı değil, teslim edilen tek
dosya üzerinden değerlendirilir.

**Grafiklerim Databricks'in kendi görselleştirmesiyle çizildi, olur mu?**
Olur. Visualization Editor ile çizilen grafikler de HTML çıktısına gömülür.

**Verimi değiştirebilir, başka bir veri kümesi kullanabilir miyim?**
Hayır. Dersin verisi ortaktır; karşılaştırılabilirlik buna bağlı.

**Türkçe mi yazayım?**
Türkçe. Kod içindeki değişken ve sütun adları İngilizce kalabilir.

**İki kişi aynı soruyu sormuş olabilir, sorun olur mu?**
Olmaz. Soru benzerliği değerlendirilmez; **yorum** benzerliği değerlendirilir.
