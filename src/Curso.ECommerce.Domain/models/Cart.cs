using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Domain.models
{
    public class Cart
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CancellationDate { get; set; }


        [Required]
        public decimal Total { get; set; }

        public string? Notes { get; set; }


        public void AddCartItem(CartItem cartItem)
        {

            cartItem.Cart = this;
            CartItems.Add(cartItem);
        }
    }
}