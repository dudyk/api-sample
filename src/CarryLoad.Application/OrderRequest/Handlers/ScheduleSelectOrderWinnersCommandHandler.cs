using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CarryLoad.Application.Core.Settings;
using CarryLoad.Application.OrderRequest.Commands;
using CarryLoad.Application.OrderRequest.Responses;
using FluentScheduler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarryLoad.Application.OrderRequest.Handlers
{
    public class ScheduleSelectOrderWinnersCommandHandler : IRequestHandler<ScheduleSelectOrderWinnersCommand, ScheduleSelectOrderWinnersResult>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly OrderRequestSettings _settings;
        private readonly IMapper _mapper;

        public ScheduleSelectOrderWinnersCommandHandler(
            IServiceScopeFactory scopeFactory,
            OrderRequestSettings settings,
            IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _settings = settings;
            _mapper = mapper;
        }

        public Task<ScheduleSelectOrderWinnersResult> Handle(ScheduleSelectOrderWinnersCommand request, CancellationToken cancellationToken)
        {
            JobManager.AddJob(async () =>
                {
                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var command = _mapper.Map<SelectOrderWinnersCommand>(request);
                    await mediator.Send(command, cancellationToken);
                },
                s => s.ToRunOnceIn(_settings.MinutesForAccept).Minutes()
            );

            return Task.FromResult(new ScheduleSelectOrderWinnersResult());
        }
    }
}