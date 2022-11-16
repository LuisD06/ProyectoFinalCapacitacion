using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;

namespace Curso.ECommerce.Application.Service
{
    public interface IProductTypeAppService
    {
        ICollection<ProductTypeDto> GetAll();

        Task<ProductTypeDto> CreateAsync(ProductTypeCreateUpdateDto productType);

        Task UpdateAsync (string productTypeId,ProductTypeCreateUpdateDto productType);

        Task<bool> DeleteAsync(string productTypeId);
        PaginatedList<ProductTypeDto> GetAllPaginated(int limit, int offset);
        
    }
}