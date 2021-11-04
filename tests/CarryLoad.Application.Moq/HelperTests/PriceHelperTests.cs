using CarryLoad.Application.Helpers;
using Xunit;

namespace CarryLoad.Application.Moq.HelperTests
{
    public class PriceHelperTests
    {
        [Theory]
        [InlineData(10, 100, 10, 100)]
        [InlineData(10, 100, 20, 100)]
        [InlineData(10, 100, 5, 50)]
        [InlineData(30, 150, 15, 75)]
        [InlineData(70, 300, 20, 85)]
        [InlineData(10, 100, 100, 100)]
        //[InlineData(10, -100, 20, -100)]
        //[InlineData(10, -100, -20, 200)]
        public void GetPriceForVehicle_ShouldReturnPrice_ProportionalToWeight(double orderWeight, decimal orderPrice, double vehicleWeight, decimal expectedPrice)
        {
            var resultPrice = PriceHelper.GetPriceForVehicle(orderWeight, orderPrice, vehicleWeight);

            Assert.Equal(expectedPrice, resultPrice);
        }

        [Theory]
        [InlineData(100, 50, 150)]
        [InlineData(100, 100, 200)]
        [InlineData(50, 50, 75)]
        [InlineData(50, 100, 100)]
        public void IncreaseForPercentage_ShouldReturnPrice_IncreasedToPercentage(decimal price, int percentage, decimal expectedPrice)
        {
            var resultPrice = PriceHelper.IncreaseForPercentage(price, percentage);

            Assert.Equal(expectedPrice, resultPrice);
        }
        
        [Theory]
        [InlineData(10, true, 275)] // MinimumPrice
        [InlineData(100, true, 500)] // Not MinimumPrice
        [InlineData(100, false, 1000)]
        public void CalcOrderPrice_ShouldReturnPrice_AsExpected(decimal distance, bool isIncludeHighway, decimal expectedPrice)
        {
            //Arrange
            var vehicle = new Models.Entities.Vehicle
            {
                Id = 1,
                CityPrice = 10,
                HighwayPrice = 5,
                MinimumPrice = 275
            };

            //Act
            var resultPrice = PriceHelper.CalcOrderPrice(vehicle, isIncludeHighway, distance);

            //Assert
            Assert.Equal(expectedPrice, resultPrice);
        }
    }
}
