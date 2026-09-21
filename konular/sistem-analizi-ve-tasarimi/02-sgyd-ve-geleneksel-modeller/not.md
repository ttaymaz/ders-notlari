# 2. Hafta: Sistem Geliştirme Yaşam Döngüsü (SGYD) ve Geleneksel Süreç Modelleri

## Giriş: Bir Bina İnşa Ederken ve Bir Yazılım İnşa Ederken

Hiç kimse bir köprü veya gökdelen inşa ederken işe doğrudan şantiyeye harç dökerek başlamaz. Önce zemin etütleri yapılır, jeolojik riskler incelenir; ardından mimarlar binanın yerleşim planını, inşaat mühendisleri statik hesaplarını ve taşıyıcı kolon şemalarını çizer. Şehir yönetiminden ruhsat ve yasal onaylar alınır; en sonunda temel kazılmaya başlanır. Süreç boyunca her adımın bir denetimi, her malzemenin bir kalite belgesi vardır.

Buna karşılık bilişim dünyasında trajikomik bir alışkanlık vardır: Müşteri bir fikirle gelir ve ekip çoğunlukla *"Hemen kodlamaya başlayalım, eksikleri yolda hallederiz"* diyerek bilgisayarın başına geçer. 

Sonuç ne olur? 
- Üç ay sonra projenin aslında neyi çözeceği konusunda müşteriyle ekip birbirine girer,
- Yazılan kodlar yeni bir özellik istendiğinde domino taşları gibi çöker,
- Bütçe tükenir, teslim tarihleri 6 ay sarkar ve ortaya kimsenin kullanmak istemediği yamalı bir yazılım çıkar.

İşte yazılım projelerini bu amatör kaostan kurtarıp mühendislik disiplinine kavuşturan çerçeveye **Sistem Geliştirme Yaşam Döngüsü (SGYD)** —İngilizce adıyla *Systems Development Life Cycle (SDLC)*— diyoruz. Bu hafta, bir yazılımın fikir aşamasından hurdaya ayrılmasına kadar izlediği adımları ve bu adımları yönetmek için geliştirilmiş üç köklü geleneksel modeli inceleyeceğiz: **Şelale (Waterfall)**, **V-Modeli** ve **Spiral Model**.

---

## 1. SGYD (SDLC) Nedir?

> **SGYD Tanımı:** Bir bilgi sisteminin planlanması, gereksinimlerinin toplanması, mimarisinin tasarlanması, kodlanması, test edilmesi, kullanıma alınması ve bakımının yapılması süreçlerini adım adım tanımlayan yapılandırılmış ve disiplinli bir metodolojik çerçevedir.

SGYD'nin temel amacı; **tahmin edilebilir**, **ölçülebilir**, **zamanında** ve **bütçesi içinde** yüksek kaliteli sistemler üretmektir. Bir yaşam döngüsü modeli kullanmak kuruma şu güvenceleri sağlar:

1. **İzlenebilirlik (Traceability):** Canlı sistemdeki her bir kod satırının veya veritabanı tablosunun, müşterinin hangi iş gereksinimine dayandığı geriye dönük olarak kanıtlanabilir.
2. **Rol ve Sorumluluk Netliği:** Analistin nerede duracağı, mimarın neyi teslim edeceği, yazılımcının neyi kodlayacağı ve test uzmanının neyi denetleyeceği kesin çizgilerle belirlenir.
3. **Maliyet ve Takvim Kontrolü:** Proje kontrolsüz bir sis perdesi olmaktan çıkar; yöneticilerin ilerlemeyi görebileceği somut teslim kilometre taşlarına (*milestones*) bölünür.

---

## 2. SGYD'nin 6 Temel Aşaması

Farklı metodolojiler bu aşamaları farklı adlarla veya farklı döngüsel hızlarla uygulasa da, her bilişim projesi mantıksal olarak şu altı adımı yaşamak zorundadır:

1. **Planlama (Planning):**
   - *"Bu projeyi neden yapıyoruz ve yapabilir miyiz?"* sorusunun yanıtıdır.
   - Projenin hedefleri, sınırları, paydaşları belirlenir. Dört boyutlu fizibilite analizi (teknik, ekonomik, operasyonel, yasal) yapılır. Kaynaklar ve kaba zaman takvimi çıkarılır.
2. **Analiz (Analysis):**
   - *"Sistem tam olarak NE yapmalı?"* sorusuna odaklanılır.
   - Kullanıcılarla mülakatlar yapılır, mevcut dokümanlar incelenir, gözlemler yürütülür. Sistemin fonksiyonel ve fonksiyonel olmayan tüm gereksinimleri toplanır; Use Case, DFD gibi kavramsal modellerle belgelenir ve müşteriyle mutabakata varılır (*Gereksinim Şartnamesi* imzalanır).
3. **Tasarım (Design):**
   - Analizde belirlenen "NE" sorusunun **"NASIL"** yapılacağı planlanır.
   - Yazılım mimarisi (örn: 3-Tier veya mikroservis), veritabanı şeması (tablolar, ilişkiler, normalizasyon), ekran Wireframe/Mockup tasarımları, güvenlik protokolleri ve ağ topolojisi projelendirilir.
4. **Geliştirme / Kodlama (Implementation / Coding):**
   - Tasarım dokümanlarının programlama dilleri (C#, Java, Python vb.) ve SQL kullanılarak çalışan yazılım modüllerine dönüştürüldüğü aşamadır.
5. **Test ve Entegrasyon (Testing):**
   - Yazılan modüllerin hem tek başlarına (birim testi) hem de bir araya geldiklerinde (entegrasyon ve sistem testi) doğru çalışıp çalışmadığı denetlenir. Sistemin şartnamedeki gereksinimleri karşıladığı **Kullanıcı Kabul Testleri (UAT)** ile doğrulanır.
6. **Canlıya Geçiş ve Bakım (Deployment & Maintenance):**
   - Sistemin canlı sunuculara kurulması, eski verilerin taşınması (migrasyon), kullanıcıların eğitilmesi ve sistemin teslim edilmesidir.
   - **Bakım Gerçeği:** Bir yazılımın toplam ömür boyu sahip olma maliyetinin (TCO) yaklaşık **%60 ile %70'i** canlıya geçtikten sonraki bakım aşamasında harcanır. Bakım dört şekilde karşımıza çıkar:
     - *Düzeltici Bakım:* Canlıda ortaya çıkan hataların giderilmesi.
     - *Uyarlayıcı Bakım:* Yeni işletim sistemi, tarayıcı veya değişen yasalara uyum sağlanması.
     - *Mükemmelleştirici Bakım:* Kullanıcılardan gelen yeni performans ve arayüz iyileştirme talepleri.
     - *Önleyici Bakım:* Kodun gelecekte arıza vermemesi için refaktör edilmesi.

Aşağıdaki şema, bu aşamalar arasındaki doğrusal geçişi ve aşamalar ilerledikçe hataları düzeltmenin maliyetini özetlemektedir:

![SGYD Aşamaları ve Hata Maliyeti](assets/01-sgyd-asomalari.svg)

---

## 3. Barry Boehm'in Hata Maliyeti Kuralı (Cost of Change)

Yazılım mühendisliğinin öncülerinden Barry Boehm tarafından ortaya konan ve onlarca ampirik araştırmayla doğrulanan kural şudur:

> **Boehm Kuralı:** Bir gereksinim veya tasarım hatasını tespit edip düzeltmenin maliyeti, SGYD aşamaları ilerledikçe **üssel (katlanarak)** artar.

Rakamları somutlaştıralım:

- **Analiz Aşamasında Fark Edilen Hata ($1\times$):** Kütüphanecinin "bir üye en fazla 3 kitap alabilir" dediğini yanlış anlayıp 5 kitap yazmışsınız. Analiz toplantısında kütüphaneci "hayır, 3 kitap olacak" der. Çözüm: Şartnamedeki "5" rakamını silip "3" yazarsınız. **Süre: 10 saniye. Maliyet: 0 TL.**
- **Tasarım Aşamasında Fark Edilen Hata ($5\times - 10\times$):** Yanlış kurala göre veritabanı kısıtlaması (constraint) ve arayüz uyarı kutuları çizilmiştir. Çözüm: Şemayı ve ekran tasarımını güncellemek gerekir. **Süre: Birkaç saat.**
- **Kodlama Aşamasında Fark Edilen Hata ($20\times$):** Geliştirici 5 kitaba göre kontroller, fonksiyonlar ve döngüler yazmıştır. Çözüm: Kodların bulunması, silinmesi, yeniden yazılması ve derlenmesi gerekir. **Süre: Birkaç gün.**
- **Test Aşamasında Fark Edilen Hata ($50\times$):** Test uzmanı senaryoyu çalıştırır ve sistem hata verir. Geliştiriciye geri döner, geliştirici kodu düzeltir, tekrar derlenir, tekrar teste girer. **Süre: 1-2 hafta.**
- **Canlı Kullanımda Fark Edilen Hata ($100\times+$):** Sistem canlıdadır. Yüzlerce öğrenci 5'er kitap alıp götürmüş, kütüphanede kitap kalmamış, kütüphane müdürü rektörlüğe şikayet etmiş, sistem durdurulmuş, canlı veritabanı bozulmuştur. Çözüm: Acil yama (hotfix), canlı veritabanı temizliği, kurumsal itibar kaybı, fazla mesai ücretleri ve hukuki kriz yönetimi. **Maliyet: On binlerce lira ve haftalarca kaos.**

Sistem analizinin ve modellemenin asıl ekonomik gerekçesi budur: **Hataları inşa etmeden önce, kağıt üzerindeyken yakalamak ve yok etmek.**

---

## 4. Geleneksel Metodoloji 1: Şelale (Waterfall) Modeli

1970 yılında Dr. Winston W. Royce tarafından tanımlanan Şelale Modeli, yazılım mühendisliğinin ilk resmi ve en bilinen süreç modelidir.

![Şelale Modeli](assets/02-selale-modeli.svg)

### Modelin Yapısı ve Çalışma Mantığı
Şelale modeli, adını bir şelaleden aşağı dökülen suyun basamak basamak ilerlemesinden alır; su nasıl geriye doğru akamazsa, bu modelde de bir aşama bitmeden diğerine geçilemez.

1. **Sıralı ve Doğrusal Akış:** Gereksinimler $\rightarrow$ Tasarım $\rightarrow$ Kodlama $\rightarrow$ Test $\rightarrow$ Bakım adımları katı bir sıra izler.
2. **Aşama Dondurma ve Onay Kapıları (Milestones / Sign-offs):** Bir aşama tamamlandığında kapsamlı bir doküman üretilir. Bu doküman paydaşlar tarafından imzalanır ve o aşama resmi olarak **"dondurulur" (freeze)**. Örneğin Analiz aşaması bittikten sonra müşteri yeni bir istek getiremez.
3. **Dokümantasyon Hakimiyeti:** Projenin ilerlemesi üretilen belgelerle ölçülür. Kodlama başlamadan önce yüzlerce sayfalık sistem tasarım şartnameleri hazır olur.

### Avantajları
- **Yönetimi ve Planlaması Çok Kolaydır:** Hangi aşamanın ne zaman başlayıp biteceği takvimde ve Gantt şemasında gün gün bellidir.
- **Güçlü Belgelendirme:** Projede çalışan mühendisler işten ayrılsa bile, sistemin tüm mimarisi ve iş kuralları yazılı olduğu için yeni gelenler sistemi kolayca devralır.
- **Sözleşmeli ve Sabit Kapsamlı İşler İçin İdealdir:** Kamu ihalelerinde, sabit bütçeli ve kapsamı yasa ile belirlenmiş projelerde maliyet sürprizi yaşatmaz.

### Dezavantajları ve Krizleri
- **Değişime Tamamen Kapalıdır:** Gerçek hayatta hiçbir müşteri projenin başında tüm ihtiyaçlarını eksiksiz öngöremez. Şelale modeli değişimi bir "hata" olarak görür ve süreci kilitler.
- **"Büyük Patlama" (Big Bang) Sendromu:** Çalışan yazılım ilk kez projenin en sonunda, canlıya geçiş aşamasında ortaya çıkar. Müşteri 8 ay boyunca sadece kağıt okumuştur; 9. ayda ekranı gördüğünde *"Ben bunu böyle hayal etmemiştim"* derse proje iflas eder.
- **Geriye Dönüşün İmkansızlığı:** Test aşamasında fark edilen kritik bir analiz hatası, şelalenin en üstüne dönmeyi gerektirir. Bu durum projenin bütçesini tüketir ve teslimi aylarca geciktirir.

---

## 5. Geleneksel Metodoloji 2: V-Modeli (Doğrulama ve Geçerleme)

V-Modeli, Şelale modelinin en büyük eksiği olan "testin en sona bırakılması" sorununu çözmek üzere geliştirilmiş disiplinli bir uzantıdır.

![V-Modeli](assets/03-v-modeli.svg)

### Doğrulama ve Geçerleme Farkı
V-Modelinin felsefesi iki temel mühendislik sorusuna dayanır:
- **Doğrulama (Verification):** *"Ürünü doğru inşa ediyor muyuz?"* (Yazılımın mimari ve teknik şartnamelere uygunluğu denetlenir).
- **Geçerleme (Validation):** *"Doğru ürünü mü inşa ediyoruz?"* (Yazılımın kullanıcının gerçek iş problemini çözüp çözmediği test edilir).

### Modelin Yapısı ve Paralel Eşleşme
Model adını V harfi şeklindeki görsel yapısından alır:
- **Sol Kanat (Geliştirme / İniş):** İhtiyaç Analizi $\rightarrow$ Sistem Tasarımı $\rightarrow$ Mimari Tasarım $\rightarrow$ Modül Tasarımı basamaklarıyla soyuttan somuta iner.
- **V'nin Dibi:** Kodlama aşamasıdır.
- **Sağ Kanat (Test / Çıkış):** Birim Testi $\rightarrow$ Entegrasyon Testi $\rightarrow$ Sistem Testi $\rightarrow$ Kullanıcı Kabul Testi (UAT) basamaklarıyla somuttan kullanıcıya çıkar.

V-Modelinin devrim niteliğindeki kuralı şudur: **Sağ kanattaki test senaryoları, sol kanattaki geliştirme aşaması devam ederken yazılır!**
- İhtiyaç analizi yapılırken $\rightarrow$ Kullanıcı Kabul Testi (UAT) kriterleri belirlenir.
- Sistem tasarımı yapılırken $\rightarrow$ Sistem Performans ve Güvenlik Testi planı yazılır.
- Mimari tasarım yapılırken $\rightarrow$ Entegrasyon Testi senaryoları hazırlanır.
- Modül tasarımı yapılırken $\rightarrow$ Birim Testi (Unit Test) kodları yazılır.

### Değerlendirme ve Kullanım Alanları
- **Avantajı:** Hatalar henüz geliştirme aşamasındayken, test senaryosu tasarımı sayesinde kağıt üzerinde yakalanır. Kalite güvencesi en üst düzeydedir.
- **Dezavantajı:** Şelale gibi esneklikten uzaktır; kapsam dondurulduktan sonra değişiklik yapmak çok maliyetlidir ve prototipleme barındırmaz.
- **Nerede Kullanılır?** Hatanın doğrudan can veya devasa finansal kayıplara yol açtığı kritik sistemlerde: Havacılık uçuş kontrol yazılımları, otomotiv fren sistemleri (ABS/ESP), tıbbi cihaz yazılımları ve savunma sanayii projeleri.

---

## 6. Geleneksel Metodoloji 3: Spiral (Sarmal) Model

1988 yılında Barry Boehm tarafından geliştirilen Spiral Model, Şelale modelinin sistemli disiplini ile tekrarlı prototiplemenin esnekliğini birleştiren **risk odaklı** bir yaklaşımdır.

![Spiral Model](assets/04-spiral-model.svg)

### Spiral Hareket ve Dört Kadran
Model, merkezdeki bir noktadan dışarıya doğru genişleyen spiral halkalar şeklinde ilerler. Her spiral halkası yazılımın bir sürümünü veya aşamasını temsil eder. Spiralin her turu dört ana çeyrekten (kadrandan) geçer:

1. **Kadran 1 (Hedefler ve Kısıtlar):** Bu döngüde neyi başarmak istiyoruz? Alternatif çözümler nelerdir? Bütçe ve teknik kısıtlar nelerdir?
2. **Kadran 2 (Risk Analizi ve Prototip):** Bizi en çok korkutan, projeyi batırabilecek riskler nelerdir? (Örn: Veritabanı saniyede 10 bin işlemi kaldırabilir mi? Arayüzü kullanıcı anlayabilir mi?). Bu riskleri gidermek için **hızlı bir prototip** üretilir ve test edilir.
3. **Kadran 3 (Geliştirme ve Doğrulama):** Prototiple riski bertaraf edilen bölüm kodlanır, test edilir ve çalışan bir ürün artışı haline getirilir.
4. **Kadran 4 (Planlama ve Müşteri Değerlendirmesi):** Müşteri çalışan parçayı inceler, geri bildirim verir ve bir sonraki spiral turunun hedefleri planlanır.

### Değerlendirme
- **Avantajı:** Risk yönetimi modelin kalbindedir; felaketler sona ertelenmez, erkenden prototiplerle çözülür. Müşteri her turun sonunda somut bir şeyler görür.
- **Dezavantajı:** Yönetimi çok karmaşıktır; risk analizi uzmanlığı gerektirir. Küçük veya bütçesi dar projeler için aşırı pahalı ve bürokratiktir.
- **Nerede Kullanılır?** Büyük ölçekli, yüksek bütçeli, daha önce benzeri yapılmamış ve teknolojik belirsizliği yüksek Ar-Ge projelerinde.

---

## 7. Geleneksel Modellerin Karşılaştırma Matrisi

Doğru metodolojiyi seçmek bir projenin kaderini belirler:

| Kriter | Şelale (Waterfall) | V-Modeli | Spiral (Sarmal) Model |
| :--- | :--- | :--- | :--- |
| **Ana Felsefe** | Doğrusal disiplin ve belge | Erken test ve kalite güvencesi | Risk yönetimi ve tekrarlı prototip |
| **Gereksinim Değişimi** | Neredeyse imkansız (katı) | Çok zor ve maliyetli | Döngü başlarında esnek |
| **Müşteri Etkileşimi** | Başta (analiz) ve sonda (teslim) | Başta ve kabul testinde | Her spiral döngüsünün sonunda |
| **Risk Yönetimi** | Zayıf (riskler sona ertelenir) | Orta (test risklerini erken çözer) | Mükemmel (modelin merkezidir) |
| **Prototipleme** | Yok | Yok | Var (her döngüde) |
| **Çalışan Ürünün Görülmesi** | Projenin en sonunda | Projenin en sonunda | Her döngü sonunda parça parça |
| **En Uygun Projeler** | Gereksinimleri sabit, net projeler | Havacılık, medikal, kritik sistemler | Büyük, karmaşık, belirsiz Ar-Ge işleri |

---

## 8. Vaka Çalışması: Kütüphane Otomasyonunu Şelale ile Planlamak

Dönem boyunca takip ettiğimiz Kütüphane Otomasyonu projemizi üniversite yönetiminin katı bir **Şelale Modeli sözleşmesiyle** başlattığını varsayalım:

- **1. ve 2. Ay:** Analistler kütüphanecilerle görüşür, 150 sayfalık teknik şartname yazılır, rektörlük tarafından imzalanıp dondurulur.
- **3. ve 4. Ay:** Veritabanı tabloları ve mimari şemalar çizilir.
- **5., 6. ve 7. Ay:** Yazılımcılar şartnameye bakarak 3 ay boyunca aralıksız kod yazar.
- **8. Ay:** Test ekibi gelir, yazılımı test eder.
- **9. Ay:** Canlıya geçiş günü!

### Yaşanan Krizler ve Projenin Tıkanması

Bu projede Şelale modelinin gerçek hayatta patlak veren üç tipik krizi yaşanır:

1. **Değişen Gereksinim Krizi (4. Ayda):**
   Üniversite yönetimi yeni bir mobil uygulama yaptırmıştır ve rektörlük *"Öğrenciler kütüphaneye girmeden cep telefonundan karekod okutarak ödünç alabilsin, sistem bunu desteklesin"* talimatı verir. Şelale modeli gereği analiz aşaması aylar önce dondurulmuştur! Geliştirici ekip *"Sözleşmede bu yok, veritabanı buna göre çizilmedi"* der. Sonuç: Proje doğduğu gün eskiyen bir teknolojiye mahkum olur.
2. **Geç Fark Edilen Tasarım Hatası (8. Ayda):**
   Test ekibi sistemi denerken fark eder: Kütüphanede aynı romandan 5 adet fiziksel kopya vardır. Ancak şartnameyi yazan analist kitapları sadece **ISBN** numarasıyla modellemiş, her fiziksel kopyaya ayrı bir **Demirbaş/Barkod No** tanımlamamıştır! Sistem, hangi kopyanın kimde olduğunu ayırt edemez. Bu hata test aşamasında fark edildiği için; veritabanı şeması, SQL sorguları ve yazılan tüm ödünç verme fonksiyonları çöpe gider (Boehm'in $50\times$ kuralı!).
3. **Kullanıcı Şoku ve Direnci (9. Ayda):**
   Kütüphaneciler 9 ay boyunca çalışan hiçbir ekran görmemiştir. Canlıya geçiş günü arayüzü açtıklarında şok olurlar: Bir kitabı ödünç vermek için ekranda 4 farklı pencere açıp 6 kez onay düğmesine basmaları gerekmektedir. Kütüphaneci *"Eski karton fişle 10 saniyede hallediyordum, bu sistem beni 2 dakika bekletiyor!"* diyerek sistemi protesto eder ve masanın altında eski deftere kayıt tutmaya devam eder!

> **Çıkarılan Ders:** Müşteri ihtiyaçlarının değişken olduğu, kullanıcı deneyiminin kritik önem taşıdığı yazılım projelerinde Şelale gibi katı modeller felaketle sonuçlanabilir. İşte bu krizler, dünyayı haftaya göreceğimiz **Çevik (Agile)** devrimine sürüklemiştir.

---

## 9. İyi Pratikler ve Sık Yapılan Hatalar

| Hata | Neden Tehlikeli? | Doğru Pratik |
| :--- | :--- | :--- |
| **"Tek Model Her İşe Yarar" Yanılgısı** | Her projeyi ezbere Şelale ile veya ezbere Çevik ile yönetmeye çalışmak. | Projenin gereksinim netliğine, bütçesine ve risk düzeyine bakın. Gereksinimi kesinse Şelale, güvenliği kritikse V-Model, belirsizse Çevik/Spiral seçin. |
| **Testi Kodlamadan Sonraya Bırakmak** | "Önce kod bitsin, testçiler sonra baksın" yaklaşımı hataların maliyetini katlar. | V-Modeli ilkesini uygulayın: Analiz yaparken kabul testini, mimari çizerken entegrasyon testini planlayın. |
| **Aşama Dondurmayı İletişimsizlik Sanmak** | Şelalede analiz bitti diye müşteriyle iletişimi 6 ay boyunca tamamen kesmek. | Model Şelale olsa bile müşteriyi düzenli durum toplantılarıyla süreçte tutun. |

---

## 10. Kendinizi Deneyin (Bölüm Sonu Soruları)

1. **Soru 1 (Model Seçimi):** Bir biyomedikal firması için hastaların kalp atışlarını takip eden ve acil durumda otomatik ilaç dozu enjekte eden bir yoğun bakım cihazı yazılımı geliştireceksiniz. Bu projede Şelale mi, V-Modeli mi yoksa Spiral Model mi tercih edersiniz? Gerekçenizi hata maliyeti ve insan hayatı riski açısından açıklayınız.
2. **Soru 2 (Boehm Kuralı):** Kütüphane otomasyonunda "kitap kopyalarının tekil barkodla tutulması gerektiği" kuralı; a) Analiz aşamasında fark edilseydi, b) Canlıya geçtikten 3 ay sonra fark edilseydi ne gibi maliyet ve zaman farkları doğardı?
3. **Soru 3 (Spiral Mantığı):** Daha önce yapay zeka tabanlı yüz tanıma sistemiyle kütüphaneye turnikeden geçiş projesi yapmamış bir ekip bu işe girecektir. Spiral modelin 2. kadranı (Risk Analizi ve Prototip) bu ekibi nasıl bir felaketten korur?

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi, Sinanpaşa MYO*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
