using System.ComponentModel.DataAnnotations;
using Curso.ECommerce.Domain;

namespace Curso.ECommerce.Application.Dto
{
    public class BrandDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }
    }
}