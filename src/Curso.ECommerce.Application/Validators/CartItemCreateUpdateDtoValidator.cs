using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class CartItemCreateUpdateDtoValidator : AbstractValidator<CartItemCreateUpdateDto>
    {
        public CartItemCreateUpdateDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}