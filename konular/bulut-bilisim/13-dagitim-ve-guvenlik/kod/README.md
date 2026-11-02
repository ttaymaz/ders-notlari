# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

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

| Hücre | Beklenen |
| ----- | -------- |
| `current_user()` | Hesabınızın e-posta adresi |
| `SHOW CATALOGS` | Az sayıda katalog; `workspace` aralarında |
| `SHOW GRANTS` | **Kısa bir liste** — paylaşacak ikinci kullanıcı yok |
| `location` | `s3://...` ile başlayan bir yol; **ülke yazmıyor** |
| `SELECT * LIMIT 3` | Satır birimi: bölge ve saat, birey değil |

Üçüncü satır bu haftanın kısıtı ve kısıtın kendisi ders: ücretsiz bir genel
bulut hesabı **tek kişiliktir**, yani çok kullanıcılı yetkilendirme
denenemez. Bu bir eksiklik değil, **dağıtım modelinin** sonucu.

Dördüncü satır haftanın sürprizi. Tablonuz üçüncü haftada öğrendiğiniz yerde
— bir nesne depolama kovasında — ama hangi ülkede olduğunu o satır
söylemiyor ve orayı siz seçmediniz.

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
