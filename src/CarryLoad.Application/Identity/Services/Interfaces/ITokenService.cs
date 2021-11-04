using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CarryLoad.Models.Entities;

namespace CarryLoad.Application.Identity.Services.Interfaces
{
    public interface ITokenService
    {
        (string, string) GenerateAccessToken(IEnumerable<Claim> claims);
        Task<string> GenerateRefreshTokenAsync(int userId, string tokenId);
        ClaimsPrincipal GetPrincipalFromToken(string token);
        Task<RefreshToken> GetRefreshTokenAsync(int userId, string refreshToken);
        Task UpdateRefreshTokenAsync(RefreshToken storedRefreshToken);
        Task RevokeRefreshTokenAsync(int userId, string refreshToken);
        Task RevokeRefreshTokensAsync(int userId);
    }
}
