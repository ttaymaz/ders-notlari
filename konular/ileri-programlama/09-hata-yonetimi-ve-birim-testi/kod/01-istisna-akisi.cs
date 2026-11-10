// İstisna nereye gider, finally ne zaman çalışır?
//
// İki bölüm. Birincisinde aynı metot iki kez çağrılıyor: bir kez sorunsuz,
// bir kez sıfıra bölerek. try, catch ve finally bloklarının her biri
// çalıştığında ekrana bir iz bırakıyor. İkincisinde istisna, onu
// yakalamayan bir metodun içinden geçerek yukarı çıkıyor.
//
// Çalıştırmadan önce iki bölümün çıktısını kâğıda yazın.
// Çalıştırmak için:  dotnet run 01-istisna-akisi.cs

Console.WriteLine("=== 1 === (try, catch, finally sırası)");
Console.WriteLine(Bol(10, 2));
Console.WriteLine(Bol(10, 0));

Console.WriteLine("\n=== 2 === (istisna yukarı çıkarken)");
try
{
    Dis();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"yakalandı: {e.Message}");
}

int Bol(int a, int b)
{
    try
    {
        Console.Write("[try] ");
        return a / b;
    }
    catch (DivideByZeroException)
    {
        Console.Write("[catch] ");
        return -1;
    }
    finally
    {
        Console.Write("[finally] ");
    }
}

void Dis()
{
    Console.Write("Dis başladı | ");
    try
    {
        Ic();
        Console.Write("Ic döndü | ");
    }
    finally
    {
        Console.Write("Dis'in finally'si | ");
    }
    Console.Write("Dis bitti | ");
}

void Ic()
{
    Console.Write("Ic | ");
    throw new InvalidOperationException("Ic işini yapamadı");
}
