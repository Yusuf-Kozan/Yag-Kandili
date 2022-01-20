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