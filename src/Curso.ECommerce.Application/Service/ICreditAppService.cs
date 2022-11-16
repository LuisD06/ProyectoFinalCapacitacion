using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;

namespace Curso.ECommerce.Application.Service
{
    public interface ICreditAppService
    {
        ICollection<CreditDto> GetAll();

        Task<CreditDto> CreateAsync(CreditCreateDto credit);


        Task<bool> PayAsync(Guid creditId);

        Task<CreditDto> GetByIdAsync(Guid creditId);

        PaginatedList<CreditDto> GetAllPaginated(int limit, int offset);

        Task<bool> DeleteAsync(Guid creditId);
    }
}