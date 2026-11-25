namespace Sozlesme;

// Uygulama ile eklentiler arasındaki SÖZLEŞME. İki taraf da yalnızca bu
// projeyi tanır: uygulama eklentileri, eklentiler uygulamayı tanımaz.
public interface IRaporEklentisi
{
    string Ad { get; }

    string Uret(IReadOnlyList<Odunc> odunclar);
}

public sealed record Odunc(string Baslik, string Uye, int GecikmeGun);
