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
    }
}