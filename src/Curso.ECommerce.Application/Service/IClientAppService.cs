using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface IClientAppService
    {
        // TODO: Añadir paginacion, debido a la magnitud de los datos
        ICollection<ClientDto> GetAll();

        Task<ClientDto> CreateAsync(ClientCreateUpdateDto client);

        Task UpdateAsync (Guid clientId, ClientCreateUpdateDto client);

        Task<bool> DeleteAsync(Guid clientId);

        Task<ClientDto> GetByIdAsync(Guid clientId);
    }
}