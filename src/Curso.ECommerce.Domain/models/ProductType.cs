using System.ComponentModel.DataAnnotations;

namespace Curso.ECommerce.Domain.Models
{
    public class ProductType
    {
        [Required]
        [StringLength(8)]
        public string Id { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string? Name { get; set; }
    }
}