
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientAppService service;

        public ClientController(IClientAppService service)
        {
            this.service = service;
        }
        [HttpGet]
        public ICollection<ClientDto> GetAll()
        {
            return service.GetAll();
        }

        [HttpPost]
        public async Task<ClientDto> CreateAsync(ClientCreateUpdateDto client)
        {   
            return await service.CreateAsync(client);
        }

        [HttpPut]
        public async Task UpdateAsync(Guid clientId, ClientCreateUpdateDto client)
        {
            await service.UpdateAsync(clientId, client);
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync(Guid clientId)
        {
            return await service.DeleteAsync(clientId);
        }
    }
}