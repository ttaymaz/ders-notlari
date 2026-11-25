# 12. Hafta — Örnek Kodlar ve Laboratuvar

**Eşzamanlılık:** birden çok iş aynı veriye aynı anda dokunduğunda ne
bozulur ve nasıl korunur. Yarış durumu, `Interlocked`, `lock`,
kilitlenme, `SemaphoreSlim`, eşzamanlı koleksiyonlar. Taşıyıcı yine
**kuyruk**, bu kez iki ucunda iki ayrı görev: `Channel<T>` ile
üretici–tüketici.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-yaris-durumu.cs` | Kaybolan artırma; `Interlocked`; `lock`; kontrol et, sonra yap | `01-kayip-guncelleme.svg` |
| `02-kilitler.cs` | Kilit bir kuralı korur; kilitlenme; `await` ile kilit: `SemaphoreSlim(1, 1)` | — |
| `03-esanli-koleksiyonlar.cs` | `ConcurrentDictionary.AddOrUpdate`; iki ayrı işlem tuzağı; `ConcurrentQueue` | — |
| `04-sinirlamak.cs` | Aynı anda en çok n iş: `SemaphoreSlim(n)`, `Parallel.ForEachAsync` | — |
| `05-kanallar.cs` | **Taşıyıcı:** sınırlı kanal, bir ve iki tüketici | `02-kanal.svg` |
| `lab/` | **Laboratuvar:** 20 hazır test, dört görev | — |

## Çalıştırma

```
dotnet run 01-yaris-durumu.cs
```

Bu haftanın dosyalarında `#:package` satırı yok. `01` dosyası birkaç
saniye sürer: 2000 kez yarış kurar.

**Bu haftanın çıktıları yarışa bağlı.** Korumasız kodun sonucu her
çalıştırmada değişir; dosyalar bu yüzden sayının kendisini değil "kayıp var
mı?" gibi soruların cevabını yazar. Yarışın ortaya çıkması için makinenin
birden çok çekirdeği olmalı; tek çekirdekli bir sanal makinede korumasız
sayaç bile doğru çıkabilir. Süreye bağlı satırlar bir gecikmenin katına
yuvarlanır.

### .NET 9 kullanıyorsanız

**Tek dosyalık örnekler:** Dosyayı bir konsol projesinin `Program.cs`
dosyasına kopyalayın:

```
dotnet new console -o Deneme
copy 05-kanallar.cs Deneme\Program.cs
dotnet run --project Deneme
```

**Laboratuvar:** iki `.csproj` dosyasında `net10.0` → `net9.0`; çözüm
dosyası yerine test projesini verin: `dotnet test EsLab.Testler`.

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün örnekler ve
laboratuvar C# 13 ile de denendi; çıktılar ve test sonuçları aynı.

## Laboratuvar

```
cd lab
dotnet test
```

İlk çalıştırmada 20 testten **18'i kırmızı**, 2'si yeşildir. Yeşil olan
ikisi hazır `KilitliSayac`'ı sınar; onu örnek olarak kullanın.

| Görev | Dosya → üye | Ne yapar | Test |
| :---: | ----------- | -------- | :--: |
| 1 | `EsLab/Sayaclar.cs` → `OduncIstatistigi` | Kitap başına ödünç sayısı; aynı anda kayıt, kayıpsız sayma, en çok okunanlar | 3 |
| 2 | `EsLab/Raf.cs` → `OduncVerAsync` | Kontrol ile deftere yazma arasında `await` var; kopyadan fazla verme | 3 |
| 3 | `EsLab/SinirliGezgin.cs` → `GezAsync` | Geçen haftanın düzey düzey gezintisi, aynı anda en çok n soru | 6 |
| 4 | `EsLab/IadeKuyrugu.cs` | Sınırlı kanal, birden çok tüketici, `Complete` | 6 |

**Testleri değiştirmeyin.** Kırmızı bir testin önce adını, sonra
`Expected` / `Actual` satırlarını okuyun.

**Eşzamanlılık testinden bir kez geçmek yetmez.** Korumasız bir kod bu
testlerden çoğu zaman kalır, ama her zaman değil. Testleri birkaç kez
çalıştırın. Test `TimeoutException` ile kırmızıysa bir şey sonsuza dek
bekliyordur: bırakılmayan bir kilit ya da `Complete` edilmemiş bir kanal.
Testler bu durumda asılı kalmasın diye her bekleme en çok 5 saniye sürer.

İpuçları:

- **1. görev:** `03-esanli-koleksiyonlar.cs`. "Önce oku, sonra yaz" iki
  ayrı işlemdir; tek işlemlik bir metot arayın
- **2. görev:** `lock` içinde `await` yazılamaz (`CS1996`).
  `02-kilitler.cs`'in 3. bölümüne bakın; `Release` `finally` içinde
- **3. görev:** Geçen haftanın `DuzeyDuzeyAsync`'i + `04-sinirlamak.cs`.
  `Parallel.ForEachAsync` sonuçları sırayla vermez
- **4. görev:** `05-kanallar.cs`. Tüketicileri kurucuda başlatın; görevlerini
  bir alanda tutun ki `TamamlaAsync` onları bekleyebilsin

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01` dosyasında `GorevSayisi`'ni `1` yapın. 1. bölüm ne yazar? Neden?
- `02` dosyasında `AsenkronSayac.ArtirAsync` içindeki `WaitAsync` ve
  `Release` satırlarını silin. 3. bölümün sonucu kaç olur? Birkaç kez
  çalıştırın.
- `02` dosyasında aynı metotta `SemaphoreSlim` yerine `lock (_kilit) { ... }`
  yazın, `await Task.Yield();` içeride kalsın. Derleyici ne der?
- `03` dosyasının 1. bölümünde `ConcurrentDictionary` yerine `Dictionary`
  kullanın: `sayilar[kitap] = sayilar.GetValueOrDefault(kitap) + 1;`.
  Üç kez çalıştırın. Ne olur?
- `04` dosyasında `new SemaphoreSlim(2)` yerine `3` yazın. 2. bölümde en
  çok kaç soru aynı anda sürer, süre kaç gecikme olur?
- `04` dosyasında `kapi.Release();` satırını silin. Ne olur? (Ctrl+C ile
  durdurun.)
- `05` dosyasında `Channel.CreateBounded<string>(kapasite)` yerine
  `Channel.CreateUnbounded<string>()` yazın. "kanalda en çok" kaç kitap
  bekler?
- `05` dosyasında `kanal.Writer.Complete();` satırını silin. Ne olur?
  (Ctrl+C ile durdurun.)

## Çıktılar

Denemeden önce bakmayın. .NET 10 çalışma zamanı, Windows'ta, 16 çekirdekli
bir makinede ölçüldü.

<details>
<summary>01-yaris-durumu.cs</summary>

```
=== 1 === (korumasız: sayac++)
beklenen 8000000, kayıp var mı? True

=== 2 === (Interlocked.Increment)
sonuç 8000000, doğru mu? True

=== 3 === (lock)
sonuç 8000000, doğru mu? True

=== 4 === (kontrol et, sonra yap: son kopya)
korumasız: 2000 denemenin bazısında kopya -1 oldu mu? True
kilitli: kopya -1 olan deneme sayısı 0
```

`GorevSayisi = 1` olunca 1. bölüm `kayıp var mı? False` yazar: tek görev
varsa yarış da yoktur. 4. bölüm iki görevle çalıştığı için değişmez.
</details>

<details>
<summary>02-kilitler.cs</summary>

```
=== 1 === (kilit bir kuralı korur)
rafta 0, defterde 50, toplam 50

=== 2 === (kilitlenme: iki kilit, ters sıra)
A: ilk kilidi tutuyorum, ikinciyi 300 ms bekledim, alamadım
B: ilk kilidi tutuyorum, ikinciyi 300 ms bekledim, alamadım

=== 3 === (await ile kilit: SemaphoreSlim)
200 artırma, sonuç 200
```

`WaitAsync` ve `Release` silinirse 3. bölüm 200'ün çok altında bir sayı
yazar (bir ölçümde 29); sayı her çalıştırmada değişir. `lock` içinde
`await`: `CS1996`, dosya derlenmez.
</details>

<details>
<summary>03-esanli-koleksiyonlar.cs</summary>

```
=== 1 === (ConcurrentDictionary.AddOrUpdate)
Huzur           100000
Aylak Adam      100000
Beş Şehir       100000
Tutunamayanlar  100000

=== 2 === (iki ayrı işlem: TryGetValue sonra indeksleyici)
toplam 400000 mü? False

=== 3 === (ConcurrentQueue: aynı anda ekleyip almak)
alınan 100000, farklı 100000, kuyrukta kalan 0
```

`Dictionary` ile üç denemenin ikisinde sayılar yanlış çıktı (her kitap
yaklaşık 64–78 bin), birinde program
`System.IndexOutOfRangeException` ile çöktü: sözlüğün iç yapısı bozuldu.
</details>

<details>
<summary>04-sinirlamak.cs</summary>

```
=== 1 === (sınırsız: hepsi birlikte)
çağrı 6 · en çok aynı anda 6 · süre ≈ 1 gecikme

=== 2 === (SemaphoreSlim(2))
çağrı 6 · en çok aynı anda 2 · süre ≈ 3 gecikme

=== 3 === (Parallel.ForEachAsync, MaxDegreeOfParallelism = 2)
çağrı 6 · en çok aynı anda 2 · süre ≈ 3 gecikme
```

`SemaphoreSlim(3)`: en çok 3, süre ≈ 2 gecikme. `Release` silinirse ilk
iki soru kapıdan geçer, kapı bir daha açılmaz; 2. bölüm hiç bitmez.
</details>

<details>
<summary>05-kanallar.cs</summary>

```
=== 1 === (bir üretici, bir tüketici, kapasite 2)
rafa giden sıra: Huzur, Aylak Adam, Beş Şehir, Tutunamayanlar, Anayurt Oteli, Tehlikeli Oyunlar
kanalda en çok 2 kitap bekledi · süre ≈ 6 yerleştirme

=== 2 === (iki tüketici)
rafa giden: 6 kitap, hepsi farklı mı? True
süre ≈ 3 yerleştirme
```

`CreateUnbounded` ile `kanalda en çok 6 kitap bekledi`: üretici beklemeden
hepsini kanala döker. `Complete()` silinirse tüketicinin `await foreach`
döngüsü "başka kitap gelecek mi?" diye sonsuza dek bekler; 1. bölüm başlığını
yazar ve program orada kalır.
</details>

<details>
<summary>lab/ — dotnet test (ilk çalıştırma)</summary>

```
Başarısız! - Başarısız: 18, Başarılı: 2, Atlanan: 0, Toplam: 20
```

Dört görev bitince `Başarılı! - Başarısız: 0, Başarılı: 20`.
</details>
