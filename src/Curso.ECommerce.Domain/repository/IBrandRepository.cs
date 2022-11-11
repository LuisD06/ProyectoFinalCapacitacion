using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Domain.Repository
{
    public interface IBrandRepository : IRepository<Brand, string>
    {
        ///<summary>
        ///Verify if a brand name exists in database
        ///</summary>
        Task<bool> BrandExist(string brandName);
        ///<summary>
        ///Verify if a brand exists in database, excluding itself
        ///</summary>
        Task<bool> BrandExist(string brandName, string brandId);
    }
}