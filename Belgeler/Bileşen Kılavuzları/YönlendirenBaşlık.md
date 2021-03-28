# **YönlendirenBaşlık**

YönlendirenBaşlık, ilk kez tıklandığında yönlendireceği sayfayı belirten ve ikinci kez tıklandığında oraya yönlendiren bir düğmedir.

## Değişkenler
### **css_sınıfı**
>Değişken Türü: string  
Ön Tanımlı Değer: *yok*  
*Zorunlu Değil*

Ön tanımlı CSS deyimlerine ek olarak başka CSS sınıflarının deyimlerinin de kullanılmasına olanak sağlar. [Ön tanımlı CSS deyimleri](#CSS-Deyimleri) CSS sınıflarının üzerine yazılır.
### **şuraya_yönlendir**
>Değişken Türü: string  
Ön Tanımlı Değer: `/ana`  
*Zorunlu Değil*

Başlığa ikinci kez tıklandığında gidilecek sayfayı belirtir. Değer olarak gidilecek sayfanın adresini alır.
### **ilk_başlık**
>Değişken Türü: string  
Ön Tanımlı Değer: *yok*  
**Zorunlu**

Sayfanın başlığıdır. Başlığa tıklanmadan önce başlık budur.
### **ikinci_başlık**
>Değişken Türü: string  
Ön Tanımlı Değer: *yok*  
**Zorunlu**

Başlığa ilk kez tıklanınca başlık bu olur. İkinci tıklamanın nereye göndereceğini belirtmelidir. "Ana Sayfaya Git", "Geri Dön" gibi değerler alması uygundur.
### **ilk_renk**
>Değişken Türü: string  
Ön Tanımlı Değer: *yok*  
**Zorunlu**

Başlığa tıklanmadan önce başlığın rengi budur. CSS dilinde renk belirtecek biçimde yazılmalıdır. Sayfanın renkleriyle uyumlu olmalıdır.
### **ikinci_renk**
>Değişken Türü: string  
Ön Tanımlı Değer: `#ff6600`  
***Belirli Durumlarda Zorunlu***

Başlığa ilk kez tıklanınca başlığın rengi bu olur. Ön tanımlı değerden başka bir değer ile kullanılması dil kuralları bakımından zorunlu değildir ancak Yağ Kandili'nin tasarımı bakımından zorunlu olduğu durumlar vardır. Ne durumda ne yapılması gerektiği şöyledir:

* İkinci tıklama ile gidilecek sayfa bir ana sayfa ise uygulamanın ya da o bölümün ana rengi kullanılmalıdır. Yağ Kandili'nin ana rengi #ff6600 olduğundan ötürü ön tanımlı değer `#ff6600` 'dır.

* Gidilecek sayfa bir ana sayfa değilse gidilecek sayfanın ana rengi kullanılmalıdır.  
Ancak YönlendirenBaşlık'ın bulunduğu sayfa ile gidilecek sayfanın ana renkleri aynıysa ya da birbirine benziyorsa, uygulamanın ya da bölümün ana rengi kullanılmalıdır.

## Ön Tanımlı Sabitler
### CSS Deyimleri
```
font-family: sans-serif;
font-size: x-large;
font-weight: bold;
border: none;
```
Buradaki `border: none;`, *background-color*'ın ön tanımlı değerinin *transparent* olmasının da etkisiyle düğmenin sıradan bir yazı gibi görünmesini sağlamak için kullanılıyor.