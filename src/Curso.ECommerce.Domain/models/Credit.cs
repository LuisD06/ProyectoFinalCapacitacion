using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.enums;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Domain.models
{
    public class Credit
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public decimal Tax { get; set; }
        [Required]
        public int Payments { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public CreditStatus Status { get; set; } 

        [Required]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}