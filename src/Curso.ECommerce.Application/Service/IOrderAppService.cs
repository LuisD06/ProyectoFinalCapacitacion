using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;

namespace Curso.ECommerce.Application.Service
{
    public interface IOrderAppService
    {
        ICollection<OrderDto> GetAll();

        Task<OrderDto> CreateAsync(OrderCreateDto order);

        Task UpdateAsync (Guid orderId, OrderUpdateDto order);

        Task<bool> DeleteAsync(Guid orderId);

        Task<OrderDto> GetByIdAsync(Guid orderId);

        PaginatedList<OrderDto> GetAllPaginated(int limit, int offset);

        List<OrderDto> GetByClientTotal(string identification, decimal minTotal = 0, decimal maxTotal = 0);
    }
}