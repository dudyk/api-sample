using System.Collections.Generic;
using CarryLoad.Application.Helpers;

namespace CarryLoad.Application.GoogleApi.Handlers
{
    public class GoogleMapsApiHandler : QueryStringInjectingHandler
    {
        public GoogleMapsApiHandler(string apiKey, string language = "en")
            : base(
                new KeyValuePair<string, string>("key", apiKey),
                new KeyValuePair<string, string>("language", language))
        {
        }
    }
}