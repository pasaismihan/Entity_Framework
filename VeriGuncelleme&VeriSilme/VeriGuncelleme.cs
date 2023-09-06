using System;
using Microsoft.EntityFrameworkCore;

#region Veri Guncelleme Islemi
ETricaretContext context = new();
// Veriyi guncellememiz icin oncelikle veriyi databaseden cagirmaliyiz daha sonra guncelleme islemine tabi tutmaliyiz. Databaseden ilk veriyi cek demek icin kullandigimiz fonksiyon asagidadir
Urun urun= await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
urun.UrunAdi = "h urunu";
urun.Fiyat = 80; // burada yeni degerlerini yaziyoruz id si 3 olan urunun.
await context.SaveChangesAsync();
#endregion
#region ChangeTracker 
/* ChangeTracker context uzerinden gelen verilerin takibinden sorumlu mekanizmadir. Bu takip mekanizmasi sayesinde context nesnesi uzerinden gelen verilerle ilgili islemler sonucunda
update veya delete sorgularinin hangisi olacagi anlasilir*/
#endregion
#region Takip Edilmeyen Nesnelerin Guncellenmesi
ETricaretContext context1 = new();
Urun urun1 = new()
{
    Id = 3,
    UrunAdi = "yeni urun",
    Fiyat = 20
};// eger ki bu sekilde mauel olarak entity intance olusturabiliriz ve bildigimiz id yi degistirmek isteyebiliriz fakat bu durumda context den alinmadigi icin . alinmadigindan dolayi takip edilmedigi icin farkli bir yontemle yapmaliyiz
// Bu tur context den alinmadan guncelleme yapmak icin Update fonksiyonu cikmistir . Buradaki sebep databasedeki veriyi almadan manuel olarak bildigimiz id ye ait verileri guncelledigimizdendir

context1.Urunler.Update(urun1); // ChangeTracker tarafindan takip edilmeyen verilerin guncellenmesi icin Updata fonksiyonunu kullandik. fakat bunu kullanmak icin id yi belirtmemiz zorunludur.
await context1.SaveChangesAsync();
#endregion
#region EntityState 
// Bir entity instancesinin durumunu ifade eden referanstir
ETricaretContext context2 = new();
Urun u = new();
Console.WriteLine(context2.Entry(u).State);
#endregion
#region Birden fazla veri guncellenirken nelere dikkat etmemiz gerekir
// Yine veriyi databaseye eklemede oldugu gibi her bir ekleme veya guncellemeden sonra SaveChangesAsync() fonksiyonunu kullanirsak Transaction yaptigi icin oldukca maliyetlidir. En son 1 defa yazmamiz daha uygundur 
#endregion
var urunler = await context1.Urunler.ToListAsync(); // Urun entity sinde Select sorgusu atilmasi anlamina gelir yani Urunler uzerindeki tum instancelari cagirmamiz demektir. tum id lere ulasabiliriz
foreach(var uruns in urunler)
{
    uruns.UrunAdi += "*";
    await context2.SaveChangesAsync(); // eger burada savechanges kullanirsak dongu icerisinde oldugumuz icin inanilmaz bir maliyete sebep olur cunku her bir urunde Transaction tekrar calisir 
}

public class ETricaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=13142");

    }
}
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public int Fiyat { get; set; }
}