using System.Net;
using System.Net.Mail;
using Project_.DAL.Models;

namespace Project.PL.Helpers
{
    public class EmailSe
    {
        public static void SendEmail(Email em)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential("Amrtaghbar@gmail.com", "rzwn rwwg hmbj cciu")
                };

                // إنشاء الرسالة البريدية
                var message = new MailMessage
                {
                    From = new MailAddress("Amrtaghbar@gmail.com"),
                    Subject = em.Subject,
                    Body = em.Body,
                    IsBodyHtml = true // تحديد أن المحتوى هو HTML
                };

                message.To.Add(new MailAddress(em.Recivers));

                // إرسال البريد الإلكتروني
                client.Send(message);
            }
            catch (Exception ex)
            {
                // التعامل مع الأخطاء في حالة حدوثها
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
