using CarryLoad.Application.GoogleApi.Models.DistanceMatrix;
using CarryLoad.Application.GoogleApi.Models.Geocode;
using Refit;
using System.Threading.Tasks;

namespace CarryLoad.Application.GoogleApi
{
    /// REQUIRE GoogleMapsApiHandler
    public interface IGoogleMapsApi
    {
        public const string DistanceMatrixApiUrl = "/api/distancematrix";
        public const string GeocodeApiUrl = "/api/geocode";

        // https://maps.googleapis.com/maps/api/distancematrix/json?origins=50.23191125658878,28.672495954174614&destinations=49.79110176865563,30.1460149463082&key=AIzaSyCi-6mTcRA1gTw5UvJVD9-X4k6ecr6tEvg
        [Get(DistanceMatrixApiUrl + "/json?origins={origin}&destinations={destination}")]
        Task<DistanceMatrixResultModel> CalculateDistance(string origin, string destination);

        [Get(GeocodeApiUrl + "/json?result_type=route&latlng={latitude},{longitude}")]
        Task<GeocodeResultModel> ReverseGeocoding(double latitude, double longitude);
    }
}
