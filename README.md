# Ders Notları — Öğr. Gör. Turgay Taymaz

Afyon Kocatepe Üniversitesi

Bu depo, ders notlarının ve örnek kodların öğrenciye açık **yansımasıdır**.
İçerik ayrı bir kaynak depoda üretilir ve buraya otomatik olarak kopyalanır.

> **Bu depo elle düzenlenmez.** Her yayında baştan üretilir; buraya yapılan
> değişiklikler bir sonraki yayında kaybolur. Bir hata fark ederseniz lütfen
> aşağıdaki e-posta adresine bildirin — düzeltme kaynakta yapılır.

## Dersler

| Kod | Ders | Program | Kısa adres |
| --- | ---- | ------- | ---------- |
| [BİL113](bil113/) | Programlama Temelleri | Bilgisayar Programcılığı | `tymz.org/bil113` |
| [SD129](sd129/) | Algoritma ve Programlama | İnternet ve Ağ Teknolojileri | `tymz.org/sd129` |
| [BİL203](bil203/) | Nesne Tabanlı Programlama I | Bilgisayar Programcılığı | `tymz.org/bil203` |
| [İNT215](int215/) | Görsel Programlama I | İnternet ve Ağ Teknolojileri | `tymz.org/int215` |
| [BVAP203](bvap203/) | Bulut Bilişim | Büyük Veri Analistliği | `tymz.org/bvap203` |
| [YBS403](ybs403/) | Sistem Analizi ve Tasarımı | Yönetim Bilişim Sistemleri | `tymz.org/ybs403` |
| [BM313](bm313/) | İşletim Sistemleri | Bilgisayar Mühendisliği | `tymz.org/bm313` |
| [SD329](sd329/) | İleri Programlama I | Yazılım Mühendisliği | `tymz.org/sd329` |

Aktif dönem: **2026–2027 Güz**

## Bu depoda ne var?

| Klasör | İçerik |
| ------ | ------ |
| `konular/` | Haftalık ders notları (`not.md`), örnek kodlar (`kod/`) ve akış şemaları |
| `<ders kodu>/` | Dersin hafta planı, sınav takvimi ve hafta → konu eşlemesi |

Ders içeriği ders kodundan bağımsız tutulur: aynı konu birden fazla derste
kullanılır. Hangi konunun hangi haftada işlendiği ilgili dersin dönem
sayfasında yazar.

**Derste kullanılan sunumlar sınıf içi materyaldir ve paylaşılmaz.** Ders
notları sunumdaki her şeyi ve fazlasını içerir — sınava çalışırken **ders
notunu** esas alınız.

## Örnek kodları çalıştırma

Örnek kodlar .NET 10 ile tek dosya olarak çalışır, proje kurmanız gerekmez:

```bash
dotnet run konular/programlamaya-giris/01-algoritma-ve-akis-semalari/kod/01-toplama.cs
```

Bilgisayarınızda .NET kurulu değilse **GitHub Codespaces** kullanabilirsiniz:
deponun ana sayfasında **Code → Codespaces → Create**. Tarayıcıda .NET 10 SDK
kurulu bir ortam açılır.

`kod/hatali/` klasöründeki dosyalar **kasıtlı olarak bozuktur** — hatayı bulup
düzeltmeniz için konulmuştur, derlenmemeleri normaldir.

## Lisans ve atıf

Ders notları ve şemalar **[CC BY-NC-SA 4.0](LICENSE)** ile lisanslanmıştır.
Örnek kodlar **MIT** lisanslıdır.

Bu materyali kullanabilir, çoğaltabilir ve uyarlayabilirsiniz. Koşullar:

- **BY — Atıf:** Kaynağı ve yazarı belirtmelisiniz.
- **NC — Ticari olmayan:** Ticari amaçla kullanamazsınız.
- **SA — Aynı lisansla paylaşım:** Uyarladığınız materyali aynı lisansla dağıtmalısınız.

Atıf biçimi için `CITATION.cff` dosyasına bakabilirsiniz.

Kendi dersinizde kullanıyorsanız haber verirseniz memnun olurum:
**turgaytaymaz@aku.edu.tr**
