using System.Threading.Tasks;

namespace Involys.Poc.Api.Common
{
    // This class is used by the application to send emails.
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //  Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
            return Task.CompletedTask;
        }
    }
}
