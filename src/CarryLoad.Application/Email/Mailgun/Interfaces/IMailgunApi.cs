using System.Threading.Tasks;
using CarryLoad.Application.Email.Mailgun.Models;
using Refit;

namespace CarryLoad.Application.Email.Mailgun.Interfaces
{
    [Headers("Authorization: Basic")]
    public interface IMailgunApi
    {
        [Post("/v3/{domainName}/messages")]
        Task<MailgunSendResponse> SendAsync(string domainName, string from, string to, string subject, string html);

        [Post("/v4/address/validate")]
        Task<MailgunValidateResponse> ValidateEmailAsync(string emailAddress);
    }
}
