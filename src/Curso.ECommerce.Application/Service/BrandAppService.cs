using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Application.Service
{
    public class BrandAppService : IBrandAppService
    {
        private readonly IBrandRepository repository;
        public BrandAppService(IBrandRepository repository)
        {
            this.repository = repository;
        }
        public async Task<BrandDto> CreateAsync(BrandCreateUpdateDto brand)
        {
            // Validaciones
            var brandExist = await repository.BrandExist(brand.Name);
            if (brandExist)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {brand.Name}");
            }

            // Mapeo Dto => Entidad
            var brandEntity = new Brand();
            brandEntity.Name = brand.Name;

            // Persistencia del objeto
            brandEntity = await repository.AddAsync(brandEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo Entidad => Dto
            var createdBrand = new BrandDto();
            createdBrand.Name = brandEntity.Name;
            createdBrand.Id = brandEntity.Id;

            // TODO: Enviar un correo electronica... 

            return createdBrand;
        }

        public async Task<bool> DeleteAsync(int brandId)
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

            var brandListDto = from b in brandList
                               select new BrandDto()
                               {
                                   Id = b.Id,
                                   Name = b.Name
                               };

            return brandListDto.ToList();
        }

        public async Task UpdateAsync(int id, BrandCreateUpdateDto brand)
        {
            var brandEntity = await repository.GetByIdAsync(id);
            if (brandEntity == null)
            {
                throw new ArgumentException($"La marca con el id: {id}, no existe");
            }

            var brandExist = await repository.BrandExist(brand.Name, id);
            if (brandExist)
            {
                throw new ArgumentException($"Ya existe una marca con el nombre {brand.Name}");
            }

            //Mapeo Dto => Entidad
            brandEntity.Name = brand.Name;

            //Persistencia objeto
            await repository.UpdateAsync(brandEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return;
        }
    }
}