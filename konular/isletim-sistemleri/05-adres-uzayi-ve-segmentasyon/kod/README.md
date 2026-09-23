# Adres Uzayı — Örnek Kodlar

Programlar Linux'ta, macOS'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md).

```
gcc 01-adresler.c -o 01-adresler.out
./01-adresler.out
```

| Dosya | Gösterdiği | Şema |
| ----- | ---------- | ---- |
| `01-adresler.c` | Kod, veri, heap ve stack'in adres uzayındaki yerleri; adres rastgeleleştirmesi | `01-adres-uzayi.svg` |
| `02-ayni-adres.c` | `fork` sonrası iki proses aynı sanal adreste farklı değer görür | `02-taban-sinir.svg` |

## Denemeniz İçin

- `01-adresler.out` programını üç kez çalıştırın. Hangi adresler değişiyor? Adreslerin **sırası** değişiyor mu?
- Aynı programı `setarch $(uname -m) -R ./01-adresler.out` ile iki kez çalıştırın. Ne farklı?
- `01-adresler.c` içinde `malloc`'u bir döngüde on kez çağırıp her dönen adresi yazdırın. Heap hangi yöne büyüyor?
- `01-adresler.c` içine kendini çağıran (özyinelemeli) bir fonksiyon ekleyin ve her çağrıda yerel bir değişkenin adresini yazdırın. Stack hangi yöne büyüyor?
- `02-ayni-adres.c` içinde `sayi` değişkenini `main`'in dışına, genel değişken olarak taşıyın. Sonuç değişiyor mu? Neden?
- `02-ayni-adres.c` içinde `fork` yerine aynı programı iki ayrı terminalde `setarch $(uname -m) -R` ile çalıştırmayı düşünün. İki bağımsız proses de aynı adresi yazdırır mı?
