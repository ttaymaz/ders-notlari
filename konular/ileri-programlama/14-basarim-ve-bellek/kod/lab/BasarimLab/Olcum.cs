namespace BasarimLab;

// HAZIR: bir işin bu iş parçacığında kaç bayt ayırdığını ölçer. Testleri
// baştan yeşildir. Testler görevlerinizi bununla ölçüyor.
public static class Olcum
{
    public static long AyrilanBayt(Action govde)
    {
        ArgumentNullException.ThrowIfNull(govde);

        govde();                                    // ısınma: JIT'in ayırdıkları sayılmasın
        long once = GC.GetAllocatedBytesForCurrentThread();
        govde();
        return GC.GetAllocatedBytesForCurrentThread() - once;
    }
}
