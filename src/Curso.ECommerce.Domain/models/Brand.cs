using System.ComponentModel.DataAnnotations;
namespace Curso.ECommerce.Domain.Models
{
    public class Brand
    {
        [Required]
        [StringLength(12)]
        public string Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Name { get; set; }


        
    }
}