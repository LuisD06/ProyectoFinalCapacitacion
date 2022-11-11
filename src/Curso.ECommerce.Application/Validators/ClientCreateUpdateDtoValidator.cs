using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class ClientCreateUpdateDtoValidator : AbstractValidator<ClientCreateUpdateDto>
    {
        public ClientCreateUpdateDtoValidator()
        {
            RuleFor(c => c.Identification).Length(10);
            RuleFor(c => c.Name).Length(2,80);
            RuleFor(c => c.Address).Length(2,80);
            RuleFor(c => c.Country).Length(2,80);
            RuleFor(c => c.ZipCode).Length(2,12);
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Phone).Length(10);
        }
    }
}