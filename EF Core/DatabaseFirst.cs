using System;
using Newtonsoft.Json;
namespace EF_Core
{
	public class DatabaseFirst
	{
		public DatabaseFirst()
		{
            //tersine muhendislik bir sunucuda database iskelesini kod kisminda olusturma surecidir , database first yaklasimi da bu sekilde hareket eder
            // bu database first yaklasim surecini package Manager Console(PMC) veya Dotnet CLI araciligiyla 2 farkli sekilde yuruturuz
            // biz veritabanindaki elemanlari sunucu tarafinda kodlama islemini yapmak icin Scaffold talimatini veririz , bu talimat hem PMC hem de dotnet CLI tarafinda verilir
            // Connection String => hangi sunucudaki hangi veritabaniyla calismak istiyorsak o veritabanini temsil eden string butunlugune denir
            // PMC tersine muhendislik ile hedef veritabanini kod kismina aktarmak istiyorsak vermemiz gereken talimat Scaffold-DbContext 'Connection String' Microsoft.EntityFrameworkCore.[Provider] buradaki provider kullanacagimiz sql turunu temsil ediyor onu yazacagiz
            // ilgili nuget package lari indirdikten sonra scaffold talimatini uyguluyoruz Scaffold-DbContext 'Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;' Microsoft.EntityFrameworkCore.SqlServer
            // yukaridaki kodu package manager consola yaziyoruz (sqlserver kullanmak istedigimiz icin onun connection stringini giriyoruz user id ve password databasemizde kurdugumuz id ve sifre olmalidir) daha sonra bize otomatik olarak entity ler geliyor
            // ayni islemi Dotnet CLI ile yapmak istersek dotnet ef dbcontext scaffold 'Connetion String' Microsoft.EntityFrameworkCore.SqlServer ile terminale yazdigimizda ayni sonucu aliriz
            // eger databaseden sadece belirledigimiz tablolari cekmek istersek PMC de kodumuzun sonuna -Tables Table1,Table2... seklide yapiyoruz dotnet CLI da ise sonuna --table Table1 --table Table2 yaziyoruz
            // Projede dbcontext ve entity ler default olarak projenin kok dizinine gelir fakat bunlari farkli path(ayri dosyalar) olarak ayarlamak istersek kodumuzun sonuna PMC icin -ContextDir context -OutputDir entities seklinde yazabiliriz boylece ayri dosyalar icerisinde olustururuz
            // yukaridaki islemi dotnet CLI icin olusturmak istersek kodumuzun sonuna --context-dir context --output-dir entities yazariz boylece contextimiz context dosyasi icinde , entitiler de entities dosyasi icerisinde olusur
            // ayni path i manuel olusturdugumuz gibi namespaceleri de kodlama ile olusturabiliriz -Namespace Entities -ContextNamespace Context ile ayri ayri namespacelerini manuel olarak olustururuz
            // eger ki entitiy ler uzerinde degisiklik yapmak istersek veya ekleme yaparsak databaseden PMC veya dotnet CLI ile tablolari cekersek bize hata verecektir degisiklik oldugu icin fakat kodumuzun sonunda -force eklersek
            // bizim yaptigimiz degisikligi ezer ve database tablolari ne ise entity olarak onlari verir . bu sekilde ezilmesini istemiyorsak Partial path olustururuz ve degistirmek istedigimiz entity yi buraya kopyalayip degisiklikleri bunun uzerinde yapariz
            // normalde ayni namespace altinda ayni isimde 2 farkli entity olusturamayiz fakat partial yapacaksak class in onune partial keywordunu ekleriz bu sekilde -force yapsak da yazdigimiz ekstra kodlar ezilmez 
        }
    }
}

