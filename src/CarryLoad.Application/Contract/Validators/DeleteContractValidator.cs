using CarryLoad.Application.Contract.Commands;
using FluentValidation;

namespace CarryLoad.Application.Contract.Validators
{
    public class DeleteContractValidator : AbstractValidator<DeleteContractCommand>
    {
        public DeleteContractValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.ContractId)
                .GreaterThan(0);
        }
    }
}
