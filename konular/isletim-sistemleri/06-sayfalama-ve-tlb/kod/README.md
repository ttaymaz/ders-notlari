# Sayfalama ve TLB — Örnek Kod

Program Linux'ta, macOS'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md).

```
gcc -O1 01-tlb-olcumu.c -o 01-tlb-olcumu.out
./01-tlb-olcumu.out
```

| Dosya | Gösterdiği | Şema |
| ----- | ---------- | ---- |
| `01-tlb-olcumu.c` | Dolaşılan sayfa sayısı TLB'nin kapasitesini aşınca erişim süresinin artması | `03-tlb-akisi.svg` |

Bu bir **ölçüm** programıdır, gösterim değil: sonuçlar makineden makineye ve çalıştırmadan çalıştırmaya değişir. Ders notunun 10. bölümünde bir makinede ölçülen değerler ve yorumlama tuzakları var.

## Denemeniz İçin

- Programı üç kez çalıştırın. Sayılar ne kadar değişiyor? Sıçramanın **yeri** değişiyor mu?
- İşlemcinizin modelini bulun (`lscpu` ya da Görev Yöneticisi) ve TLB boyutunu arayın. Sıçrama o sayıya denk geliyor mu?
- Erişim satırındaki `(p * 16) % adim` ifadesini silip her sayfanın **aynı ofsetine** dokunun. Sıçrama nereye kaydı? Ders notu bunun neden TLB ile ilgili olmadığını söylüyor — neyle ilgili olabilir?
- `-O1` yerine `-O0` ile derleyin. Sayılar neden hep birlikte büyüdü?
