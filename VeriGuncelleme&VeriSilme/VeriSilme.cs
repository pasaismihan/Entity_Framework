using Microsoft.EntityFrameworkCore;

#region Veri Silme Islemi
EUrunContext context = new();
Urun urun=  await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5);
context.Urunler.Remove(urun); // silme islemini boyle yapiyoruz
await context.SaveChangesAsync();
#endregion
#region Takip Edilmeyen Nesnelerin Silinmesi Islemi
EUrunContext context1 = new();
Urun urun1 = new()
{
    Id = 2
};
context1.Urunler.Remove(urun1); // sadece id cagirmamiz yeterlidir ilgili Dbset Nesnesini kullanarak Remove fonksiyonu ile ilgili id ye ait instance i silmis oluyoruz
await context1.SaveChangesAsync();

#endregion
#region EntityState Ile Silme Islemi
Urun urun2 = new()
{
    Id = 1
};
context.Entry(urun2).State = EntityState.Deleted; // bu sekilde de delete sorgusu yapabiliriz Remove dan hicbir farki vardir
#endregion
#region RemoveRange
// AddRange de birden fazla instace yi tek seferde ekleyebilmek icin kullanmistik ayni mantikla RemoveRange de birden fazla veriyi silmek icin tek satirlik kod yaziyoruz
 List<Urun> urunler =  await context1.Urunler.Where(u => u.Id >= 7 && u.Id <= 9).ToListAsync();
context1.Urunler.RemoveRange(urunler);
#endregion
#region EntityState Ile Birden Fazla Veri Silme

#endregion


public class EUrunContext : DbContext
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