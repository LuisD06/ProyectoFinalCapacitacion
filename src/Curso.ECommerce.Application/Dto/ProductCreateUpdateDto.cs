using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain;

namespace Curso.ECommerce.Application.Dto
{
    public class ProductCreateUpdateDto
    {

        [Required]
        [StringLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Notes { get; set; }
        public DateTime? Expiration { get; set; }
        public int? Stock { get; set; }

        [Required]
        public string BrandId { get; set; }

        [Required]
        public string ProductTypeId { get; set; }
    }
}