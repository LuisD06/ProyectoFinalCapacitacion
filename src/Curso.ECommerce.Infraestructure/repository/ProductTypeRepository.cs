using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ECommerce.Infraestructure.Repository
{
    public class ProductTypeRepository : EfRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<bool> ProductTypeExist(string productTypeName)
        {
            var response = await this.context.Set<ProductType>()
                           .AnyAsync(t => t.Name.ToUpper() == productTypeName.ToUpper());

            return response;
        }

        public async Task<bool> ProductTypeExist(string productTypeName, int productTypeId)
        {
            var query = this.context.Set<ProductType>()
                           .Where(t => t.Id != productTypeId)
                           .Where(t => t.Name.ToUpper() == productTypeName.ToUpper())
                           ;

            var response = await query.AnyAsync();

            return response;
        }
    }
}