namespace IKIA.BLL.Common.Services.Emails
{
    public interface IEmailService
	{
		Task SendAsync(string from, string recipients, string subject, string body);
	}
}
