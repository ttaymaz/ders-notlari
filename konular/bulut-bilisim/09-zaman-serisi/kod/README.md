# Örnek Not Defterleri

Bu klasördeki `.py` dosyaları **Databricks not defteri kaynağıdır.**

## İçe aktarma

Sol menü → **Workspace** → klasörünüzün sağındaki **üç nokta** → **Import** →
**File** → dosyayı seçin. `# COMMAND ----------` satırları hücre sınırlarını
gösterir.

## Dosyalar

| Dosya | Ne gösteriyor |
| ----- | ------------- |
| [`01-zaman-serisi.py`](01-zaman-serisi.py) | Zaman ekseni: tarihten parça çıkarmak, günlük seriye indirgemek, hareketli ortalama ve profil karşılaştırma |

## Bu hafta iki yenilik var

**`to_date` ile çözünürlük düşürmek.** Saatlik üç milyon satır, günlük 62
satıra iniyor. Grafiğe dökülebilir hâle gelmesi bu yüzden.

**Pencere fonksiyonu.** Hareketli ortalama `OVER (...)` ile alınıyor:

```sql
AVG(toplam_arac) OVER (ORDER BY gun ROWS BETWEEN 6 PRECEDING AND CURRENT ROW)
```

`GROUP BY` satırları birleştirip azaltır; pencere fonksiyonu **satırları
azaltmaz**, her satıra komşularından hesaplanan bir değer ekler. Fark budur.

## Çalıştırmadan önce

**İlk hücredeki `VERI` satırına bakın.** Volume'unuzun adı `trafik` değilse
yalnızca o satırı düzeltin.

Günlük seride **62 satır** bekliyoruz (Aralık 31 + Ocak 31). Daha az çıkarsa
bazı günler hiç ölçülmemiş demektir; hangilerinin eksik olduğunu bulmak
gelecek haftanın konusu.

## Denemeniz için

1. Hareketli ortalamada `6 PRECEDING` yerine `2 PRECEDING` yazın. Eğri neyi
   kaybetti, neyi kazandı?
2. İlk altı günün `yedi_gun_ortalama` değerine bakın. Neden ham veriye
   yakınlar?
3. Günlük seride en düşük değerli günü bulun. Hangi güne denk geliyor ve bu
   beklenen bir şey mi?
4. Profil grafiğinde iki eğrinin kesiştiği saatler var mı? Varsa ne anlama
   gelir?
