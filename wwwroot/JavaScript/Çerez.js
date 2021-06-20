window.Çerezİşleri = {
    ÇerezYap: function(çerez_adı, değer, kaç_saat)
    {
        var son_tarih;
        if(kaç_saat)
        {
            var tarih = new Date();
            tarih.setTime(tarih.getTime() + (kaç_saat * 60 * 60 * 1000));
            son_tarih = "expires=" + tarih.toUTCString() + "; ";
        }
        else
        {
            son_tarih = "";
        }
        document.cookie = çerez_adı + "=" + değer + "; " + son_tarih + "path=/"; 
    },

    ÇerezOku: function(çerez_adı)
    {
        çerez_adı = çerez_adı + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++)
        {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
              }
              if (c.indexOf(çerez_adı) == 0) {
                return c.substring(çerez_adı.length, c.length);
              }
        }
        return "";
    },

    ÇerezSil: function(çerez_adı)
    {
        document.cookie = çerez_adı + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    },

    ÇerezVar: function(çerez_adı)
    {
        if (this.ÇerezOku(çerez_adı) === "")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}