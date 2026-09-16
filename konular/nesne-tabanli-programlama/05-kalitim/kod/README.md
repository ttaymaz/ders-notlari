# 5. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-tekrar-sorunu.cs` | Üç sınıf, aynı kod üç kez | — |
| `02-kalitim-temel.cs` | Aynı program, üçte bir kod | `01-kalitim-hiyerarsisi.svg` |
| `03-base-kurucu.cs` | Kurucu sırası ve `base` | `02-kurucu-sirasi.svg` |
| `04-personel-hiyerarsisi.cs` | `protected` ve ikinci bir örnek alan | — |

İlk iki dosyayı **yan yana açın**: çıktı aynı, kod uzunluğu üçte bir.

## Çalıştırma

```bash
dotnet run 02-kalitim-temel.cs
```

## Denemeniz için

- `01-tekrar-sorunu.cs` içindeki `OduncVer` metotlarından **yalnızca birine**
  yeni bir kural ekleyin. Program hata veriyor mu? Hangi tür yanlış davranıyor?
- Aynı kuralı `02-kalitim-temel.cs` içinde ekleyin. Kaç yere dokundunuz?
- `02-kalitim-temel.cs` içine dördüncü bir tür ekleyin: `Harita : Demirbas`,
  alanı `Olcek` olsun. Kaç satır sürdü?
- `03-base-kurucu.cs` içindeki `: base(no, baslik)` kısmını silin. Hata mesajı
  size hangi haftayı hatırlatıyor?
- `04-personel-hiyerarsisi.cs` içindeki `protected decimal temelMaas;` alanını
  `private` yapın. Hangi satırlar derlenmiyor? Neden?
- Aynı dosyada `Yonetici` ve `Memur` sınıflarının ikisinde de `ToplamMaas`
  metodu var. Bu metot sizce nerede olmalıydı? Gelecek haftanın sorusu bu.
