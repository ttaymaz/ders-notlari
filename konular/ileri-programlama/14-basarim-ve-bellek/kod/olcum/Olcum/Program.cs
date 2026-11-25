using BenchmarkDotNet.Running;

// BenchmarkDotNet: başarımı doğru ölçmek için hazır bir araç.
//
// Stopwatch ile yaptığımız her şeyi (ısınma, birçok tekrar, ortalama) ve
// fazlasını kendisi yapar: ölçümü ayrı bir süreçte Release derlemesiyle
// çalıştırır, sonuçların ne kadar oynadığını (hata payı) hesaplar,
// [MemoryDiagnoser] ile ayrılan baytı ölçer.
//
// Çalıştırmak (Release şart; Debug derlemede araç çalışmayı reddeder):
//   dotnet run -c Release --project Olcum -- --filter "*"
// Yalnızca bir sınıf:  -- --filter "*Birlestirme*"
// Kısa ölçüm (daha az tekrar, daha büyük hata payı):  -- --filter "*" --job short
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
