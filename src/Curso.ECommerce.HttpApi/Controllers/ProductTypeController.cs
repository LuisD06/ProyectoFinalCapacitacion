using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;
using Curso.ECommerce.Application.Service;
using Curso.ECommerce.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeAppService service;
        
        public ProductTypeController(IProductTypeAppService service)
        {
            this.service = service;
        }
        [HttpGet("paginated")]
        [AllowAnonymous]
        public PaginatedList<ProductTypeDto> GetAllPaginated(int limit = 10, int offset = 0)
        {
            return service.GetAllPaginated(limit, offset);
        }
        [HttpGet]
        [AllowAnonymous]
        public ICollection<ProductTypeDto> GetAll()
        {
            return service.GetAll();
        }

        [HttpPost]
        public async Task<ProductTypeDto> CreateAsync(ProductTypeCreateUpdateDto productType)
        {   
            return await service.CreateAsync(productType);
        }

        [HttpPut]
        public async Task UpdateAsync(string productTypeId, ProductTypeCreateUpdateDto productType)
        {
            await service.UpdateAsync(productTypeId, productType);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(string productTypeId)
        {
            return await service.DeleteAsync(productTypeId);
        }

    }
}