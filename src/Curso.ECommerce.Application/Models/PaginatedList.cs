using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ECommerce.Application.Models
{
    public class PaginatedList<T>  where T : class
    {
        public ICollection<T> List { get; set; }

        public long Total { get; set; }
    }
}