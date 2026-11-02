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
display(spark.sql("SHOW CATALOGS"))

# COMMAND ----------

display(spark.sql("SHOW SCHEMAS IN workspace"))

# COMMAND ----------

display(spark.sql("SHOW TABLES IN workspace.default"))

# COMMAND ----------

# 3) KİM NEYE ERİŞİYOR?
# Yetki kodun içinde değil katalogda yaşıyor. Liste kısa olacak: bu ortamda
# hesap tek kişilik, yani paylaşacak ikinci bir kullanıcı yok.
display(spark.sql(f"SHOW GRANTS ON TABLE {TABLO}"))

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
# location alanına bakın. Tablonuz bir nesne depolama kovasında -- üçüncü
# haftada öğrendiğiniz yer. Ama o satır hangi ÜLKEDE olduğunu söylemiyor,
# ve siz orayı seçmediniz: hesap açarken böyle bir soru sorulmadı.
display(spark.sql(f"DESCRIBE DETAIL {TABLO}").select("location", "format", "sizeInBytes"))

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
# 4. Trafik verisini araç başına toplanmış hâlde hayal edin. Hangi sütunlar
#    eklenirdi ve o veri kişisel veri olur muydu?
