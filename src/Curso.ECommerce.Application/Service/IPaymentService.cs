using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface IPaymentService
    {
        Task<OrderDto> ConfirmOrder(OrderUpdateDto order); 
    }
}