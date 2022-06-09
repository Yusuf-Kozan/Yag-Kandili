# **Veri Tabanı Ayar Belgesi**

`./.Ayarlar/` dizininde bulunan `vt1` ve `vt2` belgeleri
veri tabanı bağlantısı için ayar belgeleridir.

## **vt1**
Bu belge dört satırlıdır. Her satır `>` karakteriyle başlar ve
boşluk bırakmadan sırayla sunucu adresi, port, kullanıcı adı, parola
bilgilerini içerir.

```
>sunucu
>port
>kullanıcı_adı
>parola
```

## **vt2**
Bu belge dokuz satırlıdır. İlk satır `>`, diğer satırlar `-` karakteriyle
başlar. İlk satırda veri tabanının adı, diğer satırlarda tabloların
adları bulunur. Tabloların aşağıdaki sırayla dizilmesi gerekir.

```
>veri_tabanı
-üyelik_tablosu
-oturum_tablosu
-paylaşım_tablosu
-söyleşi_tablosu
-değerlendirme_tablosu
-takip_tablosu
-kişisel_veri_işlemleri_tablosu
-geçici_bağlantı_tablosu
```

---
*Copyright (C) 2022 Yusuf Kozan*  

*Bu belge Yağ Kandili'nin parçasıdır ve
GNU Affero Genel Kamu Lisansı'nın 3. sürümü
altında dağıtılmaktadır.*  

*Bu belgeyi [GNU Affero Genel Kamu Lisansı](
/Lisans/agpl-3.0.md)'nın 3. ya da
(isteğinize göre) sonraki bir sürümünün
Free Software Foundation tarafından yayınlandığı
durumunun koşulları altında dağıtabilir veya
bu belge üzerinde değişiklik yapabilirsiniz.*