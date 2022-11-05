using System.ComponentModel.DataAnnotations;

namespace Curso.ECommerce.Domain.Models
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