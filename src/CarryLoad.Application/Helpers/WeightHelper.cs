namespace CarryLoad.Application.Helpers
{
    public static class WeightHelper
    {
        public static double CalcOrderWeight(double vehicleMaxWeight, ref double orderRequestWeightLeft)
        {
            var delta = orderRequestWeightLeft - vehicleMaxWeight;
            if (delta > 0)
            {
                orderRequestWeightLeft = delta;
                return vehicleMaxWeight;
            }

            return orderRequestWeightLeft;
        }
    }
}