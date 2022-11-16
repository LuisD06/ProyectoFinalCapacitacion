using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using FluentValidation;

namespace Curso.ECommerce.Application.Service
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        private readonly IValidator<ProductCreateUpdateDto> productCUDtoValidator;

        public ProductAppService(IProductRepository repository, IMapper mapper, IValidator<ProductCreateUpdateDto> productCUDtoValidator)
        {
            this.productCUDtoValidator = productCUDtoValidator;
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ProductDto> CreateAsync(ProductCreateUpdateDto product)
        {
            // Validaciones
            var validationResult = await productCUDtoValidator.ValidateAsync(product);
            if (!validationResult.IsValid) {
                var errorList = validationResult.Errors.Select(
                    e => e.ErrorMessage
                );
                var errorString = string.Join(" - ", errorList);
                throw new ArgumentException(errorString);
            }

            var productExist = await repository.ProductExist(product.Name);
            if (productExist)
            {
                throw new ArgumentException($"Ya existe un producto con el nombre {product.Name}");
            }

            // Mapeo Dto => Entidad
            var productEntity = mapper.Map<Product>(product);

            // Persistencia del objeto
            productEntity = await repository.AddAsync(productEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            // Mapeo Entidad => Dto

            var productListQuery = repository.GetAllIncluding(x => x.Brand, x => x.ProductType);
            var productQuery = productListQuery.Where(p => p.Id == productEntity.Id).SingleOrDefault();


            var createdProduct = mapper.Map<ProductDto>(productEntity);
            createdProduct.Brand = productQuery.Brand.Name;
            createdProduct.ProductType = productQuery.ProductType.Name;


            return createdProduct;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            //Reglas Validaciones... 
            var productEntity = await repository.GetByIdAsync(productId);
            if (productEntity == null)
            {
                throw new ArgumentException($"El producto con el id: {productId}, no existe");
            }

            repository.Delete(productEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ICollection<ProductDto> GetAll()
        {
            var productListQuery = repository.GetAllIncluding(x => x.Brand, x => x.ProductType);
            var productList = productListQuery.ToList();

            var productListDto = from p in productList
                                 select new ProductDto()
                                 {
                                     Brand = p.Brand.Name,
                                     BrandId = p.BrandId,
                                     Expiration = p.Expiration,
                                     Id = p.Id,
                                     Name = p.Name,
                                     Notes = p.Notes,
                                     Price = p.Price,
                                     HasTax = p.HasTax,
                                     ProductType = p.ProductType.Name,
                                     ProductTypeId = p.ProductTypeId,
                                     Stock = p.Stock
                                 };

            return productListDto.ToList();
        }

        public async Task UpdateAsync(Guid productId, ProductCreateUpdateDto product)
        {
            // Validaciones
            var validationResult = await productCUDtoValidator.ValidateAsync(product);
            if (!validationResult.IsValid) {
                var errorList = validationResult.Errors.Select(
                    e => e.ErrorMessage
                );
                var errorString = string.Join(" - ", errorList);
                throw new ArgumentException(errorString);
            }
            
            var productEntity = await repository.GetByIdAsync(productId);
            if (productEntity == null)
            {
                throw new ArgumentException($"El producto con el id: {productId}, no existe");
            }

            var productExist = await repository.ProductExist(product.Name, productId);
            if (productExist)
            {
                throw new ArgumentException($"Ya existe un producto con el nombre {product.Name}");
            }

            //Mapeo Dto => Entidad
            mapper.Map(product, productEntity);


            //Persistencia objeto
            await repository.UpdateAsync(productEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return;
        }

        public async Task<ProductDto> GetByIdAsync(Guid productId)
        {
            var query = repository.GetAllIncluding(p => p.ProductType, p => p.Brand);
            var product = query.Where(p => p.Id == productId);
            var productDto = product.Select(p => new ProductDto()
            {
                Brand = p.Brand.Name,
                BrandId = p.BrandId,
                Expiration = p.Expiration,
                Id = p.Id,
                Name = p.Name,
                Notes = p.Notes,
                Price = p.Price,
                ProductType = p.ProductType.Name,
                ProductTypeId = p.ProductTypeId,
                Stock = p.Stock
            }).SingleOrDefault();

            return productDto;
        }

        public async Task<ICollection<ProductDto>> GetAllByIdAsync(List<Guid> productIdList)
        {
            var consulta = repository.GetAllIncluding(p => p.Brand, p => p.ProductType);
            List<ProductDto> productDtoList = new List<ProductDto>();
            foreach (Guid productId in productIdList)
            {
                var productDto = consulta.Where(p => p.Id == productId)
                    .Select(p => new ProductDto()
                    {
                        Brand = p.Brand.Name,
                        BrandId = p.BrandId,
                        Expiration = p.Expiration,
                        Id = p.Id,
                        Name = p.Name,
                        Notes = p.Notes,
                        Price = p.Price,
                        HasTax = p.HasTax,
                        ProductType = p.ProductType.Name,
                        ProductTypeId = p.ProductTypeId,
                        Stock = p.Stock
                    }).SingleOrDefault();
                if (productDto != null) {
                    productDtoList.Add(productDto);
                }
            }
            return productDtoList;
        }

        public async Task<ICollection<ProductDto>> GetAllByTypeAsync(string productType)
        {
            var query = repository.GetAllIncluding(p => p.Brand, p => p.ProductType);
            query = query.Where(p => 
                p.ProductType.Name.ToLower().Contains(productType.ToLower()) || 
                p.ProductType.Name.ToLower().StartsWith(productType.ToLower())
            );
            var productDtoList = query.Select(p => new ProductDto(){
                Brand = p.Brand.Name,
                BrandId = p.BrandId,
                Expiration = p.Expiration,
                Id = p.Id,
                Name = p.Name,
                Notes = p.Notes,
                Price = p.Price,
                ProductType = p.ProductType.Name,
                ProductTypeId = p.ProductTypeId,
                Stock = p.Stock
            });

            return productDtoList.ToList();
        }
        public async Task<ICollection<ProductDto>> GetAllByTypeAsync(string productType, Guid productId)
        {
            var query = repository.GetAllIncluding(p => p.Brand, p => p.ProductType);
            query = query.Where(p => p.Id != productId);
            query = query.Where(p => 
                p.ProductType.Name.ToLower().Contains(productType.ToLower()) || 
                p.ProductType.Name.ToLower().StartsWith(productType.ToLower())
            );
            var productDtoList = query.Select(p => new ProductDto(){
                Brand = p.Brand.Name,
                BrandId = p.BrandId,
                Expiration = p.Expiration,
                Id = p.Id,
                Name = p.Name,
                Notes = p.Notes,
                Price = p.Price,
                ProductType = p.ProductType.Name,
                ProductTypeId = p.ProductTypeId,
                Stock = p.Stock
            });

            return productDtoList.ToList();
        }
    
        public async Task<ICollection<ProductDto>> GetAllByNameAsync(string productName)
        {
            var query = repository.GetAllIncluding(p => p.Brand, p => p.ProductType);
            query = query.Where(p => 
                p.Name.ToLower().StartsWith(productName.ToLower()) || 
                p.Name.ToLower().Contains(productName.ToLower())
            );

            var productDtoList = query.Select(p => new ProductDto(){
                Brand = p.Brand.Name,
                BrandId = p.BrandId,
                Expiration = p.Expiration,
                Id = p.Id,
                Name = p.Name,
                Notes = p.Notes,
                Price = p.Price,
                ProductType = p.ProductType.Name,
                ProductTypeId = p.ProductTypeId,
                Stock = p.Stock
            });

            return productDtoList.ToList();
        }
        public async Task<ICollection<ProductDto>> GetAllByNameAsync(string productName, Guid productId)
        {
            var query = repository.GetAllIncluding(p => p.Brand, p => p.ProductType);
            query = query.Where(p => p.Id != productId);
            query = query.Where(p => 
                p.Name.ToLower().StartsWith(productName.ToLower()) || 
                p.Name.ToLower().Contains(productName.ToLower())
            );

            var productDtoList = query.Select(p => new ProductDto(){
                Brand = p.Brand.Name,
                BrandId = p.BrandId,
                Expiration = p.Expiration,
                Id = p.Id,
                Name = p.Name,
                Notes = p.Notes,
                Price = p.Price,
                ProductType = p.ProductType.Name,
                ProductTypeId = p.ProductTypeId,
                Stock = p.Stock
            });

            return productDtoList.ToList();
        }
    
        public async Task UpdateStockAsync(Guid productId, ProductUpdateStockDto product)
        {
            var productEntity = await repository.GetByIdAsync(productId);
            if (productEntity == null)
            {
                throw new ArgumentException($"El producto con el id: {productId}, no existe");
            }

            //Mapeo Dto => Entidad
            productEntity.Stock = product.Stock;
            //Persistencia objeto
            await repository.UpdateAsync(productEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return;
        }
    
        public PaginatedList<ProductDto> GetAllPaginated(int limit, int offset)
        {
            var consulta = repository.GetAllIncluding(p => p.ProductType, p => p.Brand);
            var totalConsulta = consulta.Count();
            if (limit > totalConsulta) {
                limit = totalConsulta;
            }
            var productDtoList = consulta.Skip(offset).Take(limit).Select(p => mapper.Map<ProductDto>(p));

            var result = new PaginatedList<ProductDto>();
            result.Total = productDtoList.Count();
            result.List = productDtoList.ToList();

            return result;
        }
    
        public List<ProductDto> GetAllByNameType(string productName, string productType)
        {
            var productQuery = repository.GetAllIncluding(p => p.ProductType, p => p.Brand);
            if (!string.IsNullOrEmpty(productName)) {
                productQuery = productQuery.Where(
                    p => p.Name.ToLower().Contains(productName.ToLower()) ||
                    p.Name.ToLower().StartsWith(productName.ToLower())
                );
            }
            if (!string.IsNullOrEmpty(productType)) {
                productQuery = productQuery.Where(
                    p => p.ProductType.Name.ToLower().Contains(productType.ToLower()) ||
                    p.ProductType.Name.ToLower().StartsWith(productType.ToLower())
                );
            }

            // Mapeo entidad => dto
            var productDtoList = productQuery.Select(p => mapper.Map<ProductDto>(p));
            
            return productDtoList.ToList();
        }

        public List<ProductDto> GetAllByTaxPrice(bool hasTax, decimal minPrice = 0, decimal maxPrice = 0)
        {
            var productQuery = repository.GetAllIncluding(p => p.ProductType, p => p.Brand);
            productQuery  = productQuery.Where(p => p.HasTax == hasTax);
            if (minPrice < maxPrice) {
                productQuery = productQuery.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }
            if (minPrice > 0 && maxPrice <= 0) {
                productQuery = productQuery.Where(p => p.Price >= minPrice);
            }
            // Mapeo entidad => dto
            var productDtoList = productQuery.Select(p => mapper.Map<ProductDto>(p));

            return productDtoList.ToList();
            
            
        }


    }
}