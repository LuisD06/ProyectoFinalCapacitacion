using System.ComponentModel.DataAnnotations;
using Curso.ECommerce.Domain.enums;

namespace Curso.ECommerce.Domain.Models
{
    public class Order
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CancellationDate { get; set; }


        [Required]
        public decimal Total { get; set; }

        public string? Notes { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public void AddOrderItem(OrderItem orderItem)
        {

            orderItem.Order = this;
            OrderItems.Add(orderItem);
        }
    }
}