using CarryLoad.Application.Driver.Responses;
using MediatR;

namespace CarryLoad.Application.Driver.Commands
{
    public class DeleteDriverCommand : IRequest<DeleteDriverResult>
    {
        public int DriverId { get; set; }
        public int UserId { get; set; }
    }
}
