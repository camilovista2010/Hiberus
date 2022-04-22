
using FluentValidation;
using Hiberus.Model.ModelsDto;

namespace Hiberus.Model.ValidationModels
{
    public class TransactionDtoValidate : AbstractValidator<TransactionDto>
    {
        public TransactionDtoValidate()
        {
            RuleFor(c => c.Sku).NotEmpty().NotNull().WithMessage("Sku required");
        }
    }
}
