using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ECommerce.Application.Dto
{
    public class CartItemDto
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }


        [Required]
        public Guid CartId { get; set; }

        [Required]
        public long Quantity { get; set; }

        public decimal Price { get; set; }

        public string? Notes { get; set; }
    }
}