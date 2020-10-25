# Yağ Kandili
---
## Yapılacaklar
- [ ] Oturum kapatma düğmesi eklenecek
- [ ] Paylaşım yapma özelliği eklenecek
- [ ] Yapılan paylaşımları gösterme özelliği eklenecek
- [ ] ML.NET kullanılarak içerik filtresi yapımına başlanacak
- [ ] PostgreSQL'e göç edilecek
---
## Derleme ve Kullanma
Yağ Kandili'ni derlemek ve kullanmak için öncelikle çalışan bir .NET Core 3.1, MySQL sunucusu ve Git kurulumu gerekmektedir.

**UYARI:** Windows ile ilgili adımlar hem PowerShell hem de CMD ile test edilmiştir. İkisini de kullanabilirsiniz.

### Gerekli yazılımların kurulumları
#### 1. Git kurulumu
Öncelikle, Git yazılımının kurulu olup olmadığını tespit etmek için bir terminalde `git version` komutunu çalıştırın. Sürüm bilgisi veriyorsa bu adımı atlayın, öyle bir komut bulunamadığıyla ilgili uyarı veriyorsa bu adımı uygulayın.

* GNU/Linux tabanlı işletim sistemlerinde paket yöneticisinden `git` paketini yüklemek yeterli olacaktır.

* Windows'ta ise [bu adresten](https://git-scm.com/download/win) kurulum aracını yüklemek ve ardından öntanımlı ayarlarla kurulum yapmak gerekiyor.

#### 2. .NET Core kurulumu
Yağ Kandili, .NET'in özgür dağıtımı olan .NET Core'un 3.1 sürümü üzerinde yapılmıştır. .NET 5'in kararlı sürümü yayınlandığında yine MIT lisansıyla devam ediyor olursa .NET 5'e geçiş yapılacaktır.
Kurulumdan önce bir terminalde `dotnet --info` komutunu çalıştırın. Sisteminizde yüklüyse, sürüm bilgisini verecektir. SDK sürümünün 3.1.x olması gerekiyor. .NET Core SDK yüklü değilse veya sürümü 3.1.x değilse bu adımı uygulayın.

* Pardus için [bu bağlantıdaki](https://forum.pardus.org.tr/t/pardus-ile-c-kullanmak/15804) anlatımın `yazılım geliştirme için` başlığının altında yazan adımları uygulayın.

* Windows için [bu adresten](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.402-windows-x64-installer) kurulum aracını indirin ve kurulumu gerçekleştirin.
* Ubuntu, Alpine, CentOS, Debian, Fedora, OpenSUSE, RHEL veya SLES kullanıyorsanız [bu sayfadaki](https://docs.microsoft.com/tr-tr/dotnet/core/install/linux) anlatımlarla .NET Core SDK 3.1 kurulumu yapın.
* GNU/Linux tabanlı diğer işletim sistemleri için kaynak kodlarından kendiniz derlemeniz gerekebilir. Böyle yapacaksanız [dotnet GitHub hesabındaki](https://github.com/dotnet) [bu anlatımdan](https://github.com/dotnet/aspnetcore/blob/master/docs/BuildFromSource.md) ve [bu anlatımdan](https://github.com/dotnet/source-build) yardım almanızı öneririm. Kendim hiç denemedim ve nasıl bir sonuç alacağınızı bilmiyorum.

Kurulum adımlarını uyguladıktan sonra bir terminalde `dotnet --info` komutunu çalıştırın. Sürüm bilgisi verirse ve sürümü 3.1.x ise diğer adımlara geçebilirsiniz.

#### 3. MySQL kurulumu
* Pardus ve Debian tabanlı diğer işletim sistemlerinde paket yöneticisinden `default-mysql-server` paketini yükleyin.

* Windows için [bu adresten](https://dev.mysql.com/downloads/) kurulum aracını yükleyin. Kurulum sihirbazında sunucu ve MySQL Shell kurulumu veya tam kurulum gerçekleştirin.

### Veri tabanının ayarlanması
MySQL kurulumundan sonra veri tabanlarını oluşturmak için MySQL kabuğuna kök hesabıyla giriş yapmak gerekli. Bunun için bir terminalde `mysql -u root -p` veya `mysqlsh --sql -u root -p` komutunu çalıştırın ve parolanızı girin. Ardından şu komutları girin:
```
CREATE DATABASE yagkandili;
use yagkandili;
CREATE TABLE oturumlar(Kullanıcı_Adı TEXT NOT NULL, Kilmik TEXT NOT NULL, Tarih TEXT NOT NULL, Son_Tarih TEXT NOT NULL, Kapandı TINYINT NOT NULL, Kapanma_Tarihi TEXT);
CREATE TABLE yazılı_paylaşım(Başlık TEXT NOT NULL, İçerik TEXT NOT NULL, Paylaşan TEXT NOT NULL, Kilmik TEXT NOT NULL, Tarih TEXT NOT NULL);
CREATE TABLE üyelik(Kullanıcı_Adı TEXT NOT NULL, Ad TEXT NOT NULL, Soyadı TEXT NOT NULL, Parola TEXT NOT NULL, Üstünlük TEXT, E_Posta TEXT NOT NULL, Başlangıç TEXT NOT NULL, Resim TEXT, Kimlik TEXT NOT NULL);
CREATE USER YagKandili;
GRANT SELECT, INSERT, UPDATE ON `yagkandili`.* TO YagKandili;
```

### Derleme
Veri tabanı da kurulduğu için derleme aşamasına geçebilirsiniz.
* Yağ Kandili'nin bulunmasını istediğiniz dizinde terminal açın.

* `git clone https://github.com/Yusuf-Kozan/Yag-Kandili.git` komutuyla kodları alın.
* `cd Yag-Kandili` komutuyla Yağ Kandili'nin bulunduğu dizine geçin.
* `dotnet run` komutunu çalıştırın.

Son komut önce Yağ Kandili'ni sizin sisteminize göre derleyecek, sonra da localhost üzerinde sunmaya başlayacak. Komutun çıktısında hangi adresten yayın yaptığını verecektir. O adresi bilgisayarınızdaki bir tarayıcıya girdiğinizde Yağ Kandili açılır.