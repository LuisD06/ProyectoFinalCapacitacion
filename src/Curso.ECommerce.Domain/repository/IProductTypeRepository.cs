using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Domain.Repository
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        ///<summary>
        ///Verify if a brand name exists in database
        ///</summary>
        Task<bool> ProductTypeExist(string productTypeName);
        ///<summary>
        ///Verify if a brand exists in database, excluding itself
        ///</summary>
        Task<bool> ProductTypeExist(string productTypeName, int productTypeId);
    }
}