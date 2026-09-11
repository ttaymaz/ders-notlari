# 11. Hafta — Örnek Kodlar

| Dosya | Konu |
| ----- | ---- |
| `01-void-metot.cs` | Parametresiz, değer döndürmeyen metot |
| `02-parametreli-metot.cs` | Parametre ve argüman |
| `03-deger-donduren.cs` | `return` ve dönüş tipi |
| `04-dizi-parametresi.cs` | Diziyi metoda göndermek |
| `05-kapsam.cs` | Değişken kapsamı ve dizilerin farkı |

## Çalıştırma

```bash
dotnet run 04-dizi-parametresi.cs
```

## Denemeniz için

- `01-void-metot.cs` içinde metodu tanımlamadan **önce** çağırın. Çalışıyor mu?
- `03-deger-donduren.cs` içinde `Topla` metodunun `return` satırını silin.
  Derleyici ne diyor?
- `03-deger-donduren.cs` içinde `int Topla` yerine `void Topla` yazın. Ne oluyor?
- `04-dizi-parametresi.cs` içine **boş bir dizi** (`new int[0]`) gönderin.
  `EnBuyuk` metodu ne yapıyor? Nasıl korunurdunuz?
- `05-kapsam.cs` sonundaki `Console.WriteLine(sonuc);` satırını açın. Neden
  derlenmiyor?
