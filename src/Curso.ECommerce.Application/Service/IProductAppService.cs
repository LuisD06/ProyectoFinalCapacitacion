using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface IProductAppService
    {
        ICollection<ProductDto> GetAll();

        Task<ProductDto> CreateAsync(ProductCreateUpdateDto product);

        Task UpdateAsync (int productId, ProductCreateUpdateDto product);

        Task<bool> DeleteAsync(int productId);
    }
}