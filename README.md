# Yağ Kandili
---
## Derleme ve Kullanma
Yağ Kandili'ni derlemek ve kullanmak için öncelikle çalışan bir .NET Core 3.1, MySQL sunucusu ve Git kurulumu gerekmektedir.

### Gerekli yazılımların kurulumları
#### 1. Git kurulumu
Öncelikle, Git yazılımının kurulu olup olmadığını tespit etmek için bir terminalde `git version` komutunu çalıştırın. Sürüm bilgisi veriyorsa bu adımı atlayın, öyle bir komut bulunamadığıyla ilgili uyarı veriyorsa bu adımı uygulayın.

* GNU/Linux tabanlı işletim sistemlerinde paket yöneticisinden `git` paketini yüklemek yeterli olacaktır.

* Windows'ta ise [bu adresten](https://git-scm.com/download/win) kurulum aracını yüklemek ve ardından öntanımlı ayarlarla kurulum yapmak gerekiyor.

#### 2. .NET Core kurulumu
Yağ Kandili, .NET'in özgür dağıtımı olan .NET Core'un 3.1 sürümü üzerinde yapılmıştır. .NET 5'in kararlı sürümü yayınlandığında yine MIT lisansıyla devam ediyor olursa .NET 5'e geçiş yapılacaktır.
Kurulumdan önce bir terminalde `dotnet --info` komutunu çalıştırın. Sisteminizde yüklüyse, sürüm bilgisini verecektir. SDK sürümünün 3.1.x olması gerekiyor. .NET Core SDK yüklü değilse veya sürümü 3.1.x değilse bu adımı uygulayın.

* Pardus için [bu bağlantıdaki](https://forum.pardus.org.tr/t/pardus-ile-c-kullanmak/15804) anlatımın `yazılım geliştirme için` başlığının anltında yazan adımları uygulayın.

* Windows için [bu adresten](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.402-windows-x64-installer) kurulum aracını indirin ve kurulumu gerçekleştirin.
* Ubuntu, Alpine, CentOS, Debian, Fedora, OpenSUSE, RHEL veya SLES kullanıyorsanız [bu sayfadaki](https://docs.microsoft.com/tr-tr/dotnet/core/install/linux) anlatımlarla .NET Core SDK 3.1 kurulumu yapın.
* GNU/Linux tabanlı diğer işletim sistemleri için kaynak kodlarından kendiniz derlemeniz gerekebilir. Böyle yapacaksanız [dotnet GitHub hesabındaki](https://github.com/dotnet) [bu anlatımdan](https://github.com/dotnet/aspnetcore/blob/master/docs/BuildFromSource.md) ve [bu anlatımdan](https://github.com/dotnet/source-build) yardım almanızı öneririm. Kendim hiç denemedim ve nasıl bir sonuç alacağınızı bilmiyorum.

Kurulum adımlarını uyguladıktan sonra bir terminalde `dotnet --info` komutunu çalıştırın. Sürüm bilgisi verirse ve sürümü 3.1.x ise diğer adımlara geçebilirsiniz.

#### 3. MySQL kurulumu