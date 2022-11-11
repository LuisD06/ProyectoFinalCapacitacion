using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Infraestructure.Repository
{
    public class OrderRepository : EfRepository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(ECommerceDbContext context) : base(context)
        {

        }
    }
}