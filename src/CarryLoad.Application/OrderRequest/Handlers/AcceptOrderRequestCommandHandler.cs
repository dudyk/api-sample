using CarryLoad.Application.Extensions;
using CarryLoad.Application.OrderRequest.Commands;
using CarryLoad.Application.OrderRequest.Responses;
using CarryLoad.Models;
using CarryLoad.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarryLoad.Application.OrderRequest.Handlers
{
    public class AcceptOrderRequestCommandHandler : IRequestHandler<AcceptOrderRequestCommand, AcceptOrderRequestResult>
    {
        private readonly IRepository<Models.Entities.OrderRequestVehicle, int> _orderRequestVehicleRepository;
        public AcceptOrderRequestCommandHandler(IRepository<Models.Entities.OrderRequestVehicle, int> orderRequestVehicleRepository)
        {
            _orderRequestVehicleRepository = orderRequestVehicleRepository;
        }

        public async Task<AcceptOrderRequestResult> Handle(AcceptOrderRequestCommand request, CancellationToken cancellationToken)
        {
            var orderRequestVehicle = await _orderRequestVehicleRepository.Table
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == request.OrderRequestVehicleId
                                          && r.Vehicle.AssignedDriver.UserId == request.UserId, cancellationToken);

            if (orderRequestVehicle == null)
                throw ValidationException.Build(nameof(request.OrderRequestVehicleId), "Vehicle not found");

            orderRequestVehicle.StatusType = Enums.OrderRequestVehicleStatusTypes.UserAccepted;
            orderRequestVehicle.UpdatedAt = DateTime.UtcNow;
            orderRequestVehicle.AcceptedByDriverId = orderRequestVehicle.Vehicle.AssignedDriverId;

            await _orderRequestVehicleRepository.SaveChangesAsync();

            return new AcceptOrderRequestResult();
        }
    }
}
