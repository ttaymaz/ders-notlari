# Databricks notebook source
# Bu not defteri üstten aşağı, tek seferde çalışır.
# Amaç: erişimin nerede tanımlandığını görmek.
#
# Bu haftanın not defteri veri okumaz. Sorular veriye değil, KATALOGA
# soruluyor: kim olduğunuz, neyin var olduğu, kimin neye eriştiği.

TABLO = "workspace.default.trafik_tablo"

# COMMAND ----------

# 1) KİMSİN?
# Birinci hafta e-postanıza gelen kod YALNIZCA bu soruyu cevaplamıştı.
# Kimlik doğrulama girişte bir kez olur; yetkilendirme her işlemde sorulur.
display(spark.sql("SELECT current_user() AS kimlik"))

# COMMAND ----------

# 2) HİYERARŞİ: KATALOG, ŞEMA, TABLO
# Geçen haftanın üç parçalı adı bir adres VE bir yetki hiyerarşisi.
# Yetki üç basamağın herhangi birine verilebilir; kapsam yukarı çıktıkça
# genişler.
#
# Olculdu: DORT katalog. Ucu hazir geliyor --
#   workspace -> sizin kurdugunuz sema ve tablolar
#   samples   -> Databricks'in ornek veri kumeleri
#   system    -> platformun kendi kayitlari
display(spark.sql("SHOW CATALOGS"))

# COMMAND ----------

# Olculdu: iki sema. default sizin, information_schema ise kataogun
# kendi kayitlarini tuttugu yer.
display(spark.sql("SHOW SCHEMAS IN workspace"))

# COMMAND ----------

# samples katalogunu SIZ KURMADINIZ. Okuyabiliyorsunuz cunku size VERILMIS.
# Yani "bu ortamda yetki verme pratigi yapamayiz" derken bir seyi
# atlamistik: siz zaten bir yetkinin ALICI tarafindasiniz.
#
# Icerigini listeleyebiliyor musunuz? Hata verirse o da bir cevap:
# katalogu GORMEK, icine ERISMEK demek degil.
display(spark.sql("SHOW SCHEMAS IN samples"))

# COMMAND ----------

display(spark.sql("SHOW TABLES IN workspace.default"))

# COMMAND ----------

# 3) KİM NEYE ERİŞİYOR?
# Yetki kodun içinde değil katalogda yaşıyor.
#
# Olculdu: liste BOS donuyor. Hicbir satir yok.
display(spark.sql(f"SHOW GRANTS ON TABLE {TABLO}"))

# COMMAND ----------

# BOS LISTE "KIMSE ERISEMIYOR" DEMEK DEGIL.
# Siz erisiyorsunuz ve rahatca sorguluyorsunuz. Sebebi: tablonun SAHIBI
# sizsiniz ve sahiplik bir yetki kaydi degil. SHOW GRANTS yalnizca ACIKCA
# VERILMIS yetkileri listeler; sahibin haklari verilmemis, bastan var.
#
# "Bu tabloyu kim gorebiliyor?" sorusunu cevaplamak icin yetki listesine
# bakmak yetmez: sahibi kim, hangi semanin icinde, o semaya kim erisebiliyor.
# En az yetki ilkesinin uygulamada zor olmasinin sebebi bu.

# COMMAND ----------

# Yetki vermenin sözdizimi şudur ve tek satırdır:
#
#     GRANT SELECT ON TABLE workspace.default.trafik_tablo TO `ayse@ornek.edu.tr`
#
# BU SATIRI ÇALIŞTIRMIYORUZ. Verecek ikinci bir kullanıcı yok; ücretsiz
# genel bulut hesabı tek kişilik ve bu bir eksiklik değil, DAĞITIM
# MODELİNİN sonucu. Sorun bu satırın ne kadar KOLAY olması.

# COMMAND ----------

# 4) VERİ NEREDE DURUYOR?
# Once dogrudan soralim. Olculdu: location alani BOS geliyor.
#
# Bu bir hata degil. Kurdugunuz tablo YONETILEN bir tablodur: nerede
# durduguna platform karar verir ve bilmeniz gerekmez. Konum bilerek
# soyutlanmis.
display(spark.sql(f"DESCRIBE DETAIL {TABLO}").select("location", "format", "sizeInBytes"))

# COMMAND ----------

# Peki hic mi gorunmuyor? Sorgu planinda goruyor. Location satirini arayin:
#     Location: ... s3://dbstorage-prod-.../uc/6c6dee90-...
#
# Tablonuz bir nesne depolama kovasinda -- ucuncu haftada ogrendiginiz yer.
# Ama bunu size kimse soylemedi; PLANI OKUDUGUNUZ ICIN gordunuz. Ustelik o
# satir hangi ULKEDE oldugunu da soylemiyor ve siz orayi secmediniz.
#
# Platform soylemeyi atlamiyor, soylememeyi seciyor.
spark.sql(f"EXPLAIN SELECT COUNT(*) FROM {TABLO}").show(truncate=False)

# COMMAND ----------

# 5) VERİNİN BİRİMİ NEDİR?
# "Bu kişisel veri mi?" sorusunun cevabı sütun adlarında değil, BİR SATIRIN
# NEYİ TEMSİL ETTİĞİNDE. Tek satıra bakıp kendinize sorun: bu satır bir
# kişiyi mi anlatıyor, bir bölge-saat dilimini mi?
display(spark.sql(f"SELECT * FROM {TABLO} LIMIT 3"))

# COMMAND ----------

# Satır birimi: bölge (GEOHASH) ve saat (zaman). Bir hücrede tek bir aracın
# değil, o bölgedeki araçların SAYISI var. Hiçbir sütun bir kişiye, araca
# ya da plakaya geri götürmüyor. Bu yüzden kişisel veri değil.
#
# Aynı veri ARAÇ BAŞINA toplanmış olsaydı cevap değişirdi: bir aracın gün
# içindeki konum dizisi sahibini bulmaya yeter.
#
# Bu soruyu sormak analistin işidir, hukukçunun değil.

# COMMAND ----------

# Denemeniz için — cevabı aramayın, çalıştırıp görün:
#
# 1. SHOW GRANTS ON SCHEMA workspace.default çalıştırın. Tablo düzeyindeki
#    listeden farklı mı?
# 2. CREATE SCHEMA workspace.deneme ile yeni bir şema açın ve tablonuzun
#    kopyasını oraya alın. İki şemanın yetkileri birbirinden bağımsız mı?
# 3. SHOW GRANTS ON CATALOG workspace çalıştırın. Katalog düzeyinde kim var?
# 3b. SHOW GRANTS ON CATALOG samples deneyin. Size verilmis yetki listede
#     gorunuyor mu, yoksa o da gorunmez mi?
# 4. Trafik verisini araç başına toplanmış hâlde hayal edin. Hangi sütunlar
#    eklenirdi ve o veri kişisel veri olur muydu?
