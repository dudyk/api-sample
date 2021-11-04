using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarryLoad.Application.Identity.Responses;

namespace CarryLoad.Application.Identity.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> GenerateAuthenticationResultAsync(Models.Entities.User user);
        Task<IEnumerable<Claim>> GetUserClaims(Models.Entities.User user);
    }
}
