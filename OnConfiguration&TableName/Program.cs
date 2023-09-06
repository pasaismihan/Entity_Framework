using Microsoft.EntityFrameworkCore;
Console.WriteLine();
public class ETricaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=13142");
        // Provider(MSSQL , PostgreSql) hangi sql serveri kullanacaksak onu nuget package dan yuklememiz gerekiyor(ornegin mssql icin Microsoft.EntityFrameworkCore.SqlServer)
        // Connection String
        // Lazy Loading vb.
    }
}
public class Urun
{
    public int Id { get; set; }
    // Id , ID , UrunId , UrunID propertyleri default olarak sistemde primary key yerine konur herhangi birini yazabiliriz
    // EF Core a gore her tablonun bir primary key i olmasi gerekiyor eger olmazsa migration sirasinda hata verir ve migrate edemeyiz  
}

#region OnConfiguring ile Konfigurasyon ayarlarini gerceklestirmek
// EF Core Toolunu yapilandirmak icin kullanilan bir metottur.
// Context nesnesinde Override edilerek kullanilir
#endregion