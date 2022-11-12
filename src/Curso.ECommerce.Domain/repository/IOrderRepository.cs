using Curso.ECommerce.Domain.Models;


namespace Curso.ECommerce.Domain.Repository
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}