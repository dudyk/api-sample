using System.Collections.Generic;
using System.Linq;
using CarryLoad.Application.Helpers;
using Xunit;

namespace CarryLoad.Application.Moq.HelperTests
{
    public class WeightHelperTests
    {
        [Theory]
        [InlineData(new double[] { 20 }, 20, new double[] { 20 })]
        [InlineData(new double[] { 20, 20 }, 40, new double[] { 20, 20 })]
        [InlineData(new double[] { 20, 15, 10 }, 40, new double[] { 20, 15, 5 })] // TODO discuss how we should split the weight
        public void CalcOrderWeight_ShouldReturnWeight_AsExpected(IEnumerable<double> vehicleMaxWeightList, double orderRequestWeight, IEnumerable<double> expectedVehicleWeightList)
        {
            var orderRequestWeightLeft = orderRequestWeight;

            var resultWeightList = vehicleMaxWeightList
                .Select(r =>
                    WeightHelper.CalcOrderWeight(r, ref orderRequestWeightLeft));
            
            Assert.Equal(expectedVehicleWeightList, resultWeightList);
        }
    }
}
