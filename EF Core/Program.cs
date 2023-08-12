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

#endregion