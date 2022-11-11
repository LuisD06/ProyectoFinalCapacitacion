using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Application.Service
{
    public class ClientAppService : IClientAppService
    {
        private readonly IClientRepository repository;

        public ClientAppService(IClientRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ClientDto> CreateAsync(ClientCreateUpdateDto client)
        {
            // Validaciones
            var clientExist = await repository.IdentificationExist(client.Identification);
            if (clientExist)
            {
                throw new ArgumentException($"Ya existe un cliente con el idetificador {client.Identification}");
            }

            // Mapeo Dto => Entidad
            var clientEntity = new Client();
            clientEntity.Address = client.Address;
            clientEntity.Country = client.Country;
            clientEntity.Email = client.Email;
            clientEntity.Identification = client.Identification;
            clientEntity.Name = client.Name;
            clientEntity.Phone = client.Phone;
            clientEntity.ZipCode = client.ZipCode;

            // Persistencia del objeto
            clientEntity = await repository.AddAsync(clientEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo Entidad => Dto
            var createdClient = new ClientDto();
            createdClient.Address = clientEntity.Address;
            createdClient.Country = clientEntity.Country;
            createdClient.Email = clientEntity.Email;
            createdClient.Identification = clientEntity.Identification;
            createdClient.Name = clientEntity.Name;
            createdClient.Phone = clientEntity.Phone;
            createdClient.ZipCode = clientEntity.ZipCode;
            createdClient.Id = clientEntity.Id;

            // TODO: Enviar un correo electronica... 

            return createdClient;
        }

        public async Task<bool> DeleteAsync(Guid clientId)
        {
            //Reglas Validaciones... 
            var clientEntity = await repository.GetByIdAsync(clientId);
            if (clientEntity == null)
            {
                throw new ArgumentException($"El cliente con el id: {clientId}, no existe");
            }

            repository.Delete(clientEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ICollection<ClientDto> GetAll()
        {
            var clietList = repository.GetAll();

            var clientListDto = from c in clietList
                               select new ClientDto()
                               {
                                   Address = c.Address,
                                   Country = c.Country,
                                   Email = c.Email,
                                   Id = c.Id,
                                   Identification = c.Identification,
                                   Name = c.Name,
                                   Phone = c.Phone
                               };

            return clientListDto.ToList();
        }

        public async Task UpdateAsync(Guid clientId, ClientCreateUpdateDto client)
        {
            var clientEntity = await repository.GetByIdAsync(clientId);
            if (clientEntity == null)
            {
                throw new ArgumentException($"El cliente con el id: {clientId}, no existe");
            }

            var clientExist = await repository.IdentificationExist(client.Identification, clientId);
            if (clientExist)
            {
                throw new ArgumentException($"Ya existe una cliente con la identificacion {client.Identification}");
            }

            //Mapeo Dto => Entidad
            clientEntity.Address = client.Address;
            clientEntity.Country = client.Country;
            clientEntity.Email = client.Email;
            clientEntity.Identification = client.Identification;
            clientEntity.Name = client.Name;
            clientEntity.Phone = client.Phone;
            clientEntity.ZipCode = client.ZipCode;

            //Persistencia objeto
            await repository.UpdateAsync(clientEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return;
        }
    }
}