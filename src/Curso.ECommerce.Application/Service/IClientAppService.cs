using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;

namespace Curso.ECommerce.Application.Service
{
    public interface IClientAppService
    {
        ICollection<ClientDto> GetAll();

        Task<ClientDto> CreateAsync(ClientCreateUpdateDto client);

        Task UpdateAsync (Guid clientId, ClientCreateUpdateDto client);

        Task<bool> DeleteAsync(Guid clientId);

        Task<ClientDto> GetByIdAsync(Guid clientId);

        PaginatedList<ClientDto> GetAllPaginated(int limit, int offset);

        List<ClientDto> GetAllByNameEmail(string name, string email);
        List<ClientDto> GetAllByCountryAddress(string country, string address);


    }
}