using System.Threading;
using System.Threading.Tasks;
using CarryLoad.Application.Identity.Commands;
using CarryLoad.Application.Identity.Responses;
using CarryLoad.Application.Identity.Services.Interfaces;
using CarryLoad.Application.User.Manager;
using MediatR;

namespace CarryLoad.Application.Identity.Handlers
{
    public class WebLoginHandler : IRequestHandler<WebLoginCommand, AuthenticationResult>
    {
        private readonly IAppUserManager _userManager;
        private readonly IIdentityService _identityService;

        public WebLoginHandler(
            IAppUserManager userManager,
            IIdentityService identityService)
        {
            _userManager = userManager;
            _identityService = identityService;
        }

        public async Task<AuthenticationResult> Handle(WebLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Incorrect password" }
                };
            }

            return await _identityService.GenerateAuthenticationResultAsync(user);
        }
    }
}