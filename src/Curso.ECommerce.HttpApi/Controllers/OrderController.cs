using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAppService service;
        public OrderController(IOrderAppService service)
        {
            this.service = service;

        }
        [HttpPost]
        public async Task<OrderDto> CreateAsync(OrderCreateDto order)
        {
            return await service.CreateAsync(order);
        }
        [HttpGet]
        public ICollection<OrderDto> GetAll()
        {
            return service.GetAll();
        }
        [HttpPut]
        public async Task UpdateAsync(Guid orderId, OrderUpdateDto order)
        {
            await service.UpdateAsync(orderId, order);
        }

        [HttpPut("cancel/{orderId}")]
        public async Task<bool> CancelOrderAsync(Guid orderId)
        {
            return await service.DeleteAsync(orderId);
        }
    }
}