using AutoMapper;
using CarryLoad.Application.Extensions;
using CarryLoad.Application.Order.Commands;
using CarryLoad.Application.Order.Responses;
using CarryLoad.Models;
using CarryLoad.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarryLoad.Application.Order.Handlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, CancelOrderCommandResult>
    {
        private readonly IRepository<Models.Entities.Order, int> _orderRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CancelOrderCommandHandler(
            IRepository<Models.Entities.Order, int> orderRepository,
            IMediator mediator,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CancelOrderCommandResult> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.Table
                .Include(x => x.OrderRequest.Points)
                .Include(x => x.OrderRequest.Dimensions)
                .FirstOrDefaultAsync(x => x.Id == request.OrderId && x.Vehicle.Carrier.UserId == request.UserId, cancellationToken);

            if (order is null)
                throw ValidationException.Build(nameof(request.OrderId), "Order not found");

            order.StatusTypes = Enums.OrderStatusTypes.Canceled;
            order.UpdatedAt = DateTime.UtcNow;
            await _orderRepository.UpdateAsync(order);

            var command = _mapper.Map<ScheduleCycleCommand>(order);
            await _mediator.Send(command, cancellationToken);

            return new CancelOrderCommandResult();
        }
    }

}