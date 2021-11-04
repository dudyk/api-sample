using CarryLoad.Application.OrderRequest.Queries;
using CarryLoad.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarryLoad.Application.Driver.Hubs
{
    [Authorize(Policy = Constants.Policy.Driver)]
    public class DriverHub : Hub
    {
        private readonly IMediator _mediator;
        
        public DriverHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            var query = new SendMissedDriverNotificationsQuery { UserId = GetLoggedUserId() };
            await _mediator.Send(query);

            await base.OnConnectedAsync();
        }

        private int GetLoggedUserId()
        {
            var userId = Context.User?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new KeyNotFoundException("UserId not in claims");
            }

            return int.Parse(userId.Value);
        }
    }
}