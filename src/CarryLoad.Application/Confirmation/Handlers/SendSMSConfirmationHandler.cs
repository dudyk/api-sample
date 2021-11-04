using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarryLoad.Application.Confirmation.Commands;
using CarryLoad.Application.Confirmation.Responses;
using CarryLoad.Application.SMS;
using CarryLoad.Application.User.Manager;
using MediatR;

namespace CarryLoad.Application.Confirmation.Handlers
{
    public class SendSMSConfirmationHandler : IRequestHandler<SendSMSConfirmationCommand, SendConfirmationResponse>
    {
        private readonly IAppUserManager _userManager;
        private readonly ISMSSender _smsSender;
        private readonly IMapper _mapper;

        public SendSMSConfirmationHandler(
            IAppUserManager userManager,
            ISMSSender smsSender,
            IMapper mapper)
        {
            _userManager = userManager;
            _smsSender = smsSender;
            _mapper = mapper;
        }

        public async Task<SendConfirmationResponse> Handle(SendSMSConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var result = await _smsSender.SendVerificationCodeAsync(user.PhoneNumber);
            return _mapper.Map<SendConfirmationResponse>(result);
        }
    }
}
