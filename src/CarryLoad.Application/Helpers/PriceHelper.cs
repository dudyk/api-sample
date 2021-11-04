using System;
using CarryLoad.Models.Entities.Interfaces;

namespace CarryLoad.Application.Helpers
{
    public static class PriceHelper
    {
        public static decimal GetPriceForVehicle(double orderWeight, decimal orderPrice, double vehicleMaxWeight)
        {
            var weightCoefficient = Math.Min(vehicleMaxWeight / orderWeight, 1);

            return Math.Floor(orderPrice * Convert.ToDecimal(weightCoefficient));
        }
        
        public static decimal IncreaseForPercentage(decimal price, int percentage)
        {
            return price + price * percentage / 100;
        }

        public static decimal CalcOrderPrice(IVehiclePrice vehicle, bool isIncludeHighway, decimal distance)
        {
            var price = isIncludeHighway
                ? distance * vehicle.HighwayPrice
                : distance * vehicle.CityPrice;

            return Math.Max(price, vehicle.MinimumPrice);
        }
    }
}
