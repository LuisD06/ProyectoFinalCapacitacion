using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;
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
        [HttpGet("paginated")]
        public PaginatedList<CartDto> GetAllPaginated(int limit = 10, int offset = 0)
        {
            return service.GetAllPaginated(limit, offset);
        }
        [HttpPost]
        public async Task<CartDto> CreateAsync(CartCreateDto cart)
        {
            return await service.CreateAsync(cart);
        }
        [HttpPut]
        public async Task UpdateAsync(Guid cartId, CartUpdateDto cart)
        {
            await service.UpdateAsync(cartId, cart);
        }
        [HttpGet]
        public ICollection<CartDto> GetAll()
        {
            return service.GetAll();
        }

        [HttpGet("/Carts/date/item-count")]
        public List<CartDto> GetAllByDateItemCount(DateTime startDate, DateTime endDate, int minItemCount, int maxItemCount)
        {
            return service.GetByDateItemCount(startDate, endDate, minItemCount, maxItemCount);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(Guid cartId)
        {
            return await service.DeleteAsync(cartId);
        }
    }
}