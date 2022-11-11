using System.Text.Json;
using AutoMapper;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using FluentValidation;

namespace Curso.ECommerce.Application.Service
{
    public class BrandAppService : IBrandAppService
    {
        private readonly IBrandRepository repository;
        private readonly IMapper mapper;
        private readonly IValidator<BrandCreateUpdateDto> brandCreateUpdateValidator;
        public BrandAppService(IBrandRepository repository, IMapper mapper, IValidator<BrandCreateUpdateDto> brandCreateUpdateValidator)
        {
            this.brandCreateUpdateValidator = brandCreateUpdateValidator;
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<BrandDto> CreateAsync(BrandCreateUpdateDto brand)
        {
            // Validaciones
            var validationResult = await brandCreateUpdateValidator.ValidateAsync(brand);
            if (!validationResult.IsValid) {
                var errorList = validationResult.Errors.Select(
                    e => e.ErrorMessage
                );
                var errorString = string.Join(" - ", errorList);
                throw new ArgumentException(errorString);
            }

            var brandExist = await repository.BrandExist(brand.Name);
            if (brandExist)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {brand.Name}");
            }

            // Mapeo Dto => Entidad
            var brandEntity = mapper.Map<Brand>(brand);
            Guid guidToken = Guid.NewGuid();
            brandEntity.Id =  guidToken.ToString("N").Substring(0,12).ToUpper();

            // Persistencia del objeto
            brandEntity = await repository.AddAsync(brandEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo Entidad => Dto
            var createdBrand = mapper.Map<BrandDto>(brandEntity);
            

            return createdBrand;
        }

        public async Task<bool> DeleteAsync(string brandId)
        {
            //Reglas Validaciones... 
            var brandEntity = await repository.GetByIdAsync(brandId);
            if (brandEntity == null)
            {
                throw new ArgumentException($"La marca con el id: {brandId}, no existe");
            }

            repository.Delete(brandEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ICollection<BrandDto> GetAll()
        {
            var brandList = repository.GetAll();
            // Mapeo item Brand => BrandDto
            var brandListDto = brandList.Select(b => mapper.Map<BrandDto>(b));

            return brandListDto.ToList();
        }

        public async Task UpdateAsync(string brandId, BrandCreateUpdateDto brand)
        {
            // Validaciones
            var validationResult = await brandCreateUpdateValidator.ValidateAsync(brand);
            if (!validationResult.IsValid) {
                var errorList = validationResult.Errors.Select(
                    e => e.ErrorMessage
                );
                var errorString = string.Join(" - ", errorList);
                throw new ArgumentException(errorString);
            }

            var brandEntity = await repository.GetByIdAsync(brandId);
            if (brandEntity == null)
            {
                throw new ArgumentException($"La marca con el id: {brandId}, no existe");
            }

            var brandExist = await repository.BrandExist(brand.Name, brandId);
            if (brandExist)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {brand.Name}");
            }

            //Mapeo Dto => Entidad
            mapper.Map<BrandCreateUpdateDto,Brand>(brand, brandEntity);

            //Persistencia objeto
            await repository.UpdateAsync(brandEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return;
        }
    
        
    }
}