using CarryLoad.Application.Driver.Commands;
using CarryLoad.Application.Driver.Responses;
using CarryLoad.Application.Extensions;
using CarryLoad.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CarryLoad.Application.Driver.Handlers
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, DeleteDriverResult>
    {
        private readonly IDeletableRepository<Models.Entities.Driver, int> _driverRepository;
        public DeleteDriverCommandHandler(
            IDeletableRepository<Models.Entities.Driver, int> driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<DeleteDriverResult> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.Table
                .FirstOrDefaultAsync(r => r.Carrier.UserId == request.UserId
                                          && r.Id == request.DriverId, cancellationToken);

            if (driver == null)
            {
                throw ValidationException.Build(nameof(request.DriverId), "Vehicle not found");
            }

            await _driverRepository.DeleteAsync(driver);

            return new DeleteDriverResult();
        }
    }
}
