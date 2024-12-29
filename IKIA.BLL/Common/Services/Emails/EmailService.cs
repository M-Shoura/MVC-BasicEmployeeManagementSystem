using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace IKIA.BLL.Common.Services.Emails
{
    public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
        {
			_configuration = configuration;
		}

        public async Task SendAsync(string from, string recipients, string subject, string body)
		{
			var senderEmail = _configuration["EmailSettings:SenderEmail"];
			var senderPassword = _configuration["EmailSettings:SenderPassword"];
			
			var emailMessage = new MailMessage();
			emailMessage.From = new MailAddress(from);
			emailMessage.To.Add(recipients);
			emailMessage.Subject = subject;
			emailMessage.Body = body;
			emailMessage.Body = $"<html><body><h2>{body}</h2></body></html>";
			emailMessage.IsBodyHtml = true;
			
			var smtpClient = new SmtpClient(_configuration["EmailSettings:SMTPClientServer"], int.Parse(_configuration["EmailSettings:SMTPClientPort"]))  
			{
				Credentials = new NetworkCredential(senderEmail, senderPassword),
				EnableSsl = true
			};
			smtpClient.Send(emailMessage);
		}
	}
}
