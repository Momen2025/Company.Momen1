using System.Net;
using System.Net.Mail;

namespace Company.Momen1.PL.Helper
{
    public static class EmailSettings
    {
        public static bool SenEmail(Email email)
        {
            //Mail Serevr: Gmail
            //SMTP
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("momenhabib88@gmail.com", "myzdmnfbyyxdwwr"); //sender
                                                                                                         // myzdmnfbyyxdwwr
                client.Send("momenhabib88@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
