using System.ComponentModel.DataAnnotations;

namespace Curso.ECommerce.Domain.Models
{
    public class OrderItem
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        public long Quantity { get; set; }

        public decimal Price { get; set; }

        public string? Notes { get; set; }
    }
}