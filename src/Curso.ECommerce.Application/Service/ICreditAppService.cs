using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface ICreditAppService
    {
        ICollection<CreditDto> GetAll();

        Task<CreditDto> CreateAsync(CreditCreateDto credit);

        Task UpdateAsync (Guid creditId, OrderUpdateDto order);

        Task<bool> PayAsync(Guid creditId);

        Task<CreditDto> GetByIdAsync(Guid creditId);
    }
}