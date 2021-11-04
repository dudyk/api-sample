using CarryLoad.Application.Order.Responses;
using MediatR;

namespace CarryLoad.Application.Order.Commands
{
    public class ChangeOrderVehicleCommand :IRequest<ChangeOrderVehicleResult>
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int VehicleId { get; set; }
    }
}
