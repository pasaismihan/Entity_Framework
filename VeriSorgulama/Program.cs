using Microsoft.EntityFrameworkCore;

ETicaretContext context = new();
#region En Temel Basit Bir Sorgulama Nasil Yapilir ?
// METHOD SYNTAX 
var urunler = await context.Urunler.ToListAsync(); // metod syntaxi kullanarak tum Urun tablosunu cagirma
// QUERY SYNTAX
var urunler2 = await (from urun in context.Urunler select urun).ToListAsync(); // Linq Query
// Her iki sorgulama cesiti de ayni sonuclari getirir hangisini kullanmak istersek onu kullanabiliriz
#endregion
#region Sorguyu Execute Etmek Icin Ne Yapmamiz Gerekir ? 
// Asagidaki IQueryable ve IEnumerable asamalarina ornek gostermek icin ;
var urunler3 = from uruns in context.Urunler select uruns; // urunler3 ref uzerine geldigimizde IQueryable oldugunu goruruz yani sadece sorgu yapiyoruz burada fakat execute etmedik henuz
var urunler4 = await (from uruns in context.Urunler select uruns).ToListAsync(); // ToListAsync fonksiyonunu ekledigimizde IEnumerable oldu yani execute edildi bellekte yerini aldi 

#endregion
#region IQueryable ve IEnumerable nedir ? Basit haliyle!! 
// IQueryable ---> Sorguya karsilik gelir , EF Core da yapilan sorgunun execute edilmemis halidir yani ham hali diyebiliriz
// IEnumerable ---> Sorgunun calistirilip / execute edilip in memory e yuklenmis halini temsil eder
var urunler5 = await context.Urunler.ToListAsync(); // ToListAsync bir List Collection oldugu icin ve IEnumerable da List Collectionlar icerisinde bulundugu icin IEnumerable olmasini sagliyor
#endregion
// foreach dongusu de ToListAsync() gibi sorguyu execute ettirebilir
int urunId = 5;
string urunAdi = "2";
var urunler6 = from urun in context.Urunler where urun.Id > urunId && urun.UrunAdi.Contains(urunAdi) select urun;
foreach(Urun urun in urunler6)
{
    Console.WriteLine(urun); // foreach ile urunleri donguye soktugumuz icin sistem otomatik olarak execute etmesi gerektigini dusunuyor bu sekilde de urunleri consola yazdirabiliyoruz
}
#region Cogul Veri Getiren Sorgulama Fonksiyonlari

#endregion
#region Tekil Veri Getiren Sorgulama Fonksiyonlari

#endregion
#region Diger Sorgulama Fonksiyonlari

#endregion
#region Sorgu Sonucu Donusum Fonksiyonlari

#endregion
#region GroupBy Fonksiyonu

#endregion
#region Foreach Fonksiyonu

#endregion




public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID = sa; Password =1234");
    }
}
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public int Fiyat { get; set; }

}
public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}
