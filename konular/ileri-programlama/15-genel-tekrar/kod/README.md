# 15. Hafta — Dönem Sonu Tekrarı

Dönemin ikinci yarısının (9–14. haftalar) üç provası. Biçimleri 7. konunun
ara sınav tekrarıyla aynı; ilk yarı için oradaki dosyaları kullanın. Bu
haftanın laboratuvarı yok; `03` onun yerini tutuyor.

| Dosya | Ne için? |
| ----- | -------- |
| `01-cikti-tahmini.cs` | On bölüm, her biri 9–14. haftalardan birine dayanır: çıktıyı **önce kâğıda** yazın |
| `02-derlenir-mi.cs` | Sekiz `//?` satırı: derlenir mi, uyarı mı verir, derlenmez mi? Neden? |
| `03-yazma-provasi.cs` | Altı kısa görev (dokuz denetim); kendini denetler |

## Çalıştırma

```
dotnet run 01-cikti-tahmini.cs
```

Dosyalar paketsizdir. `01` her çalıştırmada aynı çıktıyı verir.

**`02` nasıl çalışılır:** dosya olduğu haliyle derlenir. Bir `//?`
satırının başındaki işareti silin, çalıştırın, derleyicinin cevabını
kâğıttaki tahmininizle karşılaştırın ve işareti geri koyun. Aynı anda
yalnızca **bir** satırı açın.

**`03` nasıl çalışılır:** önce her görevi kâğıda yazın, sonra dosyaya
geçirip çalıştırın. Denetim her görev için `[TAMAM]`, `[YANLIŞ]`,
`[HATA]` ya da `[YAPILMADI]` yazar. Denetim kodunu değiştirmeyin.
Yanlış bir çözüm denetimi asılı bırakmaz: 2. görevin beklemesi en çok
2 saniye sürer, 3. görevin kaynağı 1000 elemandan fazla okunursa durur.

### .NET 9 kullanıyorsanız

Dosyayı bir konsol projesinin `Program.cs` dosyasına kopyalayın:

```
dotnet new console -o Deneme
copy 03-yazma-provasi.cs Deneme\Program.cs
dotnet run --project Deneme
```

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Üç dosya C# 13 ile
de denendi; çıktılar ve `02`'nin bütün soruları aynı sonucu verdi.

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01` dosyasının 1. bölümünde `when (e.Message == "404")` yerine
  `"500"` yazın. İki satır nasıl değişir?
- `01` dosyasının 3. bölümünde son satırdaki çağrıların sırasını
  `gecici, tekil, gecici, tekil, tekil` yapın. Beş sayı ne olur?
- `01` dosyasının 6. bölümünde kanalın kapasitesini `2` yapın. Ne değişir?
- `03` dosyasının 2. görevini `Task.WhenAll` ile yazın. Denetim ne der?
  Neden?
- `03` dosyasının 3. görevinde önce bütün kaynağı bir listeye toplayıp
  sonra çiftleri verin. Hangi denetim kırmızı olur?
- 7\. konunun üç provasını da yeniden yapın: ilk yarı da sınavda.

## Çıktılar

Denemeden önce bakmayın. .NET 10 çalışma zamanı, Windows'ta ölçüldü.

<details>
<summary>01-cikti-tahmini.cs</summary>

```
=== 1 === (9. hafta: catch, when, finally)
bulunamadı iç-finally 
iç-finally dış-catch 

=== 2 === (9. hafta: using ve Dispose sırası)
aç A
aç B
gövde
kapat B
kapat A

=== 3 === (10. hafta: tekil fabrika, kapanım)
1 1 2 1 3

=== 4 === (11. hafta: await nerede durur?)
1 2 3 4 5

=== 5 === (11. hafta: WhenAll sırası)
  bitti 1
  bitti 2
  bitti 3
  dizi: 3 1 2

=== 6 === (12. hafta: kanal, kapasite 1)
ikinci yazma hemen bitti mi? False
okundu 10

=== 7 === (13. hafta: tip ağacında yukarı)
RafBulunamadi KeyNotFoundException SystemException Exception Object 

=== 8 === (13. hafta: öznitelikler)
Etiket sayısı: 2
Etiketler: aylik, rapor

=== 9 === (14. hafta: dizi aralığı ve Span)
1 2 30 4

=== 10 === (14. hafta: List<T> kapasitesi)
Count 5, Capacity 8
```

Bölüm bölüm kısa açıklama:

1. 500 için filtre yanlış: iç `catch` atlanır, `finally` yine çalışır,
   istisna dıştaki `catch`'e gider. `"500"` yazılınca iki satırın yeri
   değişir: `iç-finally dış-catch`, sonra `bulunamadı iç-finally`.
2. `using` kaynakları **ters** sırayla kapatır.
3. `tekil` ilk çağrıda bir fiş kurar ve hep onu döndürür (`??=`, 4.
   haftanın kapanımı); `gecici` her seferinde yenisini. Sıra
   `gecici, tekil, gecici, tekil, tekil` olunca: `1 2 3 2 2`.
4. `YazAsync` ilk bekleyen `await`'e kadar çağıranın içinde çalışır.
5. Bitiş sırası 1, 2, 3; ama `WhenAll` sonuçları **verilen** sırayla
   döndürür: 3, 1, 2.
6. Kapasite 1: kanal doluyken `TryWrite` beklemez, `false` döner; 20
   hiç yazılmaz. Kapasite 2 ile `True` ve iki okuma: `10`, `20`.
7. `BaseType` ile yapraktan köke.
8. `AllowMultiple = true` aynı etiketin iki kez yapıştırılmasına izin
   verir.
9. `d[1..3]` bir **kopya**: `kopya[0] = 20` diziyi değiştirmez. `AsSpan`
   bir **pencere**: `pencere[1] = 30` dizinin 2. indeksini değiştirir.
10. Kapasite 0 → 4 → 8: beşinci eleman ikinci büyümeyi tetikler.
</details>

<details>
<summary>02-derlenir-mi.cs — olduğu haliyle ve soru soru</summary>

Olduğu haliyle:

```
dene
kaydedildi
liste
7
```

| Soru | Satır | Sonuç | Neden |
| :-: | --- | --- | --- |
| 1 | `catch (ArgumentException)` genelden sonra | **Derlenmez** (`CS0160`) | `Exception` onu zaten yakalıyor; bu blok asla çalışmaz |
| 2 | `Task.Delay(10);` `await`'siz | **Uyarı** (`CS4014`) | Görev başlar ama beklenmez; hatası kaybolur |
| 3 | `async void KaydetAsync()` | **Derlenir**, uyarı yok | Ama istisnası yakalanamaz, beklenemez, sınanamaz |
| 4 | `lock` içinde `await` | **Derlenmez** (`CS1996`) | Kilit iş parçacığına bağlı; `await` sonrası başka iş parçacığında devam edilebilir |
| 5 | `[Komut]` bir sınıfa | **Derlenmez** (`CS0592`) | `[AttributeUsage]` yalnızca metotlara izin veriyor |
| 6 | `[Obsolete]` metodu çağırmak | **Uyarı** (`CS0618`) | Öznitelik derleyiciye "bunu kullanma" der; mesajı uyarıda görünür |
| 7 | `Span<int>`'yi lambda içinde kullanmak | **Derlenmez** (`CS8175`) | `Span` yalnızca stack'te yaşar; lambdanın yakaladığı değişken heap'e taşınır |
| 8 | `Span<char>` bir sınıf alanı | **Derlenmez** (`CS8345`) | Sınıf nesnesi heap'tedir; `Span` orada duramaz |
</details>

<details>
<summary>03-yazma-provasi.cs — ilk çalıştırma</summary>

```
[YAPILMADI] 1. TryBol: 7 / 2 = 3
[YAPILMADI] 1. TryBol: sıfıra bölmede false ve sonuç 0
[YAPILMADI] 2. IlkBitenAsync: önce biten kazanır
[YAPILMADI] 3. CiftlerAsync: 1..6 → 2 4 6
[YAPILMADI] 3. CiftlerAsync: erken bırakınca kaynak durur
[YAPILMADI] 4. Frekans: paralel sayımda kayıp yok
[YAPILMADI] 5. KomutAdi: etiket varsa etiketin adı, yoksa metodun adı
[YAPILMADI] 6. RakamToplami: "TR-813-07" → 19
[YAPILMADI] 6. RakamToplami: bellek ayırmaz

0/9
```

Altı görev bitince `9/9`. `Task.WhenAll` ile yazılmış 2. görev:
`[HATA] ... 2 saniyede bitmedi: iki görevin de bitmesini mi
bekliyorsunuz?` Önce listeye toplayan 3. görev: `[HATA] ... kaynağın
tamamı okunmaya çalışıldı; akış ertelenmiş değil`.
</details>
