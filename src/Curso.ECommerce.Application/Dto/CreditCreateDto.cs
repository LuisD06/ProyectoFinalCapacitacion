
using System.ComponentModel.DataAnnotations;
using Curso.ECommerce.Domain.enums;

namespace Curso.ECommerce.Application.Dto
{
    public class CreditCreateDto
    {
        [Required]
        public decimal Tax { get; set; }
        [Required]
        public int Payments { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public Guid OrderId { get; set; }

    }
}