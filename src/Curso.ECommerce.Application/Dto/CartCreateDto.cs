using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.ECommerce.Application.Dto
{
    public class CartCreateDto
    {

        [Required]
        public Guid ClientId { get; set; }

        public virtual ICollection<CartItemCreateUpdateDto> CartItems { get; set; } = new List<CartItemCreateUpdateDto>(); 

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CancellationDate { get; set; }


        [Required]
        public decimal Total { get; set; }

        public string? Notes { get; set; }
    }
}