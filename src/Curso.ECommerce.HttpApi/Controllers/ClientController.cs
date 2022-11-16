
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
    public class ClientController : ControllerBase
    {
        private readonly IClientAppService service;

        public ClientController(IClientAppService service)
        {
            this.service = service;
        }
        [HttpGet("paginated")]
        public PaginatedList<ClientDto> GetAllPaginated(int limit = 10, int offset = 0)
        {
            return service.GetAllPaginated(limit, offset);
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

        [HttpGet("/Clients/name/email")]
        public List<ClientDto> GetAllByNameEmail(string? name, string? email)
        {
            return service.GetAllByNameEmail(name, email);
        }
        [HttpGet("/Clients/country/address")]
        public List<ClientDto> GetAllByCountryAddress(string? country, string? address)
        {
            return service.GetAllByCountryAddress(country, address);
        }
    }
}