using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface IBrandAppService
    {
        ICollection<BrandDto> GetAll();

        Task<BrandDto> CreateAsync(BrandCreateUpdateDto brand);

        Task UpdateAsync (string brandId, BrandCreateUpdateDto brand);

        Task<bool> DeleteAsync(string brandId);
    }
}