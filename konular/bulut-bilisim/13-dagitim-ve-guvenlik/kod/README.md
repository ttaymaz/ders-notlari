# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

**Önce dosyayı indirin:** GitHub'da dosyayı açın → **Raw** düğmesi →
tarayıcıda **Ctrl+S** ile kaydedin. Uzantısı `.py` kalsın.

Sonra çalışma alanına aktarın: sol menü → **Workspace** → **Users** →
**kendi e-posta adresiniz** → o satırın sağındaki **üç nokta** →
**Import** → **File** → indirdiğiniz dosyayı seçin.
`# COMMAND ----------` satırları hücre sınırlarını gösterir.

> **Üstteki `Workspace` satırına değil, kendi klasörünüze aktarın.** Kök
> `Workspace` düğümünde içe aktarma hata verir; oraya yazma yetkiniz yok.
> Kendi e-posta adresinizi taşıyan klasör sizindir, içe aktarma orada
> çalışır.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-erisim.py`](01-erisim.py) | Kimlik, katalog hiyerarşisi, mevcut yetkiler, verinin fiziksel konumu ve satır biriminin kişisel veri sorusuyla ilişkisi |

## Bu hafta not defteri veri okumuyor

Dönem boyunca ilk kez. Sorular veriye değil **kataloga** soruluyor: kim
olduğunuz, neyin var olduğu, kimin neye eriştiği, verinin nerede durduğu.

Bu yüzden hücreler hızlı çalışır; bekleyecek bir şey yok.

## Çalıştırmadan önce

Geçen haftanın tablosunun kurulmuş olması gerekiyor. `TABLO` satırına
dokunmanıza gerek yok; tablo adınız farklıysa yalnızca onu düzeltin.

## Beklenen çıktılar

| Hücre | Ölçülen |
| ----- | ------- |
| `current_user()` | Hesabınızın e-posta adresi |
| `SHOW CATALOGS` | **4 katalog:** `workspace`, `samples`, `system` + 1 |
| `SHOW SCHEMAS IN workspace` | `default` ve `information_schema` |
| `SHOW GRANTS ON TABLE` | **Boş** — hiçbir satır yok |
| `DESCRIBE DETAIL` → `location` | **Boş** — konum soyutlanmış |
| `EXPLAIN` → `Location:` | `s3://...` ile başlayan yol; **ülke yazmıyor** |
| `SELECT * LIMIT 3` | Satır birimi: bölge ve saat, birey değil |

**Dördüncü satır bu haftanın en kolay yanlış okunan sonucu.** Boş liste
"kimse erişemiyor" demek **değil** — siz erişiyorsunuz. Sebebi tablonun
**sahibi** olmanız; sahiplik bir yetki kaydı değil, `SHOW GRANTS` yalnızca
açıkça verilmiş yetkileri listeler. Sahibin hakları verilmemiş, baştan var.

**Beşinci satır da bir hata değil.** Kurduğunuz tablo **yönetilen** bir
tablodur; nerede durduğuna platform karar verir ve bilmeniz gerekmez. Konum
bilerek soyutlanmış. Tek göründüğü yer sorgu planı — yani planı okuduğunuz
için görüyorsunuz.

> **`samples` kataloğunu siz kurmadınız.** Okuyabiliyorsunuz çünkü size
> **verilmiş.** "Bu ortamda yetki verme pratiği yapamayız" derken bir şeyi
> atlamışız: siz zaten bir yetkinin **alıcı** tarafındasınız.

> **Çalıştırmayacağımız bir satır var:**
> `GRANT SELECT ON TABLE ... TO ...`
> Sözdizimini bilmek, yetkinin kodun içinde değil katalogda yaşadığını
> anlamak için yeterli. Sorun bu satırın ne kadar **kolay** olması.

## Denemeniz için

1. `SHOW GRANTS ON SCHEMA workspace.default` çalıştırın. Tablo düzeyindeki
   listeden farklı mı?
2. `CREATE SCHEMA workspace.deneme` ile yeni bir şema açın ve tablonuzun
   kopyasını oraya alın. İki şemanın yetkileri birbirinden bağımsız mı?
3. `SHOW GRANTS ON CATALOG workspace` çalıştırın. Katalog düzeyinde kim var?
4. Trafik verisini araç başına toplanmış hâlde hayal edin. Hangi sütunlar
   eklenirdi ve o veri kişisel veri olur muydu?

Dördüncü madde haftanın en önemli denemesi ve kod gerektirmiyor. Cevabı
yazın: aynı olay, farklı toplama birimi, farklı cevap.
