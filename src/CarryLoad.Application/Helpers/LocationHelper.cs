using CarryLoad.Models.Entities.Interfaces;
using System;
using CarryLoad.Models.Entities;

namespace CarryLoad.Application.Helpers
{
    public static class LocationHelper
    {
        private const double EarthRadiusKm = 6372.8;

        public static double HaversineDistanceKm(ICoordinate pointA, ICoordinate pointB)
        {
            var dLat = ToRadians(pointB.Latitude - pointA.Latitude);
            var dLon = ToRadians(pointB.Longitude - pointA.Longitude);
            
            var latitudeA = ToRadians(pointA.Latitude);
            var latitudeB = ToRadians(pointB.Latitude);

            var a = Math.Sin(dLat / 2)
                * Math.Sin(dLat / 2)
                + Math.Sin(dLon / 2)
                * Math.Sin(dLon / 2)
                * Math.Cos(latitudeA)
                * Math.Cos(latitudeB);
            
            return EarthRadiusKm * 2 * Math.Asin(Math.Sqrt(a));
        }

        public static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static bool ContainsInRadius(this CoverageArea coverageArea, ICoordinate point)
        {
            var distance = HaversineDistanceKm(coverageArea, point);

            return distance <= coverageArea.WorkRadius;
        }
    }
}
