using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Domain.models
{
    public class CartItem
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public Guid CartId { get; set; }

        public virtual Cart Cart { get; set; }

        [Required]
        public long Quantity { get; set; }

        public decimal Price { get; set; }

        public string? Notes { get; set; }
    }
}