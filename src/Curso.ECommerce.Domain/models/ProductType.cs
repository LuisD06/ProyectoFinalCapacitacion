using System.ComponentModel.DataAnnotations;

namespace Curso.ECommerce.Domain.Models
{
    public class ProductType
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }
    }
}