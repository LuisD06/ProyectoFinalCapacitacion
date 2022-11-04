using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.enums;

namespace Curso.ECommerce.Domain.models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

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