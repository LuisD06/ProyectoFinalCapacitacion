using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ECommerce.Infraestructure.Repository
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<bool> ProductExist(string productName)
        {
            var response = await this.context.Set<Product>()
                           .AnyAsync(p => p.Name.ToUpper() == productName.ToUpper());

            return response;
        }

        public async Task<bool> ProductExist(string productName, int productId)
        {
            var query = this.context.Set<Product>()
                           .Where(p => p.Id != productId)
                           .Where(p => p.Name.ToUpper() == productName.ToUpper())
                           ;

            var response = await query.AnyAsync();

            return response;
        }
    }
}