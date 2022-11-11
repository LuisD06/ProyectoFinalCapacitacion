using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using FluentValidation;

namespace Curso.ECommerce.Application.Validators
{
    public class OrderItemCreateUpdateDtoValidator : AbstractValidator<OrderItemCreateUpdateDto>
    {
        public OrderItemCreateUpdateDtoValidator()
        {
            RuleFor(i => i.Quantity).GreaterThan(0).WithMessage(i => "Item {PropertyName} debe ser mayor a 0. "+$"ProductId: {i.ProductId} ");
            RuleFor(i => i.ProductId).Must((i) => {
                return (i is Guid);
            }).WithMessage("El id de producto debe ser de tipo Guid. Property: {PropertyName}");
        }
    }
}