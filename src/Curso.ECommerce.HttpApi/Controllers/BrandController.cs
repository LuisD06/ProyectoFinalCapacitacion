using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;
using Curso.ECommerce.Application.Service;
using Curso.ECommerce.Domain.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IBrandAppService service;
        public BrandController(IBrandAppService service)
        {
            this.service = service;
        }
        
        [HttpGet("paginated")]
        [AllowAnonymous]
        public PaginatedList<BrandDto> GetAllPaginated(int limit = 10, int offset = 0)
        {
            return service.GetAllPaginated(limit, offset);
        }
        
        [HttpGet]
        [AllowAnonymous]
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
        public async Task UpdateAsync(string branId, BrandCreateUpdateDto brand)
        {
            await service.UpdateAsync(branId, brand);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(string brandId)
        {
            return await service.DeleteAsync(brandId);
        }
    }
}