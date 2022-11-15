using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CreditController : ControllerBase
    {
        private readonly ICreditAppService service;
        public CreditController(ICreditAppService service){
            this.service = service;
        }
        [HttpGet]
        public ICollection<CreditDto> GetAll() 
        {
            return service.GetAll();
        }
        [HttpGet("{creditId}")]
        public async Task<CreditDto> GetByIdAsync(Guid creditId)
        {
            return await service.GetByIdAsync(creditId);
        }
        [HttpPost]
        public async Task<CreditDto> CreateAsync(CreditCreateDto credit)
        {
            return await service.CreateAsync(credit);
        }
        [HttpPut("pay")]
        public async Task<bool> PayAsync(Guid creditId)
        {
            return await service.PayAsync(creditId); 
        }

    }
}