using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Domain.Repository
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        ///<summary>
        ///Verify if a product name exists in database
        ///</summary>
        Task<bool> ProductExist(string brandName);
        ///<summary>
        ///Verify if a product exists in database, excluding itself
        ///</summary>
        Task<bool> ProductExist(string brandName, Guid brandId);
    }
}