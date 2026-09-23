# İşletim Sistemleri: Çalışma Ortamı

Bu derste iki araç kullanacağız. Birincisi zorunludur ve ilk haftadan itibaren gerekir; ikincisi derste gösterilen C örneklerini kendi bilgisayarınızda da çalıştırmak isteyenler içindir.

| Araç | Ne için | Ne zaman |
| ---- | ------- | -------- |
| **Python 3** ve **OSTEP simülatörleri** | Zamanlama, bellek, disk algoritmalarını evde yeni örneklerle denemek | 1. haftadan itibaren |
| **C derleyicisi** (`gcc`, Linux veya WSL) | Derste gösterilen `fork`, iş parçacığı gibi örnekleri kendiniz çalıştırmak | 2. haftadan itibaren, isteğe bağlı |

Bu belge dönem boyunca aynı kalır. Bir adımda takılırsanız hata mesajının tamamını, ekran görüntüsü yerine **metin olarak** kopyalayıp getirin.

---

## 1. Python 3

Simülatörler Python ile yazılmıştır. Python bilmeniz gerekmiyor — yalnızca komut satırından çalıştıracaksınız.

### Kurulu mu?

Komut satırını açın (Windows'ta *Başlat → "Terminal"* veya *"PowerShell"*) ve yazın:

```
python --version
```

`Python 3.` ile başlayan bir satır görüyorsanız bu bölümü atlayın. macOS ve Linux'ta komut `python3 --version` biçimindedir.

### Windows'ta kurulum

1. `https://www.python.org/downloads/` adresinden güncel Python 3 sürümünü indirin.
2. Kurulumun **ilk ekranında** alttaki **"Add python.exe to PATH"** kutusunu işaretleyin. Bu kutu işaretlenmezse `python` komutu bulunamaz.
3. *Install Now* ile kurun, komut satırını **kapatıp yeniden açın** ve `python --version` ile doğrulayın.

> **Sık görülen tuzak:** `python` yazınca Microsoft Store açılıyorsa, Windows'un kendi kısayolu devreye giriyordur. *Ayarlar → Uygulamalar → Gelişmiş uygulama ayarları → Uygulama yürütme diğer adları* bölümünde `python.exe` ve `python3.exe` satırlarını kapatın.

---

## 2. OSTEP Simülatörleri

Ders kitabımız *Operating Systems: Three Easy Pieces*'in yazarları, her bölüm için küçük bir simülatör hazırlamıştır. Simülatör size rastgele bir problem üretir; siz kâğıtta çözersiniz, sonra aynı komuta `-c` ekleyerek cevabı görürsünüz. Derste elle izlediğimiz her algoritmayı evde sınırsız sayıda yeni örnekle çalışmanın yolu budur.

### İndirme

Depo adresi: `https://github.com/remzi-arpacidusseau/ostep-homework`

- **Git kullanmıyorsanız:** sayfadaki yeşil *Code* düğmesi → *Download ZIP*. İnen dosyayı bir klasöre çıkarın. GitHub hesabı gerekmez.
- **Git kullanıyorsanız:** `git clone https://github.com/remzi-arpacidusseau/ostep-homework`

Klasörün içinde her konu için ayrı bir alt klasör vardır. Dönem boyunca kullanacaklarımız:

| Hafta | Klasör | Simülatör |
| :---: | ------ | --------- |
| 1 | `cpu-intro` | `process-run.py` |
| 2 | `cpu-api` | `fork.py` |
| 3 | `cpu-sched` | `scheduler.py` |
| 4 | `cpu-sched-mlfq`, `cpu-sched-lottery` | `mlfq.py`, `lottery.py` |
| 5 | `vm-mechanism`, `vm-segmentation` | `relocation.py`, `segmentation.py` |
| 6 | `vm-paging` | `paging-linear-translate.py` |
| 7 | `vm-beyondphys-policy` | `paging-policy.py` |
| 9 | `threads-intro` | `x86.py` |
| 12 | `file-disks` | `disk.py` |
| 13 | `file-implementation` | `vsfs.py` |

### İlk çalıştırma

Komut satırında simülatör klasörüne geçin ve ilk simülatörü çalıştırın:

```
cd ostep-homework/cpu-intro
python process-run.py -l 1:0,4:100 -S SWITCH_ON_IO -c -p
```

Şuna benzer bir tablo görmelisiniz:

```
Time        PID: 0        PID: 1           CPU           IOs
  1         RUN:io         READY             1
  2        BLOCKED       RUN:cpu             1             1
  ...
Stats: Total Time 7
Stats: CPU Busy 6 (85.71%)
```

Bu tablonun ne anlattığını 1. haftanın notunda satır satır izliyoruz.

> **Windows'ta komut biçimi:** Simülatörlerin kendi açıklamalarında komutlar `./process-run.py` biçiminde yazılmıştır. Bu biçim Linux ve macOS içindir; Windows'ta başına `python` yazın: `python process-run.py`.

Her simülatör `-h` ile kendi seçeneklerini listeler. `-s` ile farklı bir tohum (seed) vererek yeni bir rastgele problem üretebilirsiniz: `-s 1`, `-s 2`, ...

---

## 3. C Derleyicisi (İsteğe Bağlı, 2. Haftadan İtibaren)

Derste proses oluşturmayı (`fork`) ve iş parçacıklarını kısa C programlarıyla göstereceğiz. Bu programlar Linux'un sistem çağrılarını kullanır; `fork` Windows'ta **yoktur**. Kendi bilgisayarınızda çalıştırmak istiyorsanız bir Linux ortamı gerekir.

### Windows: WSL

WSL (Windows Subsystem for Linux), Windows'un içinde gerçek bir Linux çalıştırır.

1. PowerShell'i **yönetici olarak** açın (*Başlat → "PowerShell" → sağ tık → Yönetici olarak çalıştır*).
2. `wsl --install` yazın. Kurulum bitince bilgisayarı yeniden başlatın.
3. Açılışta Ubuntu penceresi gelir; bir kullanıcı adı ve parola belirleyin.
4. Ubuntu penceresinde derleyiciyi kurun:

```
sudo apt update
sudo apt install build-essential
```

5. Doğrulayın: `gcc --version`

### macOS ve Linux

- **macOS:** Terminal'de `xcode-select --install`. (`fork` macOS'ta vardır; iş parçacığı örnekleri de çalışır.)
- **Ubuntu / Debian:** `sudo apt install build-essential`

### Bir örneği derleyip çalıştırmak

```
gcc 01-fork.c -o 01-fork.out
./01-fork.out
```

Derste kullanılan C dosyaları her haftanın `kod/` klasöründe yayımlanır. Derlenmiş dosyaya `.out` uzantısı vermek, kaynak dosyayla karışmasını önler.

---

## 4. Takıldığınızda

| Belirti | Olası sebep |
| ------- | ----------- |
| `python` bulunamadı | Kurulumda "Add python.exe to PATH" işaretlenmedi; kurulumu *Modify* ile tekrar açıp işaretleyin |
| `python` yazınca Store açılıyor | Uygulama yürütme diğer adları (yukarıdaki tuzak kutusu) |
| `No such file or directory` | Yanlış klasördesiniz; `cd` ile simülatörün klasörüne geçin |
| `UnicodeEncodeError: 'charmap' codec ...` | Simülatörün çizdiği ağaç karakterleri Türkçe Windows kod sayfasına sığmıyor. Komutun sonuna `-P basic` ekleyin ya da PowerShell'de önce `$env:PYTHONUTF8=1` yazın |
| `wsl --install` hata veriyor | BIOS'ta sanallaştırma kapalı olabilir; hata metnini getirin |
| `gcc: command not found` | `build-essential` kurulmadı veya komut WSL yerine PowerShell'de yazıldı |

---

*Öğr. Gör. Turgay Taymaz · Afyon Kocatepe Üniversitesi*
*Bu materyal CC BY-NC-SA 4.0 ile lisanslanmıştır. Kullanırken kaynak gösteriniz.*
*Kaynak: https://github.com/ttaymaz/ders-notlari*
