# 4. Hafta: Proje Başlatma, Paydaş Analizi ve Fizibilite

## Giriş: "Yapmayalım" Diyebilmek

Bir yönetici heyecanla gelir: *"Kütüphaneye bir mobil uygulama yapalım!"* Genç bir analist hemen ekran tasarlamaya başlar. Deneyimli bir analist ise önce üç soru sorar: Bu fikrin arkasındaki problem ne? Kimler etkilenecek? Yapılmaya değer mi?

Bu hafta bir bilgi sistemi projesinin başlangıç evresini ele alıyoruz: projeyi neyin tetiklediğini, kimlerin etkilendiğini (**paydaş analizi**) ve projenin yapılmaya değer olup olmadığını (**fizibilite analizi**). Bu haftanın konuları dönem projesinin ilk resmi teslimi olan **fizibilite raporunun** bölümlerine birebir karşılık gelir.

---

## 1. Projeler Neden Başlar?

Hiçbir bilgi sistemi projesi kendiliğinden başlamaz. Arkasında her zaman üç tetikleyiciden en az biri vardır:

1. **Problem:** Mevcut süreç yavaştır, hata üretir veya şikâyet toplar.
   - *Kütüphane:* Yıllık nüsha kaybı %15'in üzerinde; gecikenleri bulmak için her cuma yüzlerce fiş elden geçiyor.
2. **Fırsat:** Yeni bir teknoloji, yeni bir hizmet kanalı veya maliyeti düşürme imkânı.
   - *Kütüphane:* Üyelerin telefondan nüshanın rafta olup olmadığını görebilmesi.
3. **Zorunluluk:** Mevzuat değişikliği veya zorunlu entegrasyon (e-Fatura, e-İrsaliye, KVKK uyumu).
   - *Kütüphane:* Defterde tutulan üye bilgilerinin KVKK'ya uygun korunması.

Dikkat: *"Mobil uygulama istiyoruz"* bir tetikleyici değil, bir **çözüm önerisidir**. Analistin görevi önerinin arkasındaki problemi bulmaktır; belki problem mobil uygulamayla değil, bir iade hatırlatma e-postasıyla çözülür.

### PIECES: Problemi Sınıflandırmak

James Wetherbe'nin **PIECES** çerçevesi, mevcut sistemdeki sorunları atlamadan sınıflamak için bir kontrol listesidir:

| Harf | Başlık | Soru | Kütüphanedeki karşılığı |
| :--- | :--- | :--- | :--- |
| **P** | Performance (performans) | İşlem hızı ve yanıt süresi yeterli mi? | Gişede uzun kuyruk |
| **I** | Information (enformasyon) | Bilgi doğru, zamanında, eksiksiz mi? | Nüshanın rafta mı ödünçte mi olduğu bilinmiyor |
| **E** | Economics (ekonomi) | Süreç gereğinden pahalı mı? | Kayıp nüshaların yenilenme maliyeti |
| **C** | Control (denetim) | Veri ve süreç hataya ve kötüye kullanıma karşı korunuyor mu? | Kimin hangi nüshayı aldığı güvenilir biçimde izlenemiyor |
| **E** | Efficiency (verimlilik) | İnsan ve kaynak boşa harcanıyor mu? | Cuma günleri fişlerin elle taranması |
| **S** | Service (hizmet) | Kullanıcıya sunulan hizmet yeterli mi? | Üye, kitabın durumunu öğrenmek için gişeye gelmek zorunda |

Bir sorun birden fazla başlığa düşebilir. Amaç tek doğru kutuyu bulmak değil, hiçbir sorunu atlamamaktır.

---

## 2. Paydaş Analizi

Bir sistemden doğrudan veya dolaylı olarak etkilenen ya da sistemi etkileyebilen kişi, grup veya kuruma **paydaş** (*stakeholder*) denir. Paydaşların beklentileri çoğu zaman çatışır: yönetim maliyetin düşmesini, kullanıcı işinin kolaylaşmasını, bilgi işlem güvenliği, hukuk birimi mevzuata uyumu ister. Hepsini aynı anda tam memnun etmek mümkün değildir; ama hepsini tanımak ve beklentilerini bilinçli yönetmek analistin işidir.

Kütüphane otomasyonunun paydaşları:

- **Kütüphane Daire Başkanı:** Proje sponsoru ve Ürün Sahibi; bütçeyi savunur, kuralları belirler.
- **Rektörlük:** Bütçeyi onaylar; projeyi durdurabilir.
- **Kütüphaneciler:** Sistemi her gün kullanacak olanlar.
- **Üyeler (öğrenciler ve akademisyenler):** Hizmeti alanlar.
- **Bilgi İşlem Daire Başkanlığı:** Sunucu, ağ ve Öğrenci İşleri Sistemi ile entegrasyon.
- **Hukuk Müşavirliği:** KVKK uyumu.
- **Kitap tedarikçileri:** Dolaylı etkilenen dış taraf.

### Mendelow Güç/İlgi Matrisi

Mendelow'un matrisi paydaşları iki eksende konumlar: **güç** (bütçe verme, onaylama, durdurma yetkisi) ve **ilgi** (projeye duyulan ilgi ve günlük temas). Ortaya dört çeyrek ve her biri için bir yönetim stratejisi çıkar:

![Paydaş güç/ilgi matrisi](assets/02-paydas-guc-ilgi-matrisi.svg)

1. **Yüksek güç, yüksek ilgi — Yakından yönet (key players):**
   - *Kütüphane:* Daire Başkanı, Rektörlük.
   - Kararlara dahil edilir, sık görüşülür, beklentileri yakından izlenir.
2. **Yüksek güç, düşük ilgi — Memnun tut (keep satisfied):**
   - *Kütüphane:* Bilgi İşlem Daire Başkanlığı, Hukuk Müşavirliği.
   - Ayrıntıya boğulmaz ama kritik kararlar öncesinde bilgilendirilir ve görüşü alınır; aksi hâlde tek bir yazıyla projeyi durdurabilir.
3. **Düşük güç, yüksek ilgi — Bilgilendir (keep informed):**
   - *Kütüphane:* Kütüphaneciler, öğrenci ve akademisyen üyeler.
   - Düzenli bilgilendirilir, görüşleri anket ve görüşmelerle alınır. Güçleri düşük görünse de sistemi kullanmayarak projeyi fiilen başarısız kılabilirler.
4. **Düşük güç, düşük ilgi — İzle (minimal effort):**
   - *Kütüphane:* Kitap tedarikçileri, dış ziyaretçiler.
   - Genel duyurularla yetinilir; zamanla çeyrek değiştirip değiştirmedikleri izlenir.

Paydaşlar zamanla çeyrek değiştirebilir. Yeni bir mevzuat çıktığında hukuk birimi "memnun tut" çeyreğinden "yakından yönet" çeyreğine geçebilir.

---

## 3. Fizibilite Analizi Nedir?

Fizibilite analizi, bir sistem fikrinin **yapılmaya değer olup olmadığını** teknik, ekonomik, operasyonel ve yasal açılardan araştıran nesnel bir ön incelemedir. Planlama aşamasının sonunda, ayrıntılı analize geçmeden önceki karar kapısıdır.

Amacı projeyi onaylatmak değil, **doğru kararı** verdirmektir. Yürümeyecek bir fikri daha kod yazılmadan durdurmak, fizibilitenin kuruma kazandırabileceği en büyük tasarruftur.

![Fizibilite boyutları ve karar kapısı](assets/01-fizibilite-boyutlari.svg)

Bu derste fizibiliteyi dört boyutta ele alıyoruz. Kaynaklarda **takvim** (*schedule*) fizibilitesi sıklıkla beşinci bir boyut olarak da sayılır (İngilizce kısaltmasıyla TELOS: *technical, economic, legal, operational, schedule*). Biz takvimi teknik boyutun içinde, "bu ekip bu işi istenen tarihe yetiştirebilir mi?" sorusuyla değerlendiriyoruz.

---

## 4. Fizibilitenin Dört Boyutu

### 4.1. Teknik Fizibilite

> *"Bu sistemi inşa edebilir miyiz?"*

- **Altyapı:** Gerekli sunucu, ağ, veritabanı ve donanım mevcut mu, yeterli mi?
- **Uzmanlık:** Ekip seçilen teknolojiyi biliyor mu? Eğitim veya dış danışmanlık gerekir mi?
- **Entegrasyon:** Kurumun mevcut sistemleriyle veri alışverişi kurulabilir mi? Kütüphane için kritik soru: Öğrenci İşleri Sistemi üyelik bilgisini verebiliyor mu?
- **Olgunluk:** Seçilen teknoloji kararlı ve destekleniyor mu?
- **Takvim:** Bu ekip bu işi istenen tarihe yetiştirebilir mi? Yeni dönem başlangıcı gibi kaçırılamaz bir tarih var mı?

### 4.2. Ekonomik Fizibilite

> *"Bu yatırım değer mi?"*

Ekonomik fizibilite bir **maliyet-fayda analizi** (*cost-benefit analysis*) ile yapılır.

#### Maliyetler

| Tür | Somut maliyet (parayla ölçülür) | Soyut maliyet (ölçülemez ama yazılır) |
| :--- | :--- | :--- |
| **Geliştirme (bir kez)** | Donanım, lisans, analist ve geliştirici emeği, danışmanlık | Geçiş döneminde işin yavaşlaması, personelin işinden ayrılan zaman |
| **İşletim (her yıl)** | Sunucu veya bulut gideri, bakım ve destek sözleşmesi, yedekleme | Alışma sürecindeki verim kaybı, çalışan stresi |

En sık unutulan kalem **yıllık işletim maliyetidir**. Birçok kurum yazılımı yaptırırken bakım ve sunucu giderini hesaba katmaz ve üçüncü yılda bütçe açığıyla karşılaşır.

#### Faydalar

- **Somut fayda:** Tasarruf edilen emek, azalan kayıp, artan tahsilat gibi parayla ifade edilebilen kazanımlar.
- **Soyut fayda:** Memnuniyet, itibar, daha hızlı karar gibi ölçülmesi zor kazanımlar. Rapora yazılır ama **tek başına yatırım gerekçesi olamaz.**

#### Finansal Ölçüler

1. **Geri ödeme süresi** (*payback period*): Yatırımın birikmiş net faydayla kendini çıkardığı süre. Faydalar her yıl eşitse:
   > Geri ödeme süresi = Başlangıç yatırımı ÷ Yıllık net fayda
   
   Anlaşılması kolaydır, bu yüzden yöneticilerin en çok baktığı ölçüdür. Ama paranın zaman değerini ve geri ödemeden sonraki yılları dikkate almaz. Kabul edilebilir süre için evrensel bir eşik yoktur; her kurum kendi eşiğini belirler.
2. **Yatırım getirisi** (*return on investment*, ROI):
   > ROI = (Toplam fayda − Toplam maliyet) ÷ Toplam maliyet × 100
3. **Net bugünkü değer** (NBD, *net present value*, NPV): Gelecekteki nakit akışlarını bir iskonto oranıyla bugüne indirger. Bugün elinizdeki 100 lira ile iki yıl sonra alacağınız 100 lira aynı değerde değildir. NBD sıfırdan büyükse yatırım, seçilen iskonto oranında kabul edilebilir.

### 4.3. Operasyonel Fizibilite

> *"Yaparsak kullanılır mı?"*

- **Kullanıcı direnci:** "Bu sistem gelirse işimi kaybeder miyim?" veya "Bilgisayar çökerse kayıtlar gider" kaygısı.
- **Alışkanlık:** Yıllardır süren elle çalışmayı bırakma isteksizliği.
- **Yönetim desteği:** Üst yönetim değişimi sahipleniyor mu?
- **Uyum:** Arayüz, kullanıcının bilgisayar yetkinliğine uygun mu?

Direnç kaygılarının çoğunun haklı bir yeri vardır ve cevabı ikna konuşması değil, somut önlemdir: yedekleme planı, eğitim, sade arayüz, geçiş döneminde yerinde destek. 2. haftadaki "masanın altındaki defter" sahnesi bu boyutun ihmal edilmesinin sonucudur.

### 4.4. Yasal Fizibilite

> *"Mevzuata uygun mu?"*

- **KVKK uyumu:** Hangi kişisel veri, hangi amaçla, hangi hukuki sebeple ve ne kadar süreyle işlenecek?
- **Aydınlatma yükümlülüğü:** Veri sahibine, verisinin neden ve nasıl işlendiği **her durumda** bildirilir.
- **Açık rıza:** Kişisel Verilerin Korunması Kanunu'nda açık rıza, işleme şartlarından yalnızca biridir. Sözleşmenin kurulması veya ifası, kanuni yükümlülük, meşru menfaat gibi başka bir şart varsa ayrıca rıza istenmez. Her işlem için rıza kutusu koymak yaygın ama yanlış bir uygulamadır. Özel nitelikli kişisel veriler (sağlık, biyometrik veri vb.) için kurallar daha sıkıdır.
- **Veri en azlığı:** Amaç için gerekmeyen veri toplanmaz. En iyi korunan veri, hiç toplanmayan veridir.
- **Lisanslar:** Kullanılan yazılım ve kütüphanelerin lisansları kurumun kullanım biçimine uygun mu?

---

## 5. Karar Kapısı: Onay, Revizyon, Ret

Fizibilite raporunun sonunda yönetim üç karardan birini verir:

1. **Onay:** Dört boyut olumludur. Bütçe, ekip ve takvim ayrılır.
2. **Revizyon:** Fikir değerlidir ama bir boyutta risk yüksektir. Kapsam daraltılır, teknoloji veya takvim değiştirilir ve fizibilite yenilenir. Uygulamada en sık çıkan karar budur.
3. **Ret:** Proje yapılamaz ya da getirisi maliyetini karşılamaz. Kaynak korunur.

Kötü bir projeyi başlamadan durdurmak başarısızlık değil, tasarruftur. Kararın rakama ve gerekçeye dayanması, analisti de korur.

---

## 6. Vaka Çalışması: Kütüphane Otomasyonu Fizibilite Raporu

Kütüphane Daire Başkanlığı'nın talebiyle hazırlanan fizibilite raporunun özeti:

### 6.1. Teknik Değerlendirme

- Kütüphanedeki 12 gişe bilgisayarı güncel web tarayıcılarını sorunsuz çalıştırıyor.
- Bilgi İşlem Daire Başkanlığı bir sanal sunucu ve veritabanı alanı ayırdı.
- Öğrenci İşleri Sistemi, öğrenci numarasıyla üyelik durumunu sorgulamaya izin veren bir servis sunuyor.
- Ekip web uygulamalarında deneyimli; dört aylık geliştirme takvimi gerçekçi.
- **Teknik risk: düşük.**

### 6.2. Ekonomik Değerlendirme (üç yıllık projeksiyon)

**Başlangıç yatırımı (Yıl 0, geliştirme dönemi):**

| Kalem | Tutar |
| :--- | ---: |
| Analist ve geliştirici emeği (3 kişi × 4 ay) | 650.000 TL |
| Barkod okuyucular ve etiket yazıcısı (12 gişe) | 100.000 TL |
| Altyapı, güvenlik sertifikası, kurulum | 50.000 TL |
| **Toplam** | **800.000 TL** |

**Yıllık işletim maliyeti (Yıl 1–3):** Sunucu, yedekleme, bakım ve destek: **150.000 TL/yıl**

**Yıllık somut fayda (Yıl 1–3):**

| Kalem | Tutar |
| :--- | ---: |
| Elle envanter sayımı ve cuma taramasından kurtulan emek | 350.000 TL |
| Kayıp nüsha oranının düşmesiyle yenilenmeyen nüshalar | 250.000 TL |
| Gecikme cezalarının düzenli tahsil edilmesi | 300.000 TL |
| **Toplam** | **900.000 TL/yıl** |

**Finansal ölçüler:**

- Yıllık net fayda: 900.000 − 150.000 = **750.000 TL**
- Geri ödeme süresi: 800.000 ÷ 750.000 ≈ **1,07 yıl**, yani canlıya geçişten sonraki **13. ay** civarı. Birinci yılın sonunda birikmiş maliyet 950.000 TL, birikmiş fayda 900.000 TL'dir; iki çizgi ikinci yılın ilk ayında kesişir.
- Üç yıllık toplam: fayda 2.700.000 TL, maliyet 800.000 + 3 × 150.000 = 1.250.000 TL.
- Üç yıllık net katkı: 2.700.000 − 1.250.000 = **1.450.000 TL**
- Üç yıllık yatırım getirisi: 1.450.000 ÷ 1.250.000 ≈ **%116**
- Daire Başkanlığı'nın belirlediği eşik iki yıl olduğu için **ekonomik risk: düşük.**

![Kütüphane Otomasyonu birikmiş maliyet ve fayda](assets/03-maliyet-fayda-egrisi.svg)

### 6.3. Operasyonel Değerlendirme

- Uzun yıllardır çalışan bazı kütüphaneciler kartoteks ve defter sistemini bırakmaya çekiniyor: *"Bilgisayar çökerse kayıtlar gider."*
- **Önlemler:** Günlük otomatik yedekleme; canlıya geçişten önce iki günlük uygulamalı eğitim; ilk ay gişelerde yerinde destek. Kütüphaneciler geliştirme boyunca sprint inceleme toplantılarına katılacak.
- **Operasyonel risk: yönetilebilir.**

### 6.4. Yasal Değerlendirme

- Üyelik için **öğrenci veya personel numarası** yeterli; T.C. kimlik numarası toplanmıyor.
- İşleme sebebi kütüphane hizmetinin sunulmasıdır; üyelere aydınlatma metni gösteriliyor, ayrıca her işlem için açık rıza istenmiyor.
- Ödünç geçmişi, iade ve ceza işlemleri kapandıktan sonra belirlenen süre dolunca anonimleştiriliyor; okuma alışkanlığı süresiz saklanmıyor.
- Veritabanı yedekleri şifreli saklanıyor.
- **Yasal risk: düşük.**

### 6.5. Karar

![Kütüphane fizibilite kapıları](assets/04-kutuphane-fizibilite-karar-agaci.svg)

**Onay.** Dört aylık geliştirme takvimiyle proje başlatıldı. Operasyonel onay eğitim planına bağlıdır; eğitim yapılmazsa onayın şartı yerine gelmemiş sayılır.

---

## 7. Dönem Projenize Yansıması: Fizibilite Raporu

Bu haftanın konuları, dönem projesinin fizibilite raporunun bölümleridir. Rapor sıfırdan yazılmaz; ilk üç haftanın çıktıları birikir:

| Rapor bölümü | Kaynağı |
| :--- | :--- |
| Problem tanımı ve PIECES sınıflaması | 1. hafta sistem tanımı + bu haftanın PIECES tablosu |
| Hedefler, sistem sınırı, kapsam içi ve kapsam dışı | 1. hafta sınır kararı + 3. hafta ürün iş listesi |
| Paydaş listesi ve güç/ilgi matrisi | Bu hafta |
| Dört boyutlu değerlendirme | Bu hafta |
| Varsayımsal maliyet-fayda tablosu ve geri ödeme süresi | Bu hafta |
| Seçilen süreç modeli ve gerekçesi | 2. hafta |
| Yönetim özeti ve öneri: onay, revizyon veya ret | Bu hafta |

Dikkat edilecekler:

- **Kapsam dışı** maddeleri yazın. "Bu sistem ne yapmayacak?" sorusunu cevaplamayan takım, dönem boyunca kapsam kaymasıyla uğraşır.
- Rakamlar varsayımsal olabilir ama **tutarlı** olmalıdır; tablodaki sayı ile özetteki sayı aynı olsun.
- Her çeyreğe en az bir paydaş yerleştirin; boş kalan çeyrek, unutulmuş bir paydaşın işareti olabilir.
- Teslim biçimi, bölüm sorumluları ve değerlendirme ölçütleri için dönem projesi kılavuzuna bakın.

---

## 8. İyi Pratikler ve Sık Yapılan Hatalar

| Hata | Neden Tehlikeli? | Doğru Pratik |
| :--- | :--- | :--- |
| **Batık maliyet yanılgısı** | "Bu kadar harcadık, bırakamayız" düşüncesi, zarar ettireceği belli bir projeyi sürdürür. | Kararı geçmişte harcanana değil, bundan sonra harcanacak ve kazanılacak olana göre verin. |
| **İyimserlik yanlılığı** | Maliyet ve süre düşük, fayda yüksek tahmin edilir. | Tahminlere risk payı ekleyin; benzer projelerin gerçekleşen değerlerine bakın. |
| **Soyut faydayı şişirmek** | "Marka değerimiz artacak" cümlesiyle bütçe istenir. | Soyut faydayı yazın ama kararı somut faydaya dayandırın. |
| **İşletim maliyetini unutmak** | Geri ödeme süresi olduğundan kısa çıkar. | Maliyet tablosunda geliştirme ve işletim satırlarını ayrı tutun. |
| **Her şey için açık rıza istemek** | Rıza gereksiz yere istenir, asıl yükümlülük olan aydınlatma ve veri en azlığı ihmal edilir. | Önce hangi verinin gerçekten gerektiğini, sonra hangi hukuki sebeple işlendiğini belirleyin. |
| **Yasal boyutu sona bırakmak** | Sistem bittikten sonra mevzuata takılır. | Yasal fizibiliteyi ilk gün yapın. |

---

## 9. Kendinizi Deneyin

1. **Tetikleyici ve çözüm:** Bir belediye başkanı "Vatandaşlar için bir yapay zekâ sohbet botu istiyoruz" diyor. Bu cümle bir tetikleyici mi, bir çözüm önerisi mi? Arkasındaki olası problemleri PIECES başlıklarıyla sıralayın.
2. **Paydaş matrisi:** Bir hastane randevu sistemi için en az altı paydaş belirleyip güç/ilgi matrisine yerleştirin. Hangi paydaşın yeri zamanla değişebilir? Neden?
3. **Geri ödeme:** Başlangıç yatırımı 600.000 TL, yıllık fayda 420.000 TL, yıllık işletim maliyeti 120.000 TL olan bir projenin geri ödeme süresini hesaplayın. İşletim maliyetini unutan bir analist hangi sonucu bulurdu? Aradaki fark karar verici için neden önemli?
4. **Yasal boyut:** Bir spor salonu, üyelerin turnikeden parmak iziyle geçmesini istiyor. Yasal fizibilitede hangi sorular sorulmalı? Daha az veriyle aynı amaca ulaşan bir alternatif önerin.
5. **Karar kapısı:** Teknik, ekonomik ve operasyonel açıdan olumlu ama ilgili mevzuatın izin vermediği bir özelliği içeren bir proje için onay, revizyon ve ret kararlarından hangisini önerirsiniz? Önerinizi kapsam değişikliğiyle birlikte gerekçelendirin.

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
