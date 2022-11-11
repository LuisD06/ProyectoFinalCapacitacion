using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class BrandCreateUpdateDtoValidator : AbstractValidator<BrandCreateUpdateDto>
    {
        public BrandCreateUpdateDtoValidator()
        {
            RuleFor(b => b.Name).Length(2,80);
        }
    }
}