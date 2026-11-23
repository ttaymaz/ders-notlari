# 11. Hafta — Örnek Kodlar ve Laboratuvar

**Asenkron programlama:** `await` satırında ne olduğu, birden çok işi
birlikte beklemek, iptal ve asenkron akışlar. Taşıyıcı yine bir **graf**,
bu kez kenarları uzakta: bir kütüphane kataloğunun "bunu okuyanlar şunları
da okudu" bağlantıları. Her bağlantıyı öğrenmek için kataloğa sormak
gerekiyor ve her soru zaman alıyor. Gezinti 5. haftanın kuyruğuyla yapılan
genişliğine gezintidir (BFS).

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-await-sirasi.cs` | `await` nerede durur; görev sıcak başlar; hata görevin içinde kalır | `01-await-akisi.svg` |
| `02-sirali-ve-eszamanli.cs` | Art arda beklemek, `Task.WhenAll`, `Task.WhenAny`, birden çok hata | — |
| `03-iptal.cs` | `CancellationTokenSource`, `CancelAfter`, iptal işbirliği, birleşik token | — |
| `04-asenkron-gezinti.cs` | **Taşıyıcı:** grafı sıralı ve düzey düzey gezmek | `02-katalog-grafi.svg` |
| `05-asenkron-akislar.cs` | `IAsyncEnumerable<T>`, `await foreach`, erken bırakma, `WithCancellation` | `02-katalog-grafi.svg` |
| `lab/` | **Laboratuvar:** 16 hazır test, dört görev | — |

## Çalıştırma

```
dotnet run 01-await-sirasi.cs
```

Bu haftanın dosyalarında `#:package` satırı yok; hepsi yalnızca .NET'in
kendisiyle çalışır. `02`–`05` dosyaları beklemeyi `Task.Delay` ile taklit
eder. Süreler makineye göre birkaç milisaniye oynar; dosyalar süreyi bir
gecikmenin katına yuvarlayarak yazar, sonuç değişmez.

### .NET 9 kullanıyorsanız

**Tek dosyalık örnekler:** Dosyayı bir konsol projesinin `Program.cs`
dosyasına kopyalayın:

```
dotnet new console -o Deneme
copy 04-asenkron-gezinti.cs Deneme\Program.cs
dotnet run --project Deneme
```

**Laboratuvar:** iki `.csproj` dosyasında `net10.0` → `net9.0`; çözüm
dosyası yerine test projesini verin: `dotnet test AsyncLab.Testler`.

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün örnekler ve
laboratuvar C# 13 ile de denendi; çıktılar ve test sonuçları aynı.

## Laboratuvar

```
cd lab
dotnet test
```

İlk çalıştırmada 16 testten **14'ü kırmızı**, 2'si yeşildir. Yeşil olan
ikisi hazır `GezSiraliAsync` metodunu sınar; onu öteki görevlerde örnek
olarak kullanın.

| Görev | Dosya → metot | Ne yapar | Test |
| :---: | ------------- | -------- | :--: |
| 1 | `AsyncLab/Gezgin.cs` → `GezDuzeyDuzeyAsync` | Aynı düzeydeki soruları birlikte başlat, `Task.WhenAll` ile bekle; sıra sıralı gezintiyle aynı | 4 |
| 2 | aynı metot | İptal: token her soruya gitsin; yeni düzeye başlamadan önce iptale bak | 3 |
| 3 | `AsyncLab/Gezgin.cs` → `KesfetAsync` | Asenkron akış: kitapları teker teker ver, erken bırakılınca geri kalanı sorma | 4 |
| 4 | `AsyncLab/Gezgin.cs` → `HatalariAtlayarakGezAsync` | `KatalogHatasi` veren kitabı atla ve listele; iptali yutma | 3 |

**Testleri değiştirmeyin.** Kırmızı bir testin önce adını, sonra
`Expected` / `Actual` satırlarını okuyun.

Testler gerçek bir katalog yerine `AsyncLab.Testler/SahteKatalog.cs`
içindeki sahte kataloğu kullanır. Sahte katalog her soruda 10 ms bekler ve
kendisine ne olduğunu kaydeder: kaç soru soruldu (`Cagri`), hangi kitaplar
(`Sorulanlar`), en çok kaç soru aynı anda sürdü (`EnCokAyniAnda`), token'sız
soru geldi mi (`TokensizCagri`). Bir test kırmızıysa bu kayda bakın.

İpuçları:

- **1. görev:** `04-asenkron-gezinti.cs` içindeki `DuzeyDuzeyAsync`'e bakın.
  Bütün soruları **önce başlatın**, sonra bekleyin; bir döngü içinde tek tek
  `await` ederseniz `Ayni_duzeydeki_sorular_birlikte_surer` kırmızı kalır
- **3. görev:** Metodun imzası değişecek: `async` eklenecek ve token
  parametresine bir öznitelik gelecek (`05-asenkron-akislar.cs`). Kitabı
  vermeden önce bağlantılarını sorarsanız erken bırakma testi kırmızı kalır
- **4. görev:** `Task.WhenAll` birden çok hatadan yalnızca birini fırlatır
  ve o düzeyin bütün cevaplarını kaybettirir. Hatayı **her kitabın kendi
  sorusunda** yakalayın: soruyu saran küçük bir yerel `async` fonksiyon
  yazın. Yalnızca `KatalogHatasi`'nı yakalayın

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01` dosyasının 1. bölümünde `Task t1 = Ornek.BekleyenAsync();` satırını
  `await Ornek.BekleyenAsync();` yapın ve `await t1;` satırını silin. Beş
  harfin sırası ne olur?
- `01` dosyasında `HemenBitenAsync` içindeki `Task.CompletedTask` yerine
  `Task.Delay(100)` yazın. 2. bölümün çıktısı nasıl değişir? Neden?
- `01` dosyasında `HataliAsync`'in başındaki `async` kelimesini silin,
  dönüş tipini `Task` bırakın ve `await Task.Delay(10);` yerine
  `return Task.Delay(10);` yazın. İstisna şimdi hangi satırda fırlar? `try`
  bloğu onu yakalar mı?
- `02` dosyasının 2. bölümünü, üç görevi art arda `await` eden biçime
  çevirin. Süre kaç gecikme olur? "bitti" satırlarının sırası değişir mi?
- `03` dosyasında `TokenAlanAsync` içindeki `Task.Delay(100, iptal)`
  çağrısından `iptal` argümanını silin. 3. bölüm ne yazar?
- `03` dosyasının 2. bölümünde `ThrowIfCancellationRequested` satırını
  silin ve çalıştırın. Ne olur? (Ctrl+C ile durdurun.)
- `04` dosyasında `DuzeyDuzeyAsync` içindeki `.ToList()` çağrısını silin.
  Sonuç değişir mi? Neden?
- `05` dosyasında `yield return baslik;` satırını `foreach` döngüsünün
  **altına** taşıyın. 2. bölümde katalog kaç kez sorulur?
- `05` dosyasında `[EnumeratorCancellation]` özniteliğini silin. Derleyici
  ne der? 3. bölüm ne yazar?

## Çıktılar

Denemeden önce bakmayın. .NET 10 çalışma zamanı, Windows'ta ölçüldü.

<details>
<summary>01-await-sirasi.cs</summary>

```
=== 1 === (await nerede durur?)
A
B
C
D
E

=== 2 === (tamamlanmış görevi beklemek)
1
2
3
4

=== 3 === (görev ve yineleyici)
görev başladı
ikisi de oluşturuldu
görev bitti
dizi başladı

=== 4 === (sonuç döndüren görev)
200000'den küçük asal: 17984

=== 5 === (istisna görevin içinde kalır)
çağrı döndü, durum: Faulted
await'te yakalandı: adet
```

`await Ornek.BekleyenAsync();` yazılırsa sıra `A B D C E` olur: çağıran
artık görevi alır almaz bekliyor. `Task.Delay(100)` yazılırsa 2. bölüm
`1 2 4 3` olur: beklenen görev henüz bitmemiştir, metot askıya alınır.
`async` silinirse istisna **çağrı satırında**, `try` bloğunun dışında
fırlar ve program `Unhandled exception` ile sonlanır.
</details>

<details>
<summary>02-sirali-ve-eszamanli.cs</summary>

```
=== 1 === (sıralı: await, await, await)
  bitti: Huzur
  bitti: Aylak Adam
  bitti: Beş Şehir
«Huzur», «Aylak Adam», «Beş Şehir»
süre ≈ 3 gecikme

=== 2 === (birlikte: önce başlat, sonra Task.WhenAll)
  bitti: Aylak Adam
  bitti: Beş Şehir
  bitti: Huzur
«Huzur», «Aylak Adam», «Beş Şehir»
süre ≈ 3 gecikme

=== 3 === (ilk gelen: Task.WhenAny)
  bitti: yedek sunucu
kazanan: «yedek sunucu»
süre ≈ 1 gecikme
asıl sunucu bitti mi? False
  bitti: asıl sunucu

=== 4 === (iki görev hata verirse)
catch'e gelen: Aylak Adam getirilemedi
görevdeki hata sayısı: 2
```

1. ve 2. bölüm aynı süreyi yazıyor ama sebep farklı: birincide 1 + 1 + 1,
ikincide en uzun olan 3. 2. bölüm art arda `await` ile yazılırsa süre
3 + 1 + 2 = **6** gecikme olur ve "bitti" satırları verilen sırayla gelir.
4\. bölümde `catch`'e **ilk hata veren** görevin hatası geliyor; listede
ilk sırada olan `Huzur` değil. İki görev aynı anda hata verirse hangisinin
geleceği değişir (denendi); bütün hatalar `InnerExceptions` içinde.
</details>

<details>
<summary>03-iptal.cs</summary>

```
=== 1 === (süre dolunca iptal: CancelAfter)
yakalanan tip: TaskCanceledException
OperationCanceledException mı? True

=== 2 === (işlemciye bağlı işte iptal: ThrowIfCancellationRequested)
hesap yarıda kesildi (OperationCanceledException)

=== 3 === (iptal işbirliğidir)
token almayan iş: 5/5 adım, iptal istendi mi? True
token alan iş: 1/5 adımda durdu

=== 4 === (iki sebep, tek token: CreateLinkedTokenSource)
kullanıcı vazgeçti
zaman aşımı
```

`Task.Delay`'den `iptal` silinirse token alan iş de beş adımı bitirir ve
istisna fırlamaz; 3. bölümün ikinci satırı hiç yazılmaz. Token'ı almak
yetmez, iletmek gerekir. `ThrowIfCancellationRequested` silinirse 2. bölüm
iptal isteğini hiç görmez ve 50 milyar adımlık döngüyü sürdürür.
</details>

<details>
<summary>04-asenkron-gezinti.cs</summary>

```
=== 1 === (sıralı gezinti)
Huzur → Tutunamayanlar → Saatleri Ayarlama Enstitüsü → Aylak Adam → Tehlikeli Oyunlar → Beş Şehir → Anayurt Oteli
çağrı: 7 · en çok aynı anda: 1 · süre ≈ 7 gecikme

=== 2 === (düzey düzey: Task.WhenAll)
Huzur → Tutunamayanlar → Saatleri Ayarlama Enstitüsü → Aylak Adam → Tehlikeli Oyunlar → Beş Şehir → Anayurt Oteli
çağrı: 7 · en çok aynı anda: 3 · süre ≈ 3 gecikme

=== 3 === (aynı zaman aşımı: 5 gecikme)
sıralı: yetişmedi
düzey düzey: yetişti, 7 kitap
```

`.ToList()` silinse de sonuç aynıdır: `Task.WhenAll` kendisine verilen
diziyi önce baştan sona gezer, bu sırada bütün görevler başlar. Liste
yalnızca niyeti açık eder.
</details>

<details>
<summary>05-asenkron-akislar.cs</summary>

```
=== 1 === (akışı oluşturmak)
akış hazır, katalog çağrısı: 0

=== 2 === (ilk dört kitap)
tüketici: Huzur
  katalog: Huzur soruluyor
tüketici: Tutunamayanlar
  katalog: Tutunamayanlar soruluyor
tüketici: Saatleri Ayarlama Enstitüsü
  katalog: Saatleri Ayarlama Enstitüsü soruluyor
tüketici: Aylak Adam
4 kitap alındı, katalog çağrısı: 3

=== 3 === (tüketici iptal ediyor: WithCancellation)
tüketici: Huzur
  katalog: Huzur soruluyor
tüketici: Tutunamayanlar
  katalog: Tutunamayanlar soruluyor
iptal edildi: 2 kitap, katalog çağrısı: 2
```

`yield return` döngünün altına taşınırsa her kitap, bağlantıları
sorulduktan sonra verilir: "katalog" satırı "tüketici" satırından önce gelir
ve dört kitap için katalog **4** kez sorulur. `[EnumeratorCancellation]`
silinirse derleyici `CS8425` uyarısı verir; 3. bölüm iptali hiç görmez,
yedi kitabın hepsini yazar ve "iptal edildi" satırı gelmez.
</details>

<details>
<summary>lab/ — dotnet test (ilk çalıştırma)</summary>

```
Başarısız! - Başarısız: 14, Başarılı: 2, Atlanan: 0, Toplam: 16
```

Dört görev bitince `Başarılı! - Başarısız: 0, Başarılı: 16`.
</details>
