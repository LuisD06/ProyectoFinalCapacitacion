using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ECommerce.Domain.models
{
    public class OrderItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        public long Quantity { get; set; }

        public decimal Price { get; set; }

        public string? Notes { get; set; }
    }
}