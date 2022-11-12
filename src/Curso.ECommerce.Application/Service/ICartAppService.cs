using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface ICartAppService
    {
        ICollection<CartDto> GetAll();

        Task<CartDto> CreateAsync(CartCreateDto cart);

        Task UpdateAsync (Guid cartId, CartUpdateDto cart);

        Task<bool> DeleteAsync(Guid cartId);

        Task<CartDto> GetByIdAsync(Guid cartId);
    }
}