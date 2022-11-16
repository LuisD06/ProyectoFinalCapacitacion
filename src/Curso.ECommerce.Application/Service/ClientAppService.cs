using AutoMapper;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using FluentValidation;

namespace Curso.ECommerce.Application.Service
{
    public class ClientAppService : IClientAppService
    {
        private readonly IClientRepository repository;
        private readonly IMapper mapper;
        private readonly IValidator<ClientCreateUpdateDto> clientCUDtoValidator;

        public ClientAppService(IClientRepository repository, IMapper mapper, IValidator<ClientCreateUpdateDto> clientCUDtoValidator)
        {
            this.clientCUDtoValidator = clientCUDtoValidator;
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<ClientDto> CreateAsync(ClientCreateUpdateDto client)
        {
            // Validaciones
            var validationResult = await clientCUDtoValidator.ValidateAsync(client);
            if (!validationResult.IsValid) {
                var errorList = validationResult.Errors.Select(
                    e => e.ErrorMessage
                );
                var errorString = string.Join(" - ", errorList);
                throw new ArgumentException(errorString);
            }

            var clientExist = await repository.IdentificationExist(client.Identification);
            if (clientExist)
            {
                throw new ArgumentException($"Ya existe un cliente con el idetificador {client.Identification}");
            }

            // Mapeo Dto => Entidad
            var clientEntity = mapper.Map<Client>(client);

            // Persistencia del objeto
            clientEntity = await repository.AddAsync(clientEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo Entidad => Dto
            var createdClient = mapper.Map<ClientDto>(clientEntity);

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
            var clientList = repository.GetAll();

            var clientListDto = clientList.Select(c => mapper.Map<ClientDto>(c));

            return clientListDto.ToList();
        }

        public async Task<ClientDto> GetByIdAsync(Guid clientId)
        {
            var clientEntity = await repository.GetByIdAsync(clientId);
            // Validaciones
            if (clientEntity == null) {
                throw new ArgumentException($"El cliente con el id {clientId} no existe");
            }
            var clientDto = mapper.Map<ClientDto>(clientEntity);
            return clientDto;
        }

        public async Task UpdateAsync(Guid clientId, ClientCreateUpdateDto client)
        {
            // Validaciones
            var validationResult = await clientCUDtoValidator.ValidateAsync(client);
            if (!validationResult.IsValid) {
                var errorList = validationResult.Errors.Select(
                    e => e.ErrorMessage
                );
                var errorString = string.Join(" - ", errorList);
                throw new ArgumentException(errorString);
            }
            
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
            mapper.Map(client, clientEntity);

            //Persistencia objeto
            await repository.UpdateAsync(clientEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return;
        }

        public PaginatedList<ClientDto> GetAllPaginated(int limit, int offset)
        {
            var consulta = repository.GetAll();
            var totalConsulta = consulta.Count();
            if (limit > totalConsulta) {
                limit = totalConsulta;
            }
            var clientDtoList = consulta.Skip(offset).Take(limit).Select(b => mapper.Map<ClientDto>(b));

            var result = new PaginatedList<ClientDto>();
            result.Total = clientDtoList.Count();
            result.List = clientDtoList.ToList();

            return result;
        }

        public List<ClientDto> GetAllByNameEmail(string name, string email)
        {
            var clientQuery = repository.GetAll();
            if (!string.IsNullOrEmpty(name)) 
            {
                clientQuery = clientQuery.Where(
                    c => c.Name.ToLower().Contains(name.ToLower()) ||
                    c.Name.ToLower().StartsWith(name.ToLower())
                );
            }
            if (!string.IsNullOrEmpty(email)) {
                clientQuery = clientQuery.Where(
                    c => c.Email.ToLower().Contains(email.ToLower()) ||
                    c.Email.ToLower().StartsWith(email.ToLower())
                );
            }

            var clientDtoList = clientQuery.Select(c => mapper.Map<ClientDto>(c));

            return clientDtoList.ToList();
        }
        public List<ClientDto> GetAllByCountryAddress(string country, string address)
        {
            var clientQuery = repository.GetAll();
            if (!string.IsNullOrEmpty(country)) 
            {
                clientQuery = clientQuery.Where(
                    c => c.Country.ToLower().Contains(country.ToLower()) ||
                    c.Country.ToLower().StartsWith(country.ToLower())
                );
            }
            if (!string.IsNullOrEmpty(address)) {
                clientQuery = clientQuery.Where(
                    c => c.Address.ToLower().Contains(address.ToLower()) ||
                    c.Address.ToLower().StartsWith(address.ToLower())
                );
            }

            var clientDtoList = clientQuery.Select(c => mapper.Map<ClientDto>(c));

            return clientDtoList.ToList();
        }
    }
}