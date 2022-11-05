using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Application.Service
{
    public class ProductTypeAppService : IProductTypeAppService
    {
        private readonly IProductTypeRepository repository;
        public ProductTypeAppService(IProductTypeRepository repository)
        {
            this.repository = repository;

        }
        public async Task<ProductTypeDto> CreateAsync(ProductTypeCreateUpdateDto productType)
        {
            // Validaciones
            var productTypeExist = await repository.ProductTypeExist(productType.Name);
            if (productTypeExist)
            {
                throw new ArgumentException($"Ya existe un tipo de producto con el nombre {productType.Name}");
            }

            // Mapeo Dto => Entidad
            var productTypeEntity = new ProductType();
            productTypeEntity.Name = productType.Name;

            // Persistencia del objeto
            productTypeEntity = await repository.AddAsync(productTypeEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo Entidad => Dto
            var createdProductType = new ProductTypeDto();
            createdProductType.Name = productTypeEntity.Name;
            createdProductType.Id = productTypeEntity.Id;

            // TODO: Enviar un correo electronica... 

            return createdProductType;
        }

        public async Task<bool> DeleteAsync(int productTypeId)
        {
            //Reglas Validaciones... 
            var productTypeEntity = await repository.GetByIdAsync(productTypeId);
            if (productTypeEntity == null)
            {
                throw new ArgumentException($"El tipo de producto con el id: {productTypeId}, no existe");
            }

            repository.Delete(productTypeEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ICollection<ProductTypeDto> GetAll()
        {
            var productTypeList = repository.GetAll();

            var productTypeListDto = from b in productTypeList
                               select new ProductTypeDto()
                               {
                                   Id = b.Id,
                                   Name = b.Name
                               };

            return productTypeListDto.ToList();
        }

        public async Task UpdateAsync(int productTypeId, ProductTypeCreateUpdateDto productType)
        {
            var productTypeEntity = await repository.GetByIdAsync(productTypeId);
            if (productTypeEntity == null)
            {
                throw new ArgumentException($"El tipo de procuto con el id: {productTypeId}, no existe");
            }

            var productTypeExist = await repository.ProductTypeExist(productType.Name, productTypeId);
            if (productTypeExist)
            {
                throw new ArgumentException($"Ya existe un tipo de producto con el nombre {productType.Name}");
            }

            //Mapeo Dto => Entidad
            productTypeEntity.Name = productType.Name;

            //Persistencia objeto
            await repository.UpdateAsync(productTypeEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return;
        }
    }
}