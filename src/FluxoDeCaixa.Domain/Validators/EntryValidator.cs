using FluentValidation;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Validators
{
    public class EntryValidator : AbstractValidator<Entry>
    {
        public EntryValidator()
        {
            RuleFor(_ => _.Description)
                .NotEmpty()
                .Length(2, 200);

            RuleFor(_ => _.EntryType)
               .IsInEnum();

            RuleFor(_ => _.Value)
                .GreaterThan(0);
        }
    }
}