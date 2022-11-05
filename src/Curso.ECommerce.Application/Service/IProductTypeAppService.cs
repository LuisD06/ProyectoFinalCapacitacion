using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface IProductTypeAppService
    {
        ICollection<ProductTypeDto> GetAll();

        Task<ProductTypeDto> CreateAsync(ProductTypeCreateUpdateDto productType);

        Task UpdateAsync (int productTypeId,ProductTypeCreateUpdateDto productType);

        Task<bool> DeleteAsync(int productTypeId);
    }
}