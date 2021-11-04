using CarryLoad.Application.Extensions;
using CarryLoad.Application.Webhook.Commands;
using CarryLoad.Application.Webhook.Notifications;
using CarryLoad.Application.Webhook.Responses;
using CarryLoad.Application.Webhook.Services.Models;

namespace CarryLoad.Application.Webhook.Mappers
{
    public class WebhookMapper : AppMapperBase
    {
        public WebhookMapper()
        {
            CreateMap<AddWebhookCommand, Models.Entities.WebhookSubscription>()
                .Ignore(x=>x.Events);

            CreateMap<EditWebhookCommand, Models.Entities.WebhookSubscription>();

            CreateMap<Models.Entities.WebhookSubscription, GetWebhookResult>();

            CreateMap<WebhookEventNotification, WebHookArg>();

            CreateMap<Models.Entities.WebhookSubscription, WebHookArg>();
        }
    }
}
