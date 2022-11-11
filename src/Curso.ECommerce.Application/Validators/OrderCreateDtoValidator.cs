using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(o => o.Status).IsInEnum();
            RuleFor(o => o.OrderItems.Count).GreaterThan(0).WithMessage("Se ha tratado de crear una orden sin items. Property: {PropertyName}");
            RuleFor(o => o.CancellationDate).Must((ob, o) => {
                if (o < ob.Date) {
                    return false;
                }
                return true;
            }).WithMessage("La fecha de cancelación de la orden no puede ser anterior a la fecha de registro de la orden. Property: {PropertyName}");
        }
    }
}