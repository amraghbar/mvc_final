using Project_.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Project.PL.Helpers
{
	public class EmailSe
	{
		public static void SendEmail(Email em) {
	   var client =new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("Amrtaghbar@gmail.com", "rzwn rwwg hmbj cciu");
			client.Send("Amrtaghbar@gmail.com", em.Recivers, em.Subject, em.Body);





		}
	}
}
