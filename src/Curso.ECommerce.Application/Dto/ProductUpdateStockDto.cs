using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ECommerce.Application.Dto
{
    public class ProductUpdateStockDto
    {
        [Required]
        public int? Stock { get; set; }
    }
}