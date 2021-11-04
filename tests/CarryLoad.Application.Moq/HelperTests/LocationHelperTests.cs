using CarryLoad.Application.Helpers;
using CarryLoad.Models.Entities;
using Xunit;

namespace CarryLoad.Application.Moq.HelperTests
{
    public class LocationHelperTests
    {
        private static readonly CoverageArea CoverageArea = new()
        {
            Latitude = 49.4106425,
            Longitude = 26.9252192,
            WorkRadius = 100
        };

        [Theory]
        [InlineData(49.2347848, 28.4346146, false)]
        [InlineData(49.408467961206725, 27.012171205702877, true)]
        [InlineData(48.6912765, 26.5449853, true)]
        public void CoverageArea_ContainsInRadius_ShouldReturn_AsExpected(double latitude, double longitude, bool expectedResult)
        {
            var point = new Point
            {
                Latitude = latitude,
                Longitude = longitude
            };

            var result = CoverageArea.ContainsInRadius(point);

            Assert.Equal(result, expectedResult);
        }
    }
}
