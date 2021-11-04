using CarryLoad.Application.Webhook.Responses;
using MediatR;

namespace CarryLoad.Application.Webhook.Queries
{
    public class GetWebhookQuery : IRequest<GetWebhookResult>
    {
        public int UserId { get; set; }
        public int WebhookId { get; set; }
    }
}
