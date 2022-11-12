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

        Task UpdateAsync (Guid productId, ProductCreateUpdateDto product);

        Task<bool> DeleteAsync(Guid productId);

        Task<ProductDto> GetByIdAsync(Guid productId);

        Task<ICollection<ProductDto>> GetAllByIdAsync(List<Guid> productIdList);

        Task<ICollection<ProductDto>> GetAllByTypeAsync(string productType);
        Task<ICollection<ProductDto>> GetAllByTypeAsync(string productType, Guid productId);

        Task<ICollection<ProductDto>> GetAllByNameAsync(string productName);
        Task<ICollection<ProductDto>> GetAllByNameAsync(string productName, Guid productId);
        Task UpdateStockAsync(Guid productId, ProductUpdateStockDto product);

    }
}