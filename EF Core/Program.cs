#region ORM  YAKLASIMI
// Biz yazilim uygulamarinda verileri fiziksel olarak veri tabaninda tutariz , bu da cogunlukla SQL olur . Bu veri tabani ile yazilim arasinda baglanti saglamamiz gerekir ki gelen verileri kaydedip , istenilen veriyi geri gondermek icin . bu durumda ORM mantigi devreye gier 
// ORM -> object relational mapping anlamina gelir . Yazilimdan SQL sorgularini soyutlamak ve OOP nesnesi nimetlerinden yararlanilarak kullanilan bir yaklasimdir
// nesne dedigimizde bir class demistik bu durumda yapilmak istenen SQL database ler icin bir class olusturmak , tablolar icin ayri bir class olusturmak ve bu veri tabani classinin tablo classini referans etmesini saglariz ve iliskilendirme yapariz bu ORM mantigidir
// ORM deki iliskisel eslestirme mantigina gore ornek su sekildedir NorthwindDbContext context = new();   ->     var employeesDatas = await context.Employees.toListAsync(); bu yapilan islemde
// context ref adresi bize tum databaseyi isaret eder . sonrasinda yapilan islemde context.Employees dedigimizde employees de bulunan employeesId , firstname , lastname ... her biri satiri 1 nesne olacak sekilde bizim onumuze sunar
// ORM olarak EF core tercih etmemizin sebebi bircok ozellik barindirmasidir fakat en hizlisidir diyemeyiz ornegin Dapper daha hizli sonuc getirir ve ham sorgular olusturur fakat ef core kadar ozelligi yoktur 
#endregion
#region DATABASE FIRST
// Eger EF core ile calisma yapacaksak fakat veritabani oncesinde hazirlanmis tablolari olusturulmus ise DataBase First yaklasimini uygulamaliyiz. Database first yaklasimi oop prensibinden yararlanilarak yapilir
// ornegin oncesinde yapilan bir databasemiz var . bunun icin kod kisminda bir dbContext class olusturuyoruz islemleri burada yapacagiz .
// birde herbir tablo icin ayri ayri class yapip her tablodaki kolonlar icin de propery ler olusturuyoruz . bu dbcontext class ile tablolari bagladigimizda da codefirst islemimiz bitmis oluyor 
#endregion
#region CODE FIRST
// Database first yaklasiminin tam tersini yapmaktadir
// Eger bir projede veritabani henuz olusturulmamis ise bu veritabanini kod kisminda modeller daha sonra bu modele uygun veritabanini sunucuda olusturtur (migrate) bu yaklasima code first denir
// Kod uzerinden veritabanini modellememizi saglar demistik bu migration islemi databaseye bagliligi azaldir , veritabanina dokunmadan kod uzerinden guncelleme yapilmasini saglar
// bu durumda yonetim gelistirici tarafindadir , bu yuzden hicbir veritabani bilgisine ihtiyac duymaz  
#endregion
#region EF Core Aktorleri
// EF core da veritabanini temsil edecek olan sinif DbContext dir
// bir classin veritabanina karsilik gelen DbContext olabilmesi icin tek basina sinif olarak tanimlamamiz yeterli degildir . bunun icin Microsoft.EntityFrameworkCore namespacesindeki DbContext den kalitim almalidir
// public class NorthwindDbContext : DbContext seklinde olmalidir
// ef core da veritabanindaki tablolari temsil edecek classlari ifade etmeye entity(varlik) denir . her bir tabloyu olusturan class ayri ayri entity lerdir . bu entity siniflarinin adi tekil olmalidir (ornegin tabloda employees olsa bile entity de employee olmalidir)
// tum entity siniflari DbContext sinifi icerisine DbSet olarak eklenmelidir . ornek olarak DbSet<Employee> = Employees{get; set;}
// yukarida sunu demis oluyoruz veritabaninda bu entity(DbSet<T>) var ve buna karsilik gelen tabloyu property olarak temsil eder. Propery ler cogul yazilir entity gibi tekil degildir
// bir entity deki kolonlari tanimlamak istersek bunlari propery olarak tanimliyoruz
/* bir tablo ve kolon un kod tarafindaki entity ve property olarak kullanimi asagidaki gibidir
public class Costumer
{
    public string name { get; set; }
    public int costumerId { get; set; }
    public string surname { get; set; }
}
*/
//Veritabanindaki veriler de entity lerin instancelarina karsilik gelmektedir. yani Costumers entity si oldugunu varsayarsak bu tablonun her bir satirini new Costumer(); seklinde instance alarak olustururuz bunlar da verileri temsil etmektedir
//her yaptigimiz new Costumer(); instance i bir alt satiri temsil etmektedir
#endregion