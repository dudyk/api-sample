using CarryLoad.Application.Webhook.Commands;
using CarryLoad.Application.Webhook.Responses;
using CarryLoad.Web.Contracts.V1.Requests.WebHook;
using CarryLoad.Web.Contracts.V1.Responses.WebHook;

namespace CarryLoad.Web.Mappers
{
    public class WebHookMapper : MapperBase
    {
        public WebHookMapper()
        {
            CreateMap<AddWebHookRequest, AddWebhookCommand>();

            CreateMap<EditWebHookRequest, EditWebhookCommand>();
            CreateMap<EditWebhookResult, EditWebHookResponse>();

            CreateMap<GetWebHookResponse, GetWebhookResult>();

        }
    }
}
