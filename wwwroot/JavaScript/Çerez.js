window.Çerezİşleri = {
    ÇerezYap: function(ad, değer, kaçSaat)
    {
        var son;
        if(kaçSaat)
        {
            var tarih = new Date();
            tarih.setTime(tarih.getTime() + (kaçSaat * 60 * 60 * 1000));
            son = "expires=" + tarih.toUTCString() + "; ";
        }
        else
        {
            son = "";
        }
        document.cookie = ad + "=" + değer + "; " + son + "path=/"; 
    },

    ÇerezOku: function(ad)
    {
        var çAd = ad + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; ca.length; i++)
        {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
              }
              if (c.indexOf(çAd) == 0) {
                return c.substring(çAd.length, c.length);
              }
        }
        return "";
    },

    ÇerezSil: function(ad)
    {
        document.cookie = ad + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    }
}