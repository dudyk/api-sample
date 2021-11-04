using System;

namespace CarryLoad.Application.Driver.Hubs.Models
{
    public class DriverOrderRequestMessage
    {
        public int OrderRequestVehicleId { get; set; }
        public DateTime ExpiryAt { get; set; }
    }
}
