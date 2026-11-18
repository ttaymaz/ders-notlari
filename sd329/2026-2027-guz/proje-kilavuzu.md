# SD329 Dönem Projesi

**Yazılım Mühendisliği** · Mühendislik Fakültesi · Afyon Kocatepe Üniversitesi
2026–2027 Güz · Öğr. Gör. Turgay Taymaz

Dersin **yarıyıl sonu notunun tamamı** (%60) bu projeden gelir. Ayrıca
yazılı final sınavı yapılmaz. **Bütünleme sınavı yazılıdır** (kod okuma,
100 puan); projenin yerine geçer.

---

## Özet

| | |
| --- | --- |
| **Çalışma biçimi** | Bireysel |
| **Ne** | Kendi seçtiğiniz bir alanda, katmanlı bir C# konsol uygulaması |
| **Nerede** | Kendi GitHub hesabınızda, ortak şablondan açılan bir repo |
| **Alan seçimi** | **27 Kasım 2026** Cuma, 23.59 — form |
| **Ara teslim** | **10 Aralık 2026** Perşembe, 23.59 — notlanmaz, geri bildirim 18 Aralık dersinde |
| **Son teslim** | **2 Ocak 2027** Cumartesi, 23.59 — repo public olur |
| **Değişiklik isteği** | 4 Ocak 2027'de reponuza açılan issue; **48 saat** içinde pull request |
| **Değerlendirme** | 100 puan; 4. bölümdeki tabloya göre |
| **Yapay zekâ** | **Serbest.** Nasıl kullandığınızı `YAPAY-ZEKA.md` dosyasına yazarsınız |

---

## 1. Proje Nedir?

Bu dönem C#'ın ve .NET'in mekanizmalarını tek tek gördük: `record`,
jenerikler, delegeler, LINQ, desen eşleme, hata yönetimi, birim testi,
bağımlılık enjeksiyonu, `async`. Projede bunları **bir arada** kullanan,
çalışan ve sınanmış bir uygulama yazıyorsunuz.

Herkes aynı **iskeletten** başlar; farklı olan **alandır**. Biriniz otopark
yönetimi, biriniz kütüphane ödünç takibi yazar. İskelet ortak olduğu için
kimse proje yapısıyla uğraşmaz; emek alanın kurallarına ve onları doğru
kodlamaya gider.

Uygulama **konsoldur**. Görsel arayüz bu dersin konusu değil; ölçülen şey
kodun kendisi.

> **Bu repo sizin.** Son teslimde public olur ve sizin adınıza durur. Bir
> iş başvurusunda "dönem projem" diye gösterebileceğiniz bir şey yazın.

---

## 2. Başlangıç

### 2.1 Repoyu açın

1. Şablon repoya gidin: **`github.com/ttaymaz/sd329-proje-sablonu`**
2. **Use this template → Create a new repository**
3. Repo adı: alanınızı anlatan kısa bir ad (`otopark-yonetimi`, `kutuphane-odunc`)
4. Görünürlük: **Private**. Son teslime kadar private kalır; böylece
   arkadaşlarınız kodunuzu göremez, siz de onlarınkini
5. **Settings → Collaborators → Add people → `ttaymaz`**. Öğretim elemanı
   ara teslimde ve sonda reponuzu buradan görür

### 2.2 Formu doldurun

Alan seçimi formu ders sayfasında duyurulur. Formda adınız, numaranız,
GitHub kullanıcı adınız, repo adresiniz ve alanınız sorulur.

**Alanlar ilk gelen alır.** Bir alanı biri seçtiğinde listeden düşer.
Listede olmayan bir alan önermek isterseniz formdaki "Diğer" kutusuna
yazın; öğretim elemanı onaylarsa sizindir.

> **Adınız görünmek zorunda değil.** README'de gerçek adınız yerine
> yalnızca GitHub kullanıcı adınızın yazmasını isteyebilirsiniz; formda
> bunu seçin. Kullanıcı adı ile öğrenci numarası eşlemesi yalnızca
> öğretim elemanında kalır.

### 2.3 Çalıştırın

```
git clone <repo adresiniz>
cd <repo adı>
dotnet test
dotnet run --project src/Uygulama
```

**.NET 10 SDK gerekir.** Şablon ilk açıldığında 5 test geçer ve boş bir
menü çalışır. Buradan başlıyorsunuz.

### 2.4 Şablonda ne var, ne yok?

| Var (dokunmanız gerekmez, ama okuyun) | Yok (sizin işiniz) |
| ------------------------------------- | ------------------ |
| Dört proje: `Cekirdek`, `Altyapi`, `Uygulama`, `Testler` | Alanınızın kayıtları ve kuralları |
| `Sonuc<T>` (9. hafta) | Servisleriniz |
| `IDepo<T>` ve onun JSON dosya sürümü | Olay(lar)ınız |
| Generic Host: DI, `appsettings.json`, günlükleme | LINQ raporlarınız |
| Boş bir konsol menüsü (`TODO` işaretli) | Menü maddeleriniz |
| 5 örnek test, GitHub Actions (her push'ta test) | Kendi testleriniz |
| `README.md` ve `YAPAY-ZEKA.md` taslakları | İkisinin içi |

---

## 3. Zorunlu Öğeler

Uygulamanız aşağıdakilerin **hepsini** içerir. Her satırın yanındaki hafta,
o konuyu derste gördüğümüz yer; takıldığınızda ilk bakacağınız ders notu.

| Konu | Projede | Hafta |
| ---- | ------- | :---: |
| `record` | Alanınızın varlıkları (`Arac`, `Rezervasyon`...) `record` olarak yazılır | 2 |
| Jenerikler | En az iki farklı kayıt tipi `IDepo<T>` üzerinden saklanır | 3 |
| Olay | En az bir `event`; abonesi menüde ya da bir serviste (ör. `KapasiteDoldu`) | 4 |
| LINQ | En az **üç** rapor ya da sorgu (filtreleme, sıralama, gruplama) | 6 |
| Desen eşleme | En az bir `switch` ifadesi, ilişkisel ya da özellik deseniyle | 6 |
| Hata yönetimi | Koruma cümleleri; olağan başarısızlıklar için `Sonuc<T>` ya da Try kalıbı | 9 |
| Birim testi | En az **15** kendi testiniz; boş, sınır ve geçersiz girdi durumları dahil | 9 |
| Bağımlılık enjeksiyonu | Servisler kurucudan bağımlılık alır, `Program.cs`'te kaydedilir | 10 |
| Yapılandırma, günlükleme | En az bir kuralınız `appsettings.json`'dan okunur; servisler `ILogger` kullanır | 10 |
| `async` | Depo çağrıları `await` ile; `CancellationToken` servise kadar iletilir | 11 |

**Kural katmanlarda durur:** iş kuralları `Cekirdek`'te, dosya işleri
`Altyapi`'de, konsol `Uygulama`'da. Menü kural içermez; servisi çağırır ve
sonucu yazar.

**Artı puan** (en fazla 5): 12–14. haftaların konularından birini anlamlı
biçimde kullanmak. Örneğin bir arka plan işini `Channel` ile yürütmek ya da
bir raporun başarımını ölçüp iyileştirmek. Notu 100'ün üzerine çıkarmaz;
eksik bir kalemi telafi eder.

---

## 4. Değerlendirme

| Ölçüt | Puan | Neye bakılır? |
| ----- | :--: | ------------- |
| **Çalışıyor** | 10 | `main` dalında GitHub Actions yeşil; `dotnet run` menüsü çalışıyor |
| **Mimari** | 20 | Katmanlar doğru; servisler DI ile; yapılandırma ve günlükleme kullanılmış |
| **Dil özellikleri** | 15 | 3. bölümdeki zorunlu öğeler, yerinde kullanılmış |
| **Hata yönetimi** | 10 | Yanlış argüman / yanlış durum / olağan başarısızlık ayrımı doğru yapılmış |
| **Testler** | 15 | En az 15 test; sınır durumları; adlar ne sınandığını söylüyor |
| **Git geçmişi** | 10 | 5: ara teslim tarihinde çalışan iskelet · 5: commit'ler haftalara dağılmış |
| **Değişiklik isteği** | 20 | 5. bölüm |

### Puan getirmeyen şeyler

- Kod satırı sayısı. Kısa ve doğru, uzun ve dağınıktan iyidir
- Görsel süs: renkli konsol, ASCII sanatı
- Zorunlu öğeyi "olsun diye" kullanmak: hiçbir şeyi bildirmeyen bir olay,
  tek yerde kullanılan bir jenerik sınıf

---

## 5. Değişiklik İsteği

Son teslimden sonraki gün (**4 Ocak 2027**) reponuza öğretim elemanı
tarafından bir **issue** açılır. Issue sizin projenize özgü bir değişiklik
ister. Örnek:

> *Rezervasyon iptali 24 saatten az kala yapılamasın. Kural ihlalinde
> `Hatali` sonuç dönsün; süre `appsettings.json`'dan okunsun.*

Issue açıldıktan sonra **48 saat** içinde:

1. Yeni bir dalda değişikliği yapın
2. Değişikliği sınayan **testler** ekleyin
3. `YAPAY-ZEKA.md` dosyasına bu değişiklik için kayıt ekleyin
4. `main` dalına bir **pull request** açın; açıklamada issue'ya bağlantı verin
   (`Closes #1`)

Pull request'i birleştirmeyin; değerlendirmeden sonra siz birleştirirsiniz.

| Değişiklik isteği (20 puan) | Puan |
| --------------------------- | :--: |
| İstenen davranış doğru, mevcut kurallar bozulmamış (CI yeşil) | 8 |
| Yeni testler değişikliği gerçekten sınıyor | 6 |
| Değişiklik doğru katmanda; kod projenin geri kalanıyla tutarlı | 3 |
| `YAPAY-ZEKA.md` kaydı: ne istendi, ne alındı, ne düzeltildi, nasıl doğrulandı | 3 |

> **Bu bölüm neyi ölçüyor?** İşe girdiğinizde çoğunlukla sıfırdan uygulama
> yazmayacaksınız. Var olan bir kod tabanında, verilen bir isteği doğru
> yapacaksınız. Yapay zekâ bu işte çok hızlıdır, ama mevcut testleri kırar,
> kuralı yanlış katmana koyar ya da istenmeyen bir şeyi değiştirir. Bunu
> fark edip düzeltmek sizin işinizdir.

---

## 6. Yapay Zekâ Kullanımı

Yapay zekâ araçlarını (sohbet asistanları, editör eklentileri) **kullanmanız
serbesttir**, hatta önerilir. Çalışma hayatınızda da kullanacaksınız. Bu
projede ölçülen şey, onları **doğru** kullanıp kullanmadığınızdır.

- Her önemli adımı `YAPAY-ZEKA.md` dosyasındaki tabloya yazın: ne istediniz,
  ne aldınız, neyi kabul ettiniz, neyi **düzelttiniz ya da reddettiniz** ve
  nasıl doğruladınız
- Yapay zekânın yazdığı kodu **çalıştırmadan önce okuyun.** Derste
  yaptığımız gibi çıktısını tahmin edin
- Testleri yapay zekâya yazdırabilirsiniz; ama bir testin **neyi** sınadığını
  siz bilmelisiniz. Hiçbir şeyi sınamayan test (her zaman geçen test) puan
  getirmez
- Çözümleyici uyarılarını okuyun. Şablon uyarısız derlenir; çıkan her uyarı
  sizin kodunuzdan gelir

Repo public olduğu için yazdığınız her şey, `YAPAY-ZEKA.md` dahil, sizin
adınıza durur.

---

## 7. Teslim

**Ara teslim (10 Aralık, 23.59):** `main` dalında dört proje derleniyor, CI
yeşil, alanınıza ait **en az 5** test var. Not verilmez; 18 Aralık dersinde
herkese kısa bir geri bildirim verilir. Bu tarihte iskeleti çalışır durumda
olan reponun git geçmişi puanının yarısını alır.

**Son teslim (2 Ocak 2027, 23.59):** Bu saatte `main` dalının durumu
kopyalanır; sonraki commit'ler değerlendirilmez. Repo bu saate kadar
**public** yapılır (**Settings → General → Danger Zone → Change visibility**).

**Teslim için ayrıca bir şey yüklemezsiniz.** Repo adresiniz formdan bilinir.

---

## 8. Özgünlük

- Şablondan gelen kod herkeste aynıdır; bu kopya sayılmaz
- Yapay zekâ kullanmak kopya sayılmaz; `YAPAY-ZEKA.md` tam olarak bunun için
- **Başka bir öğrencinin kodunu almak kopyadır.** Repolar teslime kadar
  private; teslimden sonra public olunca kodlar karşılaştırılır
- Git geçmişi bir iş günlüğüdür. Altı haftaya yayılmış küçük commit'ler ile
  son gece tek commit'le gelen bir repo farklı hikâyeler anlatır
- Değişiklik isteğini kodunuzu anlamadan yapmak çok zordur

---

## 9. Takvim

| Tarih | Ne |
| ----- | -- |
| 20 Kasım 2026 | Proje duyurusu; şablon ve form açılır |
| **27 Kasım** | Alan seçimi formu kapanır |
| **10 Aralık**, 23.59 | Ara teslim |
| 18 Aralık | Ara teslim geri bildirimi (derste) |
| **2 Ocak 2027**, 23.59 | Son teslim; repo public |
| 4 Ocak | Değişiklik isteği issue'ları açılır |
| Issue + 48 saat | Pull request |
| Final dönemi | Not ilanı, OBS üzerinden |

---

## 10. Sık Sorulan Sorular

**Laboratuvar bilgisayarında çalışabilir miyim?**
Projeyi kendi bilgisayarınızda yürütün. .NET 10 SDK gerekir; ilk
`dotnet test` paketleri internetten indirir.

**Şablondaki dosyaları değiştirebilir miyim?**
Evet. `Menu.cs` ve `Program.cs` zaten sizin dolduracağınız dosyalar.
`JsonDepo<T>`'yi değiştirmeniz gerekmez ama isterseniz geliştirebilirsiniz.
Proje adlarını değiştirmeyin.

**Bir kayıt tipim için JSON dosyası yerine başka bir saklama yolu
kullanabilir miyim?**
`IDepo<T>`'nin başka bir gerçekleştirmesini `Altyapi`'ye yazın ve
`Program.cs`'te onu kaydedin. Servisleriniz değişmez; bağımlılık
enjeksiyonunun amacı budur.

**Testlerim dosyaya yazmalı mı?**
Servis testleri için hayır. Bellekte duran sahte bir `IDepo<T>` yazın ve
servise onu verin; `JsonDepoTestleri` dosyaya yazmanın nasıl sınandığını
gösteriyor.

**Repoyu public yapmak istemiyorum.**
Gerçek adınızın görünmesini istemiyorsanız yalnızca kullanıcı adınızla
katılabilirsiniz (2.2). Başka bir çekinceniz varsa son teslimden önce
öğretim elemanıyla konuşun.

**Bütünlemeye kalırsam projem ne olur?**
Bütünleme sınavı **yazılıdır** (kod okuma, 100 puan) ve projenin yerine
geçer. Projenizi geliştirmeye devam edebilirsiniz ama bütünleme notunu
etkilemez. Bütünlemenin kapsamı dönemin bütün konularıdır.

**Değişiklik isteği tarihlerinde müsait değilim.**
4–6 Ocak tarihlerini şimdiden ayırın. Mazeretinizi issue açılmadan önce
bildirin.
