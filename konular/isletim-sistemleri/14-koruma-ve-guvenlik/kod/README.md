# Koruma ve Güvenlik — Örnek Kodlar

Programlar Linux'ta veya Windows'ta WSL içinde çalışır. Kurulum: [çalışma ortamı](../../00-calisma-ortami/not.md).

```
gcc 01-tampon-tasmasi.c -o 01-tampon-tasmasi.out
./01-tampon-tasmasi.out ABCDEFGHIJK
```

Programlar yalnızca **kendi verilerini** bozar ve hemen biter; sisteme ya da başka programlara dokunmaz. Derleyicinin korumalarını kapatmayın: amaç korumaların **çalıştığını** görmek.

| Dosya | Gösterdiği | Şema |
| ----- | ---------- | ---- |
| `01-tampon-tasmasi.c` | 8 baytlık ad dizisinden taşan baytların hemen ardındaki yetki alanını ezmesi; `snprintf` ile güvenli kopya | — |
| `02-yigin-korumasi.c` | Stack'teki bir tampon taşınca derleyicinin kanaryasının programı durdurması | `04-yigin-cercevesi.svg` |

## Beklenen Çıktı

`01-tampon-tasmasi.out ali`:

```
önce   : yetki = 0
strcpy : ad = "ali...", yetki = 0 (0x00000000) → sıradan kullanıcı
snprintf: ad = "ali", yetki = 0 → sıradan kullanıcı
```

`01-tampon-tasmasi.out ABCDEFGHIJK` (11 harf — daha uzununu vermeyin, kaydın dışına taşar):

```
önce   : yetki = 0
strcpy : ad = "ABCDEFGH...", yetki = 4934217 (0x004b4a49) → YÖNETİCİ
snprintf: ad = "ABCDEFG", yetki = 0 → sıradan kullanıcı
```

`02-yigin-korumasi.out`:

```
$ ./02-yigin-korumasi.out kisa
merhaba, kisa
selamla() normal döndü
$ ./02-yigin-korumasi.out AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
*** stack smashing detected ***: terminated
Aborted (core dumped)
```

## WSL Gösterimleri

```
openssl passwd -6 -salt tuz1 deneme123      # aynı parola, iki tuz
openssl passwd -6 -salt tuz2 deneme123
ls -l /etc/passwd /etc/shadow /usr/bin/passwd
id
echo gizli > f ; chmod 077 f ; cat f        # sahip okuyamaz
chmod 644 f
```

## Denemeniz İçin

- `01-tampon-tasmasi.out ABCDEFGH` (tam 8 harf) ile çalıştırın. Taşma oldu mu? Yetki neden 0 görünüyor?
- `ABCDEFGHI`, `ABCDEFGHIJ` ile çalıştırın ve yetkinin onaltılık değerini harf kodlarıyla karşılaştırın. Hangi harf yetkinin hangi baytına düşüyor?
- `01-tampon-tasmasi.c`'de `struct` içinde `ad` ile `yetki`'nin yerini değiştirin. Aynı girdiyle yetki hâlâ eziliyor mu?
- `02-yigin-korumasi.out`'u 16, 20, 24 ve 30 harfle çalıştırın. Kanarya kaç harften sonra devreye giriyor?
- Kendi dosyanızla `chmod 070` ve `chmod 007` deneyin. Hangisinde siz okuyabiliyorsunuz?
- `mkdir k; echo x > k/a; chmod 644 k` sonra `ls k` ve `cat k/a` deneyin. Klasörün `x` biti ne işe yarıyor? `chmod 755 k` ile geri açın.
