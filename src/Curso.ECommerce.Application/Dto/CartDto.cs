using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ECommerce.Application.Dto
{
    public class CartDto
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public virtual ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>(); 

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CancellationDate { get; set; }


        [Required]
        public decimal Total { get; set; }

        public string? Notes { get; set; }


        public void AddCartItem(CartItemDto cartItem)
        {
            CartItems.Add(cartItem);
        }
    }
}