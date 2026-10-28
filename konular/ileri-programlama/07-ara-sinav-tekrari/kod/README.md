# 7. Hafta — Ara Sınav Provası

Yeni konu yok. Üç dosya, sınavın üç soru biçiminin provasıdır.

| Dosya | Soru biçimi | Şema |
| ----- | ----------- | ---- |
| `01-cikti-tahmini.cs` | **Çıktı tahmini** — on dört bölüm, 1–6. konuların hepsinden | `01-konu-haritasi.svg` |
| `02-derlenir-mi.cs` | **Derlenir mi?** — on bir soru; satırı açıp derleyiciye sorun | — |
| `03-yazma-provasi.cs` | **Doğru kod parçası** — altı görev; sınavda seçenekli sorulan gövdeleri burada kendiniz yazarsınız | — |

## Çalıştırma

```
dotnet run 01-cikti-tahmini.cs
```

### .NET 9 kullanıyorsanız

Bu haftanın dosyalarında `#:` satırı yok. Dosyayı bir konsol projesinin
`Program.cs` dosyasına koyup çalıştırın:

```
dotnet new console -o Deneme
copy 01-cikti-tahmini.cs Deneme\Program.cs
dotnet run --project Deneme
```

`02` ve `03` için de aynı yol. Linux ve macOS'ta `copy` yerine `cp`, `\`
yerine `/`. Üç dosya bu yolla da denendi (C# 13, proje biçimi); çıktılar
`dotnet run dosya.cs` ile birebir aynı.

`02` dosyasında satırları açıp kapatırken `Deneme\Program.cs` üzerinde
çalışın; her değişiklikten sonra `dotnet run --project Deneme`.

## Nasıl çalışılır?

**Her dosyada önce kâğıt, sonra bilgisayar.** Sınavda derleyici yok.

- **`01`** — On dört bölümün çıktısını kâğıda yazın. Hepsini bitirince
  çalıştırın. Tutmayan her bölüm için "neden tutmadı?" sorusunu bir
  cümleyle cevaplayın.
- **`02`** — Başında `//?` olan her satır bir soru. Kâğıda "derlenir",
  "derlenir ama uyarı verir" veya "derlenmez + neden" yazın. Sonra `//?`
  işaretini silip çalıştırın, cevabınızı derleyicininkiyle karşılaştırın ve
  işareti geri koyun. 9, 10 ve 11. sorular bir satırı açmak yerine kodda
  küçük bir değişiklik istiyor; ne yapılacağı ilgili metodun üstünde yazılı.
- **`03`** — Altı görevi önce kâğıda yazın. Sonra buraya geçirip
  çalıştırın; dosya kaç görevin tamam olduğunu söyler. **Denetim kodunu
  değiştirmeyin.** 2. görevin denetimi sonsuz bir kaynak kullanıyor:
  çözümünüz ertelenmiş değilse `[HATA]` alırsınız.

## Çıktılar ve cevaplar

Bu haftanın çıktıları bilerek yazılmadı. Her dosya, çalıştırıldığında
cevabını kendisi verir; `03` ise yalnızca hangi görevin tamam olduğunu
söyler. Önemli olan çalıştırmadan **önce** kâğıda yazdığınız cevaptır.

## Denemeniz için

- `01` dosyasının 8. bölümünde `for` döngüsünün içine `int kopya = i;`
  ekleyin ve lambdada `i` yerine `kopya` kullanın. Çıktı ne olur? Neden?
- `01` dosyasının 11. bölümünde `var sicak = ...` satırının sonuna
  `.ToList()` ekleyin. İki sayı da ne olur?
- `01` dosyasının 13. bölümünde `0 => "sıfır"` kolunu `int => "pozitif"`
  kolunun altına taşıyın. Derlenir mi? Derlenmiyorsa neden?
- `01` dosyasının 14. bölümünde `[var tek]` kolunu silin. Derleyici ne
  söylüyor? `Bak([4])` hangi kola düşer?
- `03` dosyasında 4. görevi çözdükten sonra "Afyon için sıcak" kolu ile
  "aşırı sıcak" kolunun yerini değiştirin. Hangi denetim `[YANLIŞ]` olur?
  Derleyici neden bu kez itiraz etmedi?
