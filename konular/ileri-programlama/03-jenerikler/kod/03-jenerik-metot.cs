// Jenerik metot ve kısıt: "T her ne ise, karşılaştırılabilir olmalı."
//
// where T : IComparable<T> olmasaydı derleyici CompareTo çağrısına izin
// vermezdi; T'nin ne olduğunu bilmiyordu. Kısıt, T'ye bir söz verdirir.
// Kısıtsız hali:     hatali/01-kisit-yok.cs
// Kısıtı ihlal eden: hatali/02-kisit-ihlali.cs
// Çalıştırmak için:  dotnet run 03-jenerik-metot.cs

Console.WriteLine(EnBuyuk(3, 7));
Console.WriteLine(EnBuyuk("elma", "armut"));
Console.WriteLine(EnBuyuk(new DateOnly(1923, 10, 29), new DateOnly(1920, 4, 23)));
Console.WriteLine(EnBuyuk<double>(2, 2.5));

// Tip argümanı yazılmadıysa derleyici argümanlardan çıkarır (tür çıkarımı).
T EnBuyuk<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) >= 0 ? a : b;
