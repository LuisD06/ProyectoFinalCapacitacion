using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandAppService service;
        public BrandController(IBrandAppService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ICollection<BrandDto> GetAll()
        {
            return service.GetAll();
        }

        [HttpPost]
        public async Task<BrandDto> CreateAsync(BrandCreateUpdateDto brand)
        {   
            return await service.CreateAsync(brand);
        }

        [HttpPut]
        public async Task UpdateAsync(int id, BrandCreateUpdateDto brand)
        {
            await service.UpdateAsync(id, brand);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(int brandId)
        {
            return await service.DeleteAsync(brandId);
        }
    }
}