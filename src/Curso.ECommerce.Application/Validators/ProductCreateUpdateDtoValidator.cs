using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class ProductCreateUpdateDtoValidator : AbstractValidator<ProductCreateUpdateDto>
    {
        public ProductCreateUpdateDtoValidator()
        {
            RuleFor(p => p.Name).Length(2,80);
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Expiration).GreaterThan(DateTime.Now.AddMonths(1));
            RuleFor(p => p.Stock).GreaterThan(0);
        }
    }
}