# 13. Hafta — Örnek Kodlar ve Laboratuvar

**Yansıma ve öznitelikler:** program çalışırken kendi tiplerine bakar,
onları kurar, metotlarını çağırır; öznitelikler koda iliştirilmiş,
yansımayla okunan etiketlerdir. Bu hafta xUnit'in testlerimizi nasıl
bulduğunu kendi küçük test koşucumuzla görüyor, sonra aynı fikirle bir
eklenti mimarisi kuruyoruz. Taşıyıcı bir **ağaç**: tip hiyerarşisi.

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-tip-bilgisi.cs` | `Type`, üyeler; **taşıyıcı:** tip ağacı; `private` erişim; `TargetInvocationException` | `01-tip-agaci.svg` |
| `02-oznitelikler.cs` | Kendi özniteliğimiz `[Komut]`; etiketlerden kurulan menü; `[Obsolete]` | — |
| `03-test-kosucu.cs` | 30 satırlık test koşucusu: `[Sinama]` | — |
| `04-kaynak-ureteci.cs` | `[JsonSerializable]`, `[GeneratedRegex]`; yansıma ve AOT | — |
| `eklenti/` | **Örnek çözüm:** sözleşme + eklenti DLL'i + yükleyici + 3 test | `02-eklenti.svg` |
| `lab/` | **Laboratuvar:** 28 hazır test, dört görev | — |

## Çalıştırma

```
dotnet run 01-tip-bilgisi.cs
```

`03` ve `04` dosyalarının ilk satırı `#:property PublishAot=false`. Sebebi
dosyaların başındaki yorumda ve notun 8. bölümünde: `dotnet run dosya.cs`
tek dosyalık programı önceden derlemeye (AOT) hazır kabul eder; bütün
tipleri tarayan yansıma kodu bu kipte uyarı verir, yansımalı JSON ise hiç
çalışmaz.

Eklenti örneği:

```
cd eklenti
dotnet build
dotnet run --project Uygulama
dotnet test
```

`dotnet build` eklenti DLL'ini `Uygulama/bin/Debug/net10.0/eklentiler/`
klasörüne kopyalar (`Uygulama.csproj` içindeki `EklentileriKopyala` hedefi).
Uygulamanın kodu rapor sınıflarının adını hiçbir yerde yazmaz.

### .NET 9 kullanıyorsanız

**Tek dosyalık örnekler:** Dosyayı bir konsol projesinin `Program.cs`
dosyasına kopyalayın ve `#:property` satırını silin. Proje biçiminde AOT
varsayılan olarak kapalıdır; satırın karşılığına gerek yoktur.

```
dotnet new console -o Deneme
copy 03-test-kosucu.cs Deneme\Program.cs
dotnet run --project Deneme
```

**Eklenti ve laboratuvar:** bütün `.csproj` dosyalarında `net10.0` →
`net9.0`. Eklentide çözümle (`dotnet build`, `dotnet test`) çalışılabilir;
laboratuvarda `dotnet test YansimaLab.Testler`. Eklenti DLL'inin yolu
`bin/Debug/net9.0/eklentiler/` olur.

Linux ve macOS'ta `copy` yerine `cp`, `\` yerine `/`. Bütün örnekler,
eklenti çözümü ve laboratuvar C# 13 ile de denendi; çıktılar ve test
sonuçları aynı.

## Laboratuvar

```
cd lab
dotnet test
```

İlk çalıştırmada 28 testten **26'sı kırmızı**, 2'si yeşildir. Yeşil olan
ikisi hazır `TipAgaci.KokeGidenYol`'u sınar: taşıyıcı ağaçta yapraktan
köke yürümek.

| Görev | Dosya | Ne yapar | Test |
| :---: | ----- | -------- | :--: |
| 1 | `YansimaLab/Dogrulayici.cs` | `[Zorunlu]`, `[EnFazlaUzunluk]`, `[Aralik]` özniteliklerine göre hata mesajları | 11 |
| 2 | `YansimaLab/KomutTablosu.cs` | `[Komut]` metotlarını bul, argümanları çevirip çağır, hataları mesaja çevir | 6 |
| 3 | `YansimaLab/TestKosucu.cs` | `03`'ün koşucusu + asenkron sınama + `[Atla]` + her sınamaya yeni nesne | 5 |
| 4 | `YansimaLab/Fark.cs` | İki nesnenin değişen özellikleri: `GecikmeGun: 0 → 3` | 4 |

Öznitelikler `YansimaLab/Oznitelikler.cs` içinde hazır. Testlerin
kullandığı örnek sınıflar `YansimaLab.Testler/Modeller.cs` içinde: hangi
özelliğe hangi etiketin yapıştırıldığını oradan okuyun.

**Testleri değiştirmeyin.** Kırmızı bir testin önce adını, sonra
`Expected` / `Actual` satırlarını okuyun.

İpuçları:

- **1. görev:** `GetProperties()` sırayı garanti etmez:
  `OrderBy(p => p.MetadataToken)`. Bir özellik zorunluysa ve boşsa ötekilere
  bakmayın (boş bir metnin uzunluğu sorulmaz)
- **2. görev:** `Convert.ChangeType(metin, hedefTip, CultureInfo.InvariantCulture)`.
  Çeviremezse `FormatException` fırlatır. `Invoke`'un istisnası
  `TargetInvocationException` içinde gelir (`01`'in 5. bölümü)
- **3. görev:** `Invoke` asenkron bir metot için bir `Task` döndürür;
  beklenmezse sınama hata verse bile "geçti" görünür. Beklenen `Task`'ın
  hatası **sarılmadan** gelir: iki ayrı `catch` gerekebilir
- **4. görev:** `object.Equals(a, b)` iki tarafın `null` olmasını da ele
  alır; `==` iki `object` arasında referans karşılaştırır

## Denemeniz için

Önce **kâğıda tahmininizi yazın**, sonra çalıştırın.

- `01` dosyasında `record Kitap` yerine `class Kitap` yazın ve iki özelliği
  elle ekleyin: `public string Baslik { get; } = Baslik;` ve
  `public int Sayfa { get; } = Sayfa;`. 2. bölümün metot listesi ne olur?
  Kaybolan metotları kim yazmıştı?
- `02` dosyasında `class KutuphaneMenusu` satırının üstüne `[Komut("menu")]`
  yazın. Derleyici ne der? Neden?
- `02` dosyasında `var menu = ...` satırının altına `menu.Kaldir();` yazın.
  Program derlenir mi? Derleyici ne söyler?
- `03` dosyasında koşucuyu her sınıf için **tek** nesne kuracak biçimde
  değiştirin (nesneyi bir sözlükte saklayın). Hangi sınama kalır? Neden?
- `03` dosyasında `catch (TargetInvocationException e)` yerine
  `catch (Exception e)` yazın ve `e.InnerException!.Message` yerine
  `e.Message` kullanın. Kalan sınamanın mesajı ne olur?
- `03` dosyasının ilk satırını (`#:property PublishAot=false`) silin.
  Derleyici hangi uyarıları verir?
- `04` dosyasının ilk satırını silin. Program ne yapar? 2. ve 3. bölüm
  neden yine de çalışabilirdi?
- `eklenti/` için **kendi raporunuzu** yazın: `Sozlesme`'ye başvuran yeni
  bir sınıf kitaplığı açın, `IRaporEklentisi`'ni uygulayın, derleyin ve
  DLL'i `Uygulama/bin/Debug/net10.0/eklentiler/` klasörüne kopyalayın.
  Uygulamayı **derlemeden** `dotnet run --project Uygulama --no-build`
  ile çalıştırın.

## Çıktılar

Denemeden önce bakmayın. .NET 10 çalışma zamanı, Windows'ta ölçüldü.

<details>
<summary>01-tip-bilgisi.cs</summary>

```
=== 1 === (her nesne tipini bilir)
o.GetType().Name: String
typeof(int).FullName: System.Int32
List<int>: List`1, jenerik mi? True, T = Int32

=== 2 === (üyeler)
Baslik   String  = Huzur
Sayfa    Int32   = 431
metotlar: <Clone>$, Deconstruct, Equals, Equals, GetHashCode, Ozet, ToString, UzunMu

=== 3 === (taşıyıcı: tip ağacı)
Exception
  SystemException
    ArgumentException
      ArgumentNullException
      ArgumentOutOfRangeException
    FormatException
    InvalidOperationException
      ObjectDisposedException
    KeyNotFoundException
      KitapBulunamadiException
    NotImplementedException
    OperationCanceledException
      TaskCanceledException

=== 4 === (private üyeye erişmek)
_adet = 2
SetValue sonrası Adet = 100

=== 5 === (yansımayla çağırmak ve istisna)
Invoke sonrası Adet = 101
yakalanan: TargetInvocationException
içindeki: InvalidOperationException: Sayaç sıfırlanamaz.
```

`class Kitap` ile metot listesi `Ozet, UzunMu` olur. Ötekileri `record`
için derleyici üretiyordu: değer eşitliği (`Equals`, `GetHashCode`),
`ToString`, `with` ifadesinin kopyası (`<Clone>$`) ve `Deconstruct`.
</details>

<details>
<summary>02-oznitelikler.cs</summary>

```
=== 1 === (etiketlerden kurulan menü)
kaldir     Son kitabı siler
listele    Bütün kitapları yazar
say        Kitap sayısını yazar

=== 2 === (adıyla çağırmak)
listele: Huzur, Aylak Adam, Beş Şehir
say: 3 kitap
yardim: böyle bir komut yok

=== 3 === (öznitelik de bir nesnedir)
KomutAttribute
ObsoleteAttribute
Obsolete mesajı: Yerine 'sil' komutunu kullanın.
```

`[Komut]` sınıfa yazılırsa `CS0592` hatası: `[AttributeUsage]` onu
yalnızca metotlara izin veriyor. `menu.Kaldir()` derlenir ama `CS0618`
uyarısı verir: `[Obsolete]`'ı derleyici okur.
</details>

<details>
<summary>03-test-kosucu.cs</summary>

```
[GEÇTİ] CezaSinamalari.Bes_gun_on_bes_lira
[KALDI] CezaSinamalari.Bir_gun_dort_lira: beklenen 4, gelen 3
[GEÇTİ] CezaSinamalari.Kayit_her_sinamada_bostur
[GEÇTİ] CezaSinamalari.Kayit_her_sinamada_bostur_2
[GEÇTİ] CezaSinamalari.Tavan_altmis_lira
toplam 5, geçen 4
```

Tek nesne paylaşılırsa `Kayit_her_sinamada_bostur_2` kalır:
`beklenen 1, gelen 2`. İlk sınamanın eklediği kayıt nesnede duruyor.
`catch (Exception e)` + `e.Message` ile kalan sınamanın mesajı
`Exception has been thrown by the target of an invocation.` olur: asıl
mesaj `InnerException`'da kaldı. İlk satır silinirse `IL2026`, `IL2070`,
`IL2077` uyarıları gelir; program yine çalışır.
</details>

<details>
<summary>04-kaynak-ureteci.cs</summary>

```
=== 1 === (JSON: yansımayla ve üretilmiş kodla)
{"Baslik":"Huzur","Sayfa":431,"Etiketler":["Tanp\u0131nar","roman"]}
ikisi aynı mı? True

=== 2 === (üretilen kod gerçekten var)
JsonTypeInfo`1: Baslik, Sayfa, Etiketler
KitapBaglami üretilmiş üye sayısı > 0? True

=== 3 === ([GeneratedRegex])
TR-813-07  raf kodu mu? True
TR-81-07   raf kodu mu? False
tr-813-07  raf kodu mu? False
```

İlk satır silinirse derleyici `IL2026` ve `IL3050` uyarılarını verir ve
program 1. bölümün ilk satırında durur:
`InvalidOperationException: Reflection-based serialization has been
disabled for this application.` Üretilmiş kodla yapılan çağrı ve
`[GeneratedRegex]` yansımaya ihtiyaç duymaz; program oraya gelebilseydi
çalışırlardı. `\u0131` "ı" harfidir: System.Text.Json varsayılan olarak
ASCII dışı karakterleri kaçışlı yazar (proje şablonundaki `JsonDepo` bunu
`UnsafeRelaxedJsonEscaping` ile kapatıyor).
</details>

<details>
<summary>eklenti/ — dotnet run --project Uygulama, dotnet test</summary>

```
eklentiler/ klasöründe 1 DLL, 2 rapor

[Geciken iadeler] (GecikmeRaporu)
Mert: Huzur (11 gün)
Can: Aylak Adam (6 gün)
Deniz: Huzur (2 gün)

[En çok ödünç alınanlar] (PopulerKitapRaporu)
Huzur: 3
Aylak Adam: 2
Beş Şehir: 1
```

`TemelRapor` soyut, `EsikliRapor`'un parametresiz kurucusu yok: ikisi de
atlanır. `dotnet test`: `Başarılı! - Başarısız: 0, Başarılı: 3`. Kendi
raporunuzun DLL'i klasöre bırakılınca uygulama yeniden derlenmeden
`2 DLL, 3 rapor` yazar.
</details>

<details>
<summary>lab/ — dotnet test (ilk çalıştırma)</summary>

```
Başarısız! - Başarısız: 26, Başarılı: 2, Atlanan: 0, Toplam: 28
```

Dört görev bitince `Başarılı! - Başarısız: 0, Başarılı: 28`.
</details>
