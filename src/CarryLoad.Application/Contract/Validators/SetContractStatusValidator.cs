using CarryLoad.Application.Contract.Commands;
using CarryLoad.Models;
using FluentValidation;

namespace CarryLoad.Application.Contract.Validators
{
    public class SetContractStatusValidator : AbstractValidator<SetContractStatusCommand>
    {
        public SetContractStatusValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.StatusType)
                .IsInEnum()
                .NotEqual(Enums.ContractStatusTypes.Offered);
        }
    }
}
