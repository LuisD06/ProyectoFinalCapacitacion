using System.ComponentModel.DataAnnotations;

namespace Curso.ECommerce.Application.Dto
{
    public class BrandCreateUpdateDto 
    {
        [Required]
        public string? Name { get; set; }
    }
}