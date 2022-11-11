using Curso.ECommerce.Domain.models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Domain.repository
{
    public interface ICartRepository : IRepository<Cart, Guid>
    {

    }
}