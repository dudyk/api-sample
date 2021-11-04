using CarryLoad.Application.Identity.Responses;
using MediatR;

namespace CarryLoad.Application.Identity.Commands
{
    public class MobileLoginCommand : IRequest<AuthenticationResult>
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}