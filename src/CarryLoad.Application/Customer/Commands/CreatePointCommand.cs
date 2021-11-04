using CarryLoad.Models.Entities.Interfaces;

namespace CarryLoad.Application.Customer.Commands
{
    public class CreatePointCommand : ICoordinate
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
