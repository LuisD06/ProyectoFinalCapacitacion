using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.models;
using Curso.ECommerce.Domain.repository;

namespace Curso.ECommerce.Infraestructure.repository
{
    public class CreditRepository : EfRepository<Credit, Guid>, ICreditRepository
    {
        public CreditRepository(ECommerceDbContext context) : base(context){
        }
    }
}