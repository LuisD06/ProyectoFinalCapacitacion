using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Domain.repository
{
    public interface ICreditRepository : IRepository<Credit, Guid>
    {
        
    }
}