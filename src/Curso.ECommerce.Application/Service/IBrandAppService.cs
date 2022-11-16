using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;

namespace Curso.ECommerce.Application.Service
{
    public interface IBrandAppService
    {
        ICollection<BrandDto> GetAll();

        Task<BrandDto> CreateAsync(BrandCreateUpdateDto brand);

        Task UpdateAsync (string brandId, BrandCreateUpdateDto brand);

        Task<bool> DeleteAsync(string brandId);

        PaginatedList<BrandDto> GetAllPaginated(int limit, int offset);
    }
}