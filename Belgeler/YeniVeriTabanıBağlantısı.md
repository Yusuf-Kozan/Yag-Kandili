# Yeni Veri Tabanı Bağlantısı
Şu anda Yağ Kandili'nin veri tabanı işlemleri Esas ad alanındaki TabanlıVeri sınıfı içinde gerçekleşiyor. Veri tabanıyla ilgili her şeyin tek yerde olması işleri bir yere kadar kolaylaştırsa da kodlar arttıkça yönetmek ve hata ayıklamak zorlaşıyor. Bu nedenle veri tabanı işlemleriyle ilgili bölümü yeniden tasarlamaya karar verdim. Bu belgede TabanlıVeri'nin yerini alacak yeni yöntemi anlatacağım.

### **1. Dizin Yapısı**
Yeni yöntem, Yağ Kandili'nin Esas ad alanındaki bütünlüğünü bozmamak ancak kodları düzenli tutmak amacıyla Esas ad alanı içinde VeriTabanı adlı bir ad alanı içinde bulunacak.

VeriTabanı ad alanı ile ilgili kodlar [./Data/VeriTabanı](../Data/VeriTabanı) dizininde bulunacak. Her sınıf diğerlerinden ayrı ve kendiyle aynı adda bir sınıf kitaplığında bulunacak (örnek: ÖrnekSınıf sınıfı ÖrnekSınıf.cs belgesinde bulunacak).