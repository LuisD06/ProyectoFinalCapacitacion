using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartAppService service;

        public CartController(ICartAppService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<CartDto> CreateAsync(CartCreateDto cart)
        {
            return await service.CreateAsync(cart);
        }
        [HttpGet]
        public ICollection<CartDto> GetAll()
        {
            return service.GetAll();
        }
    }
}