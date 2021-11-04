using CarryLoad.Application.Driver.Responses;
using MediatR;

namespace CarryLoad.Application.Driver.Commands
{
    public class AddDriverCommand : EditDriverBaseCommand, IRequest<AddDriverResult>
    {
        public string Password { get; set; }
    }
}