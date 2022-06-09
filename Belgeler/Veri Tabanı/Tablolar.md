# **Veri Tabanı için Tablo Tasarımları**

Yağ Kandili'ni çalıştırmak için gereken tüm tablolar için DESCRIBE komutunun çıktıları verilmiştir.

## **Üyelik Tablosu**
```
+------------------+------+------+-----+---------+-------+
| Field            | Type | Null | Key | Default | Extra |
+------------------+------+------+-----+---------+-------+
| Kullanıcı_Adı    | text | NO   |     | NULL    |       |
| Ad               | text | NO   |     | NULL    |       |
| Parola           | text | NO   |     | NULL    |       |
| Üstünlük         | text | YES  |     | NULL    |       |
| E_Posta          | text | NO   |     | NULL    |       |
| Başlangıç        | text | NO   |     | NULL    |       |
| Resim            | text | YES  |     | NULL    |       |
| Kimlik           | text | NO   |     | NULL    |       |
+------------------+------+------+-----+---------+-------+
```

## **Oturum Tablosu**
```
+----------------------+------+------+-----+---------+-------+
| Field                | Type | Null | Key | Default | Extra |
+----------------------+------+------+-----+---------+-------+
| Kullanıcı_Kimliği    | text | NO   |     | NULL    |       |
| Oturum_Kimliği       | text | NO   |     | NULL    |       |
| Başlangıç_Tarihi     | text | NO   |     | NULL    |       |
| Son_Tarih            | text | NO   |     | NULL    |       |
+----------------------+------+------+-----+---------+-------+
```

## **Paylaşım Tablosu**
```
+-----------+--------+------+-----+---------+----------------+
| Field     | Type   | Null | Key | Default | Extra          |
+-----------+--------+------+-----+---------+----------------+
| Kimlik1   | bigint | NO   | PRI | NULL    | auto_increment |
| Kimlik2   | text   | NO   |     | NULL    |                |
| Başlık    | text   | NO   |     | NULL    |                |
| İçerik    | text   | NO   |     | NULL    |                |
| Eklenti   | text   | NO   |     | NULL    |                |
| Paylaşan  | text   | NO   |     | NULL    |                |
| Tarih     | text   | NO   |     | NULL    |                |
| Lisans    | text   | NO   |     | NULL    |                |
+-----------+--------+------+-----+---------+----------------+
```

## **Söyleşi Tablosu**
```
+----------------------+------------+------+-----+---------+----------------+
| Field                | Type       | Null | Key | Default | Extra          |
+----------------------+------------+------+-----+---------+----------------+
| Söz                  | text       | NO   |     | NULL    |                |
| Söyleyen             | text       | NO   |     | NULL    |                |
| Tarih                | text       | NO   |     | NULL    |                |
| Söyleşi              | text       | NO   |     | NULL    |                |
| Başlatan_Paylaşım    | text       | NO   |     | NULL    |                |
| Genel_Sıra           | bigint     | NO   | PRI | NULL    | auto_increment |
| Bu_İlk               | tinyint(1) | NO   |     | NULL    |                |
+----------------------+------------+------+-----+---------+----------------+
```

## **Değerlendirme Tablosu**
```
+----------+------+------+-----+---------+-------+
| Field    | Type | Null | Key | Default | Extra |
+----------+------+------+-----+---------+-------+
| Kim      | text | NO   |     | NULL    |       |
| Neyi     | text | NO   |     | NULL    |       |
| Ne_Kadar | int  | NO   |     | NULL    |       |
| Ne_Zaman | text | NO   |     | NULL    |       |
+----------+------+------+-----+---------+-------+
```

## **Takip Tablosu**
```
+---------------+---------+------+-----+---------+-------+
| Field         | Type    | Null | Key | Default | Extra |
+---------------+---------+------+-----+---------+-------+
| Takip_Eden    | text    | NO   |     | NULL    |       |
| Takip_Edilen  | text    | NO   |     | NULL    |       |
| Takip_Düzeyi  | tinyint | NO   |     | NULL    |       |
| Tarih         | text    | NO   |     | NULL    |       |
+---------------+---------+------+-----+---------+-------+
```

## **Kişisel Veri İşlemleri Tablosu**
```
+----------------------+------+------+-----+---------+-------+
| Field                | Type | Null | Key | Default | Extra |
+----------------------+------+------+-----+---------+-------+
| Kullanıcı_Kimliği    | text | NO   |     | NULL    |       |
| İşlem                | text | NO   |     | NULL    |       |
| Tarih                | text | NO   |     | NULL    |       |
| İşlem_Kimliği        | text | NO   |     | NULL    |       |
+----------------------+------+------+-----+---------+-------+
```

## **Geçici Bağlantı Tablosu**
```
+------------------------+------+------+-----+---------+-------+
| Field                  | Type | Null | Key | Default | Extra |
+------------------------+------+------+-----+---------+-------+
| Bağlantı_Değişkeni     | text | NO   |     | NULL    |       |
| Hedef_Kullanıcı        | text | NO   |     | NULL    |       |
| Başlangıç_Tarihi       | text | NO   |     | NULL    |       |
| Bitiş_Tarihi           | text | NO   |     | NULL    |       |
| İçerik_Türü            | text | NO   |     | NULL    |       |
| Sağlanacak_Belge       | text | YES  |     | NULL    |       |
+------------------------+------+------+-----+---------+-------+
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