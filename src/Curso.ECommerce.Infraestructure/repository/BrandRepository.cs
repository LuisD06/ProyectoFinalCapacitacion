using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ECommerce.Infraestructure.Repository
{
    public class BrandRepository : EfRepository<Brand, string>, IBrandRepository
    {
        public BrandRepository(ECommerceDbContext context) : base(context)
        {
            
        }

        public async Task<bool> BrandExist(string brandName)
        {
            var response = await this.context.Set<Brand>()
                           .AnyAsync(b => b.Name.ToUpper() == brandName.ToUpper());

            return response;
        }

        public async Task<bool> BrandExist(string brandName, string brandId)
        {
            var query = this.context.Set<Brand>()
                           .Where(b => b.Id != brandId)
                           .Where(b => b.Name.ToUpper() == brandName.ToUpper())
                           ;

            var response = await query.AnyAsync();

            return response;
        }
    }
}