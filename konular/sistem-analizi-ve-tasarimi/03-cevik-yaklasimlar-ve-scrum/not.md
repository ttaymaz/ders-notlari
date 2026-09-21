# Konu 03: Modern Yaklaşımlar — Çevik (Agile) ve Scrum

Bu bölüm, geleneksel ve plan odaklı yazılım geliştirme modellerinden (Şelale, V-Model gibi), günümüzün dinamik ve belirsiz iş ortamlarına yanıt veren modern, esnek ve artımlı yaklaşımlara geçişi ele alır. Çevik Manifesto'nun felsefesi, empirik süreç kontrolü, endüstride en yaygın kabul gören Scrum çerçevesi, kullanıcı hikayeleriyle gereksinim yönetimi ve Kütüphane Otomasyonu vaka çalışması üzerinden pratik sprint uygulaması detaylandırılmaktadır.

---

## 1. Gelenekselden Çevikliğe Geçiş: Değişim ve Belirsizlik Çağı

Geleneksel yazılım mühendisliği yaklaşımları (Şelale ve türevleri), projenin başlangıcında tüm gereksinimlerin eksiksiz bilinebileceği, pazar koşullarının ve kullanıcı beklentilerinin süreç boyunca sabit kalacağı varsayımına dayanır. Ancak modern bilgi sistemleri dünyasında bu varsayım neredeyse hiçbir zaman geçerli değildir:

- **Pazar Koşullarının Hızlı Değişimi:** Rakiplerin yeni özellikler sunması, mevzuat değişiklikleri veya iş modellerinin evrilmesi, 12 ay önce yazılmış bir analiz dokümanını geçersiz kılabilir.
- **Kullanıcının Ne İstediğini Görünce Anlaması:** Kullanıcılar genellikle çalışan bir ekranı veya prototipi görmeden gerçek ihtiyaçlarını tam olarak tarif edemezler ("I know it when I see it" sendromu).
- **Yüksek Belirsizlik:** Karmaşık sistemlerde neden-sonuç ilişkileri önceden öngörülemez; çözümler ancak deneyerek, keşfederek ve geri bildirim alarak inşa edilebilir (Cynefin çerçevesinde "Complex" alan).

| Boyut | Geleneksel Modeller (Şelale) | Çevik Modeller (Agile / Scrum) |
| :--- | :--- | :--- |
| **Temel Felsefe** | Öngörücü (Predictive): Plana sıkı sıkıya bağlılık | Uyarlamacı (Adaptive): Değişimi kucaklama |
| **Gereksinim Yönetimi** | Proje başında dondurulur, değişim talep formlarıyla zorlaştırılır | Sürekli gelişir, önceliklendirilir, değişime açıktır |
| **Teslimat Biçimi** | Proje sonunda tek bir büyük teslimat (Big Bang) | Düzenli aralıklarla çalışan küçük ürün artışları (Increment) |
| **Müşteri İlişkisi** | Sözleşme ve onay kapıları üzerinden resmi iletişim | Günlük/haftalık iş birliği ve sürekli geribildirim |
| **Risk Profili** | Riskler geliştirme sonundaki entegrasyona kadar gizli kalır | Riskler her döngü sonunda çalışan kodla erkenden açığa çıkar |

![Çevik Döngü vs Şelale Karşılaştırması](assets/01-cevik-dongu-vs-selale.svg)

---

## 2. Çevik Yazılım Geliştirme Manifestosu

2001 yılının Şubat ayında, farklı hafif geliştirme metodolojilerini temsil eden 17 öncü yazılımcı (Kent Beck, Jeff Sutherland, Ken Schwaber, Alistair Cockburn, Martin Fowler ve diğerleri) Utah'taki Snowbird kayak merkezinde bir araya gelerek **Çevik Yazılım Geliştirme Manifestosu**'nu (*Agile Manifesto*) yayımlamıştır.

Manifesto, sağdaki maddelerin değerini yadsımamakla birlikte, soldaki maddelere daha fazla değer verildiğini açıkça ilan eder:

> 1. **Süreçler ve araçlardan ziyade, bireyler ve aralarındaki etkileşimlere**,
> 2. **Kapsamlı dokümantasyondan ziyade, çalışan yazılıma**,
> 3. **Sözleşme pazarlıklarından ziyade, müşteri ile iş birliğine**,
> 4. **Bir plana sıkı sıkıya bağlı kalmaktan ziyade, değişime yanıt vermeye** değer veriyoruz.

### Çevik Prensiplerden Öne Çıkanlar (12 İlke Özeti)

1. En yüksek öncelik, değerli yazılımın erken ve sürekli teslimiyle müşteriyi memnun etmektir.
2. Geliştirmenin son aşamalarında bile değişen gereksinimler memnuniyetle karşılanır.
3. Çalışan yazılım, birkaç haftadan birkaç aya kadar değişen kısa zaman aralıklarında düzenli olarak teslim edilir.
4. İş birimi temsilcileri ile yazılımcılar proje boyunca her gün birlikte çalışmalıdır.
5. Projeler motive olmuş bireyler etrafında kurulmalı; onlara ihtiyaç duydukları ortam ve güven sağlanmalıdır.
6. Bilgi aktarımının en verimli yolu yüz yüze (doğrudan) iletişimdir.
7. İlerlemenin birincil ölçüsü çalışan yazılımdır.
8. Basitlik —yapılması gerekmeyen işleri maksimize etme sanatı— esastır.
9. En iyi mimariler ve tasarımlar, kendi kendini yöneten (self-organizing) takımlardan çıkar.
10. Takım, düzenli aralıklarla daha etkili olma yollarını değerlendirir ve davranışlarını buna göre ayarlar.

---

## 3. Scrum Çerçevesi: Empirizm ve Temel İlkeler

Scrum, karmaşık adaptif problemleri çözmek ve mümkün olan en yüksek değere sahip ürünleri üretken ve yaratıcı bir şekilde teslim etmek için kullanılan bir yönetim çerçevesidir (*framework*). Scrum bir metodoloji değil, bir çerçevedir; çünkü adım adım izlenecek katı talimatlar vermek yerine, takımın içinde hareket edeceği kuralları, rolleri ve etkinlik sınırlarını tanımlar.

### Empirik Süreç Kontrolü (Empiricism)

Scrum, **empirizm** (deneyimcilik) teorisine dayanır. Empirizm, bilginin deneyimden geldiğini ve kararların gözlemlenen gerçeklere dayandığını savunur. Üç ana sacayağı vardır:

1. **Şeffaflık (Transparency):** Sürecin önemli yönleri, sonuçtan sorumlu olan herkes tarafından açıkça görülebilmelidir. Örneğin "Bitti" (Done) tanımı herkes için aynı anlamı taşımalı; kimse bitmemiş bir işi bitmiş gibi göstermemelidir.
2. **Denetim (Inspection):** Scrum eserleri ve hedefe doğru ilerleme, istenmeyen sapmaları yakalamak amacıyla sık sık ve dikkatle denetlenmelidir. Bu denetim işi engellemeyecek sıklıkta olmalıdır.
3. **Uyarlama (Adaptation):** Bir denetim sürecin kabul edilebilir limitlerin dışına çıktığını gösterirse, süreç veya üretilen malzeme derhal ayarlanmalıdır.

![Scrum Çerçevesi](assets/02-scrum-cercevesi.svg)

---

## 4. Scrum Takımı ve Rolleri

Scrum takımında alt-takımlar veya hiyerarşik kademeler yoktur. Takım, tek bir ürün hedefine odaklanmış profesyonellerden oluşur ve genellikle 10 veya daha az kişidir. Takım **çapraz fonksiyoneldir** (bir işi tamamlamak için gereken tüm yeteneklere içeride sahiptir) ve **kendi kendini yönetir** (neyin, ne zaman ve nasıl yapılacağına içeride karar verir).

### 4.1. Ürün Sahibi (Product Owner - PO)
- **Misyon:** Ürünün ve geliştirme takımının ortaya koyduğu işin değerini maksimize etmek.
- **Sorumluluklar:**
  - Ürün İş Listesi'ni (*Product Backlog*) oluşturmak, açıkça ifade etmek ve maddeleri önceliklendirmek.
  - Paydaşların ve son kullanıcıların sesini temsil etmek; iş hedefleri ile teknik dünya arasında köprü olmak.
  - Bir özelliğin kabul edilip edilmeyeceğine (kabul kriterlerine uygunluğuna) karar vermek.
- **Yetki:** PO tek bir kişidir, komite değildir. Backlog'un içeriğini değiştirmek isteyen herkes PO'yu ikna etmek zorundadır.

### 4.2. Scrum Master (SM)
- **Misyon:** Scrum'ın Scrum Kılavuzu'na uygun olarak anlaşılmasını ve uygulanmasını sağlamak.
- **Sorumluluklar:**
  - Takıma ve kuruma hizmet eden liderdir (*Servant Leader*).
  - Takımın ilerlemesini engelleyen pürüzleri ve bürokratik engelleri (*impediments*) ortadan kaldırmak.
  - Etkinliklerin amacına uygun, verimli ve belirlenen zaman kutusu (*time-box*) içinde geçmesini kolaylaştırmak (*facilitation*).
  - Takımı dış baskılardan ve gereksiz bölünmelerden korumak.

### 4.3. Geliştiriciler (Developers)
- **Misyon:** Her Sprint sonunda kullanılabilir ve "Bitti" tanımına uygun bir Artış (*Increment*) üretmek.
- **Sorumluluklar:**
  - Sprint Planlama'da seçilen işleri teknik görevlere ayırmak ve Sprint Backlog'u oluşturmak.
  - Kalite standartlarına (Definition of Done) sadık kalarak analiz, tasarım, kodlama, test ve dokümantasyon yapmak.
  - Günlük Scrum'da hedefe doğru ilerlemeyi koordine etmek.

---

## 5. Scrum Eserleri ve Taahhütleri

Scrum'ın üç resmi eseri (*Artifacts*) vardır. Her eser, şeffaflığı ve odaklanmayı artırmak amacıyla belirli bir **taahhüt** (*Commitment*) ile eşleşir:

| Eser (*Artifact*) | Tanım | Eşleşen Taahhüt (*Commitment*) |
| :--- | :--- | :--- |
| **Ürün İş Listesi (Product Backlog)** | Ürünü geliştirmek için ihtiyaç duyulan tüm gereksinimlerin, iyileştirmelerin ve hata düzeltmelerinin yaşayan, dinamik listesidir. | **Ürün Hedefi (Product Goal):** Sistemin uzun vadeli vizyonunu ve nihai hedefini tanımlar. |
| **Sprint İş Listesi (Sprint Backlog)** | Mevcut Sprint içinde tamamlanmak üzere seçilen Product Backlog maddeleri ve bunların nasıl hayata geçirileceğini gösteren teknik görev planıdır. | **Sprint Hedefi (Sprint Goal):** Sprint sırasında neden değer üretildiğini ve neyin başarılacağını özetleyen tek amaçtır. |
| **Ürün Artışı (Increment)** | Sprint süresince tamamlanan ve önceki tüm sprintlerin kazanımlarıyla birleştirilmiş, çalışır durumdaki somut ürün parçasıdır. | **Bitti Tanımı (Definition of Done - DoD):** Bir işin teslim edilebilir sayılması için karşılaması gereken resmi kalite ölçütleridir. |

---

## 6. Scrum Etkinlikleri (The Events)

Scrum'daki tüm etkinlikler sabit bir zaman kutusuna (*time-box*) sahiptir; yani süresi uzatılamaz.

1. **Sprint (1–4 Hafta):**
   - Scrum'ın kalbidir. Diğer tüm etkinlikleri kapsayan ana döngüdür.
   - Her sprintte potansiyel olarak canlıya alınabilir (*potentially releasable*) bir Artış üretilir.
   - Sprint devam ederken Sprint Hedefini tehlikeye atacak değişiklikler yapılamaz.
2. **Sprint Planlama (Sprint Planning):**
   - Sprint'in ilk gününde yapılır (1 aylık sprint için en fazla 8 saat, 2 haftalık için ~4 saat).
   - Üç soruya yanıt aranır:
     1. Bu Sprint neden değerlidir? (Sprint Hedefi belirlenir)
     2. Bu Sprint'te ne yapılabilir? (Backlog'dan maddeler seçilir)
     3. Seçilen iş nasıl başarılacak? (Geliştiriciler teknik planı çıkarır)
3. **Günlük Scrum (Daily Scrum):**
   - Her iş günü aynı saatte ve aynı yerde yapılan **15 dakikalık** ayakta toplantıdır.
   - Yalnızca geliştiriciler içindir. Odak noktası durum raporu vermek değil; Sprint Hedefine doğru senkronize olmak ve engelleri paylaşmaktır.
4. **Sprint İnceleme (Sprint Review):**
   - Sprint'in son gününde, takım ve tüm paydaşların katılımıyla yapılır.
   - Tamamlanan artış canlı olarak gösterilir (demo yapılır).
   - Pazar koşulları ve geri bildirimler tartışılarak Product Backlog güncellenir.
5. **Sprint Retrospektifi (Sprint Retrospective):**
   - Sprint İnceleme'den hemen sonra, yeni Sprint Planlama'dan önce yapılır.
   - Odak ürün değil, **takımın çalışma biçimi, süreçler, ilişkiler ve araçlardır**.
   - "Neyi iyi yaptık?", "Nerede zorlandık?", "Gelecek sprintte neyi somut olarak iyileştireceğiz?" soruları cevaplanır ve en az bir eylem planı çıkarılır.

---

## 7. Çevik Gereksinim Mühendisliği: Kullanıcı Hikayeleri

Geleneksel analizde gereksinimler sayfalarca süren "Sistem ... yapmalıdır" cümleleriyle yazılırken; Çevik yaklaşımlarda gereksinimler kullanıcının bakış açısından anlatılan kısa ve samimi **Kullanıcı Hikayeleri** (*User Stories*) ile ifade edilir.

### 7.1. Hikaye Şablonu

```text
Bir [Kullanıcı Rolü / Persona] olarak,
[Bir İhtiyaç / Özellik / Eylem] istiyorum,
Böylece [Elde Edilecek Fayda / İş Değeri] sağlayabileyim.
```

- **Rol (Kim?):** Sistemin kimin için tasarlandığını netleştirir (Örn: Öğrenci, Bölüm Başkanı, Kütüphane Görevlisi).
- **İstek (Ne?):** Kullanıcının sistemde gerçekleştirmek istediği davranıştır.
- **Değer (Neden?):** Özelliğin arkasındaki asıl gerekçedir. Değer kısmı olmayan bir hikaye, amaçsız kod yazımına yol açar.

![Kullanıcı Hikayesi Yapısı](assets/03-kullanici-hikayesi-yapisi.svg)

### 7.2. Ron Jeffries'in 3C Modeli

Bir kullanıcı hikayesi sadece bir yazıdan ibaret değildir; üç bileşenden oluşur:

1. **Card (Kart):** Hikayenin adını ve şablonunu içeren kısa özet (fiziksel post-it veya Jira/Trello kartı).
2. **Conversation (Diyalog):** Geliştiriciler ile PO arasındaki sürekli sözlü iletişim. Doküman konuşmanın yerine geçmez, konuşmayı hatırlatır.
3. **Confirmation (Onay / Kabul Kriterleri):** Hikayenin bittiğini kanıtlayan test edilebilir senaryolar. Genellikle *Given-When-Then* kalıbıyla yazılır:
   - *Given (Verili Durum):* Kullanıcı giriş yapmış ve gecikmiş kitabı bulunmamaktadır.
   - *When (Eylem):* Kitap detay sayfasında "Ödünç Al" düğmesine bastığında.
   - *Then (Sonuç):* Kitap kullanıcının üzerine zimmetlenmeli ve 15 günlük iade tarihi üretilmelidir.

### 7.3. Kaliteli Hikaye Ölçütü: INVEST Modeli (Bill Wake)

- **I - Independent (Bağımsız):** Diğer hikayelere sıkı sıkıya bağımlı olmamalı; tek başına geliştirilebilmeli ve test edilebilmelidir.
- **N - Negotiable (Müzakere Edilebilir):** Değişmez bir sözleşme değildir; kapsamı PO ve geliştiriciler arasında konuşulup esnetilebilir.
- **V - Valuable (Değerli):** Müşteri veya son kullanıcı için net bir iş değeri taşımalıdır.
- **E - Estimable (Tahmin Edilebilir):** Takım hikayenin büyüklüğünü ve karmaşıklığını kestirebilmelidir.
- **S - Small (Küçük):** Tek bir sprint içinde rahatlıkla tamamlanabilecek boyutta olmalıdır.
- **T - Testable (Test Edilebilir):** Başarılı olup olmadığını doğrulayacak açık kabul kriterlerine sahip olmalıdır.

---

## 8. Karşılaştırmalı Analiz: Şelale vs. Spiral vs. Scrum

| Kriter | Şelale Modeli (Waterfall) | Spiral Model (Boehm) | Scrum (Agile) |
| :--- | :--- | :--- | :--- |
| **Gereksinim Değişimi** | Çok zor; maliyet katlanarak artar | Döngü başlarında değerlendirilir | Doğal karşılanır; her sprint başında yeniden önceliklendirilir |
| **Temel İtici Güç** | Plan ve onaylanmış dokümantasyon | Risk analizi ve prototipleme | Müşteri değeri ve çalışan yazılım |
| **Döngü Uzunluğu** | Tek döngü (Aylar veya yıllar) | Değişken (Genellikle 3–6 ay) | Sabit zaman kutusu (1–4 hafta) |
| **Kullanıcı Katılımı** | Başta (Analiz) ve sonda (Kabul Testi) | Her spiral döngüsünün değerlendirmesinde | Sürekli (PO her gün takımla; paydaşlar her incelemede) |
| **Dokümantasyon Düzeyi** | Kapsamlı, resmi ve bağlayıcı | Risk odaklı ve mimari ağırlıklı | Yalın; yalnızca gereken kadar (*Just Enough*) |
| **Başarı Ölçütü** | Plana, bütçeye ve takvime uyum | Risklerin başarıyla elenmesi | Üretilen değer ve çalışan yazılım kalitesi |

---

## 9. Vaka Çalışması: Kütüphane Otomasyonu Sprint 1 Simülasyonu

Geleneksel yaklaşımda 40 sayfalık şartnamesi hazırlanan Kütüphane Otomasyonu'nu Scrum yaklaşımıyla hayata geçirelim.

### 9.1. Product Backlog'un Oluşturulması ve Önceliklendirilmesi

Ürün Sahibi (Kütüphane Direktörü), paydaşlarla görüşerek ilk kullanıcı hikayelerini çıkarır ve iş değerine göre sıralar:

1. **[Öncelik 1 - Yüksek]** *Bir kütüphaneci olarak*, sisteme ISBN, başlık ve yazar bilgileriyle yeni bir kitap ekleyebilmek istiyorum; böylece yeni gelen bağışları envantere hemen kaydedebilirim.
2. **[Öncelik 2 - Yüksek]** *Bir öğrenci olarak*, kütüphane arama çubuğuna kitap adı veya yazar yazarak arama yapabilmek istiyorum; böylece aradığım kitabın rafta olup olmadığını rafta aramadan görebileyim.
3. **[Öncelik 3 - Orta]** *Bir öğrenci olarak*, üzerimdeki kitapları ve kalan iade sürelerimi listeleyebilmek istiyorum; böylece ceza ödemeden iade tarihini takip edebileyim.
4. **[Öncelik 4 - Düşük]** *Bir kütüphaneci olarak*, geciken kitaplar için öğrencilere otomatik e-posta uyarısı göndermek istiyorum; böylece tek tek telefonla aramak zorunda kalmayayım.

![Kütüphane Sprint 1 Akışı](assets/04-kutuphane-scrum-akisi.svg)

### 9.2. Sprint 1 Planlama ve Sprint Backlog

Takım, 1 haftalık ilk Sprint için toplanır.
- **Sprint Hedefi:** "Kütüphanecinin sisteme yeni bir kitap kaydedip envanter listesinde görebileceği çalışan ilk sürümü üretmek."
- Bu hedefe ulaşmak için Product Backlog'dan **1. Hikaye** Sprint'e seçilir.
- Geliştiriciler bu hikayeyi alt görevlere (*Tasks*) böler (Sprint Backlog):
  - `GÖREV-1:` Veritabanında `Kitaplar` tablosunun ve indekslerinin oluşturulması (Veri Analisti / Backend).
  - `GÖREV-2:` "Yeni Kitap Ekle" formu kullanıcı arayüzünün tasarlanması (UI Tasarımcısı / Frontend).
  - `GÖREV-3:` Formdan gelen girdilerin doğrulanması (ISBN-13 format denetimi) ve API servisinin yazılması.
  - `GÖREV-4:` Kayıt sonrası kütüphaneciye başarı mesajı gösterilmesi ve tablonun yenilenmesi.
  - `GÖREV-5:` Hatalı ISBN girişleri için negatif senaryo testlerinin koşulması.

### 9.3. Sprint Koşusu ve İnceleme (Review)

- **Günlük Scrum:** Takım üyeleri her sabah 15 dakika toplanır. Arayüz geliştiren analist, ISBN doğrulama servisini beklediğini söyler; backend geliştirici gün içinde servisi açacağını taahhüt ederek engeli kaldırır.
- **Sprint İnceleme (Demo):** Haftanın sonunda Kütüphane Direktörü çağrılır. Canlı ortamda yeni bir kitap eklenir ve listede gösterilir. Direktör sistemi çok beğenir ancak şu geri bildirimi verir: *"Kütüphanemizde çok sayıda çok ciltli ansiklopedi var, forma bir de 'Baskı/Cilt Numarası' alanı ekleyebilir miyiz?"*
- **Sonuç:** Bu yeni istek kavga sebebi olmaz; sözleşme değişikliği gerektirmez. PO tarafından Product Backlog'a yeni bir madde olarak yazılır ve gelecek sprintlerde önceliklendirilir.

---

## 10. Dönem Projesi: Takım Rolleri ve Konu Seçimi Rehberi

Dersimizin dönem projesinde her öğrenci takımı (3–5 kişi), gerçek dünyada karşılığı olan bir iş problemini uçtan uca analiz edip tasarlayacaktır.

### 10.1. Takım İçi Roller ve Bireysel Sorumluluk Dağılımı

Takım içi çalışmalarda "beleşçilik" (*free-riding*) riskini önlemek için her üye aşağıdaki 5 kritik rolden en az birinin liderliğini üstlenir:

1. **İş ve Süreç Analisti (Product Owner / Business Analyst):**
   - Problemin tanımı, paydaş analizi, iş hedefleri ve fizibilite çalışmasını yönetir.
   - Kullanıcı hikayelerini ve kabul kriterlerini yazar.
2. **Süreç Modelleme Uzmanı (Process Modeler):**
   - Sistemin mevcut (As-Is) ve gelecekteki (To-Be) iş akışlarını modeller.
   - UML Kullanım Senaryoları (*Use Case Diagrams*) ve Veri Akış Şemaları'nı (*DFD*) çizer.
3. **Veri Mimarı ve Analisti (Data Architect):**
   - Sistemin veri yapısını kurgular, Varlık-İlişki Şeması'nı (*ERD*) çıkarır.
   - Veri sözlüğünü hazırlar ve üçüncü normal forma (3NF) kadar normalize eder.
4. **Sistem ve Entegrasyon Mimarı (System Architect):**
   - Sistemin mimari katmanlarını (Frontend, Backend, Veritabanı, Dış API servisleri) kurgular.
   - Sınıf diyagramları (*Class Diagrams*) ve Sıralama şemalarını (*Sequence Diagrams*) tasarlar.
5. **Kullanıcı Deneyimi ve Arayüz Tasarımcısı (UI/UX Designer):**
   - Kullanıcı personolarını ve kullanıcı yolculuğu haritalarını hazırlar.
   - Düşük ve yüksek sadakatli ekran taslaklarını (*Wireframe* / Prototip) ve form akışlarını çizer.

### 10.2. İlham Verici Örnek Proje Senaryoları

Takımlar kendi özgün fikirlerini seçebileceği gibi, aşağıdaki problem alanlarından birini de uyarlayabilir:

#### Senaryo A: Spor Salonu Üyelik ve Ders Rezervasyon Sistemi
- **Problem:** Üyelerin seans rezervasyonlarını telefonla yapması, salon kapasitesinin verimsiz kullanılması ve aidat gecikmelerinin takip edilememesi.
- **Kapsam:** Üye self-servis portalı, antrenör takvim yönetimi, QR kod ile turnike geçiş kaydı, otomatik aidat tahsilat bildirimi.

#### Senaryo B: Kampüs İçi İkinci El Ders Kitabı ve Not Takas Platformu
- **Problem:** Dönem başlarında öğrencilerin pahalı ders kitaplarına erişim zorluğu ve dönem sonunda kitapların atıl kalması; güvensiz sosyal medya gruplarında yaşanan dolandırıcılıklar.
- **Kapsam:** Öğrenci e-postası doğrulamalı üyelik, ders koduna göre kitap eşleştirme, emanet usulü güvenli takas noktası modeli.

#### Senaryo C: Sivil Toplum Kuruluşları için Gönüllülük ve Etkinlik Yönetim Sistemi
- **Problem:** STK'ların acil ihtiyaç durumlarında doğru yetkinlikteki gönüllülere hızla ulaşamaması; gönüllülerin katılım saatlerinin ve yetkinliklerinin kayıt altına alınamaması.
- **Kapsam:** Gönüllü yetkinlik profili, etkinlik bazlı görev dağılımı, saha yoklama takip arayüzü, dijital gönüllülük sertifikası üretimi.

---

## 11. Sık Yapılan Hatalar ve Çevik Karşıtı Kalıplar (Scrum Anti-Patterns)

1. **ScrumBut Sendromu:** "Biz Scrum uyguluyoruz ama retrospektif yapmaya vaktimiz olmuyor" veya "Scrum yapıyoruz ama PO'muz yok, kararları müdür veriyor". Kuralların işine gelen kısmını alıp en kritik parçalarını atmak Scrum'ın faydalarını yok eder.
2. **Zombie Scrum:** Ritüellerin (Daily, Review, Retro) şeklen yapıldığı ancak hiçbir gerçek empati, geri bildirim veya çalışan yazılım artışı üretilmediği ruhsuz döngüler.
3. **Daily Scrum'ın Durum Raporuna Dönüşmesi:** Geliştiricilerin birbirleriyle konuşmak yerine Scrum Master'a hesap vermesi. Scrum Master bir patron veya proje yöneticisi değildir.
4. **Gereksiz Büyük Hikayeler (Epiklerin Parçalanmaması):** Bir sprint içine sığamayacak büyüklükteki hikayelerin sprint'e alınması ve sprint sonuna yarım kalmış işlerle girilmesi.
5. **Bitti Tanımının (DoD) Esnetilmesi:** Testi yapılmamış, dokümante edilmemiş veya canlıya hazır olmayan işlerin "yetişsin diye" bitti kabul edilmesi; bu durum teknik borç (*technical debt*) yaratır.

---

## 12. Bölüm Sonu Değerlendirme Soruları

#### Soru 1: Çevik Manifesto Değerleri
Aşağıdakilerden hangisi Çevik Manifesto'nun 4 temel değerinden birisi **değildir**?  
A) Süreçler ve araçlardan ziyade bireyler ve aralarındaki etkileşimler  
B) Kapsamlı dokümantasyondan ziyade çalışan yazılım  
C) Bir plana sıkı sıkıya bağlı kalmaktan ziyade değişime yanıt verme  
D) Müşteri iş birliğinden ziyade katı sözleşme şartlarına sadakat  
E) Değişimi bir tehdit değil, rekabet avantajı olarak görme  
*Cevap: D (Manifesto, sözleşme pazarlıklarından ziyade müşteri ile iş birliğini savunur).*

#### Soru 2: Scrum Rolleri
Bir Scrum takımında "Ürünün değerini en üst düzeye çıkarmak" ve "Ürün İş Listesi'ni (Product Backlog) yönetip önceliklendirmek"ten birincil derecede sorumlu olan rol hangisidir?  
A) Scrum Master  
B) Proje Yöneticisi  
C) Ürün Sahibi (Product Owner)  
D) Baş Yazılım Mimarı  
E) İş Analisti Komitesi  
*Cevap: C*

#### Soru 3: Scrum Eserleri ve Taahhütleri
Scrum'da her eser belirli bir taahhüt ile güvence altına alınır. Buna göre, geliştirilen işin tamamlandığını ve ürün artışına (Increment) dahil edilebileceğini belirleyen resmi kalite kriterleri bütününe ne ad verilir?  
A) Sprint Hedefi (Sprint Goal)  
B) Bitti Tanımı (Definition of Done)  
C) Kabul Testi Protokolü  
D) Ürün Hedefi (Product Goal)  
E) Kapsam Bildirimi  
*Cevap: B*

#### Soru 4: Kullanıcı Hikayesi INVEST Ölçütü
INVEST modelinde yer alan **"N"** harfi kullanıcı hikayesinin hangi özelliğini temsil eder?  
A) Non-functional (Fonksiyonel olmayan gereksinimleri içermesi)  
B) Necessary (Proje için zorunlu olması)  
C) Negotiable (Kapsamının geliştiriciler ve PO arasında müzakere edilebilir olması)  
D) Novel (Yenilikçi ve özgün bir fikir barındırması)  
E) Normalized (Veritabanı kurallarına uygun olması)  
*Cevap: C*

#### Soru 5: Vaka Analizi Senaryosu
Bir e-ticaret şirketi 3 haftalık sprintler koşmaktadır. Sprint'in 10. gününde şirket CEO'su geliştirme ekibine doğrudan gelerek *"Pazarlamadan acil bir talep geldi, mevcut işleri bırakıp derhal sevgililer günü banner'ını ve indirim kodunu yazın"* der. Scrum prensiplerine göre bu durumda geliştiricilerin ve Scrum Master'ın sergilemesi gereken en doğru davranış nedir?  
A) CEO şirketin en yetkili kişisi olduğu için işi hemen bırakıp talebi yapmalıdırlar.  
B) Geliştiriciler CEO'yu reddetmeli ve projeden çıkarmalıdır.  
C) Scrum Master devreye girmeli, sprint ortasında takımın odağının doğrudan bölünemeyeceğini açıklamalı ve CEO'yu talebini Product Backlog'a ekleyip önceliklendirmesi için Ürün Sahibi'ne (Product Owner) yönlendirmelidir.  
D) Takım fazla mesai yaparak her iki işi birden bitirmeye çalışmalıdır.  
E) Sprint derhal iptal edilmeli ve tüm ekip dağıtılmalıdır.  
*Cevap: C*

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi, Sinanpaşa MYO*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
