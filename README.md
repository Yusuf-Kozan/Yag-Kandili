# Yağ Kandili
---

>**Belgeleme işlemine başlandı. [Belgeler](./Belgeler) dizininin altında yer alacaklar.**  
Özellikler, kurallar, tasarılar ve diğer bilgilendirmeler [Belgeler](./Belgeler) dizininde yayınlanacak.

## Yapılacaklar
- [x] Oturum kapatma düğmesi eklenecek
- [x] Paylaşım yapma özelliği eklenecek
- [x] Yapılan paylaşımları gösterme özelliği eklenecek
- [ ] Parolalar şifrelenecek (Büyük olasılıkla Argon2id ile)
- [ ] Hem kişinin kendi hem de başkaları için profil sayfaları oluşturulacak
- [ ] Takip etme ve beğenme özelliği eklenecek (belki yorum da eklenebilir)
- [ ] Tasarımı duyarlı olmayan kısımlar duyarlı biçime getirilecek
- [ ] Paylaşımlara lisans ekleyebilme özelliği eklenecek (Creative Commons öncelikli)
- [ ] İçerik filtresi için tasarım taslağı oluşturulacak
---
## Derleme ve Kullanma
Yağ Kandili'ni derlemek ve kullanmak için öncelikle çalışan bir .NET Core 3.1, MySQL sunucusu ve Git kurulumu gerekmektedir.

### Gerekli yazılımların kurulumları
#### 1. Git kurulumu
Öncelikle, Git yazılımının kurulu olup olmadığını tespit etmek için bir terminalde `git version` komutunu çalıştırın. Sürüm bilgisi veriyorsa bu adımı atlayın, öyle bir komut bulunamadığıyla ilgili uyarı veriyorsa bu adımı uygulayın.

* GNU/Linux tabanlı işletim sistemlerinde paket yöneticisinden `git` paketini yüklemek yeterli olacaktır.

#### 2. .NET Core kurulumu
Yağ Kandili, .NET'in özgür dağıtımı olan .NET Core'un 3.1 sürümü üzerinde yapılmıştır.
Kurulumdan önce bir terminalde `dotnet --info` komutunu çalıştırın. Sisteminizde yüklüyse, sürüm bilgisini verecektir. SDK sürümünün 3.1.x olması gerekiyor. .NET Core SDK yüklü değilse veya sürümü 3.1.x değilse bu adımı uygulayın.

* Pardus için [bu bağlantıdaki](https://forum.pardus.org.tr/t/pardus-ile-c-kullanmak/15804) anlatımın `yazılım geliştirme için` başlığının altında yazan adımları uygulayın.

* Ubuntu, Alpine, CentOS, Debian, Fedora, OpenSUSE, RHEL veya SLES kullanıyorsanız [bu sayfadaki](https://docs.microsoft.com/tr-tr/dotnet/core/install/linux) anlatımlarla .NET Core SDK 3.1 kurulumu yapın.
* GNU/Linux tabanlı diğer işletim sistemleri için kaynak kodlarından kendiniz derlemeniz gerekebilir. Böyle yapacaksanız [dotnet GitHub hesabındaki](https://github.com/dotnet) [bu anlatımdan](https://github.com/dotnet/aspnetcore/blob/master/docs/BuildFromSource.md) ve [bu anlatımdan](https://github.com/dotnet/source-build) yardım almanızı öneririm. Kendim hiç denemedim ve nasıl bir sonuç alacağınızı bilmiyorum.

Kurulum adımlarını uyguladıktan sonra bir terminalde `dotnet --info` komutunu çalıştırın. Sürüm bilgisi verirse ve sürümü 3.1.x ise diğer adımlara geçebilirsiniz.

#### 3. MySQL kurulumu
* Pardus ve Debian tabanlı diğer işletim sistemlerinde paket yöneticisinden `default-mysql-server` paketini yükleyin.


### Veri tabanının ayarlanması
MySQL kurulumundan sonra veri tabanlarını oluşturmak için MySQL kabuğuna kök hesabıyla giriş yapmak gerekli. Bunun için bir terminalde `mysql -u root -p` veya `mysqlsh --sql -u root -p` komutunu çalıştırın ve parolanızı girin. Ardından şu komutları girin:
```
CREATE DATABASE yagkandili;

USE yagkandili;

CREATE TABLE oturumlar(Kullanıcı_Kimliği TEXT NOT NULL, Oturum_Kimliği TEXT NOT NULL, Başlangıç_Tarihi TEXT NOT NULL, Son_Tarih TEXT NOT NULL);

CREATE TABLE üyelik(Kullanıcı_Adı TEXT NOT NULL, Ad TEXT NOT NULL, Soyadı TEXT NOT NULL, Parola TEXT NOT NULL, Üstünlük TEXT, E_Posta TEXT NOT NULL, Başlangıç TEXT NOT NULL, Resim TEXT, Kimlik TEXT NOT NULL);

CREATE TABLE paylaşımlar(Kimlik1 BIGINT NOT NULL AUTO_INCREMENT, Kimlik2 TEXT NOT NULL, Başlık TEXT NOT NULL, İçerik TEXT NOT NULL, Eklenti TEXT NOT NULL, Paylaşan TEXT NOT NULL, Oturum TEXT NOT NULL, Tarih TEXT NOT NULL, PRIMARY KEY (Kimlik1));

CREATE TABLE tby (Tür TEXT NOT NULL, Kimden TEXT NOT NULL, Neye TEXT NOT NULL, İçerik TEXT NOT NULL, Kimlik TEXT NOT NULL, Ne_Zaman TEXT NOT NULL, Oturum TEXT NOT NULL);

CREATE USER YagKandili;

GRANT SELECT, INSERT, UPDATE, DELETE ON `yagkandili`.* TO YagKandili;
```

### Derleme
Veri tabanı da kurulduğu için derleme aşamasına geçebilirsiniz.
* Yağ Kandili'nin bulunmasını istediğiniz dizinde terminal açın.

* `git clone https://github.com/Yusuf-Kozan/Yag-Kandili.git` komutuyla kodları alın.
* `cd Yag-Kandili` komutuyla Yağ Kandili'nin bulunduğu dizine geçin.
* `dotnet run` komutunu çalıştırın.