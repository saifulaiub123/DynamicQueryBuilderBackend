using System.Threading.Tasks;

namespace Involys.Poc.Api.Common
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
