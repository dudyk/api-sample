using CarryLoad.Application.Extensions;
using CarryLoad.Web.Infrastructure.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarryLoad.Web.Infrastructure.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder application)
        {
            application.UseExceptionHandler(appError =>
            {
                appError.Run(Handler);
            });
        }

        private static async Task Handler(HttpContext context)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature == null)
                return;

            ApiException result;
            if (contextFeature.Error is FluentValidation.ValidationException validationException)
            {
                result = new ApiException
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Validation error.",
                    Errors = validationException.Errors.Select(r => r.ToString())
                };
            }
            else
            {
                result = new ApiException
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal Server error.",
                    Errors = contextFeature.Error.GetAllMessages()
                };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.StatusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var text =  JsonSerializer.Serialize(result, options);

            await context.Response.WriteAsync(text);
        }
    }
}
