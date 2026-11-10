# 9. Hafta — Örnek Kodlar ve Laboratuvar

İki yarı: **hata yönetimi** (tek dosyalık dört örnek) ve **birim testi**
(iki çözüm: biri örnek, biri laboratuvar). Taşıyıcı yine 5. konunun yığını
ve kuyruğu; bu kez onları sınıyoruz.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-istisna-akisi.cs` | `try`, `catch`, `finally` sırası; istisna yukarı çıkarken | — |
| `02-koruma-cumleleri.cs` | `ThrowIfNull`, `ThrowIfNegativeOrZero`; argüman mı, durum mu? | `01-istisna-hiyerarsisi.svg` |
| `03-kendi-istisnamiz-ve-filtre.cs` | `BosYapiException`, `when` filtresi, yakalamadan günlüğe yazma | `01-istisna-hiyerarsisi.svg` |
| `04-try-kalibi-ve-sonuc-tipi.cs` | Aynı yığında istisna, `TryCikar` ve `Sonuc<T>` | `02-hata-karari.svg` |
| `yapilar/` | **Örnek çözüm:** `Yigin<T>`, `Kuyruk<T>` kitaplığı + 15 xUnit testi | — |
| `lab/` | **Laboratuvar:** 22 hazır test, kodu yazılacak üç görev | — |

## Çalıştırma

Tek dosyalık örnekler her zamanki gibi:

```
dotnet run 01-istisna-akisi.cs
```

Çözümler bir proje grubudur; testleri çalıştırmak için çözüm klasörüne
girip:

```
cd yapilar
dotnet test
```

İlk çalıştırmada xUnit paketleri NuGet'ten indirilir; internet bağlantısı
gerekir.

Visual Studio 2026'da `yapilar.slnx` dosyasını açın; testler **Test
Gezgini** penceresinde görünür. VS Code'da C# Dev Kit eklentisi aynı
listeyi **Testing** panelinde gösterir.

### .NET 9 kullanıyorsanız

**Tek dosyalık örnekler** (`01`–`04`): `#:` satırı yok. Dosyayı bir konsol
projesinin `Program.cs` dosyasına koyup çalıştırın:

```
dotnet new console -o Deneme
copy 01-istisna-akisi.cs Deneme\Program.cs
dotnet run --project Deneme
```

**Çözümler** (`yapilar/`, `lab/`): iki `.csproj` dosyasında da
`<TargetFramework>net10.0</TargetFramework>` satırını `net9.0` yapın.
Sonra çözüm dosyası yerine test projesini verin:

```
dotnet test Yapilar.Testler
```

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün örnekler ve iki
çözüm bu yolla da denendi (C# 13); çıktılar ve test sonuçları aynı.

## Laboratuvar

`lab/` klasöründe iki proje var:

| Proje | Ne var |
| ----- | ------ |
| `HataLab/` | Yazacağınız kod. Her metot şu an `NotImplementedException` fırlatıyor |
| `HataLab.Testler/` | 22 hazır test. **Bu dosyaları değiştirmeyin** |

```
cd lab
dotnet test
```

İlk çalıştırmada 22 testin hepsi kırmızıdır. Görev, testlere dokunmadan
hepsini yeşile çevirmek:

| Görev | Dosya | Ne yapar | Test |
| :---: | ----- | -------- | :--: |
| 1 | `SinirliKuyruk.cs` | Kapasiteli kuyruk: koruma cümleleri, `InvalidOperationException`, `TryEkle` | 9 |
| 2 | `PuanOkuyucu.cs` → `Oku` | Metni 0–100 puana çevirir; istisna fırlatmaz, `Sonuc<int>` döndürür | 11 |
| 3 | `PuanOkuyucu.cs` → `OkuYaDaFirlat` | `Hatali` sonucu `ArgumentException`'a çevirir | 2 |

Her görevin hata sözleşmesi, dosyanın başındaki yorumda yazılı. Testler
istisnanın **türüne** birebir bakar: `ArgumentOutOfRangeException` beklenen
yerde `ArgumentException` fırlatmak testi geçmez.

`HataLab/` klasörüne başka bir `.cs` dosyası (örneğin bir deneme dosyası)
**koymayın**: klasördeki her `.cs` projeye katılır, üst düzey kod içeren bir
dosya bütün projeyi derlenmez hale getirir ve 22 testin hepsi birden kırmızı
görünür.

Bir test kırmızıysa önce **adını** okuyun; hangi davranışın bozuk olduğunu
söyler. Sonra "Hata İletisi" satırına bakın: `Expected` beklenen, `Actual`
sizin kodunuzun ürettiği değerdir.

Tek bir testi çalıştırmak için:

```
dotnet test --filter "FullyQualifiedName~TryEkle"
```

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01-istisna-akisi.cs` içinde `Bol` metodunun `catch` bloğunu silin
  (`try` ve `finally` kalsın). `Bol(10, 0)` çağrısında ekrana ne yazılır?
  Program nasıl biter?
- Aynı dosyada `catch (DivideByZeroException)` yerine
  `catch (ArithmeticException)` yazın. Bir şey değişir mi? Neden?
- `02-koruma-cumleleri.cs` içinde `Dene` metodunun ilk `catch` bloğunun
  önüne `catch (Exception) { }` ekleyin. Program derleniyor mu? Derleyici
  hangi satırlara itiraz ediyor?
- `03-kendi-istisnamiz-ve-filtre.cs` içinde `BosYapiException`'ı
  `InvalidOperationException` yerine doğrudan `Exception`'dan türetin. Hangi
  satır artık çalışmıyor? Bu değişiklik kimi etkiler?
- `04-try-kalibi-ve-sonuc-tipi.cs` içinde `Anlat` metodunun `_` kolunu
  silin. Derleyici ne diyor? `Sonuc<T>`'nin yalnızca iki alt tipi olduğu
  halde neden?
- `yapilar/Yapilar/Kuyruk.cs` içinde `if (_bas is null) _son = null;`
  satırını silin ve `dotnet test` çalıştırın. Kaç test kırıldı? Hangisi?
  Satırı geri koyun.
- `yapilar/Yapilar.Testler/YiginTestleri.cs` içinde
  `Assert.Throws<InvalidOperationException>` yerine
  `Assert.Throws<Exception>` yazın. Test geçer mi? `Assert.ThrowsAny` ile?
- `Kuyruk<T>`'ye, yığındaki `Tepe` gibi bir `Bas` özelliği ekleyin: baştaki
  elemanı çıkarmadan döndürsün, boşsa `InvalidOperationException`. Önce iki
  testini yazın (boş kuyrukta istisna, eleman çıkarmadığı), sonra kodunu.

## Çıktılar

Denemeden önce bakmayın. .NET 10.0, Windows'ta ölçüldü.

<details>
<summary>01-istisna-akisi.cs</summary>

```
=== 1 === (try, catch, finally sırası)
[try] [finally] 5
[try] [catch] [finally] -1

=== 2 === (istisna yukarı çıkarken)
Dis başladı | Ic | Dis'in finally'si | yakalandı: Ic işini yapamadı
```

`catch` silinirse: `[try] [finally] ` yazılır, ardından program yakalanmamış
`DivideByZeroException` ile sonlanır. `finally` yine çalışmıştır.
</details>

<details>
<summary>02-koruma-cumleleri.cs</summary>

```
kapasite 0     ArgumentOutOfRangeException (parametre: kapasite)
kapasite 2     sorun yok
null ekle      ArgumentNullException (parametre: deger)
ilk eleman     sorun yok
ikinci eleman  sorun yok
üçüncü eleman  InvalidOperationException: Yığın dolu.
yığında 2 eleman var
```

`catch (Exception)` başa eklenirse: `error CS0160`, iki kez — sonraki iki
`catch`'e hiçbir zaman ulaşılamaz.
</details>

<details>
<summary>03-kendi-istisnamiz-ve-filtre.cs</summary>

```
yığından çıkar       → yığın boş: işlem iptal
kuyruktan çıkar      → kuyruk boş: biraz bekleyip yeniden dene
metni sayıya çevir   → [günlük: FormatException] sayı değil
kuyruktan çıkar      → değer: 7

Taban tipi yakalayan kod da çalışır: BosYapiException
```

`Exception`'dan türetilirse: ilk dört satır aynı, son bölümdeki
`catch (InvalidOperationException)` artık yakalayamaz ve program yakalanmamış
`BosYapiException` ile sonlanır.
</details>

<details>
<summary>04-try-kalibi-ve-sonuc-tipi.cs</summary>

```
çıktı: Çalıkuşu
TryCikar boş yığında: False
hatalı: yığın boş
başarılı: Huzur
int.TryParse("12a"): False, sayi = 0
int.Parse("12a"): FormatException
```

`_` kolu silinirse: `warning CS8509`. Derleyici `Sonuc<T>`'den başka bir
yerde yeni bir alt tip türetilebileceğini bilir; kayıt hiyerarşisi
"kapalı" değildir.
</details>

<details>
<summary>yapilar/ — dotnet test</summary>

```
Başarılı!  - Başarısız: 0, Başarılı: 15, Atlanan: 0, Toplam: 15
```

`_son = null` satırı silinirse yalnızca
`KuyrukTestleri.Bosalan_kuyruga_yeniden_eklenebilir` kırılır:
`System.InvalidOperationException : Kuyruk boş.`

`Assert.Throws<Exception>` yazılırsa test kırılır:
`Exception type was not an exact match`. `Assert.ThrowsAny<Exception>`
geçer.
</details>
