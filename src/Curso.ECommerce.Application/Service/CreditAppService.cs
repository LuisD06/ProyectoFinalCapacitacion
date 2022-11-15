using AutoMapper;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.enums;
using Curso.ECommerce.Domain.models;
using Curso.ECommerce.Domain.repository;
using FluentValidation;

namespace Curso.ECommerce.Application.Service
{
    public class CreditAppService : ICreditAppService
    {
        private readonly ICreditRepository repository;
        private readonly IValidator<CreditCreateDto> creditCreateDtoValidator;
        private readonly IClientAppService clientService;
        private readonly IOrderAppService orderService;
        private readonly IMapper mapper;

        public CreditAppService(
            ICreditRepository repository, 
            IValidator<CreditCreateDto> creditCreateDtoValidator,
            IClientAppService clientService,
            IOrderAppService orderService,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.creditCreateDtoValidator = creditCreateDtoValidator;
            this.clientService = clientService;
            this.orderService = orderService;
            this.mapper = mapper;
        }
        public async Task<CreditDto> CreateAsync(CreditCreateDto credit)
        {
            var order = await orderService.GetByIdAsync(credit.OrderId);
            var creditValidation = await creditCreateDtoValidator.ValidateAsync(credit);
            if (!creditValidation.IsValid) {
                var errorList = creditValidation.Errors.Select(e => e.ErrorMessage);
                var errorString = string.Join(" - ", errorList);
                throw new ArgumentException(errorString);
            }
            // Mapeo dto => entity
            var creditEntity = mapper.Map<Credit>(credit);
            // Obtencion del id del cliente al cual pertenece la orden
            creditEntity.ClientId = order.ClientId;

            await repository.AddAsync(creditEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo entity => dto
            var creditListQuery = repository.GetAllIncluding(c => c.Client);
            var creditQuery = creditListQuery.Where(c => c.Id == creditEntity.Id).SingleOrDefault();
            var creditDto = mapper.Map<CreditDto>(creditQuery);
            return creditDto;
        }

        public async Task<bool> PayAsync(Guid creditId)
        {
            var creditEntity = await repository.GetByIdAsync(creditId);
            if (creditEntity == null) {
                throw new ArgumentException($"No existe un crédito con el id {creditId}");
            }
            // Proceso de pago del crédito
            // Actualización del estado del crédito
            creditEntity.Status = CreditStatus.Paid;
            await repository.UpdateAsync(creditEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ICollection<CreditDto> GetAll()
        {
            var creditListQuery = repository.GetAllIncluding(c => c.Client, c => c.Order);
            var creditList = creditListQuery.ToList();
            var creditDtoList = creditList.Select(c => mapper.Map<CreditDto>(c));

            return creditDtoList.ToList();
        }

        public async Task<CreditDto> GetByIdAsync(Guid creditId)
        {
            var creditQueryList = repository.GetAllIncluding(c => c.Client);
            var creditEntity = creditQueryList.Where(c => c.Id == creditId).SingleOrDefault();
            if (creditEntity == null) {
                throw new ArgumentException($"El crédito con el id {creditId} no existe");
            }   

            var creditDto = mapper.Map<CreditDto>(creditEntity);
            return creditDto;
        }

        // TODO: Implementar update credit
        public Task UpdateAsync(Guid orderId, OrderUpdateDto order)
        {
            throw new NotImplementedException();
        }
    }
}