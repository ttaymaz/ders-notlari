# YBS403 Dönem Projesi Kılavuzu

## 2026–2027 Güz Dönemi

**Ders:** YBS403 Sistem Analizi ve Tasarımı  
**Öğretim Elemanı:** Öğr. Gör. Turgay Taymaz  
**Ağırlık:** Ders başarı notunun %60'ı — **yarıyıl sonunda ayrıca yazılı sınav yapılmaz**

---

## 1. Projenin Amacı ve Felsefesi

Dönem projesinin amacı, 3–5 kişilik takımlar halinde gerçek dünyadaki bir iş problemini veya ihtiyacını seçerek, bir bilgi sisteminin fikir aşamasından nihai tasarımına kadar olan tüm **Sistem Geliştirme Yaşam Döngüsü (SGYD)** adımlarını uygulamaktır.

Bu projede temel ölçüt, sadece "diyagram çizmiş olmak" değildir. Asıl beklenen;
- Hazırlanan modellerin (Use Case, DFD, ERD, Sınıf, Sequence) birbiriyle **tutarlı** olması,
- Seçilen iş problemini gerçekçi ve kapsamlı bir şekilde çözmesi,
- Veri yapılarının, iş kurallarının ve sistem mimarisinin profesyonel standartlarda dokümante edilmesidir.

---

## 2. Takım Kuralları ve Rol Dağılımı

1. **Takım Büyüklüğü (3–5 Kişi, Hedef: 4–5 Kişi):** Bu dönem dersi 49 öğrenci almaktadır. 14 ve 15. haftalardaki sunum takviminin sağlıklı yürütülebilmesi, sınıf içi zamanın verimli kullanılması ve soru-cevapların derinleşebilmesi için **hedef takım sayısı 10–11 takımdır** (takım başına 4 veya 5 üye). 3 kişilik takımlara yalnızca zorunlu hallerde izin verilir.
2. **5 Kişilik Takımda Rol ve Sorumluluk Dağılımı:** Takım içinde "sosyal tembellik" (free-riding) yaşanmaması ve herkesin somut bir uzmanlık alanı olması için üyeler şu rollerden birini birincil sorumluluk olarak üstlenir:
   - **İş Analisti / Takım Lideri:** Paydaş analizi, problem tanımı, 4 boyutlu fizibilite ve Use Case senaryoları.
   - **Süreç Analisti:** DFD hiyerarşisi (Bağlam, Seviye 0/1), süreç tanımlamaları ve karar tabloları/ağaçları.
   - **Veri Mimarı:** Mantıksal ERD, Veri Sözlüğü standartları, fiziksel dönüşüm ve 3NF normalizasyon.
   - **Yazılım & Sistem Mimarı:** UML Sınıf diyagramı, Sequence/State diyagramları, 3-Tier/MVC katman tasarımı.
   - **Kullanıcı Deneyimi (UI/UX) Tasarımcısı:** Bilgi mimarisi, kritik ekran Wireframe tasarımları ve prototip demosu.
3. **Bireysel Katkı İlkesi:** Nihai raporda her bölümün altında o bölümü hazırlayan üyenin adı belirtilir. Sunumda **her üye** kendi sorumlu olduğu bölümü sunar ve soru-cevapta söz alır. Bireysel notlandırma takım notundan farklılaşabilir: Teslim 2'den itibaren, iş bölümünde bir üyeye ait bir bölüm raporda yoksa o bölümün puanı **yalnızca o üyeye** yansır; diğer üyeler kendi bölümleri üzerinden notlandırılır.
4. **Konu Seçimi ve Onay:** Her takım gerçek hayatta karşılığı olan bir iş problemi seçer (Örn: Lojistik Takip, Sağlık Randevu, E-Ticaret Sipariş Yönetimi, Restoran Otomasyonu, Dijital İçerik Platformu vb.). Derste örnek vaka olarak işlenen kütüphane otomasyonu proje konusu olarak seçilemez. Takım listesi ve konu, 2. haftanın sonuna kadar (**5 Ekim 2026, 23.59**) takım sorumlusu tarafından çevrim içi form ile bildirilir; form bağlantısı UES üzerinden duyurulur.

---

## 3. Ara Teslimler (Kilometre Taşları)

Dönem projesi son haftada tek seferde yazılan bir ödev değildir; dönem boyunca adım adım inşa edilir. Teslimler ayrı ödevler değil, **aynı raporun sürümleridir**: her teslimde raporun o güne kadarki tamamı yüklenir ve bir önceki teslime verilen geri bildirim bu sürümde düzeltilmiş olur.

| Aşama | Teslim Zamanı | Kapsam ve İstenen Belgeler |
| :---: | :-----------: | -------------------------- |
| **Teslim 1** | **4. Hafta**<br>25 Ekim 2026, 13.00 | **Proje Başlatma ve Fizibilite Raporu**<br>• Problem tanımı, hedefler, paydaş analizi ve sistem sınırları.<br>• 4 Boyutlu Fizibilite: Teknik, Ekonomik (Maliyet/Fayda & ROI), Operasyonel, Yasal (KVKK). |
| **Teslim 2** | **9. Hafta**<br>22 Kasım 2026, 13.00 | **Gereksinim Analizi Paketi**<br>• Teslim 1 bölümleri, geri bildirime göre düzeltilmiş ve eksikleri tamamlanmış olarak.<br>• Fonksiyonel ve fonksiyonel olmayan gereksinim listesi.<br>• UML Use Case diyagramı ve kritik senaryo metinleri.<br>• Veri Akış Diyagramları (Bağlam, Seviye 0 ve en az bir Seviye 1 patlatma).<br>• Süreç tanımlamaları (karar tablosu / karar ağacı).<br>• Mantıksal ERD (Crow's Foot notasyonu) ve Veri Sözlüğü taslağı. |
| **Teslim 3** | **13. Hafta**<br>20 Aralık 2026, 13.00 | **Nihai Rapor: Detaylı Tasarım Paketi eklenmiş tam rapor**<br>• Teslim 2 bölümleri, geri bildirime göre düzeltilmiş olarak.<br>• UML Sınıf Diyagramı (Nitelikler, metotlar ve ilişkiler).<br>• Davranışsal Modeller: En az bir kritik senaryo için Sıralama (Sequence) ve Durum (State) diyagramları.<br>• Sistem Mimarisi (3-Tier / MVC) tasarımı ve gerekçesi.<br>• Kritik ekranların Wireframe tasarımları.<br>• Fiziksel Veritabanı Şeması ve 3NF Normalizasyon analizi.<br>• Test planı, canlıya geçiş stratejisi ve takım içi iş bölümü (5. bölümdeki şablonun tamamı). |

**Değişiklik tablosu:** Teslim 2'den itibaren raporun başına, içindekilerden önce, bir değişiklik tablosu konur. Her satır bir geri bildirim maddesini, yapılan düzeltmeyi ve düzeltmenin bulunduğu bölüm ve sayfayı gösterir. Uygun bulunmayan bir düzeltme varsa gerekçesi aynı tabloya yazılır.

---

## 4. Proje Sunumu Kuralları (14. ve 15. Haftalar: 22 ve 29 Aralık 2026)

Toplam 10–11 takım iki haftaya dengeli biçimde paylaştırılır (Hafta başına 5 veya 6 takım). Her takıma **15–20 dakika** (12–15 dk sunum + 5 dk soru-cevap) süre ayrılmıştır.

### Zaman Yönetimi ve Sunum Akışı (Maksimum 20 Dk)

| Bölüm | Süre | Sorumlu Rol / Açıklama |
| ----- | :--: | ---------------------- |
| **Giriş ve Problem** | 2 dk | **İş Analisti:** İş problemi, çözüm vizyonu ve fizibilite bulguları |
| **Gereksinim Analizi** | 4 dk | **Süreç & İş Analisti:** Use Case, DFD hiyerarşisi ve ERD veri modeli özeti |
| **Sistem Tasarımı** | 6 dk | **Sistem Mimarı & UI/UX:** Sınıf diyagramı, mimari katmanlar, Sequence diyagramı ve ekran Wireframe'leri |
| **Veri Sözlüğü ve Normalizasyon** | 3 dk | **Veri Mimarı:** Örnek bir veri akışının ve tablonun sözlük üzerinden incelenmesi, 3NF adımları |
| **Soru-Cevap ve Tartışma** | 5 dk | **Tüm Takım:** Jüri / öğretim elemanı soruları ve takım değerlendirmesi |

> **Not:** Çalışan bir prototip veya arayüz demosu zorunlu değildir ancak puanlamada artı değer sağlar.

---

## 5. Nihai Rapor Şablonu (SGYD Akışı)

Teslim 3 ile 13. haftada (son teslim 20 Aralık 2026, 13.00) PDF olarak yüklenecek nihai teknik rapor şu ana başlıkları eksiksiz içermelidir. Sunumdan sonra ayrıca rapor yüklenmez.

```markdown
1. GİRİŞ VE PLANLAMA
   1.1. Projenin Amacı ve Kapsamı
   1.2. Paydaşlar ve Sistem Sınırları
   1.3. Dört Boyutlu Fizibilite Analizi (Teknik, Ekonomik, Operasyonel, Yasal)
2. GEREKSİNİM ANALİZİ
   2.1. Fonksiyonel ve Fonksiyonel Olmayan Gereksinimler
   2.2. UML Kullanım Senaryoları (Use Case Diyagramı ve Senaryo Şablonları)
   2.3. Süreç Modelleme (Veri Akış Diyagramları: Bağlam, Seviye 0, Seviye 1)
   2.4. Süreç Tanımlamaları (Karar Tablosu / Karar Ağacı)
   2.5. Kavramsal Veri Modelleme (Mantıksal ERD)
   2.6. Analiz Seviyesi Veri Sözlüğü
3. SİSTEM TASARIMI
   3.1. Nesneye Yönelik Tasarım (UML Sınıf Diyagramı)
   3.2. Davranışsal Modelleme (UML Sıralama ve Durum Diyagramları)
   3.3. Sistem Mimarisi (3-Tier / MVC Katman Yapısı)
   3.4. Kullanıcı Deneyimi ve Arayüz Tasarımı (Wireframe & Bilgi Mimarisi)
   3.5. Fiziksel Veritabanı Tasarımı ve 3NF Normalizasyon
   3.6. Fiziksel Veri Sözlüğü
4. TEST, GEÇİŞ VE BAKIM STRATEJİSİ
   4.1. Test Planı (Birim, Entegrasyon, Sistem Testleri)
   4.2. Canlıya Geçiş (Devreye Alma) Stratejisi
5. SONUÇ VE ÖZ DEĞERLENDİRME
   5.1. Takım İçi İş Bölümü ve Katkı Oranları
```

---

## 6. Değerlendirme

Dönem projesi notu aşağıdaki bileşenlerden oluşur. Dönem projesi, ders başarı notunun %60'ıdır.

| Bileşen | Ağırlık | Not türü | Değerlendirme Odakları |
| ------- | :-----: | :------: | ---------------------- |
| **Teslim 1:** Proje Başlatma ve Fizibilite | %10 | Takım | Problemin derinliği, sınırların netliği, paydaş analizi, gerçekçi ve hesabı gösterilmiş maliyet-fayda analizi |
| **Teslim 2:** Gereksinim Analizi Paketi | %20 | Takım | Use Case, DFD ve ERD arasındaki uyum; DFD seviye kurallarına uygunluk; veri sözlüğünün eksiksizliği |
| **Teslim 3:** Nihai Rapor | %30 | Takım | Sınıf ilişkileri, Sequence akışı, katmanlı mimari gerekçesi, 3NF kurallarına uygunluk; raporun bütün olarak tutarlılığı |
| **Proje Sunumu** | %20 | Takım | Ayrılan sürenin etkin kullanımı, akışın anlaşılırlığı, varsa prototip demosu |
| **Bireysel Soru-Cevap** | %20 | Bireysel | Üyenin kendi hazırladığı bölüme hâkimiyeti ve sorulara net yanıtı |

- Her teslim yalnızca o teslimde eklenen **yeni bölümlerden** puanlanır; önceki bölümler yeniden puanlanmaz.
- Teslim puanının **%10'u geri bildirime uyumdur**: değişiklik tablosu ve tabloda gösterilen düzeltmeler.
- Teslim 2'den itibaren, iş bölümünde bir üyeye ait bir bölüm raporda yoksa o bölümün puanı yalnızca o üyeye yansır; diğer üyelerin notu o bölüm dışındaki puan üzerinden hesaplanır (örnek: 10 puanlık bölüm yoksa ve takım kalan 90 puandan 63 aldıysa diğer üyelerin notu 63 / 90 × 100 = 70). Teslim 1'de takımdaki herkes aynı notu alır.
- Bireysel soru-cevapta her üyeye, bölüm altlarında adının yazılı olduğu bölümlerden soru sorulur.
