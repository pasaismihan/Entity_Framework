using System;
using Microsoft.EntityFrameworkCore;

namespace Application
{
	public class CodeFirst
	{
		public CodeFirst()
		{
            //Codefirst de domain classlari yani kodla sifirdan olusturacagimiz tablolari ve kolonlari migrate ederek databaseye aktariyoruz
            // Migration ---> Backend de olusturdugumuz Dbcontext class i ve entitiylerimizi databaseye aktarmak icin migration asamasini kullaniyoruz oncesinde Migration Classina aktariyoruz
            // Migrate ---> Migration Classinda yapilan tasarimini databaseye aktarmaya denir . Zaten anlami da goc ettirmekdir
            // Backend de Dbcontext ve entityleri olusturduktan sonra migration islemini yapmak icin yine Database yonteminde kullandigimiz Package Manager Console veya Dotnet CLI kullaniyoruz...
            /* Bir Dbcontext ve Entity Modellemesini Asagidaki gibi yapabiliriz */
            public class ECommerceDbContext : DbContext
        {
            public DbSet<Product>Products {get;set;}
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu metod context nesnesinin configurasyonlarini yapmamizi sagliyor  
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ECommerceDb;User Id=sa,Password=12345"); // hangi databaseye migrate edecegimizi belirtiyoruz . tirnak icinde connection stringi giriyoruz
                // Migrations olusturabilmek icin Microsoft.EntityFrameworkCore.Tools kutuphanesini nugettten yuklememiz gerekiyor
                // Package Manager Console ile migration yapmak icin --->> add-migration [migration adi]
                // Dotnet CLI ile migration yapmak icin --->> dotnet ef migrations add [migration adi]
                // bu sekilde migration olusturdugumuzda projemizde olusturdugumuz dosya adinda bir migration dosyasi acilir burada migrate edilecek dosyanin tum ozellikleri vardir
                // Migration silme --->> PMC ile remove-migrate , Dotnet CLI ile dotnet ef migrations remove ile yapilir tum olusan migrationlari siler
                // Migration lari migrate etme !!!!!!---> PMC ile update-database , Dotnet CLI ile dotnet ef database update
                /* ONEMLI ---> biz olusturdugumuz entityler ile bir migrations yaptiysak bunu CreateTable ile yakalar fakat entity uzerinde degisiklik yapip 2. bir migration dosyasi olusturursak
                bunu Up metodu icerisinde AddColumn ile yakalar*/
            }
        }
        public class Product
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public int Quentity { get; set; }

        }

        }

	}
}

