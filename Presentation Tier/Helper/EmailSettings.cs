using Data_Access_Tier.Entities;
using System.Net;
using System.Net.Mail;

namespace Presentation_Tier.Helper
{
	public class EmailSettings
	{
		public static void SendEmail (Email email)
		{
			var client = new SmtpClient ("smtp.gmail.com", 587);

			client.EnableSsl = true;

			client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential("user.email@gmail.com", "ueloricolhyhdif");

			client.Send("user.email@gmail.com", email.Recipient, email.Subject, email.Body);
		}
	}
}
