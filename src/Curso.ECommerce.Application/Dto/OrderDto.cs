using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.enums;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Application.Dto
{
    public class OrderDto
    {
        [Required]
        public Guid Id { get; set; }


        [Required]
        public Guid ClientId { get; set; }

        public virtual string? Client { get; set; }


        public virtual ICollection<OrderItemDto>? OrderItems { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CancellationDate { get; set; }


        [Required]
        public decimal Total { get; set; }

        public string? Notes { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

    }
}