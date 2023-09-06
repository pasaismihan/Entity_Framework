using Microsoft.EntityFrameworkCore;
#region Veri Ekleme
ETricaretContext context = new(); // veri ekleme gibi yapacagimiz tum islemlerde context nesnesi olusturmak zorundayiz...!!!
Urun urun = new()
{
    UrunAdi = "Domates",
    Fiyat = 100
};
// Yukarida bir context nesnesi icin instance olusturduk , ve bu context nesnesi icerisine gonderecegimiz yeni bir veri olusturduk . bu talimati yapmasi icin asagidaki gibi 2 farkli yontemimiz vardir
#region context.AddAsync Fonksiyonu
// await context.AddAsync(urun);
#endregion
#region context.DbSet.AddAsync Fonksiyonu
await context.Urunler.AddAsync(urun);
#endregion
#endregion

await context.SaveChangesAsync(); // SaveChanges ---> insert , update , delete sorgularini olusturup bir transaction esliginde veritabanina gonderip execute eden fonksiyondur.
                                  // Eger ki olusturulan sorgulardan herhangi biri basarisiz olursa bu islemi geri alir (Rollback) (transaction toplu sorgu yapildiginda birinde hata olmasi durumunda etki eder islemin iptal olmasini saglar) 
#region EF Core acisindan bir verinin eklenmesi gerektigi nasil anlasiliyor 
ETricaretContext context1 = new();
Urun urun1 = new()
{
    UrunAdi = "B",
    Fiyat = 50
};
await context1.AddAsync(urun1);
await context1.SaveChangesAsync();
Console.WriteLine(context1.Entry(urun1).State); // bu metod bize bulundugumuz satirdaki context bilgisini verir yani AddAsync fonksiyonu oncesi kullanirsak Detached yani tespit edilmis.
                                                // eger AddAsync fonksiyonu sonrasi eklersek consolda Added yazdirir , SaveChangesAsync fonksiyonu sonrasinda da unchanged der cunku ilgli sorgu artik databaseye gitmis olur

#endregion
#region Birden Fazla Veri Eklerken Neye Dikkat Edilmelidir
/* Bir sorgu yapildiginda SaveChanges fonksiyonunu yazmamiz gerektigini soylemistik ve bu fonksiyon Transaction ile birlikte yapiliyor fakat Transaction maliyetli oldugu icin ornegin 3 tane sorgu
yaptigimizda her AddAsync(urun) den sonra SaveChanges dersek herbir sorgu icin ayri ayri Transaction calisirir bu yuzden sorgularimizi yapip en son SaveChanges() fonksiyonunu cagirmamiz lazim*/
#endregion

// AddRangeAsync(); birden fazla instance dan olusan sorgularda ayri ayri yazmak istemez isek AddRangeAsync() fonksiyonunu kullaniriz
// await context.AddRangeAsync(urun1,urun2,urun3);
// await context.SaveChangesAsync(); bu sekilde database ye aktarma islemimizi tamamlamis oluyoruz



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
