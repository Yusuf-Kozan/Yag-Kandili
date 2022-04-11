/*
Copyright (C) 2022 Yusuf Kozan

---

Bu belge Yağ Kandili'nin bir parçasıdır.

Yağ Kandili bir özgür yazılımdır: GNU Affero Genel Kamu Lisansı'nın 3.
ya da (isteğinize göre) sonraki bir sürümünün Free Software Foundation
tarafından yayınlandığı durumunun koşulları altında Yağ Kandili'ni
dağıtabilir veya Yağ Kandili üzerinde değişiklik yapabilirsiniz.

Yağ Kandili kullanışlı olması umuduyla dağıtılmaktadır ancak HİÇBİR
GARANTİ VERMEMEKTEDİR, zımni PAZARLANABİLİRLİK veya BELİRLİ BİR
AMACA UYGUNLUK garantisi bile. Daha çok ayrıntı için GNU Affero
Genel Kamu Lisansı'na bakın.

Yağ Kandili'nin yanında GNU Affero Genel Kamu Lisansı'nın bir kopyasını
almış olmalısınız. Almadıysanız, <https://www.gnu.org/licenses/>
adresine bakın.

Yağ Kandili'nin lisansıyla ilgili daha çok bilgi için /Lisans
dizinine bakın.

---

This file is part of Yağ Kandili.

Yağ Kandili is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Yağ Kandili is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Yağ Kandili. If not, see <https://www.gnu.org/licenses/>.

For more information about the license of Yağ Kandili, see
/Lisans directory.
*/
using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace Esas.Posta
{
    public class PostaGönder
    {
        public static void TekKullanıcıyaGönder(GönderenBilgisi gönderen, ÜyeBil üye,
                            string konu, string içerik)
        {
            // Bu fonksiyonun yapımında şu iki sayfadan yardım alındı:
            //      https://github.com/jstedfast/MailKit#using-mailkit
            //      https://dotnetcoretutorials.com/2017/11/02/using-mailkit-send-receive-email-asp-net-core/

            var ileti = new MimeMessage();
            ileti.From.Add(new MailboxAddress("Yağ Kandili" , gönderen.ADRES));
            ileti.To.Add(new MailboxAddress(üye.AD, üye.E_POSTA));
            
            ileti.Subject = konu;
            ileti.Body = new TextPart("plain")
            {
                Text = içerik
            };

            using (var istemci = new SmtpClient())
            {
                istemci.Connect(gönderen.SUNUCU, int.Parse(gönderen.PORT), MailKit.Security.SecureSocketOptions.StartTls);
                istemci.Authenticate(gönderen.ADRES, gönderen.PAROLA);
                istemci.Send(ileti);
                istemci.Disconnect(true);
            }
        }
    }
}