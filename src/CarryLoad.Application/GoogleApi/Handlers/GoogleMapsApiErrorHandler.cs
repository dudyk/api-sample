using System;
using CarryLoad.Application.Helpers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CarryLoad.Application.GoogleApi.Models;
using Newtonsoft.Json;

namespace CarryLoad.Application.GoogleApi.Handlers
{
    public class GoogleMapsApiErrorHandler : QueryStringInjectingHandler
    {
        private const string SuccessResponseStatus = "OK";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<GoogleApiResponseStatus>(content);

            if (!result.Status.Equals(SuccessResponseStatus, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception($"Google response with {result.Status} status.", new Exception(content));

            return response;
        }
    }
}