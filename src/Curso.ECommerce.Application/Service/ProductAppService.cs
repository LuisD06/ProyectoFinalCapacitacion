using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Application.Service
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository repository;

        public ProductAppService(IProductRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ProductDto> CreateAsync(ProductCreateUpdateDto product)
        {
            // Validaciones
            var productExist = await repository.ProductExist(product.Name);
            if (productExist)
            {
                throw new ArgumentException($"Ya existe un producto con el nombre {product.Name}");
            }

            // Mapeo Dto => Entidad
            var productEntity = new Product();
            productEntity.Name = product.Name;
            

            // Persistencia del objeto
            productEntity = await repository.AddAsync(productEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo Entidad => Dto
            var createdProduct = new ProductDto();
            createdProduct.Name = productEntity.Name;
            createdProduct.Id = productEntity.Id;

            // TODO: Enviar un correo electronica... 

            return createdProduct;
        }

        public Task<bool> DeleteAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int productId, ProductCreateUpdateDto product)
        {
            throw new NotImplementedException();
        }
    }
}