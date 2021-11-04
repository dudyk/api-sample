using CarryLoad.Application.Driver.Responses;
using MediatR;

namespace CarryLoad.Application.Driver.Queries
{
    public class GetDriverQuery : IRequest<GetDriverResult>
    {
        public int UserId { get; set; }
        public int DriverId { get; set; }
    }
}