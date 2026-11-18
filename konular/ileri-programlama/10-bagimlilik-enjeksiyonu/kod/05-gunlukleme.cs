// Günlükleme: ILogger, düzeyler, kategoriler ve mesaj şablonları.
//
// Console.WriteLine kullanıcıya konuşur; günlük (log) ise programı
// İŞLETENLERE konuşur: ne oldu, ne zaman, hangi bileşende. Her kayıt bir
// DÜZEY (Debug, Information, Warning, Error...) ve bir KATEGORİ (genellikle
// sınıfın tam adı) taşır. Hangi düzeyin yazılacağı koddan değil,
// yapılandırmadan seçilir.
//
// Burada en düşük düzey Information; ama "Kutuphane.Odunc" kategorisi için
// Debug'a indirilmiş. Hangi satırlar yazılır, hangileri yazılmaz?
// Çalıştırmak için:  dotnet run 05-gunlukleme.cs

#:package Microsoft.Extensions.Logging.Console@10.0.14

using Microsoft.Extensions.Logging;

using (var fabrika = LoggerFactory.Create(b => b
    .SetMinimumLevel(LogLevel.Information)
    .AddFilter("Kutuphane.Odunc", LogLevel.Debug)
    .AddSimpleConsole(o => o.SingleLine = true)))
{
    var odunc = fabrika.CreateLogger("Kutuphane.Odunc");
    var rapor = fabrika.CreateLogger("Kutuphane.Rapor");

    odunc.LogDebug("Ödünç isteği geldi");
    rapor.LogDebug("Rapor başlıyor");

    // Mesaj ŞABLONU: {Uye} ve {Baslik} birer adlandırılmış alan. Metin
    // birleştirme ($"...") yerine şablon kullanılır; günlük sistemi bu
    // alanları ayrı ayrı saklayıp aranabilir yapabilir.
    odunc.LogInformation("{Uye} üyesi {Baslik} kitabını aldı", "Ece", "Huzur");
    odunc.LogWarning("{Uye} üyesinin iadesi {Gun} gün gecikti", "Can", 6);
    rapor.LogInformation("Rapor hazır: {Sayi} ödünç", 42);

    try
    {
        throw new InvalidOperationException("Depo dosyası okunamadı.");
    }
    catch (InvalidOperationException e)
    {
        // İstisna ilk argüman olarak verilir; mesajı ve yığın izi kayda girer
        rapor.LogError(e, "Rapor üretilemedi");
    }
}
// using bloğunun sonunda fabrika kapanır ve bekleyen kayıtlar yazılır
