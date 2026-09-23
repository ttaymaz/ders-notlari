# BM313 — İşletim Sistemleri

**Bilgisayar Mühendisliği** · Mühendislik Fakültesi · Afyon Kocatepe Üniversitesi
Öğr. Gör. Turgay Taymaz · Kısa adres: `tymz.org/bm313`

Bu sayfa dersin kalıcı adresidir. Sınav takvimi ve haftalık plan her dönem için ayrı tutulur; aşağıdan ilgili döneme geçiniz.

## Dönemler

| Dönem | Durum | Sayfa |
| ----- | ----- | ----- |
| 2026–2027 Güz | **Aktif** | [takvim ve plan](2026-2027-guz/) |

## İçerik

Ders içeriği [`konular/isletim-sistemleri/`](../konular/isletim-sistemleri/) altında ders kodundan bağımsız tutulur. Hangi konunun hangi haftada işlendiği her dönemin kendi sayfasında yer alır.

## Dersin Yaklaşımı

İşletim sistemi üç iş yapar ve ders bu üç işin etrafında kurulur:

- **Sanallaştırma:** Tek bir işlemciyi ve tek bir belleği, her programa kendine aitmiş gibi göstermek — prosesler, CPU zamanlama, adres uzayı, sayfalama.
- **Eşzamanlılık:** Aynı anda çalışan akışların birbirinin işini bozmaması — iş parçacıkları, kilitler, semaforlar, kilitlenme.
- **Kalıcılık:** Güç kesildiğinde verinin kaybolmaması — G/Ç, disk, dosya sistemleri.

Algoritmalar (CPU zamanlama, sayfa değiştirme, disk zamanlama) her hafta elle izlenebilir küçük bir örnek üzerinden işlenir. Aynı algoritmaları evde simülatörlerle rastgele üretilmiş yeni örnekler üzerinde deneyebilirsiniz.

## Kaynaklar

- **Ana kaynak:** Remzi H. Arpaci-Dusseau, Andrea C. Arpaci-Dusseau — *Operating Systems: Three Easy Pieces* (OSTEP). Çevrim içi ve ücretsiz: <https://pages.cs.wisc.edu/~remzi/OSTEP/>
- **Simülatörler:** OSTEP ödev simülatörleri (Python): <https://github.com/remzi-arpacidusseau/ostep-homework>
- **Ek kaynak:** Andrew S. Tanenbaum, Herbert Bos — *Modern Operating Systems*, 4. baskı, Pearson, 2015 (Merkez Kütüphane, basılı).
- **Kendi başına çalışma rehberi:** <https://cs.ossu.dev/coursepages/ostep/>

Her haftanın ders notu Türkçe ve kendi başına yeterlidir; OSTEP bölümleri ileri okuma olarak verilir.

## Değerlendirme

- **Ara Sınav (%40):** 8. hafta. Sanallaştırma bölümünü (1–7. haftalar) kapsar.
- **Yarıyıl Sonu Sınavı (%60):** Dönemin tamamını kapsar; ağırlık 9–15. haftalardadır.
