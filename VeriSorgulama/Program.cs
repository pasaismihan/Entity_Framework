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
/* ToListAsync ---> Uretilen sorguyu execute etmemizi saglayan fonksiyondur
ustteki orneklerde gordugumuz gibi metod syntax ve query syntaxi ile ToListAsync() fonksiyonunu iki yerde de cagiriyoruz*/

// Where ---> Olusturulan sorguya Where sarti eklememizi saglayan fonksiyondur
var urunler7 = await context.Urunler.Where(u => u.Fiyat > 400).ToListAsync(); // Metod syntaxi ile where kullanimi
var urunler8 = from urun in context.Urunler where urun.UrunAdi.EndsWith("7") select urun; // EndsWith ilgili datanin sonunda arar , StartsWith ilkinde arar , Contains de icerisinde arar. (sql deki Like sorgusunu yapmis oluyoruz burada . isareti de %)
var data = await urunler8.ToListAsync();

// OrderBy ---> Sorgu uzerinde siralama yapmamizi saglayan fonksiyondur
var urunler9 = await context.Urunler.Where(u => u.Id > 5 || u.UrunAdi.Contains("p")).OrderBy(u => u.UrunAdi).ToListAsync(); // Metod syntaxi ile OrderBy kullanimi (metod syntaxinda func istiyor )
var urunler10 = from urun in context.Urunler where urun.Id > 5 || urun.UrunAdi.StartsWith("a") orderby urun.Id select urun;
var execute = await urunler10.ToListAsync();

// ThenBy ---> OrderBy fonksiyonunda yaptigimiz siralamayi birden fazla kolonda yapmak istersek ThenBy fonksiyonunu kullaniriz 
var urunler11 = await context.Urunler.Where(u => u.Id > 5 || u.UrunAdi.Contains("p")).OrderBy(u => u.UrunAdi).ThenBy(u=>u.Id).ThenBy(u=>u.Fiyat).ToListAsync(); // bu sekilde birden fazla ThenBy kullanabiliriz

// OrderByDescending ---> Buyukten kucuge siralama yapar
var urunler12 = await context.Urunler.OrderByDescending(u => u.Id).ToListAsync();


/* ThenByDescending */
var urunler13 = await context.Urunler.OrderByDescending(u => u.Id).ThenByDescending(u => u.UrunAdi).ToListAsync();
#endregion
#region Tekil Veri Getiren Sorgulama Fonksiyonlari
// Yapilan sorguda sadece tek bir verinin gelmesini istiyorsak bu durumda SingleAsync veya SingleOrDefaultAsync kullanilabilir 
// SingleAsync ---> Eger ki sorgu sonucunda birden fazla veri geliyorsa veya hic veri gelmiyorsa exception yani hata firlatir 
var uruns = await context.Urunler.SingleAsync(u => u.Id == 55); // 55 id li bir urunumuz oldugunu varsayarsak yalnizca o id ye ait veriyi dondurmus oluyoruz burada 
/***********************************************************************************************************************************************************************************/
// SingleOrDefaultAsync ---> Eger ki sorgu sonucunda birden fazla veri geliyorsa exception firlatir , hic veri gelmiyorsa da null dondurur. Buradaki Default null dondurmesini saglar
var uruns1 = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 55);
/***********************************************************************************************************************************************************************************/
// FirstAsync ---> Sorgu sonucunda tek bir veri dondurur fakat SingleAsync gibi ozellestirilmis degildir . ornegin ayni isimde 10 kisi var ise bunlardan ilkini dondurur. hic veri gelmiyorsa exception firlatir
var uruns2 = await context.Urunler.FirstAsync(u => u.Id > 55); // birden fazla urunu sorguladik burada ilkini dondurecegi icin 56 dondurur
/***********************************************************************************************************************************************************************************/
// FirstOrDefaultAsync ---> sorgu sonucu hic veri gelmiyorsa null dondurur . Ozellik olarak FirstAsync nin aynisi
var uruns3  = await context.Urunler.FirstOrDefaultAsync(u => u.Id > 55);
/***********************************************************************************************************************************************************************************/
// FindAsync ---> Primary Key kolonuna ozel hizli sekilde sorgu yapmamizi saglayan fonksiyondur ( id uzerinden sorgulama)
Urun urun1 = await context.Urunler.FindAsync(55);
UrunParca urunparca = await context.UrunParca.FindAsync(2, 5); // Composite Primary Key durumu , birlesmis tablolarla her ikisine ait idleri de cagirabiliriz 
/***********************************************************************************************************************************************************************************/
// FindAsync ile SingleAsync , SingleOrDefaultAsync , FirstAsync , FirstOrDefaultAsync Fonksiyonlarinin Karsilastirmasi
// FindAsync fonksiyonu once context icerisini kontrol eder(memory de arar) ve kaydi bulamadigi takdirde DB ye gider fakat digerlerinde direkt olarak DB den sorgulama yapariz bu da FindAsync fonksiyonunu daha performansli yapar
/***********************************************************************************************************************************************************************************/
// LastAsync ---> OrderBy kullanarak siralamak zorundayiz , sorguladigimiz coklu urunlerden sonuncusunu dondurur sadece , ornegin fiyat olarak sorgu yaptigimizda en cok fiyati olani dondurur.

// LastOrDefaultAsync ---> OrderBy kullanarak siralamak zorundayiz , sorguladigimiz coklu urunlerden sonuncusunu dondurur sadece , ornegin fiyat olarak sorgu yaptigimizda en cok fiyati olani dondurur.
// Hic veri gelmediginde null dondurur , LastAsync ise exception firlatir

#endregion
#region Diger Sorgulama Fonksiyonlari
// CountAsync ---> Olusturulan sorgu durumunda bize kac adet satir geleceginin bilgisini verir (int)
var uruns4 = (await context.Urunler.ToListAsync()).Count(); // bu yanlis bir yontemdir cunku ToListAsync diyerek biz ihtiyacimiz olan urun sayisina ek olarak urunleri de execute etmis olduk bu da maliyetlidir
var uruns5 = await context.Urunler.CountAsync(); // Dogrusu bu sekildedir , CountAsync ile yine execute ediyoruz fakat yalnizca sayisal deger donduruyoruz 
/***********************************************************************************************************************************************************************************/
// LongCountAsync ---> Milyonlarca veriyle ugrasiyorsak sorgu yaparken int yetersiz kalacak bu yuzden LongCountAsync kullaniriz long turunde sayisal deger aliriz 
var uruns6 = await context.Urunler.LongCountAsync(u => u.Fiyat % 7 == 5); // bu sekilde sartli bir sekilde de kac tane veri dondugunu de gorebiliriz
/***********************************************************************************************************************************************************************************/
// AnyAsync ---> Sorgu durumunda verinin gelip gelmedigini bize soyleyip boolean donduren fonksiyondur
var uruns7 = await context.Urunler.AnyAsync();
/***********************************************************************************************************************************************************************************/
// MaxAsync ---> Sorguda en yuksek veri hangisiyse onu donduren fonksyion 
var urun2 = await context.Urunler.MaxAsync(u => u.Id);
// MinAsync ---> Sorguda en dusuk veri hangisiyse onu donduren fonksyion
var urun3 = await context.Urunler.MinAsync(u => u.Id);
/***********************************************************************************************************************************************************************************/
// Distinct ---> Sorguda cok fazla kayitlar gelirse bunlari tekillestiren bir islevi olan fonksiyondur
var urun4 = await context.Urunler.Distinct().ToListAsync();
/***********************************************************************************************************************************************************************************/
// AllAsync ---> Sorgu durumunda gelen verilerin , verilen sarta uyup uymadigini kontrol eden fonksiyondur . eger tum veriler sarta uyuyorsa true uymuyorsa false donecektir 
var m = await context.Urunler.AllAsync(u => u.Fiyat > 5000); // urunlerin tamami 5000 den buyuk olmadigi icin False dondurecektir
/***********************************************************************************************************************************************************************************/
// SumAsync ---> Toplam fonksiyonudur , ornegin fiyatlarin toplamini alir
var fiyatToplam = await context.Urunler.SumAsync(u => u.Fiyat);

// AverageAsync ---> ortalama donduren fonksiyondur , ornegin fiyatlarin ortalamasini doner
var ortalamaFiyat = await context.Urunler.AverageAsync(u => u.Fiyat);
/***********************************************************************************************************************************************************************************/
// ContainsAsync ---> Like sarti yani '% ..... %' sorgusu olusturmamizi saglar , where sarti icerisidne yazmamiz gerekir
var like = await context.Urunler.Where(u => u.UrunAdi.Contains("a")).ToListAsync();
// StartsWith
var like1 = await context.Urunler.Where(u => u.UrunAdi.StartsWith("a")).ToListAsync();
// EndsWith
var like2 = await context.Urunler.Where(u => u.UrunAdi.EndsWith("a")).ToListAsync();
/***********************************************************************************************************************************************************************************/

#endregion
#region Sorgu Sonucu Donusum Fonksiyonlari
// Bu fonksiyonlar ile sorgu neticesinde getirilen verileri istegimiz dogrultusunda farkli turlerde projeksiyon edebiliyoruz
// ToDictionaryAsync ---> Sorgu durumunda gelen veriyi bir Dictionary turunde(key - value seklinde veri tutan bir koleksiyon turu) karsilamak istiyorsak kullanilir ,
// ToList ile ayni amaca hizmet etmektedir yani olusturulan sorguyu execute eder fakat ToList(List<Entity>) List kolensiyonu turunden donduruyor , ToDictionary ise Dictionary turunden bir koleksiyon seklinde dondurur
var u1 = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat); // key value pair turunde donduruyor
/***********************************************************************************************************************************************************************************/
// ToArrayAsync ---> Olusturulan sorguyu dizi olarak elde eder . yine execute etmek icin kullanilir fakat entity dizisi olarak elde eder
var u2 = await context.Urunler.ToArrayAsync();
/***********************************************************************************************************************************************************************************/
// Select ---> Sorgulama yaparken yalnizca istedigimiz kolonlari elde etmek istersek kullandigimiz fonksiyondur , birden fazla davranisi soz konusudur
var u3 = await context.Urunler.Select(u => new Urun
{
    UrunAdi = u.UrunAdi,
    Fiyat = u.Fiyat
}).ToListAsync();
// Select fonksiyonu gelen verileri farkli turlerde de karsilayabilir , ornegin T turunde veya anonim turde ( anonim tur nesne olusturulurken new Urun(){} yerine direkt new {}  yapabiliriz)
var u4 = await context.Urunler.Select(u => new
{
    UrunAdi = u.UrunAdi,
    Fiyat = u.Fiyat
}).ToListAsync();

// SelectMany ---> Select ile ayni amaca hizmet eder , ancak Iliskisel tablolar(one-to-one , many-to-one) neticesinde gelen koleksiyonel verileri de tekillestirip projeksiyon etmemizi saglar
var u5 = await context.Urunler.Include(u => u.Parcalar).SelectMany(u=>u.Parcalar,(u,p)=> new
{
    u.Id,
    u.Fiyat,
    p.ParcaAdi,
}).ToListAsync();  // Urunler entity si icerisinde bulunan ICollection koleksiyonu ile Parca entitiysini bagladik ve icerisindeki verileri urun verileri ile cekebilmek icin Include ve SelectMany fonksiyonlarini kullaniyoruz
// GroupBy

// Foreach
#endregion
#region GroupBy Fonksiyonu

#endregion
#region Foreach Fonksiyonu

#endregion




public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID = sa; Password =1234");
    }
}

public class UrunParca
{
    public int Id { get; set; }
}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public int Fiyat { get; set; }
    public ICollection<Parca> Parcalar { get; set; }

}
public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}
