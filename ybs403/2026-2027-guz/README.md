# YBS403 — Sistem Analizi ve Tasarımı

## 2026–2027 Güz Dönemi

**Yönetim Bilişim Sistemleri** · Afyon Kocatepe Üniversitesi
Öğr. Gör. Turgay Taymaz
Kısa adres: `tymz.org/ybs403`

## Değerlendirme ve Sınav Takvimi

| Değerlendirme | Ağırlık | Tarih | Kapsam / Açıklama |
| ------------- | :-----: | ----- | ----------------- |
| **Ara Sınav** | %40 | 09–13 Kasım 2026 | 1.–7. hafta konuları · **Tamamı test** (çoktan seçmeli) |
| **Dönem Projesi** *(yarıyıl sonu sınavı yok)* | %60 | 4.–15. Hafta | Aşağıdaki üç teslim ve sunumdan oluşur; ağırlıklar dönem projesi notu içindir |
| ↳ **Teslim 1:** Fizibilite Raporu | %10 | 25 Ekim 2026, 13.00 (UES) | Proje tanımı, kapsam ve 4 boyutlu fizibilite analizi |
| ↳ **Teslim 2:** Gereksinim Analizi | %20 | 22 Kasım 2026, 13.00 (UES) | Düzeltilmiş Teslim 1 + gereksinim listesi, Use Case, DFD (Bağlam, Seviye 0, en az bir Seviye 1), ERD ve Veri Sözlüğü |
| ↳ **Teslim 3:** Nihai Rapor | %30 | 20 Aralık 2026, 13.00 (UES) | Düzeltilmiş Teslim 2 + Sınıf, Sequence ve Durum, Sistem Mimarisi, Wireframe, Veritabanı şeması ve 3NF, test ve geçiş |
| ↳ **Proje Sunumu** | %20 | 22 ve 29 Aralık 2026 | Takım başına 15–20 dk canlı sunum ve prototip demosu |
| ↳ **Bireysel Soru-Cevap** | %20 | 14.–15. Hafta | Her üyeye kendi hazırladığı bölümden soru |

Teslimler aynı raporun sürümleridir: her teslimde raporun o güne kadarki tamamı, bir önceki geri bildirime göre düzeltilmiş olarak yüklenir.

> Dönem projesinin ayrıntılı şablonu, grup kuralları ve puanlama rubriği için **[Dönem Projesi Kılavuzu](proje-kilavuzu.md)** sayfasına bakınız.

## Haftalık Plan

Ders içeriği [`konular/sistem-analizi-ve-tasarimi/`](../../konular/sistem-analizi-ve-tasarimi/) altında ders kodundan bağımsız olarak tutulur. Ders notu bağlantıları o hafta yayımlandıkça tabloya eklenir.

| Hafta | Konu | Ders notu | Dönem Projesi Görevi |
| :---: | ---- | :-------: | -------------------- |
| 0 | Ders tanıtımı, SGYD felsefesi ve proje yönergeleri | — | Takımların oluşturulması (3–5 kişi) |
| 1 | Sisteme ve sistem analizine giriş: temel kavramlar, bilgi sistemi türleri | [not](../../konular/sistem-analizi-ve-tasarimi/01-sistem-kavrami-ve-analistin-rolu/not.md) | Problem alanı ve sektör araştırması |
| 2 | SGYD ve geleneksel süreç modelleri: Şelale, V-Model, Spiral Model | [not](../../konular/sistem-analizi-ve-tasarimi/02-sgyd-ve-geleneksel-modeller/not.md) | Takım ve konu bildirimi |
| 3 | Modern yaklaşımlar: Çevik (Agile) felsefe ve Scrum çerçevesi | [not](../../konular/sistem-analizi-ve-tasarimi/03-cevik-yaklasimlar-ve-scrum/not.md) | Kullanıcı hikayeleri ve Product Backlog taslağı |
| 4 | Proje başlatma, paydaş analizi ve fizibilite analizi | [not](../../konular/sistem-analizi-ve-tasarimi/04-proje-baslatma-ve-fizibilite/not.md) | **Teslim 1:** Proje Tanımı ve Fizibilite Raporu (son teslim 25 Ekim 2026, 13.00) |
| 5 | Gereksinim analizi 1: UML Use Case diyagramları ve senaryolar | [not](../../konular/sistem-analizi-ve-tasarimi/05-gereksinim-analizi-ve-use-case/not.md) | Aktörler ve Use Case diyagramı |
| 6 | Gereksinim analizi 2: Süreç modelleme — Veri Akış Diyagramları (DFD) ve süreç mantığı | [not](../../konular/sistem-analizi-ve-tasarimi/06-surec-modelleme-ve-dfd/not.md) | Bağlam (Context) ve Seviye 0 DFD çizimleri |
| 7 | Gereksinim analizi 3: Veri modelleme — Varlık-İlişki Diyagramları (ERD), Veri Sözlüğü ve Vize Provası | [not](../../konular/sistem-analizi-ve-tasarimi/07-veri-modelleme-ve-erd/not.md) | Mantıksal ERD ve Veri Sözlüğü hazırlığı |
| 8 | **Ara Sınav Haftası** *(Tamamı çoktan seçmeli test)* | | |
| 9 | Nesneye yönelik tasarım: UML Sınıf Diyagramları ve ERD'den sınıfa geçiş | [not](../../konular/sistem-analizi-ve-tasarimi/09-nesneye-yonelik-tasarim-ve-sinif-diyagrami/not.md) | **Teslim 2:** Gereksinim Analizi Paketi (son teslim 22 Kasım 2026, 13.00) |
| 10 | Davranışsal modelleme: UML Sıralama (Sequence), Durum ve Aktivite diyagramları | [not](../../konular/sistem-analizi-ve-tasarimi/10-davranissal-modelleme/not.md) | Kritik senaryo Sequence ve Durum diyagramları |
| 11 | Sistem mimarisi ve tasarım desenleri: 3 Katmanlı Mimari (3-Tier) ve MVC | [not](../../konular/sistem-analizi-ve-tasarimi/11-sistem-mimarisi/not.md) | Sistem mimarisinin tasarlanması ve gerekçesi |
| 12 | Arayüz tasarımı ve İBE ilkeleri: Nielsen'in 10 sezgisi, Wireframe ve Mockup | [not](../../konular/sistem-analizi-ve-tasarimi/12-arayuz-tasarimi/not.md) | Kritik ekranların Wireframe tasarımları |
| 13 | Veritabanı tasarımı: Mantıksal-fiziksel dönüşüm, Normalizasyon ve test/geçiş özeti | [not](../../konular/sistem-analizi-ve-tasarimi/13-veritabani-test-ve-gecis/not.md) | **Teslim 3:** Nihai Rapor (son teslim 20 Aralık 2026, 13.00) |
| 14 | **Dönem Projesi Sunumları — 1. Oturum** *(22 Aralık 2026; takım başına 15–20 dk)* | — | 1. Grup Proje Sunumları |
| 15 | **Dönem Projesi Sunumları — 2. Oturum** *(29 Aralık 2026; takım başına 15–20 dk)* | — | 2. Grup Proje Sunumları |

## Sunumlar

Derste kullanılan sunumlar sınıf içi etkileşim materyalidir ve ham haliyle paylaşılmaz. Ders notları sunumdaki kavramları, şemaları, günlük hayat örneklerini ve pratikleri eksiksiz içerir; sınava ve projenize hazırlanırken **ders notunu** esas alınız.
