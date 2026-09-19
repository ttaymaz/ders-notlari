# 14. Hafta — Örnek Kodlar

| Dosya | Konu | Şema |
| ----- | ---- | ---- |
| `01-hesap-hiyerarsisi.cs` | Hangi üye nereye: `abstract`, `virtual`, normal metot | `02-atm-yapisi.svg` |
| `02-arayuz-yetenek.cs` | Kalıtım mı arayüz mü — `IFaizGetirir` | — |
| `03-mini-atm.cs` | Tam sistem: `Atm`, `List` + `Dictionary` | `02-atm-yapisi.svg` |
| `hatali/01-kotu-tasarim.cs` | **Kasıtlı kötü tasarım** — çalışır ama değiştirilemez | — |

## Çalıştırma

```bash
dotnet run 03-mini-atm.cs
```

`hatali/` klasöründeki dosya **derlenir ve doğru sonucu verir.** Sorun
hatada değil tasarımdadır; bedeli değişiklik istendiğinde ortaya çıkar.

## Denemeniz için

- `01-hesap-hiyerarsisi.cs` içinde `new Hesap("Test", 100m)` yazmayı
  deneyin. Derleyici ne diyor? `abstract` kelimesini silip tekrar deneyin.
- Aynı dosyada `VadesizHesap` sınıfından `override void ParaCek` metodunu
  silin. `abstract` metot yazmayı zorunlu kılar; `virtual` kılmaz.
- Aynı dosyada `hesaplar[0].Bakiye = 1000000m;` yazın. Derlenmiyor.
  `private set` ifadesini `set` yapıp tekrar deneyin — kapsülleme bunun için.
- `02-arayuz-yetenek.cs` içinde `if (h is IFaizGetirir faizli)` yerine
  `if (h is VadeliHesap v)` yazın. Sonra faiz getiren ikinci bir tür ekleyin.
  Hangi sürümde kodu değiştirmek zorunda kaldınız?
- `03-mini-atm.cs` içine dördüncü bir hesap türü ekleyin: `OgrenciHesabi`.
  `Atm` sınıfında kaç satır değiştirdiniz? Cevap sıfır olmalı.
- Aynı dosyada `Atm` sınıfına `HesapKapat(int hesapNo)` yazın. Hem listeden
  hem sözlükten silin. `foreach` ile silmeye çalışırsanız ne olur?
- `hatali/01-kotu-tasarim.cs` dosyasındaki **dört görevi** hem o dosyada
  hem `03-mini-atm.cs` içinde yapın. Değiştirdiğiniz satır sayılarını yan
  yana yazın — aradaki fark, bu haftanın bütün konusudur.
