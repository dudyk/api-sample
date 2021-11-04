using CarryLoad.Application.Contract.Commands;
using CarryLoad.Application.Contract.Responses;
using CarryLoad.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CarryLoad.Application.Extensions;

namespace CarryLoad.Application.Contract.Handlers
{
    public class DeleteContractHandler : IRequestHandler<DeleteContractCommand, DeleteContractResult>
    {
        private readonly IDeletableRepository<Models.Entities.Contract, int> _contractRepository;

        public DeleteContractHandler(
            IDeletableRepository<Models.Entities.Contract, int> contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<DeleteContractResult> Handle(DeleteContractCommand command, CancellationToken cancellationToken)
        {
            var contract = await _contractRepository.Table
                .FirstOrDefaultAsync(x => x.Customer.UserId == command.UserId
                                          && x.Id == command.ContractId, cancellationToken);

            if (contract == null)
            {
                throw ValidationException.Build(nameof(command.ContractId), "Contract not found");
            }

            await _contractRepository.DeleteAsync(contract);

            return new DeleteContractResult();
        }
    }
}
