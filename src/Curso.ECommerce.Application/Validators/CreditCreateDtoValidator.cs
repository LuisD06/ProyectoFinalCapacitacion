using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class CreditCreateDtoValidator : AbstractValidator<CreditCreateDto>
    {
        public CreditCreateDtoValidator()
        {
            RuleFor(c => c.Payments).GreaterThan(2);
        }
    }
}