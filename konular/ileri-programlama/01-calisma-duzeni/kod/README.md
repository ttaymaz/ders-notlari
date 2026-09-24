# 1. Hafta — Örnek Kodlar

| Dosya | Konu |
| ----- | ---- |
| `01-tek-dosya.cs` | Proje açmadan tek dosya çalıştırmak |
| `02-paket-yonergesi.cs` | Tek dosyada NuGet paketi: `#:package` |
| `03-nullable-uyarilar.cs` | Derleyicinin null uyarıları — okumazsanız program çöker |
| `04-nullable-duzeltme.cs` | Aynı program, uyarısız |
| `05-kultur-tuzagi.cs` | Derleyicinin görmediği, çözümleyicinin gördüğü hata |
| `06-kultur-duzeltme.cs` | Aynı program, uyarısız |
| `hatali/01-uyari-hata-olsun.cs` | **Kasıtlı olarak derlenmez** — uyarıyı hataya çevirmek |

`03`–`04` ve `05`–`06` çiftlerini **yan yana açın.** Her çiftte ilk dosya
derlenir ama yanlıştır; ikincisi doğrudur.

## Çalıştırma

.NET 10 SDK gerekir (`dotnet --version` → `10.0.xxx`).

```
dotnet run 01-tek-dosya.cs
dotnet build 03-nullable-uyarilar.cs
```

`dotnet build` uyarıları gösterir, programı çalıştırmaz. `dotnet run` önce
derler, sonra çalıştırır; uyarılar kaydırılıp kaybolabilir. **Uyarıları
görmek için önce `build` kullanın.**

> **Uyarılar ikinci kez görünmez.** Dosyayı değiştirmeden `dotnet build`
> komutunu tekrar çalıştırırsanız derleme önbellekten gelir ve uyarılar
> yazılmaz — "uyarılar gitti" sanmayın. Yeniden görmek için:
> `dotnet build 03-nullable-uyarilar.cs --no-incremental`

## Denemeniz için

Her soruda önce **tahmin edin**, sonra çalıştırın.

- `01-tek-dosya.cs` için `dotnet project convert 01-tek-dosya.cs` çalıştırın.
  Oluşan `.csproj` dosyasında sizin yazmadığınız hangi satırlar var?
- `02-paket-yonergesi.cs` içinde sürümü `0.57.2` yerine var olmayan bir
  sürüme (`0.0.1`) çevirin. Hata derleme sırasında mı, çalışma sırasında mı
  geliyor?
- `03-nullable-uyarilar.cs` için derleyici **ilk** `Console.WriteLine`
  satırını da uyarıyor, oysa o kitap rafta var. Derleyici neden bunu
  bilemiyor?
- `04-nullable-duzeltme.cs` içinde `kitap?.Baslik` yerine `kitap!.Baslik`
  yazın. Uyarı kayboluyor mu? Program ne yapıyor? `!` işareti derleyiciye
  ne söylüyor?
- `05-kultur-tuzagi.cs` içindeki `#:property` satırını silip `dotnet build`
  çalıştırın. Kaç uyarı var? Program hâlâ yanlış mı?
- `05-kultur-tuzagi.cs` içinde `"tr-TR"` yerine `"en-US"` yazın. İki satırın
  çıktısı ne oluyor? Hangi çıktı "doğru"?
- `hatali/01-uyari-hata-olsun.cs` dosyasını `06` numaralı dosyaya bakmadan
  derlenir hale getirin.

## Çıktılar

Denemeden önce bakmayın. .NET 10.0.12, Windows, Türkçe kültürde ölçüldü.

<details>
<summary>01-tek-dosya.cs</summary>

```
.NET sürümü : 10.0.12
Kültür      : tr-TR
İşletim s.  : Microsoft Windows NT 10.0.26200.0
```

Sürüm, kültür ve işletim sistemi satırları bilgisayarınıza göre değişir.
</details>

<details>
<summary>02-paket-yonergesi.cs</summary>

```
┌────────────────┬───────────────────────┬──────┐
│ Kitap          │ Yazar                 │  Yıl │
├────────────────┼───────────────────────┼──────┤
│ Nutuk          │ Mustafa Kemal Atatürk │ 1927 │
│ İnce Memed     │ Yaşar Kemal           │ 1955 │
│ Tutunamayanlar │ Oğuz Atay             │ 1972 │
└────────────────┴───────────────────────┴──────┘
```
</details>

<details>
<summary>03-nullable-uyarilar.cs</summary>

`dotnet build` üç uyarı verir: iki `CS8602` (satır 16 ve 19) ve bir
`CS8618` (`Ozet` özelliği). `dotnet run`:

```
İnce Memed
Unhandled exception. System.NullReferenceException: Object reference not set to an instance of an object.
```
</details>

<details>
<summary>04-nullable-duzeltme.cs</summary>

Uyarı yok.

```
İnce Memed
(bulunamadı)
```
</details>

<details>
<summary>05-kultur-tuzagi.cs</summary>

`dotnet build` dört uyarı verir: `CA1304`, `CA1305`, `CA1311`, `CA1862`.
`dotnet run`:

```
False
125
```

İki satır da yanlış. Türkçede `i` harfinin büyüğü `İ`'dir, `I` değil. Türkçe
ondalık ayırıcı virgül olduğu için `12.5` metnindeki nokta binlik ayırıcı
sayılır ve `125` okunur. Program çökmez, **yanlış sayıyla sessizce devam eder.**
</details>

<details>
<summary>06-kultur-duzeltme.cs</summary>

Uyarı yok.

```
True
12.5
```
</details>
