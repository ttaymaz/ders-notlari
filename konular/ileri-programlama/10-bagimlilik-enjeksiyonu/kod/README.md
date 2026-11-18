# 10. Hafta — Örnek Kodlar ve Laboratuvar

Üç yarı: **bağımlılık enjeksiyonu** (önce kendi kabımız, sonra .NET'inki),
**yapılandırma** ve **günlükleme**. Taşıyıcı bir **graf**: bir DI kabı,
servisler arasındaki bağımlılık grafını gezen bir programdır. Alan, dönem
boyunca kullandığımız kütüphane: ödünç, iade, gecikme cezası.

Bu haftanın konuları **dönem projesi şablonunun** üzerine kurulduğu konular.
Şablondaki `Program.cs`, `IOptions<T>`, `ILogger<T>` ve servis testleri
burada adım adım açılıyor.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-bagimliligi-disaridan-almak.cs` | Saati kurucudan almak: `TimeProvider` ve sahte saat | — |
| `02-kendi-di-kabimiz.cs` | **Taşıyıcı:** 50 satırlık DI kabı; grafı gezmek, döngü bulmak | `01-bagimlilik-grafi.svg` |
| `03-hazir-di-kabi.cs` | `ServiceCollection`; tekil, kapsamlı, geçici; doğrulama | `01-bagimlilik-grafi.svg` |
| `04-yapilandirma.cs` | Birden çok kaynak, sonra gelen kazanır; `IOptions<T>` | — |
| `05-gunlukleme.cs` | `ILogger`, düzeyler, kategoriler, mesaj şablonu | — |
| `odunc/` | **Örnek çözüm:** ödünç servisi + 11 test (bellekte depo, sahte saat) | `02-katmanlar.svg` |
| `lab/` | **Laboratuvar:** 15 hazır test, dört görev | — |

## Çalıştırma

```
dotnet run 02-kendi-di-kabimiz.cs
```

`03`, `04` ve `05` dosyalarının başında `#:package` satırları var; ilk
çalıştırmada paketler NuGet'ten iner, internet gerekir.

Çözümler için klasöre girip `dotnet test`:

```
cd odunc
dotnet test
```

### .NET 9 kullanıyorsanız

**Tek dosyalık örnekler:** Dosyayı bir konsol projesinin `Program.cs`
dosyasına kopyalayın, `#:package` satırlarını silin ve her biri için paketi
projeye ekleyin:

| Dosyada | .NET 9 projesinde |
| ------- | ----------------- |
| `#:package Microsoft.Extensions.DependencyInjection@10.0.14` | `dotnet add Deneme package Microsoft.Extensions.DependencyInjection --version 10.0.14` |
| `#:package Microsoft.Extensions.Configuration.Binder@10.0.14` | `dotnet add Deneme package Microsoft.Extensions.Configuration.Binder --version 10.0.14` |
| (öteki `#:package` satırları) | aynı kalıp: paket adı ve sürüm |

```
dotnet new console -o Deneme
copy 03-hazir-di-kabi.cs Deneme\Program.cs
dotnet add Deneme package Microsoft.Extensions.DependencyInjection --version 10.0.14
dotnet run --project Deneme
```

10.0.14 sürümlü `Microsoft.Extensions` paketleri .NET 9 projesinde de
çalışır.

**Çözümler** (`odunc/`, `lab/`): iki `.csproj` dosyasında `net10.0` →
`net9.0`; çözüm dosyası yerine test projesini verin: `odunc` içinde
`dotnet test Testler`, `lab` içinde `dotnet test DiLab.Testler`.

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün örnekler ve iki
çözüm C# 13 ile de denendi; çıktılar ve test sonuçları aynı.

## Laboratuvar

```
cd lab
dotnet test
```

İlk çalıştırmada 15 testten **13'ü kırmızı**, 2'si yeşildir. Yeşil olan
ikisi hazır kodu (geçici kayıt, döngü tespiti) sınar.

| Görev | Dosya | Ne yapar | Test |
| :---: | ----- | -------- | :--: |
| 1 | `DiLab/Kap.cs` → `KaydetTekil` | Tekil ömür: fabrika yalnızca ilk çözmede çalışır | 3 |
| 2 | `DiLab/Kap.cs` → `Coz` | Kayıtsız tipin mesajında, onu **isteyen** servis de yazsın | 2 |
| 3 | `DiLab/Kap.cs` → `Dogrula` | Bütün kayıtları önceden çözmeyi dener, sorunları listeler | 3 |
| 4 | `DiLab/HatirlatmaServisi.cs` | Son tarihi yaklaşan ödünçler; sahte saat ve bellekte depoyla sınanır | 5 |

**Testleri değiştirmeyin.** Kırmızı bir testin önce adını, sonra
`Expected` / `Actual` satırlarını okuyun.

4\. görevin testleri (`HatirlatmaServisiTestleri.cs`) dönem projesinde
yazacağınız servis testlerinin örneğidir: servisin bağımlılıklarının yerine
bellekte bir depo, sahte bir saat ve elle yazılmış bir ayar veriliyor.

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01` dosyasında `GecikmeHesabi.Ceza` içinde `saat.GetUtcNow()` yerine
  `DateTime.UtcNow` kullanın. Üç satır ne yazar? Bu sınıfı geçen ayın bir
  günüyle nasıl deneyebilirsiniz?
- `02` dosyasında `IadeServisi` kaydını en başa, `TimeProvider` kaydının
  önüne taşıyın. Program hâlâ çalışır mı? Neden kayıt sırası önemli değil?
- `03` dosyasının 5. bölümünde `BuildServiceProvider(...)` çağrısındaki
  seçenekleri silin. Ne yazar? Hata kayboldu mu, yoksa gizlendi mi?
- `03` dosyasında `AddSingleton<Ayarlar>` yerine `AddTransient<Ayarlar>`
  yazın. 1. bölümün ilk satırı ne olur?
- `04` dosyasını ortam değişkeni ve komut satırıyla çalıştırın. Önce
  tahmin edin: ikisi birden verilirse hangisi kazanır?

  PowerShell:

  ```
  $env:KUTUPHANE_Ceza__GunlukUcret = "8"
  dotnet run 04-yapilandirma.cs -- --Ceza:GunlukUcret=5
  Remove-Item Env:KUTUPHANE_Ceza__GunlukUcret
  ```

  Bash:

  ```
  KUTUPHANE_Ceza__GunlukUcret=8 dotnet run 04-yapilandirma.cs -- --Ceza:GunlukUcret=5
  ```

- `05` dosyasında `SetMinimumLevel(LogLevel.Information)` yerine
  `LogLevel.Warning` yazın. Hangi satırlar kaybolur? `Kutuphane.Odunc`'un
  `dbug` satırı neden hâlâ yazıyor?
- `odunc/Cekirdek/OduncServisi.cs` içinde `Math.Min(...)` ile yapılan tavan
  sınırını kaldırın ve `dotnet test` çalıştırın. Hangi test kırılır?
  `Expected` ve `Actual` ne yazar?
- `odunc/` için **kendi testinizi** yazın: bir üye iki kitap aldı, birini
  iade etti; üçüncü kitabı alabilir mi (`EnFazlaKitap = 2`)?

## Çıktılar

Denemeden önce bakmayın. .NET 10 çalışma zamanı, Windows'ta ölçüldü.

<details>
<summary>01-bagimliligi-disaridan-almak.cs</summary>

```
bugün 2026-11-19 → ceza 0 TL
bugün 2026-11-25 → ceza 15 TL
bugün 2027-01-30 → ceza 60 TL
gerçek saatle de kurulabiliyor: GecikmeHesabi
```

`DateTime.UtcNow` kullanılırsa üç satır aynı sayıyı yazar: üçü de
"bugün"ün gerçek tarihine bakar, verilen saati görmez.
</details>

<details>
<summary>02-kendi-di-kabimiz.cs</summary>

```
=== 1 === (bağımlılık grafı)
IadeServisi
  OduncDefteri
  GecikmeHesabi
    TimeProvider
    CezaKurali
hazır: IadeServisi

=== 2 === (döngü)
Fatura
  Hatirlatici
Döngüsel bağımlılık: Fatura → Hatirlatici → Fatura

=== 3 === (kayıtsız tip)
Fatura kayıtlı değil.
```

Kayıt sırası değişse de çıktı aynıdır: fabrikalar kayıt anında değil,
`Coz` çağrıldığında çalışır.
</details>

<details>
<summary>03-hazir-di-kabi.cs</summary>

```
=== 1 === (tekil ve geçici)
Ayarlar#1 Ayarlar#1
Fis#1 Fis#2

=== 2 === (kapsamlı)
kapsam 1: Sepet#1 Sepet#1
kapsam 2: Sepet#2

=== 3 === (açık jenerik)
BellekDepo`1
True

=== 4 === (kayıtsız servis)
GetService: null
GetRequiredService: InvalidOperationException

=== 5 === (tekil, kapsamlıya bağımlıysa)
kurulamadı: 1 hata
Error while validating the service descriptor 'ServiceType: Kasa Lifetime: Singleton ImplementationType: Kasa': Cannot consume scoped service 'Sepet' from singleton 'Kasa'.
```

Seçenekler silinirse 5. bölüm `kap kuruldu` yazar. Hata kaybolmadı:
tekil `Kasa`, ilk istendiği anda bir `Sepet` alır ve onu ömür boyu tutar;
o `Sepet` hiçbir kapsamın `Sepet`'i değildir. Hata artık sessizdir.
</details>

<details>
<summary>04-yapilandirma.cs</summary>

```
=== 1 === (anahtarla okumak)
Ceza:GunlukUcret = 3
Odunc:Gun        = 14
Olmayan:Anahtar  = (null)

=== 2 === (bir bölümü sınıfa bağlamak)
3 TL/gün, tavan 60 TL

=== 3 === (IOptions<T>: servise ayar vermek)
CezaYazici: 10 gün gecikme = 30 TL
```

Yalnızca komut satırıyla (`--Ceza:GunlukUcret=5`): 5, `5 TL/gün`, 50 TL.
Yalnızca ortam değişkeniyle (8): 8, `8 TL/gün`, 60 TL (tavan). İkisi
birden: **5**. Komut satırı en son eklenen kaynak olduğu için kazanır.
</details>

<details>
<summary>05-gunlukleme.cs</summary>

```
dbug: Kutuphane.Odunc[0] Ödünç isteği geldi
info: Kutuphane.Odunc[0] Ece üyesi Huzur kitabını aldı
warn: Kutuphane.Odunc[0] Can üyesinin iadesi 6 gün gecikti
info: Kutuphane.Rapor[0] Rapor hazır: 42 ödünç
fail: Kutuphane.Rapor[0] Rapor üretilemedi System.InvalidOperationException: Depo dosyası okunamadı.    at ...
```

`Kutuphane.Rapor`'un `dbug` satırı yazılmaz. En düşük düzey Warning
yapılırsa yalnızca `Kutuphane.Rapor`'un `info` satırı kaybolur:
`Kutuphane.Odunc` için ayrıca Debug denmiş, **kategoriye özel kural genel
kuralı ezer.** `appsettings.json` dosyasındaki `"Logging"` bölümü de böyle
çalışır.
</details>

<details>
<summary>odunc/ — dotnet test</summary>

```
Başarılı!  - Başarısız: 0, Başarılı: 11, Atlanan: 0, Toplam: 11
```

Tavan kaldırılırsa yalnızca `Ceza_tavani_asilmaz` kırılır:
`Expected: Basarili { Deger = 60 }`, `Actual: Basarili { Deger = 258 }`
(86 gün × 3 TL).
</details>
