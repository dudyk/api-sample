using CarryLoad.Application.Driver.Hubs;
using CarryLoad.Application.Driver.Queries;
using CarryLoad.Application.Driver.Responses;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;
using CarryLoad.Application.Driver.Hubs.Models;

namespace CarryLoad.Application.Driver.Handlers
{
    public class NotifyDriversResultOrderQueryHandler : IRequestHandler<NotifyDriversResultOrderQuery, NotifyDriversResultOrderResult>
    {
        private readonly IHubContext<DriverHub> _hubContext;

        public NotifyDriversResultOrderQueryHandler(IHubContext<DriverHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<NotifyDriversResultOrderResult> Handle(NotifyDriversResultOrderQuery request, CancellationToken cancellationToken)
        {
            // todo translation and discuss about message text

            foreach (var item in request.WinnersOrders)
            {
                var winderMessage = new DriverResultOrderMessage { Message = "You have won an order!", OrderIds = item.OrderIds };

                await _hubContext.Clients
                    .User(item.UserId)
                    .SendAsync(Application.Hubs.DriverHub.ResultOrderMessage, winderMessage, cancellationToken);
            }

            var losersMessage = new DriverResultOrderMessage { Message = "Sorry, the lowest bid has been accepted." };

            await _hubContext.Clients
                .Users(request.LoserUserIds)
                .SendAsync(Application.Hubs.DriverHub.ResultOrderMessage, losersMessage, cancellationToken);

            return new NotifyDriversResultOrderResult();
        }
    }
}
