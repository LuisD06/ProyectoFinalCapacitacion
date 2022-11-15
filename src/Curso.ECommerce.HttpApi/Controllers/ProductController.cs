using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService service;
        public ProductController(IProductAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        public ICollection<ProductDto> GetAll()
        {
            return service.GetAll();
        }

        [HttpPost]
        public async Task<ProductDto> CreateAsync(ProductCreateUpdateDto product)
        {   
            return await service.CreateAsync(product);
        }

        [HttpPut]
        public async Task UpdateAsync(Guid productId, ProductCreateUpdateDto product)
        {
            await service.UpdateAsync(productId, product);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(Guid productId)
        {
            return await service.DeleteAsync(productId);
        }

        [AllowAnonymous]
        [HttpGet("/Products/type/{productType}")]
        public async Task<ICollection<ProductDto>> GetAllByType(string productType)
        {
            return await service.GetAllByTypeAsync(productType);
        }

        [AllowAnonymous]
        [HttpGet("/Products/name/{productName}")]
        public async Task<ICollection<ProductDto>> GetAllByName(string productName)
        {
            return await service.GetAllByNameAsync(productName);
        }
    }
}