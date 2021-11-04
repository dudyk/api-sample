using CarryLoad.Application.Contract.Commands;
using CarryLoad.Application.Contract.Responses;
using CarryLoad.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using CarryLoad.Application.Extensions;

namespace CarryLoad.Application.Contract.Handlers
{
    public class SetContractStatusHandler :IRequestHandler<SetContractStatusCommand, SetContractStatusResult>
   {
       private readonly IRepository<Models.Entities.Contract, int> _contractRepository;
        public SetContractStatusHandler(
            IRepository<Models.Entities.Contract, int> contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<SetContractStatusResult> Handle(SetContractStatusCommand command, CancellationToken cancellationToken)
        {
            var contract = await _contractRepository.Table
                .FirstOrDefaultAsync(x => x.Carrier.UserId == command.UserId
                                          && x.Id == command.Id, cancellationToken);

            if (contract == null)
                throw ValidationException.Build(nameof(command.Id), "Contract not found");

            contract.StatusType = command.StatusType;
            contract.UpdatedAt = DateTime.UtcNow;

            await _contractRepository.UpdateAsync(contract);

            return new SetContractStatusResult();
        }
    }
}
