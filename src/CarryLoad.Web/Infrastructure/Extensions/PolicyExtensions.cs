using CarryLoad.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using CarryLoad.Application.Extensions;
using CarryLoad.Models;

namespace CarryLoad.Web.Infrastructure.Extensions
{
    public static class PolicyExtensions
    {
        public static void AddPolicyJwtRoleWithClaims(this AuthorizationOptions options, string name, Enums.RoleTypes role, params string[] requiredClaims)
        {
            options.AddPolicy(name, x =>
            {
                x.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                x.RequireRole(role.ToString());
                requiredClaims
                    .ForEach(r => 
                        x.RequireClaim(r, bool.TrueString));
            });
        }

        public static void AddPolicyJwtAnyRolesWithClaims(this AuthorizationOptions options, string name, Enums.RoleTypes[] roles, params string[] requiredClaims)
        {
            options.AddPolicy(name, x =>
            {
                x.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                x.RequireAssertion(p => roles
                    .Any(role => p.User.IsInRole(role.ToString())));
                requiredClaims
                    .ForEach(r =>
                        x.RequireClaim(r, bool.TrueString));
            });
        }

        public static void AddPolicyAccessTokenRole(this AuthorizationOptions options, string name, Enums.RoleTypes role)
        {
            options.AddPolicy(name, x =>
            {
                x.AuthenticationSchemes.Add(AccessTokenDefaults.AuthenticationScheme);
                x.RequireRole(role.ToString());
            });
        }
    }
}
