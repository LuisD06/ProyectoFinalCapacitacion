using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ECommerce.Application.Dto
{
    public class CartItemCreateUpdateDto {

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public long Quantity { get; set; }

        public string? Notes { get; set; }
    }
}