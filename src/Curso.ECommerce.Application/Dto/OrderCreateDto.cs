using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.enums;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Application.Dto
{
    public class OrderCreateDto
    {
        [Required]
        public Guid ClientId { get; set; }


        [Required]
        public DateTime Date { get; set; }

        public DateTime? CancellationDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }


        [Required]
        public decimal Total { get; set; }

        public string? Notes { get; set; }


        public virtual ICollection<OrderItemCreateUpdateDto>? OrderItems { get; set; }
    }
}