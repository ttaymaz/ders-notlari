namespace HataLab;

public abstract record Sonuc<T>;
public record Basarili<T>(T Deger) : Sonuc<T>;
public record Hatali<T>(string Neden) : Sonuc<T>;

public static class PuanOkuyucu
{
    // GÖREV 2 — Bir metni sınav puanına çevirin. İstisna FIRLATMAYIN;
    // her başarısızlığı bir Hatali sonucu olarak döndürün.
    //
    //   null, boş ya da yalnızca boşluk   → Hatali("boş")
    //   tamsayıya çevrilemiyor           → Hatali("sayı değil")
    //   0–100 aralığının dışında          → Hatali("aralık dışı")
    //   aksi halde                       → Basarili(puan)
    //
    // Baştaki ve sondaki boşluklar yok sayılır: " 85 " geçerli bir puandır.
    // İpucu: int.TryParse.
    public static Sonuc<int> Oku(string? metin)
    {
        throw new NotImplementedException();
    }

    // GÖREV 3 — Oku'yu kullanın; sonuç Hatali ise ArgumentException
    // fırlatın. İstisnanın mesajı Hatali'nin nedeni, ParamName'i "metin"
    // olsun. Basarili ise puanı döndürün. Bu metot, sonuç tipiyle çalışan
    // kodun, istisna bekleyen bir çağırana açıldığı SINIRDIR.
    // İpucu: switch ifadesi ve desen eşleme (6. konu).
    public static int OkuYaDaFirlat(string? metin)
    {
        throw new NotImplementedException();
    }
}
