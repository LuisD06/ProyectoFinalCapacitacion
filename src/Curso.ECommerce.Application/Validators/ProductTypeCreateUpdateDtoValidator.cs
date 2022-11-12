using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class ProductTypeCreateUpdateDtoValidator : AbstractValidator<ProductTypeCreateUpdateDto>
    {
        public ProductTypeCreateUpdateDtoValidator()
        {
            RuleFor(p => p.Name).Length(2,80);
        }
    }
}